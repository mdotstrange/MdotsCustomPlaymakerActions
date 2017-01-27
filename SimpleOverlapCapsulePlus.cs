using UnityEngine;
using System.Collections.Generic;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.Physics)]
    [Tooltip("Returns hit if an object is hit, returns hitpoint and object- DOES NOT work with Mesh or Terrain colliders")]
    public class SimpleOverlapCapsulePlus : FsmStateAction
    {
        [ActionSection("Overlap Sphere Settings")]
        public FsmGameObject scanOrigin;
        public FsmVector3 scanOriginV3;
        public FsmGameObject capsuleEnd;
        public FsmVector3 capsuleEndV3;
        [Tooltip("The size of the overlap sphere.")]
        public FsmFloat scanRange;
        [ActionSection("Filter")]
        [UIHint(UIHint.Layer)]
        [Tooltip("Pick only from these layers.")]
        public FsmInt[] layerMask;
        [Tooltip("Invert the mask, so you pick from all layers except those defined above.")]
        public FsmBool invertMask;
        [Tooltip("Set to true to ignore colliders set to trigger.")]
        public FsmBool ignoreTriggerColliders;
        public FsmEvent ErrorEvent;
        public FsmEvent hitEvent;
        public FsmEvent noHitEvent;
        public FsmInt repeatInterval;
        public FsmGameObject hitObject;
        public FsmVector3 hitPoint;
        int repeat;


        public bool everyFrame;

        public override void Reset()
        {

            ErrorEvent = null;
            scanRange = null;
            layerMask = new FsmInt[0];
            invertMask = false;
            ignoreTriggerColliders = false;
        }


        public override void OnEnter()
        {
            DoSimpleOverlap();

            if (repeatInterval.Value == 0)
            {
                Finish();
            }
        }


        public override void OnUpdate()
        {
            repeat--;



            if (repeat == 0)
            {
                DoSimpleOverlap();
            }
        }

        void DoSimpleOverlap()
        {



            repeat = repeatInterval.Value;

            if (scanOrigin.Value != null)
            {
                scanOriginV3.Value = scanOrigin.Value.transform.position;
            }

            if (capsuleEnd.Value != null)
            {

                capsuleEndV3.Value = capsuleEnd.Value.transform.position;
            }


            if (ignoreTriggerColliders.Value == true)
            {
                Collider[] colliders = Physics.OverlapCapsule(scanOriginV3.Value, capsuleEndV3.Value, scanRange.Value, ActionHelpers.LayerArrayToLayerMask(layerMask, invertMask.Value), QueryTriggerInteraction.Ignore);
                if (colliders.Length == 0)
                {
                    Fsm.Event(noHitEvent);
                } else
                {
                    var list = new List<Collider>(colliders);

                    for (int index = 0; index < list.Count; index++)
                    {
                        var i = list[index].gameObject;
                        if (i == scanOrigin.Value)
                        {
                            list.RemoveAt(index);
                        }

                    }


                    if (list.Count != 0)
                    {
                        Vector3 contactPoint;
                        bool contactPointSuccess = ClosestPointOnSurface(list[0], scanOriginV3.Value, 1.0f, out contactPoint);
                        hitPoint.Value = contactPoint;
                        hitObject.Value = list[0].gameObject;
                        Fsm.Event(hitEvent);
                    } else
                    {
                        Fsm.Event(noHitEvent);
                    }



                }

            } else
            {
                Collider[] colliders = Physics.OverlapCapsule(scanOriginV3.Value, capsuleEndV3.Value, scanRange.Value, ActionHelpers.LayerArrayToLayerMask(layerMask, invertMask.Value), QueryTriggerInteraction.Collide);
                if (colliders.Length == 0)
                {
                    Fsm.Event(noHitEvent);
                } else
                {
                    var list = new List<Collider>(colliders);
                    //Debug.Log("list count1 " + list.Count);


                    for (int index = 0; index < list.Count; index++)
                    {
                        //Debug.Log("i = " + list[index].gameObject);
                        //Debug.Log("scan origin" + scanOrigin.Value);
                        var i = list[index].gameObject;
                        if (i == scanOrigin.Value)
                        {
                            //Debug.Log("remove" + i);
                            list.RemoveAt(index);
                        }
                        //Debug.Log("list count2 " + list.Count);


                    }

                    //Debug.Log("list count3 " + list.Count);



                    if (list.Count != 0)
                    {
                        Vector3 contactPoint;
                        bool contactPointSuccess = ClosestPointOnSurface(list[0], scanOriginV3.Value, 1.0f, out contactPoint);
                        hitPoint.Value = contactPoint;
                        hitObject.Value = list[0].gameObject;
                        Fsm.Event(hitEvent);
                    } else
                    {
                        Fsm.Event(noHitEvent);
                    }
                }

            }
        }
        // following code is from Royston Ross https://github.com/IronWarrior/SuperCharacterController/blob/master/Assets/SuperCharacterController/SuperCharacterController/Core/SuperCollider.cs
        public static bool ClosestPointOnSurface(Collider collider, Vector3 to, float radius, out Vector3 closestPointOnSurface)
        {
            if (collider is BoxCollider)
            {
                closestPointOnSurface = ClosestPointOnSurface((BoxCollider)collider, to);
                return true;
            } else if (collider is SphereCollider)
            {
                closestPointOnSurface = ClosestPointOnSurface((SphereCollider)collider, to);
                return true;
            } else if (collider is CapsuleCollider)
            {
                closestPointOnSurface = ClosestPointOnSurface((CapsuleCollider)collider, to);
                return true;
            } else if (collider is CharacterController)
            {
                closestPointOnSurface = ClosestPointOnSurface((CharacterController)collider, to);
                return true;
            }

            Debug.Log(string.Format("{0} does not have an implementation for ClosestPointOnSurface; GameObject.Name='{1}'", collider.GetType(), collider.gameObject.name));
            closestPointOnSurface = Vector3.zero;
            return false;
        }

        public static Vector3 ClosestPointOnSurface(SphereCollider collider, Vector3 to)
        {
            Vector3 p;

            p = to - (collider.transform.position + collider.center);
            p.Normalize();

            p *= collider.radius * collider.transform.localScale.x;
            p += collider.transform.position + collider.center;

            return p;
        }

        public static Vector3 ClosestPointOnSurface(BoxCollider collider, Vector3 to)
        {
            // Cache the collider transform
            var ct = collider.transform;

            // Firstly, transform the point into the space of the collider
            var local = ct.InverseTransformPoint(to);

            // Now, shift it to be in the center of the box
            local -= collider.center;

            //Pre multiply to save operations.
            var halfSize = collider.size * 0.5f;

            // Clamp the points to the collider's extents
            var localNorm = new Vector3(
                    Mathf.Clamp(local.x, -halfSize.x, halfSize.x),
                    Mathf.Clamp(local.y, -halfSize.y, halfSize.y),
                    Mathf.Clamp(local.z, -halfSize.z, halfSize.z)
                );

            //Calculate distances from each edge
            var dx = Mathf.Min(Mathf.Abs(halfSize.x - localNorm.x), Mathf.Abs(-halfSize.x - localNorm.x));
            var dy = Mathf.Min(Mathf.Abs(halfSize.y - localNorm.y), Mathf.Abs(-halfSize.y - localNorm.y));
            var dz = Mathf.Min(Mathf.Abs(halfSize.z - localNorm.z), Mathf.Abs(-halfSize.z - localNorm.z));

            // Select a face to project on
            if (dx < dy && dx < dz)
            {
                localNorm.x = Mathf.Sign(localNorm.x) * halfSize.x;
            } else if (dy < dx && dy < dz)
            {
                localNorm.y = Mathf.Sign(localNorm.y) * halfSize.y;
            } else if (dz < dx && dz < dy)
            {
                localNorm.z = Mathf.Sign(localNorm.z) * halfSize.z;
            }

            // Now we undo our transformations
            localNorm += collider.center;

            // Return resulting point
            return ct.TransformPoint(localNorm);
        }

        // Courtesy of Moodie
        public static Vector3 ClosestPointOnSurface(CapsuleCollider collider, Vector3 to)
        {
            Transform ct = collider.transform; // Transform of the collider

            float lineLength = collider.height - collider.radius * 2; // The length of the line connecting the center of both sphere
            Vector3 dir = Vector3.up;

            Vector3 upperSphere = dir * lineLength * 0.5f + collider.center; // The position of the radius of the upper sphere in local coordinates
            Vector3 lowerSphere = -dir * lineLength * 0.5f + collider.center; // The position of the radius of the lower sphere in local coordinates

            Vector3 local = ct.InverseTransformPoint(to); // The position of the controller in local coordinates

            Vector3 p = Vector3.zero; // Contact point
            Vector3 pt = Vector3.zero; // The point we need to use to get a direction vector with the controller to calculate contact point

            if (local.y < lineLength * 0.5f && local.y > -lineLength * 0.5f) // Controller is contacting with cylinder, not spheres
                pt = dir * local.y + collider.center;
            else if (local.y > lineLength * 0.5f) // Controller is contacting with the upper sphere 
                pt = upperSphere;
            else if (local.y < -lineLength * 0.5f) // Controller is contacting with lower sphere
                pt = lowerSphere;

            //Calculate contact point in local coordinates and return it in world coordinates
            p = local - pt;
            p.Normalize();
            p = p * collider.radius + pt;
            return ct.TransformPoint(p);

        }

        // Courtesy of Moodie
        public static Vector3 ClosestPointOnSurface(CharacterController collider, Vector3 to)
        {
            Transform ct = collider.transform; // Transform of the collider

            float lineLength = collider.height - collider.radius * 2; // The length of the line connecting the center of both sphere
            Vector3 dir = Vector3.up;

            Vector3 upperSphere = dir * lineLength * 0.5f + collider.center; // The position of the radius of the upper sphere in local coordinates
            Vector3 lowerSphere = -dir * lineLength * 0.5f + collider.center; // The position of the radius of the lower sphere in local coordinates

            Vector3 local = ct.InverseTransformPoint(to); // The position of the controller in local coordinates

            Vector3 p = Vector3.zero; // Contact point
            Vector3 pt = Vector3.zero; // The point we need to use to get a direction vector with the controller to calculate contact point

            if (local.y < lineLength * 0.5f && local.y > -lineLength * 0.5f) // Controller is contacting with cylinder, not spheres
                pt = dir * local.y + collider.center;
            else if (local.y > lineLength * 0.5f) // Controller is contacting with the upper sphere 
                pt = upperSphere;
            else if (local.y < -lineLength * 0.5f) // Controller is contacting with lower sphere
                pt = lowerSphere;

            //Calculate contact point in local coordinates and return it in world coordinates
            p = local - pt;
            p.Normalize();
            p = p * collider.radius + pt;
            return ct.TransformPoint(p);
        }
    }
}
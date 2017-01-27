// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Keywords: boxcast box ray physics

using UnityEngine;
using System.Collections;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.Physics)]
    [Tooltip("Fires a box at a direction. Like a Spherecast except with a Box. Returns hit/no hit + extras. Option to ignore colliders set to trigger.")]
    public class BoxCast : FsmStateAction
    {
        [Tooltip("Start box at game object position.")]
        public FsmOwnerDefault fromGameObject;

  
        private Vector3 center;

        [Tooltip("Half the size of the box in each dimension.")]
        public FsmVector3 halfExtents;

        [Tooltip("The direction in which to cast the box.")]
        public FsmVector3 direction;

        [Tooltip("Rotation of the box.")]
        public FsmQuaternion orientation;

        [Tooltip("The max length of the cast.")]
        public FsmFloat maxDistance;

        [Tooltip("The length of the ray. Set to -1 for infinity.")]
        private FsmFloat distance;

        [Tooltip("Set to true to ignore colliders set to trigger.")]
        public FsmBool ignoreTriggerColliders;

        [ActionSection("Result")]

        [Tooltip("Event to send if the ray hits an object.")]
        [UIHint(UIHint.Variable)]
        public FsmEvent hitEvent;

        [Tooltip("Event to send if the ray does not hit any object.")]
        [UIHint(UIHint.Variable)]
        public FsmEvent noHitEvent;

        [Tooltip("Set a bool variable to true if hit something, otherwise false.")]
        [UIHint(UIHint.Variable)]
        public FsmBool storeDidHit;

        [Tooltip("Store the game object hit in a variable.")]
        [UIHint(UIHint.Variable)]
        public FsmGameObject storeHitObject;

        [UIHint(UIHint.Variable)]
        [Tooltip("Get the world position of the ray hit point and store it in a variable.")]
        public FsmVector3 storeHitPoint;

        [UIHint(UIHint.Variable)]
        [Tooltip("Get the normal at the hit point and store it in a variable.")]
        public FsmVector3 storeHitNormal;

        [UIHint(UIHint.Variable)]
        [Tooltip("Get the distance along the ray to the hit point and store it in a variable.")]
        public FsmFloat storeHitDistance;

        [ActionSection("Filter")]

        [Tooltip("Set how often to cast a ray. 0 = once, don't repeat; 1 = everyFrame; 2 = every other frame... \nSince raycasts can get expensive use the highest repeat interval you can get away with.")]
        public FsmInt repeatInterval;

        [UIHint(UIHint.Layer)]
        [Tooltip("Pick only from these layers.")]
        public FsmInt[] layerMask;

        [Tooltip("Invert the mask, so you pick from all layers except those defined above.")]
        public FsmBool invertMask;

        [ActionSection("Debug")]

        [Tooltip("The color to use for the debug line.")]
        public FsmColor debugColor;

        [Tooltip("Draw a debug line. Note: Check Gizmos in the Game View to see it in game.")]
        public FsmBool debug;



        int repeat;

        public override void Reset()
        {
            fromGameObject = null;
            
            direction = new FsmVector3 { UseVariable = true };
          
            
            hitEvent = null;
            noHitEvent = null;
            storeDidHit = null;
            storeHitObject = null;
            storeHitPoint = null;
            storeHitNormal = null;
            storeHitDistance = null;
            repeatInterval = 1;
            layerMask = new FsmInt[0];
            invertMask = false;
            debugColor = Color.yellow;
            debug = false;
            ignoreTriggerColliders = false;
            halfExtents = null;
            direction = null;
            orientation = null;
            maxDistance = null;
        

        }

        public override void OnEnter()
        {
            DoBoxCast();

            if (repeatInterval.Value == 0)
            {
                Finish();
            }
        }

        public override void OnUpdate()
        {
            DoBoxCast();        
        }

        void DoBoxCast()
        {
            center = Fsm.GetOwnerDefaultTarget(fromGameObject).transform.position;

            RaycastHit hitInfo;

            if (ignoreTriggerColliders.Value == true)
            {             
                Physics.BoxCast(center, halfExtents.Value, direction.Value, out hitInfo, orientation.Value = Quaternion.identity, maxDistance.Value, ActionHelpers.LayerArrayToLayerMask(layerMask, invertMask.Value), QueryTriggerInteraction.Ignore);
            } 
            else
            {            
                Physics.BoxCast(center, halfExtents.Value, direction.Value,out hitInfo, orientation.Value = Quaternion.identity, maxDistance.Value, ActionHelpers.LayerArrayToLayerMask(layerMask, invertMask.Value), QueryTriggerInteraction.Collide);
            }

            Fsm.RaycastHitInfo = hitInfo;
            var didHit = hitInfo.collider != null;
            storeDidHit.Value = didHit;

            if (didHit)
            {
                storeHitObject.Value = hitInfo.collider.GetComponent<Collider>().gameObject;
                storeHitPoint.Value = Fsm.RaycastHitInfo.point;
                storeHitNormal.Value = Fsm.RaycastHitInfo.normal;
                storeHitDistance.Value = Fsm.RaycastHitInfo.distance;
                Fsm.Event(hitEvent);
            }

           

        }
    }
}

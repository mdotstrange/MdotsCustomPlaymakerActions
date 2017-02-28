using UnityEngine;
// by MDS- important code from https://vimeo.com/9304844

namespace HutongGames.PlayMaker.Actions
{

[ActionCategory(ActionCategory.Physics)]
[Tooltip("Does basic flight pathfinding. Requires collider and rigid body with gravity set to false. Set rigid body interpolate to interpolate ")]
public class flightPathAction : FsmStateAction
{
        public FsmOwnerDefault owner;
        public FsmGameObject targetGo;
        public FsmFloat moveSpeed;
        public FsmBool stopAtTarget;
        public FsmFloat stoppingDistance;
        public FsmFloat rayDistance;     
        Rigidbody rb;
        Vector3 dir;
        Vector3 dir1; 
        bool dontMove;
        bool noHit;
        Transform target;
        GameObject go;

        [UIHint(UIHint.Layer)]
        [Tooltip("Pick only from these layers.")]
        public FsmInt[] layerMask;

        public bool debug;

        public override void OnPreprocess()
        {
            Fsm.HandleFixedUpdate = true;
        }

        public override void Reset()
        {
            moveSpeed = 30f;
            stoppingDistance = 0f;
            rayDistance = 20f;
            debug = false;
        }

        // Code that runs on entering the state.
        public override void OnEnter()
	{
            go = Fsm.GetOwnerDefaultTarget(owner);
            target = targetGo.Value.transform;
            rb = go.GetComponent<Rigidbody>();
            dontMove = false;

        }

	public override void OnFixedUpdate()
	{
            if (dontMove != true)
            {
                dir = (target.position - go.transform.position).normalized;
            } else
            {
                dir = (target.position - go.transform.position).normalized;
            }

            if (dontMove != true)
            {
                DoCasts();
            }

            CheckDistance();

            if (dir != Vector3.zero)
            {
                var rot = Quaternion.LookRotation(dir);
                rb.MoveRotation(Quaternion.Slerp(go.transform.rotation, rot, Time.deltaTime));

                if (!dontMove)
                {
                    rb.MovePosition(rb.position += go.transform.forward * moveSpeed.Value * Time.deltaTime);
                }

            }


        }

        void DoCasts()
        {
            noHit = true;
            RaycastHit hit;

            if (Physics.Raycast(go.transform.position, go.transform.forward, out hit, rayDistance.Value, ActionHelpers.LayerArrayToLayerMask(layerMask, false), QueryTriggerInteraction.Ignore))
            {
                noHit = false;
                if (hit.transform != go.transform)
                {
                    if (debug == true)
                    {
                        Debug.DrawLine(go.transform.position, hit.point, Color.red);
                    }
                    dir += hit.normal * 20;
                }
            }


            var leftR = go.transform.position;
            var rightR = go.transform.position;
            var up = go.transform.position;
            var down = go.transform.position;

            leftR.x -= 5;
            rightR.x += 5;
            up.y += 5;
            down.y -= 5;


            if (Physics.Raycast(leftR, go.transform.forward, out hit, rayDistance.Value, ActionHelpers.LayerArrayToLayerMask(layerMask, false), QueryTriggerInteraction.Ignore))
            {
                noHit = false;
                if (hit.transform != go.transform)
                {
                    if (debug == true)
                    {
                        Debug.DrawLine(go.transform.position, hit.point, Color.green);
                    }
                    dir += hit.normal * rayDistance.Value;
                }
            }

            if (Physics.Raycast(rightR, go.transform.forward, out hit, rayDistance.Value, ActionHelpers.LayerArrayToLayerMask(layerMask, false), QueryTriggerInteraction.Ignore))
            {
                noHit = false;
                if (hit.transform != go.transform)
                {
                    if (debug == true)
                    {
                        Debug.DrawLine(go.transform.position, hit.point, Color.yellow);
                    }
                    dir += hit.normal * rayDistance.Value;
                }
            }

            if (Physics.Raycast(up, go.transform.forward, out hit, rayDistance.Value, ActionHelpers.LayerArrayToLayerMask(layerMask, false), QueryTriggerInteraction.Ignore))
            {
                noHit = false;
                if (hit.transform != go.transform)
                {
                    if (debug == true)
                    {
                        Debug.DrawLine(go.transform.position, hit.point, Color.blue);
                    }
                    dir += hit.normal * rayDistance.Value;
                }
            }

            if (Physics.Raycast(down, go.transform.forward, out hit, rayDistance.Value, ActionHelpers.LayerArrayToLayerMask(layerMask, false), QueryTriggerInteraction.Ignore))
            {
                noHit = false;
                if (hit.transform != go.transform)
                {
                    if (debug == true)
                    {
                        Debug.DrawLine(go.transform.position, hit.point, Color.cyan);
                    }
                    dir += hit.normal * rayDistance.Value;
                }
            }


        }

        void CheckDistance()
        {
            var distance = Vector3.Distance(go.transform.position, target.position);
            dir1 = (target.position - go.transform.position).normalized;

            if (distance <= stoppingDistance.Value)
            {
                RaycastHit hitto;
                if (Physics.Raycast(go.transform.position, dir1, out hitto, Mathf.Infinity, ActionHelpers.LayerArrayToLayerMask(layerMask, false), QueryTriggerInteraction.Ignore))
                {

                    if (hitto.transform == target)
                    {
                        if(stopAtTarget.Value == true)
                        {
                            dontMove = true;
                        }
                       
                    } 
                    else
                    {
                        dontMove = false;
                    }
                }


            } else
            {

                dontMove = false;
            }
        }

    }

}

using UnityEngine;
using System.Collections.Generic;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.Physics)]
    [Tooltip("Returns hit if an object is hit, saves hit object.")]
    public class SimpleOverlapSphere : FsmStateAction
    {
        [ActionSection("Overlap Sphere Settings")]
        public FsmGameObject scanOrigin;
        public FsmVector3 scanOriginV3;
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
           

            if (ignoreTriggerColliders.Value == true)
            {
                Collider[] colliders = Physics.OverlapSphere(scanOriginV3.Value, scanRange.Value, ActionHelpers.LayerArrayToLayerMask(layerMask, invertMask.Value), QueryTriggerInteraction.Ignore);
                if (colliders.Length == 0)
                {
                    Fsm.Event(noHitEvent);
                } 
                else
                {
                   var list = new List<Collider>(colliders);
                    //Debug.Log(list.Count);

                    for (int index = 0; index < list.Count; index++)
                    {
                        var i = list[index];
                        if (i == scanOrigin.Value.gameObject.GetComponent<Collider>())
                        {
                            //Debug.Log("Scan origin  =" + scanOrigin.Value);
                            //Debug.Log("removed " + i);
                            list.RemoveAt(index);
                        }

                    }
                    //Debug.Log("List count" + list.Count);
                    if (list.Count != 0)
                    {
                        //Debug.Log("Hit event" + list.Count);
                        hitObject.Value = list[0].gameObject;
                        Fsm.Event(hitEvent);
                    }
                    else
                    {
                        //Debug.Log(" NO Hit event" + list.Count);
                        Fsm.Event(noHitEvent);
                    }
                   
                }

            }
            else
            {
                Collider[] colliders = Physics.OverlapSphere(scanOriginV3.Value, scanRange.Value, ActionHelpers.LayerArrayToLayerMask(layerMask, invertMask.Value), QueryTriggerInteraction.Collide);
                if (colliders.Length == 0)
                {
                    Fsm.Event(noHitEvent);
                }
                else
                {
                   var list = new List<Collider>(colliders);

                    for (int index = 0; index < list.Count; index++)
                    {
                        var i = list[index];
                        if (i == scanOrigin.Value)
                        {
                            list.RemoveAt(index);
                        }

                    }

                    if (list.Count != 0)
                    {
                        hitObject.Value = list[0].gameObject;
                        Fsm.Event(hitEvent);
                    } 
                    else
                    {
                        Fsm.Event(noHitEvent);
                    }
                }

            }
        }
    }
}
using UnityEngine;
using System.Collections.Generic;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.Physics)]
    [Tooltip("Returns hit if an object is hit, hit object also returns")]
    public class SimpleOverlapBox : FsmStateAction
    {
        [ActionSection("Overlap Box Settings")]
        public FsmGameObject scanOrigin;
        [Tooltip("The size of the overlap box.")]
        public FsmVector3 halfExtents;
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
        private Quaternion orientation;

        public bool everyFrame;

        public override void Reset()
        {

            ErrorEvent = null;
       
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
          
            //Debug.Log(" scan origin " + scanOrigin.Value);
            //Debug.Log(" scan origin =" + scanOrigin.Value.gameObject.transform.rotation);
            orientation = scanOrigin.Value.gameObject.transform.rotation;

            if (ignoreTriggerColliders.Value == true)
            {
                Collider[] colliders = Physics.OverlapBox(scanOrigin.Value.gameObject.transform.position, halfExtents.Value, orientation = Quaternion.identity, ActionHelpers.LayerArrayToLayerMask(layerMask, invertMask.Value), QueryTriggerInteraction.Ignore);
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
                        hitObject.Value = list[0].gameObject;
                        Fsm.Event(hitEvent);
                    } else
                    {
                        Fsm.Event(noHitEvent);
                    }

                
                }

            } else
            {
                Collider[] colliders = Physics.OverlapBox(scanOrigin.Value.gameObject.transform.position, halfExtents.Value, orientation = Quaternion.identity, ActionHelpers.LayerArrayToLayerMask(layerMask, invertMask.Value), QueryTriggerInteraction.Collide);
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
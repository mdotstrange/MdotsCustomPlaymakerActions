using UnityEngine;
using System.Collections.Generic;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.Array)]
    [Tooltip("Returns hit if an object is hitand stores hit objects in array")]
    public class OverlapSphereToArray : FsmStateAction
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

        [RequiredField]
        [UIHint(UIHint.Variable)]
        [Tooltip("The Array Variable to use.")]
        public FsmArray array;

 
        int repeat;


        public bool everyFrame;

        public override void Reset()
        {

            ErrorEvent = null;
            scanRange = null;
            layerMask = new FsmInt[0];
            invertMask = false;
            ignoreTriggerColliders = false;
            array = null;
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
                } else
                {
                    var list = new List<Collider>(colliders);
                

                    for (int index = 0; index < list.Count; index++)
                    {
                        var i = list[index];
                        if (i == scanOrigin.Value.gameObject.GetComponent<Collider>())
                        {
                        
                            list.RemoveAt(index);
                        }

                    }
                
                    if (list.Count != 0)
                    {

                        int count = list.Count;

                        if (count > 0)
                        {
                            array.Resize(array.Length + count);

                            for (int index = 0; index < list.Count; index++)
                            {
                                var i = list[index];
                                array.Set(array.Length - count, i.gameObject);
                                count--;
                            }

                           
                        }

                        Fsm.Event(hitEvent);
                    }
                    else
                    {
                        //Debug.Log(" NO Hit event" + list.Count);
                        Fsm.Event(noHitEvent);
                    }

                }

            } else
            {
                Collider[] colliders = Physics.OverlapSphere(scanOriginV3.Value, scanRange.Value, ActionHelpers.LayerArrayToLayerMask(layerMask, invertMask.Value), QueryTriggerInteraction.Collide);
                if (colliders.Length == 0)
                {
                    Fsm.Event(noHitEvent);
                } else
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

                        int count = list.Count;

                        if (count > 0)
                        {
                            array.Resize(array.Length + count);

                            for (int index = 0; index < list.Count; index++)
                            {
                                var i = list[index];
                                array.Set(array.Length - count, i.gameObject);
                                count--;
                            }


                        }

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
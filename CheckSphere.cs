// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Keywords: checksphere sphere ray physics

using UnityEngine;
using System.Collections.Generic;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.Physics)]
    [Tooltip("Returns a hit event if a collider is inside of the sphere. Option to ignore colliders set to trigger.")]
    public class CheckSphere : FsmStateAction
    {

        [ActionSection("Check Sphere Settings")]

        [Tooltip("Center of the sphere")]
        public FsmOwnerDefault position;

        [Tooltip("Radius of the sphere.")]
        public FsmFloat radius;



        [Tooltip("Set to true to ignore colliders set to trigger.")]
        public FsmBool ignoreTriggerColliders;


        [ActionSection("Filter")]

        [UIHint(UIHint.Layer)]
        [Tooltip("Pick only from these layers.")]
        public FsmInt[] layerMask;

        [Tooltip("Invert the mask, so you pick from all layers except those defined above.")]
        public FsmBool invertMask;

        [ActionSection("Results")]

        [UIHint(UIHint.Variable)]
        public FsmBool didHit;


        [Tooltip("Event to send if the ray hits an object.")]
        [UIHint(UIHint.Variable)]
        public FsmEvent hitEvent;

        [Tooltip("Event to send if the ray does not hit any object.")]
        [UIHint(UIHint.Variable)]
        public FsmEvent noHitEvent;

        public FsmBool everyFrame;





        public override void Reset()
        {
            position = null;


            radius = null;
            layerMask = new FsmInt[0];
            invertMask = false;
            ignoreTriggerColliders = false;
            didHit = null;
            everyFrame = null;
        }


        public override void OnEnter()
        {
            if(everyFrame.Value == false)
            {
                DoCheckSphere();
                Finish();

            }
          
        }

        public override void OnUpdate()
        {
            DoCheckSphere();
        }

        void DoCheckSphere()
        {

            GameObject go = Fsm.GetOwnerDefaultTarget(position);
            float range = radius.Value;
            if (ignoreTriggerColliders.Value == true)
            {
                didHit = Physics.CheckSphere(go.transform.position, range, ActionHelpers.LayerArrayToLayerMask(layerMask, invertMask.Value), QueryTriggerInteraction.Ignore);

            } 
            else
            {
                didHit = Physics.CheckSphere(go.transform.position, range, ActionHelpers.LayerArrayToLayerMask(layerMask, invertMask.Value), QueryTriggerInteraction.Collide);

            }
            if (didHit.Value)
            {
                Fsm.Event(hitEvent);
            } else
            {
                Fsm.Event(noHitEvent);
            }

        }
    }
}
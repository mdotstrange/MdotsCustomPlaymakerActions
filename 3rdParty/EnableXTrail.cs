using UnityEngine;
using XftWeapon;


namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.ScriptControl)]
    [Tooltip("Enables/Disables X Trail ")]
    public class EnableXTrail : FsmStateAction
    {
        [RequiredField]
        [CheckForComponent(typeof(XWeaponTrail))]
        public FsmOwnerDefault trailOwner;
        public FsmBool enable;
        public FsmBool stopSmoothly;
        public FsmFloat fadeTime;
        XWeaponTrail trail;

        public override void Reset()
        {
            trailOwner = null;
            trail = null;
        }

        public override void OnEnter()
        {
            if (trailOwner.GameObject.Value == null)
            {
                Finish();
            }

            var go = Fsm.GetOwnerDefaultTarget(trailOwner);
            trail = go.gameObject.GetComponent<XWeaponTrail>();

            if(enable.Value == true)
            {
                trail.Activate();
            }
            else
            {
                if(stopSmoothly.Value == true)
                {
                    trail.StopSmoothly(fadeTime.Value);
                }
                else
                {
                    trail.Deactivate();
                }              
            }
          
            

        }


    }
}
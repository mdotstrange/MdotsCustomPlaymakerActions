using UnityEngine;
using System.Collections.Generic;
using CircularGravityForce;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.ScriptControl)]
    [Tooltip("Enables/Disables circular gravity script. ")]
    public class EnableCircularGravity : FsmStateAction
    {
        [RequiredField]

        [CheckForComponent(typeof(CircularGravity))]
        [Tooltip("The owner")]
        public FsmOwnerDefault gameObject;
        public FsmBool enable;


        CircularGravity script;



        public override void Reset()
        {
            gameObject = null;



        }

        public override void OnEnter()
        {

            DoEnableGravity();
            Finish();


        }



        void DoEnableGravity()
        {
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (go == null)
            {
                Debug.Log("NOT RUNNING");
                return;
            }


            script = go.GetComponent<CircularGravity>();


            script.Enable = enable.Value;


        }



    }
}
using UnityEngine;
using PathologicalGames;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("Path-o-logical/Unity Constraints")]
    [Tooltip("Set target game object for transform constraint.")]
    public class SetConstraintTarget : FsmStateAction
    {
        [RequiredField]
        [CheckForComponent(typeof(TransformConstraint))]
        public FsmOwnerDefault owner;

        public FsmGameObject targetGameObject;

        [Tooltip("Repeate every frame.")]
        public bool everyFrame;



        private GameObject previousGo;
        TransformConstraint constraint;

        public override void Reset()
        {
            owner = null;
            targetGameObject = null;
            everyFrame = false;
            constraint = null;

        }

        public override void OnEnter()
        {
            DoSetConstraintTarget();
            Finish();
        }




        public override void OnUpdate()
        {
            DoSetConstraintTarget();
        }





        void DoSetConstraintTarget()
        {
            var go = Fsm.GetOwnerDefaultTarget(owner);
            if (go == null)
            {
                return;
            }

            if (go != previousGo)
            {
                constraint = go.GetComponent<TransformConstraint>();
                previousGo = go;
            }

            if (constraint == null) return;

            constraint.target = targetGameObject.Value.transform;
            
        }
    }
}
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Keywords: limb ik final component root motion

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.GameObject)]
    [Tooltip("Adds a Final IK Limb IK Component to a Game Object and stores the component as an object variable. Requires Root Motion's Final IK ")]
    public class AddLimbIKComponent : FsmStateAction
    {
        [RequiredField]
        [Tooltip("The GameObject to add the Component to.")]
        public FsmOwnerDefault gameObject;

        [UIHint(UIHint.Variable)]
        [ObjectType(typeof(Component))]
        [Tooltip("Store the component in an Object variable. E.g., to use with Set Property.")]
        public FsmObject storeComponent;

        [Tooltip("Remove the Component when this State is exited.")]
        public FsmBool removeOnExit;

        Component addedComponent;

        public override void Reset()
        {
            gameObject = null;
            storeComponent = null;
        }

        public override void OnEnter()
        {
            DoAddComponent();

            Finish();
        }

        public override void OnExit()
        {
            if (removeOnExit.Value && addedComponent != null)
            {
                Object.Destroy(addedComponent);
            }
        }

        void DoAddComponent()
        {
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (go == null) return;

            addedComponent = go.AddComponent(typeof(RootMotion.FinalIK.LimbIK));

            storeComponent.Value = addedComponent;

            if (addedComponent == null)
            {
                LogError("Can't add component: " );
            }
        }
    }
}
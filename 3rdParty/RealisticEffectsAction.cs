using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.ScriptControl)]
    [Tooltip("Set parameters on Realistic Effects prefabs by KriptoFx.")]
    public class RealisticEffectsAction : FsmStateAction
    {
        public enum EffectTypeEnum
        {
            Projectile,
            AOE,
            Other
        };

        public enum DeactivationEnum
        {
            Deactivate,
            DestroyAfterCollision,
            DestroyAfterTime,
            Nothing
        };
        [RequiredField]
        [CheckForComponent(typeof(EffectSettings))]
        public FsmOwnerDefault effectOwner;
        public EffectTypeEnum EffectType;
        public FsmFloat colliderRadius;
        public FsmFloat effectRadius;
        public FsmBool useMoveVector;
        public FsmGameObject target;
        public FsmVector3 moveVector;
        public FsmFloat moveSpeed;
        public FsmBool isHomingMove;
        public FsmFloat moveDistance;
        public FsmBool isVisible;
        public DeactivationEnum InstanceBehaviour = DeactivationEnum.Nothing;
        public FsmFloat deactivateTimeDelay;
        public FsmFloat destroyTimeDelay;
        [UIHint(UIHint.Layer)]
        public FsmInt[]  layerMask;
        public FsmBool invertMask;
        public FsmBool everyFrame;
        EffectSettings script;

        public override void Reset()
        {
            invertMask = false;
            everyFrame = false;
        }


        public override void OnEnter()
        {
            if(effectOwner == null)
            {
                Finish();
            }
            var go = Fsm.GetOwnerDefaultTarget(effectOwner);
            script = go.gameObject.GetComponent<EffectSettings>();

            if (everyFrame.Value == false)
            {
                DoFxSettings();
                Finish();
            }
        }

        public override void OnUpdate()
        {
            DoFxSettings();
        }

        void DoFxSettings()
        {
            if(script == null)
            {
                var go = Fsm.GetOwnerDefaultTarget(effectOwner);
                script = go.gameObject.GetComponent<EffectSettings>();
            }
            int eValue = (int)EffectType;
            script.EffectType = (EffectSettings.EffectTypeEnum)eValue;
            script.ColliderRadius = colliderRadius.Value;
            script.EffectRadius = effectRadius.Value;
            script.UseMoveVector = useMoveVector.Value;
            script.Target = target.Value;
            script.MoveVector = moveVector.Value;
            script.MoveSpeed = moveSpeed.Value;
            script.IsHomingMove = isHomingMove.Value;
            script.MoveDistance = moveDistance.Value;
            script.IsVisible = isVisible.Value;
            script.DeactivateTimeDelay = deactivateTimeDelay.Value;
            script.DestroyTimeDelay = destroyTimeDelay.Value;
            script.LayerMask = ActionHelpers.LayerArrayToLayerMask(layerMask, invertMask.Value);
            int eValue1 = (int)InstanceBehaviour;
            script.InstanceBehaviour = (EffectSettings.DeactivationEnum)eValue1;
        }
    }
}
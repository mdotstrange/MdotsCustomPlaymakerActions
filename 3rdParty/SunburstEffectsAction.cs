//Franken-copy-paste-coded by MDS
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Keywords: sunburst effect
//Sunburst Effects Copyright (C) 2015 Keijiro Takahashi
using UnityEngine;
using System.Collections;
using Spektr;


namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.ScriptControl)]
    [Tooltip("Control Keijiro Takahashi's Sunburst Effects script. The script must be on the Game Object.")]
    [HelpUrl("https://github.com/keijiro/unity-sunburst-effects")]
    public class SunburstEffectsAction : FsmStateAction
    {
        [RequiredField]
        [CheckForComponent(typeof(SunburstEffects))]
        public FsmOwnerDefault gameObject;



        public FsmInt beamCount;

        [HasFloatSlider(0.1f, 0.5f)]
        public FsmFloat beamWidth;

        [HasFloatSlider(0.1f, 10f)]
        public FsmFloat speed;

        [HasFloatSlider(1, 10)]
        public FsmFloat scalePower;

        public FsmBool enabled;

        public FsmBool everyFrame;

        private GameObject previousGo; // remember so we can get new controller only when it changes.
        SunburstEffects lrender;

        public override void Reset()
        {

            beamCount = 100;
            beamWidth = 0.01f;
            speed = 0.1f;
            scalePower = 1;
            gameObject = null;


            enabled = true;
        
            everyFrame = null;
        }

        public override void OnEnter()
        {
            DoSunburst();

            if (!everyFrame.Value)
            {
                Finish();
            }
        }

        public override void OnUpdate()
        {
            DoSunburst();
        }

        void DoSunburst()
        {
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (go == null)
            {
                return;
            }

            if (go != previousGo)
            {
                lrender = go.GetComponent<SunburstEffects>();
                previousGo = go;
            }

            if (lrender == null) return;


          
            lrender.scalePower = scalePower.Value;
            lrender.speed = speed.Value;
            lrender.beamWidth = beamWidth.Value;
            lrender.beamCount = beamCount.Value;
            lrender.enabled = enabled.Value;

        }
    }
}


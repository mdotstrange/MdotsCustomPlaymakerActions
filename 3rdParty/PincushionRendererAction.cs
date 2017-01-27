//Franken-copy-paste-coded by MDS
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Keywords: pincushion effect
//Pincushion script Copyright (C) 2015 Keijiro Takahashi
using UnityEngine;
using System.Collections;
using Pincushion;


namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.ScriptControl)]
    [Tooltip("Control Keijiro Takahashi's Pincushion The script  must be on the Game Object. Set the mesh object on the script.")]
    [HelpUrl("https://github.com/keijiro/Pincushion")]
    public class PincushionRendererAction : FsmStateAction
    {
        [RequiredField]
        [CheckForComponent(typeof(PincushionRenderer))]
        public FsmOwnerDefault gameObject;
      

        public FsmFloat radius;
        public FsmFloat scale;

        [HasFloatSlider(0f, 1f)]
        public FsmFloat randomness;

        public FsmFloat noiseAmplitude;
        public FsmFloat noiseFrequency;
        public FsmFloat noiseMotion;

        //[SerializeField]
        //PincushionMesh mesh;

        //public PincushionMesh mesh;

        public FsmColor color;

        //[HasFloatSlider(0f, 1f)]
        //public FsmFloat metallic;

        //[HasFloatSlider(0f, 1f)]
        //public FsmFloat smoothness;

        //public FsmTexture albedoTexture;
        //public FsmFloat textureScale;

        //public FsmTexture normalTexture;
        //public FsmFloat normalScale;

        public FsmColor lineColor;

        //public FsmInt randomSeed;

        public FsmBool enabled;

        public FsmBool everyFrame;

        private GameObject previousGo; // remember so we can get new controller only when it changes.

        PincushionRenderer lrender;
        //PincushionMesh pMesh;
    
 
        public override void Reset()
        {

            radius = 5;
            scale = 1;
            randomness = 0.1f;
            noiseAmplitude = 0.2f;
            noiseFrequency = 1;
            noiseMotion = 0.3f;
            color = Color.white;
            //metallic = 0.5f;
            //smoothness = 0.5f;
            //albedoTexture = null;
            //textureScale = 1;
            //normalTexture = null;
            //normalScale = 1;
            lineColor = Color.white;
            //randomSeed = 0;
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
                lrender = go.GetComponent<PincushionRenderer>();
                previousGo = go;
            }

            if (lrender == null) return;



           
            lrender.enabled = enabled.Value;
            lrender.radius = radius.Value;
            lrender.scale = scale.Value;
            lrender.randomness = randomness.Value;
            lrender.noiseAmplitude = noiseAmplitude.Value;
            lrender.noiseFrequency = noiseFrequency.Value;
            lrender.noiseMotion = noiseMotion.Value;
            lrender.color = color.Value;
            lrender.lineColor = lineColor.Value;
     
            
           
            

        }
    }
}


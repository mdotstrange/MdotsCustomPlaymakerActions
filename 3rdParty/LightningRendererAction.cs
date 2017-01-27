//Franken-copy-paste-coded by MDS
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Keywords: lightning effect spektr bolt electricity
//Spektr/Lightning Copyright (C) 2015 Keijiro Takahashi
using UnityEngine;
using System.Collections;
using Spektr;


namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.ScriptControl)]
    [Tooltip("Control Keijiro Takahashi's Spektr/Lightning script. The script must be on the Game Object.")]
    [HelpUrl("https://github.com/keijiro/SpektrLightning")]
    public class LightningRendererAction : FsmStateAction
    {
        [RequiredField]
        [CheckForComponent(typeof(LightningRenderer))]
        public FsmOwnerDefault gameObject;

        public FsmGameObject Emitter;

        [Tooltip("Position will only be used when the Emitter game object is null")]
        public FsmVector3 EmitterPosition;

        public FsmGameObject Receiver;

        [Tooltip("Position will only be used when the Receiver game object is null")]
        public FsmVector3 ReceiverPosition;

        [HasFloatSlider(0, 1)]
        public FsmFloat throttle;


        public FsmFloat pulse;

        [HasFloatSlider(0, 1)]
        public FsmFloat boltLength;

        [HasFloatSlider(1, 0)]
        public FsmFloat lengthRandom;

        public FsmFloat noiseAmplitude;

        public FsmFloat noiseFrequency;

        public FsmFloat noiseMotion;

        public FsmColor color;

        public FsmBool enabled;

        public FsmBool everyFrame;

        private GameObject previousGo; // remember so we can get new controller only when it changes.
        LightningRenderer lrender;

        public override void Reset()
        {
            gameObject = null;
        
            Emitter = null;
            Receiver = null;
            throttle = 0.1f;
            pulse = 0.2f;
            boltLength = 0.85f;
            lengthRandom = 0.8f;
            noiseAmplitude = 1.2f;
            noiseFrequency = 0.1f;
            noiseMotion = 0.1f;
            color = Color.yellow;
            enabled = null;
            EmitterPosition = null;
            ReceiverPosition = null;
            everyFrame = null;
        }

        public override void OnEnter()
        {
            DoLightning();

            if (!everyFrame.Value)
            {
                Finish();
            }
        }

        public override void OnUpdate()
        {
            DoLightning();
        }

        void DoLightning()
        {
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (go == null)
            {
                return;
            }

            if (go != previousGo)
            {
                lrender = go.GetComponent<LightningRenderer>();
                previousGo = go;
            }

            if (lrender == null) return;


            if  (Emitter.Value != null)
            {
                lrender.emitterTransform = Emitter.Value.transform;

            }
            else
            {

                lrender.emitterPosition = EmitterPosition.Value;
            }

            if (Receiver.Value != null)
            {
                lrender.receiverTransform = Receiver.Value.transform;

            } 
            else
            {

                lrender.receiverPosition = ReceiverPosition.Value;
            }



            lrender.throttle = throttle.Value;
            lrender.pulseInterval = pulse.Value;
            lrender.boltLength = boltLength.Value;
            lrender.lengthRandomness = lengthRandom.Value;
            lrender.noiseAmplitude = noiseAmplitude.Value;
            lrender.noiseFrequency = noiseFrequency.Value;
            lrender.noiseMotion = noiseMotion.Value;
            lrender.color = color.Value;
            lrender.enabled = enabled.Value;
            
        }
    }
}

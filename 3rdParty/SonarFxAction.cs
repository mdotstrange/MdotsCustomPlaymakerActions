//Franken-copy-paste-coded by MDS
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Keywords: sonarfx effect sonar
//SonarFx Copyright (C) 2015 Keijiro Takahashi

using UnityEngine;
using System.Collections;




namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.ScriptControl)]
    [Tooltip("Control Keijiro Takahashi's SonarFx script. The script must be on the camera owner Game Object.")]
    [HelpUrl("https://github.com/keijiro/SonarFx")]
    public class SonarFxAction : ComponentAction<Camera>
    {
        public enum SonarMode { Directional, Spherical }
        [RequiredField]
        [CheckForComponent(typeof(SonarFx))]
        [CheckForComponent(typeof(Camera))]
        public FsmOwnerDefault cameraOwner;

        //private FsmVector3 ownerPos;
        public SonarMode sonarMode ;
        private FsmString alaMode;
        private string daMode;
        [ActionSection("Used in Directional Mode")]
        [Tooltip("If in directional mode use this")]
        public FsmVector3 direction;
        [ActionSection("Used in Spherical Mode")]
        
      
        [Tooltip("If in spherical mode use these. Position will be used when the gamne object is set to none.")]
        public FsmBool useGameObject;
        public FsmGameObject originGameObject;
        public FsmBool usePosition;
        public FsmVector3 originPosition;
        [ActionSection("Base Color")]
        public FsmColor albedo;
        public FsmColor emission;
        [ActionSection("Wave Parameters")]
        public FsmColor color;
        public FsmFloat amplitude;
        public FsmFloat exponent;
        public FsmFloat interval;
        public FsmFloat speed;
        public FsmBool enabled;
        public FsmBool everyFrame;

        [ActionSection("Visibility/Camera settings")]

        [Tooltip("Cull these layers.")]
        [UIHint(UIHint.Layer)]
        public FsmInt[] cullingMask;

        private FsmInt oldMask;

        [Tooltip("Invert the mask, so you cull all layers except those defined above.")]
        public FsmBool invertMask;

        //[Tooltip("Reset the culling mask settings on exit")]
        //public FsmBool resetOnExit;

        

     

        private GameObject previousGo; // remember so we can get new controller only when it changes.
        SonarFx sonar;

      

        public override void Reset()
        {
            
            originGameObject = null;
            originPosition = new Vector3(0,0,0);
            amplitude = 2.0f;
            exponent = 22f;
            interval = 20f;
            speed = 10f;
            direction = new Vector3(0, 0, 1);        
            albedo = Color.white;
            emission = Color.black;
            color = Color.yellow;
            enabled = null;
            everyFrame = null;
            useGameObject = null;
            usePosition = null;
            cullingMask = new FsmInt[0];
            invertMask = false;
            



        }

        public override void OnEnter()
        {
       
        
            //var go1 = Fsm.GetOwnerDefaultTarget(cameraOwner);
            //ownerPos = go1.transform.position;

           
            daMode = "Directional";

            DoSonarFx();

            if (!everyFrame.Value)
            {
                Finish();
            }
        }

        //public override void OnExit()
        //{

        //    if (resetOnExit.Value == true)
        //    {

        //        camera.cullingMask = ActionHelpers.LayerArrayToLayerMask(oldMask, invertMask.Value);
        //    }
        //    return;
        //}

        public override void OnUpdate()
        {
            DoSonarFx();
        }

        void DoSonarFx()
        {
            var go = Fsm.GetOwnerDefaultTarget(cameraOwner);
            if (go == null)
            {
                return;
            }

            if (go != previousGo)
            {
                sonar = go.GetComponent<SonarFx>();
                previousGo = go;
            }

            if (sonar == null) return;

            alaMode = sonarMode.ToString();
         

            if (string.Equals(alaMode.Value, daMode))
            {
               sonar.mode = SonarFx.SonarMode.Directional;
                sonar.direction = direction.Value;
                sonar.waveColor = color.Value;
                sonar.baseColor = albedo.Value;
                sonar.addColor = emission.Value;
                sonar.waveAmplitude = amplitude.Value;
                sonar.waveExponent = exponent.Value;
                sonar.waveInterval = interval.Value;
                sonar.waveSpeed = speed.Value;
                sonar.enabled = enabled.Value;

                //var go2 = Fsm.GetOwnerDefaultTarget(cameraOwner);
                if (UpdateCache(go))
                {
                    camera.cullingMask = ActionHelpers.LayerArrayToLayerMask(cullingMask, invertMask.Value);
                }


            }

            
            else
            {



                  if  (useGameObject.Value == true )
                {
                       
                        sonar.origin = originGameObject.Value.transform.position;          
                        sonar.waveColor = color.Value;
                        sonar.baseColor = albedo.Value;
                        sonar.addColor = emission.Value;
                        sonar.waveAmplitude = amplitude.Value;
                        sonar.waveExponent = exponent.Value;
                        sonar.waveInterval = interval.Value;
                        sonar.waveSpeed = speed.Value;
                        sonar.enabled = enabled.Value;
                        sonar.mode = SonarFx.SonarMode.Spherical;

                    //var go2 = Fsm.GetOwnerDefaultTarget(cameraOwner);
                    if (UpdateCache(go))
                    {
                        camera.cullingMask = ActionHelpers.LayerArrayToLayerMask(cullingMask, invertMask.Value);
                    }

                }







                if (usePosition.Value == true)
                {
                   
                    sonar.origin = originPosition.Value;
                    sonar.waveColor = color.Value;
                    sonar.baseColor = albedo.Value;
                    sonar.addColor = emission.Value;
                    sonar.waveAmplitude = amplitude.Value;
                    sonar.waveExponent = exponent.Value;
                    sonar.waveInterval = interval.Value;
                    sonar.waveSpeed = speed.Value;
                    sonar.enabled = enabled.Value;
                    sonar.mode = SonarFx.SonarMode.Spherical;

                    //var go2 = Fsm.GetOwnerDefaultTarget(cameraOwner);
                    if (UpdateCache(go))
                    {
                        camera.cullingMask = ActionHelpers.LayerArrayToLayerMask(cullingMask, invertMask.Value);
                    }




                } 
               

                


            }

        }
    }
}


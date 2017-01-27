/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Keywords: ugui first selected set
using UnityEngine;

using UnityEngine.EventSystems;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("uGui")]
    [Tooltip("Sets a game object as first selected AND selected game object. ")]
    public class UGuiSetFirstSelectedGameObject : FsmStateAction
    {
        public FsmGameObject firstSelected;
        public FsmBool everyFrame;

        public override void Reset()
        {

            firstSelected = null;
            everyFrame = false;


        }

        public override void OnEnter()
        {
            if (!everyFrame.Value)
            {
                SF();
                Finish();
            }

        }

        public override void OnUpdate()
        {
            if (everyFrame.Value)
            {
                SF();
            }
        }

        void SF()
        {

            EventSystem.current.firstSelectedGameObject = firstSelected.Value;
            EventSystem.current.SetSelectedGameObject(firstSelected.Value);
        }

    }
}

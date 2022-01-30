using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkatanicStudios
{ 
    public class AnimatorSetBool : StateMachineBehaviour {

        public enum State
        {
            OnStateExit,
            OnStateEnter
        }

        [SerializeField]
        private State state;
        [SerializeField]
        private string booleanVariableName;
        [SerializeField]
        private bool value;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (state == State.OnStateEnter)
            {
                animator.SetBool(booleanVariableName, value);
            }
        }

        // OnStateExit is called when a transition ends and the state 
        //machine finishes evaluating this state
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (state == State.OnStateExit)
            {
                animator.SetBool(booleanVariableName, value);
            }
        }



    }
}



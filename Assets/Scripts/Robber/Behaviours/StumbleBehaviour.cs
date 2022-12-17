using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Events;

namespace Robber
{
    public class StumbleBehaviour : StateMachineBehaviour
    {
        public event UnityAction Started, Ended;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo,
            int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);
            Started?.Invoke();
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, 
            int layerIndex)
        {
            base.OnStateExit(animator, stateInfo, layerIndex);
            Ended?.Invoke();
        }
    }
}

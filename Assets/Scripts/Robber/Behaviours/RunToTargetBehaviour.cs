using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Events;

namespace Robber
{
    public class RunToTargetBehaviour : StateMachineBehaviour
    {
        public event UnityAction Started, Updated, Ended;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo,
            int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);
            Started?.Invoke();
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo,
            int layerIndex)
        {
            base.OnStateUpdate(animator, stateInfo, layerIndex);
            Updated?.Invoke();
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex,
            AnimatorControllerPlayable controller)
        {
            base.OnStateExit(animator, stateInfo, layerIndex, controller);
            Ended?.Invoke();
        }
    }
}

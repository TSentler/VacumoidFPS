using UnityEngine;
using UnityEngine.Events;

namespace Robber
{
    public class RunToExitBehaviour : StateMachineBehaviour
    {
        public event UnityAction Ended, Updated;

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo,
            int layerIndex)
        {
            base.OnStateUpdate(animator, stateInfo, layerIndex);
            Updated?.Invoke();
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo,
            int layerIndex)
        {
            base.OnStateExit(animator, stateInfo, layerIndex);
            Ended?.Invoke();
        }
    }
}

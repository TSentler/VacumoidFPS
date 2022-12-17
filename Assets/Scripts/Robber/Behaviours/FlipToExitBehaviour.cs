using UnityEngine;
using UnityEngine.Events;

namespace Robber
{
    public class FlipToExitBehaviour : StateMachineBehaviour
    {
        public event UnityAction Started;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo,
            int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);
            Started?.Invoke();
        }
    }
}

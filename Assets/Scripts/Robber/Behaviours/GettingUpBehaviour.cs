using UnityEngine;
using UnityEngine.Events;

namespace Robber
{
    public class GettingUpBehaviour : StateMachineBehaviour
    {
        public event UnityAction Ended;

        public override void OnStateExit(Animator animator,
            AnimatorStateInfo stateInfo,
            int layerIndex)
        {
            base.OnStateExit(animator, stateInfo, layerIndex);
            Ended?.Invoke();
        }
    }
}
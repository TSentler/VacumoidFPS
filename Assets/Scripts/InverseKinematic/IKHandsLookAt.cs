using UnityEngine;

namespace InverseKinematic
{
    [RequireComponent(typeof(Animator))]
    public class IKHandsLookAt : MonoBehaviour
    {
        [SerializeField] private bool _ikActive = false;
        [SerializeField] private Transform _handTarget = null;

        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void OnAnimatorIK(int layerIndex)
        {
            if (_ikActive == false)
            {
                SetIKWeight(0f);
                return;
            }

            SetIKWeight(1f);
            _animator.SetIKPosition(AvatarIKGoal.LeftHand, _handTarget.position);
            _animator.SetIKPosition(AvatarIKGoal.RightHand, _handTarget.position);
            _animator.SetIKRotation(AvatarIKGoal.LeftHand, _handTarget.rotation);
            _animator.SetIKRotation(AvatarIKGoal.RightHand, _handTarget.rotation);
        }

        private void SetIKWeight(float weight)
        {
            _animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, weight);
            _animator.SetIKPositionWeight(AvatarIKGoal.RightHand, weight);
            _animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, weight);
            _animator.SetIKRotationWeight(AvatarIKGoal.RightHand, weight);
        }
    }
}

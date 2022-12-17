using UnityEngine;

namespace Trash.Transforms
{
    public class LookAtWithoutModelRotator : LookAtRotator
    {
        [SerializeField] private Transform _model;
        
        private bool _isFirstTime = true;
        
        private void SetChildRoot(Transform root, Transform child)
        {
            child.parent = root.parent;
            root.parent = child;
        }
        
        protected override void LookAtHandler(Transform target)
        {
            if (_isFirstTime)
            {
                SetChildRoot(transform, _model);
                base.LookAtHandler(target);
                SetChildRoot(_model, transform);
                _isFirstTime = false;
            }
            else
            {
                base.LookAtHandler(target);
            }
        }
    }
}

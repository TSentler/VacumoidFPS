using UnityEngine;

namespace Trash
{
    public class MicroGarbage : Garbage
    {
        [SerializeField] private float _damp = 12f;
        
        private void Start()
        {
            if (Target != null)
            {
                gameObject.SetActive(false);
            } 
        }

        protected override void SuckHandler()
        {
        }
    }
}
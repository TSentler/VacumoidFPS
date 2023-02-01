using System;
using UnityEngine;

namespace Trash
{
    public class MicroGarbage : Garbage
    {
        [SerializeField] private GameObject _scribble;
        [SerializeField] private float _damp = 12f;
        
        private void Start()
        {
            if (Target == null)
            {
                gameObject.SetActive(false);
            } 
        }

        public void Show()
        {
            _scribble.SetActive(true);
        }

        protected override void SuckHandler()
        {
        }
    }
}
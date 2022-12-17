using System;
using UnityEngine;
using Robber;

namespace Robber
{
    public class GlassBreaker : MonoBehaviour
    {
        [SerializeField] private GameObject _fragments;

        private void OnValidate()
        {
            if (_fragments == null)
                Debug.LogWarning("Fragments was not found!", this);
        }

        private void Start()
        {
            _fragments.SetActive(false);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out RobberAI robber))
            {
                Break();
            }
        }

        private void Break()
        {
            _fragments.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}

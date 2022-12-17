using System;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class HidePanelsAtStart : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _panels;

        private void Start()
        {
            foreach (var panel in _panels)
            {
                panel.SetActive(false); 
            }
        }
    }
}

using Trash;
using UnityEngine;

namespace EcsMicroTrash
{
    public class test1 : MonoBehaviour
    {
        
        [SerializeField] private StaticGarbage[] _staticMicroGarbages;
        public void Awake()
        {
            _staticMicroGarbages = FindObjectsOfType<StaticGarbage>();
            foreach (var item in _staticMicroGarbages)
            {
                item.Suck(777);
            }
        }
    }
}
using System;
using Saves;
using UnityEngine;
using UnityEngine.Events;

namespace Money
{
    public class Wallet : MonoBehaviour
    {
        private int _money;
        private MoneySaver _saver;
        
        public event UnityAction Changed;

        public int Money => _money;
        
        private void Awake()
        {
            _saver = new MoneySaver();
            _money = _saver.Load();
            Changed?.Invoke();
        }

        public void Earn(int money)
        {
            _money += money;
            _saver.Save(_money);
            Changed?.Invoke();
        }

        public void Buy(int coast, Action successCallback)
        {
            if (coast <= _money)
            {
                _money -= coast;
                _saver.Save(_money);
                Changed?.Invoke();
                successCallback?.Invoke();
            } 
        }
    }
}

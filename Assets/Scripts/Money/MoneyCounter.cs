using UnityEngine;
using UnityEngine.Events;

namespace Money
{
    public class MoneyCounter : MonoBehaviour
    {
        private float _total;
        private int _reward;
        private bool _isPause;

        public event UnityAction<int> Collected;
        
        public int LevelTotal => (int)_total;

        public void Pause()
        {
            _isPause = true;
        }
        
        public void Collect(float count)
        {
            if (_isPause)
                return;

            var old = LevelTotal;
            _total += count;
            if (old != LevelTotal)
            {
                int money = LevelTotal - old;
                Collected?.Invoke(money);
            }
        }

        public void Reward()
        {
            if (_reward == 0)
            {
                _reward = LevelTotal;
            }

            _total += _reward;
            Collected?.Invoke(_reward);
        }
    }
}

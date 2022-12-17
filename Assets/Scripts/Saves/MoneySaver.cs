namespace Saves
{
    public class MoneySaver
    {
        private readonly string _moneyName = "Money";
            
        private GameSaver _saver;
        
        public MoneySaver()
        {
            _saver = new GameSaver();
        }
        
        public int Load()
        {
            return _saver.Load(_moneyName);
        }
        
        public void Save(int money)
        {
            _saver.Save(_moneyName, money);
        }
    }
}

namespace Saves
{
    public class TrashSaver
    {
        private readonly string _trashName = "Trash";
        
        private GameSaver _saver;
        
        public TrashSaver()
        {
            _saver = new GameSaver();
        }

        public int Load()
        {
            return _saver.Load(_trashName);
        }
        
        public void Save(int allTrashPointsRounded)
        {
            _saver.Save(_trashName, allTrashPointsRounded);
        }
    }
}

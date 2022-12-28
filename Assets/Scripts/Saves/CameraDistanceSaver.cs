namespace Saves
{
    public class CameraDistanceSaver
    {
        private readonly string _trashName = "CameraDistance";
        
        private GameSaver _saver;
        
        public CameraDistanceSaver()
        {
            _saver = new GameSaver();
        }

        public float Load()
        {
            return _saver.LoadFloat(_trashName);
        }
        
        public void Save(float distance)
        {
            _saver.Save(_trashName, (int)distance);
        }
    }
}

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

        public float Load(float defaultValue = 0f)
        {
            return _saver.LoadFloat(_trashName, defaultValue);
        }
        
        public void Save(float distance)
        {
            _saver.Save(_trashName, distance);
        }
    }
}

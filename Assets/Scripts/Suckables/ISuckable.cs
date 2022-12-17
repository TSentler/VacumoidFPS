namespace Suckables
{
    public interface ISuckable
    {
        void Suck();
    }
    
    public interface ISuckableToCenter
    {
        void Suck(ISuckCenter center);
    }
}
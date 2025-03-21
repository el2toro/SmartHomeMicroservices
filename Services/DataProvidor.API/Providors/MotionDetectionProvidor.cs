namespace DataProvidor.API.Providors;

public interface IMotionDetectionProvidor
{
    bool CheckMotionSensor();
}

public class MotionDetectionProvidor : IMotionDetectionProvidor
{
    public bool CheckMotionSensor()
    {
        Random random = new Random();
        return random.Next(2) == 0;
    }
}

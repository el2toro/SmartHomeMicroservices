namespace DataProvidor.API.Providors;

public interface ITemperatureProvidor
{
    double GetTemperature();
}

public class TemperatureProvidor : ITemperatureProvidor
{
    public double GetTemperature()
    {
        Random random = new Random();
        double min = 10;
        double max = 35;

        double temperature = min + (random.NextDouble() * (max - min));

        return Math.Round(temperature, 1);
    }
}

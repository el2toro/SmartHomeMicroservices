namespace DataProvidor.API.Providors;

public interface ITemperatureProvidor
{
    double GetIndoorTemperature();
    double GetOutdoorTemperature();
}

public class TemperatureProvidor : ITemperatureProvidor
{
    public double GetIndoorTemperature()
    {
        return GenerateTemperature(10, 35);
    }

    public double GetOutdoorTemperature()
    {
        return GenerateTemperature(-15, 45);
    }

    private double GenerateTemperature(double min, double max)
    {
        Random random = new Random();
        double temperature = min + (random.NextDouble() * (max - min));

        return Math.Round(temperature, 1);
    }
}

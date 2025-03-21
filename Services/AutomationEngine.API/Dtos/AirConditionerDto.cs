namespace AutomationEngine.API.Dtos;

public class AirConditionerDto
{
    public Guid DeviceId { get; set; }
    public bool IsOn { get; set; }
    public int Fan { get; set; }
    public double Temperature { get; set; }
    public DeviceType DeviceType { get; set; }
}

public enum DeviceType
{
    AirConditioner = 6
}

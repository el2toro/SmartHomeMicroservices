namespace DeviceManagement.API.Devices.GetDeviceById;

public record GetDeviceByIdQuery(Guid Id) : ICommand<GetDeviceByIdResult>;
public record GetDeviceByIdResult(object Device);
public class GetDeviceByIdHandler(IDeviceRepository deviceRepository)
    : ICommandHandler<GetDeviceByIdQuery, GetDeviceByIdResult>
{
    public async Task<GetDeviceByIdResult> Handle(GetDeviceByIdQuery query, CancellationToken cancellationToken)
    {
        //TODO: 
        //Add fluent validation

        var result = await deviceRepository.GetDeviceById(query.Id) ??
             throw new DeviceNotFoundException(query.Id.ToString());

        return new GetDeviceByIdResult(result.ToDevice());
    }
}

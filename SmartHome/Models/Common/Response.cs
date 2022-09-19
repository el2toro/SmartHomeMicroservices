namespace SmartHome.Models.Common
{
    public class Response<T>
    {
        public T Value { get; set; }
        public Response(T param)
        {
            Value = param;
        }
    }
}

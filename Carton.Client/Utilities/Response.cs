namespace Carton.Client.Utilities
{
    public class Response<T>
    {
        public int StatusCode { get; internal set; }
        public string Error { get; internal set; }
        public T Data { get; internal set; }
    }
}

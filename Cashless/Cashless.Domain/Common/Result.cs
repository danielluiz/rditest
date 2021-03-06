namespace Cashless.Domain.Common
{
    public class Result<T>
    {
        public bool Success { get; set; }
        public string Error { get; set; }
        public T Data { get; set; }
    }
}
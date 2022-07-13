namespace co_mute_be.Abstractions.Models
{
    public class ApiResult<T>
    {
        public bool Success { get; set; }
        public string Error { get; set; }
        public T Result { get; set; }
    }
}

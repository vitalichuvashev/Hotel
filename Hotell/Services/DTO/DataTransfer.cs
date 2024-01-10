namespace Hotell.Services.DTO
{
    public class DataTransfer<T> where T : class
    {
        public DataTransfer(T data)
        {
            Data = data;
        }

        public T Data { get; }

        public bool IsSuccess { get => string.IsNullOrEmpty(ErrorMessage); }
        public string ErrorMessage { get; set; } = string.Empty;

        public static DataTransfer<T> Empty()
        {
            return new DataTransfer<T>(default!) { ErrorMessage = "Web api system error, try later..." };
        }
    }
}

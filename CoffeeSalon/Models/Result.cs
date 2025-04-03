namespace CoffeeSalon.Models
{
    public class Result
    {
        public bool IsSuccess { get; set; } = true;

        public string Message { get; set; } = string.Empty;

        public string Value { get; set; } = default!;
    }

    public class Result<T>
    {
        public bool IsSuccess { get; set; } = true;

        public string Message { get; set; } = string.Empty;

        public T Value { get; set; } = default!;
    }
}

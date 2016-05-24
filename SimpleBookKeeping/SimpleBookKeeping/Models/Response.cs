
namespace SimpleBookKeeping.Models
{
    public enum ResponseType
    {
        Success,
        Error
    }

    public class Response
    {
        public ResponseType Result { get; set; }

        public string Message { get; set; }
    }
}
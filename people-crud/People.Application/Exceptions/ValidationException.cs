namespace People.Application.Exceptions
{
    public class ValidationException(string message, string[] error) : Exception(message)
    {
    }
}

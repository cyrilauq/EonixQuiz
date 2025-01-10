namespace People.Domain.Exceptions
{
    public class NotSuchEntityFoundException(string message) : Exception(message)
    {
    }
}

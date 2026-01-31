namespace Tests.User.Domain.Exceptions;

public class MalformedOrderByParameterException(string message) : Exception(message)
{
}
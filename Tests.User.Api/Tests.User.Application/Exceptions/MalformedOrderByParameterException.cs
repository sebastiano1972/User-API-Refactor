namespace Tests.User.Application.Exceptions;

public class MalformedOrderByParameterException(string message) : Exception(message)
{
}
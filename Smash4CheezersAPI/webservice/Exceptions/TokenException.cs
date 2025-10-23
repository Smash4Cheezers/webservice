namespace webservice.Exceptions;

/// <summary>
/// Handle the exception that concerns a token from the Web-Service layer
/// </summary>
/// <param name="message">The message that describes the error.</param>
public class TokenException(string message) : Exception(message);
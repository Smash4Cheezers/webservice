namespace webservice.Exceptions;

/// <summary>
/// Represents an exception specific to user-related operations in the Web-Service layer.
/// </summary>
/// <param name="message">The message that describes the error.</param>
public class UserException(string message) : Exception(message);
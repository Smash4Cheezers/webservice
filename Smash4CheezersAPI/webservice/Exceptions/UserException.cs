namespace webservice.Exceptions;

/// <summary>
///     Handle the exception that concerns a user from the Web-Service layer
/// </summary>
public class UserException(string? message) : Exception(message);
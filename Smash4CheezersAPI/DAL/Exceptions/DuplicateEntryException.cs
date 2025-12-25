namespace DAL.Exceptions;

public class DuplicateEntryException : Exception
{
       public DuplicateEntryException(string message = "You cannot duplicate an already existant key") : base(message)
       {
       }
       
       public DuplicateEntryException(string message, Exception inner) : base(message, inner) { }
}
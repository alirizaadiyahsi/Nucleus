namespace Nucleus.Utilities.Exceptions;

public static class ExceptionExtensions
{
    public static IEnumerable<Exception> GetInnerExceptions(this Exception ex)
    {
        if (ex == null)
        {
            throw new ArgumentNullException();
        }

        var innerException = ex;
        do
        {
            yield return innerException;
            innerException = innerException.InnerException;
        }
        while (innerException != null);
    }
}
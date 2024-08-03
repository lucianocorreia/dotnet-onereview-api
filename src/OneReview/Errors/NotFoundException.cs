using Onereview.Errors;

namespace OneReview.Errors;

public class NotFoundException(string message) : ServiceException(StatusCodes.Status404NotFound, message)
{ }

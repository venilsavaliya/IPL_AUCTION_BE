namespace IplAuction.Entities.Exceptions;

public class NotFoundException(string entity) : Exception(string.Format(Messages.NotFound, entity)) { }
namespace IplAuction.Entities.Exceptions;

public class BadRequestException(string entity) : Exception(entity)
{
}

namespace Weblu.Domain.Errors.Orders;

public class OrderStatusErrorCodes
{
    public const string NotFound = "ORDER_STATUS_NOT_FOUND";
    public const string InvalidId = "INVALID_ORDER_ID";
    public const string NameRequired = "NAME_IS_REQUIRED";
    public const string NameMaxLength = "NAME_MAX_LENGTH";
    public const string DescriptionMaxLength = "DESCRIPTION_MAX_LENGTH";


}
namespace Zeiss.Products.Application.Features;

public static class ErrorCodes
{
    public const string DuplicateRequest = "DUPLICATE_REQUEST";
    public const string MissingIdempotencyKey = "MISSING_IDEMPOTENCY_KEY";
    
    public static class Product
    {
        public const string NotFound = "PRODUCT_NOT_FOUND";
        public const string SkuConflict = "PRODUCT_SKU_CONFLICT";
        public const string Unchanged = "PRODUCT_UNCHANGED";
    }

    public static class Inventory
    {
        public const string QuantityExceeded = "INVENTORY_QUANTITY_EXCEEDED";
        public const string NotTracked = "INVENTORY_NOT_TRACKED";
    }

    public static class Account
    {
        public const string NotFound = "ACCOUNT_NOT_FOUND";
        public const string Locked = "ACCOUNT_LOCKED";
    }
}
namespace ResultPattern.Core.Errors;

public static class ProductErrors
{
    public static Error NotFound(int id)
    {
        return Error.NotFound("Product.NotFound", $"Product with ID {id} was not found.");
    }

    public static Error InvalidPrice(decimal price)
    {
        return Error.Validation("Product.InvalidPrice",
            $"The price '{price}' is invalid. It must be greater than zero.");
    }

    public static Error AlreadyExists(string name)
    {
        return Error.Conflict("Product.AlreadyExists", $"A product named '{name}' already exists.");
    }

    public static Error NoChangesDetected()
    {
        return Error.Validation("Product.NoChangesDetected",
            "No changes were detected. Please modify at least one property before updating.");
    }

    public static Error CreationFailed()
    {
        return Error.Validation("Product.CreationFailed", "Product could not be created.");
    }

    public static Error DeletionFailed()
    {
        return Error.Validation("Product.DeletionFailed", "Product could not be deleted.");
    }
}
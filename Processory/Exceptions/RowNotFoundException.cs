namespace Processory.Exceptions;

public class RowNotFoundException : Exception {
    public RowNotFoundException(string name)
        : base($"Failed to retrieve the row with name '{name}' from the CSV data.") {
    }

    public RowNotFoundException() : base() {
    }

    public RowNotFoundException(string? message, Exception? innerException) : base(message, innerException) {
    }
}

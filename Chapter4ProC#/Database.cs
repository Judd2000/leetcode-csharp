class DatabaseReader
{
    // Nullable data field.
    public int? numericValue = null;

    public bool? boolValue = true;

    // Nullable return type.

    public int? GetIntFromDatabase()
    {
        return numericValue;
    }
}
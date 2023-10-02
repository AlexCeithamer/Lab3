namespace Lab3.Model;
/// <summary>
/// Name: Alex Ceithamer, Samuel Ayoade
/// Date: 10/2/23
/// Description: Enums for error handling throughout the project
/// Bugs: none known
/// Reflection: This was very helpful to finally learn about enums. It took a little bit to grasp what the purpose was and how to use it - but after reading through the example code it made sense.
/// </summary>
public enum AirportAdditionError
{
    InvalidIdLength,
    InvalidCityLength,
    InvalidRating,
    InvalidDate,
    DuplicateAirportId,
    DBAdditionError,
    NoError
}

/// <summary>
/// Enum errors for deletion to the databaes
/// </summary>
public enum AirportDeletionError
{
    AirportNotFound,
    DBDeletionError,
    NoError
}

/// <summary>
/// Enum errors for editing elements in the databaes
/// </summary>
public enum AirportEditError
{
    AirportNotFound,
    InvalidFieldError,
    DBEditError,
    NoError
}

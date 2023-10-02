
using System.Collections.ObjectModel;

namespace Lab3.Model;

/// <summary>
/// Name: Alex Ceithamer, Samuel Ayoade
/// Date: 10/2/23
/// Description: The main brains of the program. Creates a barrier between the GUI and the Database to check for errors in input before sending anything to the Database class.
/// Bugs: none known
/// Reflection: This class is essentially the same as Lab2, so nothing was changed here. Nothing to reflect on. (other than noting that javaDoc comments look much nicer in the code than c#)
/// </summary>
public class BusinessLogic : IBusinessLogic
{
    private IDatabase db = new Database();

    public ObservableCollection<Airport> Airports
    {
        get { return db.SelectAllAirports();  }
    }

    /// <summary>
    /// If the conditions given by the user are acceptable (meet requirements), the data is created into an Airport object and entered into database
    /// using the insertAirport method in Database class
    /// </summary>
    /// <param name="id">Unique ID of the airport</param>
    /// <param name="city">City the Airport is located in</param>
    /// <param name="dateVisited">Date the Airport was visited</param>
    /// <param name="rating">Rating (1-5) of the Airport</param>
    /// <returns>Whether there was an error or not when checking conditions or trying to enter into the database</returns>
    public AirportAdditionError AddAirport(string id, string city, DateTime dateVisited, int rating)
    {
        //check if valid inputs, if not return error code.
        //Error codes: 1 = invalid ID, 2 = invalid city, 3 = invalid date, 4 = invalid rating
        int result = checkConditions(id, city, dateVisited, rating);

        //error handling for initial data entered from user (basic error checking)
        if (result != 0)
        {
            switch (result)
            {
                case 1:
                    return AirportAdditionError.InvalidIdLength;
                case 2:
                    return AirportAdditionError.InvalidCityLength;
                case 3:
                    return AirportAdditionError.InvalidDate;
                case 4:
                    return AirportAdditionError.InvalidRating;
            }
        }
        //inputs are valid, so we create the new airport and insert it
        Airport airport = new Airport(id, city, dateVisited, rating);
        return db.InsertAirport(airport);
    }

    /// <summary>
    /// Used to check if the inputs are valid or not
    /// </summary>
    /// <param name="id">Must be 3-4 characters in length: Error code (1)</param>
    /// <param name="city">At most 25 characters in length: Error code (2)</param>
    /// <param name="dateVisited">A DateTime object: Error code (3)</param>
    /// <param name="rating">Integer 1-5: Error code (4)</param>
    /// <returns>Int representing the error, 0 if no error</returns>
    private int checkConditions(string id, string city, DateTime dateVisited, int rating)
    {
        //see class comment for error code descriptions
        if (id.Length < 3 || id.Length > 4)
        {
            return 1;
        }
        if (city.Length > 25)
        {
            return 2;
        }
        if (dateVisited.GetType() != typeof(DateTime))
        {
            return 3;
        }
        if (rating < 1 || rating > 5)
        {
            return 4;
        }
        return 0;
    }

    /// <summary>
    /// Deletes an airport from the database
    /// </summary>
    /// <param name="id">Must be 3-4 characters in length</param>
    /// <returns>AirportDeletionError depenidng on what the error was.</returns>
    public AirportDeletionError DeleteAirport(string id)
    {
        //if ID is invalid, print error message, else try to delete it
        if (id.Length < 3 || id.Length > 4)
        {
            return AirportDeletionError.AirportNotFound;
        }
        else
        {
            //if delete is successful, print success message, else print error message
            return db.DeleteAirport(id);
        }
    }

    /// <summary>
    /// Edits an airport in the database
    /// </summary>
    /// <param name="id">ID of the airport we want to edit</param>
    /// <param name="city">New city to update the airport with</param>
    /// <param name="dateVisited">New date visited to update the airport with</param>
    /// <param name="rating">new rating to update the airport with</param>
    /// <returns>AirportEditError depenidng on whether there was an error or not</returns>
    public AirportEditError EditAirport(string id, string city, DateTime dateVisited, int rating)
    {
        //check if valid inputs, if not return error code. Error codes are listed in the UI method
        //Error codes: 1 = invalid ID, 2 = invalid city, 3 = invalid date, 4 = invalid rating, 5 = airport not found
        int result = checkConditions(id, city, dateVisited, rating);
        if (result != 0)
        {
            return AirportEditError.InvalidFieldError;
        }
        //inputs are valid, so we update the airport
        Airport airport = new Airport(id, city, dateVisited, rating);
        return db.UpdateAirport(airport);
    }

    /// <summary>
    /// Finds the airport with the matching ID
    /// </summary>
    /// <param name="id">ID to search for</param>
    /// <returns>Airport object with matching ID</returns>
    public Airport FindAirport(string id)
    {
        return db.SelectAirport(id);
    }

    /// <summary>
    /// Calculates the statistics from the visited airports
    /// Bronze = 42
    /// Silver = 84
    /// Gold = 125
    /// </summary>
    /// <returns>String of the statistics saying how far to go until next tier</returns>
    public string CalculateStatistics()
    {
        //Bronze = 42 airports, Silver (+42) = 84 airports, Gold (+41) = 125 airports
        //get the list of airports
        ObservableCollection<Airport> airports = Airports;
        //base case: no airports visited
        if (airports == null)
        {
            return "0 airports visited; 42 airports remaining until achieving Bronze";
        }
        //get the number of airports visited and return the correct string
        int numAirports = airports.Count;
        if (numAirports == 1)
        {
            return numAirports + " airport visited; " + (42 - numAirports) + " airports remaining until achieving Bronze";
        }
        if (numAirports < 42)
        {
            return numAirports + " airports visited; " + (42 - numAirports) + " airports remaining until achieving Bronze";
        }
        else if (numAirports < 84)
        {
            return numAirports + " airports visited; " + (84 - numAirports) + " airports remaining until achieving Silver";
        }
        else if (numAirports < 125)
        {
            return numAirports + " airports visited; " + (125 - numAirports) + " airports remaining until achieving Gold";
        }
        else
        {
            return numAirports + " airports visited; Congratulations, you have achieved Gold status!";
        }

    }

    /// <summary>
    /// Gets the list of airports
    /// </summary>
    /// <returns>Observable collection of the airports</returns>
    public ObservableCollection<Airport> GetAirports()
    {
        var airports = db.SelectAllAirports();
        return airports;
    }

}
using System.Collections.Generic;

namespace Lab3.Model;

/// <summary>
/// Name: Alex Ceithamer, Samuel Ayoade
/// Date: 10/2/23
/// Description: Main Object we work with. Contains ID, City, DateVisited, and Rating. 
/// Bugs: none known
/// Reflection: Since the Date doesn't really need a time, we end up converting our Datetime into a string when printing to the GUI. The reason we needed to make a separate variable is due to other methods
/// using the DateTime type to check for errors. Personally - I like that we have both a stirng representation and a normal DateTime because then if the program were to be expanded, there's 2 options
/// for dealing with the DateVisited field. At the current state, the CockroachDB could be remade to use a String to keep track of the date - but it's easier to format and check for errors with the 
/// DateTime type instead of the string. 
/// </summary>
[Serializable]
public class Airport
{
    public String Id { get; set; }
    public String City { get; set; }
    public DateTime DateVisited { get; set; }
    public String DateVisitedString { get; set; }
    public int Rating { get; set; }

    /// <summary>
    /// Constructor class for Airport object when values are passed in during creation. Sets all the field variables to the arguments passed in
    /// </summary>
    /// <param name="id"> unique ID of the airport</param>
    /// <param name="city">City the airport is located</param>
    /// <param name="dateVisited"> Date the airport was visited</param>
    /// <param name="rating">1-5 rating of the airport</param>
    public Airport(string id, string city, DateTime dateVisited, int rating)
    {
        Id = id;
        City = city;
        DateVisited = dateVisited;
        DateVisitedString = dateVisited.ToShortDateString();
        Rating = rating;
    }

    /// <summary>
    /// Default constructor for Airport object. Sets field variables to defaults (empty strings, 0, etc)
    /// </summary>
    public Airport()
    {
        Id = "";
        City = "";
        DateVisited = new DateTime();
        DateVisitedString = "";
        Rating = 0;
    }

    /// <summary>
    /// Returns a string representation of the Airport object
    /// </summary>
    /// <returns>"ID - CITY, SHORT_DATE_STRING, RATING"</returns>
    public override string ToString()
    {
        return Id + " - " + City + ", " + DateVisitedString + ", " + Rating;
    }

    /// <summary>
    /// Determines if a passed in object is of type Airport, and whether the ID of the argument and current airport are equal
    /// </summary>
    /// <param name="obj">Argument passed in should be of type Airport</param>
    /// <returns>Whether the two airports are equal or whether the argument is an airport</returns>
    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }
        Airport airport = (Airport)obj;
        return Id.Equals(airport.Id);
    }

    /// <summary>
    /// Method hasn't been implemented yet
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public override int GetHashCode()
    {
        throw new NotImplementedException();
    }
}
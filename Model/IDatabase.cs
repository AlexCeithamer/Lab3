using System.Collections.ObjectModel;

namespace Lab3.Model;

/// <summary>
/// Name: Alex Ceithamer, Samuel Ayoade
/// Date: 10/2/23
/// Description: Interface for Database class. Contains general methods needed in a databaes such as Insert, delete, edit, etc.
/// Bugs: none known
/// Reflection: I like the idea of Interfaces in that they ensure you keep the same methods when you chnage anything in the Database class. Moving from Lab 2 to Lab 3 this was helpful with 
/// consistency of the class and endsuring it still works properly.
/// </summary>
interface IDatabase
{
    ObservableCollection<Airport> SelectAllAirports();
    Airport SelectAirport(string id);
    AirportAdditionError InsertAirport(Airport airport);
    AirportDeletionError DeleteAirport(string id);
    AirportEditError UpdateAirport(Airport airport);
}
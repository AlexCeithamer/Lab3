using System.Collections.ObjectModel;

namespace Lab3.Model;

/// <summary>
/// Name: Alex Ceithamer, Samuel Ayoade
/// Date: 10/2/23
/// Description: Interface for Business Logic class
/// Bugs: none known
/// Reflection: didn't have to do anything with this for Lab3, so not much to reflect on
/// </summary>
public interface IBusinessLogic
{
    public AirportAdditionError AddAirport(string id, string city, DateTime dateVisited, int rating);
    public AirportDeletionError DeleteAirport(string id);
    public AirportEditError EditAirport(string id, string city, DateTime dateVisited, int rating);
    public Airport FindAirport(string id);
    public string CalculateStatistics();
    public ObservableCollection<Airport> GetAirports();


}
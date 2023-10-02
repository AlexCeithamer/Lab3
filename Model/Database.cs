using System.Collections.ObjectModel;
using Npgsql;


namespace Lab3.Model;
/// <summary>
/// Name: Alex Ceithamer, Samuel Ayoade
/// Date: 10/2/23
/// Description: This class interracts with the CockroachDB. Changes data through SQL commands using NPGSQL package.
/// Bugs: none known
/// Reflection: It was really cool to learn how to interract with an SQL database that's online. It's definitly harder than using something like SQLite with java, but not too different. Just 
/// a couple extra lines of code and an extra method for connecting to it. 
/// </summary>
public class Database : IDatabase
{
    private ObservableCollection<Airport> airports;

    private String connString;

    /// <summary>
    /// Constructor for the database class. Loads in the data from CockroachDB and initializes declared variables
    /// </summary>
    public Database()
    {
        connString = GetConnectionString();
        airports = new ObservableCollection<Airport>();
        SelectAllAirports();
    }

    
    /// <summary>
    /// Builds a ConnectionString, which is used to connect to the database
    /// </summary>
    /// <returns> The connection string for the NPGSQL Connection String Builder</returns>
    static String GetConnectionString()
    {
        var connStringBuilder = new NpgsqlConnectionStringBuilder();
        connStringBuilder.Host = "flat-gazelle-13040.5xj.cockroachlabs.cloud";
        connStringBuilder.Port = 26257;
        connStringBuilder.SslMode = SslMode.VerifyFull;
        connStringBuilder.Username = "alexceithamer"; // won't hardcode this in your app
        connStringBuilder.Password = "E49wm4tZt8ndEZBWLA2SwQ";
        connStringBuilder.Database = "defaultdb";
        connStringBuilder.ApplicationName = "Airport Application"; // ignored, apparently
        connStringBuilder.IncludeErrorDetail = true;
        return connStringBuilder.ConnectionString;
    }

    /// <summary>
    /// Reaches out to the database to retrieve the elements and update our list of airports in the application. Clears the Airports Obervable
    /// Collection and then populates it with the updated data.
    /// </summary>
    /// <returns>the list of updated airports received from the database</returns>
    public ObservableCollection<Airport> SelectAllAirports()
    {
        airports.Clear();
        var conn = new NpgsqlConnection(connString);
        conn.Open();
        // using() ==> disposable types are properly disposed of, even if there is an exception thrown 
        using var cmd = new NpgsqlCommand("SELECT id, city, date, rating FROM airports", conn);
        using var reader = cmd.ExecuteReader(); // used for SELECT statement, returns a forward-only traversable object
        while (reader.Read()) // each time through we get another row in the table (i.e., another Airport)
        {
            String id = reader.GetString(0);
            String city = reader.GetString(1);
            DateTime date = reader.GetDateTime(2);
            Int32 rating = reader.GetInt32(3);
            Airport airportToAdd = new(id, city, date, rating);
            airports.Add(airportToAdd);
            Console.WriteLine(airportToAdd);
        }
        return airports;
    }


    /// <summary>
    /// This method returns the airport with the given ID
    /// </summary>
    /// <param name="id">ID of the airport to be returned</param>
    /// <returns>Returns Airport object of the airport if found, null if not found</returns>
    public Airport SelectAirport(string id)
    {
        //iterate through each airport, if the ID matches, return it
        foreach (Airport a in airports)
        {
            if (a.Id.Equals(id))
            {
                return a;
            }
        }
        return null;
    }

    /// <summary>
    /// Inserts the argument Airport into the database, updates the airports after through SelectAllAirports()
    /// </summary>
    /// <param name="airport">The Airport to insert into the database</param>
    /// <returns>The appropriate AirportAdditionError</returns>
    public AirportAdditionError InsertAirport(Airport airport)
    {
        //iterate through each airport, if the ID given matches an existing one, return error code 5 (DuplicateID)
        foreach (Airport a in airports)
        {
            if (a.Id.Equals(airport.Id))
            {
                return AirportAdditionError.DuplicateAirportId;
            }
        }
        //no duplicate so add the airport and return success code 0. Also save to the database file
        try
        {
            //open connection
            using var conn = new NpgsqlConnection(connString); 
            conn.Open();

            // create the sql command
            var cmd = new NpgsqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "INSERT INTO airports (id, city, date, rating) VALUES (@id, @city, @date, @rating)";
            cmd.Parameters.AddWithValue("id", airport.Id);
            cmd.Parameters.AddWithValue("city", airport.City);
            cmd.Parameters.AddWithValue("date", airport.DateVisited);
            cmd.Parameters.AddWithValue("rating", airport.Rating);
            cmd.ExecuteNonQuery(); // used for INSERT, UPDATE & DELETE statements - returns # of affected rows 
            SelectAllAirports();
            conn.Close();
        }
        catch (Npgsql.PostgresException pe)
        {
            Console.WriteLine("Insert failed, {0}", pe);
            return AirportAdditionError.DBAdditionError;
        }
        
        return AirportAdditionError.NoError;

    }

    /// <summary>
    /// This method deletes an airport from the list of airports, updates the airports after through SelectAllAirports()
    /// </summary>
    /// <param name="id">The ID of the airport to be deleted</param>
    /// <returns>The appropriate AirportDeletionError</returns>
    public AirportDeletionError DeleteAirport(string id)
    {
        //open the connection
        var conn = new NpgsqlConnection(connString);
        conn.Open();

        //create sql command
        using var cmd = new NpgsqlCommand();
        cmd.Connection = conn;
        cmd.CommandText = "DELETE FROM airports WHERE id = @id";
        cmd.Parameters.AddWithValue("id", id);
        int numDeleted = cmd.ExecuteNonQuery();

        //update the list of airports
        if (numDeleted > 0)
        {
            SelectAllAirports(); 
        }
        
        conn.Close();
        return AirportDeletionError.NoError;
        
    }

    /// <summary>
    /// This method updates an airport in the list of airports, updates the airports after through SelectAllAirports()
    /// </summary>
    /// <param name="airport">Airport to be edited</param>
    /// <returns>The appropriate AirportEditError</returns>
    public AirportEditError UpdateAirport(Airport airport)
    {
        try
        {
            
            using var conn = new NpgsqlConnection(connString); // conn, short for connection, is a connection to the database
            conn.Open(); // open the connection ... now we are connected!
            var cmd = new NpgsqlCommand(); // create the sql commaned
            cmd.Connection = conn; // commands need a connection, an actual command to execute
            cmd.CommandText = "UPDATE airports SET city = @city, date = @date, rating = @rating WHERE id = @id;";
            cmd.Parameters.AddWithValue("id", airport.Id);
            cmd.Parameters.AddWithValue("city", airport.City);
            cmd.Parameters.AddWithValue("date", airport.DateVisited);
            cmd.Parameters.AddWithValue("rating", airport.Rating);
            var numAffected = cmd.ExecuteNonQuery();

            SelectAllAirports();
            return AirportEditError.NoError;
        }
        catch (Npgsql.PostgresException pe)
        {
            Console.WriteLine("Update failed, {0}", pe);
            return AirportEditError.DBEditError;
        }
    }


}
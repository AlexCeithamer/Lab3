namespace Lab3;


using Lab3.Model;

/// <summary>
/// Name: Alex Ceithamer, Samuel Ayoade
/// Date: 10/2/23
/// Description: Main page c# code. Binded to attributes in the MainPage.xaml. Handles click events. Essentially this is the brains of the user interface/GUI
/// Bugs: none known
/// Reflection: We should have looked over the requirements for Lab3 more thoroughly and not focused so much on just connecting to the database and on the slides given during the lab. Spent too much time 
/// trying to get UserSecrets to work and overlooked the Calculate Statistics button. It was easy to implement but completely overlooked adding the button at first.
/// </summary>
public partial class MainPage : ContentPage
{
    /// <summary>
    /// Default constructor initializes the program.
    /// </summary>
    public MainPage()
    {
        InitializeComponent();

        // We've set the BindingContext for the entire page to be the domain layer
        // So any control on the page can bind to the domain layer
        // There's really only one control that needs to talk to the domain layer, and that's the CollectionView

        BindingContext = MauiProgram.BusinessLogic;
    }

    /// <summary>
    /// Event handler for clicking the add airport button. Sends it to BusinessLogic class to check information and add to databaes if info is valid from user.
    /// </summary>
    /// <param name="sender">contains the binded attributes from xaml file (ID, City, Date, Rating)</param>
    /// <param name="e">Event arguments</param>
    void AddAirport_Clicked(System.Object sender, System.EventArgs e)
    {
        //convert ID from string to int type and DATE from string to DateTime type
        int rating;
        int.TryParse(RatingENT.Text, out rating);
        DateTime date;
        DateTime.TryParse(DateENT.Text, out date);

        // The UI layer talks to the domain layer, telling it what to do
        AirportAdditionError result = (AirportAdditionError)MauiProgram.BusinessLogic.AddAirport(AirportIdENT.Text, CityENT.Text, date, rating);
        if (result != AirportAdditionError.NoError)
        {
            DisplayAlert("Ruhroh", result.ToString(), "OK");
        }
    }

    /// <summary>
    /// Event handler for clicking the Delete airport button. Ensures that an airport is selected, and then sends to BusinessLogic class to delete the airport
    /// </summary>
    /// <param name="sender">Contains the binded attributes from xaml file (this method only cares about the ID</param>
    /// <param name="e">Event arguments</param>
    void DeleteAirport_Clicked(System.Object sender, System.EventArgs e)
    {
        Airport currentAirport = CV.SelectedItem as Airport;

        //make sure an airport was selected
        if (currentAirport == null)
        {
            DisplayAlert("RuhRoh", "Please select an airport", "OK");
            return;
        }
        //airport was selected, so we send to BuisnessLogic to attempt to delete it
        AirportDeletionError result = (AirportDeletionError)MauiProgram.BusinessLogic.DeleteAirport(currentAirport.Id);
        
        if (result != AirportDeletionError.NoError)
        {
            DisplayAlert("Ruhroh", result.ToString(), "OK");
        }
    }

    /// <summary>
    /// Event handler for clicking the edit airport button. ID attribute doesn't matter since they click the airport they want, so we ignore the ID field and get it from the airport that's selected
    /// We do grab the other fields though.
    /// </summary>
    /// <param name="sender">Contains binded attributes from xaml file (this method only cares about City, Date, and Rating attributes.</param>
    /// <param name="e">Event arguments</param>
    void EditAirport_Clicked(System.Object sender, System.EventArgs e)
    {
        
        //create a new airport to replace the old one (which is 'editing' it)
        Airport currentAirport = CV.SelectedItem as Airport;

        //make sure an airport was selected
        if (currentAirport == null)
        {
            DisplayAlert("RuhRoh", "Please select an airport", "OK");
            return;
        }
        
        //airport was selected, so we send to BuisinessLogic to attempt to edit it
        int rating;
        int.TryParse(RatingENT.Text, out rating);
        DateTime date;
        DateTime.TryParse(DateENT.Text, out date);
        AirportEditError result = (AirportEditError)MauiProgram.BusinessLogic.EditAirport(currentAirport.Id, CityENT.Text, date, rating);
        if (result != AirportEditError.NoError)
        {
            DisplayAlert("Ruhroh", result.ToString(), "OK");
        }
    }

    /// <summary>
    /// Displays a pop-up of the statistics
    /// </summary>
    /// <param name="sender">Contains binded attributes form xaml file</param>
    /// <param name="e">Event arguments</param>
    void CalculateStatistics_Clicked(System.Object sender, System.EventArgs e)
    {
        DisplayAlert("Tier Info", MauiProgram.BusinessLogic.CalculateStatistics(), "OK");
    }

}

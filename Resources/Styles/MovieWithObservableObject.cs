//using System;
//using System.ComponentModel;
//using CommunityToolkit.Mvvm.ComponentModel;


//namespace MovieExtravaganza.Model;

//public class Movie : ObservableObject
//{
//    String _title;
//    int _numStars;
//    int _year;

//    public override bool Equals(object obj)
//    {
//        if (obj is not Movie)
//        {
//            return false;
//        }

//        Movie otherMovie = obj as Movie;
//        return Title == otherMovie.Title && NumStars == otherMovie.NumStars && Year == otherMovie.Year;
//    }

//    public String Title
//    {
//        get { return _title; }
//        set
//        {
//            SetProperty(ref _title, value);
//        }
//    }
//    public int Year
//    {
//        get { return _year; }
//        set
//        {
//            SetProperty(ref _year, value);
//        }
//    }
//    public int NumStars
//    {
//        get
//        {
//            return _numStars;
//        }
//        set
//        {
//            if (value >= 1 && value <= 5)
//            {
//                _numStars = value;
//                SetProperty(ref _numStars, value);
//            }
//        }
//    }


//    public Movie(String title, int year, int numStars)
//    {
//        Title = title;
//        Year = year;
//        NumStars = numStars;
//    }
//    public Movie() { }

//    public override String ToString()
//    {
//        return String.Format("Title: {0}, Year: {1}, NumStars: {2}", Title, Year, NumStars);
//    }

//    public override int GetHashCode()
//    {
//        return Title.GetHashCode();
//    }
//}


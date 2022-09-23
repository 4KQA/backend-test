using NERD.Web.Business.Features;
using NERD.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NERD.Web
{
    public class User
    {
        public User()
        {

        }
        public User(string userFname, string userLname, DateTime userBday, double userLat, double userLon, Gender gender, bool alive)
        {
            UserFname = userFname;
            UserLname = userLname;
            UserBday = userBday;
            UserLat = userLat;
            UserLon = userLon;
            Gender = gender;
            Alive = alive;

        }
        public int ID { get; set; }
        public string UserFname { get; set; }
        public string UserLname { get; set; }

        public string FullName => $"{UserFname} {UserLname}";
        public DateTime UserBday { get; set; }
        public double UserLat { get; set; }
        public int Age => DateTime.Now.Year - UserBday.Year;
        public double UserLon { get; set; }

        public string UserLocation => $"{UserLat} {UserLon}";
        public Gender Gender { get; set; }
        public bool Alive { get; set; }

        public bool IsValid()
        {
            return !string.IsNullOrEmpty(UserFname?.Trim()) &&
                !string.IsNullOrEmpty(UserLname?.Trim())
                && ID >= 1;
        }

        public string ToInsertSqlValues()
        {
            return "'" + UserFname + "', '" + UserLname + "', '" + UserLat.ToString() + "', '" + UserLon.ToString() + "', " + Convert.ToInt32(Alive) + ", " + (int)Gender + ", ";
        }

        public bool IsReadyForUpdate(int newLat, int newLon, User newUser, User existingUser)
        {
            bool latitudeIsValid = HelperMethods.IsLatitudeValid(newLat);
            bool longitudeIsValid = HelperMethods.IsLongitudeValid(newLon);
            bool canUserState = HelperMethods.CanUpdateUserState(existingUser.Alive, newUser.Alive);
            bool updateCoordinatesIsValid = HelperMethods.ValidateUpdateCoordinates(newLat, HelperMethods.IsNorthernHemisphere(newLat));
            return canUserState && updateCoordinatesIsValid && latitudeIsValid && longitudeIsValid;

        }

        public bool CanCreateNewUser(int lat, int lon) 
        {
            bool latitudeIsValid = HelperMethods.IsLatitudeValid(lat);
            bool longitudeIsValid = HelperMethods.IsLongitudeValid(lon);
            return latitudeIsValid && longitudeIsValid;

        }
    }
}
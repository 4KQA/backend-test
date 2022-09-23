using NERD.Web.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace NERD.Web.Business.Features
{
    public static class HelperMethods
    {
        /*readonly string conn2 = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
        public void setParam(string paramType, SqlParameter.Value value, string paramName)
        {
            try {
                using (SqlConnection conn = new SqlConnection(conn2))
                {


                    SqlParameter paramType = new SqlParameter { 
                        

                    }
    
            }
            } catch (Exception ex) {
                throw new Exception
            }
        }
        */
        public static Gender GetGender(string id)
        {
            if (!Enum.TryParse<Gender>(id, out var g))
                return Gender.Unknown;
            return g;
        }

        public static double? ParseToDouble(string input)
        {
            if (string.IsNullOrEmpty(input))
                return null;

            double value;
            if (!double.TryParse(input, out value))
                return null;
            return value;
        }

        public static bool ParseToBool(int input)
        {
            return input == 1;
        }

        public static bool CanUpdateUserState(bool existingUserState, bool newUserState) 
        {
            if (existingUserState)
                return true;


            return !existingUserState && newUserState == false;
        }
        public static bool IsNorthernHemisphere(int lat) 
        {
            return lat > 0;
        }

        public static bool ValidateUpdateCoordinates(int newVal, bool isNorth)
        {
            return isNorth ? newVal >= 0 : newVal <= 0;
        }
        public static bool IsLatitudeValid(int newVal) 
        {
            return newVal <= 90 && newVal >= -90;
        }
        public static bool IsLongitudeValid(int newVal)
        {
            return newVal <= 180 && newVal >= -180;
        }
        
    }
    
}
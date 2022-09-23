using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using NERD.Web.Business.Exceptions;
using NERD.Web.Models;

namespace NERD.Web.Business.Features
{

    public class UserService
    {

        readonly string conn2 = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
        public User GetDataFromDB(int ID)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(conn2))
                {

                    SqlParameter parameterId = new SqlParameter();
                    parameterId.Value = ID;
                    parameterId.ParameterName = "@ID";

                    string query = @"SELECT u.Id, u.UserFname, u.UserLname, u.UserLat, u.UserLon, u.Alive, u.Gender FROM [User] AS u WHERE u.Id = @ID";
                    SqlCommand command = new SqlCommand(query, conn);
                    command.Parameters.Add(parameterId);
                    conn.Open();

                    SqlDataReader dr = command.ExecuteReader();

                    var newUser = new User();

                    while (dr.Read())
                    {
                        newUser.UserFname = dr["userFname"]?.ToString();
                        newUser.UserLname = dr["userLname"]?.ToString();
                        double? lat = HelperMethods.ParseToDouble(dr["UserLat"]?.ToString());
                        double? lon = HelperMethods.ParseToDouble(dr["UserLon"]?.ToString());

                        if (lat.HasValue)
                        {
                            newUser.UserLat = lat.Value;
                        }

                        if (lon.HasValue)
                        {
                            newUser.UserLon = lon.Value;
                        }
                        newUser.Alive = (bool)dr["Alive"];
                        newUser.Gender = HelperMethods.GetGender(dr["Gender"].ToString());
                        newUser.ID = (int)dr["Id"];

                    }

                    //UpdateUser(user1);
                    dr.Close();
                    conn.Close();
                    if (newUser.IsValid()) return newUser;
                    return null;
                }



            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }


        public string UpdateOrCreateUser(User user)
        {
            string Outcome;
            User existingUser = GetDataFromDB(user.ID);
            if (existingUser == null)
            {
                Outcome = "User did not exist. Unable to create new user";
                if (CreateUser2(user))
                {
                    Outcome = "User did not exist. Successfully created new user";
                    return Outcome;
                }
                return Outcome;
            }
            else
            {
                Outcome = "Unable to update user";
                if (UpdateUser(user, existingUser))
                {
                    Outcome = "Successfully updated user.";
                    return Outcome;
                }
                return Outcome;
            }

        }

        public bool CreateUser2(User user)
        {
            var intUserLat = (int)user.UserLat;
            var intUserLon = (int)user.UserLon;

            if (user == null)
                return false;

            if (user.ID <= 0)
            {
                user.ID = GetNextUserId(GetAllUsers());
            }
                

            if (!user.IsValid())
                return false;
            if (!user.CanCreateNewUser(intUserLat, intUserLon))
                return false;


            SqlParameter parameterId = new SqlParameter
            {
                Value = user.ID,
                ParameterName = "@ID"
            };
            string sql = "INSERT INTO [User] (UserFname, UserLname, UserLat, UserLon, Alive, Gender, Id) VALUES (" + user.ToInsertSqlValues() + "@ID" + ");";

            var connection = new SqlConnection(conn2);
            var command = new SqlCommand(sql, connection);
            command.Parameters.Add(parameterId);
            command.Connection.Open();
            command.ExecuteNonQuery();
            command.Connection.Close();

            return true;
        }



        public bool UpdateUser(User newUser, User existingUser)
        {
            try
            {


                var intUserLat = (int)newUser.UserLat;
                var intUserLon = (int)newUser.UserLon;
                if (!newUser.IsReadyForUpdate(intUserLat, intUserLon, newUser, existingUser))
                    return false;
                try
                {
                    using (SqlConnection conn = new SqlConnection(conn2))
                    {
                        if (newUser.Gender != Gender.Unknown)
                            existingUser.Gender = newUser.Gender;

                        existingUser.Alive = newUser.Alive;
                        if (newUser.UserLname != null)
                            existingUser.UserLname = newUser.UserLname;
                        if (newUser.UserFname != null)
                            existingUser.UserFname = newUser.UserFname;

                        if (newUser.UserLat != 0)
                            existingUser.UserLat = newUser.UserLat;
                        if (newUser.UserLon != 0)
                            existingUser.UserLon = newUser.UserLon;

                        SqlParameter parameterId = new SqlParameter
                        {
                            Value = existingUser.ID,
                            ParameterName = "@ID"
                        };
                        SqlParameter parameterFname = new SqlParameter
                        {
                            Value = existingUser.UserFname,
                            ParameterName = "@Fname"
                        };
                        SqlParameter parameterLname = new SqlParameter
                        {
                            Value = existingUser.UserLname,
                            ParameterName = "@Lname"
                        };
                        SqlParameter parameterLat = new SqlParameter
                        {
                            Value = existingUser.UserLat.ToString(),
                            ParameterName = "@Lat",

                        };
                        SqlParameter parameterLon = new SqlParameter
                        {
                            Value = existingUser.UserLon.ToString(),
                            ParameterName = "@Lon"
                        };
                        SqlParameter parameterAlive = new SqlParameter
                        {
                            Value = existingUser.Alive,
                            ParameterName = "@Alive"
                        };

                        SqlParameter parameterGender = new SqlParameter
                        {
                            Value = existingUser.Gender,
                            ParameterName = "@Gender"
                        };
                        string Query = @"UPDATE [User] SET
                        [User].UserLat = @Lat, 
                        [User].UserLon = @Lon, 
                        [User].Alive = @Alive, 
                        [User].Gender = @Gender, 
                        [User].UserLname = @Lname, 
                        [User].UserFname = @Fname 
                        WHERE [User].Id = @ID";

                        SqlCommand command = new SqlCommand(Query, conn);
                        command.Parameters.Add(parameterId);
                        command.Parameters.Add(parameterLat);
                        command.Parameters.Add(parameterLon);
                        command.Parameters.Add(parameterAlive);
                        command.Parameters.Add(parameterGender);
                        command.Parameters.Add(parameterFname);
                        command.Parameters.Add(parameterLname);
                        conn.Open();
                        int status = command.ExecuteNonQuery();
                        if (status != 1)
                        {
                            //error
                            conn.Close();
                            return false;
                        }
                    }

                }
                catch (Exception ex)
                {

                }
                return true;

            }

            catch (Exception e)
            {


                return false;
            }
        }

        public int GetNextUserId(List<User> users)
        {
            if (users == null)
                return 1;
            if (!users.Any())
                return 1;

            return users.OrderBy(x => x.ID).Last().ID + 1;

        }

        public List<User> GetAllUsers()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(conn2))
                {
                    string Query = @"SELECT * FROM [User]";
                    SqlCommand command = new SqlCommand(Query, conn);
                    conn.Open();
                    SqlDataReader dr = command.ExecuteReader();

                    List<User> allUsers = new List<User>();
                    while (dr.Read())
                    {
                        var newUser = new User();
                        newUser.UserFname = dr["userFname"]?.ToString();
                        newUser.UserLname = dr["userLname"]?.ToString();
                        double? lat = HelperMethods.ParseToDouble(dr["UserLat"]?.ToString());
                        double? lon = HelperMethods.ParseToDouble(dr["UserLon"]?.ToString());

                        if (lat.HasValue)
                        {
                            newUser.UserLat = lat.Value;
                        }

                        if (lon.HasValue)
                        {
                            newUser.UserLon = lon.Value;
                        }
                        newUser.Alive = (bool)dr["Alive"];
                        newUser.Gender = HelperMethods.GetGender(dr["Gender"].ToString());
                        newUser.ID = (int)dr["Id"];
                        allUsers.Add(newUser);
                    }
                    return allUsers;
                }
            }
            catch (Exception ex)
            {

            }


            return null;

        }

        public List<User> GetSurvivorList()
        {
            List<User> allUsers = GetAllUsers();
            List<User> survivors = new List<User>();
            for (int i = 0; i < allUsers.Count; i++)
            {
                User user = allUsers[i];
                if (user.Alive == true)
                    survivors.Add(user);
            }


            return survivors;
        }
        public double GetSurvivorPercentage(List<User> survivors, List<User> allUsers)
        {
            double percentageAsDouble = (double)survivors.Count/allUsers.Count;
            
            return percentageAsDouble * 100;
        }


        public List<User> GetRelatives(string Lname)
        {
            List<User> allUsers = GetAllUsers();
            List<User> relatives = new List<User>();
            for (int i = 0; i < allUsers.Count; i++)
            {

                User user = allUsers[i];
                if (user.UserLname == Lname)
                    relatives.Add(user);
            }
            return relatives;
        }




    }

}




 
    
    


    

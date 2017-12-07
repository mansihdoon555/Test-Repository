using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Data.SqlClient;

namespace COreDBWCFService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        public User getUsersbyEmail(string UserEmail)
        {
            User users = new User();
            string connectionString = "Data Source=Manish-PC\\SQLEXPRESS;Initial Catalog=stnSolutions;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = connectionString;
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                string sql = "SELECT * FROM Users where Email=" + UserEmail;
                command.CommandText = sql;
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    users.Id = Convert.ToInt32(reader["Id"]);
                    users.Email = UserEmail;
                    users.FirstName = Convert.ToString(reader["FirstName"]);
                    users.LastName = Convert.ToString(reader["LastName"]);
                    users.UserName = Convert.ToString(reader["UserName"]);
                }
            }
            return users;
        }
    }
}

using AnyStore.BLL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnyStore.DAL
{
    internal class userDAL
    {
        static string myconsstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        #region Select Data from Database
        public DataTable Select()
        {
            // Static method to connect Database
            SqlConnection conn = new SqlConnection(myconsstrng);
            // To hold the data from Database
            DataTable dt = new DataTable();

            try
            {
                // SQL Query to Get data from database  
                String sql = "SELECT * FROM tbl_users";
                // For executing command
                SqlCommand cmd = new SqlCommand(sql, conn);
                // getting Data from Database
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                // Database connection Open
                conn.Open();
                // Fill data in our Datatable
                adapter.Fill(dt);
                    
            }
            catch(Exception ex)
            {
                // Throw Message if any error occur
                MessageBox.Show(ex.Message);
            }
            finally
            {
                // Closing connection
                conn.Close();
            }

            // Return the value in Datatable
            return dt;
        }
        #endregion
        #region Insert Data in Database
        public bool Insert(userBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconsstrng);

            try
            {
                String sql = "INSERT INTO tbl_users " +
                    "(first_name, last_name, email, username, password, contact, address, gender, user_type, added_date, added_by)" +
                    "VALUES (@first_name, @last_name, @email, @username, @password, @contact, @address, @gender, @user_type, @added_date, @added_by)";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@first_name", u.first_name);
                cmd.Parameters.AddWithValue("@last_name", u.last_name);
                cmd.Parameters.AddWithValue("@email", u.email);
                cmd.Parameters.AddWithValue("@username", u.username);
                cmd.Parameters.AddWithValue("@password", u.password);
                cmd.Parameters.AddWithValue("@contact", u.contact);
                cmd.Parameters.AddWithValue("@address", u.address);
                cmd.Parameters.AddWithValue("@gender", u.gender);
                cmd.Parameters.AddWithValue("@user_type", u.user_type);
                cmd.Parameters.AddWithValue("@added_date", u.added_date);
                cmd.Parameters.AddWithValue("@added_by", u.added_by);

                conn.Open();
                

                int rows = cmd.ExecuteNonQuery();
                // if the query is execute successfully then the value to rows will be greater than 0 elese it will be less than 0

                if (rows > 0)
                {
                    // Query Succesfull
                    isSuccess = true;
                }
                else
                {
                    // Query Failed
                    isSuccess = false;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close(); 
            }

        
            return isSuccess;
        }
        #endregion
        #region Update Data in Database
        public bool Update(userBLL u)
        {
            bool isSuccess = false;

            SqlConnection conn = new SqlConnection(myconsstrng);

            try
            {
                String sql = "UPDATE tbl_users SET " +
                             "first_name = @first_name, last_name = @last_name, email = @email, username = @username, " +
                             "password = @password, contact = @contact, address = @address, gender = @gender, " +
                             "user_type = @user_type, added_date = @added_date, added_by = @added_by " +
                             "WHERE id = @id";


                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@first_name", u.first_name);
                cmd.Parameters.AddWithValue("@last_name", u.last_name);
                cmd.Parameters.AddWithValue("@email", u.email);
                cmd.Parameters.AddWithValue("@username", u.username);
                cmd.Parameters.AddWithValue("@password", u.password);
                cmd.Parameters.AddWithValue("@contact", u.contact);
                cmd.Parameters.AddWithValue("@address", u.address);
                cmd.Parameters.AddWithValue("@gender", u.gender);
                cmd.Parameters.AddWithValue("@user_type", u.user_type);
                cmd.Parameters.AddWithValue("@added_date", u.added_date);
                cmd.Parameters.AddWithValue("@added_by", u.added_by);
                cmd.Parameters.AddWithValue("@id", u.id);

                conn.Open();

                isSuccess = cmd.ExecuteNonQuery() > 0;

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return isSuccess;
        }
        #endregion

        #region Delete Data from Database
        public bool Delete(userBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconsstrng);

            try
            {
                String sql = "DELETE FROM tbl_users WHERE id = @id";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@id", u.id);
                conn.Open();

                isSuccess = cmd.ExecuteNonQuery() > 0;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return isSuccess;
        }
        #endregion

        #region Search User on Database using Keywords
        public DataTable Search(string keywords)
        {
            // Static method to connect Database
            SqlConnection conn = new SqlConnection(myconsstrng);
            // To hold the data from Database
            DataTable dt = new DataTable();

            try
            {
                // SQL Query to Get data from database  
                String sql = "SELECT * FROM tbl_users WHERE id LIKE '%"+keywords+"%' OR first_name LIKE '%"+ keywords+"%' " +
                    "OR last_name LIKE '%"+keywords+"%' OR username LIKE '%"+keywords+"%'";

                // For executing command
                SqlCommand cmd = new SqlCommand(sql, conn);
                // getting Data from Database
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                // Database connection Open
                conn.Open();
                // Fill data in our Datatable
                adapter.Fill(dt);

            }
            catch (Exception ex)
            {
                // Throw Message if any error occur
                MessageBox.Show(ex.Message);
            }
            finally
            {
                // Closing connection
                conn.Close();
            }

            // Return the value in Datatable
            return dt;
        }
        #endregion

    }
}

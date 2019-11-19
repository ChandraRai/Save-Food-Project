﻿using SafeFoodLibrary.Managers;
using System;
using System.Configuration;
using System.Data.SqlClient;

/// <summary>
/// Summary description for UserManager
/// </summary>
public class UserManager : BaseManager
{
    public UserManager(string connectionString) : base(connectionString)
    {
    }

    public bool validateUser(string username,string password)
    {
        User user = new User(username, password);
        
        using (SqlConnection conn = new SqlConnection(connStr))
        {
            conn.Open();
            string sql = "SELECT Username from USERS where Username = @username and Password = @password";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@username", user.username);
            cmd.Parameters.AddWithValue("@password", user.password);
            return cmd.ExecuteScalar() is string;
        }
    }

    public bool addUser(User user)
    {
        SqlConnection conn = new SqlConnection(connStr);
        SqlCommand comm;
        comm = new SqlCommand("INSERT INTO USERS (FirstName, LastName, Username, Password, Email, Phone)" +
            "VALUES (@first, @last, @username, @password, @email, @phone)", conn);

        comm.Parameters.AddWithValue("@first", user.firstName);
        comm.Parameters.AddWithValue("@last", user.lastName);
        comm.Parameters.AddWithValue("@username", user.username);
        comm.Parameters.AddWithValue("@password", user.password);
        comm.Parameters.AddWithValue("@email", user.email);
        comm.Parameters.AddWithValue("@phone", user.phone);

        try
        {
            conn.Open();
            comm.ExecuteNonQuery();
            return true;
        }
        catch
        {
            return false;
        }
        finally
        {
            conn.Close();
        }
    }

    public void UpdateUser(User user)
    {
        SqlConnection conn;
        SqlCommand comm;
        conn = new SqlConnection(connStr);

        comm = new SqlCommand("UPDATE USERS SET FirstName = @first, LastName = @last, Phone = @phone, Email = @email WHERE Id = @id", conn);
        comm.Parameters.AddWithValue("@first", user.firstName);
        comm.Parameters.AddWithValue("@last", user.lastName);
        comm.Parameters.AddWithValue("@email", user.email);
        comm.Parameters.AddWithValue("@phone", user.phone);
        comm.Parameters.AddWithValue("@id", user.uId);

        try
        {
            conn.Open();
            comm.ExecuteNonQuery();
        }

        catch
        {

        }

        finally
        {
            conn.Close();
        }
    }

    /// <summary>
    /// Zhi Wei Su - 300899450
    /// This method checks if the username already exists
    /// </summary>
    public bool UsernameExists(string username)
    {
        using (SqlConnection conn = new SqlConnection(connStr))
        {
            conn.Open();
            SqlCommand comm = new SqlCommand("SELECT Username From Users WHERE Username = @Username", conn);
            comm.Parameters.AddWithValue("@Username", username);

            var exists = comm.ExecuteScalar();

            if (exists != null)
            {
                return true;
            }
            else
                return false;

        }
    }

    public User getUser(string value,string field)
    {
        SqlDataReader reader;
        SqlConnection conn;
        SqlCommand comm;
        string command = "SELECT Id,Username,Email,Phone,FirstName,LastName,Privilege" +
            " From USERS WHERE " + field + " = '" + value+"'";
        User user = new User();


        conn = new SqlConnection(connStr);
        comm = new SqlCommand(command, conn);


        try
        {
            conn.Open();
            reader = comm.ExecuteReader();
            while (reader.Read())
                user = new User(reader["Id"].ToString(), reader["Username"].ToString(), reader["Email"].ToString(), reader["Phone"].ToString(), 
                    reader["FirstName"].ToString(), reader["LastName"].ToString(),Int32.Parse(reader["Privilege"].ToString()));

            reader.Close();
            return user;
            
        }
        catch
        {
            return user;
        }
        finally
        {
            conn.Close();
        }

    }

    public string GetUserId(string username) => getUser(username, "Username").uId;
}
﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace inventory_store.Models
{
    public class DataBaseConnection
    {
        private SqlConnection connection;
        public string conStr= "Data Source=DESKTOP-16KVTNK;Initial Catalog=DB1;User ID=sa;Password=123";
        private static DataBaseConnection instance = null;
        private DataBaseConnection()
        {

        }
        public static DataBaseConnection getInstance()
        {
            if (instance == null)
            {
                instance = new DataBaseConnection();
            }
            return instance;
        }
        public SqlConnection getConnection()
        {
            connection = new SqlConnection(conStr);
            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
            }
            return connection;
        }
        public int executeQuery(string query)
        {
            SqlCommand cmd = new SqlCommand(query, getConnection());
            int rowsAffecetd = cmd.ExecuteNonQuery();
            return rowsAffecetd;
        }
        public int executeScalar(string query)
        {
            SqlCommand cmd = new SqlCommand(query, getConnection());
            int countRows = int.Parse(cmd.ExecuteScalar().ToString());
            return countRows;
        }
        public SqlDataAdapter getAllData(string query)
        {
            SqlDataAdapter data = new SqlDataAdapter(query, getConnection());
            return data;
        }
        public SqlDataReader autoComplete(string query, string columnname, string txtBxValue)
        {
            SqlCommand cmd = new SqlCommand(query, getConnection());
            cmd.Parameters.Add(new SqlParameter(columnname, "%" + txtBxValue + "%"));
            cmd.ExecuteNonQuery();
            return cmd.ExecuteReader();

        }
        public SqlDataReader readData(string query)
        {
            SqlCommand cmd = new SqlCommand(query, getConnection());
            SqlDataReader data = cmd.ExecuteReader();
            return data;
        }
        public void closeConnection()
        {
            if (connection != null)
            {
                connection.Close();
            }
        }
    }
}
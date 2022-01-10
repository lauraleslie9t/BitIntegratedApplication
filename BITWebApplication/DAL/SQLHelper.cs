using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BITWebApplication.DAL
{
    public class SQLHelper
    {
        private string _conn;
        public SQLHelper()
        {
            _conn = ConfigurationManager.ConnectionStrings["BIT"].ConnectionString;
        }
        public DataTable ExecuteSQL(string sql, SqlParameter[] parameters = null, bool storedProcedure = false)
        {
            DataTable dataTable = new DataTable();
            SqlConnection dbConnection = new SqlConnection(_conn);
            SqlCommand dbCommand = new SqlCommand(sql, dbConnection);//Fire the query written in queryString using the connection object
            dbConnection.Open();
            if (storedProcedure == true)
            {
                dbCommand.CommandType = CommandType.StoredProcedure;
            }
            if (parameters != null)
            {
                AddParameters(dbCommand, parameters);
            }

            try
            {
                SqlDataReader drResults = dbCommand.ExecuteReader(CommandBehavior.CloseConnection);
                dataTable.Load(drResults);
            }
            catch (Exception)
            {

                //throw;
            }

            dbConnection.Close();
            return dataTable;
        }
        public void AddParameters(SqlCommand objCommand, SqlParameter[] parameters)
        {
            for (int i = 0; i < parameters.Length; i++)
            {
                objCommand.Parameters.Add(parameters[i]);
            }
        }
        public int ExecuteNonQuery(string sql, SqlParameter[] parameters = null, bool storedProcedure = false)
        {
            int returnValue = -1;
            SqlConnection dbConnection = new SqlConnection(_conn);
            SqlCommand dbCommand = new SqlCommand(sql, dbConnection);//Fire the query written in queryString using the connection object
            if (storedProcedure == true)
            {
                dbCommand.CommandType = CommandType.StoredProcedure;
            }
            if (parameters != null)
            {
                AddParameters(dbCommand, parameters);
            }
            dbConnection.Open();
            try
            {
                returnValue = dbCommand.ExecuteNonQuery();
            }
            catch (Exception)
            {

                //throw;
            }
            dbConnection.Close();
            return returnValue;
        }

        #region ExecuteStoredProc(SPName)
        /// <summary>
        /// executes a stored procedure without parameters and returns a DataTable resultset. 
        /// </summary>
        /// <param name="spName">string: The name of the Stored Procedure to be executed.</param>
        /// <returns>DataTable: DataTable containing the results of the stored procedure execution. </returns>
        public DataTable ExecuteStoredProc(string spName)
        {
            // 1. Create a connection object. Objects prefixed with Sql are specific to SQL Server, i.e 'connected' classes
            SqlConnection conn = new SqlConnection(_conn);

            // 2. Create a command object. Provide the SQL (or stored proc name) amd the connection to be used, to the command object. 
            SqlCommand cmd = new SqlCommand(spName, conn);

            // 3. Specify the type of command..
            cmd.CommandType = CommandType.StoredProcedure;
            //If anything is going to go wrong , it will fail either trying to open the connection to the database , or trying to execute the command. Use a Try-Catch block to mitigate the occurance of any exceptions. 
            DataTable dataTable = new DataTable();
            try
            {
                //Open the database connection
                cmd.Connection.Open();
                //Define a sqlDataReader which is a 'fire-hose' cursor i.e forward-only-read-only cursor. Which means that if we want to 'manipulate/modify' the data we must put it in some kind of 'container', like a dataTable (or dataset).
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                //create a datatable object to hold the results of the read. 

                
                //load the contents into the table with the reader. 
                dataTable.Load(dataReader);

                //Return the DataTable
            }
            catch (SqlException )
            {
                //throw new Exception(SqlEx.Message);
            }
            catch (Exception )
            {
                //this is the most generic exception
                //throw new Exception(ex.Message);
            }

            return dataTable;

        }
        #endregion ExecuteStoredProc(SPName)

        #region ExecuteStoredProc(SPName, parameters[])
        /// <summary>
        /// Executes a stored procedure WITH parameters and returns a DataTable result set.
        /// </summary>
        /// <param name="SPName">String: The name of the Stored Procedure to execute.</param>
        /// <param name="parameters">SqlParameter[] Array: An array of SqlParameters required by the Stored Procedure.</param>
        /// <returns>DataTable:</returns>
        public DataTable ExecuteStoredProc(string SPName, SqlParameter[] parameters)
        {
            SqlConnection conn = new SqlConnection(_conn);

            SqlCommand cmd = new SqlCommand(SPName, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            //Adding coupling to the class. 
            // by passing a string we create a second set of pointers as both are reference types. 

            //Add parameters to the class
            FillParameters(cmd, parameters);
            DataTable dataTable = new DataTable();
            try
            {
                cmd.Connection.Open();

                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                
                dataTable.Load(dataReader);
                
            }

            catch (Exception)
            {

                // throw;
            }
            finally
            {
                if (cmd.Connection.State == ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
            return dataTable;
        }
        #region ExecuteNonQuery(SQL)

        public int ExecuteNonQuerySql(string sql)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = _conn;
            SqlCommand cmd = new SqlCommand(sql, conn);
            //the default .commandType is text i.e sql string. 
            // The below step is not needed 
            cmd.CommandType = CommandType.Text;

            try
            {
                cmd.Connection.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected;

            }
            catch (Exception)
            {
                return -1;
                //throw;
            }
            finally
            {
                if (cmd.Connection.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            
        }


        #endregion ExecuteNonQuery(SQL)

        #region ExecuteNonQuery(SQL, parameters[])

        public int ExecuteNonQuerySql(string sql, SqlParameter[] parameters)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = _conn;
            SqlCommand cmd = new SqlCommand(sql, conn);
            //the default .commandType is text i.e sql string. 
            // The below step is not needed 
            cmd.CommandType = CommandType.Text;

            //Fil the comand object
            FillParameters(cmd, parameters);

            try
            {
                cmd.Connection.Open();
                int rowsAffected = cmd.ExecuteNonQuery();

                return rowsAffected;
            }
            catch (Exception)
            {
                return -1;
                //throw;
            }
            finally
            {
                if (cmd.Connection.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }


        #endregion ExecuteNonQuery(SQL, parameters[])
        #region ExecuteNonQuerySP(SPName, Parameters[])
        /// <summary>
        /// Executes a non-query stored procedure (INSERTs, UPDATES and DELETES).
        /// </summary>
        /// <param name="SPName">String: the name of the stored procedure to be executed.</param>
        /// <param name="parameters">SqlParameter[]: An array of parameters required by the stored procedure.</param>
        /// <returns>int: The number of records affected by the query.</returns>
        public int ExecuteNonQuerySP(string SPName, SqlParameter[] parameters)
        {
            SqlConnection conn = new SqlConnection(_conn);

            SqlCommand cmd = new SqlCommand(SPName, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            FillParameters(cmd, parameters);

            try
            {
                cmd.Connection.Open();
                //Execute the stored proc...
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected;
            }
            catch (Exception)
            {
                //throw;
                return -1;
            }
            finally
            {
                if (cmd.Connection.State == ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
        }

        #endregion ExecuteNonQuerySP(SPName, Parameters[])

        #endregion ExecuteStoredProc(SPName, parameters[])

        #region ExecuteScalarSql(strSQL)

        /// <summary>
        /// executes a string of SQL without parameters and returns the value held in the first fow/Column intersection of the result set. 
        /// </summary>
        /// <param name="sql">string: The SQL code to be executed.</param>
        /// <returns>Object: object containing the result of the SQL Execution. </returns>
        public object ExecuteScalarSql(string sql)
        {
            // 1. Create a connection object. Objects prefixed with Sql are specific to SQL Server, i.e 'connected' classes
            SqlConnection conn = new SqlConnection(_conn);

            // 2. Create a command object. Provide the SQL (or stored proc name) amd the connection to be used, to the command object. 
            SqlCommand cmd = new SqlCommand(sql, conn);

            // 3. Specify the type of command..
            cmd.CommandType = CommandType.Text;
            //If anything is going to go wrong , it will fail either trying to open the connection to the database , or trying to execute the command. Use a Try-Catch block to mitigate the occurance of any exceptions. 
            try
            {
                //Open the database connection
                cmd.Connection.Open();

                object returnValue = cmd.ExecuteScalar();

                //Return the result
                return returnValue;
            }
            catch (SqlException SqlEx)
            {
                throw new Exception(SqlEx.Message);
            }
            catch (Exception)
            {
                //this is the most generic exception
                //throw new Exception(ex.Message);
                return null;
            }
            finally
            {
                if (cmd.Connection.State == ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
        }

        #endregion ExecuteScalarSql(strSQL)

        private void FillParameters(SqlCommand cmd, SqlParameter[] parameters)
        {
            //Loop through the array of parameters that are passed in to this method and add each one to the comand objects Parameters Collection

            foreach (SqlParameter parameter in parameters)
            {
                cmd.Parameters.Add(parameter);
            }
        }
    }
}
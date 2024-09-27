using OrakYazilimLib.Util;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace OrakYazilimLib.DbUtil
{
    public class FiDbHelperMySql
    {
        public string ConnectionString;
        //private static readonly string connectionString = ConfigurationManager.ConnectionStrings["ConnStr"].ConnString;


        public FiDbHelperMySql()
        {
            
        }

        public FiDbHelperMySql(String connStr)
        {
            ConnectionString = connStr;
        }

        public DataTable GetTableFromSP(string sp, Dictionary<string, object> parametersCollection)
        {

            MySqlConnection connection = new MySqlConnection(ConnectionString);
            try
            {
                MySqlCommand command = new MySqlCommand(sp, connection) { CommandType = CommandType.StoredProcedure, CommandTimeout = connection.ConnectionTimeout };

                foreach (KeyValuePair<string, object> parameter in parametersCollection)
                    command.Parameters.AddWithValue(parameter.Key, parameter.Value);

                DataSet dataSet = new DataSet();
                (new MySqlDataAdapter(command)).Fill(dataSet);
                command.Parameters.Clear();

                if (dataSet.Tables.Count > 0)
                {
                    return dataSet.Tables[0];
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
                //return null;
            }
            finally
            {
                connection.Close();

            }
        }

        public DataTable GetTableFromSP(string sp, MySqlParameter[] prms)
        {


            MySqlConnection connection = new MySqlConnection(ConnectionString);
            try
            {
                MySqlCommand command = new MySqlCommand(sp, connection) { CommandType = CommandType.StoredProcedure, CommandTimeout = connection.ConnectionTimeout };
                connection.Open();

                command.Parameters.AddRange(prms);

                DataSet dataSet = new DataSet();
                (new MySqlDataAdapter(command)).Fill(dataSet);
                command.Parameters.Clear();

                if (dataSet.Tables.Count > 0)
                {
                    return dataSet.Tables[0];
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
                //return null;
            }
            finally
            {
                connection.Close();
            }
        }

        public DataTable GetTableFromSP(string sp)
        {

            MySqlConnection connection = new MySqlConnection(ConnectionString);
            MySqlCommand command = new MySqlCommand();
            try
            {
                command = new MySqlCommand(sp, connection) { CommandType = CommandType.StoredProcedure, CommandTimeout = connection.ConnectionTimeout };
                connection.Open();

                DataSet dataSet = new DataSet();
                (new MySqlDataAdapter(command)).Fill(dataSet);
                command.Parameters.Clear();

                if (dataSet.Tables.Count > 0)
                {
                    return dataSet.Tables[0];
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
                //return null;
            }
            finally
            {
                connection.Close();
                command.Dispose();
            }
        }

        public void ExecuteNonQuery(string sp, MySqlParameter[] prms)
        {

            MySqlConnection connection = new MySqlConnection(ConnectionString);
            MySqlCommand command = new MySqlCommand();
            try
            {
                command = new MySqlCommand(sp, connection) { CommandType = CommandType.StoredProcedure, CommandTimeout = connection.ConnectionTimeout };
                connection.Open();

                command.Parameters.AddRange(prms);

                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;

            }
            finally
            {
                connection.Close();
                command.Dispose();
            }
        }

        public void ExecuteNonQuery(string sp, MySqlParameter prms)
        {

            MySqlConnection connection = new MySqlConnection(ConnectionString);
            MySqlCommand command = new MySqlCommand();
            try
            {
                command = new MySqlCommand(sp, connection) { CommandType = CommandType.StoredProcedure, CommandTimeout = connection.ConnectionTimeout };
                connection.Open();
                //FIXME karşılığı bulunamadı
                //prms.MySqlDbType = MySqlDbType.Structured;
                command.Parameters.Add(prms);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
                command.Dispose();
            }
        }

        public void ExecuteNonQuery(string sp, MySqlParameter prm, MySqlParameter[] prms)
        {

            MySqlConnection connection = new MySqlConnection(ConnectionString);
            MySqlCommand command = new MySqlCommand();
            try
            {
                command = new MySqlCommand(sp, connection) { CommandType = CommandType.StoredProcedure, CommandTimeout = connection.ConnectionTimeout };
                connection.Open();
                //prm.MySqlDbType = MySqlDbType.Structured;
                command.Parameters.Add(prm);
                command.Parameters.AddRange(prms);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
                command.Dispose();
            }
        }

        public DataTable GetTableRow(string sp, MySqlParameter[] prms)
        {

            MySqlConnection connection = new MySqlConnection(ConnectionString);
            try
            {
                MySqlCommand command = new MySqlCommand(sp, connection) { CommandType = CommandType.StoredProcedure, CommandTimeout = connection.ConnectionTimeout };
                command.Parameters.AddRange(prms);
                connection.Open();

                DataSet dataSet = new DataSet();
                (new MySqlDataAdapter(command)).Fill(dataSet);
                command.Parameters.Clear();

                if (dataSet.Tables.Count > 0)
                {
                    return dataSet.Tables[0];
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
                //return null;
            }
            finally
            {
                connection.Close();
            }
        }

        // to 9-12-18
        public DataTable ExecuteDataTable(string sql, MySqlParameter[] qparams)
        {

            MySqlConnection connection = new MySqlConnection(ConnectionString);

            try
            {
                connection.Open();

                MySqlCommand command = new MySqlCommand(sql, connection);
                command.Parameters.AddRange(qparams);

                DataTable dataTable = new DataTable();
                new MySqlDataAdapter(command).Fill(dataTable);
                command.Parameters.Clear();
                return dataTable;
            }
            catch (Exception ex)
            {
                FiLogWeb.logException(ex);
                //throw ex;
                return null;
            }
            finally
            {
                connection.Close();
            }


        }

        public DataSet GetDatasetFromSP(string sp, MySqlParameter[] prms)
        {

            MySqlConnection connection = new MySqlConnection(ConnectionString);
            try
            {
                MySqlCommand command = new MySqlCommand(sp, connection) { CommandType = CommandType.StoredProcedure, CommandTimeout = connection.ConnectionTimeout };
                connection.Open();

                command.Parameters.AddRange(prms);

                DataSet dataSet = new DataSet();
                (new MySqlDataAdapter(command)).Fill(dataSet);
                command.Parameters.Clear();

                return dataSet;
            }
            catch (Exception ex)
            {
                throw ex;
                //return null;
            }
            finally
            {
                connection.Close();
            }
        }

        public int ExecuteNonQueryReturn(string sp, MySqlParameter[] prms)
        {

            MySqlConnection connection = new MySqlConnection(ConnectionString);
            MySqlCommand command = new MySqlCommand();

            try
            {
                command = new MySqlCommand(sp, connection) { CommandType = CommandType.StoredProcedure, CommandTimeout = connection.ConnectionTimeout };
                connection.Open();
                command.Parameters.AddRange(prms);
                int result = command.ExecuteNonQuery();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
                command.Dispose();
            }

        }

        // MySql version
        public DataTable getDataTable(String query)
        {

            using (MySqlConnection con = new MySqlConnection(ConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(query, con))
                {
                    cmd.CommandType = CommandType.Text;
                    using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                    {
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            //dataGridView1.DataSource = dt;
                            return dt;
                        }
                    }
                }
            }
        }

        public T ExecuteScalar<T>(string CommandText, params MySqlParameter[] commandParameters)
        {
            object result = null;

            MySqlConnection connection = new MySqlConnection(ConnectionString);
            MySqlCommand command = new MySqlCommand();
            try
            {
                connection.Open();
                command = new MySqlCommand(CommandText, connection);

                if (commandParameters != null && commandParameters.Length > 0)
                {
                    command.Parameters.AddRange(commandParameters);
                }
                
                result = command.ExecuteScalar();

            }
            catch (Exception ex)
            {
                Debug.WriteLine("Hata:" + ex.Message);
                //throw ex;
            }
            finally
            {
                connection.Close();
                command.Dispose();
            }

            if (typeof(T) == typeof(int))
            {
                var retForNull = -1;
                if (result == null) return (T) (object) retForNull;
            }

            if (result == null) //Convert.IsDBNull(result) 
                return default(T);
            if (result is T) // just unbox
                return (T)result;
            else            // convert
                return (T)Convert.ChangeType(result, typeof(T));

            //Next lines do compile (thanks to suggestions from answers!)
            //if (typeof(T) == typeof(string))
            //    return (T)(object)ExecuteScalar(storedProcName, parameters).ToString();
            //else if (typeof(T) == typeof(int))
            //    return (T)(object)Convert.ToInt32(ExecuteScalar(storedProcName, parameters));
            ////Next line compiles, but not all things boxed in an object can
            ////be converted straight to type T (esp db return values that might contain DbNull.Value, etc)
            //return (T)ExecuteScalar(storedProcName, parameters);

            //return result;
        }

        //deprecated
        public string ExecuteScalarFunction(string CommandText)
        {
            string Result = "";

            MySqlConnection connection = new MySqlConnection(ConnectionString);
            MySqlCommand command = new MySqlCommand();
            try
            {
                connection.Open();
                command = new MySqlCommand(CommandText, connection);
                MySqlDataAdapter da = new MySqlDataAdapter(command);
                DataTable dt = new DataTable();
                da.Fill(dt);

                Result = dt.Rows[0][0].ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
                command.Dispose();
            }

            return Result;

        }

        public void ExecuteMultipleDatatable(string sp, MySqlParameter[] prms, DataSet ds)
        {
            MySqlConnection connection = new MySqlConnection(ConnectionString);
            MySqlCommand command = new MySqlCommand();
            try
            {
                command = new MySqlCommand(sp, connection) { CommandType = CommandType.StoredProcedure, CommandTimeout = connection.ConnectionTimeout };
                connection.Open();
                command.Parameters.AddRange(prms);
                if (null != ds)
                {
                    foreach (DataTable dt in ds.Tables)
                    {
                        MySqlParameter parameter = new MySqlParameter();
                        //FIXME DBTYPE
                        //parameter.MySqlDbType = MySqlDbType.Structured;

                        //DataTable.TableName is the parameter Name
                        //e.g: @AppList
                        parameter.ParameterName = dt.TableName;
                        //DataTable.DisplayExpression is the equivalent MySqlType Name. i.e. Name of the UserDefined Table type
                        //e.g: AppCollectionType
                        //parameter.TypeName = dt.DisplayExpression;
                        // FIXME karşılık bulunamadı
                        //parameter.TypeName = dt.Namespace;
                        parameter.Value = dt;

                        command.Parameters.Add(parameter);
                    }
                }
                int result = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
                command.Dispose();
            }


        }

    }
}

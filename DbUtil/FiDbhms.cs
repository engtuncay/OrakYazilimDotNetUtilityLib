using OrakYazilimLib.DataContainer;
using OrakYazilimLib.DbGeneric;
using OrakYazilimLib.Util;
using OrakYazilimLib.Util.config;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using OrakYazilimLib.Util.core;

namespace OrakYazilimLib.DbUtil
{
	public class FiDbhms
	{
		public string connString { get; set; }
		//private static readonly string connectionString = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;

		public int errorRetValue = -1;



		public FiDbhms()
		{

		}

		public FiDbhms(string connStr)
		{
			connString = connStr;
		}

		public static FiDbhms FactoryWitProfile(string configKey)
		{
			//if()
			return new FiDbhms(FiAppConfig.GetConnectionString(configKey));
		}

		//public static FiDbhms Factory(FiConnConfig configKey)
		//{
		//    return new FiDbhms(FiAppConfig.GetConnectionString(configKey));
		//}

		/// <summary>
		/// connString direk olarak alır
		/// </summary>
		/// <param name="connString"></param>
		/// <returns></returns>
		public static FiDbhms FactoryWitCs(string connString)
		{
			return new FiDbhms(connString);
		}



		public Fdr<int> SqlExecuteNonQuery(string sql, List<FiSqlParameter> sqlParamList)
		{
			return SqlExecuteNonQuery(sql, FiSqlParameter.convertSqlParameter(sqlParamList).ToArray());
		}

		public Fdr<int> SqlExecuteNonQuery(string sql, SqlParameter[] prms)
		{

			SqlConnection connection = new SqlConnection(connString);
			Fdr<int> fdr = new Fdr<int>();

			using (connection)
			{
				try
				{
					SqlCommand command = new SqlCommand(sql, connection);
					connection.Open();
					if (prms != null && prms.Length > 0)
					{
						attachParameters(command, prms);
					}

					int totalRowsAffected = command.ExecuteNonQuery();
					fdr.obReturn = totalRowsAffected;
					fdr.lnRowsAffected = totalRowsAffected;
					fdr.blResult = true;

				}
				catch (Exception e)
				{
					FiLogWeb.logException(e);
					fdr.ExceptionQueryErrorLog(e);
				}

			}

			return fdr;

			//            command = new SqlCommand(sql, connection) { CommandType = CommandType.Text, CommandTimeout = connection.ConnectionTimeout };
			//            command.Parameters.AddRange(prms);
			//            command.Dispose();

		}

		public Fdr SqlExecuteNonQueryFi(string sql, List<FiSqlParameter> sqlParamList)
		{

			SqlConnection connection = new SqlConnection(connString);
			Fdr fiResponse = new Fdr();
			SqlParameter[] prms = FiSqlParameter.convertSqlParameter(sqlParamList).ToArray();

			using (connection)
			{
				try
				{
					SqlCommand command = new SqlCommand(sql, connection);
					connection.Open();
					if (prms != null && prms.Length > 0)
					{
						attachParameters(command, prms);
					}

					int totalRowsAffected = command.ExecuteNonQuery();
					fiResponse.obReturn = totalRowsAffected;
					fiResponse.lnRowsAffected = totalRowsAffected;
					fiResponse.blResult = true;
				}
				catch (Exception e)
				{
					FiLogWeb.logException(e);
					fiResponse.ExceptionQueryErrorLog(e);
				}

			}

			return fiResponse;

			//            command = new SqlCommand(sql, connection) { CommandType = CommandType.Text, CommandTimeout = connection.ConnectionTimeout };
			//            command.Parameters.AddRange(prms);
			//            command.Dispose();

		}

		public Fdr<T> SqlExecuteScalar<T>(string sql, List<FiSqlParameter> listParam)
		{

			SqlConnection connection = new SqlConnection(connString);
			var prms = FiSqlParameter.convertSqlParameter(listParam).ToArray();

			object result = null;
			var fiResponse = new Fdr<T>();

			using (connection)
			{
				try
				{
					SqlCommand command = new SqlCommand(sql, connection);
					connection.Open();

					if (prms != null && prms.Length > 0)
					{
						attachParameters(command, prms);
					}

					result = command.ExecuteScalar();
					fiResponse.blResult = true;

				}
				catch (Exception e)
				{
					FiLogWeb.logWeb("Hata :" + e.Message);
					FiLogWeb.logWeb("StackTrace !!!:\n" + e.StackTrace);

					fiResponse.txErrorMsgShort = e.Message;
					//fiResponse.txErrorStackTrace = e.StackTrace;
					fiResponse.blResult = false;
					fiResponse.lnRowsAffected = -1;
					return fiResponse;
				}
			}

			if (typeof(T) == typeof(int))
			{
				var returnValueForNull = 0; //-1 di

				if (result == null)
				{
					fiResponse.obReturn = (T)(object)returnValueForNull;
					return fiResponse;
				}
			}

			if (result == null)
			{
				//Convert.IsDBNull(result) 
				//return FiReturn<T>.FactoryObject(default(T));
				fiResponse.obReturn = default(T);
			}

			// sql den dönen değer T tipinde ise, obReturn T tipinde ata
			if (result is T)
			{
				// just unbox
				fiResponse.obReturn = (T)result;
				//return  FiReturn<T>.FactoryObject((T)result);  
			}
			else
			{   // convert : sqlden dönen değer T tipinde değilse dönüşüm yapar ??? hata çıkabilir
					//return FiReturn<T>.FactoryObject((T)Convert.ChangeType(result, typeof(T)));
				fiResponse.obReturn = (T)Convert.ChangeType(result, typeof(T));
			}

			return fiResponse;
		}

		public Fdr<T> SqlExecuteScalar<T>(FiSqlServerQuery fiSqlServerQuery)
		{

			SqlConnection connection = new SqlConnection(connString);
			var prms = FiSqlParameter.convertSqlParameter(fiSqlServerQuery.listParams).ToArray();

			object result = null;
			var fiResponse = new Fdr<T>();

			using (connection)
			{
				try
				{
					SqlCommand command = new SqlCommand(fiSqlServerQuery.sql, connection);
					connection.Open();

					if (prms != null && prms.Length > 0)
					{
						attachParameters(command, prms);
					}

					result = command.ExecuteScalar();
					fiResponse.blResult = true;

				}
				catch (Exception e)
				{
					FiLogWeb.logWeb("Hata :" + e.Message);
					FiLogWeb.logWeb("StackTrace !!!:\n" + e.StackTrace);

					fiResponse.txErrorMsgShort = e.Message;
					//fiResponse.txErrorStackTrace = e.StackTrace;
					fiResponse.blResult = false;
					fiResponse.lnRowsAffected = -1;
					return fiResponse;
				}
			}

			if (typeof(T) == typeof(int))
			{
				var returnValueForNull = 0; //-1 di

				if (result == null)
				{
					fiResponse.obReturn = (T)(object)returnValueForNull;
					return fiResponse;
				}
			}

			if (result == null)
			{
				//Convert.IsDBNull(result) 
				//return FiReturn<T>.FactoryObject(default(T));
				fiResponse.obReturn = default(T);
			}

			// sql den dönen değer T tipinde ise, obReturn T tipinde ata
			if (result is T)
			{
				// just unbox
				fiResponse.obReturn = (T)result;
				//return  FiReturn<T>.FactoryObject((T)result);  
			}
			else
			{   // convert : sqlden dönen değer T tipinde değilse dönüşüm yapar ??? hata çıkabilir
					//return FiReturn<T>.FactoryObject((T)Convert.ChangeType(result, typeof(T)));
				fiResponse.obReturn = (T)Convert.ChangeType(result, typeof(T));
			}

			return fiResponse;
		}

		//public FiResponse<DataTable> SqlExecuteDataTable(FiSqlQuery fiSqlQuery)
		//{
		//    return SqlExecuteDataTable(fiSqlQuery.sql, fiSqlQuery.GetListParams());
		//}

		public Fdr<DataTable> SqlExecuteDataTable(FiSqlServerQuery fiSqlServerQuery)
		{

			DataSet ds = new DataSet();
			SqlConnection connection = new SqlConnection(connString);
			var prms = FiSqlParameter.convertSqlParameter(fiSqlServerQuery.getListParams()).ToArray();

			var fiReturn = new Fdr<DataTable>();

			using (connection)
			{
				SqlCommand command = new SqlCommand(fiSqlServerQuery.sql, connection);

				if (FiCollection.isFull(prms))
				{
					attachParameters(command, prms);
				}

				using (SqlDataAdapter da = new SqlDataAdapter(command))
				{

					// Fill the DataSet using default values for DataTable names, etc
					try
					{
						da.Fill(ds);
						fiReturn.blResult = true;
						fiReturn.obReturn = ds.Tables[0];
						//fdr.txErrorMsgDetail = "Success:" + FiLogWeb.GetDetailSqlLog(fiSqlQuery);
					}
					catch (Exception ex)
					{
						Debug.Write(ex.ToString());
						fiReturn.blResult = false;
						fiReturn.txErrorMsgShort = FiLogWeb.GetMessage(ex);
						fiReturn.obReturn = new DataTable();
						fiReturn.txErrorMsgDetail = FiLogWeb.GetDetailSqlLog(fiSqlServerQuery);
					}
				}

				// Detach the SqlParameters from the command object, so they can be used again
				//cmd.Parameters.Clear();

				//cmd.CommandTimeout = 600;

				//if (mustCloseConnection)
				//  connection.Close();

				// Return the dataset
				//return ds;
			}


			return fiReturn;
		}

		public Fdr<DataTable> sqlExecuteDataTable(string sql, List<FiSqlParameter> listParam)
		{

			DataSet ds = new DataSet();
			SqlConnection connection = new SqlConnection(connString);
			var prms = FiSqlParameter.convertSqlParameter(listParam).ToArray();

			var fdr = new Fdr<DataTable>();

			using (connection)
			{
				SqlCommand command = new SqlCommand(sql, connection);

				if (FiCollection.isFull(prms))
				{
					attachParameters(command, prms);
				}

				using (SqlDataAdapter da = new SqlDataAdapter(command))
				{

					// Fill the DataSet using default values for DataTable names, etc
					try
					{
						da.Fill(ds);
						fdr.blResult = true;
						fdr.boResult = true;
						fdr.obReturn = ds.Tables[0];
					}
					catch (Exception ex)
					{
						Debug.Write(ex.ToString());
						fdr.blResult = false;
						fdr.boResult = false;
						fdr.txErrorMsgShort = ex.Message;
						fdr.obReturn = new DataTable();
					}
				}

				// Detach the SqlParameters from the command object, so they can be used again
				//cmd.Parameters.Clear();

				//cmd.CommandTimeout = 600;

				//if (mustCloseConnection)
				//  connection.Close();

				// Return the dataset
				//return ds;
			}


			return fdr;
		}

		public Fdr<DataTable> sqlExecute(FiQuery fiQuery)
		{

			DataSet ds = new DataSet();
			SqlConnection connection = new SqlConnection(connString);
			var queryParams = fiQuery.genListSqlParameter().ToArray();

			var fiReturn = new Fdr<DataTable>();

			using (connection)
			{
				String query = FiQueryTools.fixSqlProblems(fiQuery.sql);
				SqlCommand command = new SqlCommand(query, connection);

				if (FiCollection.isFull(queryParams))
				{
					attachParameters(command, queryParams);
				}

				using (SqlDataAdapter da = new SqlDataAdapter(command))
				{

					// Fill the DataSet using default values for DataTable names, etc
					try
					{
						da.Fill(ds);
						fiReturn.blResult = true;
						fiReturn.obReturn = ds.Tables[0];
					}
					catch (Exception ex)
					{
						FiLogWeb.logWeb("hata at sql execute");
						Debug.Write(ex.ToString());
						fiReturn.blResult = false;
						fiReturn.txErrorMsgShort = ex.Message;
						fiReturn.obReturn = new DataTable();
					}
				}

				// Detach the SqlParameters from the command object, so they can be used again
				//cmd.Parameters.Clear();

				//cmd.CommandTimeout = 600;

				//if (mustCloseConnection)
				//  connection.Close();

				// Return the dataset
				//return ds;
			}


			return fiReturn;
		}

		//Draft
		public Fdr SelectList(string sql, List<FiSqlParameter> listParam)
		{

			DataSet ds = new DataSet();
			SqlConnection connection = new SqlConnection(connString);
			var prms = FiSqlParameter.convertSqlParameter(listParam).ToArray();

			var fiReturn = new Fdr();

			using (connection)
			{
				SqlCommand command = new SqlCommand(sql, connection);

				if (FiCollection.isFull(prms))
				{
					attachParameters(command, prms);
				}

				using (SqlDataAdapter da = new SqlDataAdapter(command))
				{

					// Fill the DataSet using default values for DataTable names, etc
					try
					{
						da.Fill(ds);
						fiReturn.blResult = true;
						//FiDataTableEntensions.ToList<(ds.Tables[0]);
						fiReturn.obReturn = ds.Tables[0];
					}
					catch (Exception ex)
					{
						Debug.Write(ex.ToString());
						fiReturn.blResult = false;
						fiReturn.txErrorMsgShort = ex.Message;
						fiReturn.obReturn = new DataTable();
					}
				}

				// Detach the SqlParameters from the command object, so they can be used again
				//cmd.Parameters.Clear();

				//cmd.CommandTimeout = 600;

				//if (mustCloseConnection)
				//  connection.Close();

				// Return the dataset
				//return ds;
			}


			return fiReturn;
		}

		public DataTable SqlGetTableRow(string sp, SqlParameter[] prms)
		{

			SqlConnection connection = new SqlConnection(connString);
			try
			{
				SqlCommand command = new SqlCommand(sp, connection) { CommandType = CommandType.StoredProcedure, CommandTimeout = connection.ConnectionTimeout };
				command.Parameters.AddRange(prms);
				connection.Open();

				DataSet dataSet = new DataSet();
				(new SqlDataAdapter(command)).Fill(dataSet);
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


		private static void attachParameters(SqlCommand command, SqlParameter[] commandParameters)
		{
			// TODO throw düzeltilmeli
			if (command == null) throw new ArgumentNullException("parametre eklenecek command objesi yok");

			if (commandParameters != null)
			{
				foreach (SqlParameter p in commandParameters)
				{
					if (p != null)
					{
						if ((p.Direction == ParameterDirection.InputOutput ||
								 p.Direction == ParameterDirection.Input) &&
								(p.Value == null))
						{
							p.Value = DBNull.Value;
						}
						command.Parameters.Add(p);
					}
				}
			}
		}


		public DataTable GetTableFromSP(string sp, Dictionary<string, object> parametersCollection)
		{

			SqlConnection connection = new SqlConnection(connString);
			try
			{
				var command = new SqlCommand(sp, connection) { CommandType = CommandType.StoredProcedure, CommandTimeout = connection.ConnectionTimeout };

				foreach (KeyValuePair<string, object> parameter in parametersCollection)
					command.Parameters.AddWithValue(parameter.Key, parameter.Value);

				DataSet dataSet = new DataSet();
				(new SqlDataAdapter(command)).Fill(dataSet);
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

		public DataTable GetTableFromSP(string sp, SqlParameter[] prms)
		{


			SqlConnection connection = new SqlConnection(connString);
			try
			{
				SqlCommand command = new SqlCommand(sp, connection) { CommandType = CommandType.StoredProcedure, CommandTimeout = connection.ConnectionTimeout };
				connection.Open();

				command.Parameters.AddRange(prms);

				DataSet dataSet = new DataSet();
				(new SqlDataAdapter(command)).Fill(dataSet);
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

			SqlConnection connection = new SqlConnection(connString);
			SqlCommand command = new SqlCommand();
			try
			{
				command = new SqlCommand(sp, connection) { CommandType = CommandType.StoredProcedure, CommandTimeout = connection.ConnectionTimeout };
				connection.Open();

				DataSet dataSet = new DataSet();
				(new SqlDataAdapter(command)).Fill(dataSet);
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

		public void ExecuteNonQuery(string sp, SqlParameter[] prms)
		{

			SqlConnection connection = new SqlConnection(connString);
			SqlCommand command = new SqlCommand();
			try
			{
				command = new SqlCommand(sp, connection) { CommandType = CommandType.StoredProcedure, CommandTimeout = connection.ConnectionTimeout };
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



		public void ExecuteNonQuery(string sp, SqlParameter prms)
		{

			SqlConnection connection = new SqlConnection(connString);
			SqlCommand command = new SqlCommand();
			try
			{
				command = new SqlCommand(sp, connection) { CommandType = CommandType.StoredProcedure, CommandTimeout = connection.ConnectionTimeout };
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

		public void ExecuteNonQuery(string sp, SqlParameter prm, SqlParameter[] prms)
		{

			SqlConnection connection = new SqlConnection(connString);
			SqlCommand command = new SqlCommand();
			try
			{
				command = new SqlCommand(sp, connection) { CommandType = CommandType.StoredProcedure, CommandTimeout = connection.ConnectionTimeout };
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

		public DataTable GetTableRow(string sp, SqlParameter[] prms)
		{

			SqlConnection connection = new SqlConnection(connString);
			try
			{
				SqlCommand command = new SqlCommand(sp, connection) { CommandType = CommandType.StoredProcedure, CommandTimeout = connection.ConnectionTimeout };
				command.Parameters.AddRange(prms);
				connection.Open();

				DataSet dataSet = new DataSet();
				(new SqlDataAdapter(command)).Fill(dataSet);
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
		public DataTable ExecuteDataTable(string sql, SqlParameter[] qparams)
		{

			SqlConnection connection = new SqlConnection(connString);

			try
			{
				connection.Open();

				SqlCommand command = new SqlCommand(sql, connection);
				command.Parameters.AddRange(qparams);

				DataTable dataTable = new DataTable();
				new SqlDataAdapter(command).Fill(dataTable);
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

		public DataSet GetDatasetFromSP(string sp, SqlParameter[] prms)
		{

			SqlConnection connection = new SqlConnection(connString);
			try
			{
				SqlCommand command = new SqlCommand(sp, connection) { CommandType = CommandType.StoredProcedure, CommandTimeout = connection.ConnectionTimeout };
				connection.Open();

				command.Parameters.AddRange(prms);

				DataSet dataSet = new DataSet();
				(new SqlDataAdapter(command)).Fill(dataSet);
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

		public int ExecuteNonQueryReturn(string sp, SqlParameter[] prms)
		{

			SqlConnection connection = new SqlConnection(connString);
			SqlCommand command = new SqlCommand();

			try
			{
				command = new SqlCommand(sp, connection) { CommandType = CommandType.StoredProcedure, CommandTimeout = connection.ConnectionTimeout };
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

			using (SqlConnection con = new SqlConnection(connString))
			{
				using (SqlCommand cmd = new SqlCommand(query, con))
				{
					cmd.CommandType = CommandType.Text;
					using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
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

		public T ExecuteScalar<T>(string CommandText, params SqlParameter[] commandParameters)
		{
			object result = null;

			SqlConnection connection = new SqlConnection(connString);
			SqlCommand command = new SqlCommand();
			try
			{
				connection.Open();
				command = new SqlCommand(CommandText, connection);

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
				if (result == null) return (T)(object)retForNull;
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

			SqlConnection connection = new SqlConnection(connString);
			SqlCommand command = new SqlCommand();
			try
			{
				connection.Open();
				command = new SqlCommand(CommandText, connection);
				SqlDataAdapter da = new SqlDataAdapter(command);
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

		public void ExecuteMultipleDatatable(string sp, SqlParameter[] prms, DataSet ds)
		{
			SqlConnection connection = new SqlConnection(connString);
			SqlCommand command = new SqlCommand();
			try
			{
				command = new SqlCommand(sp, connection) { CommandType = CommandType.StoredProcedure, CommandTimeout = connection.ConnectionTimeout };
				connection.Open();
				command.Parameters.AddRange(prms);
				if (null != ds)
				{
					foreach (DataTable dt in ds.Tables)
					{
						SqlParameter parameter = new SqlParameter();
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

		public static FiDbhms build(string connStr)
		{
			return new FiDbhms(connStr);
		}
	}
}

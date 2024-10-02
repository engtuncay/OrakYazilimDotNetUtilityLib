//using Microsoft.Data.Sqlite;
//using System;
//using System.Data;
////using System.Data.SQLite;
//using System.Linq;

//namespace OrakYazilimLib.AdoNetHelper
//{
//	public class FiSqLite
//	{
//		public string ConnString { get; private set; }
//		public SqliteConnection Conn { get; private set; }

//		/// <summary>
//		/// Comm : Command
//		/// </summary>
//		//public SQLiteCommand Comm { get; private set; }

//		//public bool boConn { get; set; }


//		public FiSqLite(string _connString)
//		{
//			//ConnString = _connString;
//			//Conn = new SqlConnection(ConnString);
//			//Comm = Conn.CreateCommand();
//			//SQLiteConnection sqlite_conn;
//			// Create a new database connection:
//			//string connstr = @"Data Source=Y:\sqlite-data\demo.db";
//			this.ConnString = _connString;
//			this.Conn = new SqliteConnection(_connString);

//			// Open the connection:
//			try
//			{
//				this.Conn.Open();
//				Console.WriteLine("connected");
//				//this.boConn=true;
//			}
//			catch (Exception ex)
//			{
//				Console.WriteLine(ex.Message);
//				//this.boConn = false;
//			}
//			//return sqlite_conn;
//		}


//		private SqliteParameter[] ProcessParameters(params ParamItem[] parameters)
//		{
//			//SqlParameter[] pars = parameters.Select(x => new SqlParameter()
//			//{
//			//	ParameterName = x.ParamName,
//			//	Value = x.ParamValue
//			//}).ToArray();

//			//return pars;
//			return null;
//		}


//		public virtual int RunQuery(string query, params ParamItem[] parameters)
//		{
//			//Comm.Parameters.Clear();
//			//Comm.CommandText = query;
//			//Comm.CommandType = CommandType.Text;

//			//if (parameters != null && parameters.Length > 0)
//			//{
//			//	Comm.Parameters.AddRange(ProcessParameters(parameters));
//			//}

//			//int result = 0;

//			//Conn.Open();
//			//try
//			//{
//			//	result = Comm.ExecuteNonQuery();
//			//	if (result == -1) result = 1;
//			//}
//			//catch (Exception e)
//			//{
//			//	Console.WriteLine(e);
//			//	result = -2;
//			//	//throw;
//			//}

//			//Conn.Close();

//			//return result;
//			return 0;
//		}


//		public virtual DataTable RunProc(string procName, params ParamItem[] parameters)
//		{
//			//Comm.Parameters.Clear();
//			//Comm.CommandText = procName;
//			//Comm.CommandType = CommandType.StoredProcedure;

//			//if (parameters != null && parameters.Length > 0)
//			//{
//			//	Comm.Parameters.AddRange(ProcessParameters(parameters));
//			//}

//			//DataTable dt = new DataTable();
//			//SqlDataAdapter adapter = new SqlDataAdapter(Comm);
//			//adapter.Fill(dt);

//			//return dt;
//			return null;
//		}


//		public virtual DataTable GetTable(string query, params ParamItem[] parameters)
//		{
//			//Comm.Parameters.Clear();
//			//Comm.CommandText = query;
//			//Comm.CommandType = CommandType.Text;

//			//if (parameters != null && parameters.Length > 0)
//			//{
//			//	Comm.Parameters.AddRange(ProcessParameters(parameters));
//			//}

//			//SqlDataAdapter da = new SqlDataAdapter(Comm);

//			//// Adaptor : otomatik bağlantı açar. Verileri çeker(sorguyu çalıştırır) ve bir datatable 'a doldurur ve bağlantıyı otomatik kapatır.

//			//DataTable dt = new DataTable();
//			//da.Fill(dt);

//			//return dt;

//			//SQLiteDataReader sqlite_datareader;
//			SqliteCommand sqlite_cmd;
//			sqlite_cmd = this.Conn.CreateCommand();
//			sqlite_cmd.CommandText = query; // sqlite_cmd.CommandText = "SELECT * FROM test1";

			
//			// Microsoft SqLite de yok
//			//SqliteDataAdapter da = new SqliteDataAdapter(sqlite_cmd);

//			// Adaptor : otomatik bağlantı açar. Verileri çeker(sorguyu çalıştırır) ve bir datatable 'a doldurur ve bağlantıyı otomatik kapatır.
//			//DataTable dt = new DataTable();
//			//da.Fill(dt);

//			return null;
//		}
//	}

//}

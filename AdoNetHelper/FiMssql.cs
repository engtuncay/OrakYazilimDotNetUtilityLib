using OrakYazilimLib.AdoNetHelper;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace OrakYazilimLib.AdoNetHelper
{
	public partial class FiMssql
	{
		public string ConnString { get; private set; }
		public SqlConnection Conn { get; private set; }
		/// <summary>
		/// Comm : Command
		/// </summary>
		public SqlCommand Comm { get; private set; }


		public FiMssql(string _connString)
		{
			ConnString = _connString;
			Conn = new SqlConnection(ConnString);
			Comm = Conn.CreateCommand();
		}


		private SqlParameter[] ProcessParameters(params ParamItem[] parameters)
		{
			SqlParameter[] pars = parameters.Select(x => new SqlParameter()
			{
				ParameterName = x.ParamName,
				Value = x.ParamValue
			}).ToArray();

			return pars;
		}


		public virtual int RunQuery(string query, params ParamItem[] parameters)
		{
			Comm.Parameters.Clear();
			Comm.CommandText = query;
			Comm.CommandType = CommandType.Text;

			if (parameters != null && parameters.Length > 0)
			{
				Comm.Parameters.AddRange(ProcessParameters(parameters));
			}

			int result = 0;

			Conn.Open();
			try
			{
				result = Comm.ExecuteNonQuery();
				if (result == -1) result = 1;
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				result = -2;
				//throw;
			}

			Conn.Close();

			return result;
		}


		public virtual DataTable RunProc(string procName, params ParamItem[] parameters)
		{
			Comm.Parameters.Clear();
			Comm.CommandText = procName;
			Comm.CommandType = CommandType.StoredProcedure;

			if (parameters != null && parameters.Length > 0)
			{
				Comm.Parameters.AddRange(ProcessParameters(parameters));
			}

			DataTable dt = new DataTable();
			SqlDataAdapter adapter = new SqlDataAdapter(Comm);
			adapter.Fill(dt);

			return dt;
		}


		public virtual DataTable GetTable(string query, params ParamItem[] parameters)
		{
			Comm.Parameters.Clear();
			Comm.CommandText = query;
			Comm.CommandType = CommandType.Text;

			if (parameters != null && parameters.Length > 0)
			{
				Comm.Parameters.AddRange(ProcessParameters(parameters));
			}

			SqlDataAdapter da = new SqlDataAdapter(Comm);

			// Adaptor : otomatik bağlantı açar. Verileri çeker(sorguyu çalıştırır) ve bir datatable 'a doldurur ve bağlantıyı otomatik kapatır.

			DataTable dt = new DataTable();
			da.Fill(dt);

			return dt;
		}
	}
}

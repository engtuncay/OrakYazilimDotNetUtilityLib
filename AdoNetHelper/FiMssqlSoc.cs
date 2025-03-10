using OrakYazilimLib.DataContainer;
using OrakYazilimLib.Util.core;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace OrakYazilimLib.AdoNetHelper
{
	public class FiMssqlSoc
	{
		public string connString { get; private set; }
		public SqlConnection conn { get; private set; }

		/// <summary>
		/// Comm : Command
		/// </summary>
		public SqlCommand comm { get; private set; }

		public FiMssqlSoc(string connString)
		{
			this.connString = connString;
			conn = new SqlConnection(connString);
			comm = conn.CreateCommand();
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

		private SqlParameter[] ProcessParameters(FiKeybean fkbParams)
		{
			SqlParameter[] pars = fkbParams.Select(pair => new SqlParameter()
			{
				ParameterName = pair.Key,
				Value = pair.Value
			}).ToArray();

			return pars;
		}


		public virtual int RunQuery(string query, params ParamItem[] parameters)
		{
			comm.Parameters.Clear();
			comm.CommandText = query;
			comm.CommandType = CommandType.Text;

			if (parameters != null && parameters.Length > 0)
			{
				comm.Parameters.AddRange(ProcessParameters(parameters));
			}

			int result = 0;

			conn.Open();
			try
			{
				result = comm.ExecuteNonQuery();
				if (result == -1) result = 1;
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				result = -2;
				//throw;
			}

			conn.Close();

			return result;
		}

		public virtual Fdr RunQuery(string query, FiKeybean parameters)
		{
			comm.Parameters.Clear();
			comm.CommandText = query;
			comm.CommandType = CommandType.Text;

			Fdr fdrMain = new Fdr();

			if (parameters != null && parameters.Count > 0)
			{
				comm.Parameters.AddRange(ProcessParameters(parameters));
			}

			int result = 0;

			conn.Open();
			try
			{
				fdrMain.lnRowsAffected = comm.ExecuteNonQuery();
				fdrMain.boExecution = true;
			}
			catch (Exception e)
			{
				//Console.WriteLine(e);
				fdrMain.refException = e;
				fdrMain.boExecution = false;
			}
			finally
			{
				conn.Close();
			}

			return fdrMain;
		}

		public virtual DataTable RunProc(string procName, params ParamItem[] parameters)
		{
			comm.Parameters.Clear();
			comm.CommandText = procName;
			comm.CommandType = CommandType.StoredProcedure;

			if (parameters != null && parameters.Length > 0)
			{
				comm.Parameters.AddRange(ProcessParameters(parameters));
			}

			DataTable dt = new DataTable();
			SqlDataAdapter adapter = new SqlDataAdapter(comm);
			adapter.Fill(dt);

			return dt;
		}


		public virtual DataTable GetTable(string query, params ParamItem[] parameters)
		{
			comm.Parameters.Clear();
			comm.CommandText = query;
			comm.CommandType = CommandType.Text;

			if (parameters != null && parameters.Length > 0)
			{
				comm.Parameters.AddRange(ProcessParameters(parameters));
			}

			SqlDataAdapter da = new SqlDataAdapter(comm);

			// Adaptor : otomatik bağlantı açar. Verileri çeker(sorguyu çalıştırır) ve bir datatable 'a doldurur ve bağlantıyı otomatik kapatır.

			DataTable dt = new DataTable();
			da.Fill(dt);

			return dt;
		}
	}
}

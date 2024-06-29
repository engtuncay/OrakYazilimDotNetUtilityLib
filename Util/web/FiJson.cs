using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.Script.Serialization;
using OrakYazilimLib.DataContainer;
using Newtonsoft.Json;

namespace OrakYazilimLib.UtilWeb
{
    public class FiJson
    {
        public static void jsonResponseFromObject(Object data, HttpContext context)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            context.Response.Clear();
            context.Response.ContentType = "application/json";
            string response = js.Serialize(data);
            context.Response.Write(response);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.SuppressContent = true;
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }

        public static void jsonResponseNs(Object data, HttpContext context)
        {
            string response = JsonConvert.SerializeObject(data);
            // context jobs
            context.Response.Clear();
            context.Response.ContentType = "application/json";
            context.Response.Write(response);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.SuppressContent = true;
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }

        public static void jsonFiResponse<T>(Fdr<T> data, HttpContext context)
        {
            string response = JsonConvert.SerializeObject(data);
            // context jobs
            context.Response.Clear();
            context.Response.ContentType = "application/json";
            context.Response.Write(response);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.SuppressContent = true;
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }

        public static void jsonResponseFromDataTable(DataTable dt, HttpContext context)
        {
            string data = "";

            if (dt != null)
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                context.Response.Clear();
                context.Response.ContentType = "application/json";

                List<Dictionary<string, object>> parentRow = new List<Dictionary<string, object>>();
                Dictionary<string, object> childRow;

                foreach (DataRow row in dt.Rows)
                {
                    childRow = new Dictionary<string, object>();
                    foreach (DataColumn col in dt.Columns)
                    {
                        childRow.Add(col.ColumnName.Trim(),
                            (row[col] == System.DBNull.Value ? "" : (col.DataType == Type.GetType("System.DateTime") ? ((DateTime)row[col]).ToString("dd.MM.yyyy HH:mm:ss") : row[col])));
                    }
                    parentRow.Add(childRow);
                }

                data = js.Serialize(parentRow);
            }

            

            context.Response.Write(data);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.SuppressContent = true;
            HttpContext.Current.ApplicationInstance.CompleteRequest();

        }

        public static void jsonResponseFromDataTableUni(DataTable dt, HttpContext context)
        {
            string data = "";

            if (dt != null)
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                context.Response.Clear();
                context.Response.ContentType = "application/json";

                List<Dictionary<string, object>> parentRow = new List<Dictionary<string, object>>();
                Dictionary<string, object> childRow;

                foreach (DataRow row in dt.Rows)
                {
                    childRow = new Dictionary<string, object>();
                    foreach (DataColumn col in dt.Columns)
                    {
                        childRow.Add(col.ColumnName.Trim(),
                            (row[col] == System.DBNull.Value ? "" : (col.DataType == Type.GetType("System.DateTime") ? ((DateTime)row[col]).ToString("yyyy-MM-ddTHH:mm:ss") : row[col])));

                    }
                    parentRow.Add(childRow);
                }

                data = js.Serialize(parentRow);
            }

            

            context.Response.Write(data);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.SuppressContent = true;
            HttpContext.Current.ApplicationInstance.CompleteRequest();

        }

        public static void jsonReturnTrue(HttpContext context)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            context.Response.Clear();
            context.Response.ContentType = "application/json";
            string response = js.Serialize(true);
            context.Response.Write(response);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.SuppressContent = true;
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }

        public static void jsonResponseFromDataTableUni(Fdr<DataTable> fiResponseDataTable, HttpContext context)
        {
            jsonResponseFromDataTableUni(fiResponseDataTable.obReturn,context);
        }

        public static void jsonFiResponseTrue(HttpContext context)
        {
            Fdr<bool> fiResponse = new Fdr<bool>();
            fiResponse.obReturn = true;

            string response = JsonConvert.SerializeObject(fiResponse);
            // context jobs
            context.Response.Clear();
            context.Response.ContentType = "application/json";
            context.Response.Write(response);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.SuppressContent = true;
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }

        public static void JsonFiResponseMessage(bool b, string errorMessage,HttpContext context)
        {
            Fdr<string> fiResponse = new Fdr<string>();
            fiResponse.obReturn = null;
            fiResponse.txErrorMsgShort = errorMessage;
            fiResponse.blResult = false;

            string response = JsonConvert.SerializeObject(fiResponse);
            // context jobs
            context.Response.Clear();
            context.Response.ContentType = "application/json";
            context.Response.Write(response);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.SuppressContent = true;
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }
    }
}

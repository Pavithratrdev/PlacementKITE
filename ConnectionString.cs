using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Collections;

namespace KITEPlacement
{
    public class ConnectionString
    {
        private static SqlConnection sqlCon;

        #region Connection
        public static SqlConnection DBConnection()
        {
            SqlConnection ObjCon = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ToString());
            return ObjCon;
        }
        #endregion        

        #region Method to Fetch Data using Query
        public static DataSet SqlExecProc(string qry, out string errMsg)
        {
            string strException = "";
            DataSet ds = new DataSet();
            try
            {
                sqlCon = ConnectionString.DBConnection();
                sqlCon.Open();
                SqlDataAdapter SqlDA = new SqlDataAdapter(qry, sqlCon);
                SqlDA.SelectCommand.CommandTimeout = 3600;
                SqlDA.Fill(ds);

                strException = "Successful";
            }
            catch (Exception ex)
            {
                strException = ex.ToString();
            }
            finally
            {
                sqlCon.Close();
                sqlCon.Dispose();
            }
            errMsg = strException;
            return ds;
        }
        #endregion

        #region Method to Fetch Records in DataSet with Input Parameters
        public static DataSet SqlExecFetchDataSet(string procName, ArrayList arrySPParam, ArrayList arrySPValue, out string errMsg)
        {
            string strException = "";
            DataSet dsData = new DataSet();

            try
            {
                sqlCon = ConnectionString.DBConnection();
                sqlCon.Open();
                SqlDataAdapter sqlda = new SqlDataAdapter(procName, sqlCon);
                sqlda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlda.SelectCommand.CommandTimeout = 3600;

                for (int i = 0; i < arrySPParam.Count; i++)
                {
                    if (arrySPValue[i].ToString().Trim() == "")
                        sqlda.SelectCommand.Parameters.AddWithValue(arrySPParam[i].ToString(), DBNull.Value);
                    else
                        sqlda.SelectCommand.Parameters.AddWithValue(arrySPParam[i].ToString(), arrySPValue[i].ToString());
                }
                sqlda.Fill(dsData);
                sqlCon.Close();
            }
            catch (Exception ex)
            {
                strException = ex.ToString();
            }
            finally
            {
                sqlCon.Close();
                sqlCon.Dispose();
            }
            strException = "Successful";
            errMsg = strException;
            return dsData;
        }
        #endregion

        #region Method to Insert Data
        public static void SqlExecInsProc(string procName, ArrayList arrySPParam, ArrayList arrySPValue, out string errMsg)
        {
            string strException = "";
            SqlTransaction sqlTran = null;

            try
            {
                sqlCon = ConnectionString.DBConnection();
                sqlCon.Open();

                sqlTran = sqlCon.BeginTransaction("SqlTran1");
                SqlCommand sqlCmd = new SqlCommand(procName, sqlCon, sqlTran);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.CommandTimeout = 3600;

                for (int i = 0; i < arrySPParam.Count; i++)
                {
                    if (arrySPValue[i].ToString().Trim() == "")
                        sqlCmd.Parameters.AddWithValue(arrySPParam[i].ToString(), DBNull.Value);
                    else
                        sqlCmd.Parameters.AddWithValue(arrySPParam[i].ToString(), arrySPValue[i].ToString());
                }
                sqlCmd.ExecuteNonQuery();

                strException = "Successful";
                sqlTran.Commit();
            }
            catch(Exception ex)
            {
                strException = ex.ToString();
            }
            finally
            {
                if (sqlTran != null)
                    sqlTran.Dispose();
                sqlCon.Close();
                sqlCon.Dispose();
            }
            errMsg = strException;
        }
        #endregion

        #region Bulk Insert
        public static void FnSqlBulkInsert(string tableName, DataTable dataTable, out string errMsg)
        {
            string strException = "";
            using (var connection = ConnectionString.DBConnection())
            {
                connection.Open();
                using (SqlTransaction sqlTran = connection.BeginTransaction())
                {                    
                    using (var bulkCopy = new SqlBulkCopy(connection, SqlBulkCopyOptions.Default, sqlTran))
                    {
                        bulkCopy.DestinationTableName = tableName;
                        try
                        {
                            bulkCopy.WriteToServer(dataTable);
                            sqlTran.Commit();
                            strException = "Successful";
                        }
                        catch (Exception ex)
                        {
                            sqlTran.Rollback();
                            strException = ex.ToString();
                        }
                    }
                }
            }
            errMsg = strException;
        }
        #endregion

    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Data;
using System.Globalization;

namespace WMSSyncClient.components
{
    public class DB
    {
        public DB()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        int _sqlcode;
        string _SQLErrText;
        string _SQLStatement;
        long _dstrows;

        public SqlConnection SQLDBConnection = new SqlConnection();



        //System.Configuration.ConfigurationSettings  


        // ConnectionStringSettingsCollection ConStringsCollection = ConfigurationManager.ConnectionStrings;

        String SQLConnectionString =  Properties.Settings.Default.WMSDB.ToString();
        
        //ConfigurationManager.ConnectionStrings["EshopWebSync.Properties.Settings.SyncDB"].ToString();

        #region Properties

        public int sqlcode
        {
            get
            { return _sqlcode; }
            set { _sqlcode = value; }
        }
        public string SQLErrText
        {
            get
            { return _SQLErrText; }
            set { _SQLErrText = value; }
        }
        public string SQLStatement
        {
            get
            { return _SQLStatement; }
            set { _SQLStatement = value; }
        }
        //_dstrows
        public long dstrows
        {
            get
            { return _dstrows; }
            set { _dstrows = value; }
        }

        #endregion


        public void DBConnect()
        {
            
            try
            {
                SQLDBConnection.ConnectionString = SQLConnectionString;
                SQLDBConnection.Open();
            }
            catch (Exception ex)
            {
                sqlcode = -1;
                SQLErrText = ex.ToString();
            }


        } //end DBConnecto()

        public long DBGetNumResultFromSQLSelect(string SqlCmd)
        {
            SqlDataReader SQLReaderRetSelCmdResult = null;


            long CmdResultNum = 0;
            SqlCommand SQLSelectCmd = new SqlCommand();

            SQLSelectCmd.CommandText = SqlCmd;

            DBConnect();


            SQLSelectCmd.Connection = SQLDBConnection;

            try
            {
                SQLReaderRetSelCmdResult = SQLSelectCmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                SQLSelectCmd.Dispose();
                SQLReaderRetSelCmdResult.Dispose();
                _SQLErrText = ex.ToString();
                SQLDBConnection.Close();
                return -1;
            }


            while (SQLReaderRetSelCmdResult.Read())
            {
                try { CmdResultNum = long.Parse(SQLReaderRetSelCmdResult.GetValue(0).ToString()); }
                catch { CmdResultNum = -1; }
            }

            SQLSelectCmd.Dispose();
            SQLReaderRetSelCmdResult.Close();
            SQLReaderRetSelCmdResult.Dispose();
            SQLDBConnection.Close();


            return CmdResultNum;

        }// End DBWmsExSelectCmdRN2String

        public string DBWmsExSelectCmdRN2String(string SqlCmd)
        {
            SqlDataReader SQLReaderRetSelCmdResult;

            String SQLRtrnRowValue;

            string CmdResultNum = "";
            SqlCommand SQLSelectCmd = new SqlCommand();

            SQLSelectCmd.CommandText = SqlCmd;

            if (SQLDBConnection.State != ConnectionState.Open)
                DBConnect();

            if (SQLDBConnection.State != ConnectionState.Open)
                return "Null";

            SQLSelectCmd.Connection = SQLDBConnection;
            SQLReaderRetSelCmdResult = SQLSelectCmd.ExecuteReader();


            try
            {
                while (SQLReaderRetSelCmdResult.Read())
                {
                    CmdResultNum = SQLReaderRetSelCmdResult.GetValue(0).ToString();
                }
            }
            catch (Exception ex)
            {
                SQLErrText = ex.ToString();
                SQLStatement = SqlCmd;
            }
            SQLReaderRetSelCmdResult.Close();

            SQLDBConnection.Close();


            SQLRtrnRowValue = CmdResultNum;

            if (SQLRtrnRowValue.Length > 0)
            {
                return SQLRtrnRowValue;
            }
            else
            {
                return "Null";
            }

        }// End DBWmsExSelectCmdRN2String

        public string DBWmsExSelectCmdRStr2Str(string SqlCmd)
        {
            SqlDataReader SQLReaderRetSelCmdResult;

            String SQLRtrnRowValue;

            string CmdResultNum = "";
            SqlCommand SQLSelectCmd = new SqlCommand();

            SQLSelectCmd.CommandText = SqlCmd;

            if (SQLDBConnection.State != ConnectionState.Open)
                DBConnect();

            if (SQLDBConnection.State != ConnectionState.Open)
                return "Null";


            SQLSelectCmd.Connection = SQLDBConnection;
            SQLReaderRetSelCmdResult = SQLSelectCmd.ExecuteReader();


            try
            {
                while (SQLReaderRetSelCmdResult.Read())
                {
                    CmdResultNum = SQLReaderRetSelCmdResult.GetValue(0).ToString();
                }
            }
            catch (Exception ex)
            {
                SQLErrText = ex.ToString();
                SQLStatement = SqlCmd;
            }
            SQLReaderRetSelCmdResult.Close();

            SQLDBConnection.Close();


            SQLRtrnRowValue = CmdResultNum;

            if (SQLRtrnRowValue.Length > 0)
            {
                return SQLRtrnRowValue;
            }
            else
            {
                return "Null";
            }

        }// End DBWmsExSelectCmdRN2String


        public DataSet DBFillDataset(String SelectSqlStr, String FillTbl)
        {
            DataSet DbDS = new DataSet();

            if (SQLDBConnection.State != ConnectionState.Open)
                DBConnect();


            if (SQLDBConnection.State != ConnectionState.Open)
                return DbDS;

            SqlDataAdapter SqlDA = new SqlDataAdapter(SelectSqlStr, SQLDBConnection);

            try
            { SqlDA.Fill(DbDS, FillTbl); }
            catch (Exception ex)
            {
                SQLErrText = ex.ToString();
                SQLStatement = SelectSqlStr;
            }
            SQLDBConnection.Close();

            return DbDS;

        } // End DBFillDataset
        public DataTable DBFillDataTable(String SelectSqlStr, String FillTbl)
        {
            DataTable DbDT = new DataTable();

            if (SQLDBConnection.State != ConnectionState.Open)
                DBConnect();

            if (SQLDBConnection.State != ConnectionState.Open)
                return DbDT;

            SqlDataAdapter SqlDA = new SqlDataAdapter(SelectSqlStr, SQLDBConnection);

            try
            { SqlDA.Fill(DbDT); }
            catch (Exception ex)
            {
                SQLErrText = ex.ToString();
                SQLStatement = SelectSqlStr;
            }

            SQLDBConnection.Close();

            return DbDT;

        } // End DBFillDataset


        public long DBExecuteSQLCmd(String SqlExCmd)
        {


            long DBAffctRows;

            SqlCommand DBExSqlCommand;


            if (SqlExCmd.Length == 0 || SqlExCmd == "") { return -10; }

            try
            {
                if (SQLDBConnection.State != ConnectionState.Open)
                    DBConnect();

                
                DBExSqlCommand = new SqlCommand(SqlExCmd, SQLDBConnection);


                DBExSqlCommand.CommandType = CommandType.Text;
                DBAffctRows = DBExSqlCommand.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                f_sqlerrorlog(0, "InsertRecord>>", SqlExCmd.Replace("'", "|"), ">>", "Webservice", ">>");
                SQLDBConnection.Close();
                SQLErrText = ex.ToString();
                SQLStatement = SqlExCmd;
                return -1;
                // throw (ex);    // Rethrowing exception e
            }


            if (DBAffctRows > 0)
            {
                SQLDBConnection.Close();
                return DBAffctRows;
            }
            else
            {
                SQLDBConnection.Close();
                return -1;
            }

        } //end DBExecuteSQLCmd


        public long DBExecuteStoredProcedure(string SPname)
        {
            long Rtrn = 0;

            DataSet DbDS = new DataSet();



            SqlCommand SPCommand = new SqlCommand();

            SPCommand.CommandText = SPname;
            SPCommand.CommandType = CommandType.StoredProcedure;



            if (SQLDBConnection.State != ConnectionState.Open)
                DBConnect();

            SPCommand.Connection = SQLDBConnection;

            try
            { Rtrn = SPCommand.ExecuteNonQuery(); }
            catch (Exception ex)
            {
                SQLErrText = ex.ToString();
                SQLStatement = SPname;
                Rtrn = -3;
            }

            SQLDBConnection.Close();
            SPCommand.Dispose();


            return Rtrn;

        }
        public DataSet DBExecuteStoredProcedurePagedSeelction(string SPname, long PageNum, int PageSize)
        {

            DataSet DbDS = new DataSet();



            SqlCommand SPCommand = new SqlCommand();

            SPCommand.CommandText = SPname;
            SPCommand.CommandType = CommandType.StoredProcedure;


            SqlParameter PCurrentPage = new SqlParameter("@CURRENTPAGE", SqlDbType.Int, 4);
            SqlParameter PPageSize = new SqlParameter("@PAGESIZE", SqlDbType.Int, 4);


            PCurrentPage.Value = PageNum;
            PPageSize.Value = PageSize;

            SPCommand.Parameters.Add(PCurrentPage);
            SPCommand.Parameters.Add(PPageSize);

            if (SQLDBConnection.State != ConnectionState.Open)
                DBConnect();

            SPCommand.Connection = SQLDBConnection;

            SqlDataAdapter MysQLDA = new SqlDataAdapter(SPCommand);

            SPCommand.Connection = SQLDBConnection;

            MysQLDA.Fill(DbDS);
            SQLDBConnection.Close();
            SPCommand.Dispose();
            MysQLDA.Dispose();
            return DbDS;

        }
        public DataSet FDBFillDatasetFromSPWithParm(SqlCommand MyCommand, String FillTbl)
        {
            DataSet DbDS = new DataSet();



            SqlCommand SPCommand = new SqlCommand();
            SPCommand = MyCommand;

            if (SQLDBConnection.State != ConnectionState.Open)
                DBConnect();

            //cmdtxt = SPCommand.ToString(); 

            SqlDataAdapter MysQLDA = new SqlDataAdapter(SPCommand);

            SPCommand.Connection = SQLDBConnection;
            //MysQLDA.Fill(DbDS,FillTbl);
            MysQLDA.Fill(DbDS);
            SQLDBConnection.Close();
            SPCommand.Dispose();
            MysQLDA.Dispose();
            return DbDS;

        }
        public DataTable FDBFillDataTableFromSPWithParm(SqlCommand MyCommand, String FillTbl)
        {
            DataTable DbDT = new DataTable();



            SqlCommand SPCommand = new SqlCommand();
            SPCommand = MyCommand;

            if (SQLDBConnection.State != ConnectionState.Open)
                DBConnect();

            //cmdtxt = SPCommand.ToString(); 

            SqlDataAdapter MysQLDA = new SqlDataAdapter(SPCommand);

            SPCommand.Connection = SQLDBConnection;
            //MysQLDA.Fill(DbDS,FillTbl);
            MysQLDA.Fill(DbDT);
            SQLDBConnection.Close();
            SPCommand.Dispose();
            return DbDT;

        }

        public long FBlkCopyGeneral(DataTable SrcDT, string TargetTbl)
        {

            dstrows = 0;

            if (SQLDBConnection.State != ConnectionState.Open)
                DBConnect();

            if (SQLDBConnection.State != ConnectionState.Open)
                return -1;

            SqlBulkCopy SQLSvrBlkCopy = new SqlBulkCopy(SQLDBConnection);
            SQLSvrBlkCopy.DestinationTableName = TargetTbl;

            if (SrcDT.Rows.Count > 0)
            {
                try
                {
                    SQLSvrBlkCopy.WriteToServer(SrcDT);

                    dstrows = long.Parse(DBWmsExSelectCmdRN2String("SELECT COUNT(*) FROM " + TargetTbl));
                }
                catch (Exception ex)
                {
                    SQLErrText = ex.ToString();
                    if (!(dstrows > 0)) return -1;
                }
            }

            if (!(dstrows > 0))
                return 0;
            else
                return dstrows;
        }

        public string FDBInsertDecimal(decimal inval)
        {
            string decstr = null;

            if (inval > 0)
            {
                decstr = inval.ToString();
                decstr = decstr.Replace(",", ".");
            }
            else if (inval == 0)
                decstr = "0";

            return decstr;
        }

        public string FDBMSSQLFormatDate(string indate)
        {
            IFormatProvider MyDateTimeFormat = new CultureInfo("el-GR");
            string DBDate = "NULL";

            DateTime TryPrsDtTm;

            try
            {
                TryPrsDtTm = DateTime.Parse(indate, MyDateTimeFormat, DateTimeStyles.None);

                if (TryPrsDtTm.ToString().Length > 0)
                {
                    DBDate = "CONVERT(DateTime,'" + TryPrsDtTm.ToShortDateString() + "',103)";
                }
            }
            catch
            {
                DBDate = "NULL";
            }

            return DBDate;
        }//end of FDBMSSQLFormatDate

        public string FDBMSSQLFormatDateTime(string indate)
        {
            IFormatProvider MyDateTimeFormat = new CultureInfo("el-GR");
            string DBDate = "NULL";

            DateTime TryPrsDtTm;

            try
            {
                TryPrsDtTm = DateTime.Parse(indate, MyDateTimeFormat, DateTimeStyles.None);

                if (TryPrsDtTm.ToString().Length > 0)
                {
                    DBDate = "CONVERT(DateTime,'" + TryPrsDtTm.ToString() + "')";
                }
            }
            catch
            {
                DBDate = "NULL";
            }

            return DBDate;
        }//end of FDBMSSQLFormatDate


        public long f_sqlerrorlog(int CompId, string SrcCodeSnippet, string SqlErrText, string AppErrorText, string AppUserName, string WEBUserIPAddress)
        {


            String AppUserStr = "";
            String CompIdStr = "";
            String LogSqlErrText = "";

            StringBuilder sqlLOGstr = new StringBuilder();


            if (CompId > 0)
            {
                CompIdStr = CompId.ToString();
            }
            else
            {
                CompIdStr = "1";
            }

            if (AppUserName.Length > 0)
            {
                AppUserStr = AppUserName;
            }
            else
            {
                AppUserStr = "NULL";
            }

            if (SrcCodeSnippet.Length > 0)
            {
                LogSqlErrText = SrcCodeSnippet + ">>";
            }
            else
            {
                LogSqlErrText = ">>";
            }


            if (SqlErrText.Length > 0)
            {
                SqlErrText = SqlErrText + SqlErrText;
            }
            else
            {
                SqlErrText = ">>";
            }


            sqlLOGstr.Append("INSERT INTO TSYSEVENTLOGS(COMPID,DBErrorCode,DBERRORTEXT,APPUSER)  ");
            sqlLOGstr.Append(" VALUES (" + CompIdStr + ",'" + LogSqlErrText + "','" + SqlErrText + "',");


            sqlLOGstr.Append("'" + AppUserStr + "'");//)" );

            sqlLOGstr.Append(")");

            long DBAffctRows;

            SqlCommand DBExSqlCommand;


            if (sqlLOGstr.Length == 0) { return -1; }


            DBConnect();
            if (SQLDBConnection.State != ConnectionState.Open)
                return -1;

            DBExSqlCommand = new SqlCommand(sqlLOGstr.ToString(), SQLDBConnection);
            DBExSqlCommand.CommandType = CommandType.Text;

            try
            {
                DBAffctRows = DBExSqlCommand.ExecuteNonQuery();
                DBExSqlCommand.Dispose();
            }
            catch
            {
                DBExSqlCommand.Dispose();
                SQLDBConnection.Close();
                return -1;
            }

            if (DBAffctRows > 0)
            {
                SQLDBConnection.Close();
                return DBAffctRows;
            }
            else
            {
                SQLDBConnection.Close();
                return -1;
            } //if DBAffctRows

        } //enf f_sqlerorlog()
    }

}

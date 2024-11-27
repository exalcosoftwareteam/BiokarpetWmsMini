using System;
using System.Collections.Generic;
using System.Collections;
using System.Collections.Specialized;
using System.Collections.ObjectModel;
using System.Text;
using System.Configuration;
using System.Globalization;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;

namespace WMSMiniWebService
{

    public struct DBType
    {
        public static string ORACLEDB = "ORACLE";
        public static string MSSQLDB = "MSSQL";

    }

    public class DB
    {


        int _sqlcode;
        string _SQLErrText;
        string _SQLStatement;
        bool iserpconnexion = false;



        public static string _dbtype = DBType.MSSQLDB;

        public SqlConnection MSSQLConnection = new SqlConnection();

        ConnectionStringSettingsCollection connections = ConfigurationManager.ConnectionStrings;
        

        //        string ORAConnectionString = ConfigurationManager.ConnectionStrings["WCFExalcoPartsService.Properties.Settings.DBConnection"].ToString();

        string MSSQLConnectionString = ConfigurationManager.ConnectionStrings["WMSSQLConnectionString"].ToString();


        public DB()
        {
            MSSQLConnection.ConnectionString = MSSQLConnectionString;
        }

        public DB(bool ERPCONNECT)
        {
            iserpconnexion = true;
        }




        #region Properties

        public string dbtype
        {
            get { return _dbtype; }
        }
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

        #endregion

        public void DBConnect()
        {
            ConnectionStringSettings consettings = new ConnectionStringSettings();
            MSSQLConnection.ConnectionString = MSSQLConnectionString;
            try
            {
                MSSQLConnection.Open();
            }
            catch (Exception ex)
            {
                sqlcode = -1;
                SQLErrText = ex.ToString();
            }



        }



        public void DBDisconnect()
        {
            if (MSSQLConnection.State == ConnectionState.Open)
            {
                MSSQLConnection.Close();
            }
        }

 


        public long DBGetNumResultFromSQLSelectWithZero(string SqlCmd)
        {

            string CmdResultNum = "";
            SqlDataReader SQLReaderRetSelCmdResult;
            SqlCommand SQLSelectCmd = new SqlCommand();

            SQLSelectCmd.CommandText = SqlCmd;

            if (MSSQLConnection.State != ConnectionState.Open)
            {
                DBConnect();
            }

            SQLSelectCmd.Connection = MSSQLConnection;
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
                return -1;
            }

            SQLReaderRetSelCmdResult.Close();

            //
            MSSQLConnection.Close();
            SQLSelectCmd.Dispose();
            //
            try
            {
                if (long.Parse(CmdResultNum) >= 0) { return long.Parse(CmdResultNum); } else { return -1; }
            }
            catch
            {

                return -1;
            }



        }

        public long DBGetNumResultwithZero(string SqlCmd)
        {

            long SQLRtrnRowValue;
            string CmdResultNum = "";
            SqlDataReader SQLReaderRetSelCmdResult;
            SqlCommand SQLSelectCmd = new SqlCommand();

            SQLSelectCmd.CommandText = SqlCmd;

            if (MSSQLConnection.State != ConnectionState.Open)
            {
                DBConnect();
            }

            SQLSelectCmd.Connection = MSSQLConnection;
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
                return -1;
            }

            SQLReaderRetSelCmdResult.Close();

            //
            MSSQLConnection.Close();
            SQLSelectCmd.Dispose();
            //
            try
            {
                return long.Parse(CmdResultNum);
            }
            catch
            {

                return -1;
            }



        }

        public long DBGetNumResultFromSQLSelect(string SqlCmd)
        {

            string CmdResultNum = "";
            SqlDataReader SQLReaderRetSelCmdResult;
            SqlCommand SQLSelectCmd = new SqlCommand();

            SQLSelectCmd.CommandText = SqlCmd;

            if (MSSQLConnection.State != ConnectionState.Open)
            {
                DBConnect();
            }

            SQLSelectCmd.Connection = MSSQLConnection;
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
                return -1;
            }

            SQLReaderRetSelCmdResult.Close();

            //
            MSSQLConnection.Close();
            SQLSelectCmd.Dispose();
            //
            try
            {
                if (long.Parse(CmdResultNum) == -5) return -5;
                if (long.Parse(CmdResultNum) > 0) { return long.Parse(CmdResultNum); } else { return -1; }
            }
            catch
            {

                return -1;
            }



        }

        public long DBFastGetNumResultFromSQLSelect(string SqlCmd)
        {

            string CmdResultNum = "";
            SqlDataReader SQLReaderRetSelCmdResult;
            SqlCommand SQLSelectCmd = new SqlCommand();

            SQLSelectCmd.CommandText = SqlCmd;

            if (MSSQLConnection.State != ConnectionState.Open)
            {
                DBConnect();
            }

            SQLSelectCmd.Connection = MSSQLConnection;
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
                return -1;
            }

            SQLReaderRetSelCmdResult.Close();

            //
            SQLSelectCmd.Dispose();
            //
            try
            {
                if (long.Parse(CmdResultNum) > 0) { return long.Parse(CmdResultNum); } else { return -1; }
            }
            catch
            {

                return -1;
            }



        }

        //MULTIDATABASE READY

        public decimal DBGetDecimalResultFromSQLSelect(string SqlCmd)
        {

            string CmdResult = null;
            decimal rtrnvalue = 0;

            //MSSQL implementation
            decimal CmdResultNum = 0;
            SqlDataReader SQLReaderRetSelCmdResult;
            SqlCommand SQLSelectCmd = new SqlCommand();

            SQLSelectCmd.CommandText = SqlCmd;

            if (MSSQLConnection.State != ConnectionState.Open)
            {
                DBConnect();
            }

            if (MSSQLConnection.State != ConnectionState.Open)
            {
                return -10;  //connection failed
            }
            SQLSelectCmd.Connection = MSSQLConnection;
            SQLReaderRetSelCmdResult = SQLSelectCmd.ExecuteReader();


            try
            {
                while (SQLReaderRetSelCmdResult.Read())
                {
                    CmdResult = SQLReaderRetSelCmdResult.GetSqlDecimal(0).ToString();
                }

                if (!string.IsNullOrEmpty(CmdResult))
                {
                    rtrnvalue = decimal.Parse(CmdResult.Replace(",", "."));
                }
            }
            catch (Exception ex)
            {
                SQLErrText = ex.ToString();
                SQLStatement = SqlCmd;
                return -1;
            }

            SQLReaderRetSelCmdResult.Close();

            //
            MSSQLConnection.Close();
            SQLSelectCmd.Dispose();
            //

            return rtrnvalue;



        } //MULTIDATABASE READY

        public IDataReader DBReturnDatareaderResults(string SqlCmd)
        {

            SqlDataReader SQLReaderRetSelCmdResult;
            SqlCommand SQLSelectCmd = new SqlCommand();

            SQLSelectCmd.CommandText = SqlCmd;

            if (MSSQLConnection.State != ConnectionState.Open)
            {
                DBConnect();
            }

            if (MSSQLConnection.State != ConnectionState.Open)
                return null;  //connection failed


            try
            {
                SQLSelectCmd.Connection = MSSQLConnection;
                SQLReaderRetSelCmdResult = SQLSelectCmd.ExecuteReader();

                return SQLReaderRetSelCmdResult;
            }
            catch (Exception ex)
            {
                SQLErrText = ex.ToString();
                SQLStatement = SqlCmd;
                return null;
            }



        }//MULTIDATABASE READY

        public string DBGetStrResultFromSQLSelect(string SqlCmd)
        {

            SqlDataReader SQLReaderRetSelCmdResult;
            string CmdResult = null;
            SqlCommand SQLSelectCmd = new SqlCommand();
            SQLSelectCmd.CommandText = SqlCmd;
            if (MSSQLConnection.State != ConnectionState.Open)
                DBConnect();
            if (MSSQLConnection.State != ConnectionState.Open)
                return "-10";  //connection failed

            SQLSelectCmd.Connection = MSSQLConnection;
            SQLReaderRetSelCmdResult = SQLSelectCmd.ExecuteReader();

            try
            {
                while (SQLReaderRetSelCmdResult.Read())
                {
                    CmdResult = SQLReaderRetSelCmdResult.GetString(0).ToString();
                }
            }
            catch (Exception ex)
            {
                SQLErrText = ex.ToString();
                SQLStatement = SqlCmd;
                return "Null";
            }

            SQLReaderRetSelCmdResult.Close();
            MSSQLConnection.Close();
            SQLSelectCmd.Dispose();

            return CmdResult;


        }//MULTIDATABASE READY

        public DataSet DBFillDataset(String SelectSqlStr, String FillTbl)//MULTIDATABASE READY
        {
            DataSet DbDS = new DataSet();
            if (MSSQLConnection.State != ConnectionState.Open)
                DBConnect();

            if (MSSQLConnection.State != ConnectionState.Open)
                return DbDS;  //connection failed

            SqlDataAdapter SqlDA = new SqlDataAdapter(SelectSqlStr, MSSQLConnection);

            try
            { SqlDA.Fill(DbDS, FillTbl); }
            catch (Exception ex)
            {
                SQLErrText = ex.ToString();
                SQLStatement = SelectSqlStr;
            }


            MSSQLConnection.Close();
            SqlDA.Dispose();

            return DbDS;


        }

        public DataTable DBFillDataTable(String SelectSqlStr, String FillTbl)
        {
            DataTable DbDT = new DataTable(FillTbl);

            if (MSSQLConnection.State != ConnectionState.Open)
                DBConnect();

            if (MSSQLConnection.State != ConnectionState.Open)
            {
                sqlcode = -1;
                return DbDT;  //connection failed

            }
            SqlDataAdapter SqlDA = new SqlDataAdapter(SelectSqlStr, MSSQLConnection);

            try
            { SqlDA.Fill(DbDT); }
            catch (Exception ex)
            { SQLErrText = ex.ToString(); }
            MSSQLConnection.Close();
            SqlDA.Dispose();
            return DbDT;

        } // End DBFillDataTable //MULTIDATABASE READY

        public void WriteToLog(string message)
        {
            string applicationfolfer = HttpContext.Current.Server.MapPath("~");
            string filepath;
            string filename = "log.txt";

            filepath = applicationfolfer + "\\" + filename;

            try
            {
                if (!File.Exists(filepath))
                {
                    using (FileStream fs = File.Create(filepath))
                    {
                        Byte[] info = new UTF8Encoding(true).GetBytes(" ");
                        fs.Write(info, 0, info.Length);
                    }
                }


                string linetext = DateTime.Now.ToString() + " - " + message;
                FileInfo f = new FileInfo(filepath);

                if (f.Length > 1000000)
                {
                    try { File.WriteAllText(filepath, String.Empty); }
                    catch { }
                }

                TextWriter tsw = new StreamWriter(filepath, true);

                tsw.WriteLine(linetext);
                tsw.Close();
            }
            catch (Exception ex) { }
        }

        public long DBExecuteSQLCmd(String SqlExCmd)
        {
            long DBAffctRows;


            SqlCommand DBExSqlCommand;
            if (SqlExCmd.Length == 0 || SqlExCmd == "") { return -10; }
            try
            {
                if (MSSQLConnection.State != ConnectionState.Open)
                    DBConnect();
                if (MSSQLConnection.State != ConnectionState.Open)
                {
                    sqlcode = -1;
                    return -1;  //connection failed
                }

                DBExSqlCommand = new SqlCommand(SqlExCmd, MSSQLConnection);


                DBExSqlCommand.CommandType = CommandType.Text;
                DBAffctRows = DBExSqlCommand.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                WriteToLog(SqlExCmd+ex.Message.ToString());
                SQLErrText = ex.ToString();
                f_sqlerrorlog(1, SQLErrText.Replace("'", "|"), ex.InnerException.ToString().Replace("'", "|"));
                MSSQLConnection.Close();
                return -1;
            }
            if (DBAffctRows > 0)
            {
                MSSQLConnection.Close();
                return DBAffctRows;
            }
            else
            {
                MSSQLConnection.Close();
                return -1;


            } //MULTIDATABASE READY
        }

        public DataSet FDBFillDatasetFromSPWithParm(SqlCommand MyCommand, String FillTbl)
        {

            DataSet DbDS = new DataSet();

            SqlCommand SPCommand = new SqlCommand();
            SPCommand = MyCommand;
            DBConnect();
            if (MSSQLConnection.State != ConnectionState.Open)
            {
                sqlcode = -1;
                return DbDS;  //connection failed

            }

            SqlDataAdapter MysQLDA = new SqlDataAdapter(SPCommand);

            SPCommand.Connection = MSSQLConnection;
            MysQLDA.Fill(DbDS);

            MysQLDA.Dispose();
            MSSQLConnection.Close();
            SPCommand.Dispose();
            return DbDS;

        }//MULTIDATABASE READY
        
        public string DBDateFormatstring(string indate)
        {
            if (_dbtype == DBType.MSSQLDB)
                return "COVERT(DateTime,'" + indate + "',103)";
            else
                return "TO_DATE('" + indate + "','DD/MM/YYYY')";
        }

        public string DBDateTimeFormatstring(string indatetime)
        {
            if (_dbtype == DBType.MSSQLDB)
                return "COVERT(DateTime,'" + indatetime + "',103)";
            else
                return "TO_DATE('" + indatetime + "','DD/MM/YYYY HH:MM:SS')";
        }

        public string DBShortDatetoString(string indate)
        {
            IFormatProvider MyDateTimeFormat = new CultureInfo("el-GR");
            string shortdate = null;

            try
            {
                DateTime dt = DateTime.Parse(indate, MyDateTimeFormat);
                shortdate = dt.Day.ToString() + "/" + dt.Month.ToString() + "/" + dt.Year.ToString();
            }
            catch { }

            return shortdate;
        }

        public void f_sqlerrorlog(int CompId, String SrcCodeSnippet, String SqlErrText)
        {

            String sqlLOGstr = "";


            sqlLOGstr = "INSERT INTO TSYSEVENTLOGS(LogID,COMPID,LOGDATE,DBErrorCode,DBErrorText,APPUSER)  ";
            sqlLOGstr += " VALUES ((select count(logid)+1 from TSysEventLogs),1,+GETDATE(),'";
            sqlLOGstr += SrcCodeSnippet.Replace("'", "|") + "','" + SqlErrText.Replace("'", "|") + "','WEBSERVICE')";

            long DBAffctRows;

            SqlCommand DBExSqlCommand;


            if (sqlLOGstr.Length == 0 || sqlLOGstr == "") { return; }

            DBExSqlCommand = new SqlCommand(sqlLOGstr, MSSQLConnection);

            try
            {
                DBConnect();



                DBExSqlCommand.CommandType = CommandType.Text;
                DBAffctRows = DBExSqlCommand.ExecuteNonQuery();
            }
            catch
            {
                DBExSqlCommand.Dispose();
                MSSQLConnection.Close();

                return;
                // throw (ex);    // Rethrowing exception e
            }

            if (DBAffctRows > 0)
            {
                DBExSqlCommand.Dispose();
                MSSQLConnection.Close();
                return;
            }
            else
            {
                DBExSqlCommand.Dispose();
                MSSQLConnection.Close();
                return;
            }



        }
     
        public long f_sqlerrorlog(int CompId, string SrcCodeSnippet, string SqlErrText, string AppErrorText, string AppUserName, string WEBUserIPAddress)
        {
            return -1;
        }


    }
}

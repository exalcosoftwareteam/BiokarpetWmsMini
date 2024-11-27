using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Specialized;
using System.Collections.ObjectModel;
using System.Data.OracleClient;
using System.Configuration;
using System.Globalization;



namespace WMSSyncService
{

    /// <summary>
    /// Summary description for DBTrans
    /// </summary>
    public class OraDB
    {
        public OraDB()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        int _sqlcode;
        string _SQLErrText;
        string _SQLStatement;

        public OracleConnection ORADBConnection = new OracleConnection();
        
        
        //System.Configuration.ConfigurationSettings  

        String ORAConnectionString = ConfigurationManager.ConnectionStrings["WMSSyncService.Properties.Settings.ORADB"].ToString();


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

        #endregion

        public void DBConnect()
        {

            ORADBConnection.ConnectionString = ORAConnectionString;


            try
            {
                ORADBConnection.Open();
            }
            catch (Exception ex)
            {
                sqlcode = -1;
                SQLErrText = ex.ToString();
            }


        } //end DBConnectoWms()

        public void DBDisConnect()
        {
            try { ORADBConnection.Close(); }
            catch(Exception ex)
            {
                Log mylog = new Log();
                mylog.WriteToLog(ex.Message);
                mylog = null;
            }
            

        } //end DBConnectoWms()

        public string DBWmsExSelectCmdRN2String(string SqlCmd)
        {

            //  string OraRtrnRowValue;

            OracleNumber CmdResultNum = (OracleNumber)0 ;
            OracleDataReader SQLReaderRetSelCmdResult;

            String SQLRtrnRowValue;


            OracleCommand SQLSelectCmd = new OracleCommand();

            SQLSelectCmd.CommandText = SqlCmd;

            if (ORADBConnection.State != ConnectionState.Open)
            {
                DBConnect();
            }

            if (ORADBConnection.State != ConnectionState.Open)
                return "-10";  //connection failed



            SQLSelectCmd.Connection = ORADBConnection;
            SQLReaderRetSelCmdResult = SQLSelectCmd.ExecuteReader();


            try
            {
                while (SQLReaderRetSelCmdResult.Read())
                {
                    CmdResultNum = SQLReaderRetSelCmdResult.GetOracleNumber(0);
                }
            }
            catch (Exception ex)
            {
                SQLErrText = ex.ToString();
                SQLStatement = SqlCmd;
                return "Null";
            }

            SQLReaderRetSelCmdResult.Close();

            //
            ORADBConnection.Close();
            SQLSelectCmd.Dispose();
            //

            SQLRtrnRowValue = CmdResultNum.ToString();

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
            OracleDataReader SQLReaderRetSelCmdResult;


            string CmdResultNum = "";
            OracleCommand SQLSelectCmd = new OracleCommand();

            SQLSelectCmd.CommandText = SqlCmd;

            if (ORADBConnection.State != ConnectionState.Open)
                DBConnect();
            if (ORADBConnection.State != ConnectionState.Open)
                return "-10";  //connection failed

            SQLSelectCmd.Connection = ORADBConnection;
            SQLReaderRetSelCmdResult = SQLSelectCmd.ExecuteReader();

            try
            {
                while (SQLReaderRetSelCmdResult.Read())
                {
                    CmdResultNum = SQLReaderRetSelCmdResult.GetOracleString(0).ToString();
                }
            }
            catch (Exception ex)
            {
                SQLErrText = ex.ToString();
                SQLStatement = SqlCmd;
                return "Null";
            }

            SQLReaderRetSelCmdResult.Close();

            ORADBConnection.Close();
            SQLSelectCmd.Dispose();


            if (CmdResultNum.Length > 0)
            {
                return CmdResultNum;
            }
            else
            {
                return "Null";
            }

        }// End DBWmsExSelectCmdRN2String

        public DataSet DBFillDataset(String SelectSqlStr, String FillTbl)
        {
            DataSet DbDS = new DataSet();

            if (ORADBConnection.State != ConnectionState.Open)
                DBConnect();

            if (ORADBConnection.State != ConnectionState.Open)
                return DbDS;  //connection failed

            OracleDataAdapter SqlDA = new OracleDataAdapter(SelectSqlStr, ORADBConnection);

            try
            { SqlDA.Fill(DbDS, FillTbl); }
            catch (Exception ex)
            {
                SQLErrText = ex.ToString();
                SQLStatement = SelectSqlStr;
            }


            ORADBConnection.Close();
            SqlDA.Dispose();

            return DbDS;

        } // End DBFillDataset

        //SPECIFIC FILL DATATABLE FROM ALTERNATIVE ATLANTIS CONENCTION
        public DataTable DBFillDataTableAtlantis(String SelectSqlStr, String FillTbl)
        {
            DataTable DbDT = new DataTable();

            OracleConnection ORAAtlantisConnection;
            String ORAAtlantisConnectionString = ConfigurationManager.ConnectionStrings["WebSync.Properties.Settings.ORAATLANTIS"].ToString();
            ORAAtlantisConnection = new OracleConnection();
            ORAAtlantisConnection.ConnectionString = ORAAtlantisConnectionString;

            try
            { ORAAtlantisConnection.Open(); }
            catch
            { return DbDT; }

            if (ORAAtlantisConnection.State != ConnectionState.Open)
            {
                sqlcode = -1;
                return DbDT;  //connection failed
            }

            OracleDataAdapter SqlDA = new OracleDataAdapter(SelectSqlStr, ORAAtlantisConnection);

            try
            { SqlDA.Fill(DbDT); }
            catch (Exception ex)
            {
                SQLErrText = ex.ToString();
                SQLStatement = SelectSqlStr;
            }


            ORAAtlantisConnection.Close();
            SqlDA.Dispose();


            return DbDT;

        } // End DBFillDataTable

        public string DBWmsExSelectCmdRStr2StrAtlantis(string SqlCmd)
        {
            OracleDataReader SQLReaderRetSelCmdResult;

            string CmdResultNum = "";


            String ORAAtlantisConnectionString = ConfigurationManager.ConnectionStrings["WebSync.Properties.Settings.ORAATLANTIS"].ToString();

            OracleConnection ORAAtlantisConnection = new OracleConnection();
            ORAAtlantisConnection.ConnectionString = ORAAtlantisConnectionString;


            OracleCommand SQLSelectCmd = new OracleCommand();
            SQLSelectCmd.CommandText = SqlCmd;

            if (ORAAtlantisConnection.State != ConnectionState.Open)
                ORAAtlantisConnection.Open();
            if (ORAAtlantisConnection.State != ConnectionState.Open)
                return "-10";  //connection failed

            SQLSelectCmd.Connection = ORAAtlantisConnection;
            SQLReaderRetSelCmdResult = SQLSelectCmd.ExecuteReader();

            try
            {
                while (SQLReaderRetSelCmdResult.Read())
                {
                    CmdResultNum = SQLReaderRetSelCmdResult.GetOracleString(0).ToString();
                }
            }
            catch (Exception ex)
            {
                SQLErrText = ex.ToString();
                SQLStatement = SqlCmd;
                return "Null";
            }

            SQLReaderRetSelCmdResult.Close();

            ORAAtlantisConnection.Close();
            SQLSelectCmd.Dispose();


            if (CmdResultNum.Length > 0)
            {
                return CmdResultNum;
            }
            else
            {
                return "Null";
            }

        }// End DBWmsExSelectCmdRN2String
      
        public DataTable DBFillDataTable(String SelectSqlStr, String FillTbl)
        {
            DataTable DbDT = new DataTable();

            if (ORADBConnection.State != ConnectionState.Open)
                DBConnect();

            if (ORADBConnection.State != ConnectionState.Open)
            {
                sqlcode = -1;
                return DbDT;  //connection failed

            }
            OracleDataAdapter SqlDA = new OracleDataAdapter(SelectSqlStr, ORADBConnection);

            try
            { SqlDA.Fill(DbDT); }
            catch (Exception ex)
            { SQLErrText = ex.ToString(); }



            ORADBConnection.Close();
            SqlDA.Dispose();

            return DbDT;

        } // End DBFillDataTable

        public long DBExecuteSQLCmd(String SqlExCmd)
        {

            //OracleTransaction OraTrans = null;
            //OraTrans.Commit();

            long DBAffctRows;

            OracleCommand DBExSqlCommand;


            if (SqlExCmd.Length == 0 || SqlExCmd == "") { return -10; }


            try
            {
                if (ORADBConnection.State != ConnectionState.Open)
                    DBConnect();
                if (ORADBConnection.State != ConnectionState.Open)
                {
                    sqlcode = -1;
                    return -1;  //connection failed

                }
                // Start transaction               

                DBExSqlCommand = new OracleCommand(SqlExCmd, ORADBConnection);


                DBExSqlCommand.CommandType = CommandType.Text;
                DBAffctRows = DBExSqlCommand.ExecuteNonQuery();

            }
            catch //(InvalidCastException ex)
            {
                ORADBConnection.Close();
                return -1;
                // throw (ex);    // Rethrowing exception e
            }


            if (DBAffctRows > 0)
            {
                ORADBConnection.Close();
                return DBAffctRows;
            }
            else
            {
                ORADBConnection.Close();
                return -1;
            }

        } //end DBExecuteSQLCmd

        public DataSet FDBFillDatasetFromSPWithParm(OracleCommand MyCommand, String FillTbl)
        {
            DataSet DbDS = new DataSet();

            OracleCommand SPCommand = new OracleCommand();
            SPCommand = MyCommand;
            DBConnect();
            if (ORADBConnection.State != ConnectionState.Open)
            {
                sqlcode = -1;
                return DbDS;  //connection failed

            }
            //cmdtxt = SPCommand.ToString(); 

            OracleDataAdapter MysQLDA = new OracleDataAdapter(SPCommand);

            SPCommand.Connection = ORADBConnection;
            //MysQLDA.Fill(DbDS,FillTbl);
            MysQLDA.Fill(DbDS);

            MysQLDA.Dispose();
            ORADBConnection.Close();
            SPCommand.Dispose();
            return DbDS;

        }
        
        public DataTable FDBFillDataTableFromSPWithParm(OracleCommand MyCommand, String FillTbl)
        {
            DataTable DbDT = new DataTable();

            OracleCommand SPCommand = new OracleCommand();
            SPCommand = MyCommand;
            DBConnect();
            if (ORADBConnection.State != ConnectionState.Open)
            {
                sqlcode = -1;
                return DbDT;  //connection failed

            }
            //cmdtxt = SPCommand.ToString(); 

            OracleDataAdapter MysQLDA = new OracleDataAdapter(SPCommand);

            SPCommand.Connection = ORADBConnection;
            //MysQLDA.Fill(DbDS,FillTbl);
            MysQLDA.Fill(DbDT);

            MysQLDA.Dispose();
            ORADBConnection.Close();
            SPCommand.Dispose();


            return DbDT;

        }
        
        public string FDBORAFormatDate(string indate)
        {
            IFormatProvider MyDateTimeFormat = new CultureInfo("el-GR");
            string DBDate = "NULL";

            DateTime TryPrsDtTm;

            try
            {
                TryPrsDtTm = DateTime.Parse(indate, MyDateTimeFormat, DateTimeStyles.None);
                if (TryPrsDtTm.ToString().Length > 0)
                {
                    DBDate = "TO_DATE('" + TryPrsDtTm.ToShortDateString() + "')";
                }
            }
            catch
            {
                DBDate = "NULL";
            }

            return DBDate;
        }

        public IDataReader DBReturnDatareaderResults(string SqlCmd)
        {
                OracleDataReader SQLReaderRetSelCmdResult;
                OracleCommand SQLSelectCmd = new OracleCommand();

                SQLSelectCmd.CommandText = SqlCmd;

                if (ORADBConnection.State != ConnectionState.Open)
                {
                    DBConnect();
                }

                if (ORADBConnection.State != ConnectionState.Open)
                    return null;  //connection failed


                try
                {
                    SQLSelectCmd.Connection = ORADBConnection;
                    SQLReaderRetSelCmdResult = SQLSelectCmd.ExecuteReader();

                    return SQLReaderRetSelCmdResult;
                }
                catch (Exception ex)
                {
                    Log mylog = new Log();
                    mylog.WriteToLog(ex.Message);
                    mylog = null;
                    SQLErrText = ex.ToString();
                    SQLStatement = SqlCmd;
                    return null;
                }


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

        public long DBGetNumResultFromSQLSelect(string SqlCmd)
        {

            long SQLRtrnRowValue;

                OracleNumber CmdResultNum = (OracleNumber)0;
                OracleDataReader SQLReaderRetSelCmdResult;
                OracleCommand SQLSelectCmd = new OracleCommand();

                SQLSelectCmd.CommandText = SqlCmd;

                if (ORADBConnection.State != ConnectionState.Open) { DBConnect(); }

                if (ORADBConnection.State != ConnectionState.Open) { return -10; }  //connection failed

                SQLSelectCmd.Connection = ORADBConnection;
                SQLReaderRetSelCmdResult = SQLSelectCmd.ExecuteReader();


                try
                {
                    while (SQLReaderRetSelCmdResult.Read())
                    {
                        CmdResultNum = SQLReaderRetSelCmdResult.GetOracleNumber(0);
                    }

                    SQLRtrnRowValue = long.Parse(CmdResultNum.ToString());
                }
                catch (Exception ex)
                {
                    Log mylog = new Log();
                    mylog.WriteToLog(ex.Message);
                    mylog = null;
                    SQLErrText = ex.ToString();
                    SQLStatement = SqlCmd;
                    return -1;
                }

                SQLReaderRetSelCmdResult.Close();
                ORADBConnection.Close();
                SQLSelectCmd.Dispose();
                //
                if (SQLRtrnRowValue > 0) { return SQLRtrnRowValue; } else { return -1; }
            
        }

        public void f_sqlerrorlog(int CompId, String SrcCodeSnippet, String SqlErrText, String AppUserName)
        {

            String sqlLOGstr = "";
            String AppUserStr = "";
            String CompIdStr = "";
            String LogSqlErrText = "";


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


            sqlLOGstr = "INSERT INTO TSYSEVENTLOGS(COMPID,LOGDATE,DBERRORTEXT,APPUSER)  ";
            sqlLOGstr += " VALUES (" + CompIdStr + ",GETDATE(),'" + LogSqlErrText + "',";
            sqlLOGstr = sqlLOGstr + "'" + AppUserStr + "')";

            long DBAffctRows;

            OracleCommand DBExSqlCommand;


            if (sqlLOGstr.Length == 0 || sqlLOGstr == "") { return; }

            DBExSqlCommand = new OracleCommand(sqlLOGstr, ORADBConnection);

            try
            {
                DBConnect();



                DBExSqlCommand.CommandType = CommandType.Text;
                DBAffctRows = DBExSqlCommand.ExecuteNonQuery();
            }
            catch
            {
                DBExSqlCommand.Dispose();
                ORADBConnection.Close();
                return;
                // throw (ex);    // Rethrowing exception e
            }

            if (DBAffctRows > 0)
            {
                DBExSqlCommand.Dispose();
                ORADBConnection.Close();
                return;
            }
            else
            {
                DBExSqlCommand.Dispose();
                ORADBConnection.Close();
                return;
            } //if DBAffctRows

        } //enf f_sqlerorlog()
  
    
    }




    #region DBCollections
    public struct OrderStatusVariants
    {
        public const short UnConfirmed = 0;
        public const short Undone = 1;
        public const short PartiallyDone = 2;
        public const short Done = 3;
        public const short Shipped = 4;
    }

    public struct BinTypesCollection
    {
        public const short BinTypeReceive = 1;
        public const short BinTypeShipping = 2;
        public const short BinTypePutaway = 3;
        public const short BinTypePicking = 4;
        public const short BinTypeTransfer = 5;
        public const short BinTypePacketing = 6;
        public const short BinTypeProduction = 7;
        public const short BinTypeQuarantine = 8;
    }


    public struct TransTypesCollection
    {
        public const int TransTypePutawayInsert = 1;
        public const int TransTypeRemove = 2;
        public const int TransTypeShippingRemove = 3;
        public const int TransTypeInternalMove = 4;
        public const int TransTypeProdReceive = 21;
        public const int TransTypeProdSend = 23;
        public const int TransTypeInventoryAdd = 62;
        public const int TransTypeInventoryRemove = 63;
        public const int TransTypeOrderPicking = 101;

    }

    public struct TablesCollection
    {
        public const string TItems = "TITEMS";
        public const string TItemLot = "TITEMLOT";
    }

    public struct TableIds
    {
        public const string ItemID = "ITEMID";
        public const string LotID = "LOTID";

    }


    public struct WSVCReturnValues
    {
        public const long generalerror = -1;
        public const long dbconnectionfailed = -10;
        public const long serviceunreachable = -20;
        public const long notenouphqty = -101;
    }

    public struct DBOperationsReturnvalues
    {
        public const long notenouphqty = -101;
    }
    #endregion

} //end namespace
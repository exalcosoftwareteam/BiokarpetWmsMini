using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlServerCe;
using System.Data.SqlTypes;
using System.IO;
using System.Xml;
using System.Globalization;


namespace WMSMobileClient.Components
{

    public class DB
    {
        int _sqlcode;
        string _SQLErrText;
        string _SQLStatement;
        long _dstrows;
        public string DBFilePAth;

        public string SQLConnectionString;
        long DBManAffctRows;
        SqlCeCommand DBExSqlCommand;
        public SqlCeConnection SQLDBConnection = new SqlCeConnection();

        public DB()
        {
            DBFilePAth = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase) + "\\WmsMiniDB.sdf";
            SQLConnectionString = "Data Source ='" + DBFilePAth + "';LCID=1032;" + "Password =" + "'bidata';";
            SQLDBConnection.ConnectionString = SQLConnectionString;
        }



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


        public void FCompactSqlErrorLog(string Errcode, string ErrText)
        {
            DBExecuteSQLCmd("INSERT INTO TsysErrorLog(ErrorCode,ErrorText) VALUES('" + Errcode.Replace("'", "|") + "','" + ErrText.Replace("'", "|") + "')");
        }




        public void DBConnect()
        {

            SQLDBConnection.ConnectionString = SQLConnectionString;
            try
            {
                SQLDBConnection.Open();
            }
            catch (Exception ex)
            {
                sqlcode = -1;
                SQLErrText = ex.ToString();
            }


        } //end DBConnecto()

        public void DBDisConnect()
        {

            //SQLDBConnection.ConnectionString = SQLConnectionString;
            try
            {
                SQLDBConnection.Close();
             }
            catch (Exception ex)
            {
                sqlcode = -1;
                SQLErrText = ex.ToString();
            }


        } //end DBConnecto()


        public string DBWmsExSelectCmdRN2String(string SqlCmd)
        {
            SqlCeDataReader SQLReaderRetSelCmdResult;

            String SQLRtrnRowValue;

            string CmdResultNum = "";
            SqlCeCommand SQLSelectCmd = new SqlCeCommand();

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
            SqlCeDataReader SQLReaderRetSelCmdResult;

            String SQLRtrnRowValue;

            string CmdResultNum = "";
            SqlCeCommand SQLSelectCmd = new SqlCeCommand();

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
            SQLReaderRetSelCmdResult.Dispose();

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

        public long DBGetNumResultFromSQLSelect(string SqlCmd)
        {
            SqlCeDataReader SQLReaderRetSelCmdResult = null;


            long CmdResultNum = 0;
            SqlCeCommand SQLSelectCmd = new SqlCeCommand();

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
                // SQLReaderRetSelCmdResult.Dispose();
                _SQLErrText = ex.ToString();
                SQLDBConnection.Close();
                return -1;
            }


            while (SQLReaderRetSelCmdResult.Read())
            {
                try { CmdResultNum = long.Parse(SQLReaderRetSelCmdResult.GetValue(0).ToString()); }
                catch(Exception ex) { CmdResultNum = -1; }
            }

            SQLSelectCmd.Dispose();
            SQLReaderRetSelCmdResult.Close();
            SQLReaderRetSelCmdResult.Dispose();
            SQLDBConnection.Close();


            return CmdResultNum;

        }// End DBWmsExSelectCmdRN2String

        public long DBGetNumResultFromSQLSelectConON(string SqlCmd)
        {
            SqlCeDataReader SQLReaderRetSelCmdResult = null;

            long CmdResultNum = 0;
            SqlCeCommand SQLSelectCmd = new SqlCeCommand();
            if (SQLDBConnection.State != ConnectionState.Open) 
            {

                SQLDBConnection.Open();
            }

            SQLSelectCmd.CommandText = SqlCmd;

            SQLSelectCmd.Connection = SQLDBConnection;

            try
            {
                SQLReaderRetSelCmdResult = SQLSelectCmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                SQLSelectCmd.Dispose();
                // SQLReaderRetSelCmdResult.Dispose();
                _SQLErrText = ex.ToString();
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

            return CmdResultNum;

        }// End DBWmsExSelectCmdRN2String

        public decimal DBGeDecimalResultConON(string SqlCmd)
        {
            SqlCeDataReader SQLReaderRetSelCmdResult = null;

            decimal CmdResultNum = 0;
            SqlCeCommand SQLSelectCmd = new SqlCeCommand();
            if (SQLDBConnection.State != ConnectionState.Open)
            {

                SQLDBConnection.Open();
            }

            SQLSelectCmd.CommandText = SqlCmd;

            SQLSelectCmd.Connection = SQLDBConnection;

            try
            {
                SQLReaderRetSelCmdResult = SQLSelectCmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                SQLSelectCmd.Dispose();
                // SQLReaderRetSelCmdResult.Dispose();
                _SQLErrText = ex.ToString();
                return -1;

            }


            while (SQLReaderRetSelCmdResult.Read())
            {
                try { CmdResultNum = Decimal.Parse(SQLReaderRetSelCmdResult.GetValue(0).ToString()); }
                catch { CmdResultNum = 0; }
            }

            SQLSelectCmd.Dispose();
            SQLReaderRetSelCmdResult.Close();
            SQLReaderRetSelCmdResult.Dispose();

            return CmdResultNum;

        }// End DBWmsExSelectCmdRN2String

        public DataSet DBFillDataset(String SelectSqlStr, String FillTbl)
        {
            DataSet DbDS = new DataSet();

            if (SQLDBConnection.State != ConnectionState.Open)
                DBConnect();


            if (SQLDBConnection.State != ConnectionState.Open)
                return DbDS;
             
            SqlCeDataAdapter SqlDA = new SqlCeDataAdapter(SelectSqlStr, SQLDBConnection);

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

            SqlCeDataAdapter SqlDA = new SqlCeDataAdapter(SelectSqlStr, SQLDBConnection);

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

        public long DBExecuteSQLCmdManual(String SqlExCmd)
        {



            try
            {

                DBExSqlCommand = new SqlCeCommand(SqlExCmd, SQLDBConnection);
                DBExSqlCommand.CommandType = CommandType.Text;
                DBManAffctRows = DBExSqlCommand.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Logger.Flog("DBExecuteSQLCmd::" + ex.ToString() + " | SQL |" + SqlExCmd);
                return -1;
                // throw (ex);    // Rethrowing exception e
            }


            return DBManAffctRows;
 

        } //end DBExecuteSQLCmd




        public long DBExecuteSQLCmd(String SqlExCmd)
        {

            long DBAffctRows;

            SqlCeCommand DBExSqlCommand = null;


            if (SqlExCmd.Length == 0 || SqlExCmd == "") { return -10; }

            try
            {
                if (SQLDBConnection.State != ConnectionState.Open)
                    DBConnect();

                DBExSqlCommand = new SqlCeCommand(SqlExCmd, SQLDBConnection);



                DBExSqlCommand.CommandType = CommandType.Text;
                DBAffctRows = DBExSqlCommand.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Logger.Flog("DBExecuteSQLCmd::" + ex.ToString() + " | SQL |" + SqlExCmd);
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



            SqlCeCommand SPCommand = new SqlCeCommand();

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

        public DataTable FDBFillDataTableFromSPWithParm(SqlCeCommand MyCommand, String FillTbl)
        {
            DataTable DbDT = new DataTable();



            SqlCeCommand SPCommand = new SqlCeCommand();
            SPCommand = MyCommand;

            if (SQLDBConnection.State != ConnectionState.Open)
                DBConnect();

            //cmdtxt = SPCommand.ToString(); 

            SqlCeDataAdapter MysQLDA = new SqlCeDataAdapter(SPCommand);

            SPCommand.Connection = SQLDBConnection;
            //MysQLDA.Fill(DbDS,FillTbl);
            MysQLDA.Fill(DbDT);
            SQLDBConnection.Close();
            SPCommand.Dispose();
            return DbDT;

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

            SqlCeCommand DBExSqlCommand;


            if (sqlLOGstr.Length == 0 || sqlLOGstr == "") { return; }


            try
            {
                if (SQLDBConnection.State != ConnectionState.Open)
                    DBConnect();

                DBExSqlCommand = new SqlCeCommand(sqlLOGstr, SQLDBConnection);

                DBExSqlCommand.CommandType = CommandType.Text;
                DBAffctRows = DBExSqlCommand.ExecuteNonQuery();
            }
            catch
            {
                SQLDBConnection.Close();
                return;
                // throw (ex);    // Rethrowing exception e
            }

            if (DBAffctRows > 0)
            {
                SQLDBConnection.Close();
                return;
            }
            else
            {
                SQLDBConnection.Close();
                return;
            } //if DBAffctRows

        } //enf f_sqlerorlog()
    }

    public class CompactDB
    {
        public DB db = new DB();
        string dberror;
        public string DBFilePAth;

        FileInfo iFile;

  

        public string DBError
        { get { return dberror; } }

        public CompactDB()
        {
            try
            { DBFilePAth = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase) + "\\WmsMiniDB.sdf"; }
            catch (Exception ex) { dberror = ex.ToString(); }

            if (File.Exists(DBFilePAth)) iFile = new FileInfo(DBFilePAth);
        }

        public bool FileExists()
        {
            if (File.Exists(DBFilePAth))
                return true;
            else
                return false;

        }

        public long DBSizeInKB()
        {

            if (iFile.Exists)
                return iFile.Length / 1024;
            else
                return 0;
        }

        public long CreateDB()
        {
            db = new DB();

            SqlCeEngine MySQLCEengine = new SqlCeEngine(db.SQLDBConnection.ConnectionString);

            try { MySQLCEengine.CreateDatabase(); }
            catch (Exception ex) { dberror = ex.ToString(); return -1; }

            if (File.Exists(DBFilePAth)) iFile = new FileInfo(DBFilePAth);

            return 1;
        }

        public long CreateTables()
        {
            long rtrn = 0;
            rtrn += CreateTableTstores();
            rtrn += CreateTableItems();
            rtrn += CreateTableTItemMunits();
            rtrn += CreateTableLots();
            rtrn += CreateTableInventoryHeader();
            rtrn += CreateTableInventory();
            rtrn += CreateTableOrder();
            rtrn += CreateTableOrderDetails();
            rtrn += CreateTablePackingListHeader();
            rtrn += CreateTablePackingListDetails();
            rtrn += CreateTableTsysErrorLog();
            return rtrn;

        }

        public bool DeleteDB()
        {
            db.DBDisConnect();
            try
            {
                if (File.Exists(DBFilePAth))
                    File.Delete(DBFilePAth);
                return true;
            }
            catch { return false; }
        }

        public long CreateTableItems()
        {
            long rtrn = 0;

            StringBuilder DDlSqlStr = new StringBuilder();

            DBExecuteDDLSQLCmd("DROP TABLE TItems", false);

            DDlSqlStr.Append("CREATE TABLE TItems (");
            DDlSqlStr.Append("ItemID Int NOT NULL,");
            DDlSqlStr.Append("ItemCode NVARCHAR(25) NOT NULL,");
            DDlSqlStr.Append("ENTRYDATE DateTime ,");
            DDlSqlStr.Append("CompID smallint,");
            DDlSqlStr.Append("ItemDesc NVARCHAR(150) ,");
            DDlSqlStr.Append("MUnitPrimary smallint,");
            DDlSqlStr.Append("MUnitSecondary smallint,");
            DDlSqlStr.Append("MUnitsRelation numeric(38, 9),");
            DDlSqlStr.Append("MUnitDesc1 NVARCHAR(50),");
            DDlSqlStr.Append("MUnitDesc2 NVARCHAR(50)");
            DDlSqlStr.Append(")");
            rtrn = DBExecuteDDLSQLCmd(DDlSqlStr.ToString(), false);

            DBExecuteDDLSQLCmd("ALTER TABLE TItems ADD CONSTRAINT PK_TItems PRIMARY KEY  (ItemID)", false);
            return rtrn;
        }

        public long CreateTableLots()
        {
            StringBuilder DDlSqlStr = new StringBuilder();
            long rtrn = 0;
            DBExecuteDDLSQLCmd("DROP TABLE TItemLot", false);
            DDlSqlStr.Append("CREATE TABLE TItemLOT (");
            DDlSqlStr.Append("LOTID Int NOT NULL,");
            DDlSqlStr.Append("ItemID Int,");
            DDlSqlStr.Append("LOTCode NVARCHAR(25),");
            DDlSqlStr.Append("LOTCreateDate DateTime ,");
            DDlSqlStr.Append("ENTRYDATE DateTime ,");
            DDlSqlStr.Append("LOTExpireDate DateTime,");
            DDlSqlStr.Append("DRAFT NVARCHAR(100),");
            DDlSqlStr.Append("COLOR NVARCHAR(140),");
            DDlSqlStr.Append("Width numeric(16,4),");
            DDlSqlStr.Append("Length numeric(16,4),");
            DDlSqlStr.Append("ItemPrimaryQty numeric(38,9),");
            DDlSqlStr.Append("ItemSecondaryQty numeric(38,9)");

            DDlSqlStr.Append(")");
            rtrn = DBExecuteDDLSQLCmd(DDlSqlStr.ToString(), false);
            DBExecuteDDLSQLCmd("ALTER TABLE TItemLOT ADD CONSTRAINT PK_TItemLOT PRIMARY KEY  (LOTID) ", false);
            DBExecuteDDLSQLCmd("CREATE INDEX IX_TItemLot_code ON TItemLOT (LOTCode) ", false);
            DBExecuteDDLSQLCmd("CREATE INDEX IX_TitemLot_entrydate ON TItemLOT ( ENTRYDATE ) ",false);
            return rtrn;
        }

        public long CreateTableTstores()
        {
            StringBuilder DDlSqlStr = new StringBuilder();
            long rtrn = 0;
            DBExecuteDDLSQLCmd("DROP TABLE TStores", false);
            DDlSqlStr.Append("CREATE TABLE TStores (");
            DDlSqlStr.Append("StoreID int NOT NULL,");
            DDlSqlStr.Append("CompID smallint ,");
            DDlSqlStr.Append("BranchID smallint ,");
            DDlSqlStr.Append("StoreName NVARCHAR(150) ,");
            DDlSqlStr.Append("SERVERIP NVARCHAR(20) ,");
            DDlSqlStr.Append("DSRIDTOCENTRAL int,");
            DDlSqlStr.Append("DSRIDTOBRANCH int");
            DDlSqlStr.Append(")");
            rtrn = DBExecuteDDLSQLCmd(DDlSqlStr.ToString(), false);
            DBExecuteDDLSQLCmd("ALTER TABLE TStores ADD CONSTRAINT  PK_TStores PRIMARY KEY  (StoreID) ", false);
 
            return rtrn;
        }



        public long CreateTableTsysErrorLog()
        {
            StringBuilder DDlSqlStr = new StringBuilder();
            long rtrn = 0;
            DBExecuteDDLSQLCmd("DROP TABLE TsysErrorLog", false);
            DDlSqlStr.Append("CREATE TABLE TsysErrorLog (");
            DDlSqlStr.Append("SysErrLogID int IDENTITY (100,1) NOT NULL,");
            DDlSqlStr.Append(" ErrorCode nvarchar (100),");
            DDlSqlStr.Append(" ErrorText nvarchar (250),");
            DDlSqlStr.Append("LogDate datetime not null default getdate()");
            DDlSqlStr.Append(")");
            rtrn = DBExecuteDDLSQLCmd(DDlSqlStr.ToString(), false);
            DBExecuteDDLSQLCmd("ALTER TABLE TsysErrorLog ADD CONSTRAINT  PK_TsysErrorLog PRIMARY KEY  (SysErrLogID) ", false);
            return rtrn;
        }



        public long CreateTableTItemMunits()
        {
            StringBuilder DDlSqlStr = new StringBuilder();
            long rtrn = 0;
            DBExecuteDDLSQLCmd("DROP TABLE TItemMunits", false);
            DDlSqlStr.Append("CREATE TABLE TItemMunits (");
            DDlSqlStr.Append("MunitID smallint NOT NULL,");
            DDlSqlStr.Append("CompID smallint ,");
            DDlSqlStr.Append("MUnit NVARCHAR(100),");
            DDlSqlStr.Append("MunitDecimals smallint ");
            DDlSqlStr.Append(")");
            rtrn = DBExecuteDDLSQLCmd(DDlSqlStr.ToString(), false);
            DBExecuteDDLSQLCmd("ALTER TABLE TItemMunits ADD CONSTRAINT  PK_TItemMunits PRIMARY KEY  (MunitID) ", false);
            return rtrn;
        }

        public long CreateTableInventoryHeader()
        {
            StringBuilder DDlSqlStr = new StringBuilder();
            long rtrn = 0;
            DBExecuteDDLSQLCmd("DROP TABLE TInventoryHeader", false);
            DDlSqlStr.Append("CREATE TABLE TInventoryHeader (");
            DDlSqlStr.Append("InvHdrID int NOT NULL,");
            DDlSqlStr.Append("InvHdrIDServer  int,");
            DDlSqlStr.Append("CompID smallint,");
            DDlSqlStr.Append("BranchID smallint,");
            DDlSqlStr.Append("StoreID smallint,");
            DDlSqlStr.Append("StoreName NVARCHAR(200),");
            DDlSqlStr.Append("CustomerID int,");
            DDlSqlStr.Append("CustomerTitle NVARCHAR(200),");
            DDlSqlStr.Append("InvDate DateTime,");
            DDlSqlStr.Append("InvStatus smallint,");
            DDlSqlStr.Append("InvComments NVARCHAR(100),");
            DDlSqlStr.Append("InvSyncID int");
            DDlSqlStr.Append(")");
            rtrn = DBExecuteDDLSQLCmd(DDlSqlStr.ToString(), false);
            DBExecuteDDLSQLCmd("ALTER TABLE TInventoryHeader ADD CONSTRAINT PK_TInventoryHeader PRIMARY KEY  (InvHdrID) ", false);
            return rtrn;
        }


        public long CreateTableOrder()
        {
            StringBuilder DDlSqlStr = new StringBuilder();
            long rtrn = 0;
            DBExecuteDDLSQLCmd("DROP TABLE TOrders", false);
            DDlSqlStr.Append("CREATE TABLE TOrders (");
            DDlSqlStr.Append("OrderID int  NOT NULL,");
            DDlSqlStr.Append("CompID smallint,");
            DDlSqlStr.Append("BranchID smallint,");
            DDlSqlStr.Append("StoreID smallint,");
            DDlSqlStr.Append("OrderCode NVARCHAR(100) NOT NULL,");
            DDlSqlStr.Append("StoreName NVARCHAR(200),");
            DDlSqlStr.Append("CustomerID int,");
            DDlSqlStr.Append("CustomerTitle NVARCHAR(100),");
            DDlSqlStr.Append("OrderDate DateTime,");
            DDlSqlStr.Append("SalesPersonID int,");
            DDlSqlStr.Append("OrderStatus smallint,");
            DDlSqlStr.Append("OrderSyncID int,");
            DDlSqlStr.Append("OrderComments  int");
            DDlSqlStr.Append(")");
            rtrn = DBExecuteDDLSQLCmd(DDlSqlStr.ToString(), false);
            DBExecuteDDLSQLCmd("ALTER TABLE TOrders ADD CONSTRAINT PK_TOrdersHeader PRIMARY KEY  (OrderID) ", false);
            return rtrn;
        }

        public long CreateTableOrderDetails()
        {
            StringBuilder DDlSqlStr = new StringBuilder();
            long rtrn = 0;
            DBExecuteDDLSQLCmd("DROP TABLE TOrderDetails", false);
            DDlSqlStr.Append("CREATE TABLE TOrderDetails (");
            DDlSqlStr.Append("OrderdtlID int IDENTITY (1, 1) NOT NULL,");
            DDlSqlStr.Append("OrderID int NOT NULL,");
            DDlSqlStr.Append("ItemID int,");
            DDlSqlStr.Append("OrderQtyPrimary numeric(38,9),");
            DDlSqlStr.Append("OrderMunitPrimary smallint,");
            DDlSqlStr.Append("OrderQtySecondary numeric(38,9),");
            DDlSqlStr.Append("OrderMunitSecondary smallint");
            DDlSqlStr.Append(")");
            rtrn = DBExecuteDDLSQLCmd(DDlSqlStr.ToString(), false);
            DBExecuteDDLSQLCmd("ALTER TABLE TOrderDetails ADD CONSTRAINT PK_TOrdersDetails PRIMARY KEY  (OrderdtlID) ", false);
            return rtrn;
        }

        public long CreateTablePackingListHeader()
        {
            StringBuilder DDlSqlStr = new StringBuilder();
            long rtrn = 0;
            DBExecuteDDLSQLCmd("DROP TABLE TWMSPackingListsHeader", false);
            DDlSqlStr.Append("CREATE TABLE TWMSPackingListsHeader (");
            DDlSqlStr.Append("PackingListHeaderID int  NOT NULL,");
            DDlSqlStr.Append("PackingListServerID int ,");
            DDlSqlStr.Append("PackingListDate DateTime ,");
            DDlSqlStr.Append("Compid int ,");
            DDlSqlStr.Append("BranchID int ,");
            DDlSqlStr.Append("StoreID int ,");
            DDlSqlStr.Append("StoreName NVARCHAR(80),");
            DDlSqlStr.Append("OrderID int,");
            DDlSqlStr.Append("OrderDtlID int,");
            DDlSqlStr.Append("CustomerID int,");
            DDlSqlStr.Append("CustomerCode NVARCHAR(80),");
            DDlSqlStr.Append("CustomerTitle NVARCHAR(80),");
            DDlSqlStr.Append("PackingListStatus smallint,");
            DDlSqlStr.Append("TransCode NVARCHAR(20),");
            DDlSqlStr.Append("TransType int,");
            DDlSqlStr.Append("DSRID int,");
            DDlSqlStr.Append("PackingListComments NVARCHAR(150)");
            DDlSqlStr.Append(")");
            rtrn = DBExecuteDDLSQLCmd(DDlSqlStr.ToString(), false);
            DBExecuteDDLSQLCmd("ALTER TABLE TWMSPackingListsHeader ADD CONSTRAINT PK_TWMSPackingListsHeader PRIMARY KEY  (PackingListHeaderID) ", false);
            return rtrn;
        }

        public long CreateTablePackingListDetails()
        {
            StringBuilder DDlSqlStr = new StringBuilder();
            long rtrn = 0;
            DBExecuteDDLSQLCmd("DROP TABLE TWMSPackingListDetails", false);
            DDlSqlStr.Append("CREATE TABLE TWMSPackingListDetails (");
            DDlSqlStr.Append("PackingListDTLID int IDENTITY (1, 1)  NOT NULL,");
            DDlSqlStr.Append("PackingListHeaderID int NOT NULL,");
            DDlSqlStr.Append("ItemID int,");
            DDlSqlStr.Append("LotID int,");
            DDlSqlStr.Append("ItemCode NVARCHAR(25),");
            DDlSqlStr.Append("ItemDesc NVARCHAR(150),");
            DDlSqlStr.Append("LOTCode NVARCHAR(25),");
            DDlSqlStr.Append("DRAFT NVARCHAR(40),");
            DDlSqlStr.Append("COLOR NVARCHAR(140),");
            DDlSqlStr.Append("Width numeric(16,4),");
            DDlSqlStr.Append("Length numeric(16,4),");
            DDlSqlStr.Append("PackingListDTLDate DateTime ,");
            DDlSqlStr.Append("ItemQtyPrimary numeric(38,9),");
            DDlSqlStr.Append("ItemMunitPrimary smallint,");
            DDlSqlStr.Append("ItemQtySecondary numeric(38,9),");
            DDlSqlStr.Append("ItemMunitSecondary smallint");
            DDlSqlStr.Append(")");
            rtrn = DBExecuteDDLSQLCmd(DDlSqlStr.ToString(), false);
            DBExecuteDDLSQLCmd("ALTER TABLE TWMSPackingListDetails ADD CONSTRAINT PK_TWMSPackingList PRIMARY KEY  (PackingListDTLID) ", false);
            return rtrn;
        }


        public long CreateTableInventory()
        {
            StringBuilder DDlSqlStr = new StringBuilder();
            long rtrn = 0;
            DBExecuteDDLSQLCmd("DROP TABLE TInventory", false);
            DDlSqlStr.Append("CREATE TABLE TInventory (");
            DDlSqlStr.Append("InvID int IDENTITY (1, 1) NOT NULL,");
            DDlSqlStr.Append("InvHdrID int ,");
            DDlSqlStr.Append("SerialNum nvarchar(50),");
            DDlSqlStr.Append("ItemID int,");
            DDlSqlStr.Append("ItemCode nvarchar(50),");
            DDlSqlStr.Append("LotID int,");
            DDlSqlStr.Append("InvNo int,");
            DDlSqlStr.Append("LotCode nvarchar(50),");
            DDlSqlStr.Append("InvDate DateTime,");
            DDlSqlStr.Append("InvQtyPrimary numeric(20,3),");
            DDlSqlStr.Append("InvMunitPrimary smallint,");
            DDlSqlStr.Append("InvQtySecondary numeric(20,5),");
            DDlSqlStr.Append("InvMunitSecondary smallint");
            DDlSqlStr.Append(")");
            rtrn = DBExecuteDDLSQLCmd(DDlSqlStr.ToString(), false);

            DBExecuteDDLSQLCmd("ALTER TABLE TInventory ADD CONSTRAINT PK_TInventory PRIMARY KEY  (InvID)", false);
            DBExecuteDDLSQLCmd("ALTER TABLE TInventory ADD CONSTRAINT DF_TInventory_InvDate] DEFAULT (getdate()) FOR InvDate", false);
            return rtrn;
        }

        #region "DB Trans"



        public long DBExecuteDDLSQLCmd(string SqlExCmd, bool CloseDBCon)
        {

            SqlCeCommand DBExSqlCommand;

            if (string.IsNullOrEmpty(SqlExCmd)) return -10;

            //connect first to database, ? check for better way
            try
            {
                if (db.SQLDBConnection.State != ConnectionState.Open)
                {
                    db.DBConnect();
                    if (db.SQLDBConnection.State == ConnectionState.Closed)
                        return -10; // conenction failed                
                }

                DBExSqlCommand = new SqlCeCommand(SqlExCmd, db.SQLDBConnection);
                DBExSqlCommand.CommandType = CommandType.Text;
                DBExSqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                db.SQLDBConnection.Close();
                return -1;
            }

            if (CloseDBCon && db.SQLDBConnection.State == ConnectionState.Open)
                db.SQLDBConnection.Close();

            return 1;

        }
        #endregion
    }
}

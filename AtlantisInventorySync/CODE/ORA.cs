using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OracleClient;
using System.Data;

namespace AtlantisInventorySync
{

    public class dbmappingobject
    {
        public string _dbname { get; set; }
        public string _dbtype { get; set; }
        public string _modelname { get; set; }
        public string _modeltype { get; set; }
    }

    public class datalistobject
    {
        public int nothing { get; set; }
    }

    public static class dbcon
    {
        public static OracleConnection SQLDBConnection { get; set; }

    }

    public static class erpdbcon
    {
        public static OracleConnection erpSQLDBConnection { get; set; }

    }



    public class oradb
    {



        
        #region connection
        public OracleDataReader oraDR;
        public OracleCommand oraCMD { get; set; }
        public byte[] bytearray { get; set; }
        protected string SQLConnectionString { get; set; }

        #endregion

        #region Properties

        public int sqlcode { get; private set; }
        public string SQLErrText { get; private set; }
        public string SQLStatement { get; private set; }
        public bool isConnected { get; private set; }

        #endregion

        #region constructors

        public oradb()
        {
        

            oraCMD = new OracleCommand();
            oraCMD.Connection = dbcon.SQLDBConnection;
        }


 

        public oradb(string constr)
        {
            SQLConnectionString = constr;
            oraCMD = new OracleCommand();
            oraCMD.Connection = dbcon.SQLDBConnection;
        }

        #endregion

        public void DBConnect()
        {
            try
            {
                if (dbcon.SQLDBConnection == null) dbcon.SQLDBConnection = new OracleConnection();

                if (dbcon.SQLDBConnection.State != ConnectionState.Open)
                {
                    dbcon.SQLDBConnection.ConnectionString = SQLConnectionString;
                    dbcon.SQLDBConnection.Open();
                }

                if (dbcon.SQLDBConnection.State == ConnectionState.Open)
                    isConnected = true;
                else
                    isConnected = false;
            }
            catch (Exception ex)
            {
                isConnected = false;
                sqlcode = -1;
                SQLErrText = "Conenction String :" + SQLConnectionString + ex.ToString();
            }
        }

        public void DBDisconnect()
        {
            if (dbcon.SQLDBConnection.State == ConnectionState.Open)
            {
                dbcon.SQLDBConnection.Close();
                isConnected = false;
            }
        }

        public byte[] resulsAsByte(string sqlSelector)
        {
            OracleDataReader dr = null;
            oraCMD = new OracleCommand();
            bytearray = null;
            oraCMD.CommandText = sqlSelector;

            if (dbcon.SQLDBConnection == null) dbcon.SQLDBConnection = new OracleConnection();


            if (dbcon.SQLDBConnection.State != ConnectionState.Open) DBConnect();


            oraCMD.Connection = dbcon.SQLDBConnection;

            try
            {

                //(byte[])reader.Items["Data"]
                dr = oraCMD.ExecuteReader();

                while (dr.Read())
                {
                    bytearray = (byte[])dr.GetOracleLob(0).Value;
                }

            }
            catch (Exception ex)
            {
                oraCMD.Dispose();

                SQLErrText = ex.ToString();
                dbcon.SQLDBConnection.Close();
            }
            oraCMD.Dispose();
            dr.Dispose();
            DBDisconnect();
            return bytearray;



        }


        public OracleDataReader resulsAsDataReader(string sqlSelector)
        {
            OracleDataReader dr = null;

            OracleCommand SQLSelectCmd = new OracleCommand();

            SQLSelectCmd.CommandText = sqlSelector;

            if (dbcon.SQLDBConnection == null) dbcon.SQLDBConnection = new OracleConnection();


            if (dbcon.SQLDBConnection.State != ConnectionState.Open) DBConnect();


            SQLSelectCmd.Connection = dbcon.SQLDBConnection;

            try
            {
                dr = SQLSelectCmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                SQLSelectCmd.Dispose();

                SQLErrText = ex.ToString();
                dbcon.SQLDBConnection.Close();
            }
            return dr;

        }



        public long resultAsLong(string sqlSelector)
        {

            OracleDataReader SQLReaderRetSelCmdResult = null;


            long CmdResultNum = 0;
            OracleCommand SQLSelectCmd = new OracleCommand();

            SQLSelectCmd.CommandText = sqlSelector;

            DBConnect();


            SQLSelectCmd.Connection = dbcon.SQLDBConnection;

            try
            {
                SQLReaderRetSelCmdResult = SQLSelectCmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                SQLSelectCmd.Dispose();
                SQLReaderRetSelCmdResult.Dispose();
                SQLErrText = ex.ToString();
                dbcon.SQLDBConnection.Close();
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
            dbcon.SQLDBConnection.Close();


            return CmdResultNum;
        }

        public long resultAsLongBatch(string sqlSelector)
        {

            OracleDataReader SQLReaderRetSelCmdResult = null;


            long CmdResultNum = 0;
            OracleCommand SQLSelectCmd = new OracleCommand();

            SQLSelectCmd.CommandText = sqlSelector;

            DBConnect();


            SQLSelectCmd.Connection = dbcon.SQLDBConnection;

            try
            {
                SQLReaderRetSelCmdResult = SQLSelectCmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                SQLSelectCmd.Dispose();
                SQLReaderRetSelCmdResult.Dispose();
                SQLErrText = ex.ToString();
                dbcon.SQLDBConnection.Close();
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
        }

        public decimal resultAsDecimal(string sqlSelector)
        {

            OracleDataReader SQLReaderRetSelCmdResult = null;


            decimal CmdResultNum = 0;
            OracleCommand SQLSelectCmd = new OracleCommand();

            SQLSelectCmd.CommandText = sqlSelector;

            DBConnect();


            SQLSelectCmd.Connection = dbcon.SQLDBConnection;

            try
            {
                SQLReaderRetSelCmdResult = SQLSelectCmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                SQLSelectCmd.Dispose();
                SQLReaderRetSelCmdResult.Dispose();
                SQLErrText = ex.ToString();
                dbcon.SQLDBConnection.Close();
                return -1;
            }


            while (SQLReaderRetSelCmdResult.Read())
            {
                try { CmdResultNum = decimal.Parse(SQLReaderRetSelCmdResult.GetValue(0).ToString()); }
                catch { CmdResultNum = -1; }
            }

            SQLSelectCmd.Dispose();
            SQLReaderRetSelCmdResult.Close();
            SQLReaderRetSelCmdResult.Dispose();
            dbcon.SQLDBConnection.Close();


            return CmdResultNum;
        }

        public string resultAsString(string sqlSelector)
        {
            OracleDataReader SQLReaderRetSelCmdResult;

            String SQLRtrnRowValue;

            string CmdResultNum = "";

            OracleCommand SQLSelectCmd = new OracleCommand();

            SQLSelectCmd.CommandText = sqlSelector;

            DBConnect();

            SQLSelectCmd.Connection = dbcon.SQLDBConnection;
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
                SQLStatement = sqlSelector;
            }
            SQLReaderRetSelCmdResult.Close();

            dbcon.SQLDBConnection.Close();

            SQLRtrnRowValue = CmdResultNum;

            if (SQLRtrnRowValue.Length > 0)
            {
                return SQLRtrnRowValue;
            }
            else
            {
                return null;
            }

        }

        public long rowsCount(string sqlselector)
        {
            if (dbcon.SQLDBConnection.State != ConnectionState.Open)
                DBConnect();

            if (dbcon.SQLDBConnection.State != ConnectionState.Open)
                return -1;

            string sqlcmd = "SELECT COUNT(*) FROM (" + sqlselector + ") INNERTABLE";

            return resultAsLong(sqlcmd);
        }

        public long oraSqlCommand(String SqlExCmd)
        {
            long DBAffctRows;

            OracleCommand DBExSqlCommand;

            if (SqlExCmd.Length == 0 || SqlExCmd == "") { return -10; }

            if (dbcon.SQLDBConnection == null) DBConnect();
            try
            {
                DBConnect();

                DBExSqlCommand = new OracleCommand(SqlExCmd, dbcon.SQLDBConnection);

                DBAffctRows = DBExSqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                dbcon.SQLDBConnection.Close();
                SQLErrText = ex.ToString();
                SQLStatement = SqlExCmd;
                return -1;
            }


            if (DBAffctRows > 0)
            {
                dbcon.SQLDBConnection.Close();
                return DBAffctRows;
            }
            else
            {
                dbcon.SQLDBConnection.Close();
                return -1;
            }
        }


        public string oraSqlCommandBatchwEX(String SqlExCmd) //not closing connection
        {
            long DBAffctRows;

            OracleCommand DBExSqlCommand;

            if (SqlExCmd.Length == 0 || SqlExCmd == "") { return "-1"; }

            try
            {
                DBConnect();

                DBExSqlCommand = new OracleCommand(SqlExCmd, dbcon.SQLDBConnection);
                DBAffctRows = DBExSqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                dbcon.SQLDBConnection.Close();
                SQLErrText = ex.ToString();
                SQLStatement = SqlExCmd;
                return SqlExCmd + " |  |" + ex.ToString();
            }


            if (DBAffctRows > 0)
            {
                return "1";
            }
            else
            {
                dbcon.SQLDBConnection.Close();
                return "-1";
            }
        }

        public long oraSqlCommandBatch(String SqlExCmd) //not closing connection
        {
            long DBAffctRows;

            OracleCommand DBExSqlCommand;

            if (SqlExCmd.Length == 0 || SqlExCmd == "") { return -10; }

            try
            {
                DBConnect();

                DBExSqlCommand = new OracleCommand(SqlExCmd, dbcon.SQLDBConnection);
                DBAffctRows = DBExSqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                dbcon.SQLDBConnection.Close();
                SQLErrText = ex.ToString();
                SQLStatement = SqlExCmd;
                return -1;
            }


            if (DBAffctRows > 0)
            {
                return DBAffctRows;
            }
            else
            {
                dbcon.SQLDBConnection.Close();
                return -1;
            }
        }



    }




}

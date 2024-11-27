Imports System.Web.Services
Imports System.Configuration
Imports System.Data.OracleClient

Public Class WmsDBTrans
    Protected OracleWmsConnection As OracleConnection
    Public Function DBConnectoWms()

        OracleWmsConnection = New OracleConnection

        OracleWmsConnection.ConnectionString = ConfigurationSettings.AppSettings("OracleConnectionString")
        OracleWmsConnection.Open()

    End Function
    Public Function DBWmsCheckConnection() As Integer

        Dim gtdbconrtrnstr As String

        gtdbconrtrnstr = OraDBWmsExSelectCmdRN2String("SELECT COUNT(*) FROM DUAL")

        If gtdbconrtrnstr = "Null" Then
            Return 0
        ElseIf Len(gtdbconrtrnstr) > 0 Then
            Return Integer.Parse(gtdbconrtrnstr)
        End If

    End Function
    Public Function FGetDBDate() As String

        Dim sqlstr As String
        Dim MyDBDate As String
        sqlstr = "SELECT TO_CHAR(SYSDATE,'DD/MM/YYYY') AS MYDBDATE FROM DUAL"

        MyDBDate = OraDBWmsExSelectCmdRStr2Str(sqlstr)

        Return MyDBDate
    End Function
    Public Function OraDBWmsExSelectCmdRN2String(ByVal SqlCmd As String) As String


        Dim OraSelectCmd As OracleCommand

        Dim OraReaderRetSelCmdResult As OracleDataReader

        Dim OraRtrnRowValue As String

        Dim CmdResultNum As OracleNumber
        OraSelectCmd = New OracleCommand

        OraSelectCmd.CommandText = SqlCmd

        DBConnectoWms()

        OraSelectCmd.Connection = OracleWmsConnection

        Try
            OraReaderRetSelCmdResult = OraSelectCmd.ExecuteReader()
            While OraReaderRetSelCmdResult.Read()
                CmdResultNum = OraReaderRetSelCmdResult.GetOracleValue(0)
            End While
            OraReaderRetSelCmdResult.Close()
        Catch ex As Exception
        End Try


        OracleWmsConnection.Close()


        OraRtrnRowValue = CmdResultNum.ToString

        If Len(OraRtrnRowValue) > 0 Then
            Return OraRtrnRowValue
        Else
            Return "Null"
        End If

    End Function
    Public Function OraDBWmsExSelectCmdRStr2Str(ByVal SqlCmd As String) As String


        Dim OraSelectCmd As OracleCommand

        Dim OraReaderRetSelCmdResult As OracleDataReader

        Dim OraRtrnRowValue As String



        OraSelectCmd = New OracleCommand

        OraSelectCmd.CommandText = SqlCmd

        DBConnectoWms()

        OraSelectCmd.Connection = OracleWmsConnection

        Try
            OraReaderRetSelCmdResult = OraSelectCmd.ExecuteReader()

            While OraReaderRetSelCmdResult.Read()
                OraRtrnRowValue = OraReaderRetSelCmdResult.GetOracleString(0).ToString
            End While
            OraReaderRetSelCmdResult.Close()
        Catch ex As Exception
        End Try
        
        OracleWmsConnection.Close()

        If Len(OraRtrnRowValue) > 0 Then
            Return OraRtrnRowValue
        Else
            Return "Null"
        End If

    End Function
    '//////////////////////////////////////////////////////////////////////
    'OraDBExecuteSclCmd(Sqlstring,commit {0,1}) :: Executes a DB Command //
    'rtrn values -10 : Empty Sqlcmd string
    '//////////////////////////////////////////////////////////////////////
    Public Function OraDBRollbackSQLCmd() As Long

        Dim OraDBAffctRows As Long


        Dim OraDBCommitCommand As OracleCommand


        'connect first to database, ? check for better way
        Try


            DBConnectoWms()

            OraDBCommitCommand = New OracleCommand("ROLLBACK", OracleWmsConnection)
            OraDBCommitCommand.CommandType = CommandType.Text
            OraDBCommitCommand.ExecuteNonQuery()
            OracleWmsConnection.Close()
            Return 1
        Catch ex As Exception

            OracleWmsConnection.Close()
            Return -1
        End Try

        Return 0
    End Function
    Public Function OraDBCommitSQLCmd() As Long

        Dim OraDBAffctRows As Long


        Dim OraDBCommitCommand As OracleCommand


        'connect first to database, ? check for better way
        Try


            DBConnectoWms()

            OraDBCommitCommand = New OracleCommand("COMMIT", OracleWmsConnection)
            OraDBCommitCommand.CommandType = CommandType.Text
            OraDBCommitCommand.ExecuteNonQuery()
            OracleWmsConnection.Close()
            Return 1
        Catch ex As Exception

            OracleWmsConnection.Close()
            Return -1
        End Try

        Return 0
    End Function
    Public Function OraDBExecuteSQLCmd(ByVal SqlExCmd As String, ByVal DBExCmdCommit As Integer) As Long

        Dim OraDBAffctRows As Long

        Dim OraDBExSqlCommand As OracleCommand
        Dim OraDBCommitCommand As OracleCommand


        If Len(SqlExCmd) = 0 Or SqlExCmd = "" Then Return -10

        'connect first to database, ? check for better way
        Try


            DBConnectoWms()

            OraDBExSqlCommand = New OracleCommand(SqlExCmd, OracleWmsConnection)

            OraDBExSqlCommand.CommandType = CommandType.Text
            OraDBAffctRows = OraDBExSqlCommand.ExecuteNonQuery()


        Catch ex As Exception

            f_sqlerrorlog("1", "OraDBExecuteSQLCmd", ex.ToString, "MOBDEV")

            OracleWmsConnection.Close()
            Return -1
        End Try


        If OraDBAffctRows > 0 Then
            If DBExCmdCommit = 1 Then
                OraDBCommitCommand = New OracleCommand("COMMIT", OracleWmsConnection)
                OraDBCommitCommand.CommandType = CommandType.Text
                OraDBCommitCommand.ExecuteNonQuery()
            End If

            OracleWmsConnection.Close()
            Return OraDBAffctRows
        Else
            OracleWmsConnection.Close()
            Return -1
        End If
        Return 0
    End Function
    Public Function OraDBFillDataset(ByVal SelectSqlStr As String, ByVal FillTbl As String) As DataSet

        ' Dim OraDbDT As DataTable = New DataTable
        Dim OraDbDS As DataSet = New DataSet


        DBConnectoWms()

        Dim OraDA As OracleDataAdapter = New OracleDataAdapter(SelectSqlStr, OracleWmsConnection)

        ' Dim OraCmd As OracleCommand = New OracleCommand

        Dim OraCmdB As OracleCommandBuilder = New OracleCommandBuilder(OraDA)

        Try
            OraDA.MissingSchemaAction = MissingSchemaAction.AddWithKey
            OraDA.Fill(OraDbDS, FillTbl)
        Catch ex As Exception
        End Try       

        OracleWmsConnection.Close()

        OraDBFillDataset = OraDbDS

    End Function
    Public Function f_sqlerrorlog(ByVal CompId As Integer, ByVal SrcCodeSnippet As String, ByVal SqlErrText As String, ByVal AppUserName As String) As Long

        Dim sqlLOGstr As String
        Dim NewLogId As String
        Dim AppUserStr As String
        Dim CompIdStr As String
        Dim LogSqlErrText As String


        If CompId > 0 Then
            CompIdStr = CompId.ToString
        Else
            CompIdStr = "1"
        End If

        If Len(AppUserName) > 0 Then

            AppUserStr = AppUserName
        Else
            AppUserStr = "NULL"
        End If

        If Len(SrcCodeSnippet) > 0 Then
            LogSqlErrText = SrcCodeSnippet & ">>"
        Else
            LogSqlErrText = ">>"
        End If
        If Len(SqlErrText) > 0 Then
            LogSqlErrText = LogSqlErrText & SqlErrText.Replace("'", "|")

        End If

        NewLogId = OraDBWmsExSelectCmdRN2String("SELECT SQ_LOGID.NEXTVAL FROM TDUAL")

        sqlLOGstr = "INSERT INTO TSYSEVENTLOGS(LOGID,COMPID,LOGDATE,DBERRORTEXT,APPUSER)  "
        sqlLOGstr = sqlLOGstr & "SELECT " & NewLogId + "," & CompIdStr & ",SYSTIMESTAMP,'" & LogSqlErrText + "',"
        sqlLOGstr = sqlLOGstr & "'" & AppUserStr & "' FROM TDUAL "

        Dim OraDBAffctRows As Long

        Dim OraDBExSqlCommand As OracleCommand
        Dim OraDBCommitCommand As OracleCommand


        If Len(sqlLOGstr) = 0 Or sqlLOGstr = "" Then Return -10

        'connect first to database, ? check for better way
        Try


            DBConnectoWms()

            OraDBExSqlCommand = New OracleCommand(sqlLOGstr, OracleWmsConnection)

            OraDBExSqlCommand.CommandType = CommandType.Text
            OraDBAffctRows = OraDBExSqlCommand.ExecuteNonQuery()
            OracleWmsConnection.Close()
        Catch ex As Exception
            OracleWmsConnection.Close()
            Return -1
        End Try


        
        Return OraDBAffctRows
     
        Return 0

    End Function
End Class

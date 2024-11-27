Imports System.Web.Services
Imports System.Configuration
Imports System.Data.OracleClient

<System.Web.Services.WebService(Namespace:="http://tempuri.org/SOAMobWmsProvider/WebMobWMService")> _
Public Class WebMobWMService
    Inherits System.Web.Services.WebService

#Region " Web Services Designer Generated Code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Web Services Designer.
        InitializeComponent()

        'Add your own initialization code after the InitializeComponent() call

    End Sub

    'Required by the Web Services Designer
    Private components As System.ComponentModel.IContainer
    Protected OracleWmsConnection As OracleConnection
    Protected MyWmsDBTrans As WmsDBTrans





    'NOTE: The following procedure is required by the Web Services Designer
    'It can be modified using the Web Services Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        components = New System.ComponentModel.Container
        MyWmsDBTrans = New WmsDBTrans
    End Sub

    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        'CODEGEN: This procedure is required by the Web Services Designer
        'Do not modify it using the code editor.
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

#End Region

#Region "General"

    Private Function DBConnectoWms()

        OracleWmsConnection = New OracleConnection

        OracleWmsConnection.ConnectionString = ConfigurationSettings.AppSettings("OracleConnectionString")
        OracleWmsConnection.Open()

    End Function
    <WebMethod()> _
        Public Function SOA_OraDbCheckConService() As String


        Dim ConTryRtrn As Integer


        ConTryRtrn = MyWmsDBTrans.DBWmsCheckConnection

        If ConTryRtrn > 0 Then
            Return "1"
        Else
            Return "-1"
        End If
    End Function
    <WebMethod()> _
       Public Function SOA_InsertTest(ByVal TERMINAL As String) As String

        Dim MyWmsDBtrans As New WmsDBTrans
        Dim ConTryRtrn As Long
        Dim sqlstr As String

        Dim ID As Long
        Dim Idstr As String

        Idstr = MyWmsDBtrans.OraDBWmsExSelectCmdRN2String("SELECT SEQ_TSYSEVENTLOGS.NEXTVAL FROM DUAL")

        ID = Long.Parse(Idstr)



        sqlstr = "INSERT INTO TTEST VALUES (" & ID.ToString & ",'" & TERMINAL & "')"

        ConTryRtrn = MyWmsDBtrans.OraDBExecuteSQLCmd(sqlstr, 0)

        If ConTryRtrn > 0 Then
            Return "1"
        Else
            Return "-1"
        End If
    End Function
    <WebMethod()> _
     Public Function SOA_FgetItemDrawingImagePath(ByVal ItemID As Long) As String

        Dim SqlStr As String

        Dim Rtrn As String

        Dim ImgFolder, ImgPath As String

        Dim MyWMSDBTrans As New WmsDBTrans

        SqlStr = "SELECT HTTPITEMDRAWINGFOLDER FROM TITEMPARAMS "

        ImgFolder = MyWMSDBTrans.OraDBWmsExSelectCmdRStr2Str(SqlStr)


        If Len(ImgFolder) > 0 And ImgFolder <> "Null" And ItemID > 0 Then
            SqlStr = "SELECT IMGPATHTHUMB FROM TITEMDRAWINGS WHERE ITEMID=" & ItemID.ToString

            Rtrn = MyWMSDBTrans.OraDBWmsExSelectCmdRStr2Str(SqlStr)

            If Len(Rtrn) = 0 Or Rtrn = "Null" Then
                SqlStr = "SELECT DOCFILENAME FROM TITEMDRAWINGS WHERE ITEMID=" & ItemID.ToString
                Rtrn = MyWMSDBTrans.OraDBWmsExSelectCmdRStr2Str(SqlStr)

                If Right(ImgFolder, 1) = "/" Then
                    ImgPath = ImgFolder & Rtrn
                Else
                    ImgPath = ImgFolder & "/" & Rtrn
                End If

            Else
                If Right(ImgFolder, 1) = "/" Then
                    ImgPath = ImgFolder & "Thumbs/" & Rtrn
                Else
                    ImgPath = ImgFolder & "/" & "Thumbs/" & Rtrn
                End If
            End If

           

        End If


        Return ImgPath

    End Function
    <WebMethod()> _
       Public Function SOA_TestCommit(ByVal CommmitOrRollback As Integer) As String

        Dim MyWmsDBtrans As New WmsDBTrans
        Dim ConTryRtrn As Long
        Dim sqlstr As String

        Dim ID As Long
        sqlstr = "COMMIT"

        If CommmitOrRollback = 1 Then
            ConTryRtrn = MyWmsDBtrans.OraDBCommitSQLCmd()
        Else
            ConTryRtrn = MyWmsDBtrans.OraDBRollbackSQLCmd()

        End If
        If ConTryRtrn > 0 Then
            Return "1"
        Else
            Return "-1"
        End If
    End Function
    <WebMethod()> _
    Public Function SOA_FCheckUsername(ByVal Username As String, ByVal CompId As Integer) As DataSet
        Dim ThisCompID As String
        Dim ThisUsername As String
        Dim SqlStr As String


        If CompId > 0 Then
            ThisCompID = CompId.ToString()
        Else
            ThisCompID = "1"
        End If

        If Len(Username) > 0 Then
            ThisUsername = Username
        Else
            ThisUsername = " "
        End If

        SqlStr = " SELECT USERID,NVL(USERFIRSTNAME,' ') || NVL(USERLASTNAME,' ') AS USERFULLNAME FROM TSYSUSERS WHERE USERNAME='" & ThisUsername & "' AND COMPID=" & ThisCompID

        SOA_FCheckUsername = MyWmsDBTrans.OraDBFillDataset(SqlStr, "USERINFO")
    End Function
#End Region
    '///////////////////////////////////////////
    '///   COMMON functions ////////////////////
    '///////////////////////////////////////////
#Region "Common"
    '////
    <WebMethod()> _
   Public Function FMakePageNumbering(ByVal RangeBefAfter As Integer, ByVal FTotalRecords As Long, ByVal FPageSize As Integer, ByVal FCurrentPage As Integer) As String
        Dim TotalPages As Integer
        Dim DivResult As Integer
        Dim i As Integer
        Dim ThisCrntPage As Integer

        Dim PageNumbering As String = Nothing
        Dim MaxShowCount As Integer
        Dim MinShowCount As Integer
        'default
        'MaxShowNumbers = 3

        'IF FtotalRecords
        If FPageSize = 0 Or FPageSize = Nothing Or FTotalRecords = 0 Or FTotalRecords = Nothing Then
            PageNumbering = "1"
            Return PageNumbering
        End If

        If FCurrentPage > 0 Then
            ThisCrntPage = FCurrentPage
        Else
            ThisCrntPage = 1
        End If

        DivResult = FTotalRecords Mod FPageSize

        If DivResult > 0 Then
            TotalPages = Fix(FTotalRecords / FPageSize) + 1
        Else
            TotalPages = Fix(FTotalRecords / FPageSize)
        End If

        If ThisCrntPage > TotalPages Then
            ThisCrntPage = TotalPages 'In Case someone gives higher currentpage than possible
        End If

        If TotalPages < 2 Then
            PageNumbering = "1"
            Return PageNumbering
        End If
        '//////
        If FCurrentPage <= TotalPages Then
            'right numbering
            If ThisCrntPage + RangeBefAfter <= TotalPages Then
                MaxShowCount = ThisCrntPage + RangeBefAfter
            Else
                MaxShowCount = TotalPages
            End If
            'left numbering
            If ThisCrntPage > RangeBefAfter Then
                MinShowCount = ThisCrntPage - RangeBefAfter
            ElseIf ThisCrntPage = 1 Then
                MinShowCount = 1
            ElseIf RangeBefAfter > ThisCrntPage Then
                MinShowCount = RangeBefAfter - ThisCrntPage
            Else
                MinShowCount = 1
            End If
        End If

        If TotalPages > 1 Then

            For i = MinShowCount To MaxShowCount
                PageNumbering = PageNumbering & CStr(i)
                If i < MaxShowCount Then
                    PageNumbering = PageNumbering & ","
                End If

            Next

        End If

        Return PageNumbering
    End Function
    '////
    <WebMethod()> _
    Public Function SOA_FGetWhBuildings(ByVal CompID As Integer, ByVal BranchID As Integer) As DataSet


        Dim MyWmsDBTrans As New WmsDBTrans
        Dim MyWmsBinTrans As New WmsBinTrans


        Dim Ds As New DataSet
        Dim sqlstr As String

        sqlstr = "SELECT WHBUILDID,WHBUILDING   FROM  TWMSWHBUILDINGS "

        If CompID > 0 Then
            sqlstr = sqlstr & " WHERE COMPID=" & CompID.ToString()
            If BranchID > 0 Then
                sqlstr = sqlstr & " AND BRANCHID=" & BranchID.ToString()
            End If
        End If
        sqlstr = sqlstr & " ORDER BY WHBUILDID"

        Ds = MyWmsDBTrans.OraDBFillDataset(sqlstr, "ITEMSTATUS")

        Return Ds


    End Function
    <WebMethod()> _
    Public Function SOA_FgetItemStoreStatus(ByVal CompID As Integer, ByVal InItemCode As String) As DataSet


        Dim MyWmsDBTrans As New WmsDBTrans
        Dim MyWmsBinTrans As New WmsBinTrans


        Dim Ds As New DataSet
        Dim sqlstr As String

        sqlstr = "SELECT WHBINCODE AS ÈÅÓÇ,ITEMCODE AS ÐÑÏÖÉË,ITEMLENGTH AS ÌÇÊÏÓ,BINITEMQTYPRIMARY AS ÂÅÑÃÅÓ,COLORCODE AS ×ÑÙÌÁ,COLORDESC,ITEMID  FROM  VWMSBINSTATUS "
        sqlstr = sqlstr & " WHERE ITEMCODE='" & InItemCode & "' AND BINITEMQTYPRIMARY > 0"


        If CompID > 0 Then
            sqlstr = sqlstr & " AND COMPID=" & CompID.ToString()
        End If

        Ds = MyWmsDBTrans.OraDBFillDataset(sqlstr, "ITEMSTATUS")

        Return Ds


    End Function
    <WebMethod()> _
    Public Function SOA_FgetItemStoreStatusByDetails(ByVal CompID As Integer, ByVal BranchID As Integer, ByVal ItemID As Long, ByVal ItemLength As Decimal, ByVal ItemColorID As Integer, ByVal ItemColorID2 As Integer) As DataSet


        Dim MyWmsDBTrans As New WmsDBTrans
        Dim MyWmsBinTrans As New WmsBinTrans


        Dim Ds As New DataSet

        Dim sqlstr As String
        Dim ItemLengthStr As String

        sqlstr = "SELECT WHBINID,WHBINCODE ,ITEMID,ITEMCODE,ITEMLENGTH,BINITEMQTYPRIMARY ,ITEMCOLORID,COLORCODE,COLORDESC,ITEMCOLORID2,COLORCODE2,COLORDESC2  FROM  VWMSBINSTATUS "
        sqlstr = sqlstr & " WHERE "

        If CompID > 0 Then
            sqlstr = sqlstr & "  COMPID=" & CompID.ToString()
            If BranchID > 0 Then
                sqlstr = sqlstr & "  AND BRANCHID=" & BranchID.ToString()
            End If
        End If


        If ItemID > 0 Then
            If CompID > 0 Then
                sqlstr = sqlstr & " AND "
            End If
            sqlstr = sqlstr & " ITEMID=" & ItemID.ToString()
        End If

        If ItemLength > 0 Then
            ItemLengthStr = MyWmsBinTrans.f_dbinsertdecimal(ItemLength, 4)
            sqlstr = sqlstr & " AND ITEMLENGTH=" & ItemLengthStr
        End If

        If ItemColorID > 0 Then
            sqlstr = sqlstr & " AND ITEMCOLORID=" & ItemColorID.ToString()
        End If
        If ItemColorID2 > 0 Then
            sqlstr = sqlstr & " AND ITEMCOLORID2=" & ItemColorID2.ToString()
        End If

        sqlstr = sqlstr & " AND BINITEMQTYPRIMARY > 0 "

        Ds = MyWmsDBTrans.OraDBFillDataset(sqlstr, "ITEMSTATUS")

        Return Ds

    End Function
    <WebMethod()> _
    Public Function SOA_FGetItemIDByCode(ByVal InItemCode As String, ByVal CompID As Integer) As String

        Dim sqlstr As String
        Dim ExItemIDStr As String

        sqlstr = "SELECT ITEMID FROM TITEMS WHERE ITEMCODE='" & InItemCode & "' AND COMPID=" & CompID.ToString
        Try
            ExItemIDStr = MyWmsDBTrans.OraDBWmsExSelectCmdRN2String(sqlstr)
        Catch ex As Exception
            Return "-1"
        End Try

        If Len(ExItemIDStr) > 0 And ExItemIDStr <> "Null" Then
            Return ExItemIDStr
        Else
            Return "0"
        End If
    End Function
    <WebMethod()> _
    Public Function SOA_FGetPackItemNoInfo(ByVal CompID As Integer, ByVal PackItemNo As String) As DataSet
        'OraDBFillDataset()
        Dim Ds As DataSet
        Dim SqlStr As String

        SqlStr = "SELECT PACKITEMID,PACKITEMNO,ITEMCODE,ITEMDESC,ITEMID,ITEMLENGTH,ITEMCOLORID,ITEMCOLORID2,ORDERID,ORDERDTLID,"
        SqlStr = SqlStr & "COLORCODE,COLORDESC, COLORCODE2, COLORDESC2, PACKITEMBARS , PACKITEMWEIGHT,BINCODE,"
        SqlStr = SqlStr & "INITPACKITEMBARS,INITPACKITEMWEIGHT FROM VPACKAGEINFO2 WHERE PACKITEMNO = '" & PackItemNo & "'"

        Ds = MyWmsDBTrans.OraDBFillDataset(SqlStr, "PACKITEMINFO")

        Return Ds

    End Function
    <WebMethod()> _
    Public Function SOA_FGetPackItemNoInfoV2(ByVal CompID As Integer, ByVal PackItemNo As String) As DataSet
        'OraDBFillDataset()
        Dim Ds As DataSet
        Dim SqlStr As String

        SqlStr = "SELECT PACKITEMID,PACKITEMNO,ITEMCODE,ITEMDESC,ITEMID,ITEMLENGTH,ITEMCOLORID,ITEMCOLORID2,"
        SqlStr = SqlStr & "COLORCODE,COLORDESC, COLORCODE2, COLORDESC2, PACKITEMBARS, PACKITEMWEIGHT,BINCODE,"
        SqlStr = SqlStr & "INITPACKITEMBARS FROM VPACKAGEINFO2 WHERE PACKITEMNO = '" & PackItemNo & "'"

        Ds = MyWmsDBTrans.OraDBFillDataset(SqlStr, "PACKITEMINFO")

        Return Ds

    End Function
    <WebMethod()> _
    Public Function SOA_FGetBinPaletteInfo(ByVal CompID As Integer, ByVal BranchID As Long, ByVal WhBinCode As String, ByVal PackItemNo As String) As DataSet
        'OraDBFillDataset()
        Dim Ds As DataSet
        Dim SqlStr As String

        SqlStr = "SELECT WHBINCODE,WHBINCODE AS BINCODE,PACKITEMID,PACKITEMNO,ITEMCODE,ITEMDESC,ITEMID,ITEMLENGTH,ITEMCOLORID,ITEMCOLORID2,"
        SqlStr = SqlStr & "COLORCODE,COLORDESC, COLORCODE2, COLORDESC2,BINITEMQTYPRIMARY,BINITEMQTYPRIMARY AS  PACKITEMBARS, BINITEMQTYSECONDARY AS PACKITEMWEIGHT,"
        SqlStr = SqlStr & "0 AS INITPACKITEMBARS FROM VWMSBINSTATUS WHERE WHBINCODE = '" & WhBinCode & "'"

        If CompID > 0 Then
            SqlStr = SqlStr & " AND COMPID=" & CompID.ToString
        End If
        If BranchID > 0 Then
            SqlStr = SqlStr & " AND BRANCHID=" & BranchID.ToString
        End If

        If Len(PackItemNo) > 0 Then
            SqlStr = SqlStr & " AND PACKITEMNO='" & PackItemNo & "'"
        End If

        Ds = MyWmsDBTrans.OraDBFillDataset(SqlStr, "PACKITEMINFO")

        Return Ds

    End Function
    <WebMethod()> _
    Public Function SOA_FGetPaletteItemCode(ByVal PackItemNo As String) As String
        'OraDBFillDataset()
        Dim Ds As DataSet
        Dim SqlStr As String
        Dim ItemCode As String


        SqlStr = " SELECT TITEMS.ITEMCODE,MAX(TPACKAGESDTL.ITEMID) AS MAXITEMID FROM "
        SqlStr = SqlStr & "TITEMS, TPACKAGESDTL, TPACKAGES "
        SqlStr = SqlStr & "WHERE TPACKAGES.PACKITEMNO='" & PackItemNo & "' AND "
        SqlStr = SqlStr & "TPACKAGESDTL.PACKITEMID=TPACKAGES.PACKITEMID AND "

        SqlStr = SqlStr & "TPACKAGESDTL.ITEMID = TITEMS.ITEMID "
        SqlStr = SqlStr & "GROUP BY TITEMS.ITEMCODE "

        ItemCode = MyWmsDBTrans.OraDBWmsExSelectCmdRStr2Str(SqlStr)

        Return ItemCode

    End Function
    <WebMethod()> _
    Public Function SOA_FGetPaletteIDByCode(ByVal CompID As Integer, ByVal BranchID As Integer, ByVal PackItemNo As String) As String

        Dim SqlStr As String
        Dim Packitemid As String


        SqlStr = " SELECT PACKITEMID FROM TPACKAGES WHERE  COMPID=" & CompID.ToString
        If BranchID > 0 Then
            SqlStr = SqlStr & " AND BRANCHID=" & BranchID.ToString
        End If
        SqlStr = SqlStr & " AND PACKITEMNO='" & PackItemNo & "'"


        Packitemid = MyWmsDBTrans.OraDBWmsExSelectCmdRN2String(SqlStr)


        Return Packitemid

    End Function
    <WebMethod()> _
    Public Function SOA_FGetPutWarehouses(ByVal CompID As Integer, ByVal BranchID As Integer) As DataSet

        Dim SqlStr As String
        Dim WhBinCode As String

        Dim Ds As New DataSet

        SqlStr = " SELECT WHID,WHCODE,WHDESCRIPTION FROM TWMSWAREHOUSES "
        SqlStr = SqlStr & " WHERE WHID IN (SELECT WHID FROM TWMSZONES WHERE BINTYPE=3)"

        If CompID > 0 Then
            SqlStr = SqlStr & " AND COMPID=" & CompID.ToString
        End If
        If BranchID > 0 Then
            SqlStr = SqlStr & " AND BRANCHID=" & BranchID.ToString
        End If
        SqlStr = SqlStr & " ORDER BY WHID"

        Ds = MyWmsDBTrans.OraDBFillDataset(SqlStr, "WAREHOUSES")

        Return Ds

    End Function
    <WebMethod()> _
    Public Function SOA_FGetFreePutBinCodes(ByVal CompID As Integer, ByVal BranchID As Integer, ByVal WhCode As String) As DataSet

        Dim SqlStr As String
        Dim WhBinCode As String

        Dim DsBinCodes As New DataSet

        SqlStr = "SELECT DISTINCT VWMSBINSTATUS.WHBINCODE  FROM VWMSBINSTATUS WHERE (BINITEMQTYPRIMARY IS NULL "
        SqlStr = SqlStr & " OR BINITEMQTYPRIMARY= 0 ) AND WHBINTYPE=3 "

        If CompID > 0 Then
            SqlStr = SqlStr & "AND VWMSBINSTATUS.COMPID=" & CompID.ToString
        End If
        If BranchID > 0 Then
            SqlStr = SqlStr & "AND VWMSBINSTATUS.BRANCHID=" & BranchID.ToString
        End If
        If Len(WhCode) > 0 Then
            SqlStr = SqlStr & " AND VWMSBINSTATUS.WHCODE='" & WhCode & "'"
        End If
        SqlStr = SqlStr & " ORDER BY WHBINCODE "
        DsBinCodes = MyWmsDBTrans.OraDBFillDataset(SqlStr, "WHBINCODESFREE")

        Return DsBinCodes

    End Function
    <WebMethod()> _
    Public Function SOA_FPutSuggestBinCode(ByVal CompID As Integer, ByVal BranchID As Integer, ByVal WhCode As String) As String

        Dim SqlStr As String
        Dim WhBinCode As String

        SqlStr = "SELECT MIN(VWMSBINSTATUS.WHBINCODE) AS FIRSTBINCODE FROM VWMSBINSTATUS WHERE (BINITEMQTYPRIMARY IS NULL "
        SqlStr = SqlStr & " OR BINITEMQTYPRIMARY= 0 ) "

        If CompID > 0 Then
            SqlStr = SqlStr & "AND VWMSBINSTATUS.COMPID=" & CompID.ToString
        End If
        If BranchID > 0 Then
            SqlStr = SqlStr & "AND VWMSBINSTATUS.BRANCHID=" & BranchID.ToString
        End If
        If Len(WhCode) > 0 Then
            SqlStr = SqlStr & " AND VWMSBINSTATUS.WHCODE='" & WhCode & "'"
        End If

        WhBinCode = MyWmsDBTrans.OraDBWmsExSelectCmdRStr2Str(SqlStr)
        Return WhBinCode

    End Function
    <WebMethod()> _
    Public Function SOA_FTransSuggestBinCode(ByVal CompID As Integer, ByVal BranchID As Integer, ByVal CardID As Long) As String

        Dim SqlStr As String
        Dim TransBinCode As String

        SqlStr = "SELECT MIN(TWMSBINS.WHBINCODE) AS FIRSTBINCODE FROM TWMSBINS WHERE TWMSBINS.WHBINTYPE = 5 "
        SqlStr = SqlStr & " AND TWMSBINS.WHBINCODE NOT IN (SELECT DISTINCT TRANSBINCODE FROM TWMSPICKINGCARD WHERE TRANSBINCODE IS NOT NULL) "

        If CompID > 0 Then
            SqlStr = SqlStr & " AND TWMSBINS.COMPID=" & CompID.ToString
        End If
        If BranchID > 0 Then
            SqlStr = SqlStr & " AND TWMSBINS.BRANCHID=" & BranchID.ToString
        End If

        TransBinCode = MyWmsDBTrans.OraDBWmsExSelectCmdRStr2Str(SqlStr)

        If Len(TransBinCode) > 0 And (TransBinCode <> "NULL" Or TransBinCode <> "Null") And CardID > 0 Then
            SqlStr = "UPDATE TWMSPICKINGCARD SET TRANSBINCODE='" & TransBinCode & "'"
            SqlStr = SqlStr & " WHERE CARDID=" & CardID.ToString
            MyWmsDBTrans.OraDBExecuteSQLCmd(SqlStr, 1)

        End If
        Return TransBinCode

    End Function
    <WebMethod()> _
    Public Function SOA_FPickingSuggestBinCode(ByVal CompID As Integer, ByVal BranchID As Integer, ByVal CardID As Long) As String

        Dim SqlStr As String
        Dim TransBinCode As String


        SqlStr = "SELECT WHBINCODE FROM TWMSBINS WHERE WHBINID=(SELECT DEFAULTPICKINGBIN FROM TWMSPARAMS "
        If CompID > 0 Then
            SqlStr = SqlStr & " WHERE COMPID=" & CompID.ToString
            If BranchID > 0 Then
                SqlStr = SqlStr & " AND BRANCHID=" & BranchID.ToString
            End If
        End If
        SqlStr = SqlStr & ")"
        Try
            TransBinCode = MyWmsDBTrans.OraDBWmsExSelectCmdRStr2Str(SqlStr)
        Catch ex As Exception
        End Try


        If Len(TransBinCode) = 0 Or (TransBinCode = "NULL" Or TransBinCode = "Null") Or TransBinCode = "-1" Then
            SqlStr = "SELECT MIN(TWMSBINS.WHBINCODE) AS FIRSTBINCODE FROM TWMSBINS WHERE TWMSBINS.WHBINTYPE = 4 "
            SqlStr = SqlStr & " AND TWMSBINS.WHBINCODE NOT IN (SELECT DISTINCT TRANSBINCODE FROM TWMSPICKINGCARD WHERE TRANSBINCODE IS NOT NULL) "

            If CompID > 0 Then
                SqlStr = SqlStr & " AND TWMSBINS.COMPID=" & CompID.ToString
            End If
            If BranchID > 0 Then
                SqlStr = SqlStr & " AND TWMSBINS.BRANCHID=" & BranchID.ToString
            End If

            TransBinCode = MyWmsDBTrans.OraDBWmsExSelectCmdRStr2Str(SqlStr)
        End If



        If Len(TransBinCode) > 0 And (TransBinCode <> "NULL" Or TransBinCode <> "Null") And CardID > 0 Then
            SqlStr = "UPDATE TWMSPICKINGCARD SET TRANSBINCODE='" & TransBinCode & "'"
            SqlStr = SqlStr & " WHERE CARDID=" & CardID.ToString
            MyWmsDBTrans.OraDBExecuteSQLCmd(SqlStr, 1)

        End If
        Return TransBinCode

    End Function
    <WebMethod()> _
    Public Function SOA_FPalSuggestBinCode(ByVal CompID As Integer, ByVal BranchID As Integer, ByVal WhBuildingID As Integer) As DataSet

        Dim SqlStr As String
        Dim TransBinCode As String


        SqlStr = "SELECT TWMSBINS.WHBINID,TWMSBINS.WHBINCODE,TWMSBINS.FRIENDLYCODE,TWMSWAREHOUSES.WHBUILDID FROM TWMSBINS ,TWMSWAREHOUSES,TWMSZONES "
        SqlStr = SqlStr & "WHERE TWMSBINS.WHZONEID = TWMSZONES.WHZONEID AND "
        SqlStr = SqlStr & "TWMSZONES.WHID = TWMSWAREHOUSES.WHID  "
        If WhBuildingID > 0 Then
            SqlStr = SqlStr & " AND TWMSWAREHOUSES.WHBUILDID=" & WhBuildingID.ToString
        End If
        SqlStr = SqlStr & " AND TWMSBINS.WHZONEID = TWMSZONES.WHZONEID "
        SqlStr = SqlStr & "  AND TWMSBINS.WHBINTYPE = 6 "

        If CompID > 0 Then
            SqlStr = SqlStr & " AND TWMSBINS.COMPID=" & CompID.ToString
            If BranchID > 0 Then
                SqlStr = SqlStr & " AND TWMSBINS.BRANCHID=" & BranchID.ToString
            End If
        End If

        SqlStr = SqlStr & " ORDER BY TWMSBINS.WHBINCODE"

        SOA_FPalSuggestBinCode = MyWmsDBTrans.OraDBFillDataset(SqlStr, "PALBINCODES")


    End Function
    <WebMethod()> _
    Public Function SOA_FShippingPalSuggestBinCode(ByVal CompID As Integer, ByVal BranchID As Integer, ByVal WhBuildingID As Integer) As DataSet

        Dim SqlStr As String
        Dim TransBinCode As String


        SqlStr = "SELECT TWMSBINS.WHBINID,TWMSBINS.WHBINCODE,TWMSBINS.FRIENDLYCODE,TWMSWAREHOUSES.WHBUILDID FROM TWMSBINS ,TWMSWAREHOUSES,TWMSZONES "
        SqlStr = SqlStr & "WHERE TWMSBINS.WHZONEID = TWMSZONES.WHZONEID AND "
        SqlStr = SqlStr & "TWMSZONES.WHID = TWMSWAREHOUSES.WHID  "
        If WhBuildingID > 0 Then
            SqlStr = SqlStr & " AND TWMSWAREHOUSES.WHBUILDID=" & WhBuildingID.ToString
        End If
        SqlStr = SqlStr & " AND TWMSBINS.WHZONEID = TWMSZONES.WHZONEID "
        SqlStr = SqlStr & "  AND TWMSBINS.WHBINTYPE = 2 "

        If CompID > 0 Then
            SqlStr = SqlStr & " AND TWMSBINS.COMPID=" & CompID.ToString
            If BranchID > 0 Then
                SqlStr = SqlStr & " AND TWMSBINS.BRANCHID=" & BranchID.ToString
            End If
        End If
        SqlStr = SqlStr & " ORDER BY TWMSBINS.WHBINCODE"
        SOA_FShippingPalSuggestBinCode = MyWmsDBTrans.OraDBFillDataset(SqlStr, "PALBINCODES")


    End Function
    <WebMethod()> _
    Public Function SOA_FGetFreeFirstBinByWHouse(ByVal CompID As Integer, ByVal BranchID As Integer) As DataSet

        Dim SqlStr As String
        Dim WhBinCode As String

        Dim Ds As New DataSet

        SqlStr = "SELECT WHCODE,MIN(VWMSBINSTATUS.WHBINCODE) AS FIRSTBINCODE FROM VWMSBINSTATUS WHERE (BINITEMQTYPRIMARY IS NULL "
        SqlStr = SqlStr & " OR BINITEMQTYPRIMARY= 0 ) AND WHBINTYPE IN (1,3) "

        If CompID > 0 Then
            SqlStr = SqlStr & " AND VWMSBINSTATUS.COMPID=" & CompID.ToString
        End If
        If BranchID > 0 Then
            SqlStr = SqlStr & " AND VWMSBINSTATUS.BRANCHID=" & BranchID.ToString
        End If
        SqlStr = SqlStr & " GROUP BY WHCODE"

        Ds = MyWmsDBTrans.OraDBFillDataset(SqlStr, "FREEBINSBYWHOUSE")

        Return Ds
    End Function
    <WebMethod()> _
    Public Function SOA_FGetBinPalette(ByVal CompID As Integer, ByVal BranchID As Integer, ByVal BinCode As String) As String

        Dim MyWmsBinTrans As WmsBinTrans = New WmsBinTrans

        Dim SqlStr As String

        Dim PackItemNo As String

        Dim ThisBinCode As String

        If Len(BinCode) < 10 Then
            ThisBinCode = MyWmsBinTrans.FGetBinCodeByOldCode(CompID, BranchID, BinCode)
        Else
            ThisBinCode = BinCode
        End If

        If Len(ThisBinCode) = 0 Then ThisBinCode = "-1"

        SqlStr = "SELECT PACKITEMNO FROM TPACKAGES WHERE COMPID=" & CompID.ToString() & " AND BINCODE='" & ThisBinCode & "'"

        If BranchID > 0 Then
            SqlStr = SqlStr & " AND BRANCHID=" & BranchID.ToString()
        End If
        PackItemNo = MyWmsDBTrans.OraDBWmsExSelectCmdRStr2Str(SqlStr)


        Return PackItemNo

    End Function
    <WebMethod()> _
    Public Function SOA_FGetPaletteBinCode(ByVal CompID As Integer, ByVal BranchID As Integer, ByVal PackItemNo As String) As String

        Dim SqlStr As String
        Dim WhBinCode As String

        SqlStr = "SELECT BINCODE FROM TPACKAGES WHERE PACKITEMNO='" & PackItemNo & "'"

        If CompID > 0 Then
            SqlStr = SqlStr & " AND COMPID=" & CompID.ToString()
        End If
        If BranchID > 0 Then
            SqlStr = SqlStr & " AND BRANCHID=" & BranchID.ToString()
        End If

        WhBinCode = MyWmsDBTrans.OraDBWmsExSelectCmdRStr2Str(SqlStr)

        Return WhBinCode

    End Function
    <WebMethod()> _
    Public Function SOA_FSqlErrorLog(ByVal CompId As Integer, ByVal SrcCodeSnippet As String, ByVal SqlErrText As String, ByVal AppUserName As String) As String
        Dim Rtrn As Integer

        Rtrn = MyWmsDBTrans.f_sqlerrorlog(CompId, SrcCodeSnippet, SqlErrText, AppUserName)

        If Rtrn > 0 Then
            Return "1"
        Else
            Return "-1"
        End If

    End Function

    <WebMethod()> _
    Public Function SOA_FGetCustomerTitleByID(ByVal CustomerID As Integer) As String
        Dim SqlStr As String
        Dim CustomerTitle As String

        SqlStr = "SELECT CUSTOMERTITLE FROM TCUSTOMERS WHERE CUSTOMERID=" & CustomerID.ToString
        CustomerTitle = MyWmsDBTrans.OraDBWmsExSelectCmdRStr2Str(SqlStr)

        Return CustomerTitle
    End Function

#End Region
    '///////////////////////////////////////////
    '///   Bin Trns SOA functions //////////////
    '///////////////////////////////////////////
#Region " Bin Trans "
    <WebMethod()> _
    Public Function SOA_FProdPalettePutShippingBin(ByVal BranchID As Integer, ByVal PackItemID As Long, ByVal OrderID As Long, ByVal IAppUserTrans As Integer, ByVal TerminalTrans As String) As String
        Dim MyWmsBinTrans As WmsBinTrans = New WmsBinTrans

        Dim DsPacktems As DataSet = New DataSet

        Dim DsBinInfo As DataSet = New DataSet
        Dim DRBinInfo As DataRow

        Dim PackISqlStr As String

        Dim FWmsTransType As Integer
        Dim FBinID As Long
        Dim FBinIDFrom As Long
        Dim FBinIDTo As Long
        Dim FitemID As Long
        Dim FitemLotID As Long
        Dim FItemPQty As Decimal
        Dim FItemPMunit As Integer
        Dim FItemSQty As Decimal
        Dim FItemSMunit As Integer
        Dim FItemPackQty As Decimal
        Dim FItemPackMunit As Integer
        Dim FTransEx As Integer
        Dim FStoreTRansId As Long
        Dim FStoreTRansDtlId As Long
        Dim FDBCreateUser As Integer
        Dim FTerminal As String
        Dim FItemLength As Decimal
        Dim FItemColorID As Integer
        Dim FItemColorID2 As Integer

        Dim FOrderDtlID As Long


        Dim FPackItemNo As String


        Dim DsNextRow As Integer = 0
        Dim TblPackItems As DataTable
        Dim DRowPAckItems As DataRow


        Dim BinTransID As Long
        Dim InsCnt As Integer = 0

        Dim IsNewPalette As Boolean = True

        Dim ThisBinCodeTo As String
        Dim BinType As Integer

        Dim NullBinCode As String = Nothing

        Dim ExBinToQty As Decimal
        Dim SrcProdPhaseID As Integer
        Dim SrcProdBinID As Long

        Dim UpdBinQtyProd As Integer

        '///Get BinID DEfault

        PackISqlStr = ""
        '///////////////////
        'FWmsTransType = WmstransType
        FWmsTransType = 21
        '//////////////////

        SrcProdPhaseID = -1
        UpdBinQtyProd = 0



        FBinID = MyWmsBinTrans.FGetFirstShippingBinID(BranchID)
        If Not FBinID > 0 Then Return "-3"
        BinType = 2
        ThisBinCodeTo = MyWmsBinTrans.FGetBinCodeByID(1, BranchID, FBinID)


        'Get PAck Items Info
        DsPacktems = MyWmsBinTrans.FGetPackItemNoInfoInternalByID(PackItemID)

        InsCnt = 0
        If DsPacktems.Tables(0).Rows.Count() > 0 Then

            For Each TblPackItems In DsPacktems.Tables
                For Each DRowPAckItems In TblPackItems.Rows
                    DRowPAckItems = DsPacktems.Tables(0).Rows(DsNextRow)

                    FPackItemNo = DRowPAckItems("PACKITEMNO").ToString()

                    FitemID = Long.Parse(DRowPAckItems("ITEMID").ToString())
                    FItemLength = Decimal.Parse(DRowPAckItems("ITEMLENGTH").ToString())
                    Try
                        FOrderDtlID = Long.Parse(DRowPAckItems("ORDERDTLID").ToString())
                    Catch ex As Exception
                    End Try

                    FItemColorID = Integer.Parse(DRowPAckItems("ITEMCOLORID").ToString())

                    Try
                        FItemColorID2 = Integer.Parse(DRowPAckItems("ITEMCOLORID2").ToString())
                    Catch ex As Exception
                    End Try
                    '!!!!
                    If FItemColorID = FItemColorID2 Then
                        FItemColorID2 = 0
                    End If


                    FItemPQty = Long.Parse(DRowPAckItems("INITPACKITEMBARS").ToString())

                    If Len(DRowPAckItems("INITPACKITEMWEIGHT").ToString()) > 0 Then
                        FItemSQty = Decimal.Parse(DRowPAckItems("INITPACKITEMWEIGHT").ToString())
                    End If
                   

                    If SrcProdPhaseID < 0 Then
                        Try
                            SrcProdPhaseID = Integer.Parse(DRowPAckItems("SRCITEMS").ToString())

                            If SrcProdPhaseID > 0 Then
                                SrcProdBinID = MyWmsBinTrans.FGetProdPhaseBinID(SrcProdPhaseID)
                            End If
                        Catch ex As Exception

                        End Try
                    End If


                    FItemPMunit = 1 'bars
                    FItemSMunit = 2 ' weight

                    'clear if already inserted 
                    MyWmsBinTrans.FDeletePaletteBinItemQty(PackItemID)
                    '//////////////////////////////////////////////////////


                    FTransEx = 1
                    FTerminal = TerminalTrans
                    FDBCreateUser = IAppUserTrans

                    BinTransID = Long.Parse(MyWmsBinTrans.FCreateWmsTrans(1, BranchID, FWmsTransType, FBinID, FBinIDFrom, FBinIDTo, FitemID, _
                                        FitemLotID, FItemPQty, FItemPMunit, FItemSQty, FItemSMunit, _
                                        FItemPackQty, FItemPackMunit, FTransEx, _
                                        FStoreTRansId, FStoreTRansDtlId, FDBCreateUser, FTerminal, _
                                        FItemLength, FItemColorID, FItemColorID2, PackItemID, FPackItemNo, 0, ""))

                    If BinTransID > 0 Then
                        If SrcProdBinID > 0 And UpdBinQtyProd = 1 Then 'remove from src production
                            MyWmsBinTrans.FUpdateBinQtyAlter(1, BranchID, -1, "", SrcProdBinID, FitemID, FitemLotID, FItemLength, FItemColorID, FItemColorID2, FItemPQty, FItemPMunit, FItemSQty, FItemSMunit, FItemPackQty, FItemPackMunit, 0, 0, 0, FDBCreateUser, FTerminal)
                        End If
                        InsCnt += 1
                    End If

                    DsNextRow += 1
                Next DRowPAckItems
            Next TblPackItems

        End If

        If BinType = 4 Then
            FPackItemNo = ""
        End If

        If InsCnt > 0 Then
            Dim UpdBinQtyRtrn As Long
            FTransEx = 1
            UpdBinQtyRtrn = MyWmsBinTrans.FUpdateBinQty(1, BranchID, FTransEx, FPackItemNo, FBinID, FitemID, FitemLotID, FItemLength, FItemColorID, FItemColorID2, FItemPQty, FItemPMunit, FItemSQty, FItemSMunit, FItemPackQty, FItemPackMunit, OrderID, FOrderDtlID, 0, FDBCreateUser, FTerminal)

            'If (UpdBinQtyRtrn > 0) Then
            Dim RtrnLoc As Integer
            RtrnLoc = MyWmsBinTrans.FUpdatePaletteShippingLocation(1, BranchID, PackItemID, ThisBinCodeTo)
            'End If

        Return "1"
        Else
        Return "-1"
        End If
    End Function
    <WebMethod()> _
    Public Function SOA_FProdPalettePutAway(ByVal CompID As Integer, ByVal BranchID As Integer, ByVal FOrderID As Long, ByVal FRsrvStoreTRansID As Long, ByVal WmstransType As Integer, ByVal PaletteNo As String, ByVal WhBinCode As String, ByVal IAppUserTrans As Integer, ByVal TerminalTrans As String) As String
        Dim MyWmsBinTrans As WmsBinTrans = New WmsBinTrans

        Dim DsPacktems As DataSet = New DataSet

        Dim DsBinInfo As DataSet = New DataSet
        Dim DRBinInfo As DataRow

        Dim PackISqlStr As String

        Dim FWmsTransType As Integer
        Dim FBinID As Long
        Dim FBinIDFrom As Long
        Dim FBinIDTo As Long
        Dim FitemID As Long
        Dim FitemLotID As Long
        Dim FItemPQty As Decimal
        Dim FItemPMunit As Integer
        Dim FItemSQty As Decimal
        Dim FItemSMunit As Integer
        Dim FItemPackQty As Decimal
        Dim FItemPackMunit As Integer
        Dim FTransEx As Integer
        Dim FStoreTRansId As Long
        Dim FStoreTRansDtlId As Long
        Dim FDBCreateUser As Integer
        Dim FTerminal As String
        Dim FItemLength As Decimal
        Dim FItemColorID As Integer
        Dim FItemColorID2 As Integer
        Dim FPackItemID As Long
        Dim FPackItemNo As String


        Dim DsNextRow As Integer = 0
        Dim TblPackItems As DataTable
        Dim DRowPAckItems As DataRow


        Dim BinTransID As Long
        Dim InsCnt As Integer = 0

        Dim IsNewPalette As Boolean = True

        Dim ThisBinCodeTo As String
        Dim BinType As Integer

        Dim OrderDtlID As Long


        Dim NullBinCode As String = Nothing

        Dim ExBinToQty As Decimal
        Dim SrcProdPhaseID As Integer
        Dim SrcProdBinID As Long

        Dim UpdBinQtyProd As Integer

        '///Get BinID DEfault

        PackISqlStr = ""
        '///////////////////
        'FWmsTransType = WmstransType
        FWmsTransType = 21
        '//////////////////

        SrcProdPhaseID = -1
        UpdBinQtyProd = 0


        UpdBinQtyProd = MyWmsBinTrans.FCheckfiProdinWtyUpdatable(CompID)

        If Len(WhBinCode) > 0 Then

            WhBinCode = WhBinCode.ToUpper

            If Len(WhBinCode) = 5 Then
                DsBinInfo = MyWmsBinTrans.FGetBinInfo(CompID, BranchID, NullBinCode, WhBinCode)
            Else
                DsBinInfo = MyWmsBinTrans.FGetBinInfo(CompID, BranchID, WhBinCode, NullBinCode)
            End If

            If DsBinInfo.Tables(0).Rows.Count > 0 Then
                DRBinInfo = DsBinInfo.Tables(0).Rows(0)

                Try
                    FBinID = Long.Parse((DRBinInfo("WHBINID").ToString()))
                Catch ex As Exception
                End Try

                Try
                    BinType = Integer.Parse((DRBinInfo("WHBINTYPE").ToString()))
                Catch ex As Exception
                End Try

                Try
                    ThisBinCodeTo = DRBinInfo("WHBINCODE").ToString()
                Catch ex As Exception
                End Try
            Else
                Return "-3" 'not valid bin 
            End If

        Else 'Len(WhBinCode) ! > 0 
            Dim FStrBinIDTmp As String
            Try
                FStrBinIDTmp = MyWmsDBTrans.OraDBWmsExSelectCmdRN2String("SELECT WMSBINDEFAULT  FROM TWMSTRANSTYPES WHERE WMSTRANSTYPEID =" & FWmsTransType.ToString)
            Catch ex As Exception
            End Try

            If Len(FStrBinIDTmp) > 0 And FStrBinIDTmp <> "Null" Then FBinID = Long.Parse(FStrBinIDTmp)
        End If


        If Not FBinID > 0 Then Return "-3"


        FPackItemNo = PaletteNo
        'Get PAck Items Info
        DsPacktems = MyWmsBinTrans.FGetPackItemNoInfoInternal(FPackItemNo)
        InsCnt = 0
        If DsPacktems.Tables(0).Rows.Count() > 0 Then

            For Each TblPackItems In DsPacktems.Tables
                For Each DRowPAckItems In TblPackItems.Rows
                    DRowPAckItems = DsPacktems.Tables(0).Rows(DsNextRow)
                    FitemID = Long.Parse(DRowPAckItems("ITEMID").ToString())
                    FItemLength = Decimal.Parse(DRowPAckItems("ITEMLENGTH").ToString())

                    FItemColorID = Integer.Parse(DRowPAckItems("ITEMCOLORID").ToString())

                    Try
                        FItemColorID2 = Integer.Parse(DRowPAckItems("ITEMCOLORID2").ToString())
                    Catch ex As Exception
                    End Try
                    '!!!!
                    If FItemColorID = FItemColorID2 Then
                        FItemColorID2 = 0
                    End If

                    FPackItemID = Long.Parse(DRowPAckItems("PACKITEMID").ToString())
                    FItemPQty = Long.Parse(DRowPAckItems("INITPACKITEMBARS").ToString())

                    If Len(DRowPAckItems("INITPACKITEMWEIGHT").ToString()) > 0 Then
                        FItemSQty = Decimal.Parse(DRowPAckItems("INITPACKITEMWEIGHT").ToString())
                    End If
                    If Len(DRowPAckItems("ORDERDTLID").ToString()) > 0 Then
                        OrderDtlID = Long.Parse(DRowPAckItems("ORDERDTLID").ToString())
                    End If

                    If SrcProdPhaseID < 0 Then
                        Try
                            SrcProdPhaseID = Integer.Parse(DRowPAckItems("SRCITEMS").ToString())

                            If SrcProdPhaseID > 0 Then
                                SrcProdBinID = MyWmsBinTrans.FGetProdPhaseBinID(SrcProdPhaseID)
                            End If
                        Catch ex As Exception

                        End Try
                    End If

                    'If Len(DRowPAckItems("RESRVTORETRANSID").ToString()) > 0 Then
                    '    FRsrvStoreTRansID = Long.Parse(DRowPAckItems("RESRVTORETRANSID").ToString())
                    'End If

                    FItemPMunit = MyWmsBinTrans.FGetItemMUnitPrimary(FitemID)
                    FItemSMunit = MyWmsBinTrans.FGetItemMUnitSecondary(FitemID)

                    If Len(DRowPAckItems("INWAREHOUSE").ToString()) And DRowPAckItems("INWAREHOUSE").ToString() <> "Null" Then
                        If Integer.Parse(DRowPAckItems("INWAREHOUSE").ToString()) = 1 Or Integer.Parse(DRowPAckItems("INWAREHOUSE").ToString()) = -1 Then
                            Return "-2" ' Palette already inserted in warehouse
                        End If
                    End If


                    FTransEx = 1

                    FTerminal = TerminalTrans
                    FDBCreateUser = IAppUserTrans

                    If BinType > 0 And BinType <> 4 Then
                        ExBinToQty = MyWmsBinTrans.FGetBinITemQty(FBinID, 0, 0, 0, 0, 0)

                        If ExBinToQty > 0 Then
                            Return "-20"  ' Bin is not empty
                        End If
                    End If



                    BinTransID = Long.Parse(MyWmsBinTrans.FCreateWmsTrans(CompID, BranchID, FWmsTransType, FBinID, FBinIDFrom, FBinIDTo, FitemID, _
                                        FitemLotID, FItemPQty, FItemPMunit, FItemSQty, FItemSMunit, _
                                        FItemPackQty, FItemPackMunit, FTransEx, _
                                        FStoreTRansId, FStoreTRansDtlId, FDBCreateUser, FTerminal, _
                                        FItemLength, FItemColorID, FItemColorID2, FPackItemID, FPackItemNo, 0, ""))

                    If BinTransID > 0 Then
                        If SrcProdBinID > 0 And UpdBinQtyProd = 1 Then 'remove from src production
                            MyWmsBinTrans.FUpdateBinQtyAlter(CompID, BranchID, -1, "", SrcProdBinID, FitemID, FitemLotID, FItemLength, FItemColorID, FItemColorID2, FItemPQty, FItemPMunit, FItemSQty, FItemSMunit, FItemPackQty, FItemPackMunit, 0, 0, 0, FDBCreateUser, FTerminal)
                        End If
                        InsCnt += 1
                    End If

                    DsNextRow += 1
                Next DRowPAckItems
            Next TblPackItems

        End If

        If BinType = 4 Then
            FPackItemNo = ""
        End If

        If InsCnt > 0 Then
            Dim UpdBinQtyRtrn As Long

            UpdBinQtyRtrn = MyWmsBinTrans.FUpdateBinQty(CompID, BranchID, FTransEx, FPackItemNo, FBinID, FitemID, FitemLotID, FItemLength, FItemColorID, FItemColorID2, FItemPQty, FItemPMunit, FItemSQty, FItemSMunit, FItemPackQty, FItemPackMunit, FOrderID, OrderDtlID, FRsrvStoreTRansID, FDBCreateUser, FTerminal)

            If (UpdBinQtyRtrn > 0 And BinType <> 4 And FPackItemNo.Length > 0) Then
                Dim RtrnLoc As Integer
                RtrnLoc = MyWmsBinTrans.FUpdatePaletteLocation(CompID, BranchID, FPackItemNo, ThisBinCodeTo, IsNewPalette)
            ElseIf UpdBinQtyRtrn > 0 And BinType = 4 Then 'disable palette
                MyWmsBinTrans.FSetPaletteOff(CompID, BranchID, FPackItemID, FPackItemNo)
            End If

            Return "1"
        Else
            Return "-1"
        End If
    End Function
    <WebMethod()> _
   Public Function SOA_FProdPalettePutAwayNew(ByVal CompID As Integer, ByVal BranchID As Integer, ByVal FOrderID As Long, ByVal FRsrvStoreTRansID As Long, ByVal WmstransType As Integer, ByVal PaletteNo As String, ByVal WhBinCode As String, ByVal RealPackItemweight As Long, ByVal IAppUserTrans As Integer, ByVal TerminalTrans As String) As String
        Dim MyWmsBinTrans As WmsBinTrans = New WmsBinTrans

        Dim DsPacktems As DataSet = New DataSet

        Dim DsBinInfo As DataSet = New DataSet
        Dim DRBinInfo As DataRow

        Dim PackISqlStr As String

        Dim FWmsTransType As Integer
        Dim FBinID As Long
        Dim FBinIDFrom As Long
        Dim FBinIDTo As Long
        Dim FitemID As Long
        Dim FitemLotID As Long
        Dim FItemPQty As Decimal
        Dim FItemPMunit As Integer
        Dim FItemSQty As Decimal
        Dim FItemSMunit As Integer
        Dim FItemPackQty As Decimal
        Dim FItemPackMunit As Integer
        Dim FTransEx As Integer
        Dim FStoreTRansId As Long
        Dim FStoreTRansDtlId As Long
        Dim FDBCreateUser As Integer
        Dim FTerminal As String
        Dim FItemLength As Decimal
        Dim FItemColorID As Integer
        Dim FItemColorID2 As Integer
        Dim FPackItemID As Long
        Dim FPackItemNo As String


        Dim DsNextRow As Integer = 0
        Dim TblPackItems As DataTable
        Dim DRowPAckItems As DataRow


        Dim BinTransID As Long
        Dim InsCnt As Integer = 0

        Dim IsNewPalette As Boolean = True

        Dim ThisBinCodeTo As String
        Dim BinType As Integer

        Dim OrderDtlID As Long


        Dim NullBinCode As String = Nothing

        Dim ExBinToQty As Decimal
        Dim SrcProdPhaseID As Integer
        Dim SrcProdBinID As Long

        Dim UpdBinQtyProd As Integer

        '///Get BinID DEfault

        PackISqlStr = ""
        '///////////////////
        'FWmsTransType = WmstransType
        FWmsTransType = 21
        '//////////////////

        SrcProdPhaseID = -1
        UpdBinQtyProd = 0


        UpdBinQtyProd = MyWmsBinTrans.FCheckfiProdinWtyUpdatable(CompID)

        If Len(WhBinCode) > 0 Then

            WhBinCode = WhBinCode.ToUpper

            If Len(WhBinCode) = 5 Then
                DsBinInfo = MyWmsBinTrans.FGetBinInfo(CompID, BranchID, NullBinCode, WhBinCode)
            Else
                DsBinInfo = MyWmsBinTrans.FGetBinInfo(CompID, BranchID, WhBinCode, NullBinCode)
            End If

            If DsBinInfo.Tables(0).Rows.Count > 0 Then
                DRBinInfo = DsBinInfo.Tables(0).Rows(0)

                Try
                    FBinID = Long.Parse((DRBinInfo("WHBINID").ToString()))
                Catch ex As Exception
                End Try

                Try
                    BinType = Integer.Parse((DRBinInfo("WHBINTYPE").ToString()))
                Catch ex As Exception
                End Try

                Try
                    ThisBinCodeTo = DRBinInfo("WHBINCODE").ToString()
                Catch ex As Exception
                End Try
            Else
                Return "-3" 'not valid bin 
            End If

        Else 'Len(WhBinCode) ! > 0 
            Dim FStrBinIDTmp As String
            Try
                FStrBinIDTmp = MyWmsDBTrans.OraDBWmsExSelectCmdRN2String("SELECT WMSBINDEFAULT  FROM TWMSTRANSTYPES WHERE WMSTRANSTYPEID =" & FWmsTransType.ToString)
            Catch ex As Exception
            End Try

            If Len(FStrBinIDTmp) > 0 And FStrBinIDTmp <> "Null" Then FBinID = Long.Parse(FStrBinIDTmp)
        End If


        If Not FBinID > 0 Then Return "-3"


        FPackItemNo = PaletteNo
        'Get PAck Items Info
        DsPacktems = MyWmsBinTrans.FGetPackItemNoInfoInternal(FPackItemNo)
        InsCnt = 0
        If DsPacktems.Tables(0).Rows.Count() > 0 Then

            For Each TblPackItems In DsPacktems.Tables
                For Each DRowPAckItems In TblPackItems.Rows
                    DRowPAckItems = DsPacktems.Tables(0).Rows(DsNextRow)
                    FitemID = Long.Parse(DRowPAckItems("ITEMID").ToString())
                    FItemLength = Decimal.Parse(DRowPAckItems("ITEMLENGTH").ToString())

                    FItemColorID = Integer.Parse(DRowPAckItems("ITEMCOLORID").ToString())

                    Try
                        FItemColorID2 = Integer.Parse(DRowPAckItems("ITEMCOLORID2").ToString())
                    Catch ex As Exception
                    End Try
                    '!!!!
                    If FItemColorID = FItemColorID2 Then
                        FItemColorID2 = 0
                    End If

                    FPackItemID = Long.Parse(DRowPAckItems("PACKITEMID").ToString())
                    FItemPQty = Long.Parse(DRowPAckItems("INITPACKITEMBARS").ToString())

                    If Len(DRowPAckItems("INITPACKITEMWEIGHT").ToString()) > 0 And TblPackItems.Rows.Count = 1 Then
                        FItemSQty = Decimal.Parse(DRowPAckItems("INITPACKITEMWEIGHT").ToString())
                    End If
                    If RealPackItemweight > 0 And TblPackItems.Rows.Count = 1 Then
                        FItemSQty = RealPackItemweight
                    End If

                    If Len(DRowPAckItems("ORDERDTLID").ToString()) > 0 Then
                        OrderDtlID = Long.Parse(DRowPAckItems("ORDERDTLID").ToString())
                    End If

                    If SrcProdPhaseID < 0 Then
                        Try
                            SrcProdPhaseID = Integer.Parse(DRowPAckItems("SRCITEMS").ToString())

                            If SrcProdPhaseID > 0 Then
                                SrcProdBinID = MyWmsBinTrans.FGetProdPhaseBinID(SrcProdPhaseID)
                            End If
                        Catch ex As Exception

                        End Try
                    End If

                    'If Len(DRowPAckItems("RESRVTORETRANSID").ToString()) > 0 Then
                    '    FRsrvStoreTRansID = Long.Parse(DRowPAckItems("RESRVTORETRANSID").ToString())
                    'End If

                    FItemPMunit = MyWmsBinTrans.FGetItemMUnitPrimary(FitemID)
                    FItemSMunit = MyWmsBinTrans.FGetItemMUnitSecondary(FitemID)

                    If Len(DRowPAckItems("INWAREHOUSE").ToString()) And DRowPAckItems("INWAREHOUSE").ToString() <> "Null" Then
                        If Integer.Parse(DRowPAckItems("INWAREHOUSE").ToString()) = 1 Or Integer.Parse(DRowPAckItems("INWAREHOUSE").ToString()) = -1 Then
                            Return "-2" ' Palette already inserted in warehouse
                        End If
                    End If


                    FTransEx = 1

                    FTerminal = TerminalTrans
                    FDBCreateUser = IAppUserTrans

                    If BinType > 0 And BinType <> 4 Then
                        ExBinToQty = MyWmsBinTrans.FGetBinITemQty(FBinID, 0, 0, 0, 0, 0)

                        If ExBinToQty > 0 Then
                            Return "-20"  ' Bin is not empty
                        End If
                    End If



                    BinTransID = Long.Parse(MyWmsBinTrans.FCreateWmsTrans(CompID, BranchID, FWmsTransType, FBinID, FBinIDFrom, FBinIDTo, FitemID, _
                                        FitemLotID, FItemPQty, FItemPMunit, FItemSQty, FItemSMunit, _
                                        FItemPackQty, FItemPackMunit, FTransEx, _
                                        FStoreTRansId, FStoreTRansDtlId, FDBCreateUser, FTerminal, _
                                        FItemLength, FItemColorID, FItemColorID2, FPackItemID, FPackItemNo, 0, ""))

                    If BinTransID > 0 Then
                        If SrcProdBinID > 0 And UpdBinQtyProd = 1 Then 'remove from src production
                            MyWmsBinTrans.FUpdateBinQtyAlter(CompID, BranchID, -1, "", SrcProdBinID, FitemID, FitemLotID, FItemLength, FItemColorID, FItemColorID2, FItemPQty, FItemPMunit, FItemSQty, FItemSMunit, FItemPackQty, FItemPackMunit, 0, 0, 0, FDBCreateUser, FTerminal)
                        End If
                        InsCnt += 1
                    End If

                    DsNextRow += 1
                Next DRowPAckItems
            Next TblPackItems

        End If

        If BinType = 4 Then
            FPackItemNo = ""
        End If

        If InsCnt > 0 Then
            Dim UpdBinQtyRtrn As Long

            UpdBinQtyRtrn = MyWmsBinTrans.FUpdateBinQty(CompID, BranchID, FTransEx, FPackItemNo, FBinID, FitemID, FitemLotID, FItemLength, FItemColorID, FItemColorID2, FItemPQty, FItemPMunit, FItemSQty, FItemSMunit, FItemPackQty, FItemPackMunit, FOrderID, OrderDtlID, FRsrvStoreTRansID, FDBCreateUser, FTerminal)

            If (UpdBinQtyRtrn > 0 And BinType <> 4 And FPackItemNo.Length > 0) Then
                Dim RtrnLoc As Integer
                RtrnLoc = MyWmsBinTrans.FUpdatePaletteLocation(CompID, BranchID, FPackItemNo, ThisBinCodeTo, IsNewPalette)
            ElseIf UpdBinQtyRtrn > 0 And BinType = 4 Then 'disable palette
                MyWmsBinTrans.FSetPaletteOff(CompID, BranchID, FPackItemID, FPackItemNo)
            End If

            Return "1"
        Else
            Return "-1"
        End If
    End Function
    <WebMethod()> _
    Public Function SOA_FProdPaletteUpdateProdPackweight(ByVal PackItemID As Long, ByVal PackItemWeight As Long) As String
        Dim sqlstr As String
        Dim rtrn As String
        Dim ExPackitemweight As Long
        Dim setPackItemWeight As Long

        If (PackItemWeight > 0) Then
            setPackItemWeight = PackItemWeight
        ElseIf (PackItemID > 0) Then
            sqlstr = "SELECT PACKITEMWEIGHT FROM TPACKAGES WHERE PACKITEMID=" & PackItemID.ToString()
            Try
                ExPackitemweight = MyWmsDBTrans.OraDBWmsExSelectCmdRN2String(sqlstr)
            Catch ex As Exception
                ExPackitemweight = 0
            End Try

            If ExPackitemweight > 0 Then
                setPackItemWeight = ExPackitemweight
            End If
        Else
            rtrn = "0"
        End If
        If setPackItemWeight > 0 Then
            sqlstr = "UPDATE TPACKAGING SET ITEMWEIGHT= " & PackItemWeight.ToString() & " WHERE PACKITEMDTLID=(SELECT PACKITEMDTLID FROM TPACKAGESDTL WHERE PACKITEMID=" & PackItemID.ToString() & ")"
            Try
                rtrn = MyWmsDBTrans.OraDBExecuteSQLCmd(sqlstr, 1).ToString()
            Catch ex As Exception
                rtrn = "-1"
            End Try

        Else
            rtrn = "0"
        End If

        Return rtrn
    End Function
    <WebMethod()> _
    Public Function SOA_FItemsExportFromPalette(ByVal CompID As Integer, ByVal BranchID As Integer, ByVal FOrderID As Long, ByVal FRsrvStoreTRansID As Long, ByVal WmstransType As Integer, ByVal PaletteNo As String, ByVal ExItemPqty As Decimal, ByVal WhBinCode As String, ByVal IAppUserTrans As Integer, ByVal TerminalTrans As String) As String
        Dim MyWmsBinTrans As WmsBinTrans = New WmsBinTrans

        Dim DsPacktems As DataSet = New DataSet
        Dim PackISqlStr As String

        Dim DsBinInfo As DataSet = New DataSet
        Dim DRBinInfo As DataRow

        Dim FWmsTransType As Integer
        Dim FBinID As Long
        Dim FBinIDFrom As Long
        Dim FBinIDTo As Long
        Dim FitemID As Long
        Dim FitemLotID As Long
        Dim FItemPQty As Decimal
        Dim FItemPMunit As Integer
        Dim FItemSQty As Decimal
        Dim FItemSMunit As Integer
        Dim FItemPackQty As Decimal
        Dim FItemPackMunit As Integer
        Dim FTransEx As Integer
        Dim FStoreTRansId As Long
        Dim FStoreTRansDtlId As Long
        Dim FDBCreateUser As Integer
        Dim FTerminal As String
        Dim FItemLength As Decimal
        Dim FItemColorID As Integer
        Dim FItemColorID2 As Integer
        Dim FPackItemID As Long
        Dim FProdID As Long
        Dim FPackItemNo As String


        Dim DsNextRow As Integer = 0
        Dim TblPackItems As DataTable
        Dim DRowPAckItems As DataRow
        Dim BinTransID As Long
        Dim InsCnt As Integer = 0

        Dim OrderDtlID As Long


        Dim UpdBinQtyRtrn As Long


        Dim PaletteItemPqty As Decimal
        Dim NullBinCode As String = Nothing

        Dim BinType As Integer



        '///Get BinID DEfault

        PackISqlStr = ""
        '///////////////////
        'FWmsTransType = WmstransType
        FWmsTransType = 2
        '//////////////////

        '////////////////// [ perform some general checks ] /////////////
        If ExItemPqty < 0 Or ExItemPqty = Nothing Then Return -2
        '////////////////////////////////////////////////////////////////

        If Len(WhBinCode) > 0 Then
            If Len(WhBinCode) = 5 Then
                DsBinInfo = MyWmsBinTrans.FGetBinInfo(CompID, BranchID, NullBinCode, WhBinCode)
            ElseIf Len(WhBinCode) = 10 Then
                DsBinInfo = MyWmsBinTrans.FGetBinInfo(CompID, BranchID, WhBinCode, NullBinCode)
            Else
                Return "-3"
            End If

            If DsBinInfo.Tables(0).Rows.Count > 0 Then
                DRBinInfo = DsBinInfo.Tables(0).Rows(0)

                If Len(DRBinInfo("WHBINID").ToString()) > 0 Then
                    FBinID = Long.Parse((DRBinInfo("WHBINID").ToString()))
                End If
                If Len(DRBinInfo("WHBINTYPE").ToString()) > 0 Then
                    BinType = Integer.Parse((DRBinInfo("WHBINTYPE").ToString()))
                End If

            End If
        Else
            Dim FStrBinIDTmp As String
            FStrBinIDTmp = MyWmsDBTrans.OraDBWmsExSelectCmdRN2String("SELECT WMSBINDEFAULT  FROM TWMSTRANSTYPES WHERE WMSTRANSTYPEID =" & FWmsTransType.ToString)

            If Len(FStrBinIDTmp) > 0 Then FBinID = Long.Parse(FStrBinIDTmp)
        End If

        FPackItemNo = PaletteNo
        'Get PAck Items Info
        DsPacktems = MyWmsBinTrans.FGetPackItemNoInfoInternal(FPackItemNo)
        InsCnt = 0
        If DsPacktems.Tables(0).Rows.Count() > 0 Then

            For Each TblPackItems In DsPacktems.Tables
                For Each DRowPAckItems In TblPackItems.Rows
                    DRowPAckItems = DsPacktems.Tables(0).Rows(DsNextRow)
                    FitemID = Long.Parse(DRowPAckItems("ITEMID").ToString())
                    FItemLength = Decimal.Parse(DRowPAckItems("ITEMLENGTH").ToString())
                    FItemColorID = Integer.Parse(DRowPAckItems("ITEMCOLORID").ToString())


                    If Len(DRowPAckItems("ITEMCOLORID2").ToString()) > 0 Then
                        FItemColorID2 = Integer.Parse(DRowPAckItems("ITEMCOLORID2").ToString())
                    End If

                    If Len(DRowPAckItems("ORDERDTLID").ToString()) > 0 Then
                        OrderDtlID = Long.Parse(DRowPAckItems("ORDERDTLID").ToString())
                    End If

                    'If Len(DRowPAckItems("RESRVTORETRANSID").ToString()) > 0 Then
                    '    ResrvStoreTransID = Long.Parse(DRowPAckItems("RESRVTORETRANSID").ToString())
                    'End If

                    If Len(DRowPAckItems("PACKITEMBARS").ToString()) > 0 Then
                        PaletteItemPqty = Decimal.Parse(DRowPAckItems("PACKITEMBARS").ToString())
                    End If

                    Try
                        FProdID = Long.Parse(DRowPAckItems("PRODID").ToString())
                    Catch ex As Exception

                    End Try

                    FPackItemID = Long.Parse(DRowPAckItems("PACKITEMID").ToString())

                    FItemPQty = ExItemPqty

                    FItemPMunit = MyWmsBinTrans.FGetItemMUnitPrimary(FitemID)
                    FItemSMunit = MyWmsBinTrans.FGetItemMUnitSecondary(FitemID)

                    FTransEx = -1

                    FTerminal = TerminalTrans
                    FDBCreateUser = IAppUserTrans


                    If PaletteItemPqty = Nothing Then PaletteItemPqty = 0

                    If FItemPQty > PaletteItemPqty Then
                        Return -2 ' not enouph qty to be exported
                    End If

                    BinTransID = Long.Parse(MyWmsBinTrans.FCreateWmsTrans(CompID, BranchID, FWmsTransType, FBinID, FBinIDFrom, FBinIDTo, FitemID, _
                                        FitemLotID, FItemPQty, FItemPMunit, FItemSQty, FItemSMunit, _
                                        FItemPackQty, FItemPackMunit, FTransEx, _
                                        FStoreTRansId, FStoreTRansDtlId, FDBCreateUser, FTerminal, _
                                        FItemLength, FItemColorID, FItemColorID2, FPackItemID, FPackItemNo, 0, ""))

                    If BinTransID > 0 Then InsCnt += 1

                    DsNextRow += 1
                Next DRowPAckItems
            Next TblPackItems

        End If


        If InsCnt > 0 Then

            If FBinID > 0 Then
                UpdBinQtyRtrn = MyWmsBinTrans.FUpdateBinQty(CompID, BranchID, FTransEx, FPackItemNo, FBinID, FitemID, FitemLotID, FItemLength, FItemColorID, FItemColorID2, FItemPQty, FItemPMunit, FItemSQty, FItemSMunit, FItemPackQty, FItemPackMunit, FOrderID, OrderDtlID, FRsrvStoreTRansID, FDBCreateUser, FTerminal)

                If UpdBinQtyRtrn > 0 Then
                    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    'Check if there is any Internal transger for orders so update production source info
                    MyWmsBinTrans.FUpdateIntenalTransferProdSource(FProdID, FPackItemID, FitemID, FItemColorID, FItemLength)
                    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

                    'check if all items were wxported from the palette so set pallet off
                    If FItemPQty = PaletteItemPqty Then
                        MyWmsBinTrans.FSetPaletteOff(CompID, BranchID, FPackItemID, FPackItemNo)
                    End If
                    Return 1
                ElseIf UpdBinQtyRtrn = -2 Then
                    MyWmsBinTrans.FCancelBinTrans(BinTransID)
                    Return "-2" 'trans succedded, NotEnouph Qty to be exported   
                Else
                    Return "-1"
                End If
            End If
        Else
            Return "-1"  'trans failed
        End If
    End Function
    <WebMethod()> _
    Public Function SOA_FItemsAddToPalette(ByVal CompID As Integer, ByVal BranchID As Integer, ByVal FOrderID As Long, ByVal FRsrvStoreTRansID As Long, ByVal WmstransType As Integer, ByVal PaletteNo As String, ByVal AddItemPqty As Decimal, ByVal WhBinCode As String, ByVal IAppUserTrans As Integer, ByVal TerminalTrans As String) As String
        Dim MyWmsBinTrans As WmsBinTrans = New WmsBinTrans

        Dim DsPacktems As DataSet = New DataSet
        Dim PackISqlStr As String

        Dim DsBinInfo As DataSet = New DataSet
        Dim DRBinInfo As DataRow

        Dim FWmsTransType As Integer
        Dim FBinID As Long
        Dim FBinIDFrom As Long
        Dim FBinIDTo As Long
        Dim FitemID As Long
        Dim FitemLotID As Long
        Dim FItemPQty As Decimal
        Dim FItemPMunit As Integer
        Dim FItemSQty As Decimal
        Dim FItemSMunit As Integer
        Dim FItemPackQty As Decimal
        Dim FItemPackMunit As Integer
        Dim FTransEx As Integer
        Dim FStoreTRansId As Long
        Dim FStoreTRansDtlId As Long
        Dim FDBCreateUser As Integer
        Dim FTerminal As String
        Dim FItemLength As Decimal
        Dim FItemColorID As Integer
        Dim FItemColorID2 As Integer
        Dim FPackItemID As Long
        Dim FPackItemNo As String


        Dim DsNextRow As Integer = 0
        Dim TblPackItems As DataTable
        Dim DRowPAckItems As DataRow
        Dim BinTransID As Long
        Dim InsCnt As Integer = 0

        Dim IsNewPalette As Boolean = False


        Dim OrderDtlID As Long
        Dim ResrvStoreTransID As Long

        Dim UpdBinQtyRtrn As Long

        Dim NullBinCode As String = Nothing

        Dim BinType As Integer
        '///Get BinID DEfault

        PackISqlStr = ""
        '///////////////////
        'FWmsTransType = WmstransType
        FWmsTransType = 42
        '//////////////////

        '////////////////// [ perform some general checks ] /////////////
        If AddItemPqty < 0 Or AddItemPqty = Nothing Then Return -2
        '////////////////////////////////////////////////////////////////

        If Len(WhBinCode) > 0 Then
            If Len(WhBinCode) = 5 Then
                DsBinInfo = MyWmsBinTrans.FGetBinInfo(CompID, BranchID, NullBinCode, WhBinCode)
            ElseIf Len(WhBinCode) = 10 Then
                DsBinInfo = MyWmsBinTrans.FGetBinInfo(CompID, BranchID, WhBinCode, NullBinCode)
            Else
                Return "-3"
            End If

            If DsBinInfo.Tables(0).Rows.Count > 0 Then
                DRBinInfo = DsBinInfo.Tables(0).Rows(0)

                If Len(DRBinInfo("WHBINID").ToString()) > 0 Then
                    FBinID = Long.Parse((DRBinInfo("WHBINID").ToString()))
                End If
                If Len(DRBinInfo("WHBINTYPE").ToString()) > 0 Then
                    BinType = Integer.Parse((DRBinInfo("WHBINTYPE").ToString()))
                End If
            End If
        Else
            Dim FStrBinIDTmp As String
            Try
                FStrBinIDTmp = MyWmsDBTrans.OraDBWmsExSelectCmdRN2String("SELECT WMSBINDEFAULT  FROM TWMSTRANSTYPES WHERE WMSTRANSTYPEID =" & FWmsTransType.ToString)
                FBinID = Long.Parse(FStrBinIDTmp)
            Catch ex As Exception
                FBinID = 0
            End Try

        End If

        If Not (FBinID > 0) Then
            Return "-1"
        End If

        FPackItemNo = PaletteNo
        'Get PAck Items Info
        DsPacktems = MyWmsBinTrans.FGetPackItemNoInfoInternal(FPackItemNo)
        InsCnt = 0
        If DsPacktems.Tables(0).Rows.Count() > 0 Then

            For Each TblPackItems In DsPacktems.Tables
                For Each DRowPAckItems In TblPackItems.Rows
                    DRowPAckItems = DsPacktems.Tables(0).Rows(DsNextRow)
                    FitemID = Long.Parse(DRowPAckItems("ITEMID").ToString())
                    FItemLength = Decimal.Parse(DRowPAckItems("ITEMLENGTH").ToString())
                    FItemColorID = Integer.Parse(DRowPAckItems("ITEMCOLORID").ToString())

                    If Len(DRowPAckItems("ITEMCOLORID2").ToString()) > 0 Then
                        FItemColorID2 = Integer.Parse(DRowPAckItems("ITEMCOLORID2").ToString())
                    End If

                    If Len(DRowPAckItems("ORDERDTLID").ToString()) > 0 Then
                        OrderDtlID = Long.Parse(DRowPAckItems("ORDERDTLID").ToString())
                    End If

                    If Len(DRowPAckItems("RESRVTORETRANSID").ToString()) > 0 Then
                        ResrvStoreTransID = Long.Parse(DRowPAckItems("RESRVTORETRANSID").ToString())
                    End If

                    FPackItemID = Long.Parse(DRowPAckItems("PACKITEMID").ToString())

                    FItemPQty = AddItemPqty

                    'If Len(DRowPAckItems("PACKITEMWEIGHT").ToString()) > 0 Then
                    '    FItemSQty = Integer.Parse(DRowPAckItems("PACKITEMWEIGHT").ToString())
                    'Else
                    '    FItemSQty = 0
                    'End If

                    FItemPMunit = MyWmsBinTrans.FGetItemMUnitPrimary(FitemID)
                    FItemSMunit = MyWmsBinTrans.FGetItemMUnitSecondary(FitemID)
                    FTransEx = 1

                    FTerminal = TerminalTrans
                    FDBCreateUser = IAppUserTrans



                    BinTransID = Long.Parse(MyWmsBinTrans.FCreateWmsTrans(CompID, BranchID, FWmsTransType, FBinID, FBinIDFrom, FBinIDTo, FitemID, _
                                        FitemLotID, FItemPQty, FItemPMunit, FItemSQty, FItemSMunit, _
                                        FItemPackQty, FItemPackMunit, FTransEx, _
                                        FStoreTRansId, FStoreTRansDtlId, FDBCreateUser, FTerminal, _
                                        FItemLength, FItemColorID, FItemColorID2, FPackItemID, FPackItemNo, 0, ""))

                    If BinTransID > 0 Then InsCnt += 1

                    DsNextRow += 1
                Next DRowPAckItems
            Next TblPackItems

        End If


        If InsCnt > 0 Then

            If FBinID > 0 Then
                UpdBinQtyRtrn = MyWmsBinTrans.FUpdateBinQty(CompID, BranchID, FTransEx, FPackItemNo, FBinID, FitemID, FitemLotID, FItemLength, FItemColorID, FItemColorID2, FItemPQty, FItemPMunit, FItemSQty, FItemSMunit, FItemPackQty, FItemPackMunit, FOrderID, OrderDtlID, FRsrvStoreTRansID, FDBCreateUser, FTerminal)
                If UpdBinQtyRtrn > 0 Then
                    Return 1
                ElseIf UpdBinQtyRtrn = -2 Then
                    Return "-2" 'trans succedded, NotEnouph Qty to be exported                    
                Else
                    Return "-1"
                End If
            End If
        Else
            Return "-1"  'trans failed
        End If
    End Function
    <WebMethod()> _
 Public Function SOA_FInternalMovePalette(ByVal CompID As Integer, ByVal BranchID As Integer, ByVal WmstransType As Integer, ByVal FStoreTransID As Long, ByVal FOrderID As Long, ByVal PaletteNo As String, ByVal WhBinCodeFrom As String, ByVal IntTransQty As Decimal, ByVal WhBinCodeTo As String, ByVal IAppUserTrans As Integer, ByVal TerminalTrans As String, ByVal MoveORMerge As Integer) As String
        '//////////////////
        'MoveORMerge{1:Move,2:Merge,3:Trans}
        '///////// [ Rtrn Values ] ////////////
        '{1:SUCCED,-1:FAILED FENERAL,-2:TRANS SUCCEDD EXPORT FAILED,-3:BINODE ERROR}
        '{-4:INVALID BINCODEFROM,-5:INVALID BINCODETO,-20:BINTO IS NOT EMPTY FOR PALETTEMOVE}
        '{-7: ÈÝóç Ðñ. , ÈÝóç Ðñïïñ ßäéá}
        '{-6:PROBLEM WITH PALETTE INFO}
        '{-10: Problem on Exporting}
        '{-21:BINTO HAS NOT SAME ITEMS FOR MERGE}
        '/////////////////////////////////////

        Dim MyWmsBinTrans As WmsBinTrans = New WmsBinTrans

        Dim DsPackItems As DataSet = New DataSet
        Dim PackISqlStr As String

        Dim DsBinInfo As DataSet = New DataSet
        Dim DRBinInfo As DataRow

        Dim FWmsTransType As Integer
        Dim FBinID As Long
        Dim FBinIDFrom As Long
        Dim FBinIDTo As Long
        Dim FitemID As Long
        Dim FitemLotID As Long
        Dim FItemPQty As Decimal
        Dim FItemPMunit As Integer
        Dim FItemSQty As Decimal
        Dim FItemSMunit As Integer
        Dim FItemPackQty As Decimal
        Dim FItemPackMunit As Integer
        Dim FTransEx As Integer
        Dim FStoreTRansDtlId As Long
        Dim FDBCreateUser As Integer
        Dim FTerminal As String
        Dim FItemLength As Decimal
        Dim FItemColorID As Integer
        Dim FItemColorID2 As Integer
        Dim FPackItemID As String
        Dim FPackItemNo As String


        Dim DsNextRow As Integer = 0
        Dim TblPackItems As New DataTable
        Dim DRowPackItems As DataRow
        Dim BinTransID As Long
        Dim InsCnt As Integer = 0

        Dim NullVl As Long

        Dim IsNewPalette As Boolean = False

        Dim ExBinToQty As Decimal


        Dim OrderDtlID As Long
        Dim ResrvStoreTransID As Long

        Dim ThisBinCodeTo As String
        Dim NullBinCode As String = Nothing
        Dim BinType As Integer

        Dim PackItemIDTo As Long
        Dim PackItemNoTo As String
        Dim DsPaletteInfoTo As New DataSet
        Dim DrPaletteInfoTo As DataRow
        Dim NullVal As String
        '///Get BinID DEfault

        PackISqlStr = ""
        '///////////////////
        'FWmsTransType = WmstransType
        FWmsTransType = 4
        '//////////////////

        If Len(WhBinCodeFrom) > 0 Then
            If Len(WhBinCodeFrom) = 5 Then
                FBinIDFrom = MyWmsBinTrans.FGetBinIDByOldCode(CompID, BranchID, WhBinCodeFrom)
            Else
                FBinIDFrom = MyWmsBinTrans.FGetBinIDByCode(CompID, BranchID, WhBinCodeFrom)
            End If

            If Not FBinIDFrom > 0 Then
                Return "-4" 'INVALID BINCODE FROM
            End If
        Else
            Return "-4" 'INVALID BINCODE FROM
        End If

        If Len(WhBinCodeTo) > 0 Then
            WhBinCodeTo = WhBinCodeTo.ToUpper

            If Len(WhBinCodeTo) = 5 Or Len(WhBinCodeTo) = 6 Then
                DsBinInfo = MyWmsBinTrans.FGetBinInfo(CompID, BranchID, NullBinCode, WhBinCodeTo)
            ElseIf Len(WhBinCodeTo) > 0 Then
                DsBinInfo = MyWmsBinTrans.FGetBinInfo(CompID, BranchID, WhBinCodeTo, NullBinCode)
            Else
                Return "-5"
            End If

            If DsBinInfo.Tables(0).Rows.Count > 0 Then
                DRBinInfo = DsBinInfo.Tables(0).Rows(0)

                If Len(DRBinInfo("WHBINID").ToString()) > 0 Then
                    FBinIDTo = Long.Parse((DRBinInfo("WHBINID").ToString()))
                End If
                If Len(DRBinInfo("WHBINTYPE").ToString()) > 0 Then
                    BinType = Integer.Parse((DRBinInfo("WHBINTYPE").ToString()))
                End If
                If Len(DRBinInfo("WHBINCODE").ToString()) > 0 Then
                    ThisBinCodeTo = DRBinInfo("WHBINCODE").ToString()
                End If
            Else
                Return "-5"
            End If
        Else
            Return "-5"
        End If

        If FBinIDFrom = FBinIDTo Then
            Return "-7"
        End If

        FPackItemNo = PaletteNo
        'Get PAck Items Info
        DsPackItems = MyWmsBinTrans.FGetPackItemNoInfoInternal(FPackItemNo)

        If DsPackItems.Tables(0).Rows.Count() > 0 Then
            TblPackItems = DsPackItems.Tables(0)

            DRowPackItems = TblPackItems.Rows(0)


            DRowPackItems = DsPackItems.Tables(0).Rows(DsNextRow)
            FitemID = Long.Parse(DRowPackItems("ITEMID").ToString())
            FItemLength = Decimal.Parse(DRowPackItems("ITEMLENGTH").ToString())
            FItemColorID = Integer.Parse(DRowPackItems("ITEMCOLORID").ToString())

            If Len(DRowPackItems("ITEMCOLORID2").ToString()) > 0 Then
                FItemColorID2 = Integer.Parse(DRowPackItems("ITEMCOLORID2").ToString())
            End If

            FPackItemID = Long.Parse(DRowPackItems("PACKITEMID").ToString())

            If Len(DRowPackItems("PACKITEMBARS").ToString()) > 0 Then
                FItemPQty = Long.Parse(DRowPackItems("PACKITEMBARS").ToString())
            End If
            If Len(DRowPackItems("PACKITEMWEIGHT").ToString()) > 0 Then
                FItemSQty = Decimal.Parse(DRowPackItems("PACKITEMWEIGHT").ToString())
            End If

            If Len(DRowPackItems("ORDERDTLID").ToString()) > 0 Then
                OrderDtlID = Long.Parse(DRowPackItems("ORDERDTLID").ToString())
            End If

            If Len(DRowPackItems("RESRVTORETRANSID").ToString()) > 0 Then
                ResrvStoreTransID = Long.Parse(DRowPackItems("RESRVTORETRANSID").ToString())
            End If

            FItemPMunit = MyWmsBinTrans.FGetItemMUnitPrimary(FitemID)
            FItemSMunit = MyWmsBinTrans.FGetItemMUnitSecondary(FitemID)


            FTerminal = TerminalTrans
            FDBCreateUser = IAppUserTrans


            FTransEx = -1
            '=== [get dest palete info]
            DsPaletteInfoTo = MyWmsBinTrans.FGetBinPaletteInfo(CompID, BranchID, ThisBinCodeTo)

            If DsPaletteInfoTo.Tables(0).Rows.Count > 0 Then
                'PackItemNoTo()
                DrPaletteInfoTo = DsPaletteInfoTo.Tables(0).Rows(0)
                If Len(DrPaletteInfoTo("PACKITEMNO").ToString) > 0 Then
                    PackItemNoTo = DrPaletteInfoTo("PACKITEMNO").ToString
                End If
                If Len(DrPaletteInfoTo("PACKITEMID").ToString) > 0 Then
                    PackItemIDTo = DrPaletteInfoTo("PACKITEMID").ToString
                End If
            End If
            '=================
            'Check if BinTo is empty, if no check on merge if has same items otherwise Cancel move
            If MoveORMerge <> 3 Then
                ExBinToQty = MyWmsBinTrans.FGetBinITemQty(FBinIDTo, 0, 0, 0, 0, 0)
                If ExBinToQty > 0 Then
                    If MoveORMerge = 1 Then
                        Return "-20"
                    ElseIf MoveORMerge = 2 Then
                        ExBinToQty = MyWmsBinTrans.FGetBinITemQty(FBinIDTo, FitemID, FitemLotID, FItemLength, FItemColorID, FItemColorID2)

                        If ExBinToQty = 0 Then
                            Return "-21" 'not same items
                        End If
                    End If

                End If
            End If 'MoveORMerge <> 3
            'End BinTo CheckQTY
            '====================

            ' In Case that we set spcif qty
            If IntTransQty > 0 Then
                FItemPQty = IntTransQty
                FItemSQty = MyWmsBinTrans.FCalcItemWeightQty(FitemID, FItemLength, FItemPQty, CompID)
            End If

            BinTransID = Long.Parse(MyWmsBinTrans.FCreateWmsTrans(CompID, BranchID, FWmsTransType, NullVl, FBinIDFrom, FBinIDTo, FitemID, _
                                FitemLotID, FItemPQty, FItemPMunit, FItemSQty, FItemSMunit, _
                                FItemPackQty, FItemPackMunit, FTransEx, _
                                FStoreTransID, FStoreTRansDtlId, FDBCreateUser, FTerminal, _
                                FItemLength, FItemColorID, FItemColorID2, FPackItemID, FPackItemNo, PackItemIDTo, PackItemNoTo))

            If BinTransID > 0 Then InsCnt += 1

            DsNextRow += 1

        Else
            Return "-1"
        End If


        If InsCnt > 0 Then
            Dim UpdBinQtyExpRtrn, UpdBinQtyRtrn As Long

            If FBinIDFrom > 0 And FBinIDTo > 0 Then
                UpdBinQtyExpRtrn = MyWmsBinTrans.FUpdateBinQty(CompID, BranchID, -1, FPackItemNo, FBinIDFrom, FitemID, FitemLotID, FItemLength, FItemColorID, FItemColorID2, FItemPQty, FItemPMunit, FItemSQty, FItemSMunit, FItemPackQty, FItemPackMunit, FOrderID, OrderDtlID, FStoreTransID, FDBCreateUser, FTerminal)
                If UpdBinQtyExpRtrn = Nothing Or UpdBinQtyExpRtrn < 0 Then
                    MyWmsBinTrans.FCancelBinTrans(BinTransID)
                    Return "-10"
                End If

                If MoveORMerge = 2 Then
                    'Disable Palette from warehouse
                    MyWmsBinTrans.FSetPaletteOff(CompID, BranchID, FPackItemID, FPackItemNo)
                    UpdBinQtyRtrn = MyWmsBinTrans.FUpdateBinQty(CompID, BranchID, 1, PackItemNoTo, FBinIDTo, FitemID, FitemLotID, FItemLength, FItemColorID, FItemColorID2, FItemPQty, FItemPMunit, FItemSQty, FItemSMunit, FItemPackQty, FItemPackMunit, FOrderID, OrderDtlID, ResrvStoreTransID, FDBCreateUser, FTerminal)
                ElseIf MoveORMerge = 1 Or MoveORMerge = 3 Then
                    UpdBinQtyRtrn = MyWmsBinTrans.FUpdateBinQty(CompID, BranchID, 1, FPackItemNo, FBinIDTo, FitemID, FitemLotID, FItemLength, FItemColorID, FItemColorID2, FItemPQty, FItemPMunit, FItemSQty, FItemSMunit, FItemPackQty, FItemPackMunit, FOrderID, OrderDtlID, FStoreTransID, FDBCreateUser, FTerminal)
                    MyWmsBinTrans.FUpdatePaletteLocation(CompID, BranchID, FPackItemNo, ThisBinCodeTo, IsNewPalette)
                Else 'Trans
                    UpdBinQtyRtrn = MyWmsBinTrans.FUpdateBinQty(CompID, BranchID, 1, NullVal, FBinIDTo, FitemID, FitemLotID, FItemLength, FItemColorID, FItemColorID2, FItemPQty, FItemPMunit, FItemSQty, FItemSMunit, FItemPackQty, FItemPackMunit, FOrderID, OrderDtlID, FStoreTransID, FDBCreateUser, FTerminal)
                End If


                If UpdBinQtyRtrn > 0 Then
                    Return "1"
                ElseIf UpdBinQtyRtrn = -2 Then
                    MyWmsBinTrans.FCancelBinTrans(BinTransID)
                    Return "-2" 'trans succedded, NotEnouph Qty to be exported
                End If
            Else
                Return "-3" ' ERROR ON BINCODES
            End If
        Else
            Return "-1"  'trans failed
        End If
    End Function



    <WebMethod()> _
    Public Function SOA_FcreateInventoryTrans( _
            ByVal FCompID As Integer, _
            ByVal FBranchID As Integer, _
            ByVal FBinCode As String, _
            ByVal FInItemID As Long, _
            ByVal FitemCode As String, _
            ByVal FitemLotCode As String, _
            ByVal FItemPQty As Decimal, _
            ByVal FItemSQty As Decimal, _
            ByVal FItemPackQty As Decimal, _
            ByVal FDBCreateUser As Integer, _
            ByVal FTerminal As String, _
            ByVal FItemLength As Decimal, _
            ByVal FItemColorID As Integer, _
            ByVal FItemColorID2 As Integer, _
            ByVal FPackItemNo As String) As String

        Dim MyWmsBinTrans As WmsBinTrans = New WmsBinTrans
        Dim NewInventoryID As Long
        Dim FitemID As Long
        Dim FItemLotID As Long
        Dim FBinID As Long
        Dim FStoreID As Long
        Dim FPackItemID As Long

        Dim FItemPMunit As Integer
        Dim FItemSMunit As Integer
        Dim FItemPackMunit As Integer

        Dim SqlExRtr As Long

        Dim sqlstr As String
        'DEBUG

        Dim ThisItemSQty As Decimal

        NewInventoryID = 1

        NewInventoryID = MyWmsBinTrans.FGetNewInventoryID()

        FBinID = MyWmsBinTrans.FGetBinIDByCode(FCompID, FBranchID, FBinCode)

        FStoreID = MyWmsBinTrans.FGetBinStoreID(FBinID)

        FPackItemID = MyWmsBinTrans.FGetPackItemIDByNo(FPackItemNo)

        If FInItemID > 0 Then
            FitemID = FInItemID
        Else
            FitemID = MyWmsBinTrans.FGetItemIDByCode(FitemCode, FCompID)
        End If

        FItemPMunit = MyWmsBinTrans.FGetItemMUnitPrimary(FitemID)
        FItemSMunit = MyWmsBinTrans.FGetItemMUnitSecondary(FitemID)

        'FItemPackMunit= MyWmsBinTrans.????

        If Not FBinID > 0 Then
            Return "-3"  'invalid BinCode
        End If

        If Not FitemID > 0 Then
            Return "-11"   'invalid Item
        End If


        If FItemSQty > 0 Then
            ThisItemSQty = FItemSQty
        Else
            ThisItemSQty = MyWmsBinTrans.FCalcItemWeightQty(FitemID, FItemLength, FItemPQty, FCompID)
        End If
        sqlstr = "INSERT INTO TWMSINVENTORY (INVID,COMPID,BRANCHID,WHBINID,STOREID,"
        sqlstr = sqlstr & "ITEMID,ITEMLENGTH,ITEMCOLORID,ITEMCOLORID2,ITEMQTYPRIMARY,MUNITPRIMARY,"
        sqlstr = sqlstr & "ITEMQTYSECONDARY,MUNITSECONDARY,"
        sqlstr = sqlstr & "PACKITEMID,PACKITEMNO,"
        sqlstr = sqlstr & "INVDATE, APPLIEDTOSTORE,DBRECCREATEUSER,TERMINAL"
        sqlstr = sqlstr & ") VALUES ("

        If NewInventoryID > 0 Then
            sqlstr = sqlstr & NewInventoryID.ToString() & ","
        Else
            sqlstr = sqlstr & "NULL,"
        End If

        If FCompID > 0 Then
            sqlstr = sqlstr & FCompID.ToString() & ","
        Else
            sqlstr = sqlstr & "NULL,"
        End If
        If FBranchID > 0 Then
            sqlstr = sqlstr & FBranchID.ToString() & ","
        Else
            sqlstr = sqlstr & "NULL,"
        End If
        If FBinID > 0 Then
            sqlstr = sqlstr & FBinID.ToString() & ","
        Else
            sqlstr = sqlstr & "NULL,"
        End If
        If FStoreID > 0 Then
            sqlstr = sqlstr & FStoreID.ToString() & ","
        Else
            sqlstr = sqlstr & "NULL,"
        End If
        'FItemID
        If FitemID > -2 Then
            sqlstr = sqlstr & FitemID.ToString() & ","
        Else
            sqlstr = sqlstr & "NULL,"
        End If
        If FItemLength > 0 Then
            sqlstr = sqlstr & MyWmsBinTrans.f_dbinsertdecimal(FItemLength, 3) & ","
        Else
            sqlstr = sqlstr & "NULL,"
        End If
        If FItemColorID > 0 Then
            sqlstr = sqlstr & FItemColorID.ToString & ","
        Else
            sqlstr = sqlstr & "NULL,"
        End If

        If FItemColorID2 > 0 Then
            sqlstr = sqlstr & FItemColorID2.ToString & ","
        Else
            sqlstr = sqlstr & "NULL,"
        End If
        If FItemPQty > 0 Then
            sqlstr = sqlstr & MyWmsBinTrans.f_dbinsertdecimal(FItemPQty, 0) & ","
        Else
            sqlstr = sqlstr & "NULL,"
        End If


        If FItemPMunit > 0 Then
            sqlstr = sqlstr & FItemPMunit.ToString & ","
        Else
            sqlstr = sqlstr & "NULL,"
        End If

        If ThisItemSQty > 0 Then
            sqlstr = sqlstr & MyWmsBinTrans.f_dbinsertdecimal(ThisItemSQty, 0) & ","
        Else
            sqlstr = sqlstr & "NULL,"
        End If

        If FItemSMunit > 0 Then
            sqlstr = sqlstr & FItemSMunit.ToString & ","
        Else
            sqlstr = sqlstr & "NULL,"
        End If
        If FPackItemID > 0 Then
            sqlstr = sqlstr & FPackItemID.ToString & ","
        Else
            sqlstr = sqlstr & "NULL,"
        End If
        If Len(FPackItemNo) > 0 Then
            sqlstr = sqlstr & "'" & FPackItemNo & "',"
        Else
            sqlstr = sqlstr & "NULL,"
        End If

        sqlstr = sqlstr & "SYSDATE" & ","
        '====APPLIED====
        sqlstr = sqlstr & "0,"
        '==============
        If FDBCreateUser > 0 Then
            sqlstr = sqlstr & FDBCreateUser.ToString & ","
        Else
            sqlstr = sqlstr & "NULL,"
        End If

        If Len(FTerminal) > 0 Then
            sqlstr = sqlstr & "'" & FTerminal & "'"
        Else
            sqlstr = sqlstr & "NULL"
        End If

        sqlstr = sqlstr & ")"

        'DEBUG
        'MyWmsDBTrans.f_sqlerrorlog(FCompID, "SOA_FcreateInventoryTrans>>", sqlstr, "Admin")

        SqlExRtr = MyWmsDBTrans.OraDBExecuteSQLCmd(sqlstr, 1)

        If SqlExRtr > 0 Then
            'delete current BinIemQty
            'Insert New Bin Iem Qty
            ' Update Palette BinCode
            ' MyWmsDBTrans.FInventoryUpdateBinQty

            MyWmsBinTrans.FInventoryUpdateBinQty(FCompID, FBranchID, FPackItemNo, FBinID, FitemID, 0, FItemLength, FItemColorID, FItemColorID2, FItemPQty, FItemPMunit, ThisItemSQty, FItemSMunit, FItemPackQty, FItemPackMunit, FDBCreateUser, FTerminal)

            Return NewInventoryID.ToString()
        Else
            'DEBUG
            If sqlstr = Nothing Then sqlstr = "Sqlstr empty!"
            MyWmsDBTrans.f_sqlerrorlog(FCompID, "SOA_FcreateInventoryTrans>>", sqlstr, "Admin")

            Return "-1"
        End If

    End Function
#End Region
    '///////////////////////////////////////////
    '///   Picking                //////////////
    '///////////////////////////////////////////
#Region "Orders Picking "
    <WebMethod(Description:="Picking::Orders Picking List")> _
    Public Function SOA_FGetOrdersPickingList(ByVal CompID As Integer, ByVal BranchID As Integer, ByVal RouteID As Long) As DataSet
        Dim SqlStr As String
        Dim Ds As New DataSet
        Dim I As Integer
        Dim DTColumn As DataColumn

        SqlStr = "SELECT DISTINCT NVL(WMSDESTINATION,'...') AS WMSDESTINATION,STORETRANSID,ORDERCODE,SALESORDERID,NVL(CUSTOMERTITLE,'...') AS CUSTOMERTITLE,ORDERDATE,PROMISEDDELIVDATE,TO_CHAR( PROMISEDDELIVDATE,'DD/MM/YY') AS PROMISDELDATECHR  FROM VWMSORDERSWMSTASKSQTY "
        SqlStr = SqlStr & " WHERE ORDERITEMQTYBALANCE > 0 "
        If CompID > 0 Then
            SqlStr = SqlStr & "AND COMPID = " & CompID.ToString
            If BranchID > 0 Then
                SqlStr = SqlStr & " AND BRANCHID=" & BranchID.ToString
            End If
        End If

        If RouteID > 0 Then
            SqlStr = SqlStr & " AND ROUTEID=" & RouteID.ToString
        End If

        SqlStr = SqlStr & " ORDER BY WMSDESTINATION,PROMISEDDELIVDATE  "


        If Len(SqlStr) > 0 Then
            Ds = MyWmsDBTrans.OraDBFillDataset(SqlStr, "ORDERSPICKINGLIST")


            If Ds.Tables(0).Rows.Count > 0 Then
                DTColumn = New DataColumn

                DTColumn.DataType = System.Type.GetType("System.Int32")
                DTColumn.ColumnName = "ID"
                DTColumn.AutoIncrement = False
                Ds.Tables(0).Columns.Add(DTColumn)

                For I = 0 To Ds.Tables(0).Rows.Count - 1
                    Ds.Tables(0).Rows(I)("ID") = I + 1
                Next
            End If

            Return Ds
        Else
            SOA_FGetOrdersPickingList = Nothing
        End If

    End Function

    <WebMethod(Description:="Picking")> _
    Public Function SOA_FGetRsrvStoreTransInfo(ByVal RsrvStoreTransID As Long) As DataSet
        Dim SqlStr As String
        Dim Ds As New DataSet
        If RsrvStoreTransID > 0 Then


            SqlStr = " SELECT VWMSORDERITEMSBINLOCATIONS.WHCODE, VWMSORDERITEMSBINLOCATIONS.WHZONECODE,VWMSORDERITEMSBINLOCATIONS.CUSTOMERTITLE,VWMSORDERITEMSBINLOCATIONS.ORDERCODE,VWMSORDERITEMSBINLOCATIONS.ITEMCODE,"
            SqlStr = SqlStr & "VWMSORDERITEMSBINLOCATIONS.ITEMDESC,VWMSORDERITEMSBINLOCATIONS.ITEMLENGTH,VWMSORDERITEMSBINLOCATIONS.PROMISEDDELIVDATE,TO_CHAR( VWMSORDERITEMSBINLOCATIONS.PROMISEDDELIVDATE,'DD/MM/YY') AS PROMISDELDATECHR,"
            SqlStr = SqlStr & "VWMSORDERITEMSBINLOCATIONS.COLORCODE,VWMSORDERITEMSBINLOCATIONS.COLORDESC, SUM(VWMSORDERITEMSBINLOCATIONS.ORDERITEMQTYBALANCE) AS ORDERITEMQTYBALANCE,SUM(VWMSORDERITEMSBINLOCATIONS.BINITEMQTYPRIMARY) AS BINITEMQTYPRIMARY, COUNT(VWMSORDERITEMSBINLOCATIONS.WHBINID) AS BINCNT "
            SqlStr = SqlStr & "FROM VWMSORDERITEMSBINLOCATIONS "
            SqlStr = SqlStr & "WHERE VWMSORDERITEMSBINLOCATIONS.STORETRANSID=" & RsrvStoreTransID.ToString & " "
            SqlStr = SqlStr & " GROUP BY "
            SqlStr = SqlStr & "VWMSORDERITEMSBINLOCATIONS.WHCODE,"
            SqlStr = SqlStr & "VWMSORDERITEMSBINLOCATIONS.WHZONECODE,VWMSORDERITEMSBINLOCATIONS.CUSTOMERTITLE,VWMSORDERITEMSBINLOCATIONS.ORDERCODE,"
            SqlStr = SqlStr & "VWMSORDERITEMSBINLOCATIONS.ITEMCODE,VWMSORDERITEMSBINLOCATIONS.ITEMDESC,"
            SqlStr = SqlStr & "VWMSORDERITEMSBINLOCATIONS.ITEMLENGTH,VWMSORDERITEMSBINLOCATIONS.PROMISEDDELIVDATE,"
            SqlStr = SqlStr & "VWMSORDERITEMSBINLOCATIONS.COLORCODE,VWMSORDERITEMSBINLOCATIONS.COLORDESC"



            Ds = MyWmsDBTrans.OraDBFillDataset(SqlStr, "ORDERSPICKINGITEMS")
        Else
            Ds = Nothing
        End If
        Return Ds
    End Function
    <WebMethod(Description:="Picking")> _
      Public Function SOA_FGetZonesWithOrdersItems(ByVal CompID As Integer, ByVal BranchID As Integer, ByVal WhZoneCode As String) As DataSet
        Dim SqlStr As String
        Dim Ds As New DataSet


        SqlStr = " SELECT VWMSORDERITEMSBINLOCATIONS.WHCODE, VWMSORDERITEMSBINLOCATIONS.WHZONECODE,VWMSORDERITEMSBINLOCATIONS.CUSTOMERTITLE,VWMSORDERITEMSBINLOCATIONS.ORDERCODE,VWMSORDERITEMSBINLOCATIONS.ITEMCODE,"
        SqlStr = SqlStr & "VWMSORDERITEMSBINLOCATIONS.ITEMDESC,VWMSORDERITEMSBINLOCATIONS.ITEMLENGTH,VWMSORDERITEMSBINLOCATIONS.PROMISEDDELIVDATE,TO_CHAR( VWMSORDERITEMSBINLOCATIONS.PROMISEDDELIVDATE,'DD/MM/YY') AS PROMISDELDATECHR,"
        SqlStr = SqlStr & "VWMSORDERITEMSBINLOCATIONS.COLORCODE,VWMSORDERITEMSBINLOCATIONS.COLORDESC, SUM(VWMSORDERITEMSBINLOCATIONS.ORDERITEMQTYBALANCE) AS ORDERITEMQTYBALANCE,SUM(VWMSORDERITEMSBINLOCATIONS.BINITEMQTYPRIMARY) AS BINITEMQTYPRIMARY, COUNT(VWMSORDERITEMSBINLOCATIONS.WHBINID) AS BINCNT "
        SqlStr = SqlStr & "FROM VWMSORDERITEMSBINLOCATIONS "

        If CompID > 0 Then
            SqlStr = SqlStr & "WHERE VWMSORDERITEMSBINLOCATIONS.COMPID=" & CompID.ToString
            If BranchID > 0 Then
                SqlStr = SqlStr & " AND VWMSORDERITEMSBINLOCATIONS.BRANCHID=" & BranchID.ToString
            End If
        ElseIf Len(WhZoneCode) > 0 Then
            SqlStr = SqlStr & "WHERE VWMSORDERITEMSBINLOCATIONS.WHZONECODE='" & WhZoneCode & "' "
        End If

        If Len(WhZoneCode) > 0 Then
            If CompID > 0 Then
                SqlStr = SqlStr & " AND "
            Else
                SqlStr = SqlStr & " WHERE "
            End If
            SqlStr = SqlStr & "VWMSORDERITEMSBINLOCATIONS.WHZONECODE='" & WhZoneCode & "' "
        End If

        SqlStr = SqlStr & "GROUP BY "
        SqlStr = SqlStr & "VWMSORDERITEMSBINLOCATIONS.WHCODE,"
        SqlStr = SqlStr & "VWMSORDERITEMSBINLOCATIONS.WHZONECODE,VWMSORDERITEMSBINLOCATIONS.CUSTOMERTITLE,VWMSORDERITEMSBINLOCATIONS.ORDERCODE,"
        SqlStr = SqlStr & "VWMSORDERITEMSBINLOCATIONS.ITEMCODE,VWMSORDERITEMSBINLOCATIONS.ITEMDESC,"
        SqlStr = SqlStr & "VWMSORDERITEMSBINLOCATIONS.ITEMLENGTH,VWMSORDERITEMSBINLOCATIONS.PROMISEDDELIVDATE,"
        SqlStr = SqlStr & "VWMSORDERITEMSBINLOCATIONS.COLORCODE,VWMSORDERITEMSBINLOCATIONS.COLORDESC"



        Ds = MyWmsDBTrans.OraDBFillDataset(SqlStr, "ORDERSPICKINGITEMS")

        Return Ds
    End Function
    <WebMethod(Description:="Picking")> _
     Public Function SOA_FGetWholePaletsWithOrdersItems(ByVal CompID As Integer, ByVal BranchID As Integer) As DataSet
        Dim SqlStr As String
        Dim Ds As New DataSet
        Dim Dsr As New DataSet
        Dim DT As DataTable
        Dim DTr As New DataTable
        Dim DV As DataView
        Dim Dr As DataRow

        Dim WhCode, WhZoneCode, WhBinCode As String
        Dim ItemCode, ItemDesc As String
        Dim ColorCode, ColorDesc As String
        Dim ColorCode2, ColorDesc2 As String
        Dim ItemID, ItemIDTmp As Long
        Dim ItemColorID As Integer
        Dim ItemColorID2 As Integer
        Dim ItemLength As Decimal
        Dim BinItemQty, OrdersBalanceQty As Decimal
        Dim ItemSetQty, ItemSetQtyBalance As Decimal

        Dim i, j As Integer

        SqlStr = " SELECT VWMSTASKSITEMBINLOCATIONS.WHCODE, VWMSTASKSITEMBINLOCATIONS.WHZONECODE,VWMSTASKSITEMBINLOCATIONS.WHBINID,VWMSTASKSITEMBINLOCATIONS.WHBINCODE,"
        SqlStr = SqlStr & "VWMSTASKSITEMBINLOCATIONS.ITEMID, VWMSTASKSITEMBINLOCATIONS.ITEMCODE,"
        SqlStr = SqlStr & "VWMSTASKSITEMBINLOCATIONS.ITEMDESC,VWMSTASKSITEMBINLOCATIONS.ITEMLENGTH,"
        SqlStr = SqlStr & "VWMSTASKSITEMBINLOCATIONS.ITEMCOLORID,VWMSTASKSITEMBINLOCATIONS.ITEMCOLORID2,VWMSTASKSITEMBINLOCATIONS.COLORCODE,VWMSTASKSITEMBINLOCATIONS.COLORDESC,VWMSTASKSITEMBINLOCATIONS.ORDERITEMQTYBALANCE,"
        SqlStr = SqlStr & "VWMSTASKSITEMBINLOCATIONS.BINITEMQTYPRIMARY "
        SqlStr = SqlStr & "FROM VWMSTASKSITEMBINLOCATIONS  "

        SqlStr = SqlStr & " WHERE VWMSTASKSITEMBINLOCATIONS.BINITEMQTYPRIMARY <= VWMSTASKSITEMBINLOCATIONS.ORDERITEMQTYBALANCE "

        If CompID > 0 Then
            SqlStr = SqlStr & " AND  VWMSTASKSITEMBINLOCATIONS.COMPID=" & CompID.ToString

            If BranchID > 0 Then
                SqlStr = SqlStr & " AND VWMSTASKSITEMBINLOCATIONS.BRANCHID=" & BranchID.ToString
            End If

        End If
        SqlStr = SqlStr & " ORDER BY VWMSTASKSITEMBINLOCATIONS.ITEMID,VWMSTASKSITEMBINLOCATIONS.BINITEMQTYPRIMARY DESC"

        Ds = MyWmsDBTrans.OraDBFillDataset(SqlStr, "ORDERSPICKINGITEMS")

        If Ds.Tables(0).Rows.Count > 0 Then


            ItemSetQty = 0
            ItemSetQtyBalance = 0
            ItemIDTmp = 0

            For Each DT In Ds.Tables
                DTr = DT.Clone
                'DTr.Rows.Clear()
                i = 0
                j = DT.Rows.Count
                For Each Dr In DT.Rows

                    'Dr = Ds.Tables(0).Rows(i)

                    If Len(DT.Rows(i).Item("ORDERITEMQTYBALANCE").ToString()) And DT.Rows(i).Item("ORDERITEMQTYBALANCE").ToString() <> "NULL" Then
                        OrdersBalanceQty = Decimal.Parse(DT.Rows(i).Item("ORDERITEMQTYBALANCE").ToString())
                    End If

                    If Len(DT.Rows(i).Item("BINITEMQTYPRIMARY").ToString()) And DT.Rows(i).Item("BINITEMQTYPRIMARY").ToString() <> "NULL" Then
                        BinItemQty = Decimal.Parse(DT.Rows(i).Item("BINITEMQTYPRIMARY").ToString())
                    End If

                    ItemID = Long.Parse(DT.Rows(i).Item("ITEMID").ToString())

                    If ItemID <> ItemIDTmp Then
                        ItemIDTmp = ItemID
                        DTr.ImportRow(Dr)
                        'DTr.Rows.Add(Dr)
                        ItemSetQtyBalance = OrdersBalanceQty - BinItemQty
                    ElseIf ItemIDTmp = ItemID Then
                        If ItemSetQtyBalance > 0 And ItemSetQtyBalance >= BinItemQty Then
                            'DTr.Rows.Add(Dr)
                            Dr.Item("ITEMCODE") = " '' "
                            DTr.ImportRow(Dr)
                            ItemSetQtyBalance = ItemSetQtyBalance - BinItemQty

                        End If

                    End If

                    i = i + 1
                Next
            Next

        Else
            DTr = Ds.Tables(0).Clone
        End If
        Dsr.Tables.Add(DTr)
        Dsr.DataSetName = "ORDERSPICKINGITEMS"

        Return Dsr
    End Function
    <WebMethod(Description:="Picking")> _
    Public Function SOA_FGetZoneRoutePicking(ByVal CompID As Integer, ByVal BranchID As Integer) As DataSet
        Dim SqlStr As String
        Dim Ds As New DataSet
        Dim Dsr As New DataSet
        Dim DT As DataTable
        Dim DTr As DataTable
        Dim DV As DataView
        Dim Dr As DataRow

        Dim WhCode, WhZoneCode, WhBinCode As String
        Dim ItemCode, ItemDesc As String
        Dim ColorCode, ColorDesc As String
        Dim ColorCode2, ColorDesc2 As String
        Dim ItemID, ItemIDTmp As Long
        Dim ItemColorID As Integer
        Dim ItemColorID2 As Integer
        Dim ItemLength As Decimal
        Dim BinItemQty, OrdersBalanceQty As Decimal
        Dim ItemSetQty, ItemSetQtyBalance As Decimal

        Dim i, j As Integer

        SqlStr = " SELECT VWMSTASKSITEMBINLOCATIONS.WHCODE, VWMSTASKSITEMBINLOCATIONS.WHZONECODE,VWMSTASKSITEMBINLOCATIONS.WHBINCODE,"
        SqlStr = SqlStr & "VWMSTASKSITEMBINLOCATIONS.ITEMID, VWMSTASKSITEMBINLOCATIONS.ITEMCODE,"
        SqlStr = SqlStr & "VWMSTASKSITEMBINLOCATIONS.ITEMDESC,VWMSTASKSITEMBINLOCATIONS.ITEMLENGTH,"
        SqlStr = SqlStr & "VWMSTASKSITEMBINLOCATIONS.COLORCODE,VWMSTASKSITEMBINLOCATIONS.COLORDESC,VWMSTASKSITEMBINLOCATIONS.ORDERITEMQTYBALANCE,"
        'SqlStr = SqlStr & "MAX(VWMSTASKSITEMBINLOCATIONS.BINITEMQTYPRIMARY) AS BINITEMQTYPRIMARY "
        SqlStr = SqlStr & "VWMSTASKSITEMBINLOCATIONS.BINITEMQTYPRIMARY "
        SqlStr = SqlStr & "FROM VWMSTASKSITEMBINLOCATIONS  "

        SqlStr = SqlStr & " WHERE VWMSTASKSITEMBINLOCATIONS.BINITEMQTYPRIMARY <= VWMSTASKSITEMBINLOCATIONS.ORDERITEMQTYBALANCE "

        If CompID > 0 Then
            SqlStr = SqlStr & " AND  VWMSTASKSITEMBINLOCATIONS.COMPID=" & CompID.ToString
            If BranchID > 0 Then
                SqlStr = SqlStr & " AND VWMSTASKSITEMBINLOCATIONS.BRANCHID=" & BranchID.ToString
            End If

        End If
        'SqlStr = SqlStr & " GROUP BY VWMSTASKSITEMBINLOCATIONS.WHCODE, VWMSTASKSITEMBINLOCATIONS.WHZONECODE,VWMSTASKSITEMBINLOCATIONS.ITEMCODE,"
        'SqlStr = SqlStr & "VWMSTASKSITEMBINLOCATIONS.ITEMDESC,VWMSTASKSITEMBINLOCATIONS.ITEMLENGTH,"
        'SqlStr = SqlStr & "VWMSTASKSITEMBINLOCATIONS.COLORCODE,VWMSTASKSITEMBINLOCATIONS.COLORDESC,VWMSTASKSITEMBINLOCATIONS.ORDERITEMQTYBALANCE "
        SqlStr = SqlStr & " ORDER BY VWMSTASKSITEMBINLOCATIONS.ITEMID,VWMSTASKSITEMBINLOCATIONS.BINITEMQTYPRIMARY DESC"

        Ds = MyWmsDBTrans.OraDBFillDataset(SqlStr, "ORDERSPICKINGITEMS")

        'Dr = CType(Ds.Tables(0).Rows(0), DataRow)
        'DT.Rows.Add(Dr)

        If Ds.Tables(0).Rows.Count > 0 Then
            'DT = Ds.Tables(0)

            ItemSetQty = 0
            ItemSetQtyBalance = 0
            ItemIDTmp = 0

            For Each DT In Ds.Tables
                DTr = DT.Clone
                'DTr.Rows.Clear()
                i = 0
                j = DT.Rows.Count
                For Each Dr In DT.Rows
                    'Dr = Ds.Tables(0).Rows(i)
                    If Len(DT.Rows(i).Item("ORDERITEMQTYBALANCE").ToString()) And DT.Rows(i).Item("ORDERITEMQTYBALANCE").ToString() <> "NULL" Then
                        OrdersBalanceQty = Decimal.Parse(DT.Rows(i).Item("ORDERITEMQTYBALANCE").ToString())
                    End If

                    If Len(DT.Rows(i).Item("BINITEMQTYPRIMARY").ToString()) And DT.Rows(i).Item("BINITEMQTYPRIMARY").ToString() <> "NULL" Then
                        BinItemQty = Decimal.Parse(DT.Rows(i).Item("BINITEMQTYPRIMARY").ToString())
                    End If

                    ItemID = Long.Parse(DT.Rows(i).Item("ITEMID").ToString())

                    If ItemID <> ItemIDTmp Then
                        ItemIDTmp = ItemID
                        DTr.ImportRow(Dr)
                        'DTr.Rows.Add(Dr)
                        ItemSetQtyBalance = OrdersBalanceQty - BinItemQty
                    ElseIf ItemIDTmp = ItemID Then
                        If ItemSetQtyBalance > 0 And ItemSetQtyBalance >= BinItemQty Then
                            'DTr.Rows.Add(Dr)
                            Dr.Item("ITEMCODE") = " '' "
                            DTr.ImportRow(Dr)
                            ItemSetQtyBalance = ItemSetQtyBalance - BinItemQty
                        End If
                    End If
                    i = i + 1
                Next
            Next
            Dsr.DataSetName = "ORDERSPICKINGITEMS"
            Dsr.Tables.Add(DTr)
        End If
        Return Dsr
    End Function
    <WebMethod(Description:="Picking")> _
    Public Function SOA_FCreateGroupOrdersPickingRoute(ByVal CompID As Integer, ByVal BranchID As Integer, ByVal WhBuildingID As Integer, ByVal RouteID As Long, ByVal RsrvStoreTransID As Long, ByVal OrderID As Long) As DataSet
        Dim MyWmsDBTrans As New WmsDBTrans
        Dim MyWmsBinTrans As New WmsBinTrans

        Dim RouteDs As New DataSet
        Dim ZonesMaxLoadDs As New DataSet
        Dim OrderItemsDs As New DataSet
        Dim ItemsInZoneDs As New DataSet

        Dim OrderTransDs As New DataSet
        Dim OrderItemsLocDs As New DataSet
        Dim OrderItemsLocDT As New DataTable

        Dim ZoneCandDT As New DataTable("DTORDERPICKZONES")
        Dim ZoneCandDR, ItemZoneDR As DataRow
        Dim ZoneMaxDr As DataRow
        Dim DTColumn As DataColumn

        Dim DrOrderItems As DataRow

        Dim ItemID, ItemIDTmp, ZoneItemID As Long
        Dim ZoneCodeTmp As String
        Dim ItemColorID, ItemColorIDTmp, ZoneItemColorID As Integer
        Dim ItemColorID2, ItemColorID2Tmp, ZoneItemColorID2 As Integer
        Dim ItemLength, ItemLengthTmp, ZoneItemLength As Decimal

        Dim ThisRouteID, ThisOrderID, ThisStoreTransID As Long


        Dim ItemSetQty, ItemSetQtyBalance, OrdersBalanceQty As Decimal

        Dim i, j, k As Integer
        Dim HMBinPerItem As Integer = 0 'How many bins per each order item

        Dim ItemCode, ColorDesc, FrBinCode, ZoneCode, ZonecodeMax, BinCode As String

        Dim ItemPQty, ItemZonePQty, ItemBalanceQty As Decimal
        Dim ItemCnt As Integer
        Dim SqlStr As String

        Dim IItemStart, IItemEnd As Integer

        'Dim GroupOrders As Boolean

        '//create data table stucture
        'ZoneCandDT


        DTColumn = New DataColumn
        DTColumn.DataType = System.Type.GetType("System.Int32")
        DTColumn.ColumnName = "ID"
        DTColumn.AutoIncrement = True
        DTColumn.Unique = True
        ZoneCandDT.Columns.Add(DTColumn)
        'zone
        DTColumn = New DataColumn
        DTColumn.DataType = System.Type.GetType("System.String")
        DTColumn.ColumnName = "WHZONECODE"
        DTColumn.AutoIncrement = False
        DTColumn.Unique = False
        ZoneCandDT.Columns.Add(DTColumn)
        'zone
        DTColumn = New DataColumn
        DTColumn.DataType = System.Type.GetType("System.String")
        DTColumn.ColumnName = "WHBINCODE"
        DTColumn.AutoIncrement = False
        DTColumn.Unique = False
        ZoneCandDT.Columns.Add(DTColumn)
        '
        DTColumn = New DataColumn
        DTColumn.DataType = System.Type.GetType("System.Int32")
        DTColumn.ColumnName = "ROUTEID"
        DTColumn.AutoIncrement = False
        DTColumn.Unique = False
        ZoneCandDT.Columns.Add(DTColumn)
        '
        DTColumn = New DataColumn
        DTColumn.DataType = System.Type.GetType("System.Int32")
        DTColumn.ColumnName = "STORETRANSID"
        DTColumn.AutoIncrement = False
        DTColumn.Unique = False
        ZoneCandDT.Columns.Add(DTColumn)
        DTColumn = New DataColumn
        DTColumn.DataType = System.Type.GetType("System.Int32")
        DTColumn.ColumnName = "ORDERID"
        DTColumn.AutoIncrement = False
        DTColumn.Unique = False
        ZoneCandDT.Columns.Add(DTColumn)
        'itemid
        DTColumn = New DataColumn
        DTColumn.DataType = System.Type.GetType("System.Int32")
        DTColumn.ColumnName = "ITEMID"
        DTColumn.AutoIncrement = False
        DTColumn.Unique = False
        ZoneCandDT.Columns.Add(DTColumn)
        'itemcode
        DTColumn = New DataColumn
        DTColumn.DataType = System.Type.GetType("System.String")
        DTColumn.ColumnName = "ITEMCODE"
        DTColumn.AutoIncrement = False
        DTColumn.Unique = False
        ZoneCandDT.Columns.Add(DTColumn)
        '
        DTColumn = New DataColumn
        DTColumn.DataType = System.Type.GetType("System.Decimal")
        DTColumn.ColumnName = "ITEMLENGTH"
        DTColumn.AutoIncrement = False
        DTColumn.Unique = False
        ZoneCandDT.Columns.Add(DTColumn)
        '
        DTColumn = New DataColumn
        DTColumn.DataType = System.Type.GetType("System.Int16")
        DTColumn.ColumnName = "ITEMCOLORID"
        DTColumn.AutoIncrement = False
        DTColumn.Unique = False
        ZoneCandDT.Columns.Add(DTColumn)
        '
        DTColumn = New DataColumn
        DTColumn.DataType = System.Type.GetType("System.Int16")
        DTColumn.ColumnName = "ITEMCOLORID2"
        DTColumn.AutoIncrement = False
        DTColumn.Unique = False
        ZoneCandDT.Columns.Add(DTColumn)
        '
        DTColumn = New DataColumn
        DTColumn.DataType = System.Type.GetType("System.String")
        DTColumn.ColumnName = "COLORDESC"
        DTColumn.AutoIncrement = False
        DTColumn.Unique = False
        ZoneCandDT.Columns.Add(DTColumn)
        'qty
        'PrimaryQty
        DTColumn = New DataColumn
        DTColumn.DataType = System.Type.GetType("System.Decimal")
        DTColumn.ColumnName = "ZONEAVAILABLEQTY"
        DTColumn.AutoIncrement = False
        DTColumn.Unique = False
        ZoneCandDT.Columns.Add(DTColumn)
        'OrderItemQty
        DTColumn = New DataColumn
        DTColumn.DataType = System.Type.GetType("System.Decimal")
        DTColumn.ColumnName = "ORDERSQTY"
        DTColumn.AutoIncrement = False
        DTColumn.Unique = False
        ZoneCandDT.Columns.Add(DTColumn)
        'setItemQty
        DTColumn = New DataColumn
        DTColumn.DataType = System.Type.GetType("System.Decimal")
        DTColumn.ColumnName = "ITEMSETQTY"
        DTColumn.AutoIncrement = False
        DTColumn.Unique = False
        ZoneCandDT.Columns.Add(DTColumn)
        'ItemBalanceQty
        DTColumn = New DataColumn
        DTColumn.DataType = System.Type.GetType("System.Decimal")
        DTColumn.ColumnName = "ITEMBALANCEQTY"
        DTColumn.AutoIncrement = False
        DTColumn.Unique = False
        ZoneCandDT.Columns.Add(DTColumn)

        'ItemChoices

        '////////////////////////////////////////////
        '////Get  Distinct Items with Orders Qty  //
        '////////////////////////////////////////////
        'GroupOrders = False

        If RsrvStoreTransID > 0 Then
            SqlStr = "SELECT COMPID,BRANCHID,ROUTEID,WMSDESTINATION,STORETRANSID,SALESORDERID AS ORDERID,SALESORDERDTL,ITEMID,ITEMCODE,ITEMCOLORID,ITEMCOLORID2,COLORCODE,COLORDESC,COLORCODE2,COLORDESC2,ITEMLENGTH,"
            SqlStr = SqlStr & " SUM(ORDERITEMQTYBALANCE) AS ORDERITEMQTYBALANCE "
            SqlStr = SqlStr & " FROM VWMSORDERSWMSTASKSQTY "
            SqlStr = SqlStr & " WHERE STORETRANSID = " & RsrvStoreTransID.ToString
            If CompID > 0 Then
                SqlStr = SqlStr & " AND  COMPID=" & CompID.ToString
                If BranchID > 0 Then
                    SqlStr = SqlStr & " AND BRANCHID=" & BranchID.ToString
                End If
            End If
            SqlStr = SqlStr & " GROUP BY COMPID,BRANCHID,ROUTEID,WMSDESTINATION,STORETRANSID,SALESORDERID,SALESORDERDTL,ITEMID,ITEMCODE,ITEMCOLORID,ITEMCOLORID2,COLORCODE,COLORDESC,COLORCODE2,COLORDESC2,ITEMLENGTH"

        ElseIf OrderID > 0 Then
            SqlStr = "SELECT COMPID,BRANCHID,ROUTEID,WMSDESTINATION,STORETRANSID,SALESORDERID AS ORDERID,SALESORDERDTL,ITEMID,ITEMCODE,ITEMCOLORID,ITEMCOLORID2,COLORCODE,COLORDESC,COLORCODE2,COLORDESC2,ITEMLENGTH,"
            SqlStr = SqlStr & " SUM(ORDERITEMQTYBALANCE) AS ORDERITEMQTYBALANCE "
            SqlStr = SqlStr & " FROM VWMSORDERSWMSTASKSQTY "
            SqlStr = SqlStr & " WHERE SALESORDERID = " & OrderID.ToString
            If CompID > 0 Then
                SqlStr = SqlStr & " AND  COMPID=" & CompID.ToString
                If BranchID > 0 Then
                    SqlStr = SqlStr & " AND BRANCHID=" & BranchID.ToString
                End If
            End If
            SqlStr = SqlStr & " GROUP BY COMPID,BRANCHID,ROUTEID,WMSDESTINATION,STORETRANSID,SALESORDERID,SALESORDERDTL,ITEMID,ITEMCODE,ITEMCOLORID,ITEMCOLORID2,COLORCODE,COLORDESC,COLORCODE2,COLORDESC2,ITEMLENGTH"
        ElseIf RouteID > 0 Then
            SqlStr = "SELECT COMPID,BRANCHID,ROUTEID,WMSDESTINATION,STORETRANSID,SALESORDERID AS ORDERID,SALESORDERDTL,ITEMID,ITEMCODE,ITEMCOLORID,ITEMCOLORID2,COLORCODE,COLORDESC,COLORCODE2,COLORDESC2,ITEMLENGTH, "
            SqlStr = SqlStr & " SUM(ORDERITEMQTYBALANCE) AS ORDERITEMQTYBALANCE "
            SqlStr = SqlStr & " FROM VWMSORDERSWMSTASKSQTY "
            SqlStr = SqlStr & " WHERE ROUTEID = " & RouteID.ToString
            If CompID > 0 Then
                SqlStr = SqlStr & " AND  COMPID=" & CompID.ToString
                If BranchID > 0 Then
                    SqlStr = SqlStr & " AND BRANCHID=" & BranchID.ToString
                End If
            End If
            SqlStr = SqlStr & " GROUP BY COMPID,BRANCHID,ROUTEID,WMSDESTINATION,STORETRANSID,SALESORDERID,SALESORDERDTL,ITEMID,ITEMCODE,ITEMCOLORID,ITEMCOLORID2,COLORCODE,COLORDESC,COLORCODE2,COLORDESC2,ITEMLENGTH"
        Else 'all
            'GroupOrders = True
            SqlStr = "SELECT COMPID,BRANCHID,ITEMID,ITEMCODE,ITEMCOLORID,ITEMCOLORID2,COLORCODE,COLORDESC,COLORCODE2,COLORDESC2,ITEMLENGTH,ORDERITEMQTYBALANCE FROM VWMSORDERSWMSTASKSBYITEMID "
            If CompID > 0 Then
                SqlStr = SqlStr & " WHERE  COMPID=" & CompID.ToString
                If BranchID > 0 Then
                    SqlStr = SqlStr & " AND BRANCHID=" & BranchID.ToString
                End If
            End If
            SqlStr = SqlStr & "ORDER BY ORDERITEMQTYBALANCE DESC "
        End If
        OrderItemsDs = MyWmsDBTrans.OraDBFillDataset(SqlStr, "ORDERITEMSQTY")
        '=====================================
        ' Get zones with more th
        'MAY YO HAVE TOCHANGE THE OTHER QUERY, CREATE ANOTHER OE WITHDISTINCT ITEMS
        '//////////////////////////
        '//Get Order ItmsQty
        '//////////////////////////

        ItemsInZoneDs = MyWmsBinTrans.FGetOrderItemsInZones(CompID, BranchID, WhBuildingID)
        '////////////////////////////////
        '// Get Zones with max load   ///
        '////////////////////////////////

        ZonesMaxLoadDs = MyWmsBinTrans.FGetZoneswithMaxPickload(CompID, BranchID, WhBuildingID)
        '//////////////////////////////////////
        '// Set Item,Zone, setqty, new scenario
        '//////////////////////////////////////
        ItemIDTmp = 0
        ItemColorIDTmp = 0
        ItemColorID2Tmp = 0
        ItemLengthTmp = 0
        ItemCnt = 0
        'OrderItemsDs
        'ItemsInZoneDs

        OrdersBalanceQty = 0
        For i = 0 To OrderItemsDs.Tables(0).Rows.Count - 1
            DrOrderItems = OrderItemsDs.Tables(0).Rows(i)

            ItemID = Long.Parse(DrOrderItems("ITEMID").ToString)
            ItemLength = Decimal.Parse(DrOrderItems("ITEMLENGTH").ToString)
            ItemColorID = Integer.Parse(DrOrderItems("ITEMCOLORID").ToString)
            ItemColorID2 = Integer.Parse(DrOrderItems("ITEMCOLORID2").ToString)

            ItemCode = DrOrderItems("ITEMCODE").ToString
            ColorDesc = DrOrderItems("COLORDESC").ToString

            Try
                If RouteID > 0 Or OrderID > 0 Or RsrvStoreTransID > 0 Then
                    ThisRouteID = Long.Parse(DrOrderItems("ROUTEID").ToString)
                    ThisOrderID = Long.Parse(DrOrderItems("ORDERID").ToString)
                    ThisStoreTransID = Long.Parse(DrOrderItems("STORETRANSID").ToString)
                End If
            Catch ex As Exception

            End Try



            If ItemID <> ItemIDTmp Or ItemColorID <> ItemColorIDTmp Or ItemColorID2 <> ItemColorID2Tmp Or ItemLength <> ItemLengthTmp Then
                ItemIDTmp = ItemID
                ItemColorIDTmp = ItemColorID
                ItemColorID2Tmp = ItemColorID2
                ItemLengthTmp = ItemLength
                ItemPQty = Decimal.Parse(DrOrderItems("ORDERITEMQTYBALANCE").ToString)
                OrdersBalanceQty = ItemPQty
            End If
            'ItemsInZoneDs.Tables(0).DefaultView.re

            If OrdersBalanceQty > 0 Then
                ItemsInZoneDs.Tables(0).DefaultView.RowFilter() = "ITEMID=" & ItemID.ToString

                For j = 0 To ZonesMaxLoadDs.Tables(0).Rows.Count - 1

                    ZoneMaxDr = ZonesMaxLoadDs.Tables(0).Rows(j)
                    ZonecodeMax = ZoneMaxDr("WHZONECODE").ToString


                    For k = 0 To ItemsInZoneDs.Tables(0).Rows.Count - 1
                        ItemZoneDR = ItemsInZoneDs.Tables(0).Rows(k)

                        ZoneCode = ItemZoneDR("WHZONECODE").ToString
                        ZoneItemID = Long.Parse(ItemZoneDR("ITEMID").ToString)
                        ZoneItemColorID = Long.Parse(ItemZoneDR("ITEMCOLORID").ToString)
                        ZoneItemColorID2 = Long.Parse(ItemZoneDR("ITEMCOLORID2").ToString)
                        ZoneItemLength = Decimal.Parse(ItemZoneDR("ITEMLENGTH").ToString)
                        ItemZonePQty = Decimal.Parse(ItemZoneDR("ZONEITEMQTYPRIMARY").ToString)

                        If ZoneCode = ZonecodeMax And ItemID = ZoneItemID And ItemColorID = ZoneItemColorID And ItemColorID2 = ZoneItemColorID2 And ItemLength = ZoneItemLength Then

                            If ItemZonePQty >= OrdersBalanceQty Then
                                ItemSetQty = OrdersBalanceQty
                                OrdersBalanceQty = 0
                            Else
                                ItemSetQty = ItemZonePQty
                                OrdersBalanceQty = OrdersBalanceQty - ItemSetQty
                            End If

                            If ItemSetQty > 0 Then
                                ItemCnt = 1
                                ZoneCandDR = ZoneCandDT.NewRow()
                                ZoneCandDR("WHZONECODE") = ZoneCode
                                ZoneCandDR("ITEMID") = ItemID
                                ZoneCandDR("ITEMCODE") = ItemCode
                                ZoneCandDR("ITEMLENGTH") = ZoneItemLength
                                ZoneCandDR("ITEMCOLORID") = ZoneItemColorID
                                ZoneCandDR("ITEMCOLORID2") = ZoneItemColorID2
                                ZoneCandDR("COLORDESC") = ColorDesc
                                ZoneCandDR("ORDERSQTY") = ItemPQty

                                ZoneCandDR("ITEMSETQTY") = ItemSetQty
                                ZoneCandDR("ZONEAVAILABLEQTY") = ItemZonePQty
                                ZoneCandDR("ITEMBALANCEQTY") = OrdersBalanceQty

                                If ThisRouteID > 0 Then
                                    ZoneCandDR("ROUTEID") = ThisRouteID
                                Else
                                    ZoneCandDR("ROUTEID") = 0
                                End If
                                If ThisStoreTransID > 0 Then
                                    ZoneCandDR("STORETRANSID") = ThisStoreTransID
                                Else
                                    ZoneCandDR("STORETRANSID") = 0
                                End If
                                If ThisOrderID > 0 Then
                                    ZoneCandDR("ORDERID") = ThisOrderID
                                Else
                                    ZoneCandDR("ORDERID") = 0
                                End If
                                'ZoneCandDR("ITEMCHOICES") = ItemCnt
                                'ZoneCandDT.ImportRow(ZoneCandDR)
                                ZoneCandDT.Rows.Add(ZoneCandDR)

                            End If
                        End If

                    Next

                Next

            End If 'OrdersBalanceQty > 0

        Next

        If ZoneCandDT.Rows.Count > 0 Then
            ZoneCandDT.DefaultView.Sort() = "WHZONECODE ASC"
            'ZoneCandDT.DefaultView. 
            RouteDs.Tables.Add(ZoneCandDT)
            RouteDs.Tables(0).TableName = "ORDERSPICKINGITEMS"
            'RouteDs.Tables.Add(OrderItemsLocDs.Tables(0))
        End If
        Return RouteDs
    End Function
    <WebMethod(Description:="Picking")> _
   Public Function SOA_FCreateZonePickingRoute(ByVal CompID As Integer, ByVal BranchID As Integer, ByVal WhZoneCode As String, ByVal ZoneItemsDs As DataSet) As DataSet
        Dim SqlStr As String
        Dim Ds As New DataSet
        Dim Dsr As New DataSet
        Dim DT As DataTable
        Dim DTr As DataTable
        Dim DV As DataView
        Dim Dr As DataRow

        Dim WhCode, WhBinCode As String
        Dim ItemCode, ItemDesc As String
        Dim ColorCode, ColorDesc As String
        Dim ColorCode2, ColorDesc2 As String
        Dim ItemID, ItemIDTmp As Long
        Dim ItemColorID As Integer
        Dim ItemColorID2 As Integer
        Dim ItemLength As Decimal
        Dim BinItemQty, OrdersBalanceQty As Decimal
        Dim ItemSetQty, ItemSetQtyBalance As Decimal

        Dim DTColumn As DataColumn
        Dim BinCandDT As New DataTable

        Dim i, j As Long

        Dim ItemZoneDr, ItemBinDr, BinCandDr As DataRow
        Dim ZoneItemID As Long
        Dim ZoneItemColorID As Integer
        Dim ZoneItemColorID2 As Integer
        Dim ZoneItemLength As Decimal
        Dim ItemZonePQty As Decimal


        'BinCandDT
        DTColumn = New DataColumn
        DTColumn.DataType = System.Type.GetType("System.Int32")
        DTColumn.ColumnName = "ID"
        DTColumn.AutoIncrement = True
        DTColumn.Unique = True
        BinCandDT.Columns.Add(DTColumn)
        'zone
        DTColumn = New DataColumn
        DTColumn.DataType = System.Type.GetType("System.String")
        DTColumn.ColumnName = "WHBINCODE"
        DTColumn.AutoIncrement = False
        DTColumn.Unique = False
        BinCandDT.Columns.Add(DTColumn)
        'itemid
        DTColumn = New DataColumn
        DTColumn.DataType = System.Type.GetType("System.Int32")
        DTColumn.ColumnName = "ITEMID"
        DTColumn.AutoIncrement = False
        DTColumn.Unique = False
        BinCandDT.Columns.Add(DTColumn)
        'itemcode
        DTColumn = New DataColumn
        DTColumn.DataType = System.Type.GetType("System.String")
        DTColumn.ColumnName = "ITEMCODE"
        DTColumn.AutoIncrement = False
        DTColumn.Unique = False
        BinCandDT.Columns.Add(DTColumn)
        'ITEMLENGTH
        DTColumn = New DataColumn
        DTColumn.DataType = System.Type.GetType("System.Decimal")
        DTColumn.ColumnName = "ITEMLENGTH"
        DTColumn.AutoIncrement = False
        DTColumn.Unique = False
        BinCandDT.Columns.Add(DTColumn)
        'itemid
        DTColumn = New DataColumn
        DTColumn.DataType = System.Type.GetType("System.Int32")
        DTColumn.ColumnName = "ITEMCOLORID"
        DTColumn.AutoIncrement = False
        DTColumn.Unique = False
        BinCandDT.Columns.Add(DTColumn)
        'COLORDESC
        DTColumn = New DataColumn
        DTColumn.DataType = System.Type.GetType("System.String")
        DTColumn.ColumnName = "COLORDESC"
        DTColumn.AutoIncrement = False
        DTColumn.Unique = False
        BinCandDT.Columns.Add(DTColumn)
        'itemid
        DTColumn = New DataColumn
        DTColumn.DataType = System.Type.GetType("System.Int32")
        DTColumn.ColumnName = "ITEMCOLORID2"
        DTColumn.AutoIncrement = False
        DTColumn.Unique = False
        BinCandDT.Columns.Add(DTColumn)
        'COLORDESC
        DTColumn = New DataColumn
        DTColumn.DataType = System.Type.GetType("System.String")
        DTColumn.ColumnName = "COLORDESC2"
        DTColumn.AutoIncrement = False
        DTColumn.Unique = False
        BinCandDT.Columns.Add(DTColumn)
        'qty     
        'setItemQty
        DTColumn = New DataColumn
        DTColumn.DataType = System.Type.GetType("System.Decimal")
        DTColumn.ColumnName = "ITEMSETQTY"
        DTColumn.AutoIncrement = False
        DTColumn.Unique = False
        BinCandDT.Columns.Add(DTColumn)
        'ItemBalanceQty


        SqlStr = " SELECT VWMSTASKSITEMBINLOCATIONS.WHCODE, VWMSTASKSITEMBINLOCATIONS.WHZONECODE,VWMSTASKSITEMBINLOCATIONS.WHBINCODE,"
        SqlStr = SqlStr & "VWMSTASKSITEMBINLOCATIONS.ITEMID, VWMSTASKSITEMBINLOCATIONS.ITEMCODE,"
        SqlStr = SqlStr & "VWMSTASKSITEMBINLOCATIONS.ITEMDESC,VWMSTASKSITEMBINLOCATIONS.ITEMLENGTH,"
        SqlStr = SqlStr & "VWMSTASKSITEMBINLOCATIONS.ITEMCOLORID,VWMSTASKSITEMBINLOCATIONS.COLORCODE,VWMSTASKSITEMBINLOCATIONS.COLORDESC,VWMSTASKSITEMBINLOCATIONS.ITEMCOLORID2,VWMSTASKSITEMBINLOCATIONS.ORDERITEMQTYBALANCE,"
        SqlStr = SqlStr & "VWMSTASKSITEMBINLOCATIONS.BINITEMQTYPRIMARY "
        SqlStr = SqlStr & "FROM VWMSTASKSITEMBINLOCATIONS  "


        SqlStr = SqlStr & " WHERE VWMSTASKSITEMBINLOCATIONS.WHBINTYPE NOT IN(2,5,6) AND VWMSTASKSITEMBINLOCATIONS.BINITEMQTYPRIMARY > 0 AND VWMSTASKSITEMBINLOCATIONS.WHZONECODE='" & WhZoneCode & "'"

        If CompID > 0 Then
            SqlStr = SqlStr & " AND  VWMSTASKSITEMBINLOCATIONS.COMPID=" & CompID.ToString
            If BranchID > 0 Then
                SqlStr = SqlStr & " AND VWMSTASKSITEMBINLOCATIONS.BRANCHID=" & BranchID.ToString
            End If

        End If

        SqlStr = SqlStr & " ORDER BY VWMSTASKSITEMBINLOCATIONS.ITEMID,VWMSTASKSITEMBINLOCATIONS.ITEMLENGTH,VWMSTASKSITEMBINLOCATIONS.ITEMCOLORID,VWMSTASKSITEMBINLOCATIONS.BINITEMQTYPRIMARY DESC"

        Ds = MyWmsDBTrans.OraDBFillDataset(SqlStr, "ORDERSPICKINGITEMS")

        If Ds.Tables(0).Rows.Count > 0 And ZoneItemsDs.Tables(0).Rows.Count > 0 Then

            ItemSetQty = 0
            ItemSetQtyBalance = 0
            ItemIDTmp = 0


            For Each ItemZoneDr In ZoneItemsDs.Tables(0).Rows

                ZoneItemID = Long.Parse(ItemZoneDr("ITEMID").ToString)
                ZoneItemColorID = Long.Parse(ItemZoneDr("ITEMCOLORID").ToString)
                ZoneItemColorID2 = Long.Parse(ItemZoneDr("ITEMCOLORID2").ToString)
                ZoneItemLength = Decimal.Parse(ItemZoneDr("ITEMLENGTH").ToString)
                ItemZonePQty = Decimal.Parse(ItemZoneDr("ITEMSETQTY").ToString)

                ItemSetQtyBalance = ItemZonePQty

                For Each ItemBinDr In Ds.Tables(0).Rows

                    ItemID = Long.Parse(ItemBinDr("ITEMID").ToString)
                    ItemLength = Decimal.Parse(ItemBinDr("ITEMLENGTH").ToString)
                    ItemColorID = Integer.Parse(ItemBinDr("ITEMCOLORID").ToString)
                    ItemColorID2 = Integer.Parse(ItemBinDr("ITEMCOLORID2").ToString)

                    If Len(ItemBinDr("BINITEMQTYPRIMARY").ToString) > 0 Then
                        BinItemQty = Decimal.Parse(ItemBinDr("BINITEMQTYPRIMARY").ToString)
                    Else
                        BinItemQty = 0
                    End If


                    If ItemSetQtyBalance > 0 And BinItemQty > 0 And ZoneItemID = ItemID And ZoneItemLength = ItemLength And ZoneItemColorID = ItemColorID And ZoneItemColorID2 = ItemColorID2 Then

                        If BinItemQty >= ItemSetQtyBalance Then
                            ItemSetQty = ItemSetQtyBalance
                            ItemSetQtyBalance = 0
                        Else
                            ItemSetQty = BinItemQty
                            ItemSetQtyBalance = ItemSetQtyBalance - BinItemQty
                        End If

                        WhBinCode = ItemBinDr("WHBINCODE").ToString
                        ItemCode = ItemBinDr("ITEMCODE").ToString
                        ColorDesc = ItemBinDr("COLORDESC").ToString
                        'ColorDesc2 = ItemBinDr("COLORDESC2").ToString
                        'INSERT VALUES
                        BinCandDr = BinCandDT.NewRow()
                        BinCandDr("WHBINCODE") = WhBinCode
                        BinCandDr("ITEMID") = ItemID
                        BinCandDr("ITEMCODE") = ItemCode
                        BinCandDr("ITEMLENGTH") = ItemLength

                        BinCandDr("ITEMCOLORID") = ItemColorID
                        BinCandDr("COLORDESC") = ColorDesc
                        BinCandDr("ITEMCOLORID2") = ItemColorID2
                        ' BinCandDr("COLORDESC2") = ColorDesc2
                        BinCandDr("ITEMSETQTY") = ItemSetQty

                        BinCandDT.Rows.Add(BinCandDr)

                    End If
                Next

            Next

        End If

        Dsr.DataSetName = "ZONEBINPICKINGITEMS"

        Dsr.Tables.Add(BinCandDT)
        Dsr.Tables(0).TableName = "ZONEBINPICKINGITEMS"

        Return Dsr
    End Function
    <WebMethod(Description:="Picking::Create Zone Picking Card")> _
    Public Function SOA_FGetPickingCardType(ByVal CardID As Long) As String
        Dim SqlStr As String
        Dim Rtrn As String

        SqlStr = " SELECT COUNT(*) AS CNT FROM TWMSPICKINGCARD WHERE CARDID =" & CardID.ToString & "AND (STORETRANSID > 0 OR ORDERID > 0)"

        Rtrn = MyWmsDBTrans.OraDBWmsExSelectCmdRN2String(SqlStr)

        If Len(Rtrn) > 0 And (Rtrn <> "Null" Or Rtrn <> "NULL") Then
            If Integer.Parse(Rtrn) > 0 Then
                Return "3"
            Else
                Return "1"
            End If
        Else
            Return "1"
        End If
    End Function
    <WebMethod(Description:="Picking::Create Zone Picking Card")> _
        Public Function SOA_FCreateZonePickingCard(ByVal CompID As Integer, ByVal BranchID As Integer, ByVal CardID As Long, ByVal WhZoneCode As String, ByVal ZoneItemsDs As DataSet, ByVal appuser As Integer, ByVal Terminal As String) As DataSet
        Dim SqlStr As String
        Dim Ds As New DataSet
        Dim Dsr As New DataSet
        Dim DT As DataTable
        Dim DTr As DataTable
        Dim DV As DataView
        Dim Dr As DataRow

        Dim WhCode, WhBinCode As String
        Dim WhBinID As Long
        Dim ItemCode, ItemDesc As String
        Dim ColorCode, ColorDesc As String
        Dim ColorCode2, ColorDesc2 As String
        Dim ItemID, ItemIDTmp As Long
        Dim ItemColorID As Integer
        Dim ItemColorID2 As Integer
        Dim ItemLength As Decimal
        Dim BinItemQty, OrdersBalanceQty As Decimal
        Dim ItemSetQty, ItemSetQtyBalance As Decimal



        Dim i, j As Long

        Dim ItemZoneDr, ItemBinDr, BinCandDr As DataRow
        Dim ZoneItemID As Long
        Dim ZoneItemColorID As Integer
        Dim ZoneItemColorID2 As Integer
        Dim ZoneItemLength As Decimal
        Dim ItemZonePQty As Decimal

        Dim MyWmsBinTrans As New WmsBinTrans
        '===
        ' Create CardID
        '===


        SqlStr = " SELECT VWMSTASKSITEMBINLOCATIONS.WHCODE, VWMSTASKSITEMBINLOCATIONS.WHZONECODE,VWMSTASKSITEMBINLOCATIONS.WHBINID,VWMSTASKSITEMBINLOCATIONS.WHBINCODE,"
        SqlStr = SqlStr & "VWMSTASKSITEMBINLOCATIONS.ITEMID, VWMSTASKSITEMBINLOCATIONS.ITEMCODE,"
        SqlStr = SqlStr & "VWMSTASKSITEMBINLOCATIONS.ITEMDESC,VWMSTASKSITEMBINLOCATIONS.ITEMLENGTH,"
        SqlStr = SqlStr & "VWMSTASKSITEMBINLOCATIONS.ITEMCOLORID,VWMSTASKSITEMBINLOCATIONS.COLORCODE,VWMSTASKSITEMBINLOCATIONS.COLORDESC,VWMSTASKSITEMBINLOCATIONS.ITEMCOLORID2,VWMSTASKSITEMBINLOCATIONS.ORDERITEMQTYBALANCE,"
        SqlStr = SqlStr & "VWMSTASKSITEMBINLOCATIONS.BINITEMQTYPRIMARY "
        SqlStr = SqlStr & "FROM VWMSTASKSITEMBINLOCATIONS  "


        SqlStr = SqlStr & " WHERE VWMSTASKSITEMBINLOCATIONS.WHBINTYPE NOT IN (2,5,6) AND VWMSTASKSITEMBINLOCATIONS.BINITEMQTYPRIMARY > 0 AND VWMSTASKSITEMBINLOCATIONS.WHZONECODE='" & WhZoneCode & "'"

        If CompID > 0 Then
            SqlStr = SqlStr & " AND  VWMSTASKSITEMBINLOCATIONS.COMPID=" & CompID.ToString
            If BranchID > 0 Then
                SqlStr = SqlStr & " AND VWMSTASKSITEMBINLOCATIONS.BRANCHID=" & BranchID.ToString
            End If

        End If

        SqlStr = SqlStr & " ORDER BY VWMSTASKSITEMBINLOCATIONS.ITEMID,VWMSTASKSITEMBINLOCATIONS.ITEMLENGTH,VWMSTASKSITEMBINLOCATIONS.ITEMCOLORID,VWMSTASKSITEMBINLOCATIONS.BINITEMQTYPRIMARY DESC"

        Ds = MyWmsDBTrans.OraDBFillDataset(SqlStr, "ORDERSPICKINGITEMS")

        If Ds.Tables(0).Rows.Count > 0 And ZoneItemsDs.Tables(0).Rows.Count > 0 Then

            ItemSetQty = 0
            ItemSetQtyBalance = 0
            ItemIDTmp = 0


            For Each ItemZoneDr In ZoneItemsDs.Tables(0).Rows

                ZoneItemID = Long.Parse(ItemZoneDr("ITEMID").ToString)
                ZoneItemColorID = Long.Parse(ItemZoneDr("ITEMCOLORID").ToString)
                ZoneItemColorID2 = Long.Parse(ItemZoneDr("ITEMCOLORID2").ToString)
                ZoneItemLength = Decimal.Parse(ItemZoneDr("ITEMLENGTH").ToString)
                ItemZonePQty = Decimal.Parse(ItemZoneDr("ITEMSETQTY").ToString)

                ItemSetQtyBalance = ItemZonePQty

                For Each ItemBinDr In Ds.Tables(0).Rows

                    ItemID = Long.Parse(ItemBinDr("ITEMID").ToString)
                    ItemLength = Decimal.Parse(ItemBinDr("ITEMLENGTH").ToString)
                    ItemColorID = Integer.Parse(ItemBinDr("ITEMCOLORID").ToString)
                    ItemColorID2 = Integer.Parse(ItemBinDr("ITEMCOLORID2").ToString)

                    If Len(ItemBinDr("BINITEMQTYPRIMARY").ToString) > 0 Then
                        BinItemQty = Decimal.Parse(ItemBinDr("BINITEMQTYPRIMARY").ToString)
                    Else
                        BinItemQty = 0
                    End If


                    If ItemSetQtyBalance > 0 And BinItemQty > 0 And ZoneItemID = ItemID And ZoneItemLength = ItemLength And ZoneItemColorID = ItemColorID And ZoneItemColorID2 = ItemColorID2 Then

                        If BinItemQty >= ItemSetQtyBalance Then
                            ItemSetQty = ItemSetQtyBalance
                            ItemSetQtyBalance = 0
                        Else
                            ItemSetQty = BinItemQty
                            ItemSetQtyBalance = ItemSetQtyBalance - BinItemQty
                        End If

                        WhBinID = Long.Parse(ItemBinDr("WHBINID").ToString)
                        WhBinCode = ItemBinDr("WHBINCODE").ToString
                        ItemCode = ItemBinDr("ITEMCODE").ToString
                        ColorDesc = ItemBinDr("COLORDESC").ToString


                        MyWmsBinTrans.FCreateNewPickingCard(CompID, BranchID, CardID, WhBinID, WhBinCode, 0, WhZoneCode, 0, 0, 0, ItemID, ItemLength, ItemColorID, ItemColorID2, ItemSetQty, 0, 0, 0, appuser, Terminal)

                    End If
                Next

            Next


        End If
        'Return Picking Card to the user


        SqlStr = " SELECT CARDID,COMPID,BRANCHID,WHBINCODE,WHBINID,WHZONEID,WHZONECODE,STORETRANSID,ORDERID,ORDERDTLID,"
        SqlStr = SqlStr & "ITEMID,ITEMCODE,ITEMDESC,ITEMLENGTH,ITEMCOLORID,COLORCODE,COLORDESC,ITEMCOLORID2,COLORCODE2,COLORDESC2,ITEMPRIMARYQTY,ITEMPRIMARYQTY,ITEMBALANCEQTY ,"
        SqlStr = SqlStr & "ITEMMUNITPRIMARY,ITEMSECONDARYQTY,ITEMMUNITSECONDARY,DBRECCREATEDATE,DBRECCREATEUSER,DBRECMODDATE,TERMINAL"
        SqlStr = SqlStr & " FROM VWMSPICKINGCARD WHERE CARDID=" & CardID.ToString
        SqlStr = SqlStr & "ORDER BY WHBINCODE "


        Dsr = MyWmsDBTrans.OraDBFillDataset(SqlStr, "PICKINGCARD")

        Return Dsr
    End Function
    <WebMethod(Description:="Picking::Just Get a New CardID Number")> _
       Public Function SOA_FCreateNewCardID() As Long
        Dim MyWmsBinTrans As New WmsBinTrans

        SOA_FCreateNewCardID = MyWmsBinTrans.FCreateNewCardID()
    End Function
    <WebMethod(Description:="Picking::Get Picking Card ")> _
     Public Function SOA_FGetPickingCard(ByVal CardID As Long, ByVal FilterBalance As Integer) As DataSet
        Dim SqlStr As String
        Dim Dsr As New DataSet

        SqlStr = " SELECT CARDROWID,CARDID,COMPID,BRANCHID,WHBINCODE,WHBINID,WHZONEID,WHZONECODE,STORETRANSID,ORDERID,ORDERDTLID,"
        SqlStr = SqlStr & "ITEMID,ITEMCODE,ITEMDESC,ITEMLENGTH,ITEMCOLORID,COLORCODE,COLORDESC,ITEMCOLORID2,COLORCODE2,COLORDESC2,ITEMPRIMARYQTY,ITEMPRIMARYQTY,ITEMBALANCEQTY ,"
        SqlStr = SqlStr & "ITEMMUNITPRIMARY,ITEMSECONDARYQTY,ITEMMUNITSECONDARY,DBRECCREATEDATE,DBRECCREATEUSER,DBRECMODDATE,TERMINAL"
        SqlStr = SqlStr & " FROM VWMSPICKINGCARD WHERE  CARDID=" & CardID.ToString

        If FilterBalance = 1 Then
            SqlStr = SqlStr & " AND ITEMBALANCEQTY > 0 "
        End If

        SqlStr = SqlStr & " ORDER BY WHBINCODE "


        Dsr = MyWmsDBTrans.OraDBFillDataset(SqlStr, "PICKINGCARD")

        Return Dsr
    End Function
    <WebMethod(Description:="Picking::Get Picking Method for exsiting Picking Card ")> _
        Public Function SOA_FGetPickingCardMethod(ByVal CardID As Long) As String
        Dim SqlStr As String
        Dim RtrnStr As String

        SqlStr = " SELECT NVL(COUNT(*),0) AS ORDERPICKING "
        SqlStr = SqlStr & " FROM VWMSPICKINGCARD WHERE  CARDID=" & CardID.ToString
        SqlStr = SqlStr & " AND (STORETRANSID > 0 OR ORDERID > 0 ) "


        RtrnStr = MyWmsDBTrans.OraDBWmsExSelectCmdRN2String(SqlStr)


        If Len(RtrnStr) > 0 And (RtrnStr <> "Null" Or RtrnStr <> "NULL") Then
            Return RtrnStr
        Else
            Return "-1"
        End If

    End Function

    <WebMethod(Description:="Picking::UpdatePicking Card")> _
    Public Function SOA_FUpdatePickingCard(ByVal CardID As Integer, ByVal CardRowID As Long, ByVal RsrvStoreTrnsID As Long, ByVal OrderID As Long, ByVal ItemID As Long, ByVal ItemLength As Decimal, ByVal ItemColorID As Long, ByVal ItemColorID2 As Long, ByVal PickedQty As Decimal) As String
        Dim SqlStr As String

        Dim UpdNRows As Integer

        Dim MyWmsBinTrans As New WmsBinTrans

        Dim MUnitPrimary, MUnitDecimals As Integer

        MUnitPrimary = MyWmsBinTrans.FGetItemMUnitPrimary(ItemID)
        MUnitDecimals = MyWmsBinTrans.FGetItemMUnitDecimal(MUnitPrimary)

        If MUnitDecimals = Nothing Then MUnitDecimals = 0

        SqlStr = "UPDATE TWMSPICKINGCARD SET ITEMBALANCEQTY = ITEMBALANCEQTY - " & MyWmsBinTrans.f_dbinsertdecimal(PickedQty, MUnitDecimals)
        SqlStr = SqlStr & " WHERE CARDID=" & CardID.ToString

        If CardRowID > 0 Then
            SqlStr = SqlStr & " AND CARDROWID=" & CardRowID.ToString
        End If

        If RsrvStoreTrnsID > 0 Then
            SqlStr = SqlStr & " AND STORETRANSID=" & RsrvStoreTrnsID.ToString
        ElseIf OrderID > 0 Then
            SqlStr = SqlStr & " AND ORDERID=" & OrderID.ToString
        End If


        SqlStr = SqlStr & " AND ITEMID=" & ItemID.ToString
        If ItemLength > 0 Then
            SqlStr = SqlStr & " AND ITEMLENGTH=" & MyWmsBinTrans.f_dbinsertdecimal(ItemLength, 3)
        End If
        If ItemColorID > 0 Then
            SqlStr = SqlStr & " AND ITEMCOLORID=" & ItemColorID.ToString
        End If


        If ItemColorID2 > 0 Then
            SqlStr = SqlStr & " AND ITEMCOLORID2=" & ItemColorID2.ToString
        End If

        UpdNRows = MyWmsDBTrans.OraDBExecuteSQLCmd(SqlStr, 1)
        MyWmsDBTrans.OraDBExecuteSQLCmd("UPDATE TWMSPICKINGCARD SET ITEMBALANCEQTY=0 WHERE ITEMBALANCEQTY < 0", 1)

        If UpdNRows > 0 Then
            Return "1"
        Else
            Return "-1"
        End If
    End Function
    <WebMethod(Description:="Picking::End Picking Card")> _
    Public Function FEndPickingCard(ByVal CompID As Integer, ByVal BranchID As Integer, ByVal TransBinCode As String, ByVal EndORPutAway As Integer, ByVal CardID As Long, ByVal FRsrvStoreTRansID As Long, ByVal FOrderID As Long, ByVal WhBinCodeTo As String, ByVal AppUser As Integer, ByVal Terminal As String) As String
        '{-2::Error transfering items from Virtual transfer area to Paletizing Area}
        '{-3::Errordeleting Picking Card
        'Transfer items from Trans (On The Fly) Area to Picked Items Area
        'Delete CardID Items From TWMSPickingCard
        'EndORPutAway {1:End,0:PutAway on Paletizing Stage Area}

        Dim SqlStr As String

        Dim PackItemNo As String
        Dim ItemID As Long
        Dim ItemLength As Decimal
        Dim ItemColorID, ItemColorID2 As Long

        Dim ItemPMunit, ItemSMUnit As Integer

        Dim WBinIDFrom, WhBinIDTo As Long
        Dim WhBinCodeFrom, ThisBinCodeTo As String

        Dim BinItemQty, BinItemSQty, BinItemPackQty As Decimal


        Dim ThisStoreTransID, ThisOrderID As Long
        Dim MyWmsBinTrans As New WmsBinTrans
        Dim DsBinInfo As New DataSet

        Dim Dr As DataRow


        Dim UpdRtrn As Integer
        Dim I As Integer

        Dim UpdBinQtyRtrn As Long
        Dim SqlUpdRtrn As Long

        If Len(TransBinCode) = 0 Or Len(TransBinCode) < 5 Then
            SqlStr = "SELECT DISTINCT TRANSBINCODE FROM TWMSPICKINGCARD WHERE CARDID=" & CardID.ToString
            WhBinCodeFrom = MyWmsDBTrans.OraDBWmsExSelectCmdRStr2Str(SqlStr)
        Else
            WhBinCodeFrom = TransBinCode
        End If


        If Len(WhBinCodeTo) > 0 And (WhBinCodeTo <> "Null" Or WhBinCodeTo <> "NULL") Then
            ThisBinCodeTo = WhBinCodeTo
        Else
            SqlStr = "SELECT  MIN(WHBINCODE) FROM TWMSBINS WHERE WHBINTYPE = 6 "
            ThisBinCodeTo = MyWmsDBTrans.OraDBWmsExSelectCmdRStr2Str(SqlStr)
        End If

        WhBinIDTo = MyWmsBinTrans.FGetBinIDByCode(CompID, BranchID, ThisBinCodeTo)

        If Len(WhBinCodeFrom) > 0 And (WhBinCodeFrom <> "Null" Or WhBinCodeFrom <> "NULL") Then

            WBinIDFrom = MyWmsBinTrans.FGetBinIDByCode(CompID, BranchID, WhBinCodeFrom)

            DsBinInfo = MyWmsBinTrans.FGetBinItemsInfo(CompID, BranchID, WhBinCodeFrom)

            For I = 1 To DsBinInfo.Tables(0).Rows.Count - 1
                Dr = DsBinInfo.Tables(0).Rows(I)
                Try

                    WBinIDFrom = Long.Parse(Dr("WHBINID").ToString)

                    ItemID = Long.Parse(Dr("ITEMID").ToString)
                    ItemColorID = Long.Parse(Dr("ITEMCOLORID").ToString)
                    ItemColorID2 = Long.Parse(Dr("ITEMCOLORID2").ToString)
                    ItemLength = Decimal.Parse(Dr("ITEMLENGTH").ToString)
                    BinItemQty = Decimal.Parse(Dr("BINITEMQTYPRIMARY").ToString)

                    BinItemSQty = Decimal.Parse(Dr("BINITEMQTYSECONDARY").ToString)

                    ItemPMunit = Integer.Parse(Dr("MUNITPRIMARY").ToString)
                    ItemSMUnit = Integer.Parse(Dr("MUNITSECONDARY").ToString)

                    ThisStoreTransID = Long.Parse(Dr("STORETRANSID").ToString)
                    ThisOrderID = Long.Parse(Dr("ORDERID").ToString)
                Catch ex As Exception

                End Try

                UpdBinQtyRtrn = MyWmsBinTrans.FUpdateBinQty(CompID, BranchID, -1, PackItemNo, WBinIDFrom, ItemID, 0, ItemLength, ItemColorID, ItemColorID2, BinItemQty, ItemPMunit, BinItemSQty, ItemSMUnit, 0, 0, ThisOrderID, 0, ThisStoreTransID, AppUser, Terminal)

                If UpdBinQtyRtrn > 0 Then
                    UpdRtrn = 1
                Else
                    UpdRtrn = 0
                End If

                UpdBinQtyRtrn = MyWmsBinTrans.FUpdateBinQty(CompID, BranchID, 1, PackItemNo, WhBinIDTo, ItemID, 0, ItemLength, ItemColorID, ItemColorID2, BinItemQty, ItemPMunit, BinItemSQty, ItemSMUnit, 0, ThisOrderID, 0, 0, ThisStoreTransID, AppUser, Terminal)

                If UpdBinQtyRtrn > 0 Then
                    UpdRtrn = UpdRtrn + 1
                End If

            Next
            'Delete Picking Card

            'If Not UpdRtrn > 0 Then
            '    Return "-2"
            'End If

            If EndORPutAway = 1 Then
                SqlStr = "DELETE FROM TWMSPICKINGCARD WHERE CARDID=" & CardID.ToString
                SqlUpdRtrn = MyWmsDBTrans.OraDBExecuteSQLCmd(SqlStr, 1)


                If SqlUpdRtrn > 0 Then
                    Return "1"
                Else
                    Return "-3"
                End If
            End If
            'Check UpdRtrn for ensured Results
            If UpdRtrn > 0 Then
                Return "1"
            Else
                Return "-1"
            End If
        Else
            Return "-1"
        End If
    End Function
    <WebMethod(Description:="Picking")> _
    Public Function SOA_FCreatePalettePickingCard(ByVal CompID As Integer, ByVal BranchID As Integer, ByVal g_CardID As Long, ByVal WhBinID As Long, ByVal WhZoneCode As String, ByVal WhBinCode As String, ByVal ItemID As Long, ByVal ItemLength As Decimal, ByVal ItemColorID As Integer, ByVal ItemColorID2 As Integer, ByVal ItemSetQty As Decimal, ByVal g_UserID As Integer, ByVal g_Terminal As String) As String

        Dim SqlStr As String
        Dim MyWmsBinTrans As New WmsBinTrans

        Dim Rtrn As Long

        Rtrn = MyWmsBinTrans.FCreateNewPickingCard(CompID, BranchID, g_CardID, WhBinID, WhBinCode, 0, WhZoneCode, 0, 0, 0, ItemID, ItemLength, ItemColorID, ItemColorID2, ItemSetQty, 0, 0, 0, g_UserID, g_Terminal)

        If Rtrn > 0 Then
            Return "1"
        Else
            Return "-1"
        End If
    End Function
#End Region

#Region "Orders Picking Tasks"
    <WebMethod(Description:="Picking::Tasks Get Task Info")> _
    Public Function SOA_FGetTaskInfo(ByVal RouteID As Long, ByVal RsrvStoreTransID As Long, ByVal ORderID As Long) As DataSet

        Dim Ds As New DataSet
        Dim SqlStr As String

        SqlStr = "SELECT DISTINCT WMSDESTINATION,SALESORDERID AS ORDERID,ORDERCODE,PROMISEDDELIVDATE,TO_CHAR(TO_DATE(PROMISEDDELIVDATE,'DD/MM/YY')) AS PROMISEDDELIVDATECHR,"
        SqlStr = SqlStr & "CUSTOMERTITLE FROM VWMSORDERSWMSTASKSQTY "


        If RsrvStoreTransID > 0 Then
            SqlStr = SqlStr & "WHERE STORETRANSID = " & RsrvStoreTransID.ToString
        ElseIf ORderID > 0 Then
            SqlStr = SqlStr & "WHERE SALESORDERID = " & ORderID.ToString
        Else
            SqlStr = SqlStr & "WHERE ROUTEID = " & RouteID.ToString
        End If

        Ds = MyWmsDBTrans.OraDBFillDataset(SqlStr, "TASKGENERALINFO")
        Return Ds

    End Function
    <WebMethod(Description:="Picking::Tasks set for Picking Orders")> _
    Public Function SOA_FGetOrdersPickingListInTasks(ByVal CompID As Integer, ByVal BranchID As Integer, ByVal UserID As Integer) As DataSet
        Dim SqlStr As String

        Dim MyWmsDBTrans As New WmsDBTrans

        Dim Ds As New DataSet
        Dim DTColumn As DataColumn
        Dim I As Integer

        SqlStr = "SELECT DISTINCT COMPID,BRANCHID,ROUTEID,ROUTEDATE,TO_CHAR( ROUTEDATE,'DD/MM/YY') AS SHORTROUTEDATE,ROUTESTATUS,ASSIGNEDTOUSER,"
        SqlStr = SqlStr & "USERFULLNAME,ROUTESTATUSDESC,ROUTESTATUSDESCEN "
        SqlStr = SqlStr & "FROM  VWMSORDERSWMSTASKSQTY WHERE RESTFORPICKINGQTY > 0 "
        If CompID > 0 Then
            SqlStr = SqlStr & " AND COMPID=" & CompID.ToString
            If BranchID > 0 Then
                SqlStr = SqlStr & " AND BranchID=" & BranchID.ToString
            End If

            If UserID > 0 Then
                SqlStr = SqlStr & " AND ASSIGNEDTOUSER=" & UserID.ToString
            End If
        Else
            If UserID > 0 Then
                SqlStr = SqlStr & " AND  ASSIGNEDTOUSER=" & UserID.ToString
            End If
        End If

        Ds = MyWmsDBTrans.OraDBFillDataset(SqlStr, "ORDERSPICKLISTINTASKS")
        DTColumn = New DataColumn

        DTColumn.DataType = System.Type.GetType("System.Int32")
        DTColumn.ColumnName = "ID"
        DTColumn.AutoIncrement = False
        Ds.Tables(0).Columns.Add(DTColumn)

        If Ds.Tables(0).Rows.Count > 0 Then
            Dim IDValue As Integer
            IDValue = 0
            For I = 0 To Ds.Tables(0).Rows.Count - 1
                IDValue = IDValue + 1
                Ds.Tables(0).Rows(I)("ID") = IDValue.ToString()
            Next
        End If
        Return Ds
    End Function
    <WebMethod(Description:="Picking::Create Task Picking Route")> _
    Public Function SOA_FCreateTaskPickingRoute(ByVal CompID As Integer, ByVal BranchID As Integer, ByVal WhBuildingID As Integer, ByVal RouteID As Long, ByVal RsrvStoreTransID As Long, ByVal OrderID As Long) As DataSet
        Dim SqlStr As String
        Dim Ds As New DataSet
        Dim Dsr As New DataSet
        Dim DT As DataTable
        Dim DTr As DataTable
        Dim DV As DataView
        Dim Dr As DataRow

        Dim WhCode, WhBinCode, WhZoneCode As String
        Dim ItemCode, ItemDesc As String
        Dim ColorCode, ColorDesc As String
        Dim ColorCode2, ColorDesc2 As String
        Dim ItemID, ItemIDTmp As Long
        Dim ItemColorID As Integer
        Dim ItemColorID2 As Integer
        Dim ItemLength As Decimal
        Dim BinItemQty, OrdersBalanceQty As Decimal
        Dim ItemSetQty, ItemSetQtyBalance As Decimal

        Dim ZoneItemsDs As DataSet
        Dim DTColumn As DataColumn
        Dim BinCandDT As New DataTable
        Dim ThisRouteID, ThisOrderID, ThisStoreTransID, ThisOrderDtlID As Long

        Dim i, j As Long

        Dim ItemZoneDr, ItemBinDr, BinCandDr As DataRow
        Dim ZoneItemID As Long
        Dim ZoneItemColorID As Integer
        Dim ZoneItemColorID2 As Integer
        Dim ZoneItemLength As Decimal
        Dim ItemZonePQty As Decimal


        'BinCandDT
        DTColumn = New DataColumn
        DTColumn.DataType = System.Type.GetType("System.Int32")
        DTColumn.ColumnName = "ID"
        DTColumn.AutoIncrement = True
        DTColumn.Unique = True
        BinCandDT.Columns.Add(DTColumn)
        'WHBIN       
        DTColumn = New DataColumn
        DTColumn.DataType = System.Type.GetType("System.String")
        DTColumn.ColumnName = "WHZONECODE"
        DTColumn.AutoIncrement = False
        DTColumn.Unique = False
        BinCandDT.Columns.Add(DTColumn)
        DTColumn = New DataColumn
        DTColumn.DataType = System.Type.GetType("System.String")
        DTColumn.ColumnName = "WHBINCODE"
        DTColumn.AutoIncrement = False
        DTColumn.Unique = False
        BinCandDT.Columns.Add(DTColumn)
        'ROUTEID
        DTColumn = New DataColumn
        DTColumn.DataType = System.Type.GetType("System.Int32")
        DTColumn.ColumnName = "ROUTEID"
        DTColumn.AutoIncrement = False
        DTColumn.Unique = False
        BinCandDT.Columns.Add(DTColumn)
        'ORDERID
        'STORETRANSID
        DTColumn = New DataColumn
        DTColumn.DataType = System.Type.GetType("System.Int32")
        DTColumn.ColumnName = "STORETRANSID"
        DTColumn.AutoIncrement = False
        DTColumn.Unique = False
        BinCandDT.Columns.Add(DTColumn)
        'ORDERID
        DTColumn = New DataColumn
        DTColumn.DataType = System.Type.GetType("System.Int32")
        DTColumn.ColumnName = "ORDERID"
        DTColumn.AutoIncrement = False
        DTColumn.Unique = False
        BinCandDT.Columns.Add(DTColumn)
        'ORDERDTLID
        DTColumn = New DataColumn
        DTColumn.DataType = System.Type.GetType("System.Int32")
        DTColumn.ColumnName = "ORDERDTLID"
        DTColumn.AutoIncrement = False
        DTColumn.Unique = False
        BinCandDT.Columns.Add(DTColumn)
        'itemid
        DTColumn = New DataColumn
        DTColumn.DataType = System.Type.GetType("System.Int32")
        DTColumn.ColumnName = "ITEMID"
        DTColumn.AutoIncrement = False
        DTColumn.Unique = False
        BinCandDT.Columns.Add(DTColumn)
        'itemcode
        DTColumn = New DataColumn
        DTColumn.DataType = System.Type.GetType("System.String")
        DTColumn.ColumnName = "ITEMCODE"
        DTColumn.AutoIncrement = False
        DTColumn.Unique = False
        BinCandDT.Columns.Add(DTColumn)
        'ITEMLENGTH
        DTColumn = New DataColumn
        DTColumn.DataType = System.Type.GetType("System.Decimal")
        DTColumn.ColumnName = "ITEMLENGTH"
        DTColumn.AutoIncrement = False
        DTColumn.Unique = False
        BinCandDT.Columns.Add(DTColumn)
        'itemid
        DTColumn = New DataColumn
        DTColumn.DataType = System.Type.GetType("System.Int32")
        DTColumn.ColumnName = "ITEMCOLORID"
        DTColumn.AutoIncrement = False
        DTColumn.Unique = False
        BinCandDT.Columns.Add(DTColumn)
        'COLORDESC
        DTColumn = New DataColumn
        DTColumn.DataType = System.Type.GetType("System.String")
        DTColumn.ColumnName = "COLORDESC"
        DTColumn.AutoIncrement = False
        DTColumn.Unique = False
        BinCandDT.Columns.Add(DTColumn)
        'itemid
        DTColumn = New DataColumn
        DTColumn.DataType = System.Type.GetType("System.Int32")
        DTColumn.ColumnName = "ITEMCOLORID2"
        DTColumn.AutoIncrement = False
        DTColumn.Unique = False
        BinCandDT.Columns.Add(DTColumn)
        'COLORDESC
        DTColumn = New DataColumn
        DTColumn.DataType = System.Type.GetType("System.String")
        DTColumn.ColumnName = "COLORDESC2"
        DTColumn.AutoIncrement = False
        DTColumn.Unique = False
        BinCandDT.Columns.Add(DTColumn)
        'qty     
        'setItemQty
        DTColumn = New DataColumn
        DTColumn.DataType = System.Type.GetType("System.Decimal")
        DTColumn.ColumnName = "ITEMSETQTY"
        DTColumn.AutoIncrement = False
        DTColumn.Unique = False
        BinCandDT.Columns.Add(DTColumn)
        'ItemBalanceQty

        ZoneItemsDs = SOA_FCreateGroupOrdersPickingRoute(CompID, BranchID, WhBuildingID, RouteID, RsrvStoreTransID, OrderID)

        Dim ZonesFltr As String
        If ZoneItemsDs.Tables(0).Rows.Count > 0 Then
            ZonesFltr = " "
            For i = 0 To ZoneItemsDs.Tables(0).Rows.Count - 1
                ZonesFltr = ZonesFltr & "'" & ZoneItemsDs.Tables(0).Rows(i)("WHZONECODE").ToString & "'"
                If i < ZoneItemsDs.Tables(0).Rows.Count - 1 Then
                    ZonesFltr = ZonesFltr & ","
                End If
            Next
        End If
        SqlStr = " SELECT VWMSTASKSITEMBINLOCATIONS.WHCODE, VWMSTASKSITEMBINLOCATIONS.WHZONECODE,VWMSTASKSITEMBINLOCATIONS.WHBINCODE,"
        SqlStr = SqlStr & "VWMSTASKSITEMBINLOCATIONS.ITEMID, VWMSTASKSITEMBINLOCATIONS.ITEMCODE,"
        SqlStr = SqlStr & "VWMSTASKSITEMBINLOCATIONS.ITEMDESC,VWMSTASKSITEMBINLOCATIONS.ITEMLENGTH,"
        SqlStr = SqlStr & "VWMSTASKSITEMBINLOCATIONS.ITEMCOLORID,VWMSTASKSITEMBINLOCATIONS.COLORCODE,VWMSTASKSITEMBINLOCATIONS.COLORDESC,VWMSTASKSITEMBINLOCATIONS.ITEMCOLORID2,VWMSTASKSITEMBINLOCATIONS.ORDERITEMQTYBALANCE,"
        SqlStr = SqlStr & "VWMSTASKSITEMBINLOCATIONS.BINITEMQTYPRIMARY "
        SqlStr = SqlStr & "FROM VWMSTASKSITEMBINLOCATIONS  "


        SqlStr = SqlStr & " WHERE VWMSTASKSITEMBINLOCATIONS.WHBINTYPE NOT IN(2,5,6) AND VWMSTASKSITEMBINLOCATIONS.BINITEMQTYPRIMARY > 0 AND VWMSTASKSITEMBINLOCATIONS.WHZONECODE IN (" & ZonesFltr & ")"

        If CompID > 0 Then
            SqlStr = SqlStr & " AND  VWMSTASKSITEMBINLOCATIONS.COMPID=" & CompID.ToString
            If BranchID > 0 Then
                SqlStr = SqlStr & " AND VWMSTASKSITEMBINLOCATIONS.BRANCHID=" & BranchID.ToString
            End If

        End If

        SqlStr = SqlStr & " ORDER BY VWMSTASKSITEMBINLOCATIONS.ITEMID,VWMSTASKSITEMBINLOCATIONS.ITEMLENGTH,VWMSTASKSITEMBINLOCATIONS.ITEMCOLORID,VWMSTASKSITEMBINLOCATIONS.BINITEMQTYPRIMARY DESC"

        Ds = MyWmsDBTrans.OraDBFillDataset(SqlStr, "ORDERSPICKINGITEMS")

        If Ds.Tables(0).Rows.Count > 0 And ZoneItemsDs.Tables(0).Rows.Count > 0 Then

            ItemSetQty = 0
            ItemSetQtyBalance = 0
            ItemIDTmp = 0


            For Each ItemZoneDr In ZoneItemsDs.Tables(0).Rows

                ZoneItemID = Long.Parse(ItemZoneDr("ITEMID").ToString)
                ZoneItemColorID = Long.Parse(ItemZoneDr("ITEMCOLORID").ToString)
                ZoneItemColorID2 = Long.Parse(ItemZoneDr("ITEMCOLORID2").ToString)
                ZoneItemLength = Decimal.Parse(ItemZoneDr("ITEMLENGTH").ToString)
                ItemZonePQty = Decimal.Parse(ItemZoneDr("ITEMSETQTY").ToString)
                WhZoneCode = ItemZoneDr("WHZONECODE").ToString
                ThisRouteID = Long.Parse(ItemZoneDr("ROUTEID").ToString)
                ThisOrderID = Long.Parse(ItemZoneDr("ORDERID").ToString)
                ThisStoreTransID = Long.Parse(ItemZoneDr("STORETRANSID").ToString)
                Try
                    ThisStoreTransID = Long.Parse(ItemZoneDr("STORETRANSID").ToString)
                Catch ex As Exception

                End Try

                ThisOrderID = Long.Parse(ItemZoneDr("ORDERID").ToString)
                'ThisOrderID = ItemZoneDr("ORDERDTLID").ToString

                ItemSetQtyBalance = ItemZonePQty

                For Each ItemBinDr In Ds.Tables(0).Rows

                    ItemID = Long.Parse(ItemBinDr("ITEMID").ToString)
                    ItemLength = Decimal.Parse(ItemBinDr("ITEMLENGTH").ToString)
                    ItemColorID = Integer.Parse(ItemBinDr("ITEMCOLORID").ToString)
                    ItemColorID2 = Integer.Parse(ItemBinDr("ITEMCOLORID2").ToString)

                    If Len(ItemBinDr("BINITEMQTYPRIMARY").ToString) > 0 Then
                        BinItemQty = Decimal.Parse(ItemBinDr("BINITEMQTYPRIMARY").ToString)
                    Else
                        BinItemQty = 0
                    End If


                    If ItemSetQtyBalance > 0 And BinItemQty > 0 And ZoneItemID = ItemID And ZoneItemLength = ItemLength And ZoneItemColorID = ItemColorID And ZoneItemColorID2 = ItemColorID2 Then

                        If BinItemQty >= ItemSetQtyBalance Then
                            ItemSetQty = ItemSetQtyBalance
                            ItemSetQtyBalance = 0
                        Else
                            ItemSetQty = BinItemQty
                            ItemSetQtyBalance = ItemSetQtyBalance - BinItemQty
                        End If

                        WhBinCode = ItemBinDr("WHBINCODE").ToString
                        ItemCode = ItemBinDr("ITEMCODE").ToString
                        ColorDesc = ItemBinDr("COLORDESC").ToString
                        'ColorDesc2 = ItemBinDr("COLORDESC2").ToString
                        'INSERT VALUES
                        BinCandDr = BinCandDT.NewRow()
                        BinCandDr("WHBINCODE") = WhBinCode
                        BinCandDr("WHZONECODE") = WhZoneCode
                        BinCandDr("ITEMID") = ItemID
                        BinCandDr("ITEMCODE") = ItemCode
                        BinCandDr("ITEMLENGTH") = ItemLength

                        BinCandDr("ITEMCOLORID") = ItemColorID
                        BinCandDr("COLORDESC") = ColorDesc
                        BinCandDr("ITEMCOLORID2") = ItemColorID2
                        ' BinCandDr("COLORDESC2") = ColorDesc2
                        BinCandDr("ITEMSETQTY") = ItemSetQty

                        If ThisRouteID > 0 Then
                            BinCandDr("ROUTEID") = ThisRouteID
                        Else
                            BinCandDr("ROUTEID") = 0
                        End If
                        If ThisStoreTransID > 0 Then
                            BinCandDr("STORETRANSID") = ThisStoreTransID
                        Else
                            BinCandDr("STORETRANSID") = 0
                        End If
                        If ThisOrderID > 0 Then
                            BinCandDr("ORDERID") = ThisOrderID
                        Else
                            BinCandDr("ORDERID") = 0
                        End If

                        BinCandDT.Rows.Add(BinCandDr)

                    End If
                Next

            Next

        End If

        Dsr.DataSetName = "ZONEBINPICKINGITEMS"

        Dsr.Tables.Add(BinCandDT)
        Dsr.Tables(0).TableName = "ZONEBINPICKINGITEMS"

        Return Dsr
    End Function
    <WebMethod(Description:="Picking::Create Zone Picking Card")> _
    Public Function SOA_FCreateTaskPickingCard(ByVal CompID As Integer, ByVal BranchID As Integer, ByVal CardID As Long, ByVal WhRouteID As Long, ByVal RsrvStoreTransID As Long, ByVal OrderID As Long, ByVal ZoneItemsDs As DataSet, ByVal appuser As Integer, ByVal Terminal As String) As DataSet
        Dim SqlStr As String
        Dim Ds As New DataSet
        Dim Dsr As New DataSet
        Dim DT As DataTable
        Dim DTr As DataTable
        Dim DV As DataView
        Dim Dr As DataRow

        Dim WhZoneCode As String

        Dim WhCode, WhBinCode As String
        Dim WhBinID As Long
        Dim ItemCode, ItemDesc As String
        Dim ColorCode, ColorDesc As String
        Dim ColorCode2, ColorDesc2 As String
        Dim ItemID, ItemIDTmp As Long
        Dim ItemColorID As Integer
        Dim ItemColorID2 As Integer
        Dim ItemLength As Decimal
        Dim BinItemQty, OrdersBalanceQty As Decimal
        Dim ItemSetQty, ItemSetQtyBalance As Decimal



        Dim i, j As Long

        Dim ItemZoneDr, ItemBinDr, BinCandDr As DataRow
        Dim ZoneItemID As Long
        Dim ZoneItemColorID As Integer
        Dim ZoneItemColorID2 As Integer
        Dim ZoneItemLength As Decimal
        Dim ItemZonePQty As Decimal

        Dim ZoneRouteID, ZoneStoreTransID, ZoneOrderID, ZoneOrderDtlID As Long

        Dim MyWmsBinTrans As New WmsBinTrans
        '===
        ' Create CardID
        '===

        Dim ZonesFltr As String

        If ZoneItemsDs.Tables(0).Rows.Count > 0 Then
            ZonesFltr = " "
            For i = 0 To ZoneItemsDs.Tables(0).Rows.Count - 1
                ZonesFltr = ZonesFltr & "'" & ZoneItemsDs.Tables(0).Rows(i)("WHZONECODE").ToString & "'"
                If i < ZoneItemsDs.Tables(0).Rows.Count - 1 Then
                    ZonesFltr = ZonesFltr & ","
                End If
            Next
        End If

        SqlStr = " SELECT VWMSTASKSITEMBINLOCATIONS.WHCODE, VWMSTASKSITEMBINLOCATIONS.WHZONECODE,VWMSTASKSITEMBINLOCATIONS.WHBINID,VWMSTASKSITEMBINLOCATIONS.WHBINCODE,"
        SqlStr = SqlStr & "VWMSTASKSITEMBINLOCATIONS.ITEMID, VWMSTASKSITEMBINLOCATIONS.ITEMCODE,"
        SqlStr = SqlStr & "VWMSTASKSITEMBINLOCATIONS.ITEMDESC,VWMSTASKSITEMBINLOCATIONS.ITEMLENGTH,"
        SqlStr = SqlStr & "VWMSTASKSITEMBINLOCATIONS.ITEMCOLORID,VWMSTASKSITEMBINLOCATIONS.COLORCODE,VWMSTASKSITEMBINLOCATIONS.COLORDESC,VWMSTASKSITEMBINLOCATIONS.ITEMCOLORID2,VWMSTASKSITEMBINLOCATIONS.ORDERITEMQTYBALANCE,"
        SqlStr = SqlStr & "VWMSTASKSITEMBINLOCATIONS.BINITEMQTYPRIMARY "
        SqlStr = SqlStr & "FROM VWMSTASKSITEMBINLOCATIONS  "


        SqlStr = SqlStr & " WHERE VWMSTASKSITEMBINLOCATIONS.WHBINTYPE NOT IN(2,5,6) AND VWMSTASKSITEMBINLOCATIONS.BINITEMQTYPRIMARY > 0 AND VWMSTASKSITEMBINLOCATIONS.WHZONECODE IN (" & ZonesFltr & ")"

        If CompID > 0 Then
            SqlStr = SqlStr & " AND  VWMSTASKSITEMBINLOCATIONS.COMPID=" & CompID.ToString
            If BranchID > 0 Then
                SqlStr = SqlStr & " AND VWMSTASKSITEMBINLOCATIONS.BRANCHID=" & BranchID.ToString
            End If

        End If

        SqlStr = SqlStr & " ORDER BY VWMSTASKSITEMBINLOCATIONS.ITEMID,VWMSTASKSITEMBINLOCATIONS.ITEMLENGTH,VWMSTASKSITEMBINLOCATIONS.ITEMCOLORID,VWMSTASKSITEMBINLOCATIONS.BINITEMQTYPRIMARY DESC"

        Ds = MyWmsDBTrans.OraDBFillDataset(SqlStr, "ORDERSPICKINGITEMS")

        If Ds.Tables(0).Rows.Count > 0 And ZoneItemsDs.Tables(0).Rows.Count > 0 Then

            ItemSetQty = 0
            ItemSetQtyBalance = 0
            ItemIDTmp = 0


            For Each ItemZoneDr In ZoneItemsDs.Tables(0).Rows

                ZoneItemID = Long.Parse(ItemZoneDr("ITEMID").ToString)
                ZoneItemColorID = Long.Parse(ItemZoneDr("ITEMCOLORID").ToString)
                ZoneItemColorID2 = Long.Parse(ItemZoneDr("ITEMCOLORID2").ToString)
                ZoneItemLength = Decimal.Parse(ItemZoneDr("ITEMLENGTH").ToString)
                ItemZonePQty = Decimal.Parse(ItemZoneDr("ITEMSETQTY").ToString)


                Try
                    ZoneRouteID = Long.Parse(ItemZoneDr("ROUTEID").ToString)
                    ZoneStoreTransID = Long.Parse(ItemZoneDr("STORETRANSID").ToString)
                    ZoneOrderID = Long.Parse(ItemZoneDr("ORDERID").ToString)
                Catch ex As Exception
                End Try

                'ZoneOrderDtlID = Long.Parse(ItemZoneDr("SALESORDERDTLID").ToString)

                ItemSetQtyBalance = ItemZonePQty

                For Each ItemBinDr In Ds.Tables(0).Rows

                    ItemID = Long.Parse(ItemBinDr("ITEMID").ToString)
                    ItemLength = Decimal.Parse(ItemBinDr("ITEMLENGTH").ToString)
                    ItemColorID = Integer.Parse(ItemBinDr("ITEMCOLORID").ToString)
                    ItemColorID2 = Integer.Parse(ItemBinDr("ITEMCOLORID2").ToString)
                    'WhZoneCode = ItemBinDr("WHZONECODE").ToString

                    If Len(ItemBinDr("BINITEMQTYPRIMARY").ToString) > 0 Then
                        BinItemQty = Decimal.Parse(ItemBinDr("BINITEMQTYPRIMARY").ToString)
                    Else
                        BinItemQty = 0
                    End If


                    If ItemSetQtyBalance > 0 And BinItemQty > 0 And ZoneItemID = ItemID And ZoneItemLength = ItemLength And ZoneItemColorID = ItemColorID And ZoneItemColorID2 = ItemColorID2 Then

                        If BinItemQty >= ItemSetQtyBalance Then
                            ItemSetQty = ItemSetQtyBalance
                            ItemSetQtyBalance = 0
                        Else
                            ItemSetQty = BinItemQty
                            ItemSetQtyBalance = ItemSetQtyBalance - BinItemQty
                        End If

                        WhBinID = Long.Parse(ItemBinDr("WHBINID").ToString)
                        WhBinCode = ItemBinDr("WHBINCODE").ToString
                        ItemCode = ItemBinDr("ITEMCODE").ToString
                        ColorDesc = ItemBinDr("COLORDESC").ToString

                        MyWmsBinTrans.FCreateNewPickingCard(CompID, BranchID, CardID, WhBinID, WhBinCode, 0, WhZoneCode, ZoneStoreTransID, ZoneOrderID, ZoneOrderDtlID, ItemID, ItemLength, ItemColorID, ItemColorID2, ItemSetQty, 0, 0, 0, appuser, Terminal)

                    End If
                Next

            Next


        End If
        'Return Picking Card to the user


        SqlStr = " SELECT CARDID,COMPID,BRANCHID,WHBINCODE,WHBINID,WHZONEID,WHZONECODE,STORETRANSID,ORDERID,ORDERDTLID,"
        SqlStr = SqlStr & "ITEMID,ITEMCODE,ITEMDESC,ITEMLENGTH,ITEMCOLORID,COLORCODE,COLORDESC,ITEMCOLORID2,COLORCODE2,COLORDESC2,ITEMPRIMARYQTY,ITEMPRIMARYQTY,ITEMBALANCEQTY ,"
        SqlStr = SqlStr & "ITEMMUNITPRIMARY,ITEMSECONDARYQTY,ITEMMUNITSECONDARY,DBRECCREATEDATE,DBRECCREATEUSER,DBRECMODDATE,TERMINAL"
        SqlStr = SqlStr & " FROM VWMSPICKINGCARD WHERE CARDID=" & CardID.ToString
        SqlStr = SqlStr & "ORDER BY WHBINCODE "


        Dsr = MyWmsDBTrans.OraDBFillDataset(SqlStr, "PICKINGCARD")

        Return Dsr
    End Function

#End Region

#Region "Orders Shipping "

    <WebMethod(Description:="Shipping::Get Orders for directly picking from production")> _
    Public Function SOA_FGetOrderDetailsUndone(ByVal CompID As Integer, ByVal BranchID As Integer, ByVal OrderID As Long, ByVal OrderCode As String) As DataSet
        Dim Ds As New DataSet

        Dim SqlStr As String
        Dim MyWmsDBtrans As New WmsDBTrans

        SqlStr = "SELECT CUSTOMERID,CUSTOMERTITLE,ORDERID,ORDERCODE,ORDERDTLID,ITEMID,ITEMCODE,ITEMQTYPRIMARY,SHIPPEDPQTY,"
        SqlStr = SqlStr & "ITEMLENGTH,ITEMCOLORID,ITEMCOLORID2,COLORCODE,COLORDESC, "
        SqlStr = SqlStr & "ORDERDATE,0 AS ENTEREDONCE,NVL(PROMISEDDELIVDATE,ORDERDATE) AS PROMISEDDELIVDATE,TO_CHAR( NVL(PROMISEDDELIVDATE,ORDERDATE),'DD/MM/YY') AS PROMISDELDATECHR,ITEMQTYBALANCEFORSHIPP,ITEMQTYBALANCEFORSHIPP AS INITITEMQTYBALANCE "
        SqlStr = SqlStr & "FROM VWMSORDERSSTATUSFULLUNDONE2 WHERE "

        If OrderID > 0 Then
            SqlStr = SqlStr & " ORDERID=" & OrderID.ToString
        ElseIf Len(OrderCode) > 0 And (OrderCode <> " " Or OrderCode <> "NULL") Then
            If CompID > 0 Then
                SqlStr = SqlStr & " COMPID=" & CompID.ToString
                If BranchID > 0 Then
                    SqlStr = SqlStr & " AND BRANCHID = " & BranchID.ToString
                End If
                SqlStr = SqlStr & " AND "
            End If
            SqlStr = SqlStr & " ORDERCODE= '" & OrderCode & "'"
        End If
        SqlStr = SqlStr & "ORDER BY ITEMCODE,COLORCODE,ITEMLENGTH"

        Ds = MyWmsDBtrans.OraDBFillDataset(SqlStr, "ORDERITEMS")
        Return Ds
    End Function
    <WebMethod(Description:="Shipping::Get Orders Undone By Item")> _
    Public Function SOA_FGetOrderUndoneByItem(ByVal CompID As Integer, ByVal BranchID As Integer, ByVal ItemID As Long, ByVal ItemColorid As Integer, ByVal itemlength As Decimal) As DataSet
        Dim Ds As New DataSet

        Dim SqlStr As String
        Dim MyWmsDBtrans As New WmsDBTrans

        SqlStr = "SELECT CUSTOMERID,CUSTOMERTITLE,ORDERID,ORDERCODE,ORDERDTLID,ITEMID,ITEMCODE,ITEMQTYPRIMARY,SHIPPEDPQTY,"
        SqlStr = SqlStr & "ITEMLENGTH,ITEMCOLORID,ITEMCOLORID2,COLORCODE,COLORDESC, "
        SqlStr = SqlStr & "ORDERDATE,0 AS ENTEREDONCE,NVL(PROMISEDDELIVDATE,ORDERDATE) AS PROMISEDDELIVDATE,TO_CHAR( NVL(PROMISEDDELIVDATE,ORDERDATE),'DD/MM/YY') AS PROMISDELDATECHR,ITEMQTYBALANCEFORSHIPP,ITEMQTYBALANCEFORSHIPP AS INITITEMQTYBALANCE "
        SqlStr = SqlStr & "FROM VWMSORDERSSTATUSFULLUNDONE2 WHERE ITEMQTYBALANCEFORSHIPP > 0 AND "


        If CompID > 0 Then
            SqlStr = SqlStr & " COMPID=" & CompID.ToString
            If BranchID > 0 Then
                SqlStr = SqlStr & " AND BRANCHID = " & BranchID.ToString
            End If
            SqlStr = SqlStr & " AND "
        End If

        If ItemID > 0 Then
            SqlStr = SqlStr & "  ITEMID=" & ItemID.ToString()
        End If
        If ItemColorid > 0 Then
            SqlStr = SqlStr & " AND ITEMCOLORID=" & ItemColorid.ToString()
        End If
        If itemlength > 0 Then
            SqlStr = SqlStr & " AND ITEMLENGTH=" & itemlength.ToString().Replace(",", ".")
        End If

        SqlStr = SqlStr & " ORDER BY ITEMQTYBALANCEFORSHIPP DESC "

        Ds = MyWmsDBtrans.OraDBFillDataset(SqlStr, "ORDERITEMS")
        Return Ds
    End Function
    <WebMethod(Description:="Shipping::Get Trans BinCode By CardID")> _
    Public Function SOA_GetTransBinCode(ByVal CardID As Long) As String
        Dim SqlStr As String
        Dim TransBinCode

        SqlStr = "SELECT DISTINCT TRANSBINCODE FROM TWMSPICKINGCARD WHERE CARDID=" & CardID.ToString

        TransBinCode = MyWmsDBTrans.OraDBWmsExSelectCmdRStr2Str(SqlStr)

        If Len(TransBinCode) > 0 And (TransBinCode <> "Null" Or TransBinCode <> "NULL") Then
            Return TransBinCode
        Else
            Return "-1"
        End If
    End Function

    <WebMethod(Description:="Shipping::Get Orders/Trans Docs With Picked Items")> _
    Public Function SOA_GetOrdersWithPickedItems(ByVal CompID As Integer, ByVal BranchID As Integer, ByVal TransBinCode As String) As DataSet

        Dim SqlStr As String
        Dim Ds As New DataSet

        Dim I As Integer
        Dim DTColumn As DataColumn

        SqlStr = "SELECT DISTINCT WMSDESTINATION ,CUSTOMERTITLE,ORDERCODE,SALESORDERID,STORETRANSID,PROMISEDDELIVDATE FROM VWMSORDERITEMSBINLOCATIONS "
        SqlStr = SqlStr & "WHERE WHBINCODE = '" & TransBinCode & "'"
        If CompID > 0 Then
            SqlStr = SqlStr & " AND  COMPID=" & CompID.ToString
            If BranchID > 0 Then
                SqlStr = SqlStr & " AND   BRANCHID=" & BranchID.ToString
            End If
        End If

        SqlStr = SqlStr & " ORDER BY PROMISEDDELIVDATE"

        Ds = MyWmsDBTrans.OraDBFillDataset(SqlStr, "PICKEDORDERSLIST")


        If Ds.Tables(0).Rows.Count > 0 Then
            DTColumn = New DataColumn

            DTColumn.DataType = System.Type.GetType("System.Int32")
            DTColumn.ColumnName = "ID"
            DTColumn.AutoIncrement = False
            Ds.Tables(0).Columns.Add(DTColumn)

            For I = 0 To Ds.Tables(0).Rows.Count - 1
                Ds.Tables(0).Rows(I)("ID") = I + 1
            Next
        End If

        Return Ds
    End Function

    <WebMethod(Description:="Shipping::Get Orders/Trans Docs Picked With Picked Items")> _
    Public Function SOA_GetPickedOrderItems(ByVal CompID As Integer, ByVal BranchID As Integer, ByVal RsrvStoreTransID As Long) As DataSet

        Dim SqlStr As String
        Dim Ds As New DataSet
        Dim I As Integer
        Dim DTColumn As DataColumn

        SqlStr = "SELECT WMSDESTINATION ,CUSTOMERTITLE,ORDERCODE,SALESORDERID,STORETRANSID,PROMISEDDELIVDATE,TO_CHAR(PROMISEDDELIVDATE,'D/MM/YY') AS PROMISDELDATECHR,"
        SqlStr = SqlStr & "ITEMID,ITEMCODE,ITEMDESC,ITEMLENGTH,ITEMCOLORID,COLORCODE,COLORDESC,ITEMCOLORID,COLORCODE2,COLORDESC2,"
        SqlStr = SqlStr & "WHBINID,WHBINCODE,ORDERITEMQTYBALANCE,BINITEMQTYPRIMARY "
        SqlStr = SqlStr & " FROM VWMSORDERITEMSBINLOCATIONS "

        SqlStr = SqlStr & "WHERE STORETRANSID = " & RsrvStoreTransID.ToString
        'If CompID > 0 Then
        '    SqlStr = SqlStr & " AND  COMPID=" & CompID.ToString
        '    If BranchID > 0 Then
        '        SqlStr = SqlStr & " AND   BRANCHID=" & BranchID.ToString
        '    End If
        'End If

        Ds = MyWmsDBTrans.OraDBFillDataset(SqlStr, "PICKEDORDERITEMS")

        If Ds.Tables(0).Rows.Count > 0 Then
            DTColumn = New DataColumn

            DTColumn.DataType = System.Type.GetType("System.Int32")
            DTColumn.ColumnName = "ID"
            DTColumn.AutoIncrement = False
            Ds.Tables(0).Columns.Add(DTColumn)

            For I = 0 To Ds.Tables(0).Rows.Count - 1
                Ds.Tables(0).Rows(I)("ID") = I + 1
            Next
        End If

        Return Ds
    End Function

    <WebMethod(Description:="Shipping::Get Picked Items From Paletizing Area")> _
    Public Function SOA_GetPalPickedItems(ByVal CompID As Integer, ByVal BranchID As Integer, ByVal PalBinCode As String) As DataSet

        Dim MyWmsBinTRans As New WmsBinTrans

        Dim Ds As New DataSet

        Dim I As Integer
        Dim DTColumn As DataColumn
        Dim WhBinID As Long
        Dim ThisWhBinCode As String

        Dim SqlStr As String

        If Len(PalBinCode) = 5 Then
            WhBinID = MyWmsBinTRans.FGetBinIDByOldCode(CompID, BranchID, PalBinCode)
        ElseIf Len(PalBinCode) > 3 Then
            WhBinID = MyWmsBinTRans.FGetBinIDByCode(CompID, BranchID, PalBinCode)
        End If

        SqlStr = "SELECT ITEMID,ITEMCODE,ITEMDESC,ITEMLENGTH,ITEMCOLORID,ITEMCOLORID2,COLORCODE,COLORDESC,COLORCODE2,COLORDESC2,"
        SqlStr = SqlStr & "BINITEMQTYPRIMARY,BINITEMQTYSECONDARY FROM VWMSBINSTATUS "

        If WhBinID > 0 Then
            SqlStr = SqlStr & "WHERE WHBINID=" & WhBinID.ToString
        Else
            SqlStr = SqlStr & "WHERE WHBINTYPE IN(4,6)"
        End If

        SqlStr = SqlStr & " ORDER BY ITEMCODE "

        Ds = MyWmsDBTrans.OraDBFillDataset(SqlStr, "PALPICKEDITEMS")

        If Ds.Tables(0).Rows.Count > 0 Then
            DTColumn = New DataColumn

            DTColumn.DataType = System.Type.GetType("System.Int32")
            DTColumn.ColumnName = "ID"
            DTColumn.AutoIncrement = False
            Ds.Tables(0).Columns.Add(DTColumn)

            For I = 0 To Ds.Tables(0).Rows.Count - 1
                Ds.Tables(0).Rows(I)("ID") = I + 1
            Next
        End If
        Return Ds
    End Function
    <WebMethod(Description:="Shipping - Create Customer Palette")> _
    Public Function SOA_CreateCustomerPalette(ByVal CompID As Integer, ByVal BranchID As Integer, ByVal WhBinCode As String, ByVal CustomerID As Long, ByVal RouteID As Long, ByVal RsrvStoreTransID As Long, ByVal ItemID As Long, ByVal ItemColorID As Integer, ByVal ItemColorID2 As Integer, ByVal OrderID As Long, ByVal ProdOrderID As Long, ByVal ProdORStore As Integer, ByVal AppUser As Integer, ByVal Terminal As String) As String

        Dim MyWmsBinTrans As New WmsBinTrans
        Dim PaletteNoStr As String

        Dim CrRtrn As Long

        Dim AccItemID As Long

        Dim ITemGroupID, ItemCatID As Integer
        Dim DsCatItems As New DataSet

        Dim PaletteType As Integer  '{1:Bins,2:Shipping}
        '
        PaletteType = 2
        '
        Try
            DsCatItems = MyWmsBinTrans.FGetItemCategories(ItemID)
            If DsCatItems.Tables(0).Rows.Count > 0 Then
                ITemGroupID = Long.Parse(DsCatItems.Tables(0).Rows(0)("GROUPITEMID").ToString())
                ItemCatID = Long.Parse(DsCatItems.Tables(0).Rows(0)("MAINITEMCATID").ToString())
            End If
        Catch ex As Exception

        End Try

        AccItemID = MyWmsBinTrans.FGetAccountMaterialID(CompID, BranchID, ITemGroupID, ItemColorID, ItemColorID2)

        PaletteNoStr = MyWmsBinTrans.FCreatePaletteNo(CompID, BranchID, PaletteType)

        CrRtrn = MyWmsBinTrans.FCreateCustomerPalette(CompID, BranchID, PaletteNoStr, WhBinCode, CustomerID, RouteID, AccItemID, ItemCatID, RsrvStoreTransID, OrderID, ProdOrderID, ProdORStore)

        If CrRtrn > 0 Then
            Return PaletteNoStr
        Else
            Return "-1"
        End If

    End Function
    <WebMethod(Description:="Shipping - Add to Customer  Palette")> _
    Public Function SOA_FAddToCustomerPaletteTop(ByVal CompID As Integer, ByVal BranchID As Integer, ByVal PackitemID As Long, ByVal IsNewPalette As Integer, ByVal WhBinCodeFrom As String, ByVal WhBinCodeTo As String, ByVal PackItemNo As String, ByVal FRsrvStoreTransID As Long, ByVal FOrderID As Long, ByVal OrderDtlID As Long, ByVal ItemID As Long, ByVal ItemLength As Decimal, ByVal ItemColorID As Integer, ByVal ItemColorID2 As Integer, ByVal ItemPQty As Decimal, ByVal ItemSQty As Decimal, ByVal ProdOrStore As Integer, ByVal AppUser As Integer, ByVal Terminal As String) As String

        Dim MyWmsBinTrans As New WmsBinTrans

        Dim Sqlstr

        Dim excnt As String
        Dim FSqty As Decimal
        Dim CrRtrn As Long
        Dim CrRtrnStr As String

        'prodor store {2:from production,3: from picked items}
        If IsNewPalette = 0 Then

            Sqlstr = "SELECT COUNT(*) AS excnt FROM   TPACKAGESDTL  WHERE PACKITEMID=" & PackitemID.ToString

            Sqlstr = Sqlstr & " AND ORDERDTLID = " & OrderDtlID.ToString

            excnt = MyWmsDBTrans.OraDBWmsExSelectCmdRN2String(Sqlstr)

            If IsNumeric(excnt) And PackitemID > 0 Then
                If Long.Parse(excnt) Then
                    If ItemSQty > 0 Then
                        FSqty = ItemSQty
                    ElseIf ItemPQty > 0 Then
                        FSqty = MyWmsBinTrans.FCalcItemWeightQty(ItemID, ItemLength, ItemPQty, CompID)
                    Else
                        FSqty = 0
                    End If

                    CrRtrn = MyWmsBinTrans.FUpdateCustomerPalette(CompID, BranchID, PackitemID, FOrderID, OrderDtlID, ItemID, ItemLength, ItemColorID, ItemColorID2, ItemPQty, FSqty)
                    Return CrRtrn.ToString
                End If
            End If
        End If

        'else
        CrRtrnStr = SOA_FAddToCustomerPalette(CompID, BranchID, WhBinCodeFrom, WhBinCodeTo, PackItemNo, FRsrvStoreTransID, FOrderID, OrderDtlID, ItemID, ItemLength, ItemColorID, ItemColorID2, ItemPQty, ItemSQty, ProdOrStore, AppUser, Terminal)
        Return CrRtrnStr


    End Function

    <WebMethod(Description:="Shipping - Add to Customer  Palette")> _
    Public Function SOA_FAddToCustomerPalette(ByVal CompID As Integer, ByVal BranchID As Integer, ByVal WhBinCodeFrom As String, ByVal WhBinCodeTo As String, ByVal PackItemNo As String, ByVal FRsrvStoreTransID As Long, ByVal FOrderID As Long, ByVal OrderDtlID As Long, ByVal ItemID As Long, ByVal ItemLength As Decimal, ByVal ItemColorID As Integer, ByVal ItemColorID2 As Integer, ByVal ItemPQty As Decimal, ByVal ItemSQty As Decimal, ByVal ProdOrStore As Integer, ByVal AppUser As Integer, ByVal Terminal As String) As String
        Dim MyWmsBinTrans As New WmsBinTrans

        Dim SqlStr As String
        Dim SqlStr2 As String
        Dim PackItemID As Long

        Dim CrntOrderID As Long
        Dim ThisBinCodeTo As String

        Dim WhBinIDFrom, WhBinIDTo As Long

        Dim CrRtrn As Long

        Dim UpdBinQtyRtrn As Long
        Dim AddBinQtyRtrn As Long

        Dim ItemPMunit, ItemSMunit As Integer

        Dim NlValue As String
        Dim ExPaletteBinCode As String
        Dim IsNewPalette As Boolean

        Dim WH As String

        Dim DsPackInfo As New DataSet
        Dim DsCatItems As New DataSet

        Dim FSqty As Decimal

        Dim Rtrn As Long

        Dim IsAnodized As Integer
        Dim IsThermoBreak As Integer

        Dim PickingSrcBinCode As String
        Dim UpdProdWhBinID As Long
        Dim UpdProdBins As Integer ' this is like a boolean 
        Dim UnpColorID As Integer

        IsAnodized = -1 'default not anodized
        IsThermoBreak = -1 'default not thermobreak

        'ProdOrStore {2:prod bins,3:Picked items Bins}

        If ProdOrStore = 2 Then
            Try
                UpdProdBins = MyWmsBinTrans.FCheckfiProdinWtyUpdatable(CompID)
            Catch ex As Exception
                UpdProdBins = 0
            End Try
        End If

        If UpdProdBins = 1 And ProdOrStore = 2 Then

            Try
                UnpColorID = Integer.Parse(MyWmsDBTrans.OraDBWmsExSelectCmdRN2String("SELECT UNPCOLORID FROM TORDERSPARAMS"))
            Catch ex As Exception
                UnpColorID = 0
            End Try

            If ItemColorID <> UnpColorID Then
                Try
                    If Integer.Parse(MyWmsDBTrans.OraDBWmsExSelectCmdRN2String("SELECT ANODIZED FROM TITEMCOLORS WHERE COLORID=" & ItemColorID.ToString())) = 1 Then
                        IsAnodized = 1
                    End If
                Catch ex As Exception
                    IsAnodized = -1
                End Try
            End If

            Try
                If IsAnodized = 1 Then
                    UpdProdWhBinID = Long.Parse(MyWmsDBTrans.OraDBWmsExSelectCmdRN2String(" SELECT WHBINID FROM TPRODPHASES WHERE PRODPHASEID = (SELECT  ANODPHASE FROM TPRODPARAMS ) "))
                Else
                    UpdProdWhBinID = Long.Parse(MyWmsDBTrans.OraDBWmsExSelectCmdRN2String(" SELECT WHBINID FROM TPRODPHASES WHERE PRODPHASEID = (SELECT  PAINTPHASE FROM TPRODPARAMS ) "))
                End If
            Catch ex As Exception
                UpdProdWhBinID = 0
            End Try

        End If


        If ItemSQty > 0 Then
            FSqty = ItemSQty
        ElseIf ItemPQty > 0 Then
            FSqty = MyWmsBinTrans.FCalcItemWeightQty(ItemID, ItemLength, ItemPQty, CompID)
        Else
            FSqty = 0
        End If
        '
        'FSqty = Long.Parse(FSqty)
        '
        Dim PalERPAccITemID, ThisERPAccItemID, PalItemCatID, ItemGroupID, ItemCatID As Long
        IsNewPalette = True
        CrRtrn = 0

        '!!!!ProdOrStore {1:directly from production,0 from stock}
        If ProdOrStore = 3 Or ProdOrStore = 4 Then 'src from picked items
            PickingSrcBinCode = SOA_FPickingSuggestBinCode(CompID, BranchID, 0)
            WhBinCodeFrom = PickingSrcBinCode
            'ElseIf ProdOrStore = 2 And UpdProdWhBinID > 0 Then
            '    WhBinCodeFrom = MyWmsBinTrans.FGetBinCodeByID(CompID, BranchID, UpdProdWhBinID)
        End If


        If Len(PackItemNo) > 0 Then
            DsPackInfo = MyWmsBinTrans.FGetPackItemTinyInfo(CompID, BranchID, 0, PackItemNo)

            If DsPackInfo.Tables(0).Rows.Count > 0 Then
                If IsDBNull(DsPackInfo.Tables(0).Rows(0)("PACKITEMID")) = False Then
                    PackItemID = Long.Parse(DsPackInfo.Tables(0).Rows(0)("PACKITEMID").ToString)
                End If
                'Check if has same destination
                'CrntOrderID
                Try
                    If IsDBNull(DsPackInfo.Tables(0).Rows(0)("ORDERID")) = False Then
                        CrntOrderID = Long.Parse(DsPackInfo.Tables(0).Rows(0)("ORDERID").ToString)
                    End If
                Catch ex As Exception
                End Try
                If CrntOrderID > 0 And FOrderID > 0 And CrntOrderID <> FOrderID Then
                    Rtrn = MyWmsBinTrans.FCheckPaletteDestination(PackItemID, FOrderID)
                    If Rtrn = -1 Then
                        Return "-11" 'Different Destination 
                    End If
                End If


                'Check if the item can be in the same palette
                If IsDBNull(DsPackInfo.Tables(0).Rows(0)("ERPITEMID")) = False Then
                    PalERPAccITemID = Long.Parse(DsPackInfo.Tables(0).Rows(0)("ERPITEMID").ToString)
                    PalItemCatID = Long.Parse(DsPackInfo.Tables(0).Rows(0)("MAINITEMCATID").ToString)
                    PalERPAccITemID = Long.Parse(DsPackInfo.Tables(0).Rows(0)("ERPITEMID").ToString)
                    'ItemGroupID = MyWmsBinTrans.FGetItemGroupID(ItemID)
                    Try
                        DsCatItems = MyWmsBinTrans.FGetItemCategories(ItemID)

                        If DsCatItems.Tables(0).Rows.Count > 0 Then
                            ItemGroupID = Long.Parse(DsCatItems.Tables(0).Rows(0)("GROUPITEMID").ToString())
                            ItemCatID = Long.Parse(DsCatItems.Tables(0).Rows(0)("MAINITEMCATID").ToString())
                        End If

                    Catch ex As Exception

                    End Try

                    ThisERPAccItemID = MyWmsBinTrans.FGetAccountMaterialID(CompID, BranchID, ItemGroupID, ItemColorID, ItemColorID2)
                    If ThisERPAccItemID <> PalERPAccITemID Or PalItemCatID <> ItemCatID Then
                        Return "-10" ' Different item for this palette
                    End If
                    'ThisERPAccItemID 
                End If
            End If

            If PackItemID > 0 Then
                CrRtrn = MyWmsBinTrans.FAddToCustomerPalette(CompID, BranchID, PackItemID, FOrderID, OrderDtlID, ItemID, ItemLength, ItemColorID, ItemColorID2, ItemPQty, FSqty)

                SqlStr = "SELECT BINCODE FROM TPACKAGES WHERE PACKITEMID=" & PackItemID.ToString
                ExPaletteBinCode = MyWmsDBTrans.OraDBWmsExSelectCmdRStr2Str(SqlStr)

                If Len(ExPaletteBinCode) > 0 And (ExPaletteBinCode <> "NULL" Or ExPaletteBinCode <> "Null") Then
                    IsNewPalette = False
                End If

            End If
        End If

        If Len(WhBinCodeTo) > 0 And (WhBinCodeTo <> "NULL" Or WhBinCodeTo <> "Null") Then
            ThisBinCodeTo = WhBinCodeTo
        Else
            SqlStr = "SELECT  MIN(WHBINCODE) FROM TWMSBINS WHERE WHBINTYPE = 2 "
            ThisBinCodeTo = MyWmsDBTrans.OraDBWmsExSelectCmdRStr2Str(SqlStr)
        End If

        If Len(ThisBinCodeTo) > 0 Then IsNewPalette = False

        If UpdProdWhBinID > 0 Then
            WhBinIDFrom = UpdProdWhBinID
        ElseIf Len(WhBinCodeFrom) > 0 Then
            WhBinIDFrom = MyWmsBinTrans.FGetBinIDByCode(CompID, BranchID, WhBinCodeFrom)
        End If

        WhBinIDTo = MyWmsBinTrans.FGetBinIDByCode(CompID, BranchID, ThisBinCodeTo)


        ItemPMunit = MyWmsBinTrans.FGetItemMUnitPrimary(ItemID)
        ItemSMunit = MyWmsBinTrans.FGetItemMUnitSecondary(ItemID)

        If WhBinIDFrom > 0 Then
            If ProdOrStore = 2 Or ProdOrStore = 3 Or ProdOrStore = 4 Then 'src is production 
                UpdBinQtyRtrn = MyWmsBinTrans.FUpdateBinQtyAlter(CompID, BranchID, -1, NlValue, WhBinIDFrom, ItemID, 0, ItemLength, ItemColorID, ItemColorID2, ItemPQty, ItemPMunit, FSqty, ItemSMunit, 0, 0, 0, 0, 0, AppUser, Terminal)
            Else
                UpdBinQtyRtrn = MyWmsBinTrans.FUpdateBinQty(CompID, BranchID, -1, NlValue, WhBinIDFrom, ItemID, 0, ItemLength, ItemColorID, ItemColorID2, ItemPQty, ItemPMunit, FSqty, ItemSMunit, 0, 0, FOrderID, OrderDtlID, FRsrvStoreTransID, AppUser, Terminal)
            End If

            If ProdOrStore = 3 Then
                MyWmsBinTrans.FOrderRemoveReservedItems(CompID, BranchID, OrderDtlID, ItemPQty, ProdOrStore)
            End If
        Else
            UpdBinQtyRtrn = ItemPQty ' indicates that there is no source in the warehouse!!next line
        End If

        If WhBinIDTo > 0 Then
            CrRtrn = CrRtrn + 1

            AddBinQtyRtrn = MyWmsBinTrans.FUpdateBinQty(CompID, BranchID, 1, PackItemNo, WhBinIDTo, ItemID, 0, ItemLength, ItemColorID, ItemColorID2, ItemPQty, ItemPMunit, FSqty, ItemSMunit, 0, 0, FOrderID, OrderDtlID, FRsrvStoreTransID, AppUser, Terminal)

            If AddBinQtyRtrn > 0 Then CrRtrn = CrRtrn + 1

            If PackItemID > 0 Then
                MyWmsBinTrans.FUpdatePaletteLocation(CompID, BranchID, PackItemNo, ThisBinCodeTo, IsNewPalette)
            End If

        End If

        If CrRtrn > 0 Then

            Return CrRtrn.ToString
        Else
            Return "-1"
        End If

    End Function
    <WebMethod(Description:="Shipping - Check Customer  Palette")> _
    Public Function SOA_FCheckCustomerPalette(ByVal CompID As Integer, ByVal BranchID As Integer, ByVal PackItemID As Long, ByVal PackItemNo As String, ByVal FRsrvStoreTransID As Long, ByVal FOrderID As Long, ByVal OrderDtlID As Long, ByVal ItemID As Long, ByVal ItemLength As Decimal, ByVal ItemColorID As Integer, ByVal ItemColorID2 As Integer, ByVal ItemPQty As Decimal, ByVal ItemSQty As Decimal, ByVal ProdOrStore As Integer, ByVal AppUser As Integer, ByVal Terminal As String) As String
        Dim MyWmsBinTrans As New WmsBinTrans

        Dim SqlStr As String

        Dim CrRtrn As Long
        Dim FSqty As Decimal

        If ItemSQty > 0 Then
            FSqty = ItemSQty
        ElseIf ItemPQty > 0 Then
            FSqty = MyWmsBinTrans.FCalcItemWeightQty(ItemID, ItemLength, ItemPQty, CompID)
        Else
            FSqty = 0
        End If
        'convert to long
        'FSqty = Long.Parse(FSqty)
        ''
        CrRtrn = MyWmsBinTrans.FUpdateCustomerPalette(CompID, BranchID, PackItemID, FOrderID, OrderDtlID, ItemID, ItemLength, ItemColorID, ItemColorID2, ItemPQty, FSqty)
        Return CrRtrn.ToString

    End Function

    <WebMethod(Description:="Shipping - Orders Picked ITems belong to")> _
    Public Function SOA_FGetOrdersListByPickedItem(ByVal CompID As Integer, ByVal BranchID As Integer, ByVal ItemID As Long, ByVal ItemLength As Decimal, ByVal ItemColorID As Integer, ByVal ItemColorID2 As Integer) As DataSet
        Dim SqlStr As String

        Dim MyWmsBinTRans As New WmsBinTrans
        Dim Ds As New DataSet

        Dim PlusWhere As String

        PlusWhere = " WHERE "

        SqlStr = "SELECT DISTINCT STORETRANSID,SALESORDERID,ORDERCODE,CUSTOMERID,CUSTOMERTITLE,"
        SqlStr = SqlStr & "SALESORDERDTL,ITEMID,ITEMCODE,ITEMDESC,ITEMLENGTH,ITEMCOLORID,ITEMCOLORID2,COLORCODE,COLORDESC,COLORCODE2,COLORDESC2,ITEMQTYPRIMARY,ORDERITEMQTYBALANCE,"
        SqlStr = SqlStr & "ORDERDATE,PROMISEDDELIVDATE,TO_CHAR( PROMISEDDELIVDATE,'DD/MM/YY') AS PROMISDELDATECHR  FROM VWMSORDERSWMSTASKSQTY "

        If CompID > 0 Then
            PlusWhere = " AND "
            SqlStr = SqlStr & "WHERE COMPID = " & CompID.ToString
            If BranchID > 0 Then
                SqlStr = SqlStr & " AND BRANCHID=" & BranchID.ToString
            End If
        End If

        If ItemID > 0 Then
            SqlStr = SqlStr & PlusWhere & " ITEMID=" & ItemID.ToString

            If ItemLength > 0 Then
                SqlStr = SqlStr & " AND ITEMLENGTH=" & MyWmsBinTRans.f_dbinsertdecimal(ItemLength, 3)
            End If
            If ItemColorID > 0 Then
                SqlStr = SqlStr & " AND ITEMCOLORID=" & ItemColorID.ToString
            End If
            If ItemColorID > 0 Then
                SqlStr = SqlStr & " AND ITEMCOLORID2=" & ItemColorID2.ToString
            End If
        End If

        SqlStr = SqlStr & " ORDER BY PROMISEDDELIVDATE  "

        If Len(SqlStr) > 0 Then
            Ds = MyWmsDBTrans.OraDBFillDataset(SqlStr, "ORDERSPICKINGLIST")
        End If

        Return Ds
    End Function
    <WebMethod(Description:="Shipping - Get Order Palettes")> _
    Public Function SOA_FGetOrderShippingPalettes(ByVal StoreTransID As Long, ByVal OrderID As Long) As DataSet

        Dim MyWmsDBTrans As New WmsDBTrans

        Dim ds As New DataSet
        Dim SqlStr As String

        SqlStr = "SELECT PACKITEMID,PACKITEMNO,PACKDATE,CUSTOMERID,ORDERID,BINCODE,INWAREHOUSE "
        SqlStr = SqlStr & "FROM VWMSPACKAGES WHERE "
        If StoreTransID > 0 Then
            SqlStr = SqlStr & " RESRVTORETRANSID = " & StoreTransID.ToString
        ElseIf OrderID > 0 Then
            SqlStr = SqlStr & " ORDERID = " & OrderID.ToString
        Else
            ds = Nothing
            Return ds
        End If

        ds = MyWmsDBTrans.OraDBFillDataset(SqlStr, "ORDERPALETTES")
        Return ds

    End Function
    <WebMethod(Description:="Shipping - Get Order Palettes")> _
    Public Function SOA_FGetCustomerShippingPalettes(ByVal CustomerID As Long) As DataSet

        Dim MyWmsDBTrans As New WmsDBTrans

        Dim ds As New DataSet
        Dim SqlStr As String

        SqlStr = "SELECT TPACKAGES.PACKITEMID,TPACKAGES.PACKITEMNO,TPACKAGES.PACKDATE,TPACKAGES.CUSTOMERID,TPACKAGES.ORDERID,TPACKAGES.PRODORDERID,TPACKAGES.BINCODE,TPACKAGES.INWAREHOUSE,TPACKAGES.MAINITEMCATID, "
        SqlStr = SqlStr & "TPACKAGES.PACKITEMWEIGHT,TITEMMAINCAT.MAINITEMCATDESC "
        SqlStr = SqlStr & "FROM TPACKAGES,TITEMMAINCAT WHERE TPACKAGES.MAINITEMCATID = TITEMMAINCAT.MAINITEMCATID (+) AND  TPACKAGES.INWAREHOUSE > -1 AND "

        SqlStr = SqlStr & " TPACKAGES.CUSTOMERID = " & CustomerID.ToString
        SqlStr = SqlStr & " ORDER BY TPACKAGES.PACKITEMID DESC "

        ds = MyWmsDBTrans.OraDBFillDataset(SqlStr, "CUSTOMERPALETTES")
        Return ds

    End Function

    <WebMethod(Description:="Shipping - Get Customer Palette Info")> _
    Public Function SOA_FGetCustomerPaletteInfo(ByVal CompID As Integer, ByVal PackItemNo As String) As DataSet
        Dim Ds As New DataSet
        Dim MyWmsDBTrans As New WmsDBTrans

        Dim sqlstr As String
        sqlstr = "SELECT * FROM VWMSCUSTPACKITEMNOSTATUS WHERE "
        If CompID > 0 Then
            sqlstr = sqlstr & "COMPID=" & CompID.ToString & " AND "
        End If
        sqlstr = sqlstr & "PACKITEMNO='" & PackItemNo & "'"

        Ds = MyWmsDBTrans.OraDBFillDataset(sqlstr, "CUSTPALETTEINFO")
        Return Ds
    End Function

    <WebMethod(Description:="Shipping - Set Palette Status Ready for shipping")> _
    Public Function SOA_FSetPaletteReadyforShipp(ByVal CompID As Integer, ByVal PackItemNo As String) As String

        Dim MyWmsDBTrans As New WmsDBTrans

        Dim Rtrn As Long
        Dim SqlStr As String

        SqlStr = "UPDATE TPACKAGES SET INWAREHOUSE=2 WHERE "
        If CompID > 0 Then
            SqlStr = SqlStr & " COMPID=" & CompID.ToString & " AND "
        End If
        SqlStr = SqlStr & "PACKITEMNO='" & PackItemNo.ToString & "'"


        Rtrn = MyWmsDBTrans.OraDBExecuteSQLCmd(SqlStr, 1)

        If Rtrn > 0 Then
            Return "1"
        Else
            Return "-1"
        End If


    End Function

    <WebMethod(Description:="Shipping - Set Palette Status Ready for shipping")> _
    Public Function SOA_FSetPaletteWeight(ByVal CompID As Integer, ByVal PAckItemNo As String, ByVal PackItemID As Long, ByVal PackItemWeight As Long) As String

        Dim MyWmsDBTrans As New WmsDBTrans

        Dim Rtrn As Long
        Dim SqlStr As String

        SqlStr = "UPDATE TPACKAGES SET INWAREHOUSE=2,PACKITEMWEIGHT=" & PackItemWeight.ToString
        If CompID > 0 Then
            SqlStr = SqlStr & " WHERE COMPID = " & CompID.ToString & " AND "
        Else
            SqlStr = SqlStr & " WHERE"
        End If
        If PackItemID > 0 Then
            SqlStr = SqlStr & " PACKITEMID=" & PackItemID.ToString
        ElseIf Len(PAckItemNo) > 0 Then
            SqlStr = SqlStr & " PACKITEMNO='" & PAckItemNo & "'"
        End If

        Rtrn = MyWmsDBTrans.OraDBExecuteSQLCmd(SqlStr, 1)

        Try
            If PackItemID > 0 And Long.Parse(MyWmsDBTrans.OraDBWmsExSelectCmdRN2String("SELECT COUNT(*) FROM TPACKAGESDTL WHERE PACKITEMID=" & PackItemID.ToString)) = 1 Then
                MyWmsDBTrans.OraDBExecuteSQLCmd("UPDATE  TPACKAGESDTL SET ITEMWEIGHT=" & PackItemWeight.ToString & " WHERE PACKITEMID=" & PackItemID.ToString, 1)
                MyWmsDBTrans.OraDBExecuteSQLCmd("UPDATE  TWMSBINITEMSQTY SET ITEMQTYSECONDARY=" & PackItemWeight.ToString & " WHERE PACKITEMID=" & PackItemID.ToString, 1)
            End If
        Catch ex As Exception

        End Try

        If Rtrn > 0 Then
            Return "1"
        Else
            Return "-1"
        End If


    End Function

    <WebMethod(Description:="Ovewrite palete weight during putway")> _
       Public Function SOA_FSetPaletteReceivedRealWeight(ByVal CompID As Integer, ByVal Customerid As Long, ByVal PAckItemNo As String, ByVal PackItemID As Long, ByVal PackItemWeight As Long) As String

        Dim MyWmsDBTrans As New WmsDBTrans

        Dim Rtrn As Long
        Dim SqlStr As String


        If Customerid > 0 Then
            SqlStr = "UPDATE TPACKAGES SET INWAREHOUSE=2, PACKITEMWEIGHT=" & PackItemWeight.ToString
        Else
            SqlStr = "UPDATE TPACKAGES SET INWAREHOUSE=1 ,PACKITEMWEIGHT=" & PackItemWeight.ToString
        End If

        SqlStr = SqlStr & " WHERE "

        If PackItemID > 0 Then
            SqlStr = SqlStr & " PACKITEMID=" & PackItemID.ToString
        ElseIf Len(PAckItemNo) > 0 Then
            SqlStr = SqlStr & " PACKITEMNO='" & PAckItemNo & "'"
        End If

        Rtrn = MyWmsDBTrans.OraDBExecuteSQLCmd(SqlStr, 1)

        Try
            If PackItemID > 0 And Long.Parse(MyWmsDBTrans.OraDBWmsExSelectCmdRN2String("SELECT COUNT(*) FROM TPACKAGESDTL WHERE PACKITEMID=" & PackItemID.ToString)) = 1 Then
                MyWmsDBTrans.OraDBExecuteSQLCmd("UPDATE  TPACKAGESDTL SET ITEMWEIGHT=" & PackItemWeight.ToString & " WHERE PACKITEMID=" & PackItemID.ToString, 1)
                MyWmsDBTrans.OraDBExecuteSQLCmd("UPDATE  TWMSBINITEMSQTY SET ITEMQTYSECONDARY=" & PackItemWeight.ToString & " WHERE PACKITEMID=" & PackItemID.ToString, 1)
            End If
        Catch ex As Exception

        End Try

        SOA_FProdPaletteUpdateProdPackweight(PackItemID, PackItemWeight)

        If Rtrn > 0 Then
            Return "1"
        Else
            Return "-1"
        End If


    End Function

    <WebMethod(Description:="Shipping - Print Palette LAbel")> _
    Public Function SOA_FPrintPaletteLabel(ByVal CompID As Integer, ByVal BranchID As Integer, ByVal PackItemID As Long, ByVal WhLocation As Integer) As String
        Dim SqlStr As String

        Dim rtrn As Long
        'Dim MyWmsDBTrans as new WmsDBTrans 
        SqlStr = "INSERT INTO TWMSPRINTLABELS(COMPID,BRANCHID,PACKITEMID,WHBUILDID) VALUES ("

        If CompID > 0 Then
            SqlStr = SqlStr & CompID.ToString & ","
        Else
            SqlStr = SqlStr & "NULL,"
        End If
        If BranchID > 0 Then
            SqlStr = SqlStr & BranchID.ToString & ","
        Else
            SqlStr = SqlStr & "NULL,"
        End If
        If PackItemID > 0 Then
            SqlStr = SqlStr & PackItemID.ToString & ","
        Else
            SqlStr = SqlStr & "NULL,"
        End If
        If WhLocation > 0 Then
            SqlStr = SqlStr & WhLocation.ToString
        Else
            SqlStr = SqlStr & "NULL"
        End If
        SqlStr = SqlStr & ")"

        rtrn = MyWmsDBTrans.OraDBExecuteSQLCmd(SqlStr, 1)



        If rtrn > 0 Then
            Return "1"
        Else
            Return "-1"
        End If

    End Function

#End Region

#Region "Internal Shipping"
    <WebMethod(Description:="Internal Shipping::Get Internal Shipping Tasks ")> _
       Public Function SOA_FGetInternalShippingTasks(ByVal BranchID As Integer) As DataSet
        Dim Ds As New DataSet

        Dim SqlStr As String
        Dim MyWmsDBtrans As New WmsDBTrans

        SqlStr = "SELECT SHIPPINGID,COMPID,BRANCHID,TO_CHAR(SHIPPINGDATE,'DD/MM/YYYY') AS SHIPPINGDATE,NVL(SHIPCOMMENTS,'...') AS SHIPCOMMENTS,CONFIRMED,BRANCHIDTO,RECEIVED FROM TWMSSHIPPINGINTERNAL "
        SqlStr = SqlStr & " WHERE NVL(CONFIRMED,0) = 0 "
        If BranchID > 0 Then
            SqlStr = SqlStr & " AND BRANCHID = " & BranchID.ToString
        End If


        Ds = MyWmsDBtrans.OraDBFillDataset(SqlStr, "DSUNTSHIPPTASKS")
        Return Ds
    End Function

    <WebMethod(Description:="Internal Shipping::Add Palette to Internal Shipping Task ")> _
    Public Function SOA_FAddPaletteToIntShippingTask(ByVal BranchID As Integer, ByVal ShippingID As Long, ByVal PackItemID As Long, ByVal CurrentPackItemWeight As Long) As Long
        Dim Rtrn As Long
        Dim INSCNT As Integer = 0

        Dim SqlStr, SQLCMD As String
        Dim MyWmsDBtrans As New WmsDBTrans
        Dim MyWmsBinTrans As New WmsBinTrans
        Dim ds As DataSet

        Dim ItemID As Long
        Dim ItemColorID, ItemColorID2 As Integer
        Dim ItemLength As Decimal

        Dim ItemPQty, ItemSQty As Long
        Dim PackItemWeight As Long
        Dim MunitP, MunitS As Integer

        Dim OrderID, OrderDtlID As Long
        Dim PackItemNO As String
        Dim MainitemcatID As Integer
        Dim ERPItemID As Long
        Dim CustomerID As Long
        Dim SourceBinCode As String

        Dim PListID As Long
        Dim CompID As Integer

        CompID = 1

        SqlStr = "SELECT DISTINCT TPACKAGES.PACKITEMID,TPACKAGES.PACKITEMNO,TPACKAGES.PACKITEMWEIGHT,TPACKAGES.ERPITEMID,TPACKAGES.MAINITEMCATID,"
        SqlStr = SqlStr & "NVL(TPACKAGES.SOURCEBINCODE,TPACKAGES.BINCODE) AS SOURCEBINCODE,  TPACKAGESDTL.ITEMBARS AS BINITEMQTYPRIMARY,"
        SqlStr = SqlStr & "TPACKAGESDTL.ITEMWEIGHT AS BINITEMQTYSECONDARY,"
        SqlStr = SqlStr & "TPACKAGESDTL.ITEMID,TPACKAGESDTL.ITEMCOLORID,TPACKAGESDTL.ITEMCOLORID2,TPACKAGESDTL.ITEMLENGTH,"
        SqlStr = SqlStr & "TPACKAGES.CUSTOMERID,TPACKAGESDTL.ORDERID,TPACKAGESDTL.ORDERDTLID "
        SqlStr = SqlStr & " FROM TPACKAGES,TPACKAGESDTL "
        SqlStr = SqlStr & " WHERE TPACKAGES.PACKITEMID = " & PackItemID.ToString & " AND "
        SqlStr = SqlStr & " TPACKAGES.PACKITEMID = TPACKAGESDTL.PACKITEMID  AND "
        SqlStr = SqlStr & " TPACKAGESDTL.ITEMBARS > 0 "

        ds = MyWmsDBtrans.OraDBFillDataset(SqlStr, "DsPaletteData")


        If ds.Tables.Count > 0 Then
            For Each dr As DataRow In ds.Tables(0).Rows
                Try
                    ItemID = Long.Parse(dr("ITEMID").ToString)
                Catch ex As Exception
                End Try
                Try
                    ItemColorID = Integer.Parse(dr("ITEMCOLORID").ToString)
                Catch ex As Exception
                End Try
                Try
                    ItemColorID2 = Integer.Parse(dr("ITEMCOLORID2").ToString)
                Catch ex As Exception
                End Try
                Try
                    ItemLength = Decimal.Parse(dr("ITEMLENGTH").ToString)
                Catch ex As Exception
                End Try
                Try
                    ItemPQty = Long.Parse(dr("BINITEMQTYPRIMARY").ToString)
                Catch ex As Exception
                End Try
                Try
                    ItemSQty = Long.Parse(dr("BINITEMQTYSECONDARY").ToString)
                Catch ex As Exception
                End Try

                MunitP = 1
                MunitS = 2
                Try
                    OrderID = Long.Parse(dr("ORDERID").ToString)
                Catch ex As Exception
                End Try
                Try
                    OrderDtlID = Long.Parse(dr("ORDERDTLID").ToString)
                Catch ex As Exception
                End Try

                If CurrentPackItemWeight > 0 Then
                    PackItemWeight = CurrentPackItemWeight
                Else
                    Try
                        PackItemWeight = Long.Parse(dr("PACKITEMWEIGHT").ToString)
                    Catch ex As Exception
                    End Try

                End If



                Try
                    PackItemNO = dr("PACKITEMNO").ToString
                Catch ex As Exception
                End Try
                Try
                    ERPItemID = Long.Parse(dr("ERPITEMID").ToString)
                Catch ex As Exception
                End Try
                Try
                    MainitemcatID = Integer.Parse(dr("MAINITEMCATID").ToString)
                Catch ex As Exception
                End Try
                Try
                    CustomerID = Long.Parse(dr("CUSTOMERID").ToString)
                Catch ex As Exception
                End Try

                '=================================================
                MyWmsDBtrans.OraDBExecuteSQLCmd("DELETE FROM TWMSSHIPPINGINTERNALITEMS WHERE PACKITEMID=" & PackItemID.ToString, 1)
                '==================================================
                SQLCMD = "INSERT INTO TWMSSHIPPINGINTERNALITEMS (PACKINGLISTID,COMPID,BRANCHID,SHIPPINGID,"
                SQLCMD = SQLCMD & "ORDERID,ORDERDTLID,ITEMID,ITEMCOLORID,ITEMCOLORID2,ITEMLENGTH,"
                SQLCMD = SQLCMD & "ITEMPRIMARYQTY,ITEMMUNITPRIMARY,ITEMSECONDARYQTY,ITEMMUNITSECONDARY,"
                SQLCMD = SQLCMD & "PACKITEMID,PACKITEMNO,PACKITEMWEIGHT,MAINITEMCATID,ERPITEMID,CUSTOMERID) VALUES ("

                Try
                    PListID = Long.Parse(MyWmsDBtrans.OraDBWmsExSelectCmdRN2String("SELECT SEQ_TWMSSHIPPINGINTERNALITEMS.NEXTVAL FROM TDUAL"))
                Catch ex As Exception
                    Return -1
                End Try

                SQLCMD = SQLCMD & PListID.ToString & "," & CompID.ToString & ","
                If BranchID > 0 Then
                    SQLCMD = SQLCMD & BranchID.ToString & ","
                Else
                    SQLCMD = SQLCMD & "NULL,"
                End If
                If ShippingID > 0 Then
                    SQLCMD = SQLCMD & ShippingID.ToString & ","
                Else
                    SQLCMD = SQLCMD & "NULL,"
                End If
                '====================================================
                If OrderID > 0 Then
                    SQLCMD = SQLCMD & OrderID.ToString & ","
                Else
                    SQLCMD = SQLCMD & "NULL,"
                End If
                If OrderDtlID > 0 Then
                    SQLCMD = SQLCMD & OrderDtlID.ToString & ","
                Else
                    SQLCMD = SQLCMD & "NULL,"
                End If
                If ItemID > 0 Then
                    SQLCMD = SQLCMD & ItemID.ToString & ","
                Else
                    SQLCMD = SQLCMD & "NULL,"
                End If
                If ItemColorID > 0 Then
                    SQLCMD = SQLCMD & ItemColorID.ToString & ","
                Else
                    SQLCMD = SQLCMD & "NULL,"
                End If
                If ItemColorID2 > 0 Then
                    SQLCMD = SQLCMD & ItemColorID2.ToString & ","
                Else
                    SQLCMD = SQLCMD & "NULL,"
                End If
                If ItemLength > 0 Then
                    SQLCMD = SQLCMD & MyWmsBinTrans.f_dbinsertdecimal(ItemLength, 3) & ","
                Else
                    SQLCMD = SQLCMD & "NULL,"
                End If
                '==================================================================
                If ItemPQty > 0 Then
                    SQLCMD = SQLCMD & ItemPQty.ToString & ","
                Else
                    SQLCMD = SQLCMD & "NULL,"
                End If
                If MunitP > 0 Then
                    SQLCMD = SQLCMD & MunitP.ToString & ","
                Else
                    SQLCMD = SQLCMD & "NULL,"
                End If
                If ItemSQty > 0 Then
                    SQLCMD = SQLCMD & ItemSQty.ToString & ","
                Else
                    SQLCMD = SQLCMD & "NULL,"
                End If
                If MunitS > 0 Then
                    SQLCMD = SQLCMD & MunitS.ToString & ","
                Else
                    SQLCMD = SQLCMD & "NULL,"
                End If
                '===============================================================
                If PackItemID > 0 Then
                    SQLCMD = SQLCMD & PackItemID.ToString & ","
                Else
                    SQLCMD = SQLCMD & "NULL,"
                End If
                If PackItemNO.Length > 0 Then
                    SQLCMD = SQLCMD & "'" & PackItemNO & "',"
                Else
                    SQLCMD = SQLCMD & "NULL,"
                End If
                If PackItemWeight > 0 Then
                    SQLCMD = SQLCMD & PackItemWeight.ToString & ","
                Else
                    SQLCMD = SQLCMD & "NULL,"
                End If
                If MainitemcatID > 0 Then
                    SQLCMD = SQLCMD & MainitemcatID.ToString & ","
                Else
                    SQLCMD = SQLCMD & "NULL,"
                End If
                If ERPItemID > 0 Then
                    SQLCMD = SQLCMD & ERPItemID.ToString & ","
                Else
                    SQLCMD = SQLCMD & "NULL,"
                End If
                If CustomerID > 0 Then
                    SQLCMD = SQLCMD & CustomerID.ToString
                Else
                    SQLCMD = SQLCMD & "NULL"
                End If
                SQLCMD = SQLCMD & ")"

                If MyWmsDBtrans.OraDBExecuteSQLCmd(SQLCMD, 1) > 0 Then
                    INSCNT = INSCNT + 1
                Else
                    MyWmsDBtrans.f_sqlerrorlog(1, "SOA_FAddPaletteToIntShippingTask", SQLCMD, "mobile")
                End If
            Next
        Else
            MyWmsDBtrans.f_sqlerrorlog(1, "SOA_FAddPaletteToIntShippingTask", SqlStr, "mobile")
        End If

        If INSCNT > 0 Then
            Return INSCNT
        Else
            Return -1
        End If

    End Function


    <WebMethod(Description:="Internal Shipping::Add Route Order to Internal Shipping Task ")> _
    Public Function SOA_FAddRouteOrderToIntShippingTask(ByVal BranchID As Integer, ByVal ShippingID As Long, ByVal strbarcode As String, ByVal CurrentPackItemWeight As Long) As Long
        Dim SqlStr, SQLCMD As String
        Dim MyWmsDBtrans As New WmsDBTrans
        Dim MyWmsBinTrans As New WmsBinTrans
        Dim ipackid, igrouporderid, iprocessid, isrcgrouporderid, tmpgrouporderid As Long
        Dim fframetransferdtlid, fframetransferid, fgrouporderid, forderid, dsi, srcgrouporderid As Long

        If strbarcode = "" Then Return -1

        If strbarcode.StartsWith("1") Then
            fframetransferdtlid = strbarcode.Remove(0, 1)
            If fframetransferdtlid > 0 Then
                ipackid = Long.Parse(MyWmsDBtrans.OraDBWmsExSelectCmdRN2String("SELECT PACKID FROM TPRODFRAMETRANFERDTL WHERE FRAMETRANFERDTLID = " & fframetransferdtlid.ToString))
                fframetransferid = Long.Parse(MyWmsDBtrans.OraDBWmsExSelectCmdRN2String("SELECT FRMTRSNFID FROM TPRODFRAMETRANFERDTL WHERE FRAMETRANFERDTLID = " & fframetransferdtlid.ToString))
            Else
                Return -1
            End If
        ElseIf strbarcode.StartsWith("2") Then
            ipackid = strbarcode.Remove(0, 1)
        ElseIf strbarcode.StartsWith("3") Then
            igrouporderid = strbarcode.Remove(0, 1)
        End If


        If fframetransferid > 0 Then
            'Check if frame has been added
            If Long.Parse(MyWmsDBtrans.OraDBWmsExSelectCmdRN2String("select count(*) from TWMSSHIPPINGINTERNALITEMS  where SHIPPINGID = " & ShippingID.ToString & " AND  FRAMETRANFERDTLID = " & fframetransferdtlid.ToString)) > 0 Then Return -3
        Else
            'Check if routeorder has been added
            If Long.Parse(MyWmsDBtrans.OraDBWmsExSelectCmdRN2String("select count(*) from TWMSSHIPPINGINTERNALITEMS  where SHIPPINGID = " & ShippingID.ToString & " AND  (PACKID = " & ipackid.ToString & " OR GROUPORDERID = " & igrouporderid.ToString & ")")) > 0 Then Return -3

        End If

        Return FAddRouteOrderInternal(BranchID, ShippingID, ipackid, igrouporderid, fframetransferdtlid, CurrentPackItemWeight)

    End Function
    Private Function FAddRouteOrderInternal(ByVal fbranchid As Integer, ByVal shippingid As Long, ByVal packid As Long, ByVal grouporderid As Long, ByVal frametranferdtlid As Long, ByVal CurrentPackItemWeight As Long) As Long
        Dim SqlStr As String
        Dim packinglistid, dsi, insrows, itemid, orderid, orderdtlid, packitemweight, erpitemid, groupitemid, mainitemcatid, dscustomerid As Long
        Dim itemcolorid, itemcolorid2, munitprimary, munitsecondary As Integer
        Dim itemlength, itempqty, itemsqty As Decimal
        Dim ds As DataSet
        Dim Compid, BranchID As Integer
        insrows = 0
        Dim MyWmsBinTrans As New WmsBinTrans

        ds = New DataSet

        If frametranferdtlid > 0 Then
            SqlStr += "SELECT TPRODFRAMETRANFERDTL.FRAMETRANFERDTLID as FRAMETRANFERDTLID,TPRODFRAMETRANFERDTL.PACKID as PACKID,TPRODFRAMETRANFERDTL.ITEMBARS as ITEMBARS, "
            SqlStr += "TPRODFRAMETRANFERDTL.ITEMWEIGHT AS PACKITEMWEIGHT,TPRODFRAMETRANFERDTL.ITEMLENGTH as ITEMLENGTH,TPRODFRAMETRANFERDTL.ITEMWEIGHT as ITEMWEIGHT, "
            SqlStr += "TPRODPACKAGING.ORDERID as ORDERID,TPRODPACKAGING.ORDERDTLID as ORDERDTLID,TPRODPACKAGING.BRANCHID as BRANCHID,TPRODPACKAGING.PACKDATE as PACKDATE, "
            SqlStr += "TPRODPACKAGING.ITEMCOLORID as ITEMCOLORID,TPRODPACKAGING.ITEMCOLORID2 as ITEMCOLORID2,TITEMS.MAINITEMCATID as MAINITEMCATID, "
            SqlStr += "TITEMS.MUNITPRIMARY as MUNITPRIMARY,TITEMS.MUNITSECONDARY as MUNITSECONDARY,TORDERS.CUSTOMERID as CUSTOMERID,TITEMS.ITEMID as ITEMID,TITEMS.GROUPITEMID as GROUPITEMID "
            SqlStr += "FROM  TPRODFRAMETRANFERDTL,TPRODPACKAGING,TITEMS,TORDERS WHERE  "
            SqlStr += "TPRODFRAMETRANFERDTL.PACKID = TPRODPACKAGING.PACKID AND  "
            SqlStr += "TPRODPACKAGING.ORDERID = TORDERS.ORDERID (+) AND "
            SqlStr += "TPRODFRAMETRANFERDTL.ITEMID = TITEMS.ITEMID AND "
            SqlStr += "TPRODFRAMETRANFERDTL.FRAMETRANFERDTLID = " & frametranferdtlid.ToString

        Else

            SqlStr += "SELECT TPRODPACKAGING.PACKID as PACKID,TPRODPACKAGING.ITEMBARS as ITEMBARS,TPRODPACKAGING.ITEMWEIGHT AS PACKITEMWEIGHT,TPRODPACKAGING.ITEMLENGTH as ITEMLENGTH, "
            SqlStr += "TPRODPACKAGING.ITEMWEIGHT as ITEMWEIGHT,TPRODPACKAGING.ORDERID as ORDERID,TPRODPACKAGING.ORDERDTLID as ORDERDTLID, "
            SqlStr += "TPRODPACKAGING.BRANCHID as BRANCHID,TPRODPACKAGING.PACKDATE as PACKDATE,TPRODPACKAGING.ITEMCOLORID as ITEMCOLORID,TPRODPACKAGING.ITEMCOLORID2 as ITEMCOLORID2, "
            SqlStr += "TITEMS.MAINITEMCATID as MAINITEMCATID,TITEMS.MUNITPRIMARY as MUNITPRIMARY,TITEMS.MUNITSECONDARY as MUNITSECONDARY,TORDERS.CUSTOMERID as CUSTOMERID,TITEMS.ITEMID as ITEMID,TITEMS.GROUPITEMID as GROUPITEMID "
            SqlStr += "FROM  TPRODPACKAGING,TITEMS,TORDERS WHERE  "
            SqlStr += "TPRODPACKAGING.ORDERID = TORDERS.ORDERID (+) AND "
            SqlStr += "TPRODPACKAGING.ITEMID = TITEMS.ITEMID"
            If grouporderid > 0 Then
                SqlStr += " AND TPRODPACKAGING.GROUPORDERID = " & grouporderid.ToString
            Else
                SqlStr += " AND TPRODPACKAGING.PACKID = " & packid.ToString
            End If
        End If




        ds = MyWmsDBTrans.OraDBFillDataset(SqlStr, "DsRouteOrderData")





        If ds.Tables.Count > 0 Then



            For Each dr As DataRow In ds.Tables(0).Rows





                Try
                    itemcolorid = Long.Parse(dr("itemcolorid").ToString)
                Catch ex As Exception
                End Try
                Try
                    itemcolorid2 = Long.Parse(dr("itemcolorid2").ToString)
                Catch ex As Exception
                End Try

                Try
                    itemlength = Decimal.Parse(dr("itemlength").ToString)
                Catch ex As Exception
                End Try


                Try
                    itempqty = Decimal.Parse(dr("ITEMBARS").ToString)
                Catch ex As Exception
                End Try

                Try
                    itemsqty = Decimal.Parse(dr("ITEMWEIGHT").ToString)
                Catch ex As Exception
                End Try



                If CurrentPackItemWeight > 0 Then
                    packitemweight = CurrentPackItemWeight
                Else
                    Try
                        packitemweight = Long.Parse(dr("packitemweight").ToString)
                    Catch ex As Exception
                    End Try

                End If




                Try
                    orderid = Long.Parse(dr("orderid").ToString)
                Catch ex As Exception
                End Try
                Try
                    orderdtlid = Long.Parse(dr("orderdtlid").ToString)
                Catch ex As Exception
                End Try
                Try
                    munitprimary = Long.Parse(dr("MUNITPRIMARY").ToString)
                Catch ex As Exception
                End Try

                Try
                    munitsecondary = Long.Parse(dr("MUNITSECONDARY").ToString)
                Catch ex As Exception
                End Try

                Try
                    itemid = Long.Parse(dr("itemid").ToString)
                Catch ex As Exception
                End Try

                Try
                    groupitemid = Long.Parse(dr("groupitemid").ToString)
                Catch ex As Exception
                End Try

                Try
                    mainitemcatid = Long.Parse(dr("mainitemcatid").ToString)
                Catch ex As Exception
                End Try

                Try
                    dscustomerid = Long.Parse(dr("customerid").ToString)
                Catch ex As Exception
                End Try
                Compid = Integer.Parse(MyWmsDBTrans.OraDBWmsExSelectCmdRN2String("SELECT COMPID FROM TWMSSHIPPINGINTERNAL WHERE SHIPPINGID =" & shippingid.ToString))
                BranchID = Integer.Parse(MyWmsDBTrans.OraDBWmsExSelectCmdRN2String("SELECT BRANCHID FROM TWMSSHIPPINGINTERNAL WHERE SHIPPINGID =" & shippingid.ToString))


                If grouporderid > 0 Then
                    packid = Long.Parse(dr("packid").ToString)
                End If

                SqlStr = "INSERT INTO TWMSSHIPPINGINTERNALITEMS (PACKINGLISTID,COMPID,BRANCHID,SHIPPINGID,"
                SqlStr = SqlStr + "ORDERID,ORDERDTLID,ITEMID,ITEMCOLORID,ITEMCOLORID2,ITEMLENGTH,"
                SqlStr = SqlStr + "ITEMPRIMARYQTY,ITEMMUNITPRIMARY,ITEMSECONDARYQTY,ITEMMUNITSECONDARY,"
                SqlStr = SqlStr + "PACKITEMWEIGHT,MAINITEMCATID,ERPITEMID,CUSTOMERID,GROUPORDERID,PACKID,FRAMETRANFERDTLID) VALUES ("

                Try
                    packinglistid = Long.Parse(MyWmsDBTrans.OraDBWmsExSelectCmdRN2String("SELECT SEQ_TWMSSHIPPINGINTERNALITEMS.NEXTVAL FROM TDUAL"))
                Catch ex As Exception
                    Return -1
                End Try






                SqlStr = SqlStr & packinglistid.ToString & "," & Compid.ToString & ","
                If BranchID > 0 Then
                    SqlStr = SqlStr & BranchID.ToString & ","
                Else
                    SqlStr = SqlStr & "NULL,"
                End If
                If shippingid > 0 Then
                    SqlStr = SqlStr & shippingid.ToString & ","
                Else
                    SqlStr = SqlStr & "NULL,"
                End If
                '====================================================
                If orderid > 0 Then
                    SqlStr = SqlStr & orderid.ToString & ","
                Else
                    SqlStr = SqlStr & "NULL,"
                End If
                If orderdtlid > 0 Then
                    SqlStr = SqlStr & orderdtlid.ToString & ","
                Else
                    SqlStr = SqlStr & "NULL,"
                End If




                If itemid > 0 Then
                    SqlStr = SqlStr & itemid.ToString & ","
                Else
                    SqlStr = SqlStr & "NULL,"
                End If
                If itemcolorid > 0 Then
                    SqlStr = SqlStr & itemcolorid.ToString & ","
                Else
                    SqlStr = SqlStr & "NULL,"
                End If
                If itemcolorid2 > 0 Then
                    SqlStr = SqlStr & itemcolorid2.ToString & ","
                Else
                    SqlStr = SqlStr & "NULL,"
                End If
                If itemlength > 0 Then
                    SqlStr = SqlStr & MyWmsBinTrans.f_dbinsertdecimal(itemlength, 4) & ","
                Else
                    SqlStr = SqlStr & "NULL,"
                End If

                If itempqty > 0 Then
                    SqlStr = SqlStr & MyWmsBinTrans.f_dbinsertdecimal(itempqty, 4) & ","
                Else
                    SqlStr = SqlStr & "NULL,"
                End If

                If munitprimary > 0 Then
                    SqlStr = SqlStr & munitprimary.ToString & ","
                Else
                    SqlStr = SqlStr & "NULL,"
                End If

                If itemsqty > 0 Then
                    SqlStr = SqlStr & MyWmsBinTrans.f_dbinsertdecimal(itemsqty, 4) & ","
                Else
                    SqlStr = SqlStr & "NULL,"
                End If


                If munitsecondary > 0 Then
                    SqlStr = SqlStr & munitsecondary.ToString & ","
                Else
                    SqlStr = SqlStr & "NULL,"
                End If

                If packitemweight > 0 Then
                    SqlStr = SqlStr & packitemweight.ToString & ","
                Else
                    SqlStr = SqlStr & "NULL,"
                End If

                If mainitemcatid > 0 Then
                    SqlStr = SqlStr & mainitemcatid.ToString & ","
                Else
                    SqlStr = SqlStr & "NULL,"
                End If

                erpitemid = Long.Parse(MyWmsDBTrans.OraDBWmsExSelectCmdRN2String("select ACCOUNTITEMID  from TITEMCOLORACCOUNTS WHERE ITEMCOLORID = " & itemcolorid.ToString & " AND ITEMCATEGORYID =" & groupitemid.ToString))


                If erpitemid > 0 Then
                    SqlStr = SqlStr & erpitemid.ToString & ","
                Else
                    SqlStr = SqlStr & "NULL,"
                End If

                If dscustomerid > 0 Then
                    SqlStr = SqlStr & dscustomerid.ToString & ","
                Else
                    SqlStr = SqlStr & "NULL,"
                End If

                If grouporderid > 0 Then
                    SqlStr = SqlStr & grouporderid.ToString & ","
                Else
                    SqlStr = SqlStr & "NULL,"
                End If

                If packid > 0 Then
                    SqlStr = SqlStr & packid.ToString & ","
                Else
                    SqlStr = SqlStr & "NULL,"
                End If

                If frametranferdtlid > 0 Then
                    SqlStr = SqlStr & frametranferdtlid.ToString & ")"
                Else
                    SqlStr = SqlStr & "NULL)"
                End If

                If MyWmsDBTrans.OraDBExecuteSQLCmd(SqlStr, 1) > 0 Then
                    insrows = insrows + 1
                Else
                    MyWmsDBTrans.f_sqlerrorlog(1, "SOA_FAddPaletteToIntShippingTask", SqlStr, "mobile")
                End If

                '==================================================================

            Next
        Else
            MyWmsDBTrans.f_sqlerrorlog(1, "SOA_FAddRouteOrderShippingInternal", SqlStr, "mobile")
        End If

        If insrows > 0 Then
            Return insrows
        Else
            Return -1
        End If
        Return 1
    End Function

    <WebMethod(Description:="Internal Shipping::Get Route Order Info")> _
    Public Function SOA_FGetRouteOrderInfo(ByVal strbarcode As String) As DataSet
        'OraDBFillDataset()
        Dim SqlStr, SQLCMD As String
        Dim MyWmsDBtrans As New WmsDBTrans
        Dim MyWmsBinTrans As New WmsBinTrans
        Dim ipackid, igrouporderid, iprocessid, isrcgrouporderid, tmpgrouporderid As Long
        Dim fframetransferdtlid, fframetransferid, fgrouporderid, forderid, dsi, srcgrouporderid As Long
        Dim ds As DataSet

        ds = New DataSet

        If strbarcode = "" Then Return ds

        If strbarcode.StartsWith("1") Then
            fframetransferdtlid = strbarcode.Remove(0, 1)
            If fframetransferdtlid > 0 Then
                ipackid = Long.Parse(MyWmsDBtrans.OraDBWmsExSelectCmdRN2String("SELECT PACKID FROM TPRODFRAMETRANFERDTL WHERE FRAMETRANFERDTLID = " & fframetransferdtlid.ToString))
                fframetransferid = Long.Parse(MyWmsDBtrans.OraDBWmsExSelectCmdRN2String("SELECT FRMTRSNFID FROM TPRODFRAMETRANFERDTL WHERE FRAMETRANFERDTLID = " & fframetransferdtlid.ToString))
            Else
                Return ds
            End If
        ElseIf strbarcode.StartsWith("2") Then
            ipackid = strbarcode.Remove(0, 1)
        ElseIf strbarcode.StartsWith("3") Then
            igrouporderid = strbarcode.Remove(0, 1)
        End If




        If fframetransferdtlid > 0 Then
            SqlStr = "select TITEMS.ITEMCODE as ITEMCODE,TPRODFRAMETRANFERDTL.ITEMBARS as INITPACKITEMBARS, "
            SqlStr += "TPRODFRAMETRANFERDTL.ITEMLENGTH as ITEMLENGTH,TITEMCOLORS.COLORDESC AS COLORDESC "
            SqlStr += "FROM  TPRODFRAMETRANFERDTL,TPRODPACKAGING,TITEMS,TORDERS,TITEMCOLORS WHERE  "
            SqlStr += "TPRODFRAMETRANFERDTL.ITEMCOLORID = TITEMCOLORS.COLORID AND  "
            SqlStr += "TPRODFRAMETRANFERDTL.PACKID = TPRODPACKAGING.PACKID AND  "
            SqlStr += "TPRODPACKAGING.ORDERID = TORDERS.ORDERID (+) AND "
            SqlStr += "TPRODFRAMETRANFERDTL.ITEMID = TITEMS.ITEMID AND "
            SqlStr += "TPRODFRAMETRANFERDTL.FRAMETRANFERDTLID = " & fframetransferdtlid.ToString

        Else

            SqlStr += "select TITEMS.ITEMCODE as ITEMCODE,TPRODPACKAGING.ITEMBARS as INITPACKITEMBARS,TPRODPACKAGING.ITEMLENGTH as ITEMLENGTH, "
            SqlStr += "TPRODPACKAGING.ITEMWEIGHT as ITEMWEIGHT,TPRODPACKAGING.ORDERID as ORDERID,TPRODPACKAGING.ORDERDTLID as ORDERDTLID, "
            SqlStr += "TITEMCOLORS.COLORDESC AS COLORDESC "
            SqlStr += "FROM  TPRODPACKAGING,TITEMS,TORDERS,TITEMCOLORS WHERE  "
            SqlStr += "TPRODPACKAGING.ITEMCOLORID = TITEMCOLORS.COLORID AND  "
            SqlStr += "TPRODPACKAGING.ORDERID = TORDERS.ORDERID (+) AND "
            SqlStr += "TPRODPACKAGING.ITEMID = TITEMS.ITEMID"
            If igrouporderid > 0 Then
                SqlStr += " AND TPRODPACKAGING.GROUPORDERID = " & igrouporderid.ToString
            Else
                SqlStr += " AND TPRODPACKAGING.PACKID = " & ipackid.ToString
            End If
        End If



        'Dim Ds As DataSet
        'Dim SqlStr As String

        'SqlStr = "SELECT PACKITEMID,PACKITEMNO,ITEMCODE,ITEMDESC,ITEMID,ITEMLENGTH,ITEMCOLORID,ITEMCOLORID2,ORDERID,ORDERDTLID,"
        'SqlStr = SqlStr & "COLORCODE,COLORDESC, COLORCODE2, COLORDESC2, PACKITEMBARS , PACKITEMWEIGHT,BINCODE,"
        'SqlStr = SqlStr & "INITPACKITEMBARS,INITPACKITEMWEIGHT FROM VPACKAGEINFO2 WHERE PACKITEMNO = '" & PackItemNo & "'"

        ds = MyWmsDBtrans.OraDBFillDataset(SqlStr, "PACKITEMINFO")

        Return ds

    End Function



#End Region

End Class

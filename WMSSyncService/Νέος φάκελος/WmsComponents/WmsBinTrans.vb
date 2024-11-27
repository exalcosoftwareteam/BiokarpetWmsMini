Imports System.Configuration
Imports System.Data.OracleClient
Public Class WmsBinTrans

    Public gTerminal As String
    Public gUserName As String

    Public gMPUnit As Integer
    Public gMSUnit As Integer
    Public gNewWmsTRansID As Long
#Region " General "
    Public Function FGetDefaultStoreID() As Integer
        Dim MyWmsDBTrans As New WmsDBTrans
        Dim StoreIDstr As String

        StoreIDstr = MyWmsDBTrans.OraDBWmsExSelectCmdRN2String("SELECT STOREID FROM TSTORES WHERE STOREIDDEFAULT=1")

        If Len(StoreIDstr) And (StoreIDstr <> "NULL" Or StoreIDstr <> "Null") Then
            Return Integer.Parse(StoreIDstr)
        Else
            Return -1
        End If
    End Function
    Public Function FGetCurrentYear() As Integer
        Dim MyWmsDBtrans As New WmsDBTrans
        Dim Ystr As String

        Ystr = MyWmsDBtrans.OraDBWmsExSelectCmdRN2String("SELECT EXTRACT(YEAR FROM SYSDATE) FROM DUAL")

        If Len(Ystr) > 0 And (Ystr <> "NULL" Or Ystr <> "Null") Then
            Return Integer.Parse(Ystr)
        Else
            Return -1
        End If
    End Function
    Public Function FGetItemMUnitPrimary(ByVal ItemID As Long) As Integer
        FGetItemMUnitPrimary = Integer.Parse(ConfigurationSettings.AppSettings("ItemMunitPrimary"))
    End Function
    Public Function FGetItemMUnitSecondary(ByVal ItemID As Long) As Integer
        FGetItemMUnitSecondary = Integer.Parse(ConfigurationSettings.AppSettings("ItemMunitSecondary"))
    End Function
    Public Function FGetItemMUnitDecimal(ByVal MUnit As Integer) As Integer
        FGetItemMUnitDecimal = 0
    End Function

    Public Function FMyFill(ByVal FillChar As Char, ByVal FillNo As Integer) As String

        Dim NewStr As String
        Dim i As Integer

        NewStr = FillChar

        If FillNo > 1 Then
            For i = 1 To FillNo
                NewStr = NewStr & FillChar
            Next
        
        End If
        Return NewStr
    End Function
    Public Function f_dbinsertdecimal(ByVal decnumber As Decimal, ByVal decno As Long) As String
        Dim decstr As String
        Dim DecFormat As String = "0."

        If decno > 0 Then
            For i As Integer = 1 To decno
                DecFormat += "0"
            Next
        Else
            DecFormat = "0.0000"
        End If
        decstr = decnumber.ToString(DecFormat)
        Return Replace(decstr, ",", ".")
    End Function
    Public Function FGetItemGroupID(ByVal ItemID As Long) As String
        Dim SqlStr As String

        Dim AccItemIDStr As String
        Dim MyWmsDbTrans As New WmsDBTrans

        SqlStr = "SELECT GROUPITEMID  FROM TITEMS "
        SqlStr = SqlStr & "WHERE ITEMID=" & ItemID.ToString

        AccItemIDStr = MyWmsDbTrans.OraDBWmsExSelectCmdRN2String(SqlStr)

        If Len(AccItemIDStr) > 0 And (AccItemIDStr <> "Null" Or AccItemIDStr <> "NULL") Then
            Return AccItemIDStr
        Else
            Return "-1"
        End If

    End Function
    Public Function FGetItemCategories(ByVal ItemID As Long) As DataSet
        Dim SqlStr As String

        Dim AccItemIDStr As String
        Dim MyWmsDbTrans As New WmsDBTrans

        Dim Ds As New DataSet

        SqlStr = "SELECT GROUPITEMID,MAINITEMCATID  FROM TITEMS "
        SqlStr = SqlStr & "WHERE ITEMID=" & ItemID.ToString

        Ds = MyWmsDbTrans.OraDBFillDataset(SqlStr, "ITEMCATEGORIES")
        
        Return Ds

    End Function
    Public Function FGetAccountMaterialID(ByVal CompID As Integer, ByVal BranchID As Integer, ByVal GroupItemID As Integer, ByVal ItemColorID As Long, ByVal ItemColorID2 As Long) As String
        Dim SqlStr As String

        Dim AccItemIDStr As String
        Dim MyWmsDbTrans As New WmsDBTrans


        If ItemColorID > 0 And ItemColorID2 > 0 And ItemColorID <> ItemColorID2 Then
            'COLOR2ERPITEMID
            SqlStr = "SELECT COLOR2ERPITEMID FROM TSYSERPPARAMS WHERE COMPID=" & CompID.ToString

            Try
                AccItemIDStr = MyWmsDbTrans.OraDBWmsExSelectCmdRN2String(SqlStr)

                If IsNumeric(AccItemIDStr) Then
                    If Long.Parse(AccItemIDStr) > 0 Then
                        Return AccItemIDStr
                    End If
                End If
            Catch ex As Exception

            End Try
        End If

        SqlStr = "SELECT ACCOUNTITEMID  FROM TITEMCOLORACCOUNTS "
        SqlStr = SqlStr & "WHERE ITEMCATEGORYID=" & GroupItemID.ToString & " AND ITEMCOLORID=" & ItemColorID.ToString()

        AccItemIDStr = MyWmsDbTrans.OraDBWmsExSelectCmdRN2String(SqlStr)

        If Len(AccItemIDStr) > 0 And (AccItemIDStr <> "Null" Or AccItemIDStr <> "NULL") Then
            Return AccItemIDStr
        Else
            Return "-1"
        End If

    End Function
    Public Function FCheckPaletteDestination(ByVal PackItemID As Long, ByVal OrderID As Long) As Long
        Dim SqlStr As String

        Dim PaletteShippAddress As String
        Dim OrderShippAddress As String
        Dim MyWmsDbTrans As New WmsDBTrans


        SqlStr = "SELECT SHIPPADDRESS FROM VORDERS WHERE ORDERID= "
        SqlStr = SqlStr & "(SELECT ORDERID FROM TPACKAGES WHERE PACKITEMID= " & PackItemID.ToString & " )"

        Try
            PaletteShippAddress = MyWmsDbTrans.OraDBWmsExSelectCmdRStr2Str(SqlStr)

            If Len(PaletteShippAddress) > 0 Then
                SqlStr = "SELECT SHIPPADDRESS FROM VORDERS WHERE ORDERID= " & OrderID.ToString
                OrderShippAddress = MyWmsDbTrans.OraDBWmsExSelectCmdRStr2Str(SqlStr)

                If Len(OrderShippAddress) > 0 And OrderShippAddress <> PaletteShippAddress Then
                    Return -1 'different destination
                End If
            End If
        Catch ex As Exception

        End Try

        Return 1
    End Function
    Public Function FGetAccountMaterialCode(ByVal GroupItemID, ByVal ItemColorID) As String
        Dim SqlStr As String

        Dim AccItemCode As String
        Dim MyWmsDbTrans As New WmsDBTrans

        SqlStr = "SELECT ACCOUNTERPITEMCODE  FROM TITEMCOLORACCOUNTS "
        SqlStr = SqlStr & "WHERE ITEMCATEGORYID=" & GroupItemID.ToString & " AND ITEMCOLORID=" & ItemColorID.ToString()

        AccItemCode = MyWmsDbTrans.OraDBWmsExSelectCmdRStr2Str(SqlStr)

        If Len(AccItemCode) > 0 And (AccItemCode <> "Null" Or AccItemCode <> "NULL") Then
            Return AccItemCode
        Else
            Return "-1"
        End If

    End Function
    Public Function FGetAccountMaterialCode2Colors(ByVal CompID As Integer, ByVal GroupItemID As Integer, ByVal ItemColorID As Integer, ByVal ItemColorID2 As Integer) As String
        Dim SqlStr As String

        Dim AccItemCode As String
        Dim MyWmsDbTrans As New WmsDBTrans

        If ItemColorID > 0 And ItemColorID2 > 0 And ItemColorID <> ItemColorID2 Then
            'COLOR2ERPITEMID
            SqlStr = "SELECT COLOR2ERPITEMCODE FROM TSYSERPPARAMS WHERE COMPID=" & CompID.ToString

            Try
                AccItemCode = MyWmsDbTrans.OraDBWmsExSelectCmdRStr2Str(SqlStr)

                If Len(AccItemCode) > 0 And (AccItemCode <> "Null" Or AccItemCode <> "NULL") Then
                    Return AccItemCode
                End If
            Catch ex As Exception
            End Try
        End If

        SqlStr = "SELECT ACCOUNTERPITEMCODE  FROM TITEMCOLORACCOUNTS "
        SqlStr = SqlStr & "WHERE ITEMCATEGORYID=" & GroupItemID.ToString & " AND ITEMCOLORID=" & ItemColorID.ToString()

        AccItemCode = MyWmsDbTrans.OraDBWmsExSelectCmdRStr2Str(SqlStr)

        If Len(AccItemCode) > 0 And (AccItemCode <> "Null" Or AccItemCode <> "NULL") Then
            Return AccItemCode
        Else
            Return "-1"
        End If

    End Function


    Public Function FCheckfiProdinWtyUpdatable(ByVal CompID as Integer) As Integer

        Dim Upd As Integer
        Dim SqlStr As String

        Dim MyWmsDBTrans As New WmsDBTrans

        Upd = 0

        SqlStr = "SELECT UPDATEPRODBINSONMOVEMENTS FROM TPRODPARAMS"
        If CompID > 0 Then
            SqlStr = SqlStr + " WHERE COMPID=" + CompID.ToString()
        End If


        Try
            Upd = Integer.Parse(MyWmsDBTrans.OraDBWmsExSelectCmdRN2String(SqlStr))
        Catch ex As Exception
            Upd = -1
        End Try

        Return Upd
    End Function
#End Region  'general

    Public Function FGetBinInfo(ByVal CompID As Integer, ByVal BranchID As Integer, ByVal FBinCode As String, ByVal FFriendlyBinCode As String) As DataSet
        Dim MyWmsDBTrans As WmsDBTrans = New WmsDBTrans

        Dim Ds As New DataSet
        Dim FBinIDStr As String
        Dim SqlSelStr As String

        SqlSelStr = "SELECT WHBINID,WHBINCODE,WHBINTYPE FROM TWMSBINS WHERE "
        If Len(FBinCode) > 0 Then
            SqlSelStr = SqlSelStr & "WHBINCODE='" & FBinCode & "'"
        ElseIf Len(FFriendlyBinCode) > 0 Then
            SqlSelStr = SqlSelStr & "FRIENDLYCODE='" & FFriendlyBinCode & "'"
        Else
            Ds = Nothing
            Return Ds
        End If

        Try
            Ds = MyWmsDBTrans.OraDBFillDataset(SqlSelStr, "WMSBININFO")
        Catch ex As Exception
            MyWmsDBTrans.f_sqlerrorlog(CompID, "WmsBinTrans.FGetBinInfo", SqlSelStr, "Admin")
        End Try


        Return (Ds)

    End Function

    Public Function FGetBinPaletteInfo(ByVal CompID As Integer, ByVal BranchID As Integer, ByVal BinCode As String) As DataSet

        Dim MyWmsDBTrans As New WmsDBTrans
        Dim Ds As New DataSet

        Dim SqlStr As String

        Dim PackItemNo As String

        Dim ThisBinCode As String

        Dim ThisBinID As Long

        If Len(BinCode) < 10 Then
            ThisBinCode = FGetBinCodeByOldCode(CompID, BranchID, BinCode)
        Else
            ThisBinCode = BinCode
        End If

        If Len(ThisBinCode) = 0 Then ThisBinCode = "-1"

        ThisBinID = FGetBinIDByCode(CompID, BranchID, ThisBinCode)

        SqlStr = "SELECT PACKITEMID,PACKITEMNO FROM TPACKAGES WHERE COMPID=" & CompID.ToString() ' " AND BINCODE='" & ThisBinCode & "'"
        SqlStr = SqlStr & " AND PACKITEMID IN (SELECT DISTINCT PACKITEMID FROM TWMSBINITEMSQTY WHERE WHBINID=" & ThisBinID.ToString() & ")"

        If BranchID > 0 Then
            SqlStr = SqlStr & " AND BRANCHID=" & BranchID.ToString()
        End If

        Ds = MyWmsDBTrans.OraDBFillDataset(SqlStr, "BINPALETTEINFO")

        Return Ds

    End Function

    Public Function FGetZoneIDByCode(ByVal CompID As Integer, ByVal BranchID As Integer, ByVal FZoneCode As String) As Long
        Dim MyWmsDBTrans As WmsDBTrans = New WmsDBTrans

        Dim FZoneIDStr As String
        Dim SqlSelStr As String

        SqlSelStr = "SELECT WHZONEID FROM TWMSZONES WHERE WHZONECODE='" & FZoneCode & "'"
        If CompID > 0 Then
            SqlSelStr = SqlSelStr & " AND COMPID =" & CompID.ToString()
        End If
        If BranchID > 0 Then
            SqlSelStr = SqlSelStr & " AND BRANCHID =" & BranchID.ToString()
        End If

        FZoneIDStr = MyWmsDBTrans.OraDBWmsExSelectCmdRN2String(SqlSelStr)

        Return Long.Parse(FZoneIDStr.ToString)
    End Function

    Public Function FGetZoneCodeByID(ByVal CompID As Integer, ByVal BranchID As Integer, ByVal WhZoneID As Long) As String
        Dim MyWmsDBTrans As WmsDBTrans = New WmsDBTrans

        Dim FZoneCodeStr As String
        Dim SqlSelStr As String

        SqlSelStr = "SELECT WHZONECODE FROM TWMSZONES WHERE WHZONEID=" & WhZoneID.ToString
        If CompID > 0 Then
            SqlSelStr = SqlSelStr & " AND COMPID =" & CompID.ToString()
        End If
        If BranchID > 0 Then
            SqlSelStr = SqlSelStr & " AND BRANCHID =" & BranchID.ToString()
        End If

        FZoneCodeStr = MyWmsDBTrans.OraDBWmsExSelectCmdRN2String(SqlSelStr)

        Return FZoneCodeStr
    End Function

    Public Function FGetBinIDByCode(ByVal CompID As Integer, ByVal BranchID As Integer, ByVal FBinCode As String) As Long
        Dim MyWmsDBTrans As WmsDBTrans = New WmsDBTrans

        Dim FBinIDStr As String
        Dim SqlSelStr As String

        SqlSelStr = "SELECT WHBINID FROM TWMSBINS WHERE WHBINCODE='" & FBinCode & "'"
        If CompID > 0 Then
            SqlSelStr = SqlSelStr & " AND COMPID =" & CompID.ToString()
        End If
        If BranchID > 0 Then
            SqlSelStr = SqlSelStr & " AND BRANCHID =" & BranchID.ToString()
        End If

        FBinIDStr = MyWmsDBTrans.OraDBWmsExSelectCmdRN2String(SqlSelStr)

        Return Long.Parse(FBinIDStr.ToString)
    End Function

    Public Function FGetBinCodeByID(ByVal CompID As Integer, ByVal BranchID As Integer, ByVal FBinID As Long) As String
        Dim MyWmsDBTrans As WmsDBTrans = New WmsDBTrans

        Dim FBinCode As String
        Dim SqlSelStr As String

        SqlSelStr = "SELECT WHBINCODE FROM TWMSBINS WHERE WHBINID=" & FBinID.ToString

       

        FBinCode = MyWmsDBTrans.OraDBWmsExSelectCmdRStr2Str(SqlSelStr)

        Return FBinCode
    End Function

    Public Function FGetBinIDByOldCode(ByVal CompID As Integer, ByVal BranchID As Integer, ByVal FBinOldCode As String) As Long
        Dim MyWmsDBTrans As WmsDBTrans = New WmsDBTrans

        Dim FBinIDStr As String
        Dim SqlSelStr As String

        Dim NewBinCode As String



        If Len(FBinOldCode) > 0 Then

            SqlSelStr = "SELECT WHBINID FROM TWMSBINS WHERE  FRIENDLYCODE='" & FBinOldCode & "'"

            If CompID > 0 Then
                SqlSelStr = SqlSelStr & " AND TWMSBINS.COMPID =" & CompID.ToString()
            End If
            If BranchID > 0 Then
                SqlSelStr = SqlSelStr & " AND TWMSBINS.BRANCHID =" & BranchID.ToString()
            End If

            FBinIDStr = MyWmsDBTrans.OraDBWmsExSelectCmdRN2String(SqlSelStr)
        Else
            Return -1
        End If

        If Len(FBinIDStr) > 0 And FBinIDStr <> "Null" Then
            Return Long.Parse(FBinIDStr.ToString)
        Else
            Return -1
        End If

    End Function

    Public Function FGetBinCodeByOldCode(ByVal CompID As Integer, ByVal BranchID As Integer, ByVal FBinOdCode As String) As String
        Dim MyWmsDBTrans As WmsDBTrans = New WmsDBTrans

        Dim SqlSelStr As String
        Dim NewBinCode As String

        Dim ThisCompID As Integer

        Dim FriendlyBinCode As String

        FriendlyBinCode = FBinOdCode.ToUpper


        SqlSelStr = "SELECT WHBINCODE  FROM TWMSBINS WHERE FRIENDLYCODE='" & FriendlyBinCode & "'"


        If CompID > 0 Then
            SqlSelStr = SqlSelStr & " AND COMPID =" & CompID.ToString()
        End If
        If BranchID > 0 Then
            SqlSelStr = SqlSelStr & " AND BRANCHID =" & BranchID.ToString()
        End If


        NewBinCode = MyWmsDBTrans.OraDBWmsExSelectCmdRStr2Str(SqlSelStr)

        If Len(NewBinCode) > 0 And NewBinCode <> "Null" Then

            Return NewBinCode
        Else
            Return "-1"
        End If



    End Function

    Public Function FGetItemIDByCode(ByVal CompID As Integer, ByVal FItemCode As String) As Long
        Dim MyWmsDBTrans As WmsDBTrans = New WmsDBTrans

        Dim FItemIDStr As String
        Dim SqlSelStr As String

        SqlSelStr = "SELECT ITEMID FROM TITEMS WHERE ITEMCODE='" & FItemCode & "' AND COMPID=" & CompID.ToString

        FItemIDStr = MyWmsDBTrans.OraDBWmsExSelectCmdRN2String(SqlSelStr)

        Return Long.Parse(FItemIDStr.ToString)
    End Function

    Public Function FCreateNewBinTransID() As Long
        Dim MyWmsDBTrans As WmsDBTrans = New WmsDBTrans
        Dim SqlSelect As String
        Dim SNewBinTransID As String

        SqlSelect = "SELECT SEQ_TWMSBINSTORETRANS.NEXTVAL  FROM DUAL"

        SNewBinTransID = MyWmsDBTrans.OraDBWmsExSelectCmdRN2String(SqlSelect)
        If Len(SNewBinTransID) > 0 Then
            If Long.Parse(SNewBinTransID) > 0 Then
                Return Long.Parse(SNewBinTransID)
            Else
                Return -1
            End If
        Else
            Return -1
        End If
    End Function

    'FNewBinQtyID
    Public Function FGetNewStoreTransID() As Long
        Dim MyWmsDBTrans As WmsDBTrans = New WmsDBTrans
        Dim SqlSelect As String
        Dim SNewID As String

        SqlSelect = "SELECT SEQ_TSTORETRANS.NEXTVAL  FROM DUAL"

        SNewID = MyWmsDBTrans.OraDBWmsExSelectCmdRN2String(SqlSelect)
        If Len(SNewID) > 0 Then
            If Long.Parse(SNewID) > 0 Then
                Return Long.Parse(SNewID)
            Else
                Return -1
            End If
        Else
            Return -1
        End If
    End Function

    Public Function FGetNewStoreTransDetailsID() As Long
        Dim MyWmsDBTrans As WmsDBTrans = New WmsDBTrans
        Dim SqlSelect As String
        Dim SNewID As String

        SqlSelect = "SELECT SEQ_TSTORETRANSDETAILS.NEXTVAL  FROM DUAL"

        SNewID = MyWmsDBTrans.OraDBWmsExSelectCmdRN2String(SqlSelect)
        If Len(SNewID) > 0 Then
            If Long.Parse(SNewID) > 0 Then
                Return Long.Parse(SNewID)
            Else
                Return -1
            End If
        Else
            Return -1
        End If
    End Function

    Public Function FCreateNewBinQtyID() As Long
        Dim MyWmsDBTrans As WmsDBTrans = New WmsDBTrans
        Dim SqlSelect As String
        Dim SNewBinQtyID As String

        SqlSelect = "SELECT SEQ_TWMSBINITEMSQTY.NEXTVAL  FROM DUAL"

        SNewBinQtyID = MyWmsDBTrans.OraDBWmsExSelectCmdRN2String(SqlSelect)
        If Len(SNewBinQtyID) > 0 Then
            If Long.Parse(SNewBinQtyID) > 0 Then
                Return Long.Parse(SNewBinQtyID)
            Else
                Return -1
            End If
        Else
            Return -1
        End If
    End Function

    Public Function FCreateNewCardID() As Long
        Dim MyWmsDBTrans As WmsDBTrans = New WmsDBTrans
        Dim SqlSelect As String
        Dim SNewCardID As String

        SqlSelect = "SELECT SEQ_TWMSPICKINGCARD.NEXTVAL FROM DUAL"

        SNewCardID = MyWmsDBTrans.OraDBWmsExSelectCmdRN2String(SqlSelect)
        If Len(SNewCardID) > 0 Then
            If Long.Parse(SNewCardID) > 0 Then
                Return Long.Parse(SNewCardID)
            Else
                Return -1
            End If
        Else
            Return -1
        End If
    End Function

    Public Function FCreateNewCardRowID() As Long
        Dim MyWmsDBTrans As WmsDBTrans = New WmsDBTrans
        Dim SqlSelect As String
        Dim SNewCardRowID As String

        SqlSelect = "SELECT SEQ_TWMSPICKINGCARDROWID.NEXTVAL FROM DUAL"

        SNewCardRowID = MyWmsDBTrans.OraDBWmsExSelectCmdRN2String(SqlSelect)
        If Len(SNewCardRowID) > 0 Then
            If Long.Parse(SNewCardRowID) > 0 Then
                Return Long.Parse(SNewCardRowID)
            Else
                Return -1
            End If
        Else
            Return -1
        End If
    End Function

    Public Function FGetNewPackItemID() As Long
        Dim MyWmsDBTrans As WmsDBTrans = New WmsDBTrans
        Dim SqlSelect As String
        Dim SNewID As String

        SqlSelect = "SELECT SEQ_TPACKAGES.NEXTVAL  FROM DUAL"

        SNewID = MyWmsDBTrans.OraDBWmsExSelectCmdRN2String(SqlSelect)
        If Len(SNewID) > 0 Then
            If Long.Parse(SNewID) > 0 Then
                Return Long.Parse(SNewID)
            Else
                Return -1
            End If
        Else
            Return -1
        End If
    End Function

    Public Function FGetNewPackItemDtlID() As Long
        Dim MyWmsDBTrans As WmsDBTrans = New WmsDBTrans
        Dim SqlSelect As String
        Dim SNewID As String

        SqlSelect = "SELECT SEQ_TPACKAGESDTL.NEXTVAL  FROM DUAL"

        SNewID = MyWmsDBTrans.OraDBWmsExSelectCmdRN2String(SqlSelect)
        If Len(SNewID) > 0 Then
            If Long.Parse(SNewID) > 0 Then
                Return Long.Parse(SNewID)
            Else
                Return -1
            End If
        Else
            Return -1
        End If
    End Function

    Public Function FCreateNewSessionID() As Long
        Dim MyWmsDBTrans As WmsDBTrans = New WmsDBTrans
        Dim SqlSelect As String
        Dim SNewSessionID As String

        SqlSelect = "SELECT SEQ_TWMSSESSIONS.NEXTVAL FROM DUAL"

        SNewSessionID = MyWmsDBTrans.OraDBWmsExSelectCmdRN2String(SqlSelect)
        If Len(SNewSessionID) > 0 Then
            If Long.Parse(SNewSessionID) > 0 Then
                Return Long.Parse(SNewSessionID)
            Else
                Return -1
            End If
        Else
            Return -1
        End If
    End Function

    Public Function FGetPackItemIDByNo(ByVal PackItemNo As String) As Long
        Dim MyWmsDBTrans As WmsDBTrans = New WmsDBTrans
        Dim SqlSelect As String
        Dim SPackItemID As String

        SqlSelect = "SELECT PACKITEMID FROM TPACKAGES WHERE PACKITEMNO='" & PackItemNo & "'"

        SPackItemID = MyWmsDBTrans.OraDBWmsExSelectCmdRN2String(SqlSelect)
        If Len(SPackItemID) > 0 Then
            If Long.Parse(SPackItemID) > 0 Then
                Return Long.Parse(SPackItemID)
            Else
                Return -1
            End If
        Else
            Return -1
        End If
    End Function

    Public Function FGetPackItemTinyInfo(ByVal CompID As Integer, ByVal BranchID As Integer, ByVal PackItemID As Long, ByVal PackItemNo As String) As DataSet
        Dim MyWmsDBTrans As WmsDBTrans = New WmsDBTrans
        Dim SqlSelect As String
        Dim SPackItemID As String
        Dim Ds As New DataSet

        SqlSelect = "SELECT PACKITEMNO,PACKITEMID,STORETRANSID,RESRVTORETRANSID,INWAREHOUSE,ORDERID,ERPITEMID,MAINITEMCATID,"
        SqlSelect = SqlSelect & "ROUTEID,BINCODE "
        SqlSelect = SqlSelect & "FROM TPACKAGES WHERE "
        If CompID > 0 Then
            SqlSelect = SqlSelect & " COMPID=" & CompID.ToString
            If BranchID > 0 Then
                SqlSelect = SqlSelect & " AND BRANCHID=" & BranchID.ToString()
            End If
        End If
        If PackItemID > 0 Then
            SqlSelect = SqlSelect & " AND PACKITEMID=" & PackItemID.ToString
        ElseIf Len(PackItemNo) > 0 Then
            SqlSelect = SqlSelect & " AND PACKITEMNO='" & PackItemNo & "'"
        Else
            Ds = Nothing
            Return Ds
        End If


        Ds = MyWmsDBTrans.OraDBFillDataset(SqlSelect, "PACKAGETINYINFO")

        Return Ds
    End Function

    Public Function FGetBinStoreID(ByVal WhBinID As Long) As Long
        Dim MyWmsDBTrans As WmsDBTrans = New WmsDBTrans
        Dim SqlSelect As String
        Dim SZoneID, SStoreID As String

        SqlSelect = "SELECT WHZONEID FROM TWMSBINS WHERE WHBINID= " & WhBinID.ToString()

        SZoneID = MyWmsDBTrans.OraDBWmsExSelectCmdRN2String(SqlSelect)

        Try
            SqlSelect = "SELECT STOREID  FROM TWMSZONES WHERE WHZONEID= " & SZoneID
            SStoreID = MyWmsDBTrans.OraDBWmsExSelectCmdRN2String(SqlSelect)
        Catch ex As Exception
            Return -1
        End Try

        If Len(SStoreID) > 0 Then
            If Long.Parse(SStoreID) > 0 Then
                Return Long.Parse(SStoreID)
            Else
                Return -1
            End If
        Else
            Return -1
        End If
    End Function

    Public Function FGetProdPhaseBinID(ByVal ProdPhaseID As Integer) As Long
        Dim MyWmsDBTrans As WmsDBTrans = New WmsDBTrans
        Dim SqlSelect As String
        Dim SZoneID, SStoreID As String
        Dim WhBinID As Long

        SqlSelect = "SELECT WHBINID FROM TPRODPHASES WHERE  PRODPHASEID = " & ProdPhaseID.ToString()

        Try
            WhBinID = Long.Parse(MyWmsDBTrans.OraDBWmsExSelectCmdRN2String(SqlSelect))
        Catch ex As Exception
            WhBinID = -1
        End Try



        If WhBinID > 0 Then

            Return WhBinID
        Else
            Return -1
        End If

    End Function

    Public Function FGetNewInventoryID() As Long
        Dim MyWmsDBTrans As WmsDBTrans = New WmsDBTrans
        Dim SqlSelect As String
        Dim SNewInventoryID As String

        SqlSelect = "SELECT SEQ_TWMSINVENTORY.NEXTVAL  FROM DUAL"

        SNewInventoryID = MyWmsDBTrans.OraDBWmsExSelectCmdRN2String(SqlSelect)

        If Len(SNewInventoryID) > 0 Then
            If Integer.Parse(SNewInventoryID) > 0 Then
                Return Long.Parse(SNewInventoryID)
            Else
                Return -1
            End If
        Else
            Return -1
        End If
    End Function

    Public Function FSetPaletteOff(ByVal CompID As Integer, ByVal BranchId As Integer, ByVal PackItemID As Long, ByVal PackItemNo As String) As Integer

        Dim MyWmsDBtrans As New WmsDBTrans

        Dim SqlCmd As String

        If PackItemID > 0 Then
            SqlCmd = " UPDATE TPACKAGES SET BINCODE = NULL,INWAREHOUSE=-1 WHERE PACKITEMID= " & PackItemID.ToString()
        ElseIf Len(PackItemNo) > 0 Then
            SqlCmd = " UPDATE TPACKAGES SET BINCODE = NULL,INWAREHOUSE=-1 WHERE PACKITEMNO= '" & PackItemID.ToString() & "'"
            If CompID > 0 Then
                SqlCmd = SqlCmd & " AND COMPID=" & CompID.ToString
            End If
            If BranchId > 0 Then
                SqlCmd = SqlCmd & " AND BRANCHID=" & BranchId.ToString
            End If
        End If

        FSetPaletteOff = MyWmsDBtrans.OraDBExecuteSQLCmd(SqlCmd, 1)

    End Function

    Public Function FCalcItemWeightQty(ByVal ItemID As Long, ByVal ItemLength As Decimal, ByVal ItemBarsQty As Long, ByVal CompId As Integer) As Decimal
        Dim MyWmsDBTrans As WmsDBTrans = New WmsDBTrans
        Dim SqlSelect As String
        Dim WeightPerMUnit As Decimal
        Dim WeightPerMUnitStr As String
        Dim ItemLengthPrecisionStr As String
        Dim ItemLengthPrecision As Integer

        Dim ItemWeightQty As Decimal

        Dim itemlprecstr As String

        ItemWeightQty = 0

        SqlSelect = "SELECT MUNITSRELATION  FROM TITEMS WHERE ITEMID =" & ItemID.ToString()

        WeightPerMUnitStr = MyWmsDBTrans.OraDBWmsExSelectCmdRN2String(SqlSelect)

        SqlSelect = "SELECT  ITEMLENGTHPRECISION  FROM  TSTOREPARAMS WHERE COMPID=" & CompId.ToString
        ItemLengthPrecisionStr = MyWmsDBTrans.OraDBWmsExSelectCmdRN2String(SqlSelect)


        If Len(ItemLengthPrecisionStr) > 0 Then
            If ItemLengthPrecisionStr <> "Null" Then
                ItemLengthPrecision = Integer.Parse(ItemLengthPrecisionStr)
            Else
                ItemLengthPrecision = 0

            End If
        Else
            ItemLengthPrecision = 0
        End If

        If Len(WeightPerMUnitStr) > 0 Then
            If WeightPerMUnitStr <> "Null" Then
                WeightPerMUnit = Decimal.Parse(WeightPerMUnitStr)
                If ItemLengthPrecision > 0 Then
                    ItemWeightQty = ItemBarsQty * ItemLength * WeightPerMUnit
                Else
                    'itemlprecstr = "1" + fill("0", ItemLengthPrecision)
                    itemlprecstr = "1" & New String("0", 1, ItemLengthPrecision)
                    ItemWeightQty = ItemBarsQty * (ItemLength / Long.Parse(itemlprecstr)) * WeightPerMUnit
                End If

            End If
        End If

        Return ItemWeightQty

    End Function

    Public Function FGetFirstShippingBinID(ByVal BranchID As Integer) As Long

        Dim MyWmsDBTrans As WmsDBTrans = New WmsDBTrans

        Dim SqlStr As String
        Dim TransBinCode As String


        SqlStr = "SELECT MIN(WHBINID) AS  WHBINID FROM TWMSBINS WHERE TWMSBINS.WHBINTYPE = 2 AND BRANCHID=" + BranchID.ToString()
        Try
            Return Long.Parse(MyWmsDBTrans.OraDBWmsExSelectCmdRN2String(SqlStr))
        Catch ex As Exception
            Return -1
        End Try

    End Function

#Region " Bin Trans "

    Public Function FCreateWmsTrans( _
            ByVal FCompID As Integer, _
            ByVal FBranchID As Integer, _
            ByVal FWmsTransType As Integer, _
            ByVal FBinID As Integer, _
            ByVal FBinIDFrom As Integer, _
            ByVal FBinIDTo As Integer, _
            ByVal FitemID As Long, _
            ByVal FitemLotID As Long, _
            ByVal FItemPQty As Decimal, _
            ByVal FItemPMunit As Integer, _
            ByVal FItemSQty As Decimal, _
            ByVal FItemSMunit As Integer, _
            ByVal FItemPackQty As Decimal, _
            ByVal FItemPackMunit As Integer, _
            ByVal FTransEx As Integer, _
            ByVal FStoreTRansId As Long, _
            ByVal FStoreTRansDtlId As Long, _
            ByVal FDDCreateUser As Integer, _
            ByVal FTerminal As String, _
            ByVal FItemLength As Decimal, _
            ByVal FItemColorID As Integer, _
            ByVal FItemColorID2 As Integer, _
            ByVal FPackItemID As Long, _
            ByVal FPackItemNo As String, _
            ByVal FPackItemIDTo As Long, _
            ByVal FPackItemNoTo As String) As Long

        Dim InsSqlStr As String

        Dim NewWmsTransID As Long
        Dim MyWmsDBTrans As WmsDBTrans = New WmsDBTrans

        Dim SqlExRtr As Long
        InsSqlStr = FCreateWmsTransSqlStr( _
                     FCompID, _
                     FBranchID, _
                     FWmsTransType, _
                     FBinID, _
                     FBinIDFrom, _
                     FBinIDTo, _
                     FitemID, _
                     FitemLotID, _
                     FItemPQty, _
                     FItemPMunit, _
                     FItemSQty, _
                     FItemSMunit, _
                     FItemPackQty, _
                     FItemPackMunit, _
                     FTransEx, _
                     FStoreTRansId, _
                     FStoreTRansDtlId, _
                     FDDCreateUser, _
                     FTerminal, _
                     FItemLength, _
                     FItemColorID, _
                     FItemColorID2, _
                     FPackItemID, _
                     FPackItemNo, _
                     FPackItemIDTo, _
                     FPackItemNoTo)

        SqlExRtr = MyWmsDBTrans.OraDBExecuteSQLCmd(InsSqlStr, 1)
        If SqlExRtr > 0 Then
            NewWmsTransID = gNewWmsTRansID
        Else
            MyWmsDBTrans.f_sqlerrorlog(FCompID, "WmsBinTrans.FCreateWmsTrans>>", InsSqlStr, "Admin")
        End If
        gNewWmsTRansID = 0

        Return NewWmsTransID
    End Function

    Public Function FCreateWmsTransSqlStr( _
            ByVal FCompID As Integer, _
            ByVal FBranchID As Integer, _
            ByVal FWmsTransType As Integer, _
            ByVal FBinID As Long, _
            ByVal FBinIDFrom As Integer, _
            ByVal FBinIDTo As Integer, _
            ByVal FitemID As Long, _
            ByVal FitemLotID As Long, _
            ByVal FItemPQty As Decimal, _
            ByVal FItemPMunit As Integer, _
            ByVal FItemSQty As Decimal, _
            ByVal FItemSMunit As Integer, _
            ByVal FItemPackQty As Decimal, _
            ByVal FItemPackMunit As Integer, _
            ByVal FTransEx As Integer, _
            ByVal FStoreTRansId As Long, _
            ByVal FStoreTRansDtlId As Long, _
            ByVal FDBCreateUser As Integer, _
            ByVal FTerminal As String, _
            ByVal FItemLength As Decimal, _
            ByVal FItemColorID As Integer, _
            ByVal FItemColorID2 As Integer, _
            ByVal FPackItemID As Long, _
            ByVal FPackItemNo As String, _
            ByVal FPackItemIDTo As Long, _
            ByVal FPackItemNoTo As String) As String

        ' build SQL statement        
        Dim SWmsTransType As String
        Dim SBinID As String
        Dim SBinIDFrom As String
        Dim SBinIDTo As String
        Dim SItemID As String
        Dim SItemLotID As String
        Dim SItemPQty As String
        Dim SItemPMunit As String
        Dim SItemSQty As String
        Dim SItemSMunit As String
        Dim SItemPackQty As String
        Dim SItemPackMunit As String
        Dim STransEx As String
        Dim SStoreTRansId As String
        Dim SStoreTRansDtlId As String
        Dim SDBCreateUser As String
        Dim STerminal As String
        Dim SItemLength As String
        Dim SItemColorID As String
        Dim SItemColorID2 As String
        Dim SPackItemID As String
        Dim SPackItemNo As String

        Dim LBinStoreTransID As Long
        Dim SBinStoreTransID As String
        Dim SqlStrCmd As String
        SqlStrCmd = "INSERT INTO TWMSBINSTORETRANS(COMPID,BRANCHID,BINSTORETRANSID,WMSTRANSTYPE,TRANSEX,"
        SqlStrCmd = SqlStrCmd & "WHBINID,WHBINIDFROM,WHBINIDTO,"
        SqlStrCmd = SqlStrCmd & "ITEMID,ITEMLOTID,"
        SqlStrCmd = SqlStrCmd & "ITEMQTYPRIMARY, MUNITPRIMARY,"
        SqlStrCmd = SqlStrCmd & "ITEMQTYSECONDARY,MUNITSECONDARY,"
        SqlStrCmd = SqlStrCmd & "ITEMPACKQTY,MUNITPACK,"
        SqlStrCmd = SqlStrCmd & "STORETRANSID,STORETRANSDTLID,"
        SqlStrCmd = SqlStrCmd & "ITEMLENGTH,ITEMCOLORID,ITEMCOLORID2,"
        SqlStrCmd = SqlStrCmd & "PACKITEMID,PACKITEMNO,PACKITEMIDTO,PACKITEMNOTO,"
        SqlStrCmd = SqlStrCmd & "DBRECCREATEUSER,"
        SqlStrCmd = SqlStrCmd & "TERMINAL) "
        SqlStrCmd = SqlStrCmd & " VALUES ("

        LBinStoreTransID = FCreateNewBinTransID()
        gNewWmsTRansID = LBinStoreTransID

        If FCompID > 0 Then
            SqlStrCmd = SqlStrCmd & FCompID.ToString() & ","
        Else
            SqlStrCmd = SqlStrCmd & "NULL,"
        End If
        If FBranchID > 0 Then
            SqlStrCmd = SqlStrCmd & FBranchID.ToString() & ","
        Else
            SqlStrCmd = SqlStrCmd & "NULL,"
        End If
        If LBinStoreTransID > 0 Then
            SBinStoreTransID = LBinStoreTransID.ToString
        Else
            SBinStoreTransID = "NULL"
        End If
        SqlStrCmd = SqlStrCmd & SBinStoreTransID & ","

        If FWmsTransType > 0 Then
            SWmsTransType = FWmsTransType.ToString
        Else
            SWmsTransType = "NULL"
        End If
        SqlStrCmd = SqlStrCmd & SWmsTransType & ","

        If FTransEx > -2 Then
            STransEx = FTransEx.ToString
        Else
            STransEx = "NULL"
        End If
        SqlStrCmd = SqlStrCmd & STransEx & ","
        If FBinID > 0 Then
            SBinID = FBinID.ToString
        Else
            SBinID = "NULL"
        End If
        SqlStrCmd = SqlStrCmd & SBinID & ","
        'SBinIDFrom
        If FBinIDFrom > -2 Then
            SBinIDFrom = FBinIDFrom.ToString
        Else
            SBinIDFrom = "NULL"
        End If
        SqlStrCmd = SqlStrCmd & SBinIDFrom & ","
        'SBinIDTo
        If FBinIDTo > -2 Then
            SBinIDTo = FBinIDTo.ToString
        Else
            SBinIDTo = "NULL"
        End If
        SqlStrCmd = SqlStrCmd & SBinIDTo & ","
        'FItemID
        If FitemID > -2 Then
            SItemID = FitemID.ToString
        Else
            SItemID = "NULL"
        End If
        SqlStrCmd = SqlStrCmd & SItemID & ","
        'SItemLotID
        If FitemLotID > -2 Then
            SItemLotID = FitemLotID.ToString
        Else
            SItemLotID = "NULL"
        End If
        SqlStrCmd = SqlStrCmd & SItemLotID & ","

        If FItemPQty > 0 Then
            SItemPQty = f_dbinsertdecimal(FItemPQty, 0)
        Else
            SItemPQty = "NULL"
        End If
        SqlStrCmd = SqlStrCmd & SItemPQty & ","
        'f_dbinsertdecimal()
        If FItemPMunit > 0 Then
            SItemPMunit = FItemPMunit.ToString
        Else
            SItemPMunit = "NULL"
        End If
        SqlStrCmd = SqlStrCmd & SItemPMunit & ","
        '=== Calc weight if needed

        'FCalcItemWeightQty
        If FItemSQty = 0 Or FItemSQty = Nothing Then

            FItemSQty = FCalcItemWeightQty(FitemID, FItemLength, FItemPQty, FCompID)
        End If
        'Fill String
        If FItemSQty > 0 Then
            SItemSQty = f_dbinsertdecimal(FItemSQty, 0)
        Else
            SItemSQty = "NULL"
        End If
        SqlStrCmd = SqlStrCmd & SItemSQty & ","
        If FItemSMunit > 0 Then
            SItemSMunit = FItemSMunit.ToString
        Else
            SItemSMunit = "NULL"
        End If
        SqlStrCmd = SqlStrCmd & SItemSMunit & ","
        If FItemPackQty > 0 Then
            SItemPackQty = f_dbinsertdecimal(FItemPackQty, 0)
        Else
            SItemPackQty = "NULL"
        End If
        SqlStrCmd = SqlStrCmd & SItemPackQty & ","
        If FItemPackMunit > 0 Then
            SItemPackMunit = FItemPackMunit.ToString
        Else
            SItemPackMunit = "NULL"
        End If
        SqlStrCmd = SqlStrCmd & SItemPackMunit & ","
        If FStoreTRansId > 0 Then
            SStoreTRansId = FStoreTRansId.ToString
        Else
            SStoreTRansId = "NULL"
        End If
        SqlStrCmd = SqlStrCmd & SStoreTRansId & ","
        If FStoreTRansDtlId > 0 Then
            SStoreTRansDtlId = FStoreTRansDtlId.ToString
        Else
            SStoreTRansDtlId = "NULL"
        End If
        SqlStrCmd = SqlStrCmd & SStoreTRansDtlId & ","
        If FItemLength > 0 Then
            SItemLength = f_dbinsertdecimal(FItemLength, 3)
        Else
            SItemLength = "NULL"
        End If
        SqlStrCmd = SqlStrCmd & SItemLength & ","
        If FItemColorID > 0 Then
            SItemColorID = FItemColorID.ToString
        Else
            SItemColorID = "NULL"
        End If
        SqlStrCmd = SqlStrCmd & SItemColorID & ","
        If FItemColorID2 > 0 Then
            SItemColorID2 = FItemColorID2.ToString
        Else
            SItemColorID2 = "NULL"
        End If
        SqlStrCmd = SqlStrCmd & SItemColorID2 & ","
        If FPackItemID > 0 Then
            SPackItemID = FPackItemID.ToString
        Else
            SPackItemID = "NULL"
        End If
        SqlStrCmd = SqlStrCmd & SPackItemID & ","
        If Len(FPackItemNo) > 0 Then
            SPackItemNo = "'" & FPackItemNo & "'"
        Else
            SPackItemNo = "NULL"
        End If
        SqlStrCmd = SqlStrCmd & SPackItemNo & ","
        If FPackItemIDTo > 0 Then
            SqlStrCmd = SqlStrCmd & FPackItemIDTo.ToString & ","
        Else
            SqlStrCmd = SqlStrCmd & "NULL,"
        End If
        If Len(FPackItemNoTo) > 0 Then
            SqlStrCmd = SqlStrCmd & "'" & FPackItemNoTo & "',"
        Else
            SqlStrCmd = SqlStrCmd & "NULL,"
        End If
        If FDBCreateUser > 0 Then
            SDBCreateUser = FDBCreateUser.ToString
        Else
            SDBCreateUser = "NULL"
        End If
        SqlStrCmd = SqlStrCmd & SDBCreateUser & ","

        If Len(FTerminal) > 0 Then
            STerminal = "'" & FTerminal & "'"
        Else
            STerminal = "NULL"
        End If
        SqlStrCmd = SqlStrCmd & STerminal
        SqlStrCmd = SqlStrCmd & ")"

        Return SqlStrCmd
    End Function

    Public Function FGetPackItemNoInfoInternal(ByVal PackItemNo As String) As DataSet
        'OraDBFillDataset()
        Dim Ds As DataSet
        Dim SqlStr As String

        Dim MyWmsDBTrans As WmsDBTrans = New WmsDBTrans

        SqlStr = "SELECT PACKITEMID,PACKITEMNO,ITEMCODE,ITEMDESC,ITEMID,ITEMLENGTH,ITEMCOLORID,ITEMCOLORID2,"
        SqlStr = SqlStr & "COLORCODE,COLORDESC, COLORCODE2, COLORDESC2, PACKITEMBARS, PACKITEMWEIGHT,"
        SqlStr = SqlStr & "INITPACKITEMBARS,INITPACKITEMWEIGHT,INWAREHOUSE,ORDERDTLID,RESRVTORETRANSID,SRCITEMS,PRODID  "
        SqlStr = SqlStr & "FROM VPACKAGEINFO2 WHERE PACKITEMNO = '" & PackItemNo & "'"
        Try
            Ds = MyWmsDBTrans.OraDBFillDataset(SqlStr, "PACKITEMINFO")
        Catch ex As Exception
            MyWmsDBTrans.f_sqlerrorlog(1, "WmsBinTrans.FGetPackItemNoInfoInternal", SqlStr, "Admin")
        End Try


        Return Ds
    End Function
    Public Function FGetPackItemNoInfoInternalByID(ByVal PackItemID As Long) As DataSet
        'OraDBFillDataset()
        Dim Ds As DataSet
        Dim SqlStr As String

        Dim MyWmsDBTrans As WmsDBTrans = New WmsDBTrans

        SqlStr = "SELECT PACKITEMID,PACKITEMNO,ITEMCODE,ITEMDESC,ITEMID,ITEMLENGTH,ITEMCOLORID,ITEMCOLORID2,"
        SqlStr = SqlStr & "COLORCODE,COLORDESC, COLORCODE2, COLORDESC2, PACKITEMBARS, PACKITEMWEIGHT,"
        SqlStr = SqlStr & "INITPACKITEMBARS,INITPACKITEMWEIGHT,INWAREHOUSE,ORDERDTLID,RESRVTORETRANSID,SRCITEMS,PRODID  "
        SqlStr = SqlStr & "FROM VPACKAGEINFO2 WHERE PACKITEMID = " & PackItemID.ToString

        Try
            Ds = MyWmsDBTrans.OraDBFillDataset(SqlStr, "PACKITEMINFO")
        Catch ex As Exception
            MyWmsDBTrans.f_sqlerrorlog(1, "WmsBinTrans.FGetPackItemNoInfoInternal", SqlStr, "Admin")
        End Try


        Return Ds
    End Function

    Public Function FGetBinItemsInfo(ByVal CompID As Integer, ByVal BranchID As Integer, ByVal WhBinCode As String) As DataSet
        'OraDBFillDataset()
        Dim Ds As DataSet
        Dim SqlStr As String

        Dim MyWmsDBTrans As WmsDBTrans = New WmsDBTrans

        SqlStr = "SELECT WHBINID,WHBINCODE,FRIENDLYCODE,STORETRANSID,ORDERID,ITEMID,ITEMCODE,ITEMDESC,ITEMLENGTH,ITEMCOLORID,ITEMCOLORID2,"
        SqlStr = SqlStr & "COLORCODE,COLORDESC, COLORCODE2, COLORDESC2, BINITEMQTYPRIMARY, BINITEMQTYSECONDARY,BINPACKITEMQTY,MUNITPRIMARY,MUNITSECONDARY,"
        SqlStr = SqlStr & "PACKITEMID,PACKITEMNO "
        SqlStr = SqlStr & "FROM VWMSBINSTATUS WHERE WHBINCODE = '" & WhBinCode & "'"

        If CompID > 0 Then
            SqlStr = SqlStr & " AND COMPID=" & CompID.ToString
        End If
        If BranchID > 0 Then
            SqlStr = SqlStr & " AND BRANCHID=" & BranchID.ToString
        End If

        Try
            Ds = MyWmsDBTrans.OraDBFillDataset(SqlStr, "BINITEMSINFO")
        Catch ex As Exception
            MyWmsDBTrans.f_sqlerrorlog(1, "WmsBinTrans.FGetBinItemsInfo", SqlStr, "Admin")
        End Try


        Return Ds
    End Function


    Function FUpdateBinQty( _
                      ByVal CompID As Integer, _
                      ByVal BranchID As Integer, _
                      ByVal TransEx As Integer, _
                      ByVal PackItemNo As String, _
                      ByVal WhBinID As Long, _
                      ByVal ItemID As Long, _
                      ByVal ItemLotID As Long, _
                      ByVal ItemLength As Decimal, _
                      ByVal ItemColorID As Integer, _
                      ByVal ItemColorID2 As Integer, _
                      ByVal ItemPQty As Decimal, _
                      ByVal ItemPMUnit As Integer, _
                      ByVal ItemSQty As Decimal, _
                      ByVal ItemSMUnit As Integer, _
                      ByVal ItemPackQty As Decimal, _
                      ByVal ItemPackMUnit As Integer, _
                      ByVal FOrderID As Long, _
                      ByVal FOrderDtlID As Long, _
                      ByVal FResrvStoreTRansID As Long, _
                      ByVal DBCreateUser As Integer, _
                      ByVal Terminal As String) As Integer

        Dim MyWmsDBTrans As New WmsDBTrans
        Dim ExPackItemQty As Decimal  'Existing qty
        Dim UpdRtRn As Long

        Dim ThisItemSQty As Decimal

        If ItemSQty > 0 Then
            ThisItemSQty = ItemSQty
        Else
            ThisItemSQty = Me.FCalcItemWeightQty(ItemID, ItemLength, ItemPQty, CompID)
        End If

        ExPackItemQty = FGetBinIPackNoTemQty(PackItemNo, WhBinID, ItemID, ItemLotID, ItemLength, ItemColorID, ItemColorID2, FOrderID, FOrderDtlID)

        If ExPackItemQty > 0 Then
            'Update Bin Qty
            If ItemPQty > ExPackItemQty And TransEx = -1 Then
                Return -2 'Exist Qty not enouph for export
            ElseIf ItemPQty = ExPackItemQty And TransEx = -1 Then
                'Delete Bin Qty
                UpdRtRn = FDeleteBinItemQty(CompID, BranchID, PackItemNo, WhBinID, ItemID, ItemLotID, ItemLength, ItemColorID, ItemColorID2, ItemPQty, ItemPMUnit, ThisItemSQty, ItemSMUnit)

            Else
                UpdRtRn = FUpdateBinItemQty(CompID, BranchID, TransEx, PackItemNo, WhBinID, ItemID, ItemLotID, ItemLength, ItemColorID, ItemColorID2, ItemPQty, ItemPMUnit, ThisItemSQty, ItemSMUnit, FOrderID, FOrderDtlID, FResrvStoreTRansID)
            End If
        ElseIf TransEx = 1 Then
            'Insert new Item qty in Bin
            UpdRtRn = FInsertBinItemQty(CompID, BranchID, TransEx, PackItemNo, WhBinID, ItemID, ItemLotID, ItemLength, ItemColorID, ItemColorID2, ItemPQty, ItemPMUnit, ThisItemSQty, ItemSMUnit, FOrderID, FOrderDtlID, FResrvStoreTRansID, DBCreateUser, Terminal)
        End If

        If UpdRtRn > 0 Then
            Return 1
        Else
            'DEBUG
            MyWmsDBTrans.f_sqlerrorlog(CompID, "WmsBinTrans.FUpdateBinQty>>", "Nothing updated!", "Admin")
            Return -1
        End If
    End Function

    Function FUpdateBinQtyAlter( _
                      ByVal CompID As Integer, _
                      ByVal BranchID As Integer, _
                      ByVal TransEx As Integer, _
                      ByVal PackItemNo As String, _
                      ByVal WhBinID As Long, _
                      ByVal ItemID As Long, _
                      ByVal ItemLotID As Long, _
                      ByVal ItemLength As Decimal, _
                      ByVal ItemColorID As Integer, _
                      ByVal ItemColorID2 As Integer, _
                      ByVal ItemPQty As Decimal, _
                      ByVal ItemPMUnit As Integer, _
                      ByVal ItemSQty As Decimal, _
                      ByVal ItemSMUnit As Integer, _
                      ByVal ItemPackQty As Decimal, _
                      ByVal ItemPackMUnit As Integer, _
                      ByVal FOrderID As Long, _
                      ByVal FOrderDtlID As Long, _
                      ByVal FResrvStoreTRansID As Long, _
                      ByVal DBCreateUser As Integer, _
                      ByVal Terminal As String) As Integer

        Dim MyWmsDBTrans As New WmsDBTrans
        Dim ExPackItemQty As Decimal  'Existing qty
        Dim UpdRtRn As Long

        Dim ThisItemSQty As Decimal

        If ItemSQty > 0 Then
            ThisItemSQty = ItemSQty
        Else
            ThisItemSQty = Me.FCalcItemWeightQty(ItemID, ItemLength, ItemPQty, CompID)
        End If


        'ExPackItemQty = FGetBinIPackNoTemQty(PackItemNo, WhBinID, ItemID, ItemLotID, ItemLength, ItemColorID, ItemColorID2, FOrderID, FOrderDtlID)
        ExPackItemQty = FGetBinIPackNoTemQty("", WhBinID, ItemID, ItemLotID, ItemLength, ItemColorID, ItemColorID2, FOrderID, FOrderDtlID)

        'MyWmsDBTrans.f_sqlerrorlog(CompID, "WmsBinTrans.FUpdateBinQtyAlter>>ExPackItemQty::", ExPackItemQty.ToString(), "Admin")

        If ExPackItemQty > 0 Then
            'Update Bin Qty
            If ItemPQty >= ExPackItemQty And TransEx = -1 Then
                'Delete Bin Qty
                UpdRtRn = FDeleteBinItemQty(CompID, BranchID, PackItemNo, WhBinID, ItemID, ItemLotID, ItemLength, ItemColorID, ItemColorID2, ItemPQty, ItemPMUnit, ThisItemSQty, ItemSMUnit)

            Else
                UpdRtRn = FUpdateBinItemQty(CompID, BranchID, TransEx, PackItemNo, WhBinID, ItemID, ItemLotID, ItemLength, ItemColorID, ItemColorID2, ItemPQty, ItemPMUnit, ThisItemSQty, ItemSMUnit, FOrderID, FOrderDtlID, FResrvStoreTRansID)
            End If
        ElseIf TransEx = 1 Then
            'Insert new Item qty in Bin
            UpdRtRn = FInsertBinItemQty(CompID, BranchID, TransEx, PackItemNo, WhBinID, ItemID, ItemLotID, ItemLength, ItemColorID, ItemColorID2, ItemPQty, ItemPMUnit, ThisItemSQty, ItemSMUnit, FOrderID, FOrderDtlID, FResrvStoreTRansID, DBCreateUser, Terminal)
        End If

        If UpdRtRn > 0 Then
            Return 1
        Else
            'DEBUG
            MyWmsDBTrans.f_sqlerrorlog(CompID, "WmsBinTrans.FUpdateBinQtyAlter>>", "Nothing updated!", "Admin")
            Return -1
        End If
    End Function

    Function FGetBinITemQty(ByVal WhBinID As Long, _
                          ByVal ItemID As Long, _
                          ByVal ItemLotID As Long, _
                          ByVal ItemLength As Decimal, _
                          ByVal ItemColorID As Integer, _
                          ByVal ItemColorID2 As Integer) As Decimal

        Dim MyWmsDBTrans As WmsDBTrans = New WmsDBTrans
        Dim SqlStr As String
        Dim ItemLengthStr As String
        Dim SqlSelRtrn As String

        SqlStr = "SELECT NVL(SUM(ITEMQTYPRIMARY),0) AS BINITEMPQTY FROM TWMSBINITEMSQTY WHERE WHBINID=" & WhBinID.ToString

        If ItemID > 0 Then
            SqlStr = SqlStr & " AND ITEMID=" & ItemID.ToString
        End If

        If ItemLotID > 0 Then
            SqlStr = SqlStr & " AND ITEMLOTID=" & ItemLotID.ToString
        End If

        If ItemLength > 0 Then
            ItemLengthStr = f_dbinsertdecimal(ItemLength, 4)
            SqlStr = SqlStr & " AND ITEMLENGTH=" & ItemLengthStr
        End If
        If ItemColorID > 0 Then
            SqlStr = SqlStr & " AND ITEMCOLORID=" & ItemColorID.ToString
        End If
        If ItemColorID2 > 0 Then
            SqlStr = SqlStr & " AND ITEMCOLORID2=" & ItemColorID2.ToString
        End If

        SqlSelRtrn = MyWmsDBTrans.OraDBWmsExSelectCmdRN2String(SqlStr)

        If Len(SqlSelRtrn) > 0 And SqlSelRtrn <> "Null" Then
            FGetBinITemQty = Decimal.Parse(SqlSelRtrn)
        Else
            FGetBinITemQty = 0
        End If

    End Function



    Function FGetBinIPackNoTemQty( _
                          ByVal PackItemNo As String, _
                          ByVal WhBinID As Long, _
                          ByVal ItemID As Long, _
                          ByVal ItemLotID As Long, _
                          ByVal ItemLength As Decimal, _
                          ByVal ItemColorID As Integer, _
                          ByVal ItemColorID2 As Integer, _
                          ByVal OrderID As Long, _
                          ByVal OrderDtlID As Long) As Decimal

        Dim MyWmsDBTrans As WmsDBTrans = New WmsDBTrans
        Dim SqlStr As String
        Dim ItemLengthStr As String
        Dim SqlSelRtrn As String

        SqlStr = "SELECT SUM(ITEMQTYPRIMARY) AS BINITEMPQTY FROM TWMSBINITEMSQTY WHERE WHBINID=" & WhBinID.ToString & " AND ITEMID=" & ItemID.ToString

        If Len(PackItemNo) > 0 Then
            SqlStr = SqlStr & " AND PACKITEMNO='" & PackItemNo.ToString + "'"
        End If
        If ItemLotID > 0 Then
            SqlStr = SqlStr & " AND ITEMLOTID=" & ItemLotID.ToString
        End If

        If ItemLength > 0 Then
            ItemLengthStr = f_dbinsertdecimal(ItemLength, 4)
            SqlStr = SqlStr & " AND ITEMLENGTH=" & ItemLengthStr
        End If
        If ItemColorID > 0 Then
            SqlStr = SqlStr & " AND ITEMCOLORID=" & ItemColorID.ToString
        End If
        If ItemColorID2 > 0 Then
            SqlStr = SqlStr & " AND ITEMCOLORID2=" & ItemColorID2.ToString
        End If
        If OrderID > 0 Then
            SqlStr = SqlStr & " AND ORDERID=" & OrderID.ToString
        End If
        If OrderDtlID > 0 Then
            SqlStr = SqlStr & " AND ORDERDTLID=" & OrderDtlID.ToString
        End If

        SqlSelRtrn = MyWmsDBTrans.OraDBWmsExSelectCmdRN2String(SqlStr)
        'DEBUG
        'MyWmsDBTrans.f_sqlerrorlog(1, "WmsBinTrans.FGetBinIPackNoTemQty>>", SqlStr, "Admin")

        If Len(SqlSelRtrn) > 0 And SqlSelRtrn <> "Null" Then
            FGetBinIPackNoTemQty = Decimal.Parse(SqlSelRtrn)
        Else
            FGetBinIPackNoTemQty = 0
        End If

    End Function


    Function FUpdatePaletteLocation(ByVal CompID As Integer, ByVal BranchID As Integer, ByVal PackItemNo As String, ByVal WhBinCode As String, ByVal IsNewPalette As Boolean) As Integer
        Dim SqlStr As String
        Dim SqlExRtrn
        Dim MyWmsDBTrans As WmsDBTrans = New WmsDBTrans

        Dim ThisPAckItemNo As String
        Dim ThisBinCode As String

        ThisPAckItemNo = PackItemNo
        ThisBinCode = WhBinCode

        If Len(ThisPAckItemNo) = 0 Then ThisPAckItemNo = " "
        If Len(ThisBinCode) = 0 Then ThisBinCode = " "


        SqlStr = "UPDATE TPACKAGES SET BINCODE='" & ThisBinCode & "'"

        If IsNewPalette = True Then
            SqlStr = SqlStr & ",INWAREHOUSE=1 "
        End If

        SqlStr = SqlStr & " WHERE PACKITEMNO='" & ThisPAckItemNo & "'"

        If CompID > 0 Then
            SqlStr = SqlStr & " AND COMPID=" & CompID.ToString()
        End If

        SqlExRtrn = MyWmsDBTrans.OraDBExecuteSQLCmd(SqlStr, 1)

        If SqlExRtrn > 0 Then
            Return 1
        Else
            'DEBUG
            MyWmsDBTrans.f_sqlerrorlog(CompID, "WmsBinTrans.FUpdatePaletteLocation>>", SqlStr, "Admin")

            Return -1
        End If

    End Function


    Function FUpdatePaletteShippingLocation(ByVal CompID As Integer, ByVal BranchID As Integer, ByVal PackItemID As Long, ByVal WhBinCode As String) As Integer
        Dim SqlStr As String
        Dim SqlExRtrn
        Dim MyWmsDBTrans As WmsDBTrans = New WmsDBTrans


        SqlStr = "UPDATE TPACKAGES SET BINCODE='" & WhBinCode & "'"

        SqlStr = SqlStr & ",INWAREHOUSE=2 "
        SqlStr = SqlStr & " WHERE PACKITEMID=" & PackItemID.ToString()

        SqlExRtrn = MyWmsDBTrans.OraDBExecuteSQLCmd(SqlStr, 1)

        If SqlExRtrn > 0 Then
            Return 1
        Else
            'DEBUG
            MyWmsDBTrans.f_sqlerrorlog(CompID, "WmsBinTrans.FUpdatePaletteLocation>>", SqlStr, "Admin")

            Return -1
        End If

    End Function


    Function FUpdateBinItemQty( _
                          ByVal CompID As Integer, _
                          ByVal BranchID As Integer, _
                          ByVal TransEx As Integer, _
                          ByVal PackItemNo As String, _
                          ByVal WhBinID As Long, _
                          ByVal ItemID As Long, _
                          ByVal ItemLotID As Long, _
                          ByVal ItemLength As Decimal, _
                          ByVal ItemColorID As Integer, _
                          ByVal ItemColorID2 As Integer, _
                          ByVal ItemPQty As Decimal, _
                          ByVal ItemPMUnit As Integer, _
                          ByVal ItemSQty As Decimal, _
                          ByVal ItemSMUnit As Integer, _
                          ByVal FOrderID As Long, _
                          ByVal FOrderDtlID As Long, _
                          ByVal FResrvStoreTRansID As Long) As Integer


        Dim SqlStr As String
        Dim SqlExRtrn As Long
        Dim MyWmsDBTrans As WmsDBTrans = New WmsDBTrans

        Dim putwhere As Boolean

        SqlStr = "UPDATE TWMSBINITEMSQTY SET "
        SqlStr = SqlStr & " ITEMQTYPRIMARY = NVL(ITEMQTYPRIMARY,0) + " & TransEx.ToString & " * " & f_dbinsertdecimal(ItemPQty, FGetItemMUnitDecimal(0)) & ","
        SqlStr = SqlStr & " ITEMQTYSECONDARY = NVL(ITEMQTYSECONDARY,0) + " & TransEx.ToString & " * " & f_dbinsertdecimal(ItemSQty, FGetItemMUnitDecimal(0))

        If FOrderID > 0 Then
            SqlStr = SqlStr & ",ORDERID=" & FOrderID.ToString()
        End If
        If FOrderDtlID > 0 Then
            SqlStr = SqlStr & ",ORDERDTLID=" & FOrderDtlID.ToString()
        End If
        If FResrvStoreTRansID > 0 Then
            SqlStr = SqlStr & ",RESRVSTORETRANSID=" & FResrvStoreTRansID.ToString()
        End If

        If WhBinID > 0 Then
            putwhere = True
            SqlStr = SqlStr & " WHERE WHBINID=" & WhBinID.ToString
        End If

        If ItemID > 0 Then
            If putwhere Then
                SqlStr = SqlStr & " AND ITEMID=" & ItemID.ToString
            Else
                SqlStr = SqlStr & " WHERE ITEMID=" & ItemID.ToString
            End If
            putwhere = True
        End If
        If ItemLength > 0 Then
            If putwhere Then
                SqlStr = SqlStr & " AND ITEMLENGTH=" & f_dbinsertdecimal(ItemLength, 4)
            Else
                putwhere = True
                SqlStr = SqlStr & " WHERE  ITEMLENGTH=" & f_dbinsertdecimal(ItemLength, 4)
            End If
        End If

        If ItemColorID > 0 Then
            If putwhere Then
                SqlStr = SqlStr & " AND ITEMCOLORID=" & ItemColorID.ToString
            Else
                putwhere = True
                SqlStr = SqlStr & " WHERE  ITEMCOLORID=" & ItemColorID.ToString
            End If

        End If

        If ItemColorID2 > 0 Then
            SqlStr = SqlStr & " AND ITEMCOLORID2=" & ItemColorID2.ToString
        End If

        If ItemLotID > 0 Then
            SqlStr = SqlStr & " AND ITEMLOTID=" & ItemLotID.ToString
        End If

        SqlExRtrn = MyWmsDBTrans.OraDBExecuteSQLCmd(SqlStr, 1)

        If SqlStr.Length = 0 Then SqlStr = "Empty sql"

        If SqlExRtrn > 0 Then
            Return 1
        Else
            'DEBUG
            MyWmsDBTrans.f_sqlerrorlog(CompID, "WmsBinTrans.FUpdateBinItemQty>>", SqlStr, "Admin")

            Return -1
        End If

    End Function


    Function FInsertBinItemQty( _
                              ByVal CompID As Integer, _
                              ByVal BranchID As Integer, _
                              ByVal TransEx As Integer, _
                              ByVal PackItemNo As String, _
                              ByVal WhBinID As Long, _
                              ByVal ItemID As Long, _
                              ByVal ItemLotID As Long, _
                              ByVal ItemLength As Decimal, _
                              ByVal ItemColorID As Integer, _
                              ByVal ItemColorID2 As Integer, _
                              ByVal ItemPQty As Decimal, _
                              ByVal ItemPMUnit As Integer, _
                              ByVal ItemSQty As Decimal, _
                              ByVal ItemSMUnit As Integer, _
                              ByVal FOrderID As Long, _
                              ByVal FOrderDtlID As Long, _
                              ByVal FResrvStoreTRansID As Long, _
                              ByVal FDBCreateUser As Integer, _
                              ByVal FTerminal As String) As Integer


        Dim SqlStr As String
        Dim SqlExRtrn As Long
        Dim PackItemIDStr As String

        Dim BinQtyID As Long

        Dim MyWmsDBTrans As WmsDBTrans = New WmsDBTrans


        BinQtyID = FCreateNewBinQtyID()

        If Len(PackItemNo) > 0 Then
            PackItemIDStr = MyWmsDBTrans.OraDBWmsExSelectCmdRN2String("SELECT PACKITEMID FROM TPACKAGES WHERE PACKITEMNO='" & PackItemNo.ToString & "'")
        End If

        SqlStr = "INSERT INTO TWMSBINITEMSQTY(BINITEMSQTYID,COMPID,BRANCHID,WHBINID,ITEMID,ITEMLOTID,PACKITEMID,PACKITEMNO,"
        SqlStr = SqlStr & "ITEMQTYPRIMARY,MUNITPRIMARY,ITEMQTYSECONDARY,MUNITSECONDARY,"
        SqlStr = SqlStr & "ITEMCOLORID,ITEMCOLORID2,ITEMLENGTH,ORDERID,ORDERDTLID,RESRVSTORETRANSID,"
        SqlStr = SqlStr & "DBRECCREATEUSER)"
        SqlStr = SqlStr & " VALUES ("

        If BinQtyID > 0 Then
            SqlStr = SqlStr & BinQtyID.ToString & ","
        Else
            SqlStr = SqlStr & "NULL,"
        End If
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
        If WhBinID > 0 Then
            SqlStr = SqlStr & WhBinID.ToString & ","
        Else
            SqlStr = SqlStr & "NULL,"
        End If
        If ItemID > 0 Then
            SqlStr = SqlStr & ItemID.ToString & ","
        Else
            SqlStr = SqlStr & "NULL,"
        End If
        If ItemLotID > 0 Then
            SqlStr = SqlStr & ItemLotID.ToString & ","
        Else
            SqlStr = SqlStr & "NULL,"
        End If
        If Len(PackItemIDStr) And PackItemIDStr <> "Null" Then
            SqlStr = SqlStr & PackItemIDStr & ","
        Else
            SqlStr = SqlStr & "NULL,"
        End If

        If Len(PackItemNo) > 0 Then
            SqlStr = SqlStr & "'" & PackItemNo & "',"
        Else
            SqlStr = SqlStr & "NULL,"
        End If



        If ItemPQty > 0 Then
            SqlStr = SqlStr & f_dbinsertdecimal(ItemPQty, FGetItemMUnitDecimal(ItemPMUnit)) & ","
        Else
            SqlStr = SqlStr & "NULL,"
        End If
        'FGetItemMUnitPrimary
        If ItemPMUnit > 0 Then
            SqlStr = SqlStr & ItemPMUnit.ToString & ","
        Else
            SqlStr = SqlStr & "NULL,"
        End If

        If ItemSQty > 0 Then
            SqlStr = SqlStr & f_dbinsertdecimal(ItemSQty, FGetItemMUnitDecimal(ItemSMUnit)) & ","
        Else
            SqlStr = SqlStr & "NULL,"
        End If

        If ItemSMUnit > 0 Then
            SqlStr = SqlStr & ItemSMUnit.ToString & ","
        Else
            SqlStr = SqlStr & "NULL,"
        End If
        If ItemColorID > 0 Then
            SqlStr = SqlStr & ItemColorID.ToString & ","
        Else
            SqlStr = SqlStr & "NULL,"
        End If

        If ItemColorID2 > 0 Then
            SqlStr = SqlStr & ItemColorID2.ToString & ","
        Else
            SqlStr = SqlStr & "NULL,"
        End If
        If ItemLength > 0 Then
            SqlStr = SqlStr & f_dbinsertdecimal(ItemLength, 3) & ","
        Else
            SqlStr = SqlStr & "NULL,"
        End If
        'FOrderID
        If FOrderID > 0 Then
            SqlStr = SqlStr & FOrderID.ToString() & ","
        Else
            SqlStr = SqlStr & "NULL,"
        End If
        'foRDERdTLID
        If FOrderDtlID > 0 Then
            SqlStr = SqlStr & FOrderDtlID.ToString() & ","
        Else
            SqlStr = SqlStr & "NULL,"
        End If
        If FResrvStoreTRansID > 0 Then
            SqlStr = SqlStr & FResrvStoreTRansID.ToString() & ","
        Else
            SqlStr = SqlStr & "NULL,"
        End If
        If FDBCreateUser > 0 Then
            SqlStr = SqlStr & FDBCreateUser.ToString
        Else
            SqlStr = SqlStr & "NULL"
        End If
        SqlStr = SqlStr & ")"

        SqlExRtrn = MyWmsDBTrans.OraDBExecuteSQLCmd(SqlStr, 1)

        If SqlExRtrn > 0 Then
            Return 1
        Else
            'DEBUG
            MyWmsDBTrans.f_sqlerrorlog(CompID, "WmsBinTrans. FInsertBinItemQty>>", SqlStr, "Admin")

            Return -1
        End If

    End Function

    Function FCancelBinTrans(ByVal BinTransID As Long) As Integer
        Dim SqlStr As String

        Dim MyWmsDBTrans As WmsDBTrans = New WmsDBTrans

        If BinTransID > 0 Then
            SqlStr = "DELETE FROM TWMSBINSTORETRANS WHERE BINSTORETRANSID =" & BinTransID.ToString()
            MyWmsDBTrans.OraDBExecuteSQLCmd(SqlStr, 1)
            Return 1
        Else
            Return -2
        End If

    End Function

    Function FDeleteBinItemQty( _
                          ByVal CompID As Integer, _
                          ByVal BranchID As Integer, _
                          ByVal PackItemNo As String, _
                          ByVal WhBinID As Long, _
                          ByVal ItemID As Long, _
                          ByVal ItemLotID As Long, _
                          ByVal ItemLength As Decimal, _
                          ByVal ItemColorID As Integer, _
                          ByVal ItemColorID2 As Integer, _
                          ByVal ItemPQty As Decimal, _
                          ByVal ItemPMUnit As Integer, _
                          ByVal ItemSQty As Decimal, _
                          ByVal ItemSMUnit As Integer) As Integer

        Dim SqlStr As String
        Dim SqlExRtrn As Long
        Dim MyWmsDBTrans As WmsDBTrans = New WmsDBTrans

        SqlStr = "DELETE FROM TWMSBINITEMSQTY  "
        SqlStr = SqlStr & " WHERE WHBINID=" & WhBinID.ToString

        If Len(PackItemNo) > 0 Then
            SqlStr = SqlStr & " AND  PACKITEMNO='" & PackItemNo & "'"
        End If
        If ItemID > 0 Then
            SqlStr = SqlStr & " AND  ITEMID=" & ItemID.ToString
        End If
        If ItemLength > 0 Then
            SqlStr = SqlStr & " AND ITEMLENGTH=" & f_dbinsertdecimal(ItemLength, 4)
        End If
        If ItemColorID > 0 Then
            SqlStr = SqlStr & " AND ITEMCOLORID=" & ItemColorID.ToString
        End If
        If ItemColorID2 > 0 Then
            SqlStr = SqlStr & " AND ITEMCOLORID2=" & ItemColorID2.ToString
        End If

        If ItemLotID > 0 Then
            SqlStr = SqlStr & " AND ITEMLOTID=" & ItemLotID.ToString
        End If

        SqlExRtrn = MyWmsDBTrans.OraDBExecuteSQLCmd(SqlStr, 1)

        If SqlExRtrn > 0 Then
            Return 1
        Else
            'DEBUG
            MyWmsDBTrans.f_sqlerrorlog(CompID, "WmsBinTrans.FDeleteBinItemQty>>", SqlStr, "Admin")
            Return -1
        End If

    End Function

    Function FDeletePaletteBinItemQty(ByVal PackitemID As Long) As Long
        Dim SqlStr As String
        Dim SqlExRtrn As Long
        Dim MyWmsDBTrans As WmsDBTrans = New WmsDBTrans

        SqlStr = "DELETE FROM TWMSBINITEMSQTY  "
        SqlStr = SqlStr & " WHERE PACKITEMID=" & PackitemID.ToString


        SqlExRtrn = MyWmsDBTrans.OraDBExecuteSQLCmd(SqlStr, 1)

        If SqlExRtrn > 0 Then
            Return 1
        Else
            'DEBUG
            MyWmsDBTrans.f_sqlerrorlog(1, "WmsBinTrans.FDeletePaletteBinItemQty>>", SqlStr, "Admin")
            Return -1
        End If

    End Function

    Function FUpdateIntenalTransferProdSource(ByVal ProdID As Long, ByVal packitemid As Long, ByVal itemid As Long, ByVal itemcolorid As Integer, ByVal itemlength As Decimal) As Integer

        Dim MyWmsDBTrans As WmsDBTrans = New WmsDBTrans

        Dim SqlStr As String
        Dim SqlExRtrn As Long

        If ProdID > 0 Then
            SqlStr = "UPDATE TPRODPACKAGING SET PRODID=" & ProdID.ToString() & " WHERE PACKDATE >= (SYSDATE -1) AND ITEMID=" & itemid.ToString() & " AND ITEMCOLORID=" + itemcolorid.ToString() & " AND ITEMLENGTH=" + itemlength.ToString().Replace(",", ".") & " AND PRODID IS NULL"

            SqlExRtrn = MyWmsDBTrans.OraDBExecuteSQLCmd(SqlStr, 1)
        Else
            SqlExRtrn = 0
        End If



        Return SqlExRtrn

    End Function


    Function FInventoryUpdateBinQty( _
                     ByVal CompID As Integer, _
                     ByVal BranchID As Integer, _
                     ByVal PackItemNo As String, _
                     ByVal WhBinID As Long, _
                     ByVal ItemID As Long, _
                     ByVal ItemLotID As Long, _
                     ByVal ItemLength As Decimal, _
                     ByVal ItemColorID As Integer, _
                     ByVal ItemColorID2 As Integer, _
                     ByVal ItemPQty As Decimal, _
                     ByVal ItemPMUnit As Integer, _
                     ByVal ItemSQty As Decimal, _
                     ByVal ItemSMUnit As Integer, _
                     ByVal ItemPackQty As Decimal, _
                     ByVal ItemPackMUnit As Integer, _
                     ByVal iAppUser As Integer, _
                     ByVal Terminal As String) As Integer

        Dim MyWmsDBTrans As New WmsDBTrans
        Dim ExPackItemQty As Decimal  'Existing qty
        Dim UpdRtRn As Long
        Dim DSqlstr As String
        Dim TransEx As Integer
        Dim ExSqlStr As String

        Dim ItemLengthStr As String

        Dim ThisItemSQty As Decimal
        Dim ExistPQtyStr As String
        Dim ExistPQty As Decimal
        Dim TransItemPQty As Decimal
        Dim TransItemSQty As Decimal
        Dim TransItemPAckQty As Decimal
        Dim WmsTransType As Integer

        Dim StoreTransID As Long
        Dim StoreTransDtlID As Long

        Dim WhBinIDFrom As Long
        Dim WhBinIDTo As Long

        Dim PackItemID As Long


        TransEx = 1

        If ItemSQty > 0 Then
            ThisItemSQty = ItemSQty
        Else
            ThisItemSQty = Me.FCalcItemWeightQty(ItemID, ItemLength, ItemPQty, CompID)
        End If
        'delete current content
        If Len(PackItemNo) > 0 Then
            ExSqlStr = "SELECT ITEMQTYPRIMARY FROM TWMSBINITEMSQTY WHERE WHBINID=" & WhBinID.ToString() & "  AND PACKITEMNO='" & PackItemNo & "' AND ITEMID=" & ItemID.ToString()
            DSqlstr = "DELETE FROM TWMSBINITEMSQTY WHERE WHBINID=" & WhBinID.ToString() & "  AND PACKITEMNO='" & PackItemNo & "' AND ITEMID=" & ItemID.ToString()
        Else
            ExSqlStr = "SELECT ITEMQTYPRIMARY FROM TWMSBINITEMSQTY WHERE WHBINID=" & WhBinID.ToString() & " AND ITEMID=" & ItemID.ToString()
            DSqlstr = "DELETE FROM TWMSBINITEMSQTY WHERE WHBINID=" & WhBinID.ToString() & " AND ITEMID=" & ItemID.ToString()
        End If

        If ItemLength > 0 Then
            ItemLengthStr = Me.f_dbinsertdecimal(ItemLength, 4)
            ExSqlStr = ExSqlStr & " AND ITEMLENGTH=" & ItemLengthStr
            DSqlstr = DSqlstr & " AND ITEMLENGTH=" & ItemLengthStr
        End If
        If ItemColorID > 0 Then
            ExSqlStr = ExSqlStr & " AND ITEMCOLORID=" & ItemColorID.ToString
            DSqlstr = DSqlstr & " AND ITEMCOLORID=" & ItemColorID.ToString
        End If
        If ItemColorID2 > 0 Then
            ExSqlStr = ExSqlStr & " AND ITEMCOLORID2=" & ItemColorID2.ToString
            DSqlstr = DSqlstr & " AND ITEMCOLORID2=" & ItemColorID2.ToString
        End If
        '=============================
        'Get existing Qty
        'Create Inventory Trans if needit
        ExistPQtyStr = MyWmsDBTrans.OraDBWmsExSelectCmdRN2String(ExSqlStr)

        If Len(ExistPQtyStr) > 0 And ExistPQtyStr <> "Null" Then
            ExistPQty = Decimal.Parse(ExistPQtyStr)

            PackItemID = Me.FGetPackItemIDByNo(PackItemNo)

            If ExistPQty > ItemPQty Then
                TransEx = -1
                TransItemPQty = ExistPQty - ItemPQty
                TransItemSQty = FCalcItemWeightQty(ItemID, ItemLength, TransItemPQty, CompID)


                FCreateWmsTrans(CompID, BranchID, WmsTransType, WhBinID, WhBinIDFrom, WhBinIDTo, ItemID, ItemLotID, TransItemPQty, ItemPMUnit, _
                TransItemSQty, ItemSMUnit, TransItemPAckQty, ItemPackMUnit, TransEx, StoreTransID, StoreTransDtlID, iAppUser, Terminal, ItemLength, ItemColorID, ItemColorID2, PackItemID, PackItemNo, 0, "")


            ElseIf ExistPQty < ItemPQty Then
                TransEx = 1
                TransItemPQty = ItemPQty - ExistPQty
                TransItemSQty = FCalcItemWeightQty(ItemID, ItemLength, TransItemPQty, CompID)

                FCreateWmsTrans(CompID, BranchID, WmsTransType, WhBinID, WhBinIDFrom, WhBinIDTo, ItemID, ItemLotID, TransItemPQty, ItemPMUnit, _
                TransItemSQty, ItemSMUnit, TransItemPAckQty, ItemPackMUnit, TransEx, StoreTransID, StoreTransDtlID, iAppUser, Terminal, ItemLength, ItemColorID, ItemColorID2, PackItemID, PackItemNo, 0, "")

            End If
        End If
        ' End Wms Inventory Trans
        '=========================
        MyWmsDBTrans.OraDBExecuteSQLCmd(DSqlstr, 1)

        UpdRtRn = FInsertBinItemQty(CompID, BranchID, TransEx, PackItemNo, WhBinID, ItemID, ItemLotID, ItemLength, ItemColorID, ItemColorID2, ItemPQty, ItemPMUnit, ThisItemSQty, ItemSMUnit, 0, 0, 0, iAppUser, Terminal)

        If UpdRtRn > 0 Then
            Return 1
        Else
            'DEBUG
            MyWmsDBTrans.f_sqlerrorlog(CompID, "WmsBinTrans.FUpdateBinQty>>", "Nothing updated!", "Admin")
            Return -1
        End If
    End Function

#End Region

#Region "StoreTrans"

    Public Function FGetStoreTransTypeParams(ByVal CompID As Integer, _
                                             ByVal StoreTransType As Integer, _
                                             ByRef StoreIDFrom As Integer, _
                                             ByRef StoreIDTo As Integer, _
                                             ByRef StoreIDDefault As Integer, _
                                             ByRef TransEx As Integer, _
                                             ByRef ApprReq As Integer, _
                                             ByRef WMSTask As Integer) As Long

        Dim MyWmsDbTrans As New WmsDBTrans
        Dim Ds As New DataSet
        Dim SqlStr As String

        SqlStr = "SELECT StoreIDFrom,StoreIDTo,StoreIDDefault,TRANSEX,ApprovalReq,WMSAUTOTASK "
        SqlStr = SqlStr & "FROM TSTORETRANSTYPES WHERE STORETRANSTYPE=" & StoreTransType

        Ds = MyWmsDbTrans.OraDBFillDataset(SqlStr, "STORETRANSTYPEPARAMS")

        If Ds.Tables(0).Rows.Count > 0 Then '//always  1 row, 0 in particular
            If IsDBNull(Ds.Tables(0).Rows(0)("StoreIDFrom")) = False Then
                StoreIDFrom = Integer.Parse(Ds.Tables(0).Rows(0)("StoreIDFrom").ToString)
            End If
            If IsDBNull(Ds.Tables(0).Rows(0)("StoreIDTo")) = False Then
                StoreIDTo = Integer.Parse(Ds.Tables(0).Rows(0)("StoreIDTo").ToString)
            End If
            If IsDBNull(Ds.Tables(0).Rows(0)("StoreIDDefault")) = False Then
                StoreIDDefault = Integer.Parse(Ds.Tables(0).Rows(0)("StoreIDDefault").ToString)
            End If
            If IsDBNull(Ds.Tables(0).Rows(0)("ApprovalReq")) = False Then
                ApprReq = Integer.Parse(Ds.Tables(0).Rows(0)("ApprovalReq").ToString)
            End If
            If IsDBNull(Ds.Tables(0).Rows(0)("WMSAUTOTASK")) = False Then
                WMSTask = Integer.Parse(Ds.Tables(0).Rows(0)("WMSAUTOTASK").ToString)
            End If
            If IsDBNull(Ds.Tables(0).Rows(0)("TRANSEX")) = False Then
                TransEx = Integer.Parse(Ds.Tables(0).Rows(0)("TRANSEX").ToString)
            End If
        End If
        ' first timetrying By reference, let's see
        Return 0

    End Function
    Public Function FGetProdOrderPhase(ByVal ProdOrderID As Long) As Long
        Dim ProdPhaseid As Long
        Dim ProdPhaseidstr, sqlstr As String

        Dim MyWmsDbTrans As New WmsDBTrans

        sqlstr = "SELECT ProdPhaseID  FROM TProdOrders WHERE ProdOrderID=" & ProdOrderID.ToString

        ProdPhaseidstr = MyWmsDbTrans.OraDBWmsExSelectCmdRN2String(sqlstr)

        If Len(ProdPhaseidstr) > 0 And (ProdPhaseidstr <> "NULL" Or ProdPhaseidstr <> "-1") Or ProdPhaseidstr <> "Null" Then
            ProdPhaseid = Long.Parse(ProdPhaseidstr)
            Return ProdPhaseid
        Else
            Return -1
        End If


    End Function
    Public Function FCreateNewStoreTransCode(ByVal CompID As Integer, ByVal StoreTransType As Integer) As String
        Dim transtypecode, newtranscode, counttransstr As String

        Dim MyWmsDBTrans As New WmsDBTrans

        Dim counttrans As Long

        Dim sqlstr As String

        Dim MyDBDate As String

        sqlstr = "SELECT storetranstypecode FROM tstoretranstypes WHERE storetranstype=" & StoreTransType.ToString
        transtypecode = MyWmsDBTrans.OraDBWmsExSelectCmdRStr2Str(sqlstr)

        MyDBDate = MyWmsDBTrans.FGetDBDate()

        If IsDate(MyDBDate) = False Then
            MyDBDate = Now().ToShortDateString
        End If
        If Len(transtypecode) = 0 Or IsDBNull(transtypecode) Then Return "-1"

        sqlstr = "SELECT NVL(COUNT(*),0) + 1  FROM TSTORETRANS WHERE storetranstype =" & StoreTransType.ToString

        counttransstr = MyWmsDBTrans.OraDBWmsExSelectCmdRN2String(sqlstr)
        If counttransstr <> "-1" Or counttransstr <> "NULL" Then
            counttrans = Long.Parse(counttransstr)
        Else
            Return "-1"
        End If

        newtranscode = transtypecode & "-" & Right((Year(Date.Parse(MyDBDate)).ToString), 2) & "-" + FMyFill("0", 4 - Len(counttrans.ToString)) & counttrans.ToString

        Return newtranscode

    End Function
    Public Function FCheckIfExistsDailyStoreTrans(ByVal CompID As Integer, ByVal BranchID As Integer, ByVal StoreTRanstype As Integer) As Long
        Dim MyWmsDBTrans As New WmsDBTrans
        Dim StoreTransID As Long
        Dim SqlStr, RtrnStr As String
        Dim CntDatestr As String

        CntDatestr = MyWmsDBTrans.FGetDBDate()

        SqlStr = "SELECT STORETRANSID    FROM TSTORETRANS WHERE STORETRANSTYPE =" & StoreTRanstype.ToString
        SqlStr = SqlStr & " AND TO_CHAR(STORETRANSDATE,'DD/MM/YYYY')='" & CntDatestr & "'"
        If CompID > 0 Then
            SqlStr = SqlStr & " AND COMPID=" & CompID.ToString
            If BranchID > 0 Then
                SqlStr = SqlStr & " AND BRANCHID=" & BranchID.ToString
            End If
        End If

        RtrnStr = MyWmsDBTrans.OraDBWmsExSelectCmdRN2String(SqlStr)

        If IsNumeric(RtrnStr) Then
            StoreTransID = Long.Parse(RtrnStr)
        Else
            StoreTransID = -1
        End If
        Return StoreTransID
    End Function
    Public Function FCreateProdStoreTransColors(ByVal CompID As Integer, _
                                                ByVal BranchID As Integer, _
                                                ByVal CustomerID As Long, _
                                                ByVal SalesOrderID As Long, _
                                                ByVal ItemID As Long, _
                                                ByVal ItemLotID As Long, _
                                                ByVal ItemLength As Decimal, _
                                                ByVal ItemColorID As Long, _
                                                ByVal ItemColorID2 As Long, _
                                                ByVal ItemPQty As Decimal, _
                                                ByVal ITemPMUnit As Integer, _
                                                ByVal ItemSqty As Decimal, _
                                                ByVal ITemSMUnit As Integer, _
                                                ByVal AccERPItemID As Long) As Long

        Dim StoreTransType As Integer
        Dim SqlStr, RtrnStr As String

        Dim StoreIDFrom, StoreIDTo, StoreIDDefault, transex, AppReq, WMSTask As Integer
        Dim StoreTransID As Long
        Dim MyWmsDBTrans As New WmsDBTrans

        Dim rtrnDtl As Long


        SqlStr = "SELECT PALETTETRANSREADY FROM TSTOREPARAMS WHERE COMPID=" & CompID.ToString
        RtrnStr = MyWmsDBTrans.OraDBWmsExSelectCmdRN2String(SqlStr)
        If IsNumeric(RtrnStr) Then
            StoreTransType = Long.Parse(RtrnStr)
            If StoreTransType > 0 Then
                FGetStoreTransTypeParams(CompID, StoreTransType, StoreIDFrom, StoreIDTo, StoreIDDefault, transex, AppReq, WMSTask)
            Else
                Return 0
            End If
        End If

        'check first if exist toretrans in same day
        StoreTransID = FCheckIfExistsDailyStoreTrans(CompID, BranchID, StoreTransType)

        If IsNothing(StoreTransID) Or StoreTransID < 1 Then
            StoreTransID = FCreateStoreTransHeader(CompID, BranchID, StoreTransType, 0, CustomerID, 0, SalesOrderID, 0, 0)
        End If


        If StoreTransID > 0 Then
            rtrnDtl = FCreateStoreTransDetail(CompID, StoreTransType, StoreTransID, transex, ItemID, 0, ItemLotID, ItemColorID, ItemColorID2, ItemPQty, 0, ItemSqty, 0, AccERPItemID)
            If rtrnDtl > 0 Then
                Return rtrnDtl
            Else
                Return -1
            End If
        Else
            Return -2
        End If



    End Function
    Public Function FCreateStoreTransHeader(ByVal CompID As Integer, _
                                            ByVal BranchID As Integer, _
                                            ByVal StoreTransType As Integer, _
                                            ByVal ProdOrderID As Long, _
                                            ByVal CustomerID As Long, _
                                            ByVal SupplierID As Long, _
                                            ByVal SalesOrderID As Long, _
                                            ByVal PurchOrderID As Long, _
                                            ByVal ShippingID As Long) As Long
        Dim MyWmsDbTrans As New WmsDBTrans

        Dim StoreTransID As Long
        Dim StApproval, WmsTask As Integer

        Dim ProdPhaseID
        Dim StoreIDFrom, StoreIDTo, StoreIDDefault As Long

        Dim StoreTransCode As String

        Dim SqlStr As String
        Dim SqlInsRows As Long
        Dim transex As Integer
        '///// Set Values

        StoreTransID = FGetNewStoreTransID()

        'Get ProdPhase
        If ProdOrderID > 0 Then
            ProdPhaseID = FGetProdOrderPhase(ProdOrderID)
        End If
        StoreTransCode = FCreateNewStoreTransCode(CompID, StoreTransType)

        FGetStoreTransTypeParams(CompID, StoreTransType, StoreIDFrom, StoreIDTo, StoreIDDefault, transex, StApproval, WmsTask)


        SqlStr = "INSERT INTO TSTORETRANS(STORETRANSID,COMPID,BRANCHID,STORETRANSTYPE,STORETRANSCODE,STORETRANSDATE,"
        SqlStr = SqlStr & "STOREIDDEFAULT,STOREIDFROM,STOREIDTO,"
        SqlStr = SqlStr & "PRODORDERID,CUSTOMERID,SUPPLIERID,SALESORDERID,PURCHASEORDERID,SHIPPINGID,"
        SqlStr = SqlStr & "TRANSAPPROVED,WMSTASK) VALUES ("

        If StoreTransID > 0 Then
            SqlStr = SqlStr & StoreTransID.ToString & ","
        Else
            SqlStr = SqlStr & "NULL,"
        End If

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
        If StoreTransType > 0 Then
            SqlStr = SqlStr & StoreTransType.ToString & ","
        Else
            SqlStr = SqlStr & "NULL,"
        End If
        If Len(StoreTransCode) > 0 Then
            SqlStr = SqlStr & "'" & StoreTransCode & "',"
        Else
            SqlStr = SqlStr & "NULL,"
        End If
        'current date
        SqlStr = SqlStr & "TO_CHAR(SYSDATE,'DD/MM/YYYY'),"
        If StoreIDDefault > 0 Then
            SqlStr = SqlStr & StoreIDDefault.ToString & ","
        Else
            SqlStr = SqlStr & "NULL,"
        End If
        If StoreIDFrom > 0 Then
            SqlStr = SqlStr & StoreIDFrom.ToString & ","
        Else
            SqlStr = SqlStr & "NULL,"
        End If
        If StoreIDTo > 0 Then
            SqlStr = SqlStr & StoreIDTo.ToString & ","
        Else
            SqlStr = SqlStr & "NULL,"
        End If
        If ProdOrderID > 0 Then
            SqlStr = SqlStr & ProdOrderID.ToString & ","
        Else
            SqlStr = SqlStr & "NULL,"
        End If
        If CustomerID > 0 Then
            SqlStr = SqlStr & CustomerID.ToString & ","
        Else
            SqlStr = SqlStr & "NULL,"
        End If
        If SupplierID > 0 Then
            SqlStr = SqlStr & SupplierID.ToString & ","
        Else
            SqlStr = SqlStr & "NULL,"
        End If
        If SalesOrderID > 0 Then
            SqlStr = SqlStr & SalesOrderID.ToString & ","
        Else
            SqlStr = SqlStr & "NULL,"
        End If
        If PurchOrderID > 0 Then
            SqlStr = SqlStr & PurchOrderID.ToString & ","
        Else
            SqlStr = SqlStr & "NULL,"
        End If

        If ShippingID > 0 Then
            SqlStr = SqlStr & ShippingID.ToString & ","
        Else
            SqlStr = SqlStr & "NULL,"
        End If

        If StApproval = 1 Or StApproval = 0 Then
            SqlStr = SqlStr & StApproval & ","
        Else
            SqlStr = SqlStr & "NULL,"
        End If
        'last field !!!!
        If WmsTask = 1 Or WmsTask = 0 Then
            SqlStr = SqlStr & WmsTask
        Else
            SqlStr = SqlStr & "NULL"
        End If
        SqlStr = SqlStr & ")"


        SqlInsRows = MyWmsDbTrans.OraDBExecuteSQLCmd(SqlStr, 1)

        If SqlInsRows > 0 Then
            Return StoreTransID
        Else
            MyWmsDbTrans.f_sqlerrorlog(CompID, "WmsBinTRans.FCreateStoreTransHeader", SqlStr, "Admin")
            Return -1
        End If
    End Function

    Public Function FCreateStoreTransDetail(ByVal CompID As Integer, _
                                            ByVal StoreTransType As Integer, _
                                            ByVal StoreTransID As Long, _
                                            ByVal TransEx As Integer, _
                                            ByVal ItemID As Long, _
                                            ByVal ItemLotID As Long, _
                                            ByVal ItemLength As Decimal, _
                                            ByVal ItemColorID As Long, _
                                            ByVal ItemColorID2 As Long, _
                                            ByVal ItemPQty As Decimal, _
                                            ByVal ITemPMUnit As Integer, _
                                            ByVal ItemSqty As Decimal, _
                                            ByVal ITemSMUnit As Integer, _
                                            ByVal AccERPItemID As Long) As Long

        Dim MyWmsDBTrans As New WmsDBTrans
        Dim StoreTransDtlID As Long
        Dim SqlStr As String

        Dim SqlInsRows As Long

        StoreTransDtlID = FGetNewStoreTransDetailsID()


        SqlStr = "INSERT INTO TSTORETRANSDETAILS(COMPID,STORETRANSID,STORETRANSDTLID,TRANSEX,TRANSINSTATUS,"
        SqlStr = SqlStr & "ITEMID,ITEMLOTID,ITEMLENGTH,ITEMCOLORID,ITEMCOLORID2,"
        SqlStr = SqlStr & "ITEMQTYPRIMARY,MUNITPRIMARY,ITEMQTYSECONDARY,MUNITSECONDARY,"
        SqlStr = SqlStr & "ACCERPITEMID) VALUES("

        If CompID > 0 Then
            SqlStr = SqlStr & CompID.ToString & ","
        Else
            SqlStr = SqlStr & "NULL,"
        End If
        If StoreTransID > 0 Then
            SqlStr = SqlStr & StoreTransID.ToString & ","
        Else
            SqlStr = SqlStr & "NULL,"
        End If
        If StoreTransDtlID > 0 Then
            SqlStr = SqlStr & StoreTransDtlID.ToString & ","
        Else
            SqlStr = SqlStr & "NULL,"
        End If
        If TransEx = 0 Or TransEx = 1 Or TransEx = -1 Then
            SqlStr = SqlStr & TransEx.ToString & ","
        Else
            SqlStr = SqlStr & "NULL,"
        End If
        'transinsstaus set default to 1
        SqlStr = SqlStr & "1,"
        If ItemID > 0 Then
            SqlStr = SqlStr & ItemID.ToString & ","
        Else
            SqlStr = SqlStr & "NULL,"
        End If
        If ItemLotID > 0 Then
            SqlStr = SqlStr & ItemLotID.ToString & ","
        Else
            SqlStr = SqlStr & "NULL,"
        End If
        If ItemLength > 0 Then
            SqlStr = SqlStr & f_dbinsertdecimal(ItemLength, 3) & ","
        Else
            SqlStr = SqlStr & "NULL,"
        End If
        If ItemColorID > 0 Then
            SqlStr = SqlStr & ItemColorID.ToString & ","
        Else
            SqlStr = SqlStr & "NULL,"
        End If
        If ItemColorID2 > 0 Then
            SqlStr = SqlStr & ItemColorID2.ToString & ","
        Else
            SqlStr = SqlStr & "NULL,"
        End If
        If ItemPQty > 0 Then
            SqlStr = SqlStr & f_dbinsertdecimal(ItemPQty, FGetItemMUnitDecimal(ITemPMUnit)) & ","
        Else
            SqlStr = SqlStr & "NULL,"
        End If
        'ITemPMUnit
        If ITemPMUnit > 0 Then
            SqlStr = SqlStr & ITemPMUnit.ToString & ","
        Else
            SqlStr = SqlStr & "NULL,"
        End If
        If ItemSqty > 0 Then
            SqlStr = SqlStr & f_dbinsertdecimal(ItemSqty, FGetItemMUnitDecimal(ITemSMUnit)) & ","
        Else
            SqlStr = SqlStr & "NULL,"
        End If
        If ITemSMUnit > 0 Then
            SqlStr = SqlStr & ITemSMUnit.ToString & ","
        Else
            SqlStr = SqlStr & "NULL,"
        End If
        'AccERPItemID !!!last
        If AccERPItemID > 0 Then
            SqlStr = SqlStr & AccERPItemID.ToString
        Else
            SqlStr = SqlStr & "NULL"
        End If
        SqlStr = SqlStr & ")"


        SqlInsRows = MyWmsDBTrans.OraDBExecuteSQLCmd(SqlStr, 1)

        If SqlInsRows > 0 Then
            Return StoreTransDtlID
        Else
            MyWmsDBTrans.f_sqlerrorlog(CompID, "WmsBinTRans.FCreateStoreTransDetail", SqlStr, "Admin")
            Return -1
        End If

    End Function

#End Region


#Region " Orders Picking "
    Public Function FGetOrdersItemBinLocations(ByVal CompID As Integer, ByVal BranchID As Integer, ByVal ItemID As Long, ByVal ItemLength As Decimal, ByVal ItemColorID As Integer, ByVal ItemColorID2 As Integer)


    End Function

    Public Function FCreateNewPickingCard( _
                        ByVal CompID As Integer, _
                        ByVal BranchID As Integer, _
                        ByVal CardID As Long, _
                        ByVal WhBinID As Long, _
                        ByVal WhBinCode As String, _
                        ByVal WhZoneID As Long, _
                        ByVal WhZoneCode As String, _
                        ByVal StoreTransID As Long, _
                        ByVal OrderID As Long, _
                        ByVal OrderDtlID As Long, _
                        ByVal ItemID As Long, _
                        ByVal ItemLength As Decimal, _
                        ByVal ItemColorID As Integer, _
                        ByVal ItemColorID2 As Integer, _
                        ByVal ItemQtyPrimary As Decimal, _
                        ByVal MUnitPrimary As Integer, _
                        ByVal ItemQtySecondary As Decimal, _
                        ByVal MUnitSecondary As Integer, _
                        ByVal DbCreateUser As Integer, _
                        ByVal Terminal As String) As Long

        Dim MyWmsDBTrans As WmsDBTrans = New WmsDBTrans
        Dim SqlExRtrn As Long
        Dim SqlStr As String
        Dim ThisWhZoneID As Long
        Dim ThisWhZoneCode As String

        Dim ThisWhBinID As Long
        Dim ThisWhBinCode As String

        Dim ThisPQtyStr As String
        Dim CardRowID As Long

        If WhZoneID > 0 And Len(WhZoneCode) = 0 Then
            ThisWhZoneCode = FGetZoneCodeByID(CompID, BranchID, WhZoneID)
        ElseIf Len(WhZoneCode) > 0 And (WhZoneID = Nothing Or WhZoneID = 0) Then
            ThisWhZoneID = FGetZoneIDByCode(CompID, BranchID, WhZoneCode)
        Else
            ThisWhZoneCode = WhZoneCode
            ThisWhZoneID = WhZoneID
        End If

        If WhBinID > 0 And Len(WhBinCode) = 0 Then
            ThisWhBinCode = FGetBinCodeByID(CompID, BranchID, WhBinID)
        ElseIf Len(WhBinCode) > 0 And (WhBinID = Nothing Or WhBinID = 0) Then
            ThisWhBinID = FGetBinIDByCode(CompID, BranchID, WhBinCode)
        Else
            ThisWhBinCode = WhBinCode
            ThisWhBinID = WhBinID
        End If

        CardRowID = FCreateNewCardRowID()



        SqlStr = "INSERT INTO  TWMSPICKINGCARD(CARDROWID,CARDID,COMPID,BRANCHID, WHBINID,WHBINCODE,WHZONEID,WHZONECODE,STORETRANSID,ORDERID,ORDERDTLID,ITEMID,"
        SqlStr = SqlStr & "ITEMLENGTH,ITEMCOLORID,ITEMCOLORID2,ITEMPRIMARYQTY,ITEMBALANCEQTY,ITEMMUNITPRIMARY,ITEMSECONDARYQTY,ITEMMUNITSECONDARY, DBRECCREATEUSER,TERMINAL) "
        SqlStr = SqlStr & "VALUES ("

        If CardRowID > 0 Then
            SqlStr = SqlStr & CardRowID.ToString & ","
        Else
            SqlStr = SqlStr & "NULL,"
        End If
        If CardID > 0 Then
            SqlStr = SqlStr & CardID.ToString & ","
        Else
            SqlStr = SqlStr & "NULL,"
        End If
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
        If ThisWhBinID > 0 Then
            SqlStr = SqlStr & ThisWhBinID.ToString & ","
        Else
            SqlStr = SqlStr & "NULL,"
        End If
        If Len(ThisWhBinCode) > 0 Then
            SqlStr = SqlStr & "'" & ThisWhBinCode & "',"
        Else
            SqlStr = SqlStr & "NULL,"
        End If
        If ThisWhZoneID > 0 Then
            SqlStr = SqlStr & ThisWhZoneID.ToString & ","
        Else
            SqlStr = SqlStr & "NULL,"
        End If
        If Len(ThisWhZoneCode) > 0 Then
            SqlStr = SqlStr & "'" & ThisWhZoneCode & "',"
        Else
            SqlStr = SqlStr & "NULL,"
        End If
        If StoreTransID > 0 Then
            SqlStr = SqlStr & StoreTransID.ToString & ","
        Else
            SqlStr = SqlStr & "NULL,"
        End If
        If OrderID > 0 Then
            SqlStr = SqlStr & OrderID.ToString & ","
        Else
            SqlStr = SqlStr & "NULL,"
        End If
        If OrderDtlID > 0 Then
            SqlStr = SqlStr & OrderDtlID.ToString & ","
        Else
            SqlStr = SqlStr & "NULL,"
        End If
        If ItemID > 0 Then
            SqlStr = SqlStr & ItemID.ToString & ","
        Else
            SqlStr = SqlStr & "NULL,"
        End If
        If ItemLength > 0 Then
            SqlStr = SqlStr & f_dbinsertdecimal(ItemLength, 3) & ","
        Else
            SqlStr = SqlStr & "NULL,"
        End If
        If ItemColorID > 0 Then
            SqlStr = SqlStr & ItemColorID.ToString & ","
        Else
            SqlStr = SqlStr & "NULL,"
        End If

        If ItemColorID2 > 0 Then
            SqlStr = SqlStr & ItemColorID2.ToString & ","
        Else
            SqlStr = SqlStr & "NULL,"
        End If
        'ITEMPRIM QTY , ITEMBALANCEQTY
        If ItemQtyPrimary > 0 Then
            ThisPQtyStr = f_dbinsertdecimal(ItemQtyPrimary, FGetItemMUnitDecimal(MUnitPrimary))
            SqlStr = SqlStr & ThisPQtyStr & ","
            SqlStr = SqlStr & ThisPQtyStr & ","
        Else
            SqlStr = SqlStr & "NULL,NULL,"
        End If
        'FGetItemMUnitPrimary
        If MUnitPrimary > 0 Then
            SqlStr = SqlStr & MUnitPrimary.ToString & ","
        Else
            SqlStr = SqlStr & "NULL,"
        End If

        If ItemQtySecondary > 0 Then
            SqlStr = SqlStr & f_dbinsertdecimal(ItemQtySecondary, FGetItemMUnitDecimal(MUnitSecondary)) & ","
        Else
            SqlStr = SqlStr & "NULL,"
        End If

        If MUnitSecondary > 0 Then
            SqlStr = SqlStr & MUnitSecondary.ToString & ","
        Else
            SqlStr = SqlStr & "NULL,"
        End If
        If DbCreateUser > 0 Then
            SqlStr = SqlStr & DbCreateUser.ToString & ","
        Else
            SqlStr = SqlStr & "NULL,"
        End If
        If Len(Terminal) > 0 Then
            SqlStr = SqlStr & "'" & Terminal & "'"
        Else
            SqlStr = SqlStr & "NULL"
        End If
        SqlStr = SqlStr & ")"


        SqlExRtrn = MyWmsDBTrans.OraDBExecuteSQLCmd(SqlStr, 1)

        If SqlExRtrn > 0 Then
            Return CardID
        Else
            'DEBUG
            MyWmsDBTrans.f_sqlerrorlog(CompID, "WmsBinTrans.FCreateNewPickingCard>>", SqlStr, "Admin")
            Return -1
        End If
    End Function
#End Region ' Orders Picking

    Public Function FGetZoneswithMaxPickload(ByVal CompID As Integer, ByVal BranchID As Integer, ByVal WhBuildingID As Integer) As DataSet
        Dim MyWmsDBTrans As New WmsDBTrans

        Dim SessionID As Long
        Dim SqlStr As String

        Dim Ds As New DataSet

        SessionID = FCreateNewSessionID()

        If WhBuildingID > 0 Then
            SqlStr = "INSERT INTO TWMSZONESWITHMAXPICKINGLOAD(SESSIONID,COMPID,BRANCHID,WHBUILDID,WHCODE,WHZONECODE,ITEMSCNT) "
            SqlStr = SqlStr & "SELECT " & SessionID.ToString & ",COMPID,BRANCHID,WHBUILDID,WHCODE,WHZONECODE,ITEMSCNT FROM VWMSZONESWITHMAXPICKINGLOAD "
            SqlStr = SqlStr & " WHERE  WHBUILDID=" & WhBuildingID.ToString
            If CompID > 0 Then
                SqlStr = SqlStr & " AND  COMPID=" & CompID.ToString
                If BranchID > 0 Then
                    SqlStr = SqlStr & " AND BRANCHID=" & BranchID.ToString
                End If
            End If
            SqlStr = SqlStr & " ORDER BY ITEMSCNT DESC "
            '
            MyWmsDBTrans.OraDBExecuteSQLCmd(SqlStr, 1)
            '
            SqlStr = "INSERT INTO TWMSZONESWITHMAXPICKINGLOAD(SESSIONID,COMPID,BRANCHID,WHBUILDID,WHCODE,WHZONECODE,ITEMSCNT) "
            SqlStr = SqlStr & "SELECT " & SessionID.ToString & ",COMPID,BRANCHID, WHBUILDID,WHCODE,WHZONECODE,ITEMSCNT FROM VWMSZONESWITHMAXPICKINGLOAD "
            SqlStr = SqlStr & " WHERE  WHBUILDID <> " & WhBuildingID.ToString
            If CompID > 0 Then
                SqlStr = SqlStr & " AND  COMPID=" & CompID.ToString
                If BranchID > 0 Then
                    SqlStr = SqlStr & " AND BRANCHID=" & BranchID.ToString
                End If
            End If
            SqlStr = SqlStr & " ORDER BY ITEMSCNT DESC "
            MyWmsDBTrans.OraDBExecuteSQLCmd(SqlStr, 1)
        Else
            SqlStr = "INSERT INTO TWMSZONESWITHMAXPICKINGLOAD(SESSIONID,COMPID,BRANCHID,WHBUILDID,WHCODE,WHZONECODE,ITEMSCNT) "
            SqlStr = SqlStr & "SELECT " & SessionID.ToString & ",COMPID,BRANCHID, WHBUILDID,WHCODE,WHZONECODE,ITEMSCNT FROM VWMSZONESWITHMAXPICKINGLOAD "

            If CompID > 0 Then
                SqlStr = SqlStr & " WHERE  COMPID=" & CompID.ToString
                If BranchID > 0 Then
                    SqlStr = SqlStr & " AND BRANCHID=" & BranchID.ToString
                End If
            End If
            SqlStr = SqlStr & " ORDER BY ITEMSCNT DESC "
            MyWmsDBTrans.OraDBExecuteSQLCmd(SqlStr, 1)
        End If

        SqlStr = "SELECT * FROM TWMSZONESWITHMAXPICKINGLOAD WHERE SESSIONID=" & SessionID.ToString
        SqlStr = SqlStr & "ORDER BY ROWID"
        Ds = MyWmsDBTrans.OraDBFillDataset(SqlStr, "ZONESWITHMAXLOAD")

        MyWmsDBTrans.OraDBExecuteSQLCmd("DELETE FROM TWMSZONESWITHMAXPICKINGLOAD WHERE SESSIONID=" & SessionID.ToString, 1)

        Return Ds
    End Function

    Public Function FGetOrderItemsInZones(ByVal CompID As Integer, ByVal BranchID As Integer, ByVal WhBuildingID As Integer) As DataSet
        Dim MyWmsDBTrans As New WmsDBTrans

        Dim SessionID As Long
        Dim SqlStr As String

        Dim Ds As New DataSet

        SessionID = FCreateNewSessionID()

        If WhBuildingID > 0 Then
            SqlStr = "INSERT INTO TWMSTASKSITEMBINLOCATIONS(SESSIONID,COMPID,BRANCHID,ITEMID,ITEMLENGTH,ITEMCOLORID,ITEMCOLORID2,"
            SqlStr = SqlStr & "ITEMCODE,ITEMDESC,"
            SqlStr = SqlStr & "WHBUILDID,WHCODE,WHZONECODE,"
            SqlStr = SqlStr & "ORDERITEMQTYBALANCE,BINITEMQTYPRIMARY) "
            SqlStr = SqlStr & "SELECT " & SessionID.ToString & ",COMPID,BRANCHID, ITEMID, ITEMLENGTH, ITEMCOLORID, ITEMCOLORID2,"
            SqlStr = SqlStr & "ITEMCODE,ITEMDESC,WHBUILDID,WHCODE,WHZONECODE,"
            SqlStr = SqlStr & "SUM(ORDERITEMQTYBALANCE) AS ORDERITEMQTYBALANCE,"
            SqlStr = SqlStr & "SUM(BINITEMQTYPRIMARY) AS BINITEMQTYPRIMARY FROM VWMSTASKSITEMBINLOCATIONS "
            SqlStr = SqlStr & "WHERE VWMSTASKSITEMBINLOCATIONS.WHBINTYPE NOT IN (2,5,6)"
            SqlStr = SqlStr & " AND  VWMSTASKSITEMBINLOCATIONS.WHBUILDID=" & WhBuildingID.ToString
            If CompID > 0 Then
                SqlStr = SqlStr & " AND  COMPID=" & CompID.ToString
                If BranchID > 0 Then
                    SqlStr = SqlStr & " AND BRANCHID=" & BranchID.ToString
                End If
            End If
            SqlStr = SqlStr & " GROUP BY COMPID,BRANCHID,WHBUILDID,WHCODE,WHZONECODE,ITEMID,ITEMCODE,ITEMDESC,ITEMLENGTH,ITEMCOLORID,ITEMCOLORID2 "
            SqlStr = SqlStr & " ORDER BY COMPID,BRANCHID,WHBUILDID,WHCODE,WHZONECODE,ITEMID,ITEMLENGTH,ITEMCOLORID,ITEMCOLORID2"
            '
            MyWmsDBTrans.OraDBExecuteSQLCmd(SqlStr, 1)
            '
            SqlStr = "INSERT INTO TWMSTASKSITEMBINLOCATIONS(SESSIONID,COMPID,BRANCHID,ITEMID,ITEMLENGTH,ITEMCOLORID,ITEMCOLORID2,"
            SqlStr = SqlStr & "ITEMCODE,ITEMDESC,"
            SqlStr = SqlStr & "WHBUILDID,WHCODE,WHZONECODE,"
            SqlStr = SqlStr & "ORDERITEMQTYBALANCE,BINITEMQTYPRIMARY) "
            SqlStr = SqlStr & "SELECT " & SessionID.ToString & ",COMPID,BRANCHID, ITEMID, ITEMLENGTH, ITEMCOLORID, ITEMCOLORID2,"
            SqlStr = SqlStr & "ITEMCODE,ITEMDESC,WHBUILDID,WHCODE,WHZONECODE,"
            SqlStr = SqlStr & "SUM(ORDERITEMQTYBALANCE) AS ORDERITEMQTYBALANCE,"
            SqlStr = SqlStr & "SUM(BINITEMQTYPRIMARY) AS BINITEMQTYPRIMARY FROM VWMSTASKSITEMBINLOCATIONS "
            SqlStr = SqlStr & "WHERE VWMSTASKSITEMBINLOCATIONS.WHBINTYPE NOT IN (2,5,6)"
            SqlStr = SqlStr & " AND  VWMSTASKSITEMBINLOCATIONS.WHBUILDID <> " & WhBuildingID.ToString
            If CompID > 0 Then
                SqlStr = SqlStr & " AND  COMPID=" & CompID.ToString
                If BranchID > 0 Then
                    SqlStr = SqlStr & " AND BRANCHID=" & BranchID.ToString
                End If
            End If
            SqlStr = SqlStr & " GROUP BY COMPID,BRANCHID,WHBUILDID,WHCODE,WHZONECODE,ITEMID,ITEMCODE,ITEMDESC,ITEMLENGTH,ITEMCOLORID,ITEMCOLORID2 "
            SqlStr = SqlStr & " ORDER BY COMPID,BRANCHID,WHBUILDID,WHCODE,WHZONECODE,ITEMID,ITEMLENGTH,ITEMCOLORID,ITEMCOLORID2"
            '
            MyWmsDBTrans.OraDBExecuteSQLCmd(SqlStr, 1)
            '
        Else
            SqlStr = "INSERT INTO TWMSTASKSITEMBINLOCATIONS(SESSIONID,COMPID,BRANCHID,ITEMID,ITEMLENGTH,ITEMCOLORID,ITEMCOLORID2,"
            SqlStr = SqlStr & "ITEMCODE,ITEMDESC,"
            SqlStr = SqlStr & "WHBUILDID,WHCODE,WHZONECODE,"
            SqlStr = SqlStr & "ORDERITEMQTYBALANCE,BINITEMQTYPRIMARY) "
            SqlStr = SqlStr & "SELECT " & SessionID.ToString & ",COMPID,BRANCHID, ITEMID, ITEMLENGTH, ITEMCOLORID, ITEMCOLORID2,"
            SqlStr = SqlStr & "ITEMCODE,ITEMDESC,WHBUILDID,WHCODE,WHZONECODE,"
            SqlStr = SqlStr & "SUM(ORDERITEMQTYBALANCE) AS ORDERITEMQTYBALANCE,"
            SqlStr = SqlStr & "SUM(BINITEMQTYPRIMARY) AS BINITEMQTYPRIMARY FROM VWMSTASKSITEMBINLOCATIONS "
            SqlStr = SqlStr & "WHERE VWMSTASKSITEMBINLOCATIONS.WHBINTYPE NOT IN (2,5,6)"
            If CompID > 0 Then
                SqlStr = SqlStr & " AND  COMPID=" & CompID.ToString
                If BranchID > 0 Then
                    SqlStr = SqlStr & " AND BRANCHID=" & BranchID.ToString
                End If
            End If
            SqlStr = SqlStr & " GROUP BY COMPID,BRANCHID,WHBUILDID,WHCODE,WHZONECODE,ITEMID,ITEMCODE,ITEMDESC,ITEMLENGTH,ITEMCOLORID,ITEMCOLORID2 "
            SqlStr = SqlStr & " ORDER BY COMPID,BRANCHID,WHBUILDID,WHCODE,WHZONECODE,ITEMID,ITEMLENGTH,ITEMCOLORID,ITEMCOLORID2"
            '
            MyWmsDBTrans.OraDBExecuteSQLCmd(SqlStr, 1)
            '

        End If
        SqlStr = "SELECT WHBUILDID,WHZONECODE,ITEMID,ITEMCODE,ITEMDESC,"
        SqlStr = SqlStr & "ITEMLENGTH,ITEMCOLORID,ITEMCOLORID2,"
        SqlStr = SqlStr & "ORDERITEMQTYBALANCE,BINITEMQTYPRIMARY AS ZONEITEMQTYPRIMARY "
        SqlStr = SqlStr & "FROM TWMSTASKSITEMBINLOCATIONS WHERE SESSIONID=" & SessionID.ToString
        SqlStr = SqlStr & "ORDER BY ROWID"
        Ds = MyWmsDBTrans.OraDBFillDataset(SqlStr, "ORDERITEMSINZONESQTY")

        MyWmsDBTrans.OraDBExecuteSQLCmd("DELETE FROM TWMSTASKSITEMBINLOCATIONS WHERE SESSIONID=" & SessionID.ToString, 1)

        Return Ds
    End Function

#Region "Shipping"
    Public Function FCreatePaletteNo(ByVal CompID As Integer, ByVal BranchID As Integer, ByVal PaletteType As Integer) As String

        Dim PacknoStr As String

        Dim branchidstr As String

        Dim nextpackno As String

        Dim MyWmsDBtrans As New WmsDBTrans

        Dim ZerosStr As String

        nextpackno = Long.Parse(MyWmsDBtrans.OraDBWmsExSelectCmdRN2String("SELECT SEQ_PACKAGENO.NEXTVAL FROM DUAL"))
        'if isnull(branchid) then branchid = 1

        If PaletteType = 2 Then
            branchidstr = "00" + "S"
        Else
            branchidstr = "00" & BranchID.ToString
        End If

        ZerosStr = New String("0", 6 - Len(nextpackno))

        PacknoStr = branchidstr & FGetCurrentYear().ToString & ZerosStr & nextpackno

        Return PacknoStr

    End Function

    Public Function FCreateCustomerPalette(ByVal CompID As Integer, ByVal BranchID As Integer, ByVal PaletteNo As String, ByVal WhBinCode As String, ByVal CustomerID As Long, ByVal RouteID As Long, ByVal AccItemID As Long, ByVal ITemCatID As Long, ByVal RsrvStoreTransID As Long, ByVal OrderID As Long, ByVal ProdOrderID As Long, ByVal ProdOrStore As Integer) As Long


        Dim SqlStr As String
        Dim StoreID As Integer
        Dim PackItemID As Long

        Dim packdate As String


        Dim MyWmsDbTrans As New WmsDBTrans

        Dim Rtrn As Long

        Dim InWarehouse, srcitems


        If ProdOrStore = 2 Then
            srcitems = 1
        Else
            srcitems = 0
        End If

        If CustomerID > 0 Then
            InWarehouse = 2 ' ready for shipping
        Else
            InWarehouse = 1
        End If

        PackItemID = FGetNewPackItemID()
        StoreID = FGetDefaultStoreID()

        'packdate = MyWmsDbTrans.OraDBWmsExSelectCmdRStr2Str("SELECT TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY')) AS DT FROM DUAL")


        SqlStr = "INSERT INTO TPACKAGES (PACKITEMID,COMPID,BRANCHID,PACKDATE,CUSTOMERID,RESRVTORETRANSID,ORDERID,PRODORDERID,STOREID,MIXED, "
        SqlStr = SqlStr & " PACKITEMNO,BINCODE,INWAREHOUSE,ERPITEMID,MAINITEMCATID,ROUTEID,SRCITEMS) VALUES ("

        If PackItemID > 0 Then
            SqlStr = SqlStr & PackItemID.ToString & ","
        Else
            SqlStr = SqlStr & "NULL,"
        End If
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

        'SqlStr = SqlStr & packdate & ","
        SqlStr = SqlStr & "TO_DATE(TO_CHAR(SYSDATE,'DD/MM/YYYY')),"
        If CustomerID > 0 Then
            SqlStr = SqlStr & CustomerID.ToString & ","
        Else
            SqlStr = SqlStr & "NULL,"
        End If
        'RsrvStoreTransID
        If RsrvStoreTransID > 0 Then
            SqlStr = SqlStr & RsrvStoreTransID.ToString & ","
        Else
            SqlStr = SqlStr & "NULL,"
        End If
        If OrderID > 0 Then
            SqlStr = SqlStr & OrderID.ToString & ","
        Else
            SqlStr = SqlStr & "NULL,"
        End If
        If ProdOrderID > 0 Then
            SqlStr = SqlStr & ProdOrderID.ToString & ","
        Else
            SqlStr = SqlStr & "NULL,"
        End If
        If StoreID > 0 Then
            SqlStr = SqlStr & StoreID.ToString & ","
        Else
            SqlStr = SqlStr & "NULL,"
        End If
        'mixed 1
        SqlStr = SqlStr & "1,"
        If Len(PaletteNo) > 0 Then
            SqlStr = SqlStr & "'" & PaletteNo & "',"
        Else
            SqlStr = SqlStr & "NULL,"
        End If
        'WhBinCode
        If Len(WhBinCode) > 0 Then
            SqlStr = SqlStr & "'" & WhBinCode & "',"
        Else
            SqlStr = SqlStr & "NULL,"
        End If
        'INWAREHOUSE, init 0 
        SqlStr = SqlStr & "0,"
        'AccItemID
        If AccItemID > 0 Then
            SqlStr = SqlStr & AccItemID.ToString & ","
        Else
            SqlStr = SqlStr & "NULL,"
        End If
        'ITemCatID
        If ITemCatID > 0 Then
            SqlStr = SqlStr & ITemCatID.ToString & ","
        Else
            SqlStr = SqlStr & "NULL,"
        End If
        'RouteID
        If RouteID > 0 Then
            SqlStr = SqlStr & RouteID.ToString & ","
        Else
            SqlStr = SqlStr & "NULL,"
        End If
        'srcitems
        If srcitems > -2 Then
            SqlStr = SqlStr & srcitems.ToString & ")"
        Else
            SqlStr = SqlStr & "0)"
        End If

        Rtrn = MyWmsDbTrans.OraDBExecuteSQLCmd(SqlStr, 1)

        If Rtrn > 0 Then
            Return PackItemID
        Else
            MyWmsDbTrans.f_sqlerrorlog(CompID, "FCreateCustomerPalette", SqlStr, "mobile")
            Return -1
        End If
    End Function

    Public Function FAddToCustomerPalette(ByVal CompID As Integer, ByVal BranchID As Integer, ByVal PackItemID As Long, ByVal OrderID As Long, ByVal OrderDtlID As Long, ByVal ItemID As Long, ByVal ItemLength As Decimal, ByVal ItemColorID As Integer, ByVal ItemColorID2 As Integer, ByVal ItemPQty As Decimal, ByVal ItemSQty As Decimal) As Long
        Dim SqlStr As String
        Dim PAckItemDtlID As Long


        Dim MyWmsDbTrans As New WmsDBTrans

        Dim Rtrn As Long

        PAckItemDtlID = FGetNewPackItemDtlID()

        SqlStr = "INSERT INTO TPACKAGESDTL(PACKITEMDTLID,PACKITEMID,"
        SqlStr = SqlStr & "ITEMID ,ITEMLENGTH,ITEMCOLORID,ITEMCOLORID2,ORDERID,ORDERDTLID,ITEMBARS,ITEMWEIGHT) VALUES ("

        If PAckItemDtlID > 0 Then
            SqlStr = SqlStr & PAckItemDtlID.ToString & ","
        Else
            SqlStr = SqlStr & "NULL,"
        End If
        'PackItemID
        If PackItemID > 0 Then
            SqlStr = SqlStr & PackItemID.ToString & ","
        Else
            SqlStr = SqlStr & "NULL,"
        End If
        If ItemID > 0 Then
            SqlStr = SqlStr & ItemID.ToString & ","
        Else
            SqlStr = SqlStr & "NULL,"
        End If
        If ItemLength > 0 Then
            SqlStr = SqlStr & f_dbinsertdecimal(ItemLength, 3) & ","
        Else
            SqlStr = SqlStr & "NULL,"
        End If
        If ItemColorID > 0 Then
            SqlStr = SqlStr & ItemColorID.ToString & ","
        Else
            SqlStr = SqlStr & "NULL,"
        End If
        If ItemColorID2 > 0 Then
            SqlStr = SqlStr & ItemColorID2.ToString & ","
        Else
            SqlStr = SqlStr & "NULL,"
        End If
        If OrderID > 0 Then
            SqlStr = SqlStr & OrderID.ToString & ","
        Else
            SqlStr = SqlStr & "NULL,"
        End If
        'OrderDtlID
        If OrderDtlID > 0 Then
            SqlStr = SqlStr & OrderDtlID.ToString & ","
        Else
            SqlStr = SqlStr & "NULL,"
        End If
        If ItemPQty > 0 Then
            SqlStr = SqlStr & f_dbinsertdecimal(ItemPQty, 0) & ","
        Else
            SqlStr = SqlStr & "NULL,"
        End If
        If ItemSQty > 0 Then
            SqlStr = SqlStr & f_dbinsertdecimal(ItemSQty, 0)
        Else
            SqlStr = SqlStr & "NULL"
        End If
        SqlStr = SqlStr & ")"

        Rtrn = MyWmsDbTrans.OraDBExecuteSQLCmd(SqlStr, 1)

        If Rtrn > 0 Then
            Return PackItemID
        Else
            MyWmsDbTrans.f_sqlerrorlog(CompID, "WmsBinTrans.FAddToCustomerPalette", SqlStr, "Admin")
            Return -1
        End If

    End Function

    Public Function FUpdateCustomerPalette(ByVal CompID As Integer, ByVal BranchID As Integer, ByVal PackItemID As Long, ByVal OrderID As Long, ByVal OrderDtlID As Long, ByVal ItemID As Long, ByVal ItemLength As Decimal, ByVal ItemColorID As Integer, ByVal ItemColorID2 As Integer, ByVal ItemPqty As Decimal, ByVal ItemSqty As Decimal) As Long
        Dim SqlStr As String
        Dim CrRtrn As Long
        Dim MyWmsDBTrans As New WmsDBTrans

        Dim ItemPqtyStr, ItemSqtyStr As String

        '===Delete entry when qty = 0
        If ItemPqty = 0 Then

            SqlStr = "DELETE FROM TPACKAGESDTL WHERE PACKITEMID=" & PackItemID.ToString & " AND "
            SqlStr = SqlStr & " ORDERID=" & OrderID.ToString & " AND "

            If OrderDtlID > 0 Then
                SqlStr = SqlStr & " ORDERDTLID = " & OrderDtlID.ToString
            Else
                SqlStr = SqlStr & " ITEMID = " & ItemID.ToString & " AND "
                SqlStr = SqlStr & " ITEMCOLORID =" & ItemColorID.ToString & " "
                If ItemColorID2 > 0 Then
                    SqlStr = SqlStr & " AND ITEMCOLORID2 =" & ItemColorID2.ToString & " AND "
                End If
                SqlStr = SqlStr & " AND ITEMLENGTH=" & f_dbinsertdecimal(ItemLength, 3)
            End If


            CrRtrn = MyWmsDBTrans.OraDBExecuteSQLCmd(SqlStr, 1)

            If Not CrRtrn > 0 Then
                If Len(SqlStr) = 0 Then SqlStr = "empty string>>"
                MyWmsDBTrans.f_sqlerrorlog(CompID, "FUpdateCustomerPalette>>", SqlStr, "admin")
            End If

            If CrRtrn > 0 And PackItemID > 0 Then
                SqlStr = "DELETE FROM TWMSBINITEMSQTY WHERE PACKITEMID=" & PackItemID.ToString & " AND "
                SqlStr = SqlStr & " ORDERID=" & OrderID.ToString & " AND "

                If OrderDtlID > 0 Then
                    SqlStr = SqlStr & " ORDERDTLID = " & OrderDtlID.ToString
                Else
                    SqlStr = SqlStr & " ITEMID = " & ItemID.ToString & " AND "
                    SqlStr = SqlStr & " ITEMCOLORID =" & ItemColorID.ToString
                    If ItemColorID2 > 0 Then
                        SqlStr = SqlStr & " AND ITEMCOLORID2 =" & ItemColorID2.ToString
                    End If
                    SqlStr = SqlStr & " AND ITEMLENGTH=" & f_dbinsertdecimal(ItemLength, 3)
                End If

                CrRtrn = MyWmsDBTrans.OraDBExecuteSQLCmd(SqlStr, 1)

                If Not CrRtrn > 0 Then
                    If Len(SqlStr) = 0 Then SqlStr = "empty string>>"
                    MyWmsDBTrans.f_sqlerrorlog(CompID, "FUpdateCustomerPalette>>", SqlStr, "admin")
                End If
                '==delete from twmspackinglists
                SqlStr = "DELETE FROM TWMSPACKINGLISTS WHERE PACKITEMID=" & PackItemID.ToString & " AND "
                SqlStr = SqlStr & " ORDERID=" & OrderID.ToString & " AND "

                If OrderDtlID > 0 Then
                    SqlStr = SqlStr & " ORDERDTLID = " & OrderDtlID.ToString
                Else
                    SqlStr = SqlStr & " ITEMID = " & ItemID.ToString & " AND "
                    SqlStr = SqlStr & " ITEMCOLORID =" & ItemColorID.ToString
                    If ItemColorID2 > 0 Then
                        SqlStr = SqlStr & " AND ITEMCOLORID2 =" & ItemColorID2.ToString
                    End If
                    SqlStr = SqlStr & " AND ITEMLENGTH=" & f_dbinsertdecimal(ItemLength, 3)
                End If

                CrRtrn = MyWmsDBTrans.OraDBExecuteSQLCmd(SqlStr, 1)

                If Not CrRtrn > 0 Then
                    If Len(SqlStr) = 0 Then SqlStr = "empty string>>"
                    MyWmsDBTrans.f_sqlerrorlog(CompID, "FUpdateCustomerPalette>>", SqlStr, "admin")
                End If

                '====
            Else
                Return -1
            End If 'CrRtrn > 0 And PackItemID > 0

            Return 1
        End If  'ItemPqty = 0 remove qty from palette
        '=====
        '====update when qty > 0
        If ItemPqty > 0 Then
            ItemPqtyStr = Long.Parse(Math.Round(ItemPqty)).ToString
        End If

        If ItemSqty > 0 Then
            ItemSqtyStr = Long.Parse(Math.Round(ItemSqty)).ToString
        Else
            ItemSqtyStr = "0"
        End If


        SqlStr = "UPDATE  TPACKAGESDTL SET ITEMBARS=" & ItemPqtyStr & ",ITEMWEIGHT=" & ItemSqtyStr & " WHERE PACKITEMID=" & PackItemID.ToString & " AND "
        SqlStr = SqlStr & " ORDERID=" & OrderID.ToString & " AND "

        If OrderDtlID > 0 Then
            SqlStr = SqlStr & " ORDERDTLID = " & OrderDtlID.ToString
        Else
            SqlStr = SqlStr & " ITEMID = " & ItemID.ToString & " AND "
            SqlStr = SqlStr & " ITEMCOLORID =" & ItemColorID.ToString & " "
            If ItemColorID2 > 0 Then
                SqlStr = SqlStr & " AND ITEMCOLORID2 =" & ItemColorID2.ToString
            End If
            SqlStr = SqlStr & " AND ITEMLENGTH=" & f_dbinsertdecimal(ItemLength, 3)
        End If

        CrRtrn = MyWmsDBTrans.OraDBExecuteSQLCmd(SqlStr, 1)

        If Not CrRtrn > 0 Then
            If Len(SqlStr) = 0 Then SqlStr = "empty string>>"
            MyWmsDBTrans.f_sqlerrorlog(CompID, "FUpdateCustomerPalette>>", SqlStr, "admin")
        End If

        If CrRtrn > 0 Then
            ' update palette if in Shipping plan
            SqlStr = "UPDATE  TWMSPACKINGLISTS SET ITEMPRIMARYQTY=" & ItemPqtyStr & ",ITEMSECONDARYQTY=" & ItemSqtyStr & " WHERE PACKITEMID=" & PackItemID.ToString & " AND "
            SqlStr = SqlStr & " ORDERID=" & OrderID.ToString & " AND "
            If OrderDtlID > 0 Then
                SqlStr = SqlStr & " ORDERDTLID = " & OrderDtlID.ToString
            Else
                SqlStr = SqlStr & " ITEMID = " & ItemID.ToString & " AND "
                SqlStr = SqlStr & " ITEMCOLORID =" & ItemColorID.ToString
                If ItemColorID2 > 0 Then
                    SqlStr = SqlStr & " AND ITEMCOLORID2 =" & ItemColorID2.ToString
                End If
                SqlStr = SqlStr & " AND ITEMLENGTH=" & f_dbinsertdecimal(ItemLength, 3)
            End If

            Try

            Catch ex As Exception
                MyWmsDBTrans.OraDBExecuteSQLCmd(SqlStr, 1)
            End Try

            SqlStr = "UPDATE  TWMSBINITEMSQTY SET ITEMQTYPRIMARY=" & ItemPqtyStr & ",ITEMQTYSECONDARY=" & ItemSqtyStr & " WHERE PACKITEMID=" & PackItemID.ToString & " AND "
            SqlStr = SqlStr & " ORDERID=" & OrderID.ToString & " AND "
            If OrderDtlID > 0 Then
                SqlStr = SqlStr & " ORDERDTLID = " & OrderDtlID.ToString
            Else
                SqlStr = SqlStr & " ITEMID = " & ItemID.ToString & " AND "
                SqlStr = SqlStr & " ITEMCOLORID =" & ItemColorID.ToString
                If ItemColorID2 > 0 Then
                    SqlStr = SqlStr & " AND ITEMCOLORID2 =" & ItemColorID2.ToString
                End If
                SqlStr = SqlStr & " AND ITEMLENGTH=" & f_dbinsertdecimal(ItemLength, 3)
            End If

            CrRtrn = MyWmsDBTrans.OraDBExecuteSQLCmd(SqlStr, 1)
            If CrRtrn > 0 Then
                Return CrRtrn
            Else
                If Len(SqlStr) = 0 Then SqlStr = "empty string>>"
                MyWmsDBTrans.f_sqlerrorlog(CompID, "FUpdateCustomerPalette>>", SqlStr, "admin")
            End If

        End If

        Return 1
    End Function

    Public Function FOrderRemoveReservedItems(ByVal CompID As Integer, ByVal BranchID As Integer, ByVal OrderDtlId As Long, ByVal ItemDoneQty As Long, ByVal SrcITems As Integer) As Long

        '{SrcItems 3: picked items for shipping,4:PickedItems for Prod}
        Dim SqlStr As String
        Dim MyWmsDBTrans As New WmsDBTrans

        Dim TransTypeset As String
        Dim StoreTransID As Long
        Dim StoreTRansDtlID As Long
        Dim TransDtlItemQty As Long
        Dim i As Integer

        Dim OrderDtlItems As New DataSet
        Dim TRANSTYPERESERVATION As Integer
        Dim TRANSTYPERESERVE4PROD As Integer

        Dim TransType As Integer

        If SrcITems = 4 Then
            SqlStr = "SELECT TRANSTYPERESERVE4PROD  FROM TPRODPARAMS "
            If CompID > 0 Then
                SqlStr = SqlStr & " WHERE COMPID=" & CompID.ToString
                If BranchID > 0 Then
                    SqlStr = SqlStr & " AND BRANCHID=" & BranchID.ToString()
                End If
            End If
            Try
                TRANSTYPERESERVE4PROD = Integer.Parse(MyWmsDBTrans.OraDBWmsExSelectCmdRN2String(SqlStr))
            Catch ex As Exception
                Return -3
            End Try
            TransType = TRANSTYPERESERVE4PROD
        Else
            SqlStr = "SELECT TRANSTYPERESERVATION  FROM TSTOREPARAMS "
            If CompID > 0 Then
                SqlStr = SqlStr & " WHERE COMPID=" & CompID.ToString
            End If
            Try
                TRANSTYPERESERVATION = Integer.Parse(MyWmsDBTrans.OraDBWmsExSelectCmdRN2String(SqlStr))
            Catch ex As Exception
                Return -2
            End Try
            TransType = TRANSTYPERESERVATION
        End If


        SqlStr = "SELECT STORETRANSDTLID,STORETRANSID,ITEMQTYPRIMARY FROM TSTORETRANSDETAILS WHERE SALESORDERDTL =" & OrderDtlId.ToString()
        SqlStr = SqlStr & " AND STORETRANSID IN (SELECT STORETRANSID FROM TSTORETRANS WHERE STORETRANSTYPE = " & TransType.ToString() & ")"
        SqlStr = SqlStr & " ORDER BY STORETRANSDTLID"

        Try
            OrderDtlItems = MyWmsDBTrans.OraDBFillDataset(SqlStr, "DSORDEITEMSRESERVED")
        Catch ex As Exception
            Return -1
        End Try

        'ItemDoneQty
        If OrderDtlItems.Tables(0).Rows.Count > 0 Then
            For Each Dr As DataRow In OrderDtlItems.Tables(0).Rows
                StoreTransID = 0
                StoreTRansDtlID = 0

                Try
                    StoreTransID = Long.Parse(Dr("STORETRANSID").ToString())
                Catch ex As Exception
                End Try
                Try
                    StoreTRansDtlID = Long.Parse(Dr("STORETRANSDTLID").ToString())
                Catch ex As Exception
                End Try
                Try
                    TransDtlItemQty = Long.Parse(Dr("ITEMQTYPRIMARY").ToString())
                Catch ex As Exception
                End Try

                If (StoreTRansDtlID > 0 And TransDtlItemQty > 0) Then
                    If (ItemDoneQty - TransDtlItemQty) >= 0 Then ' delete 
                        ItemDoneQty = ItemDoneQty - TransDtlItemQty
                        FClearReservedTrans(StoreTransID, StoreTRansDtlID)
                    Else
                        FUpdateReservedTransQty(StoreTRansDtlID, ItemDoneQty)
                        ItemDoneQty = 0
                    End If

                    If ItemDoneQty < 1 Then Exit For
                End If
            Next

        End If

    End Function

    Public Function FUpdateReservedTransQty(ByVal StoreTransDtlID As Long, ByVal RestQty As Long) As Long
        Dim MyWmsDBTrans As New WmsDBTrans
        Dim SqlStr As String
        Dim StoreTransDtls As Integer
        Dim Rtrn As Long

        Rtrn = 0

        SqlStr = "UPDATE TSTORETRANSDETAILS SET ITEMQTYPRIMARY = " & RestQty.ToString & " WHERE STORETRANSDTLID=" & StoreTransDtlID.ToString()
        Try
            Rtrn = MyWmsDBTrans.OraDBExecuteSQLCmd(SqlStr, 1)
        Catch ex As Exception
            Rtrn = -1
        End Try


        Return Rtrn

    End Function

    Public Function FClearReservedTrans(ByVal StoreTransID As Long, ByVal StoreTransDtlID As Long) As Long
        Dim MyWmsDBTrans As New WmsDBTrans
        Dim SqlStr As String
        Dim StoreTransDtls As Integer
        Dim Rtrn As Long

        Rtrn = 0

        Try
            SqlStr = "DELETE FROM TWMSROUTESDTL WHERE STORETRANSDTLID=" & StoreTransDtlID.ToString()
            Rtrn = MyWmsDBTrans.OraDBExecuteSQLCmd(SqlStr, 1)
        Catch ex As Exception
        End Try

        SqlStr = "DELETE FROM TSTORETRANSDETAILS WHERE STORETRANSDTLID=" & StoreTransDtlID.ToString()
        Rtrn = MyWmsDBTrans.OraDBExecuteSQLCmd(SqlStr, 1)


        If Rtrn > 0 Then
            Try
                StoreTransDtls = Integer.Parse(MyWmsDBTrans.OraDBWmsExSelectCmdRN2String("SELECT COUNT(*) FROM TSTORETRANS WHERE STORETRANSID=" & StoreTransID.ToString))
            Catch ex As Exception
                StoreTransDtls = 0
            End Try

            If Not (StoreTransDtls > 0) Then
                MyWmsDBTrans.OraDBExecuteSQLCmd("DELETE FROM TSTORETRANS WHERE STORETRANSID=" & StoreTransID.ToString(), 1)
            End If
        End If

        Return Rtrn

    End Function

#End Region

End Class

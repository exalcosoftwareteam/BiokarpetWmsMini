using System;
using System.Data;
using System.Text;
using System.Collections.Generic;
using WMSSyncService;
using System.IO;
using System.Web;


namespace WMSSyncService
{


    public  class Log
    {
        public void WriteToLog(string message)
        {
            string filepath = HttpContext.Current.Server.MapPath("~/");
            Console.WriteLine(message);

            try
            {
                if (!File.Exists(filepath + "\\logfile.txt"))
                {


                    using (FileStream fs = File.Create(filepath + "\\logfile.txt"))
                    {
                        Byte[] info = new UTF8Encoding(true).GetBytes("Log File Created  ");
                        fs.Write(info, 0, info.Length);
                    }
                    using (StreamReader sr = File.OpenText(filepath + "\\logfile.txt"))
                    {
                        string s = "";
                        while ((s = sr.ReadLine()) != null)
                        {
                            Console.WriteLine(s);
                        }
                    }

                }

                // Compose a string that consists of three lines.

                string lines = DateTime.Now.ToString() + " - " + message;
                // Write the string to a file.


                FileInfo f = new FileInfo(filepath + "\\logfile.txt");
                long s1 = f.Length;


                if (s1 > 5000)
                {
                    try
                    {
                        File.WriteAllText(filepath + "\\logfile.txt", String.Empty);
                    }
                    catch { }
                }
                TextWriter tsw = new StreamWriter(filepath + "\\logfile.txt", true);


                tsw.WriteLine(lines);
                tsw.Close();



            }
            catch
            {


            }

        }
    }


    public class PackingListHeaderHandler
    {
        public long InsertRecord(PackingListHeader packhdr)
        {
            OraDB db = new OraDB();
            StringBuilder sqlstr = new StringBuilder();
            long packheaderid = 0;
            long rtrn = 0;
            long customerid = 0;

            try
            {

                packheaderid = db.DBGetNumResultFromSQLSelect("SELECT ZIMPORTHEADER2_SEQ.NEXTVAL FROM DUAL");
            }
            catch { return -1; }
            //GET CUSTOMERID
            customerid = db.DBGetNumResultFromSQLSelect("SELECT ID FROM CUSTOMER WHERE CODE = '" + packhdr.CustomerCode+"'");
            //
            
            //GET NEXT VALUE  "SELECT ZIMPORTHEADER_SEQ.NEXTVAL FROM DUAL"
             sqlstr.Append("INSERT INTO ZIMPORTHEADER2(ID,STATUS,COMID,BRANCHID,FTRDATE,PERSONCODE,TRADECODE,KINDID,ERPCUSTOMERID,DSRID,TRANSCODE,STOREID) VALUES (");
            if (packheaderid > 0) sqlstr.Append(packheaderid.ToString() + ","); else sqlstr.Append("NULL,");
            if (packhdr.PackingListStatus != -1) sqlstr.Append("1,"); else sqlstr.Append("NULL,");
            if (packhdr.Compid > 0) sqlstr.Append(packhdr.Compid.ToString() + ","); else sqlstr.Append("NULL,");
            if (packhdr.Branchid > 0) sqlstr.Append(packhdr.Branchid.ToString() + ","); else sqlstr.Append("NULL,");
            //    if (!string.IsNullOrEmpty(packhdr.PackingListDate)) sqlstr.Append("CONVERT(DATETIME,'" + packhdr.PackingListDate + "',103),"); else sqlstr.Append("NULL,");
            //if (packhdr.TransType > 0) sqlstr.Append(packhdr.TransType.ToString() + ","); else sqlstr.Append("NULL,");
            sqlstr.Append("TO_DATE(SYSDATE),");
            if (!string.IsNullOrEmpty(packhdr.CustomerCode)) sqlstr.Append("" + packhdr.CustomerCode + ","); else sqlstr.Append("NULL,");
            if (!string.IsNullOrEmpty(packhdr.TransCode)) sqlstr.Append("'" + packhdr.TransCode + "',"); else sqlstr.Append("NULL,");
            if (packhdr.Kindid > 0) sqlstr.Append(packhdr.Kindid.ToString()+","); else sqlstr.Append("NULL,");
            if (customerid > 0) sqlstr.Append(customerid.ToString() + ","); else sqlstr.Append("NULL,");
            if (packhdr.DSRID > 0) sqlstr.Append(packhdr.DSRID.ToString() + ","); else sqlstr.Append("NULL,");
            if (packheaderid > 0) sqlstr.Append(packheaderid.ToString() + ","); else sqlstr.Append("NULL,");
            if (packhdr.StoreID > 0) sqlstr.Append(packhdr.StoreID.ToString()); else sqlstr.Append("NULL");
            sqlstr.Append(")");
            rtrn = db.DBExecuteSQLCmd(sqlstr.ToString());

            if (rtrn > 0)
            {

                db.DBExecuteSQLCmd("COMMIT");
                return packheaderid;
            }
            else
            {
                if (!(rtrn > 0)) db.f_sqlerrorlog(0, "ZIMPORTHEADER.InsertRecord", sqlstr.ToString().Replace("'", "|"), "WebService");
                return -1;
            }

        }

        public long TestErpCon()
        {

            OraDB db = new OraDB();
            return db.DBGetNumResultFromSQLSelect("SELECT ZIMPORTHEADER_SEQ.NEXTVAL FROM DUAL");

        }

    }

    public class PackingListHandler
    {
        OraDB db = new OraDB();
        public string GetBranchIDFromPackingListHeader(long packheaderid)
        {
            return db.DBWmsExSelectCmdRN2String("SELECT BRANCHID  FROM ZIMPORTHEADER2 WHERE ID=" + packheaderid.ToString());
        
        }
        public long InsertRecord(PackingListDetail pack)
        {
            long packdtlid = 0;
            PackingListHandler pckheader = new PackingListHandler();
            StringBuilder sqlstr = new StringBuilder();
            long rtrn = 0;
            string branchid ="";

            try
            {

                packdtlid = db.DBGetNumResultFromSQLSelect("SELECT ZIMPORTDETAIL2_SEQ.NEXTVAL FROM DUAL");
            }
            catch { return -1; }

            branchid = pckheader.GetBranchIDFromPackingListHeader(pack.PackingListHeaderID);
            sqlstr.Append("INSERT INTO ZIMPORTDETAIL2(ID,BRANCHID,STATUS,IMPID,ITEMCODE,QTY,QTY2,ZWIDTH,ZLENGTH,ZDRAFT,ZCOLOR,ITEMID,LOTID) VALUES (");
            if (packdtlid > 0) sqlstr.Append(packdtlid.ToString() + ","); else return -1;
            if (!string.IsNullOrEmpty(branchid)) sqlstr.Append(branchid+",1,"); else sqlstr.Append("NULL,1,");
            if (pack.PackingListHeaderID > 0) sqlstr.Append(pack.PackingListHeaderID.ToString() + ","); else sqlstr.Append("NULL,");
            if (!string.IsNullOrEmpty(pack.ItemCode)) sqlstr.Append("'" + pack.ItemCode + "',"); else sqlstr.Append("NULL,");
            if (pack.ItemQTYprimary > 0) sqlstr.Append(pack.ItemQTYprimary.ToString().Replace(",", ".") + ","); else sqlstr.Append("NULL,");
            if (pack.ItemQTYsecondary > 0) sqlstr.Append(pack.ItemQTYsecondary.ToString().Replace(",", ".") + ","); else sqlstr.Append("NULL,");
            if (pack.Width > 0) sqlstr.Append(pack.Width.ToString().Replace(",", ".") + ","); else sqlstr.Append("NULL,");
            if (pack.Length > 0) sqlstr.Append(pack.Length.ToString().Replace(",", ".") + ","); else sqlstr.Append("NULL,");
            if (!string.IsNullOrEmpty(pack.Draft)) sqlstr.Append("'" + pack.Draft + "',"); else sqlstr.Append("NULL,");
            if (!string.IsNullOrEmpty(pack.Color)) sqlstr.Append("'" + pack.Color + "',"); else sqlstr.Append("NULL,");
            if (pack.ItemID > 0) sqlstr.Append(pack.ItemID.ToString().Replace(",", ".") + ","); else sqlstr.Append("NULL,");
            if (pack.LotID > 0) sqlstr.Append(pack.LotID.ToString().Replace(",", ".")); else sqlstr.Append("NULL");
            sqlstr.Append(")");
            try { rtrn = db.DBExecuteSQLCmd(sqlstr.ToString()); }
            catch (Exception ex) { db.f_sqlerrorlog(0, "InsertRecord>>", ex.ToString().Replace("'", "|"), "Webservice"); }

            if (!(rtrn > 0))
            {
                db.f_sqlerrorlog(0, "InsertRecord>>" + pack.ItemCode, sqlstr.ToString().Replace("'", "|"), "Webservice");
                return -1;
            }
            else
            {
                db.DBExecuteSQLCmd("COMMIT");
                return rtrn;
            }
        }

    }

    public class CustomerHandler
    {
        public Customer GetCustomerbycode(string customercode)
        {
            OraDB db = new OraDB();
            Customer erpcustomer = new Customer();
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.Append("SELECT  ID,CODE,NAME,OCCUPATION,STREET1,ZIPCODE1,CITY1,PHONE11,PHONE12,FAX1,ZALPISCODE   FROM  CUSTOMER where COMID = 2 AND CODE ='" + customercode + "'");
            DataTable dt = new DataTable();
            dt = db.DBFillDataTable(sqlstr.ToString(), "Customer");
            if (dt.Rows.Count > 0)
            {
                if (!(dt.Rows[0]["CITY1"] == DBNull.Value)) erpcustomer.CITY1 = dt.Rows[0]["CITY1"].ToString();
                if (!(dt.Rows[0]["CODE"] == DBNull.Value)) erpcustomer.CustomerCode = dt.Rows[0]["CODE"].ToString();
                if (!(dt.Rows[0]["NAME"] == DBNull.Value)) erpcustomer.CustomerTitle = dt.Rows[0]["NAME"].ToString();
                if (!(dt.Rows[0]["ID"] == DBNull.Value)) erpcustomer.erpCustomerID = long.Parse(dt.Rows[0]["ID"].ToString());
                if (!(dt.Rows[0]["FAX1"] == DBNull.Value)) erpcustomer.FAX1 = dt.Rows[0]["FAX1"].ToString();
                if (!(dt.Rows[0]["OCCUPATION"] == DBNull.Value)) erpcustomer.Occupation = dt.Rows[0]["OCCUPATION"].ToString();
                if (!(dt.Rows[0]["PHONE11"] == DBNull.Value)) erpcustomer.phone1 = dt.Rows[0]["PHONE11"].ToString();
                if (!(dt.Rows[0]["PHONE12"] == DBNull.Value)) erpcustomer.phone2 = dt.Rows[0]["PHONE12"].ToString();
                if (!(dt.Rows[0]["STREET1"] == DBNull.Value)) erpcustomer.STREET1 = dt.Rows[0]["STREET1"].ToString();
                if (!(dt.Rows[0]["ZALPISCODE"] == DBNull.Value)) erpcustomer.ZALPISCODE = dt.Rows[0]["ZALPISCODE"].ToString();
                if (!(dt.Rows[0]["ZIPCODE1"] == DBNull.Value)) erpcustomer.ZIPCODE1 = dt.Rows[0]["ZIPCODE1"].ToString();
            }
             
            return erpcustomer ;
        }


    }

    public class CarpetCleaningHandler
    {
        OraDB db = new OraDB();
        public long InsertRecord(CarpetTrans trans)
        {
            StringBuilder sqlstr = new StringBuilder();
            long rtrn = 0;

            sqlstr.Append("INSERT INTO ZCLEANINGCARPETSTRANS (TRANSID,ERPCompID, ERPBRACHID ,AlpisStoreTransID,WMSTRANSID, AlpisStoreTransDate,DispatchDate,ERPTransSeriesID, ERPCustomerID,ERPSupplierID,");
            sqlstr.Append("ERPFromStoreID,ERPToStoreID, ERPItemID, ERPItemMUnit,ERPITEMSMUNIT,ERPItemQty,ERPITEMSQTY,ERPItemUnitPrice, ERPItemQtyPrice,EXTRASERVICE,ERPTRANSCODE,DOCTYPE,isnew) VALUES (");
            if (trans.TRANSID > 0) sqlstr.Append(trans.TRANSID.ToString() + ","); else return -1;
            if (trans.ERPCompID > 0) sqlstr.Append(trans.ERPCompID.ToString() + ","); else sqlstr.Append("NULL,");
            if (trans.ERPBRACHID > 0) sqlstr.Append(trans.ERPBRACHID.ToString() + ","); else sqlstr.Append("NULL,");
            if (trans.AlpisStoreTransID > 0) sqlstr.Append(trans.AlpisStoreTransID.ToString() + ","); else sqlstr.Append("NULL,");
            if (trans.WMSTRANSID > 0) sqlstr.Append(trans.WMSTRANSID.ToString() + ","); else sqlstr.Append("NULL,");
            if (!string.IsNullOrEmpty(trans.AlpisStoreTransDate)) sqlstr.Append(" TO_DATE('" + trans.AlpisStoreTransDate + "','DD/MM/YYYY')" + ","); else sqlstr.Append("NULL,");
            if (!string.IsNullOrEmpty(trans.DispatchDate)) sqlstr.Append(" TO_DATE('" + trans.DispatchDate + "','DD/MM/YYYY')" + ","); else sqlstr.Append("NULL,");
            if (trans.ERPTransSeriesID > 0) sqlstr.Append(trans.ERPTransSeriesID.ToString() + ","); else sqlstr.Append("NULL,");
            if (trans.ERPCustomerID > 0) sqlstr.Append(trans.ERPCustomerID.ToString() + ","); else sqlstr.Append("NULL,");
            if (trans.ERPSupplierID > 0) sqlstr.Append(trans.ERPSupplierID.ToString() + ","); else sqlstr.Append("NULL,");
            if (trans.ERPFromStoreID > 0) sqlstr.Append(trans.ERPFromStoreID.ToString() + ","); else sqlstr.Append("NULL,");
            if (trans.ERPToStoreID > 0) sqlstr.Append(trans.ERPToStoreID.ToString() + ","); else sqlstr.Append("NULL,");
            if (trans.ERPItemID > 0) sqlstr.Append(trans.ERPItemID.ToString() + ","); else sqlstr.Append("NULL,");
            if (trans.ERPItemMUnit > 0) sqlstr.Append(trans.ERPItemMUnit.ToString() + ","); else sqlstr.Append("NULL,");
            if (trans.ERPItemSMUnit > 0) sqlstr.Append(trans.ERPItemSMUnit.ToString() + ","); else sqlstr.Append("NULL,");
            if (trans.ERPItemQty > 0) sqlstr.Append(trans.ERPItemQty.ToString().Replace(",", ".") + ","); else sqlstr.Append("NULL,");
            if (trans.ERPITEMSQTY > 0) sqlstr.Append(trans.ERPITEMSQTY.ToString().Replace(",", ".") + ","); else sqlstr.Append("NULL,");
            if (trans.ERPItemUnitPrice > 0) sqlstr.Append(trans.ERPItemUnitPrice.ToString().Replace(",", ".") + ","); else sqlstr.Append("NULL,");
            if (trans.ERPItemQtyPrice > 0) sqlstr.Append(trans.ERPItemQtyPrice.ToString().Replace(",", ".") + ","); else sqlstr.Append("NULL,");
            if (!string.IsNullOrEmpty(trans.EXTRASERVICE)) sqlstr.Append("'" + trans.EXTRASERVICE + "',"); else sqlstr.Append("NULL,");
            if (!string.IsNullOrEmpty(trans.ERPTRANSCODE)) sqlstr.Append("'" + trans.ERPTRANSCODE + "',"); else sqlstr.Append("NULL,");
            if (trans.DOCTYPE > 0) sqlstr.Append(trans.DOCTYPE.ToString() + ","); else sqlstr.Append("NULL,");
            if (trans.isnew >= 0) sqlstr.Append(trans.isnew.ToString()); else sqlstr.Append("NULL");
            sqlstr.Append(")");

            try { rtrn = db.DBExecuteSQLCmd(sqlstr.ToString()); }
            catch (Exception ex) { db.f_sqlerrorlog(0, "InsertRecord>>", ex.ToString().Replace("'", "|"), "Webservice"); }

            if (!(rtrn > 0))
            {
                //   db.f_sqlerrorlog(0, "InsertRecord>>" + pack.ItemCode, sqlstr.ToString().Replace("'", "|"), "Webservice");
                return -1;
            }
            else
            {
                db.DBExecuteSQLCmd("COMMIT");
                return rtrn;
            }
        }

        public long CancelErpSend(long wmstransid) 
        {
            try 
            {
                db.DBExecuteSQLCmd("DELETE FROM ZCLEANINGCARPETSTRANS WHERE WMSTRANSID = " + wmstransid.ToString());
                db.DBExecuteSQLCmd("COMMIT");
                return 1;
            }
            catch 
            {
                return -1;
            }


        
        }

    }

    public class OrderHandler {

        public long GetErpOrderID(string DSRNUMBER)
        {

            OraDB db = new OraDB();
            return db.DBGetNumResultFromSQLSelect("SELECT ID from FINTRADE WHERE DSRNUMBER ='"+DSRNUMBER+"'");

        }
    
    }





    public class SyncData
    {
        OraDB db = new OraDB();
        public SyncInfo syncinfo = new SyncInfo();

        public SyncData() { }

        public int DBConnectionState()
        {
            int rtrn = 0;
            try { rtrn = int.Parse(db.DBWmsExSelectCmdRN2String("SELECT 1 FROM DUAL")); }
            catch { }
            return rtrn;
        }

        public List<ERPStore> FGetStores(short compid)
        {
            DataTable DT = new DataTable();
            StringBuilder sqlstr = new StringBuilder();
            List<ERPStore> stores = new List<ERPStore>();


            sqlstr.Append("SELECT  COMID AS COMPID,CODEID AS STOREID,NAME AS STORENAME   FROM STORE");
            if (compid > 0) sqlstr.Append(" WHERE COMID=" + compid.ToString());

            sqlstr.Append(" ORDER BY NAME");

            DT= db.DBFillDataTable(sqlstr.ToString(),"DSSTORE");

            if (DT.Rows.Count > 0)
            {
                foreach (DataRow dr in DT.Rows)
                {
                    ERPStore store = new ERPStore();
                    if (dr["COMPID"] != DBNull.Value) store.compID = short.Parse(dr["COMPID"].ToString());
                    if (dr["STOREID"] != DBNull.Value) store.storeID = short.Parse(dr["STOREID"].ToString());

                    store.storeName = dr["STORENAME"].ToString();

                    stores.Add(store);
                }
            }

            return stores;
        }

        public List<ERPMunit> FGetMunits()
        {
            DataTable DT = new DataTable();
            StringBuilder sqlstr = new StringBuilder();

            List<ERPMunit> munits = new List<ERPMunit>();
            

            sqlstr.Append("SELECT  CODEID AS MUNITID,DESCR AS MUNIT,DECIMALS  AS MUNITDECIMALS   FROM MESUNIT");  
       

            DT = db.DBFillDataTable(sqlstr.ToString(), "DSSTORE");

            if (DT.Rows.Count > 0)
            {
                foreach (DataRow dr in DT.Rows)
                {
                    ERPMunit munit = new ERPMunit();
                    if (dr["MUNITID"] != DBNull.Value) munit.munitID = int.Parse(dr["MUNITID"].ToString());
                    munit.mUnit = dr["MUNIT"].ToString();
                    if (dr["MUNITDECIMALS"] != DBNull.Value) munit.munitDecimals = short.Parse(dr["MUNITDECIMALS"].ToString());

                    munits.Add(munit);
                }
            }

            return munits;
        }

        public SyncInfo FCreateInvItemsData(int StoreID, bool GetZeroQty, string lastsyncdate)
        {
            SyncInfo itemsyncinfo = new SyncInfo();
            StringBuilder sqlstr = new StringBuilder();
            long affectedrows = 0;

            //GetZeroQty = true  21/6/2013
            GetZeroQty = true;


            string SrcTableData = "ZVBRAPOGRAFH";
            string tblname = "ZSTORESINVERNTORYITEMS_";
            bool wherecl = false;

            if (StoreID > 0)
                tblname += StoreID.ToString();

            sqlstr = new StringBuilder();
            db.DBExecuteSQLCmd("DROP TABLE " + tblname);
            db.DBExecuteSQLCmd("commit");
            sqlstr.Append("CREATE TABLE " + tblname + " AS ");

            sqlstr.Append("  SELECT ROWNUM AS ID,DATA.* FROM (SELECT  DISTINCT COMPID,ITEMID,ITEMCODE,ITEMDESC,MUNITPRIMARY,MUNITSECONDARY,NVL(ENTRYDATE,'01-01-2005') as ENTRYDATE ");
            sqlstr.Append(" FROM " + SrcTableData+" ");


            
            if ((!(string.IsNullOrEmpty(lastsyncdate)))&&(lastsyncdate != "null"))
            {
                if (wherecl) sqlstr.Append(" AND "); else sqlstr.Append(" WHERE ");

                sqlstr.Append(" NVL(ENTRYDATE,'01-01-2005') >=  TO_DATE('" + lastsyncdate + "','DD/MM/YYYY')");
            }

            sqlstr.Append(" ORDER BY TO_DATE(ENTRYDATE,'DD/MM/YYYY') ASC) DATA ");

            affectedrows = db.DBExecuteSQLCmd(sqlstr.ToString());
            if (affectedrows > 0)

            try { itemsyncinfo.MinItemID = long.Parse(db.DBWmsExSelectCmdRN2String("SELECT MIN(ITEMID) AS MINITEMID FROM " + tblname)); }
            catch { }
            try { itemsyncinfo.MaxItemID = long.Parse(db.DBWmsExSelectCmdRN2String("SELECT MAX(ITEMID) AS MAXITEMID FROM " + tblname)); }
            catch { }
            try { itemsyncinfo.ItemsRowsCount = long.Parse(db.DBWmsExSelectCmdRN2String("SELECT COUNT(DISTINCT ITEMID) AS CNTITEMS FROM " + tblname)); }
            catch { }

            itemsyncinfo.MinItemRowid = 1;
            itemsyncinfo.MaxItemRowid = itemsyncinfo.ItemsRowsCount;
            return itemsyncinfo;
        }

        public string FCreateInvItemsData(int StoreID, bool GetZeroQty)
        {
            StringBuilder sqlstr = new StringBuilder();
            string rtrn = null;
            long affectedrows = 0;

            //GetZeroQty = true  21/6/2013
            GetZeroQty = true;

            string SrcTableData = "ZVBRAPOGRAFH";
            string tblname = "ZSTORESINVERNTORYITEMS_";

            if (StoreID > 0)
                tblname += StoreID.ToString();


            db.DBExecuteSQLCmd("DROP TABLE " + tblname);

            sqlstr.Append("CREATE TABLE " + tblname + " AS ");

            sqlstr.Append("  SELECT ROWNUM AS ID,DATA.* FROM (SELECT  DISTINCT COMPID,ITEMID,ITEMCODE,ITEMDESC,MUNITPRIMARY,MUNITSECONDARY,NVL(ENTRYDATE,'01-01-2009') as ENTRYDATE ");
            sqlstr.Append(" FROM " + SrcTableData + " ");

            sqlstr.Append(" ORDER BY TO_DATE(ENTRYDATE,'DD/MM/YYYY') ASC) DATA ");

            affectedrows = db.DBExecuteSQLCmd(sqlstr.ToString());
            if (affectedrows > 0)

            try { syncinfo.MinItemID = long.Parse(db.DBWmsExSelectCmdRN2String("SELECT MIN(ITEMID) AS MINITEMID FROM " + tblname)); }
            catch { }
            try { syncinfo.MaxItemID = long.Parse(db.DBWmsExSelectCmdRN2String("SELECT MAX(ITEMID) AS MAXITEMID FROM " + tblname)); }
            catch { }
            try { syncinfo.ItemsRowsCount = long.Parse(db.DBWmsExSelectCmdRN2String("SELECT COUNT(DISTINCT ITEMID) AS CNTITEMS FROM " + tblname)); }
            catch { }

            syncinfo.MinItemRowid = 1;
            syncinfo.MaxItemRowid = syncinfo.ItemsRowsCount;
            return rtrn = affectedrows.ToString();
        }


        public SyncInfo FCreateInvLotsData(int StoreID, bool GetZeroQty, string lastsyncdate)
        {
            SyncInfo lotsyncinfo = new SyncInfo();
            StringBuilder sqlstr = new StringBuilder();
            long affectedrows = 0;
            bool wherecl = false;

            //GetZeroQty = true  21/6/2013
            GetZeroQty = true;


            string SrcTableData = "ZVBRAPOGRAFH";
            string tblname = "ZSTORESINVERNTORYLOTS_";
           

            if (StoreID > 0)
                tblname += StoreID.ToString();

            db.DBExecuteSQLCmd("DROP TABLE " + tblname);
            db.DBExecuteSQLCmd("commit");

            sqlstr.Append("CREATE TABLE " + tblname + " ");


          sqlstr.Append(" AS  SELECT ROWNUM AS ID,DATA.* FROM (SELECT  DISTINCT COMPID,LOTID,LOTCODE,ITEMID,WIDTH,LENGTH,DRAFT,COLOR,ZENTRYDATE FROM "+SrcTableData+" ");

           

            if ((!(string.IsNullOrEmpty(lastsyncdate))) && (lastsyncdate != "null"))
            {
                if (wherecl) sqlstr.Append(" AND "); else sqlstr.Append(" WHERE ");

                sqlstr.Append(" ZENTRYDATE >=  TO_DATE('" + lastsyncdate + "','DD/MM/YYYY')");
            }

            sqlstr.Append(" ORDER BY TO_DATE( ZENTRYDATE,'DD-MM-YYYY') ASC) DATA ");

            affectedrows = db.DBExecuteSQLCmd(sqlstr.ToString());

            if (affectedrows > 0)
            try { lotsyncinfo.MinLotID = long.Parse(db.DBWmsExSelectCmdRN2String("SELECT MIN(LOTID) AS MINLOTID FROM " + tblname)); }
            catch { }
            try { lotsyncinfo.MaxLotID = long.Parse(db.DBWmsExSelectCmdRN2String("SELECT MAX(LOTID) AS MAXLOTID FROM " + tblname)); }
            catch { }
            try { lotsyncinfo.LotRowsCount = long.Parse(db.DBWmsExSelectCmdRN2String("SELECT COUNT(LOTID) AS CNTLOTID FROM " + tblname)); }
            catch { }
            syncinfo.MinLotRowID = 1;
            syncinfo.MaxLotRowID = lotsyncinfo.LotRowsCount;

            return lotsyncinfo;
        }
        

        public string FCreateInvLotsData(int StoreID, bool GetZeroQty)
        {
            StringBuilder sqlstr = new StringBuilder();
            string rtrn = null;
            long affectedrows = 0;

            string SrcTableData = "ZVBRAPOGRAFH";
            string tblname = "ZSTORESINVERNTORYLOTS_";

            //GetZeroQty = true  21/6/2013
            GetZeroQty = true;


            if (StoreID > 0)
                tblname += StoreID.ToString();

            db.DBExecuteSQLCmd("DROP TABLE " + tblname);

            sqlstr.Append("CREATE TABLE " + tblname + " ");

            sqlstr.Append(" AS  SELECT ROWNUM AS ID,COMPID,LOTID,LOTCODE,ITEMID,WIDTH,LENGTH,DRAFT,COLOR,ZENTRYDATE FROM " + SrcTableData + " ");


            sqlstr.Append(" ORDER BY TO_DATE( ZENTRYDATE,'DD-MM-YYYY') ASC ");

            affectedrows = db.DBExecuteSQLCmd(sqlstr.ToString());

            if (affectedrows > 0)

            try { syncinfo.MinLotID = long.Parse(db.DBWmsExSelectCmdRN2String("SELECT MIN(LOTID) AS MINLOTID FROM " + tblname)); }
            catch { }
            try { syncinfo.MaxLotID = long.Parse(db.DBWmsExSelectCmdRN2String("SELECT MAX(LOTID) AS MAXLOTID FROM " + tblname)); }
            catch { }
            try { syncinfo.LotRowsCount = long.Parse(db.DBWmsExSelectCmdRN2String("SELECT COUNT(LOTID) AS CNTLOTID FROM " + tblname)); }
            catch { }

            syncinfo.MinLotRowID = 1;
            syncinfo.MaxLotRowID = syncinfo.LotRowsCount;

            return rtrn = affectedrows.ToString();
        }


        public string FCreateInvStatusData(int StoreID, bool GetZeroQty)
        {
            StringBuilder sqlstr = new StringBuilder();
            string rtrn = null;
            long affectedrows = 0;
            bool wherecl = false;

            string SrcTableData = "ZVBRAPOGRAFH";
            string tblname = "ZSTORESINVERNTORYSTATUS_";

            if (StoreID > 0)
                tblname += StoreID.ToString();


            db.DBExecuteSQLCmd("DROP TABLE " + tblname);

            sqlstr.Append("CREATE TABLE " + tblname + " ");
            //sqlstr.Append(" ");
            //
            sqlstr.Append("AS SELECT ROWNUM AS ID,STOREID,COMPID,BRANCHID,STORENAME,LOTID,LOTCODE,ITEMID,ITEMCODE,ITEMDESC,DESCRIPTION,MUNITPRIMARY,MUNITSECONDARY,");
            sqlstr.Append("MUNIT,MUNIT2,WIDTH,LENGTH,DRAFT,COLOR,QUANT1,QUANT2 ");
            sqlstr.Append(" FROM  " + SrcTableData + " ");

            if (StoreID > 0 && StoreID != 9999)
            {
                //sqlstr.Append("WHERE STOREID=" + StoreID.ToString());
                wherecl = false;
            }
            if (StoreID != 9999)
            {
                if (!GetZeroQty)
                {
                    if (wherecl) sqlstr.Append(" AND NVL(QUANT1,0) <> 0"); else sqlstr.Append(" WHERE NVL(QUANT1,0) <> 0");
                }
            }
            sqlstr.Append(" ORDER BY LOTID");
            affectedrows = db.DBExecuteSQLCmd(sqlstr.ToString());

            return rtrn = affectedrows.ToString();
        }
             
        public DataSet FgetCurrentInventoryByLot(int StoreID, long StartLotID, long EndLotID)
        {
            StringBuilder sqlstr = new StringBuilder();

            DataSet DT = new DataSet();           
            string tblname = "ZSTORESINVERNTORYSTATUS_";

            if (StoreID > 0)
                tblname += StoreID.ToString();

            sqlstr.Append("SELECT ID,STOREID,BRANCHID,LOTID,LOTCODE,");
            sqlstr.Append("ITEMID,ITEMCODE,ITEMDESC,WIDTH,LENGTH,DRAFT,COLOR,QUANT1,QUANT2 ");
            sqlstr.Append("FROM " + tblname + " ");
            sqlstr.Append("WHERE  STOREID=" + StoreID.ToString() + " ");
            if (StartLotID > 0 && EndLotID > 0)
                sqlstr.Append("AND (ID BETWEEN " + StartLotID.ToString() + " AND " + EndLotID.ToString() +")");
            sqlstr.Append("ORDER BY ID");


            DT = db.DBFillDataset(sqlstr.ToString(),"DSINV");
            return DT;
        }

        public long FClearData(int StoreID)

        {
            long rtrn=0;
            string tblname = "ZSTORESINVERNTORYITEMS_";
            if (StoreID > 0)
                tblname += StoreID.ToString();

            rtrn += db.DBExecuteSQLCmd("DROP TABLE " + tblname);


            tblname = "ZSTORESINVERNTORYLOTS_";
            if (StoreID > 0)
                tblname += StoreID.ToString();

            rtrn += db.DBExecuteSQLCmd("DROP TABLE " + tblname);

            tblname = "ZSTORESINVERNTORYSTATUS_";
            if (StoreID > 0)
                tblname += StoreID.ToString();

            rtrn += db.DBExecuteSQLCmd("DROP  TABLE " + tblname);

            return rtrn;
        }

        #region sync items

        public List<SyncERPItem> FGetListItems(int StoreID, string lastsyncdate)
        {
            StringBuilder sqlstr = new StringBuilder();
            List<SyncERPItem> ListItems = new List<SyncERPItem>();

            DataTable DT = new DataTable();

            string tblname = "ZSTORESINVERNTORYITEMS_";
            Boolean WhereCl = false;

            if (StoreID > 0)
            {
                tblname += StoreID.ToString();
            }

            sqlstr.Append("SELECT DISTINCT ITEMID,COMPID,MUNITPRIMARY,MUNITSECONDARY,ITEMCODE,ITEMDESC,to_char(ENTRYDATE, 'dd/mm/yyyy') as ENTRYDATE FROM " + tblname);

            if (StoreID > 0 && StoreID != 9999)
            {
                //sqlstr.Append(" WHERE STOREID=" + StoreID.ToString());
                WhereCl = false;
            }

            if( !(string.IsNullOrEmpty(lastsyncdate)))
            {
                if (WhereCl) sqlstr.Append(" AND "); else sqlstr.Append(" WHERE ");

                sqlstr.Append(" ENTRYDATE >=  TO_DATE('" + lastsyncdate + "','DD/MM/YYYY')");
            }

            sqlstr.Append(" ORDER BY TO_DATE(ENTRYDATE,'DD/MM/YYYY') ASC ");
            DT = db.DBFillDataTable(sqlstr.ToString(), "DSITEMS");


            for (int i = 0; i < DT.Rows.Count; i++)
            {
                ListItems.Add(ItemParse(DT.Rows[i]));
            }
            
            return ListItems;
        }

        public SyncERPItem FGetItem(long itemid)
        {
            StringBuilder sqlstr = new StringBuilder();

            DataTable DT = new DataTable();

            sqlstr.Append("select id as ITEMID,comid as COMPID,mu1 as MUNITPRIMARY,mu2 as MUNITSECONDARY,code as ITEMCODE,description as ITEMDESC,to_char(ENTRYDATE, 'dd/mm/yyyy') as ENTRYDATE from material WHERE ID = " + itemid.ToString());
            DT = db.DBFillDataTable(sqlstr.ToString(), "DSITEMS");

            return ItemParse(DT.Rows[0]);
        }
         
        public SyncLot FGetLot(long lotid)
        {
            StringBuilder sqlstr = new StringBuilder();
            DataTable DT = new DataTable();


            sqlstr.Append("SELECT COMPID,LOTID,LOTCODE,ITEMID,WIDTH,LENGTH,DRAFT,COLOR,to_char(ZENTRYDATE, 'dd/mm/yyyy') as ZENTRYDATE FROM ZVBRAPOGRAFH WHERE LOTID =" + lotid.ToString());
            DT = db.DBFillDataTable(sqlstr.ToString(), "DSITEMS");

            return FParseLot(DT.Rows[0]);
            
        }

        public SyncLot FGetLotbyCode(string lotcode)
        {
            StringBuilder sqlstr = new StringBuilder();
            DataTable DT = new DataTable();

            if (lotcode.Length > 10) //OTHERLOTS
            {
                sqlstr.Append("SELECT COMPID,LOTID,LOTCODE,ITEMID,WIDTH,LENGTH,DRAFT,COLOR,to_char(ZENTRYDATE, 'dd/mm/yyyy') as ZENTRYDATE FROM ZVBRAPOGRAFH WHERE LOTCODE ='" + lotcode + "'");
            }
            else //THIS IS A BARCODE FROM STORETRADELINES
            {
                sqlstr.Append("SELECT COMPID,LOTID,LOTCODE,ITEMID,WIDTH,LENGTH,DRAFT,COLOR,to_char(ZENTRYDATE, 'dd/mm/yyyy') as ZENTRYDATE FROM ZVSTORETRADELINESDETAILS WHERE STLID = " + lotcode);
            }

           DT = db.DBFillDataTable(sqlstr.ToString(), "DSITEMS");

            return FParseLot(DT.Rows[0]);

        }

        public SyncLot FGetLotbyCodeInventory(string lotcode)
        {
            StringBuilder sqlstr = new StringBuilder();
            DataTable DT = new DataTable();

            sqlstr.Append(" SELECT ");
            sqlstr.Append(" ZVB.COMPID,   ");
            sqlstr.Append(" ZVB.LOTID,   ");
            sqlstr.Append(" ZVB.LOTCODE,   ");
            sqlstr.Append(" ZVB.ITEMID,   ");
            sqlstr.Append(" ZVB.WIDTH,   ");
            sqlstr.Append(" ZVB.LENGTH,   ");
            sqlstr.Append(" ZVB.DRAFT,   ");
            sqlstr.Append(" ZVB.COLOR,");
            sqlstr.Append(" MAT.CODE as ITEMCODE,");
            sqlstr.Append(" MAT.DESCRIPTION as ITEMDESC,");
            sqlstr.Append(" MAT.MU1 AS MUNITPRIMARY,");
            sqlstr.Append(" MAT.MU2 AS MUNITSECONDARY,");
            sqlstr.Append(" MU1.DESCR AS MUNITDESC1,");
            sqlstr.Append(" MU2.DESCR AS MUNITDESC2, ");
            sqlstr.Append(" MAT.ENTRYDATE,");
            sqlstr.Append(" to_char(ZVB.ZENTRYDATE, 'dd/mm/yyyy') as ZENTRYDATE");
            sqlstr.Append(" FROM ZVBRAPOGRAFH ZVB,MATERIAL MAT , MESUNIT MU1, MESUNIT MU2");
            sqlstr.Append(" WHERE ZVB.ITEMID = MAT.ID(+) ");
            sqlstr.Append(" AND MAT.MU1 = MU1.CODEID(+)");
            sqlstr.Append(" AND MAT.MU2 = MU2.CODEID(+)");
            sqlstr.Append(" AND ZVB.LOTCODE ='" + lotcode + "'");

            DT = db.DBFillDataTable(sqlstr.ToString(), "DSLOT");

            return FParseLotInventory(DT.Rows[0]);

        }

        public List<SyncERPItem> FGetListItems(int StoreID, long StartID, long EndID)
        {
            StringBuilder sqlstr = new StringBuilder();
            List<SyncERPItem> ListItems = new List<SyncERPItem>();

            DataTable DT = new DataTable();

            string tblname = "ZSTORESINVERNTORYITEMS_";
            Boolean WhereCl = false;

            if (StoreID > 0)
            {
                tblname += StoreID.ToString();
            }

            sqlstr.Append("SELECT DISTINCT ITEMID,COMPID,MUNITPRIMARY,MUNITSECONDARY,ITEMCODE,ITEMDESC,to_char(ENTRYDATE, 'dd/mm/yyyy') AS ENTRYDATE FROM " + tblname);


            if ((StartID > 0 && EndID > 0) || StoreID > 0)
                sqlstr.Append("  ");

            if (StoreID > 0 && StoreID != 9999)
            {
                //sqlstr.Append(" WHERE STOREID=" + StoreID.ToString());
                WhereCl = false;
            }

            if (StartID > 0 && EndID > 0)
            {
                if (WhereCl) sqlstr.Append(" AND "); else sqlstr.Append(" WHERE ");

                sqlstr.Append(" ID BETWEEN " + StartID.ToString() + " AND " + EndID.ToString());
            }
            //else if (!string.IsNullOrEmpty(lastsyncdate))
            //{
            //    if (WhereCl) sqlstr.Append(" AND "); else sqlstr.Append(" WHERE ");
            //    sqlstr.Append(" ENTRYDATE <= TO_DATE('" + lastsyncdate + "','DD/YY/YYYY'");
            //}

            //sqlstr.Append(" ORDER BY ID");
            DT = db.DBFillDataTable(sqlstr.ToString(), "DSITEMS");


            for (int i = 0; i < DT.Rows.Count; i++)
            {
                ListItems.Add(ItemParse(DT.Rows[i]));
            }

            return ListItems;
        }
        
        public List<SyncLot> FGetListLots(int StoreID, string lastsyncdate)
        {
            StringBuilder sqlstr = new StringBuilder();
            List<SyncLot> ListLots = new List<SyncLot>();

            DataTable DT = new DataTable();

            string tblname = "ZSTORESINVERNTORYLOTS_";
            Boolean WhereCl = false;

            if (StoreID > 0)
            {
                tblname += StoreID.ToString();
            }
            sqlstr.Append("SELECT COMPID,LOTID,LOTCODE,WIDTH,LENGTH,COLOR,DRAFT,ITEMID,to_char(ZENTRYDATE, 'DD/MM/YYYY') as ZENTRYDATE FROM " + tblname);

            if (StoreID > 0 && StoreID != 9999)
            {
               // sqlstr.Append(" WHERE STOREID=" + StoreID.ToString());
                WhereCl = false;
            }

            if (!string.IsNullOrEmpty(lastsyncdate))
            {
                if (WhereCl) sqlstr.Append(" AND "); else sqlstr.Append(" WHERE ");
                sqlstr.Append(" ZENTRYDATE >= TO_DATE('" + lastsyncdate + "','DD/MM/YYYY')");
            }

            sqlstr.Append(" ORDER BY TO_DATE( ZENTRYDATE,'DD-MM-YYYY') ASC ");
            DT = db.DBFillDataTable(sqlstr.ToString(), "DSLOTS");

            for (int i = 0; i < DT.Rows.Count; i++)
            {
                ListLots.Add(FParseLot(DT.Rows[i]));
            }
            
            return ListLots;
        }

        public List<SyncLot> FGetListLots(int StoreID, long StartID, long EndID)
        {
            StringBuilder sqlstr = new StringBuilder();
            List<SyncLot> ListLots = new List<SyncLot>();

            DataTable DT = new DataTable();

            string tblname = "ZSTORESINVERNTORYLOTS_";
            Boolean WhereCl = false;

            if (StoreID > 0)
            {
                tblname += StoreID.ToString();
            }

            sqlstr.Append("SELECT ID,COMPID,LOTID,LOTCODE,ITEMID,WIDTH,LENGTH,DRAFT,COLOR,to_char(ZENTRYDATE, 'dd/mm/yyyy') as ZENTRYDATE FROM " + tblname);

            if ((StartID > 0 && EndID > 0) || StoreID > 0)
                sqlstr.Append("  ");

            if (StoreID > 0 && StoreID != 9999)
            {
                //sqlstr.Append(" WHERE STOREID=" + StoreID.ToString());
                WhereCl = false;
            }


            if (StartID > 0 && EndID > 0)
            {
                if (WhereCl) sqlstr.Append(" AND "); else sqlstr.Append(" WHERE ");

                sqlstr.Append(" ID BETWEEN " + StartID.ToString() + " AND " + EndID.ToString());
            }

            sqlstr.Append(" ORDER BY ID ");


            IDataReader dr = db.DBReturnDatareaderResults(sqlstr.ToString());

            if (dr != null)
            {
                while (dr.Read())
                {
                    SyncLot lot = new SyncLot();

                    if (dr["LOTID"] != DBNull.Value) lot.LotID = long.Parse(dr["LOTID"].ToString());
                    if (dr["ITEMID"] != DBNull.Value) lot.ItemID = long.Parse(dr["ITEMID"].ToString());
                    if (dr["COMPID"] != DBNull.Value) lot.CompID = short.Parse(dr["COMPID"].ToString());
                    if (dr["COLOR"] != DBNull.Value) lot.Color = dr["COLOR"].ToString();
                    if (dr["DRAFT"] != DBNull.Value) lot.Draft = dr["DRAFT"].ToString();
                    if (dr["WIDTH"] != DBNull.Value) lot.Width = decimal.Parse(dr["WIDTH"].ToString());
                    if (dr["LENGTH"] != DBNull.Value) lot.Length = decimal.Parse(dr["LENGTH"].ToString());
                    if (dr["LOTCODE"] != DBNull.Value) lot.LotCode = dr["LOTCODE"].ToString();
                    if (dr["ZENTRYDATE"] != DBNull.Value) lot.EntryDate = dr["ZENTRYDATE"].ToString();
                    ListLots.Add(lot);
                }
            }

            return ListLots;
        }
                
        public SyncERPItem ItemParse(DataRow Dr)
        {
            SyncERPItem item = new SyncERPItem();
            try { item.ItemID = long.Parse(Dr["ITEMID"].ToString()); }
            catch { }
            try { item.CompID = short.Parse(Dr["COMPID"].ToString()); }
            catch { }
            try { item.BranchID = short.Parse(Dr["BRANCHID"].ToString()); }
            catch { }
            try { item.BranchID = short.Parse(Dr["STOREID"].ToString()); }
            catch { }
            try { item.MUnitPrimary = int.Parse(Dr["MUNITPRIMARY"].ToString()); }
            catch { }
            try { item.MUnitSecondary = int.Parse(Dr["MUNITSECONDARY"].ToString()); }
            catch { }
            try { item.ItemCode = Dr["ITEMCODE"].ToString(); }
            catch { }
            try { item.ItemDesc = Dr["ITEMDESC"].ToString().Replace("'","").Replace("\"",""); }
            catch { }
            try { item.MUnitDesc1 = Dr["MUnitDesc1"].ToString(); }
            catch { }
            try { item.MUnitDesc2 = Dr["MUnitDesc2"].ToString(); }
            catch { }
            try { item.EntryDate = Dr["ENTRYDATE"].ToString(); }
            catch { }

            return item;
        }

        private SyncLot FParseLot(DataRow Dr)
        {
            SyncLot lot = new SyncLot();
            try { lot.LotID = long.Parse(Dr["LOTID"].ToString()); }
            catch { }
            try { lot.ItemID = long.Parse(Dr["ITEMID"].ToString()); }
            catch { }
            try { lot.CompID = short.Parse(Dr["COMPID"].ToString()); }
            catch { }
            try { lot.BranchID = short.Parse(Dr["BRANCHID"].ToString()); }
            catch { }
            try { lot.StoreID = short.Parse(Dr["STOREID"].ToString()); }
            catch { }
            try { lot.Color = Dr["COLOR"].ToString(); }
            catch { }
            try { lot.Draft = Dr["DRAFT"].ToString(); }
            catch { }
            try { lot.Width = decimal.Parse(Dr["WIDTH"].ToString()); }
            catch {}
            try { lot.Length = decimal.Parse(Dr["LENGTH"].ToString());}
            catch {}
            try { lot.LotCode = Dr["LOTCODE"].ToString(); }
            catch { }
            try { lot.EntryDate = Dr["ZENTRYDATE"].ToString(); }
            catch { }
           
            return lot;
        }


        private SyncLot FParseLotInventory(DataRow Dr)
        {
            SyncLot lot = new SyncLot();
            try { lot.LotID = long.Parse(Dr["LOTID"].ToString()); }
            catch { }
            try { lot.ItemID = long.Parse(Dr["ITEMID"].ToString()); }
            catch { }
            try { lot.CompID = short.Parse(Dr["COMPID"].ToString()); }
            catch { }
            try { lot.BranchID = short.Parse(Dr["BRANCHID"].ToString()); }
            catch { }
            try { lot.StoreID = short.Parse(Dr["STOREID"].ToString()); }
            catch { }
            try { lot.Color = Dr["COLOR"].ToString(); }
            catch { }
            try { lot.Draft = Dr["DRAFT"].ToString(); }
            catch { }
            try { lot.Width = decimal.Parse(Dr["WIDTH"].ToString()); }
            catch { }
            try { lot.Length = decimal.Parse(Dr["LENGTH"].ToString()); }
            catch { }
            try { lot.LotCode = Dr["LOTCODE"].ToString(); }
            catch { }
            try { lot.EntryDate = Dr["ZENTRYDATE"].ToString(); }
            catch { }
             try { lot.ItemCode = Dr["ITEMCODE"].ToString(); }
            catch { }
            try { lot.ItemDesc = Dr["ITEMDESC"].ToString(); }
            catch { }
             try { lot.MunitPrimary = short.Parse(Dr["MUNITPRIMARY"].ToString()); }
            catch { }
           
             try { lot.MunitSecondary = short.Parse(Dr["MUNITSECONDARY"].ToString()); }
            catch { }

            try { lot.MunitDesc1 = Dr["MUNITDESC1"].ToString(); }
            catch { }

            try { lot.MunitDesc2 = Dr["MUNITDESC2"].ToString(); }
            catch { }

            return lot;
        }       


        #endregion
    }

    public class Orders
    {
        OraDB db = new OraDB();
        public List<TOrderDetails> OrderDetails(long dsrnumber, short compid, short branchid)
        {
            List<TOrderDetails> orderdetails = new List<TOrderDetails>();
            if (dsrnumber > 0)
            {
                try { orderdetails = OrderDetailsList(" fintrade.comid =" + compid.ToString() + " AND fintrade.braid =" + branchid.ToString() + "AND STO.FTRID=" + dsrnumber.ToString(), " STO.ID ASC"); }
                catch { }

             }

            return orderdetails;
        }

        public List<TOrderDetails> OrderDetailsList(string searchcriteria, string sortcriteria)
        {


                 List<TOrderDetails> orderdetailslist = new List<TOrderDetails>();
                string sqlstr = "SELECT * FROM STORETRADELINES ";


                sqlstr = "select STO.ID as ORDERDTLID,STO.ITEID as ITEMID,";
                sqlstr += "STO.FTRID as  ORDERID,mat.code as ITEMCODE,";
                sqlstr += "MAT.description as ITEMDESC";
                sqlstr += ",fintrade.cusid as CUSTOMERID,";
                sqlstr += "fintrade.ftrdate as ORDERDATE,MAT.MU1 as MUNITPRIMARY,MAT.MU2 as MUNITSECONDARY,";
                sqlstr +="fintrade.dsrnumber as ORDERCODE,";
                sqlstr += "customer.name as CUSTOMERTITLE,";
                sqlstr +="sto.qty as ITEMQTYPRIMARY,";
                sqlstr +="sto.secondaryqty as ItemQtySecondary ";
                sqlstr +="from storetradelines STO,MATERIAL MAT,fintrade,customer ";
                sqlstr +="where STO.ITEID = mat.id  AND ";
                sqlstr +="fintrade.id = STO.ftrid AND ";
                sqlstr +="customer.id = fintrade.cusid AND ";

                             
                 

            if (!string.IsNullOrEmpty(searchcriteria))
            {
                sqlstr += searchcriteria;
            }

            if (!string.IsNullOrEmpty(sortcriteria))
            {
                sqlstr += " ORDER BY " + sortcriteria;
            }
            else
                sqlstr += " ORDER BY ID";


            IDataReader dr = db.DBReturnDatareaderResults(sqlstr);
            int i = 0;
            if (dr != null)
            {
                while (dr.Read())
                {
                    TOrderDetails orderdetails = new TOrderDetails();

                    orderdetails.id = ++i;
                    if (dr["ORDERDTLID"] != DBNull.Value) orderdetails.orderdtlid = long.Parse(dr["ORDERDTLID"].ToString());
                    if (dr["ITEMCODE"] != DBNull.Value) orderdetails.itemcode = dr["ITEMCODE"].ToString();
                    if (dr["ITEMDESC"] != DBNull.Value) orderdetails.itemdesc = dr["ITEMDESC"].ToString();
                    if (dr["ITEMQTYPRIMARY"] != DBNull.Value) orderdetails.itemqtyprimary = decimal.Parse(dr["ITEMQTYPRIMARY"].ToString());
                    if (dr["ORDERID"] != DBNull.Value) orderdetails.orderid = long.Parse(dr["ORDERID"].ToString());
                    if (dr["ORDERCODE"] != DBNull.Value) orderdetails.ordercode = dr["ORDERCODE"].ToString();
                    if (dr["CUSTOMERID"] != DBNull.Value) orderdetails.customerid = long.Parse(dr["CUSTOMERID"].ToString());
                    if (dr["CUSTOMERTITLE"] != DBNull.Value) orderdetails.customertitle = dr["CUSTOMERTITLE"].ToString();
                    //if (dr["COLORDESC"] != DBNull.Value) orderdetails.colordesc = dr["COLORDESC"].ToString();
                    //if (dr["ORDERDTLCOMMENTS"] != DBNull.Value) orderdetails.orderdtlcomments = dr["ORDERDTLCOMMENTS"].ToString();
                    if (dr["ORDERDATE"] != DBNull.Value) orderdetails.orderdate = dr["ORDERDATE"].ToString();

                    
                    if (dr["ITEMID"] != DBNull.Value) orderdetails.itemid = long.Parse(dr["ITEMID"].ToString());
                    if (dr["MUNITPRIMARY"] != DBNull.Value) orderdetails.munitprimary = short.Parse(dr["MUNITPRIMARY"].ToString());
                    if (dr["MUNITSECONDARY"] != DBNull.Value) orderdetails.munitsecondary = short.Parse(dr["MUNITSECONDARY"].ToString());

                    if (dr["ITEMQTYSECONDARY"] != DBNull.Value) orderdetails.itemqtysecondary = long.Parse(dr["ITEMQTYSECONDARY"].ToString());

                    if (!string.IsNullOrEmpty(orderdetails.orderdate))
                    {
                        string shrtdate = db.DBShortDatetoString(orderdetails.orderdate);
                        if (!string.IsNullOrEmpty(shrtdate)) orderdetails.orderdate = shrtdate;
                    }

                    orderdetailslist.Add(orderdetails);
                    orderdetails = null;
                }
            }

            db.DBDisConnect();
            return orderdetailslist;



        }

        public long UpdateOrderLinesStatus(long orderid)
        {
            DataTable dt = new DataTable();
            StringBuilder sqlstr = new StringBuilder();

            long orderdtlid;
            decimal orderrequestedpqty = 0, orderrequestedsqty = 0;
            decimal orderdonepqty = 0, orderdonesqty = 0;
            decimal ordershippedpqty = 0, ordershippedsqty = 0;
            decimal orderbalancepqty = 0, orderbalancesqty = 0;
            decimal ordershipbalancepqty = 0, ordershipbalancesqty = 0;

            short orderdtlstatus, neworderdtlstatus;

            short munitbackorder = 1;
            short munitprimary = 0, munitsecondary = 0;
            long ordersdtlupd = 0, rtrn = 0; ;


            sqlstr.Append("SELECT ORDERID,ORDERDTLID,ORDERDTLSTATUS,MUNITBACKORDER,MUNITPRIMARY,MUNITSECONDARY,");
            sqlstr.Append("ITEMREQUESTEDPQTY,ITEMREQUESTEDSQTY,");
            sqlstr.Append("ITEMDONEPQTY,ITEMDONESQTY,SHIPPEDPQTY,SHIPPEDSQTY,");
            sqlstr.Append("ITEMPQTYBALANCE,ITEMSQTYBALANCE,ITEMPQTYSHIPPBALANCE,ITEMSQTYSHIPPBALANCE");
            sqlstr.Append(" FROM VSALESORDERSCHECKSTATUS WHERE ORDERID=" + orderid.ToString());

            dt = db.DBFillDataTable(sqlstr.ToString(), "DTOrderStatus");


            foreach (DataRow dr in dt.Rows)
            {
                orderdtlid = long.Parse(dr["ORDERDTLID"].ToString());

                if (dr["ITEMREQUESTEDPQTY"] != DBNull.Value) orderrequestedpqty = decimal.Parse(dr["ITEMREQUESTEDPQTY"].ToString());
                if (dr["ITEMREQUESTEDSQTY"] != DBNull.Value) orderrequestedsqty = decimal.Parse(dr["ITEMREQUESTEDSQTY"].ToString());

                if (dr["ITEMDONEPQTY"] != DBNull.Value) orderdonepqty = decimal.Parse(dr["ITEMDONEPQTY"].ToString());
                if (dr["ITEMDONESQTY"] != DBNull.Value) orderdonesqty = decimal.Parse(dr["ITEMDONESQTY"].ToString());

                if (dr["SHIPPEDPQTY"] != DBNull.Value) ordershippedpqty = decimal.Parse(dr["SHIPPEDPQTY"].ToString());
                if (dr["SHIPPEDSQTY"] != DBNull.Value) ordershippedsqty = decimal.Parse(dr["SHIPPEDSQTY"].ToString());

                if (dr["ITEMPQTYBALANCE"] != DBNull.Value) orderbalancepqty = decimal.Parse(dr["ITEMPQTYBALANCE"].ToString());
                if (dr["ITEMSQTYBALANCE"] != DBNull.Value) orderbalancesqty = decimal.Parse(dr["ITEMSQTYBALANCE"].ToString());


                if (dr["ITEMPQTYSHIPPBALANCE"] != DBNull.Value) ordershipbalancepqty = decimal.Parse(dr["ITEMPQTYSHIPPBALANCE"].ToString());
                if (dr["ITEMSQTYSHIPPBALANCE"] != DBNull.Value) ordershipbalancesqty = decimal.Parse(dr["ITEMSQTYSHIPPBALANCE"].ToString());

                if (dr["MUNITPRIMARY"] != DBNull.Value) munitprimary = short.Parse(dr["MUNITPRIMARY"].ToString());
                if (dr["MUNITSECONDARY"] != DBNull.Value) munitsecondary = short.Parse(dr["MUNITSECONDARY"].ToString());
                if (dr["MUNITBACKORDER"] != DBNull.Value) munitbackorder = short.Parse(dr["MUNITBACKORDER"].ToString());

                orderdtlstatus = short.Parse(dr["ORDERDTLSTATUS"].ToString());

                if (munitbackorder == 1 || munitbackorder == munitprimary)
                {
                    if (ordershippedpqty >= orderrequestedpqty)
                        neworderdtlstatus = OrderStatusVariants.Shipped;
                    else if ((ordershippedpqty + orderdonepqty) >= orderrequestedpqty)
                        neworderdtlstatus = OrderStatusVariants.Done;
                    else if ((ordershippedpqty + orderdonepqty) > 0)
                        neworderdtlstatus = OrderStatusVariants.PartiallyDone;
                    else
                        neworderdtlstatus = OrderStatusVariants.Undone;
                }
                else
                {
                    if (ordershipbalancesqty >= orderrequestedsqty)
                        neworderdtlstatus = OrderStatusVariants.Shipped;
                    else if (orderbalancesqty >= orderrequestedsqty)
                        neworderdtlstatus = OrderStatusVariants.Done;
                    else if ((ordershippedsqty + orderdonesqty) > 0)
                        neworderdtlstatus = OrderStatusVariants.PartiallyDone;
                    else
                        neworderdtlstatus = OrderStatusVariants.Undone;
                }

                if (neworderdtlstatus != orderdtlstatus)
                {
                    rtrn = db.DBExecuteSQLCmd("UPDATE TORDERDETAILS SET ORDERDTLSTATUS=" + neworderdtlstatus.ToString() + " WHERE ORDERDTLID=" + orderdtlid.ToString());
                    if (rtrn > 0) ordersdtlupd++;
                }

            }

            if (ordersdtlupd > 0)
                UpdateOrderStatus(orderid);

            return ordersdtlupd;
        }

        public long UpdateOrderStatus(long oprderid)
        {
            short minstatus, maxstatus, neworderstatus;
            long rtrn = 0;

            DataTable dt = db.DBFillDataTable("SELECT MIN(ORDERDTLSTATUS) AS MINSTATUS,MAX(ORDERDTLSTATUS) AS MAXSTATUS FROM TORDERDETAILS WHERE ORDERID=" + oprderid.ToString(), "dtorderstatus");

            if (dt.Rows.Count > 0)
            {
                minstatus = short.Parse(dt.Rows[0]["MINSTATUS"].ToString());
                maxstatus = short.Parse(dt.Rows[0]["MAXSTATUS"].ToString());


                if (minstatus == maxstatus)
                    neworderstatus = minstatus;
                else if (minstatus < OrderStatusVariants.PartiallyDone && maxstatus > OrderStatusVariants.Undone)
                    neworderstatus = OrderStatusVariants.PartiallyDone;
                else
                    neworderstatus = minstatus;




                rtrn = db.DBExecuteSQLCmd("UPDATE TORDERS SET ORDERSTATUS=" + neworderstatus.ToString() + " WHERE ORDERID=" + oprderid.ToString());
            }

            return rtrn;

        }
    }

    public class Customer
    {


        public long erpCustomerID { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerTitle { get; set; }
        public string Occupation { get; set; }
        public string STREET1 { get; set; }
        public string ZIPCODE1 { get; set; }
        public string CITY1 { get; set; }
        public string phone1 { get; set; }
        public string phone2 { get; set; }
        public string FAX1 { get; set; }
        public string ZALPISCODE { get; set; }
    }


    public class ReceivesController
    {
        OraDB db = new OraDB();
        Log mylog = new Log();
        public TransCodeHeader GetTransCodeHeader(long ftrid)
        {

            StringBuilder sqlstr = new StringBuilder();
            

            sqlstr.Append(" SELECT FINTRADE.ID,");
            sqlstr.Append(" TO_CHAR(FINTRADE.FTRDATE,'DD/MM/YYYY') AS FTRDATE,");
            sqlstr.Append(" FINTRADE.BRAID,");
            sqlstr.Append(" FINTRADE.TRADECODE,");
            sqlstr.Append(" STORETRADE.STOID,");
            sqlstr.Append(" STORETRADE.SECSTOID");
            sqlstr.Append(" FROM  FINTRADE,STORETRADE");
            sqlstr.Append(" WHERE FINTRADE.ID = STORETRADE.FTRID");
            sqlstr.Append(" AND  FINTRADE.ID = " + ftrid.ToString());

            TransCodeHeader t = new TransCodeHeader();
            IDataReader dr = db.DBReturnDatareaderResults(sqlstr.ToString());
            if (dr != null)
            {
                while (dr.Read())
                {
                    if (dr["ID"] != DBNull.Value) t.FtrID = long.Parse(dr["ID"].ToString());
                    if (dr["FTRDATE"] != DBNull.Value) t.transdate = dr["FTRDATE"].ToString();
                    if (dr["BRAID"] != DBNull.Value) t.branchid = short.Parse(dr["BRAID"].ToString());
                    if (dr["TRADECODE"] != DBNull.Value) t.tradecode = dr["TRADECODE"].ToString();
                    if (dr["STOID"] != DBNull.Value) t.fromstoreid = int.Parse(dr["STOID"].ToString());
                    if (dr["SECSTOID"] != DBNull.Value) t.tostoreid = int.Parse(dr["SECSTOID"].ToString());
                }




            }

            db.DBDisConnect();

            return t;

        }


        public List<TransCodeDetail> GetTransDetails(long ftrid)
        {
            StringBuilder sqlstr = new StringBuilder();
            List<TransCodeDetail> ldtl = new List<TransCodeDetail>();
            try
            {

                sqlstr.Append(" SELECT ");
                sqlstr.Append(" STORETRADELINES.FTRID AS FTRID,");
                sqlstr.Append(" STORETRADELINES.ITEID AS ITEMID,");
                sqlstr.Append(" MATERIAL.CODE AS ITEMCODE,");
                sqlstr.Append(" MATERIAL.DESCRIPTION AS ITEMDESC,");
                sqlstr.Append(" PARTITION.ZBARCODE AS LOTCODE,");
                sqlstr.Append(" STORETRADELINES.ZWIDTH,");
                sqlstr.Append(" STORETRADELINES.ZLENGTH,");
                sqlstr.Append(" NVL(STORETRADELINES.ZCOLOR,STORETRADELINES.ZCOLOR2) AS ZCOLOR,");
                sqlstr.Append(" STORETRADELINES.ZDRAFT,");
                sqlstr.Append(" MATERIAL.MU1,");
                sqlstr.Append(" MATERIAL.MU2,");
                sqlstr.Append(" CASE WHEN NVL(STORETRADELINES.SECONDARYQTY, 0) = 0 THEN STORETRADELINES.QTY ELSE STORETRADELINES.SECONDARYQTY END AS QTY,");
                //  sqlstr.Append(" STORETRADELINES.SECONDARYQTY, ");
                sqlstr.Append(" STORETRADELINES.PARID AS LOTID");
                sqlstr.Append(" FROM STORETRADELINES,MATERIAL,PARTITION");
                sqlstr.Append(" WHERE STORETRADELINES.ITEID = MATERIAL.ID");
                sqlstr.Append(" AND STORETRADELINES.PARID = PARTITION.ID(+)");
                sqlstr.Append(" AND STORETRADELINES.FTRID = " + ftrid.ToString());



                IDataReader dr = db.DBReturnDatareaderResults(sqlstr.ToString());
                if (dr != null)
                {
                    while (dr.Read())
                    {
                        TransCodeDetail tdtl = new TransCodeDetail();
                        if (dr["FTRID"] != DBNull.Value) tdtl.FtrID = long.Parse(dr["FTRID"].ToString());
                        if (dr["LOTCODE"] != DBNull.Value) tdtl.lotcode = dr["LOTCODE"].ToString();
                        if (dr["ITEMCODE"] != DBNull.Value) tdtl.itemcode = dr["ITEMCODE"].ToString();
                        if (dr["ITEMDESC"] != DBNull.Value) tdtl.itemdesc = dr["ITEMDESC"].ToString();
                        if (dr["ITEMID"] != DBNull.Value) tdtl.ItemID = long.Parse(dr["ITEMID"].ToString());
                        if (dr["LOTID"] != DBNull.Value) tdtl.LotID = long.Parse(dr["LOTID"].ToString());

                        if (dr["ZWIDTH"] != DBNull.Value) tdtl.Zwidth = decimal.Parse(dr["ZWIDTH"].ToString());
                        if (dr["ZLENGTH"] != DBNull.Value) tdtl.Zlength = decimal.Parse(dr["ZLENGTH"].ToString());

                        if (dr["ZCOLOR"] != DBNull.Value) tdtl.zcolor = dr["ZCOLOR"].ToString();
                        if (dr["ZDRAFT"] != DBNull.Value) tdtl.zdraft = dr["ZDRAFT"].ToString();

                        if (dr["QTY"] != DBNull.Value) tdtl.ItemQtyPrimary = decimal.Parse(dr["QTY"].ToString());
                        //  if (dr["SECONDARYQTY"] != DBNull.Value) tdtl.ItemQtySecondary = decimal.Parse(dr["SECONDARYQTY"].ToString());

                        if (dr["MU1"] != DBNull.Value) tdtl.munitprimary = int.Parse(dr["MU1"].ToString());
                        if (dr["MU2"] != DBNull.Value) tdtl.munitsecondary = int.Parse(dr["MU2"].ToString());

                        mylog.WriteToLog("tdtl.Zwidth = {tdtl.Zwidth} = " + tdtl.Zwidth.ToString());

                        ldtl.Add(tdtl);
                    }




                }

            }
            catch (Exception ex) 
            {
                mylog.WriteToLog(ex.Message);
            
            }

            db.DBDisConnect();

            return ldtl;

        }


    }



}
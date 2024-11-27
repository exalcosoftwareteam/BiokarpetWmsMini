using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.IO;

namespace AtlantisInventorySync
{
    public class AtlantisInventoryController
    {
        ERPItem item;
        ItemDetailsQTY dqty;
        ERPLot lot;
        StringBuilder sqlstr = new StringBuilder();

        MSSQLdb mssqldb;
        oradb odb;
        public AtlantisInventoryController() 
        {
             odb = new oradb(ConfigurationManager.AppSettings["oraconstring"].ToString());
             mssqldb = new MSSQLdb(ConfigurationManager.AppSettings["mssqlconstring"].ToString());
        

        }


        public long SyncItemsAndLots() 
        {
            //01-01-2023
            //Changed the way we will handle SyncItemsAndLots , we created an agent instead
            return 1;
            ////////////////////////////////////////////////////////////////////////////

            mssqldb.DBExecuteSQLCmd(" DELETE FROM TITEMS ");
            mssqldb.DBExecuteSQLCmd(" DELETE FROM TITEMLOT ");
            long cnt = 0;

            odb.oraDR = odb.resulsAsDataReader(" SELECT  DISTINCT COMPID,IGPID,ICTID,ITEMID,ITEMCODE,MUNITRELATION,ITEMDESC,MUNITPRIMARY,MUNITSECONDARY,TO_CHAR(NVL(ENTRYDATE,'01-01-2009'),'DD/MM/YYYY') as ENTRYDATE,ZSGBDESCR,ZCOMPOSITION FROM ZVBRAPOGRAFH ");

            while (odb.oraDR.Read())
            {
                item = new ERPItem();
                if (odb.oraDR["COMPID"] != DBNull.Value) item.CompID = short.Parse(odb.oraDR["COMPID"].ToString());
                if (odb.oraDR["ITEMID"] != DBNull.Value) item.ItemID = long.Parse(odb.oraDR["ITEMID"].ToString());
                if (odb.oraDR["IGPID"] != DBNull.Value) item.igpid = long.Parse(odb.oraDR["IGPID"].ToString());
                if (odb.oraDR["ICTID"] != DBNull.Value) item.ictid = long.Parse(odb.oraDR["ICTID"].ToString());
                if (odb.oraDR["ITEMCODE"] != DBNull.Value) item.ItemCode = odb.oraDR["ITEMCODE"].ToString().Replace("'", "|");
                if (odb.oraDR["ITEMDESC"] != DBNull.Value) item.ItemDesc = odb.oraDR["ITEMDESC"].ToString().Replace("'", "|");
                if (odb.oraDR["MUNITPRIMARY"] != DBNull.Value) item.MUnitPrimary = int.Parse(odb.oraDR["MUNITPRIMARY"].ToString());
                if (odb.oraDR["MUNITSECONDARY"] != DBNull.Value) item.MUnitSecondary = int.Parse(odb.oraDR["MUNITSECONDARY"].ToString());
                if (odb.oraDR["MUNITRELATION"] != DBNull.Value) item.MUnitsRelation = decimal.Parse(odb.oraDR["MUNITRELATION"].ToString());
                if (odb.oraDR["ENTRYDATE"] != DBNull.Value) item.EntryDate = odb.oraDR["ENTRYDATE"].ToString();

                if (odb.oraDR["ZSGBDESCR"] != DBNull.Value) item.zsgbdescr = odb.oraDR["ZSGBDESCR"].ToString().Replace("'", "|");
                if (odb.oraDR["ZCOMPOSITION"] != DBNull.Value) item.zcomposition = odb.oraDR["ZCOMPOSITION"].ToString().Replace("'", "|");


                FInsertItem(item);

            }

            odb.oraDR.Close();


            odb.oraDR = odb.resulsAsDataReader("SELECT  DISTINCT COMPID,LOTID,LOTCODE,ITEMID,WIDTH,LENGTH,DRAFT,COLOR,TO_CHAR(ZENTRYDATE,'DD/MM/YYYY') AS ZENTRYDATE,ZSETDESCR,ZDESCRIPTION,ZBARCODE2 FROM ZVBRAPOGRAFH ");
            while (odb.oraDR.Read())
            {

                lot = new ERPLot();
                if (odb.oraDR["COMPID"] != DBNull.Value) lot.CompID = short.Parse(odb.oraDR["COMPID"].ToString());
                if (odb.oraDR["LOTID"] != DBNull.Value) lot.LotID = long.Parse(odb.oraDR["LOTID"].ToString());
                if (odb.oraDR["ITEMID"] != DBNull.Value) lot.ItemID = long.Parse(odb.oraDR["ITEMID"].ToString());
                if (odb.oraDR["LOTCODE"] != DBNull.Value) lot.LotCode = odb.oraDR["LOTCODE"].ToString().Replace("'", "|");
                if (odb.oraDR["WIDTH"] != DBNull.Value) lot.Width = decimal.Parse(odb.oraDR["WIDTH"].ToString());
                if (odb.oraDR["LENGTH"] != DBNull.Value) lot.Length = decimal.Parse(odb.oraDR["LENGTH"].ToString());
                if (odb.oraDR["DRAFT"] != DBNull.Value) lot.Draft = odb.oraDR["DRAFT"].ToString().Replace("'", "|");
                if (odb.oraDR["COLOR"] != DBNull.Value) lot.Color = odb.oraDR["COLOR"].ToString().Replace("'", "|");
                if (odb.oraDR["ZENTRYDATE"] != DBNull.Value) lot.EntryDate = odb.oraDR["ZENTRYDATE"].ToString();

                if (odb.oraDR["ZSETDESCR"] != DBNull.Value) lot.zsetdescr = odb.oraDR["ZSETDESCR"].ToString();
                if (odb.oraDR["ZBARCODE2"] != DBNull.Value) lot.zbarcode2 = odb.oraDR["ZBARCODE2"].ToString();
                if (odb.oraDR["ZDESCRIPTION"] != DBNull.Value) lot.zdescription = odb.oraDR["ZDESCRIPTION"].ToString();

                cnt += FInsertLot(lot);
            }
            odb.oraDR.Close();
            return cnt;
        }

        public long TESTGetDetailsQtyFromWmsMiniDB(short branchid, short storeid, short comid)
        {

            long cnt = 0;

            List<ItemDetailsQTY> lqty = new List<ItemDetailsQTY>();

            mssqldb.mssqlDR = mssqldb.returnAsDataReader(" SELECT * FROM VIMPORT100");


            while (mssqldb.mssqlDR.Read())
            {
                dqty = new ItemDetailsQTY();
                try
                {
                    if (mssqldb.mssqlDR["ITEID"] != DBNull.Value) dqty.iteid = long.Parse(mssqldb.mssqlDR["ITEID"].ToString());
                    if (mssqldb.mssqlDR["PARID"] != DBNull.Value) dqty.parid = long.Parse(mssqldb.mssqlDR["PARID"].ToString());
                    if (mssqldb.mssqlDR["QTYINSTOCK1"] != DBNull.Value) dqty.qtyinstock1 = decimal.Parse(mssqldb.mssqlDR["QTYINSTOCK1"].ToString());
                    if (mssqldb.mssqlDR["QTYINSTOCK2"] != DBNull.Value) dqty.qtyinstock2 = decimal.Parse(mssqldb.mssqlDR["QTYINSTOCK2"].ToString());
                }
                catch (Exception ex) { string a = ex.ToString(); }
                lqty.Add(dqty);

            }
            mssqldb.mssqlDR.Close();

            cnt = 0;
            foreach (ItemDetailsQTY d in lqty)
            {
                decimal oldqty1, oldqty2, currentinitqty1, currentinitqty2, qtydiff1, qtydiff2 , newqty1 , newqty2;
                string barcode = "";
                oldqty1 = odb.resultAsDecimal("SELECT INITQTY1+PRIMARYQTY FROM DETAILITEMQTYS WHERE COMID = 2 AND STOID = 1 AND PARID = " + d.parid.ToString());
                oldqty2 = odb.resultAsDecimal("SELECT INITQTY2+SECONDARYQTY FROM DETAILITEMQTYS WHERE COMID = 2 AND STOID = 1 AND PARID = " + d.parid.ToString());

             //oldqty1 = odb.resultAsDecimal("SELECT INITQTY1+PRIMARYQTY FROM DETAILITEMQTYS WHERE COMID = 2 AND STOID = 1 AND PARID = " + d.parid.ToString());
             //oldqty2 = odb.resultAsDecimal("SELECT INITQTY2+SECONDARYQTY FROM DETAILITEMQTYS WHERE COMID = 2 AND STOID = 1 AND PARID = " + d.parid.ToString());


                currentinitqty1 = odb.resultAsDecimal("SELECT INITQTY1 FROM DETAILITEMQTYS WHERE COMID = 2 AND STOID = 1 AND PARID = " + d.parid.ToString());
                currentinitqty2 = odb.resultAsDecimal("SELECT INITQTY2 FROM DETAILITEMQTYS WHERE COMID = 2 AND STOID = 1 AND PARID = " + d.parid.ToString());

                barcode = odb.resultAsString(" SELECT ZBARCODE FROM PARTITION WHERE ID =" + d.parid.ToString());

                qtydiff1 = d.qtyinstock1 - oldqty1;
                qtydiff2 = d.qtyinstock2 - oldqty2;

                if (qtydiff2 == 0) continue;


                newqty1 = currentinitqty1 + qtydiff1;
                newqty2 = currentinitqty2 + qtydiff2;

                cnt += odb.oraSqlCommandBatch(" UPDATE DETAILITEMQTYS SET ZOLDINITQTY1 =  INITQTY1 " +
                " ,ZOLDINITQTY2 = INITQTY2" +
                " ,ZINVDATE = SYSDATE" +
                " ,INITQTY1 = " + newqty1.ToString().Replace(",", ".") +
                " ,INITQTY2 = " + newqty2.ToString().Replace(",", ".") +
                " WHERE COMID = " + comid.ToString()
                + " AND STOID = " + storeid.ToString()
                + " AND PARID = " + d.parid.ToString());


                cnt++;
            }
            odb.DBDisconnect();
            return cnt;
        }


        public long GetDetailsQtyFromWmsMiniDB(short branchid,short storeid,short comid)
        {

            long cnt = 0;

            List<ItemDetailsQTY> lqty = new List<ItemDetailsQTY>();

            mssqldb.mssqlDR = mssqldb.returnAsDataReader(" SELECT * FROM ATLDETAILITEMQTYS WHERE BRANCHID = " + branchid.ToString());
           
          
            while (mssqldb.mssqlDR.Read())
            {
                dqty = new ItemDetailsQTY();
                try
                {
                    if (mssqldb.mssqlDR["ITEID"] != DBNull.Value) dqty.iteid = long.Parse(mssqldb.mssqlDR["ITEID"].ToString());
                    if (mssqldb.mssqlDR["PARID"] != DBNull.Value) dqty.parid = long.Parse(mssqldb.mssqlDR["PARID"].ToString());
                    if (mssqldb.mssqlDR["INITQTY1"] != DBNull.Value) dqty.initqty1 = decimal.Parse(mssqldb.mssqlDR["INITQTY1"].ToString());
                    if (mssqldb.mssqlDR["INITQTY2"] != DBNull.Value) dqty.initqty2 = decimal.Parse(mssqldb.mssqlDR["INITQTY2"].ToString());
                    if (mssqldb.mssqlDR["PRIMARYQTY"] != DBNull.Value) dqty.primaryqty = decimal.Parse(mssqldb.mssqlDR["PRIMARYQTY"].ToString());
                    if (mssqldb.mssqlDR["SECONDARYQTY"] != DBNull.Value) dqty.secondaryqty = decimal.Parse(mssqldb.mssqlDR["SECONDARYQTY"].ToString());
                    if (mssqldb.mssqlDR["QTYINSTOCK1"] != DBNull.Value) dqty.qtyinstock1 = decimal.Parse(mssqldb.mssqlDR["QTYINSTOCK1"].ToString());
                    if (mssqldb.mssqlDR["QTYINSTOCK2"] != DBNull.Value) dqty.qtyinstock2 = decimal.Parse(mssqldb.mssqlDR["QTYINSTOCK2"].ToString());
                }
                catch (Exception ex) { string a = ex.ToString(); }
                lqty.Add(dqty);

            }
            mssqldb.mssqlDR.Close();


            foreach (ItemDetailsQTY d in lqty) 
            {

                cnt += odb.oraSqlCommandBatch(" UPDATE DETAILITEMQTYS SET INITQTY1 = " +
                dqty.initqty1.ToString().Replace(",",".")+
                " ,INITQTY2 = "+dqty.initqty2.ToString().Replace(",",".")+
                " ,ZOLDINITQTY1 = NULL"+
                " ,ZOLDINITQTY2 = NULL"+
                " ,ZINVDATE = NULL" +
                " WHERE COMID = "+comid.ToString()
                +" AND STOID = "+storeid.ToString()
                +" AND PARID = "+d.parid.ToString());
            
            
            
            }
            odb.DBDisconnect();
            return cnt;
        }

        public void WriteToLog(string message)
        {
            string applicationfolfer = Directory.GetCurrentDirectory();
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

        public long GetDetailsQtyNow(int branchid, bool forcedelete)
        {

            long cnt = 0;


            if (forcedelete) mssqldb.DBExecuteSQLCmd(" DELETE FROM ATLDETAILITEMQTYSNOW WHERE BRANCHID =  " + branchid.ToString());
            if (mssqldb.DBGetNumResultFromSQLSelect(" SELECT COUNT(*) FROM ATLDETAILITEMQTYSNOW WHERE BRANCHID =  " + branchid.ToString()) > 0) { return 1; }
            string erpfilter = mssqldb.DBGetStrResultFromSQLSelect(" SELECT ERPINVENTORYFILTER FROM TBRANCHES WHERE BRANCHID =  " + branchid.ToString());
 

            sqlstr.Length = 0;
            sqlstr.Append(" SELECT STORE.BRAID, ");
            sqlstr.Append(" DETAILITEMQTYS.ITEID,");
            sqlstr.Append(" DETAILITEMQTYS.PARID,");
            sqlstr.Append(" ROUND(DETAILITEMQTYS.INITQTY1,4) AS INITQTY1,");
            sqlstr.Append(" ROUND(DETAILITEMQTYS.INITQTY2,4) AS INITQTY2,");
            sqlstr.Append(" ROUND(DETAILITEMQTYS.PRIMARYQTY,4) AS PRIMARYQTY,");
            sqlstr.Append(" ROUND(DETAILITEMQTYS.SECONDARYQTY,4) AS SECONDARYQTY,");
            sqlstr.Append(" ROUND(DETAILITEMQTYS.INITQTY1 + DETAILITEMQTYS.PRIMARYQTY,4) AS QTYINSTOCK1,");
            sqlstr.Append(" ROUND(DETAILITEMQTYS.INITQTY2 + DETAILITEMQTYS.SECONDARYQTY,4) AS QTYINSTOCK2");
            sqlstr.Append(" FROM DETAILITEMQTYS,STORE,MATERIAL,PARTITION");
            sqlstr.Append(" WHERE DETAILITEMQTYS.ITEID = MATERIAL.ID");
            sqlstr.Append(" AND DETAILITEMQTYS.STOID = STORE.CODEID");
            sqlstr.Append(" AND DETAILITEMQTYS.PARID = PARTITION.ID");
            sqlstr.Append(" AND MATERIAL.ISACTIVE = 1 ");
            sqlstr.Append(" AND ((ROUND((DETAILITEMQTYS.INITQTY1 + DETAILITEMQTYS.PRIMARYQTY),2) <> 0) OR (ROUND((DETAILITEMQTYS.INITQTY2 + DETAILITEMQTYS.SECONDARYQTY),2) <> 0))");
            sqlstr.Append(" AND (((DETAILITEMQTYS.INITQTY1 + DETAILITEMQTYS.PRIMARYQTY) <> 0) OR ((DETAILITEMQTYS.INITQTY2 + DETAILITEMQTYS.SECONDARYQTY) <> 0)) ");
            sqlstr.Append(" AND MATERIAL.IGPID NOT IN (16,17,86) ");
            sqlstr.Append(" AND PARTITION.PURCHABLE = 1 ");
            sqlstr.Append(" AND PARTITION.SALEABLE = 1 ");
            sqlstr.Append(" AND STORE.COMID = 2");

            if (branchid >= 100)
            {
                sqlstr.Append(" AND STORE.BRAID = 1 AND STORE.CODEID = 1"); //ΚΕΝΤΡΙΚΟ
                if (erpfilter.Length > 5) { sqlstr.Append(" AND " + erpfilter); }

            }
            else if (branchid == 16)
            {
                sqlstr.Append(" AND STORE.BRAID = 16 AND STORE.CODEID = 18 ");//ΑΡΤΕΜΙΔΟΣ (ΜΟΝΟ ΤΟ ΥΠΟΚ ΟΧΙ ΤΟ ΚΑΘΑΡΙΣΤΗΡΙΟ

            }
            else
            {
                sqlstr.Append(" AND STORE.BRAID = " + branchid.ToString());


            }
          





            //sqlstr.Append(" AND (DETAILITEMQTYS.INITQTY1  > 0 OR  DETAILITEMQTYS.PRIMARYQTY > 0)");

            odb.oraDR = odb.resulsAsDataReader(sqlstr.ToString());
            while (odb.oraDR.Read())
            {
                dqty = new ItemDetailsQTY();
                try
                {
                    if (odb.oraDR["BRAID"] != DBNull.Value) dqty.branchid = short.Parse(branchid.ToString());
                    if (odb.oraDR["ITEID"] != DBNull.Value) dqty.iteid = long.Parse(odb.oraDR["ITEID"].ToString());
                    if (odb.oraDR["PARID"] != DBNull.Value) dqty.parid = long.Parse(odb.oraDR["PARID"].ToString());
                    if (odb.oraDR["INITQTY1"] != DBNull.Value) dqty.initqty1 = decimal.Parse(odb.oraDR["INITQTY1"].ToString());
                    if (odb.oraDR["INITQTY2"] != DBNull.Value) dqty.initqty2 = decimal.Parse(odb.oraDR["INITQTY2"].ToString());
                    if (odb.oraDR["PRIMARYQTY"] != DBNull.Value) dqty.primaryqty = decimal.Parse(odb.oraDR["PRIMARYQTY"].ToString());
                    if (odb.oraDR["SECONDARYQTY"] != DBNull.Value) dqty.secondaryqty = decimal.Parse(odb.oraDR["SECONDARYQTY"].ToString());
                    if (odb.oraDR["QTYINSTOCK1"] != DBNull.Value) dqty.qtyinstock1 = decimal.Parse(odb.oraDR["QTYINSTOCK1"].ToString());
                    if (odb.oraDR["QTYINSTOCK2"] != DBNull.Value) dqty.qtyinstock2 = decimal.Parse(odb.oraDR["QTYINSTOCK2"].ToString());
                }
                catch (Exception ex) { WriteToLog(ex.ToString()); }
                cnt += FInsertDetailQTYNow(dqty);

            }
            odb.oraDR.Close();
            return cnt;


        }


        public long GetDetailsQty(int branchid,bool forcedelete) 
        {
            //01-01-2023
            //Changed the way we will handle snapshot qtys creation , we created an agent instead
            return 1;
            // // // // // // // // // // // // // // // // // // // // // // // // // //
 
            long cnt = 0;


          if (forcedelete) mssqldb.DBExecuteSQLCmd(" DELETE FROM ATLDETAILITEMQTYS WHERE BRANCHID =  " + branchid.ToString());



          if (mssqldb.DBGetNumResultFromSQLSelect(" SELECT COUNT(*) FROM ATLDETAILITEMQTYS WHERE BRANCHID =  " + branchid.ToString()) > 0) { return 1; }
 


           



            string erpfilter = mssqldb.DBGetStrResultFromSQLSelect(" SELECT ERPINVENTORYFILTER FROM TBRANCHES WHERE BRANCHID =  " + branchid.ToString());


           sqlstr.Length = 0;
           sqlstr.Append(" SELECT STORE.BRAID, ");
           sqlstr.Append(" DETAILITEMQTYS.ITEID,");
           sqlstr.Append(" DETAILITEMQTYS.PARID,");
           sqlstr.Append(" ROUND(NVL(DETAILITEMQTYS.INITQTY1,0),4) AS INITQTY1,");
           sqlstr.Append(" ROUND(NVL(DETAILITEMQTYS.INITQTY2,0),4) AS INITQTY2,");
           sqlstr.Append(" ROUND(NVL(DETAILITEMQTYS.PRIMARYQTY,0),4) AS PRIMARYQTY,");
           sqlstr.Append(" ROUND(NVL(DETAILITEMQTYS.SECONDARYQTY,0),4) AS SECONDARYQTY,");
           sqlstr.Append(" ROUND(NVL(DETAILITEMQTYS.INITQTY1,0) + NVL(DETAILITEMQTYS.PRIMARYQTY,0),4) AS QTYINSTOCK1,");
           sqlstr.Append(" ROUND(NVL(DETAILITEMQTYS.INITQTY2,0) + NVL(DETAILITEMQTYS.SECONDARYQTY,0),4) AS QTYINSTOCK2");
           sqlstr.Append(" FROM DETAILITEMQTYS,STORE,MATERIAL,PARTITION");
           sqlstr.Append(" WHERE DETAILITEMQTYS.ITEID = MATERIAL.ID");
           sqlstr.Append(" AND DETAILITEMQTYS.STOID = STORE.CODEID");
           sqlstr.Append(" AND DETAILITEMQTYS.PARID = PARTITION.ID");
           sqlstr.Append(" AND MATERIAL.ISACTIVE = 1 ");
           sqlstr.Append(" AND ((ROUND((NVL(DETAILITEMQTYS.INITQTY1,0) + NVL(DETAILITEMQTYS.PRIMARYQTY,0)),2) <> 0) OR (ROUND((NVL(DETAILITEMQTYS.INITQTY2,0) + NVL(DETAILITEMQTYS.SECONDARYQTY,0)),2) <> 0))");
           //sqlstr.Append(" AND (((DETAILITEMQTYS.INITQTY1 + DETAILITEMQTYS.PRIMARYQTY) <> 0) OR ((DETAILITEMQTYS.INITQTY2 + DETAILITEMQTYS.SECONDARYQTY) <> 0)) ");
           sqlstr.Append(" AND MATERIAL.IGPID NOT IN (16,17) ");
           sqlstr.Append(" AND NVL(MATERIAL.ICTID,0) NOT IN (86) ");
           sqlstr.Append(" AND PARTITION.PURCHABLE = 1 ");
           sqlstr.Append(" AND PARTITION.SALEABLE = 1 "); 
           sqlstr.Append(" AND STORE.COMID = 2");

           if (branchid >= 100)
           {
               sqlstr.Append(" AND STORE.BRAID = 1 AND STORE.CODEID = 1"); //ΚΕΝΤΡΙΚΟ
               if (erpfilter.Length > 5) { sqlstr.Append(" AND (" + erpfilter+")"); }
               
           }
           else if (branchid == 16)
           {
               sqlstr.Append(" AND STORE.BRAID = 16 AND STORE.CODEID = 18 ");//ΑΡΤΕΜΙΔΟΣ (ΜΟΝΟ ΤΟ ΥΠΟΚ ΟΧΙ ΤΟ ΚΑΘΑΡΙΣΤΗΡΙΟ

           }
           else 
           {
               sqlstr.Append(" AND STORE.BRAID = " + branchid.ToString());
           
           
           }





           //sqlstr.Append(" AND (DETAILITEMQTYS.INITQTY1  > 0 OR  DETAILITEMQTYS.PRIMARYQTY > 0)");

           odb.oraDR = odb.resulsAsDataReader(sqlstr.ToString());
           while (odb.oraDR.Read())
           {
               dqty = new ItemDetailsQTY();
               try
               {
                   if (odb.oraDR["BRAID"] != DBNull.Value) dqty.branchid = short.Parse(branchid.ToString());
                   if (odb.oraDR["ITEID"] != DBNull.Value) dqty.iteid = long.Parse(odb.oraDR["ITEID"].ToString());
                   if (odb.oraDR["PARID"] != DBNull.Value) dqty.parid = long.Parse(odb.oraDR["PARID"].ToString());
                   if (odb.oraDR["INITQTY1"] != DBNull.Value) dqty.initqty1 = decimal.Parse(odb.oraDR["INITQTY1"].ToString());
                   if (odb.oraDR["INITQTY2"] != DBNull.Value) dqty.initqty2 = decimal.Parse(odb.oraDR["INITQTY2"].ToString());
                   if (odb.oraDR["PRIMARYQTY"] != DBNull.Value) dqty.primaryqty = decimal.Parse(odb.oraDR["PRIMARYQTY"].ToString());
                   if (odb.oraDR["SECONDARYQTY"] != DBNull.Value) dqty.secondaryqty = decimal.Parse(odb.oraDR["SECONDARYQTY"].ToString());
                   if (odb.oraDR["QTYINSTOCK1"] != DBNull.Value) dqty.qtyinstock1 = decimal.Parse(odb.oraDR["QTYINSTOCK1"].ToString());
                   if (odb.oraDR["QTYINSTOCK2"] != DBNull.Value) dqty.qtyinstock2 = decimal.Parse(odb.oraDR["QTYINSTOCK2"].ToString());
               }
               catch (Exception ex) { WriteToLog(ex.ToString()); }
               cnt += FInsertDetailQTY(dqty);

           }
           odb.oraDR.Close();
           return cnt;
        
        
        }

        public long FInsertDetailQTY(ItemDetailsQTY dqty)
        {
            sqlstr.Length = 0;
            sqlstr.Append("INSERT INTO ATLDETAILITEMQTYS  (branchid,");
            sqlstr.Append(" iteid,");
            sqlstr.Append(" parid,");
            sqlstr.Append(" initqty1,");
            sqlstr.Append(" initqty2,");
            sqlstr.Append(" primaryqty,");
            sqlstr.Append(" secondaryqty,");
            sqlstr.Append(" qtyinstock1,");
            sqlstr.Append(" qtyinstock2) VALUES (");
            if (dqty.branchid > 0) sqlstr.Append(dqty.branchid.ToString() + ","); else sqlstr.Append("0,");
            if (dqty.iteid > 0) sqlstr.Append(dqty.iteid.ToString() + ","); else sqlstr.Append("0,");
            if (dqty.parid > 0) sqlstr.Append(dqty.parid.ToString() + ","); else sqlstr.Append("0,");
            if (dqty.initqty1 != null) sqlstr.Append(dqty.initqty1.ToString().Replace(",", ".") + ","); else sqlstr.Append("0,");
            if (dqty.initqty2 != null) sqlstr.Append(dqty.initqty2.ToString().Replace(",", ".") + ","); else sqlstr.Append("0,");
            if (dqty.primaryqty != null) sqlstr.Append(dqty.primaryqty.ToString().Replace(",", ".") + ","); else sqlstr.Append("0,");
            if (dqty.secondaryqty != null) sqlstr.Append(dqty.secondaryqty.ToString().Replace(",", ".") + ","); else sqlstr.Append("0,");
            if (dqty.qtyinstock1 != null) sqlstr.Append(dqty.qtyinstock1.ToString().Replace(",", ".") + ","); else sqlstr.Append("0,");
            if (dqty.qtyinstock2 != null) sqlstr.Append(dqty.qtyinstock2.ToString().Replace(",", ".")); else sqlstr.Append("0");
            sqlstr.Append(")");
            return mssqldb.DBExecuteSQLCmdBatch(sqlstr.ToString());

        }

        public long FInsertDetailQTYNow(ItemDetailsQTY dqty)
        {
            sqlstr.Length = 0;
            sqlstr.Append("INSERT INTO ATLDETAILITEMQTYSNOW  (branchid,");
            sqlstr.Append(" iteid,");
            sqlstr.Append(" parid,");
            sqlstr.Append(" initqty1,");
            sqlstr.Append(" initqty2,");
            sqlstr.Append(" primaryqty,");
            sqlstr.Append(" secondaryqty,");
            sqlstr.Append(" qtyinstock1,");
            sqlstr.Append(" qtyinstock2) VALUES (");
            if (dqty.branchid > 0) sqlstr.Append(dqty.branchid.ToString() + ","); else sqlstr.Append("0,");
            if (dqty.iteid > 0) sqlstr.Append(dqty.iteid.ToString() + ","); else sqlstr.Append("0,");
            if (dqty.parid > 0) sqlstr.Append(dqty.parid.ToString() + ","); else sqlstr.Append("0,");
            if (dqty.initqty1 != null) sqlstr.Append(dqty.initqty1.ToString().Replace(",", ".") + ","); else sqlstr.Append("0,");
            if (dqty.initqty2 != null) sqlstr.Append(dqty.initqty2.ToString().Replace(",", ".") + ","); else sqlstr.Append("0,");
            if (dqty.primaryqty != null) sqlstr.Append(dqty.primaryqty.ToString().Replace(",", ".") + ","); else sqlstr.Append("0,");
            if (dqty.secondaryqty != null) sqlstr.Append(dqty.secondaryqty.ToString().Replace(",", ".") + ","); else sqlstr.Append("0,");
            if (dqty.qtyinstock1 != null) sqlstr.Append(dqty.qtyinstock1.ToString().Replace(",", ".") + ","); else sqlstr.Append("0,");
            if (dqty.qtyinstock2 != null) sqlstr.Append(dqty.qtyinstock2.ToString().Replace(",", ".")); else sqlstr.Append("0");
            sqlstr.Append(")");
            return mssqldb.DBExecuteSQLCmdBatch(sqlstr.ToString());

        }


 
        public long FInsertItem(ERPItem item)
        {
            sqlstr.Length = 0;

            if (string.IsNullOrEmpty(item.EntryDate)) item.EntryDate = DateTime.Today.ToString("dd/MM/yyyy");

            sqlstr.Append("INSERT INTO TItems  (ItemID,Igpid,IctID,CompID, ItemCode, ItemDesc, MUnitPrimary,Transid, MUnitSecondary,MUnitsRelation,ENTRYDATE,ZSGBDESCR,ZCOMPOSITION) VALUES     ( ");
            if (item.ItemID > 0) sqlstr.Append(item.ItemID.ToString() + ","); else sqlstr.Append("NULL,");
            if (item.igpid > 0) sqlstr.Append(item.igpid.ToString() + ","); else sqlstr.Append("NULL,");
            if (item.ictid > 0) sqlstr.Append(item.ictid.ToString() + ","); else sqlstr.Append("NULL,");
            if (item.CompID > 0) sqlstr.Append(item.CompID.ToString() + ","); else sqlstr.Append("2,");
            if (!string.IsNullOrEmpty(item.ItemCode)) sqlstr.Append("'" + item.ItemCode + "',"); else sqlstr.Append("NULL,");
            if (!string.IsNullOrEmpty(item.ItemDesc)) sqlstr.Append("'" + item.ItemDesc + "',"); else sqlstr.Append("NULL,");
            if (item.MUnitPrimary > 0) sqlstr.Append(item.MUnitPrimary.ToString() + ",99,"); else sqlstr.Append("NULL,99,");
            if (item.MUnitSecondary > 0) sqlstr.Append(item.MUnitSecondary.ToString() + ","); else sqlstr.Append("NULL,");
            if (item.MUnitsRelation > 0) sqlstr.Append(item.MUnitsRelation.ToString().Replace(",",".") + ","); else sqlstr.Append("NULL,");

            if (!string.IsNullOrEmpty(item.EntryDate))
            {
                item.EntryDate = item.EntryDate.Replace("πμ", "am");
                item.EntryDate = item.EntryDate.Replace("μμ", "pm");
                sqlstr.Append("CONVERT(DATETIME,'" + item.EntryDate + "',103),");
            }
            else
            {
                sqlstr.Append("NULL,");
            }
            if (!string.IsNullOrEmpty(item.zsgbdescr)) sqlstr.Append("'" + item.zsgbdescr.Replace("'", "|") + "',"); else sqlstr.Append("NULL,");
            if (!string.IsNullOrEmpty(item.zcomposition)) sqlstr.Append("'" + item.zcomposition.Replace("'", "|") + "'"); else sqlstr.Append("NULL");
            sqlstr.Append(")");

           return  mssqldb.DBExecuteSQLCmdBatch(sqlstr.ToString());
 
        }

        public long FInsertLot(ERPLot lot)
        {
            sqlstr.Length = 0;

            if (string.IsNullOrEmpty(lot.EntryDate)) lot.EntryDate = DateTime.Today.ToString("dd/MM/yyyy");


            sqlstr.Append("INSERT INTO TItemLot  (LOTID, compid, ItemID, LOTCode,Width,Length,ItemPrimaryQty,ItemSecondaryQty,Color,Transid,Draft,ENTRYDATE,ZSETDECR,ZDESCRIPTION,ZBARCODE2) VALUES     ( ");
            if (lot.LotID > 0) sqlstr.Append(lot.LotID.ToString() + ","); else sqlstr.Append("NULL,");
            if (lot.CompID > 0) sqlstr.Append(lot.CompID.ToString() + ","); else sqlstr.Append("2,");
            if (lot.ItemID > 0) sqlstr.Append(lot.ItemID.ToString() + ","); else sqlstr.Append("NULL,");
            if (!string.IsNullOrEmpty(lot.LotCode)) sqlstr.Append("'" + lot.LotCode + "',"); else sqlstr.Append("NULL,");
            if (lot.Width > 0) sqlstr.Append(lot.Width.ToString().Replace(",", ".") + ","); else sqlstr.Append("NULL,");
            if (lot.Length > 0) sqlstr.Append(lot.Length.ToString().Replace(",", ".") + ","); else sqlstr.Append("NULL,");
            if (lot.ItemPrimaryQty > 0) sqlstr.Append(lot.ItemPrimaryQty.ToString().Replace(",", ".") + ","); else sqlstr.Append("NULL,");
            if (lot.ItemSecondaryQty > 0) sqlstr.Append(lot.ItemSecondaryQty.ToString().Replace(",", ".") + ","); else sqlstr.Append("NULL,");
            if (!string.IsNullOrEmpty(lot.Color)) sqlstr.Append("'" + lot.Color.Replace("'", "|") + "',99,"); else sqlstr.Append("NULL,99,");
            if (!string.IsNullOrEmpty(lot.Draft)) sqlstr.Append("'" + lot.Draft.Replace("'", "|") + "',"); else sqlstr.Append("NULL,");
            if (!string.IsNullOrEmpty(lot.EntryDate)) sqlstr.Append("CONVERT(DATETIME,'" + lot.EntryDate + "',103),");else sqlstr.Append("NULL,");
            if (!string.IsNullOrEmpty(lot.zsetdescr)) sqlstr.Append("'" + lot.zsetdescr.Replace("'", "|") + "',"); else sqlstr.Append("NULL,");
            if (!string.IsNullOrEmpty(lot.zdescription)) sqlstr.Append("'" + lot.zdescription.Replace("'", "|") + "',"); else sqlstr.Append("NULL,");
            if (!string.IsNullOrEmpty(lot.zbarcode2)) sqlstr.Append("'" + lot.zbarcode2.Replace("'", "|") + "'"); else sqlstr.Append("NULL");

            sqlstr.Append(")");
            return mssqldb.DBExecuteSQLCmdBatch(sqlstr.ToString());
        }

    }






}

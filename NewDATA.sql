insert into WMSminiBIOKARPET.dbo.Titems(ItemID ,CompID,ItemCode,ItemDesc,MUnitPrimary,MUnitSecondary,MUnitsRelation,TransID,ENTRYDATE)
SELECT ItemID ,CompID,ItemCode,ItemDesc,MUnitPrimary,MUnitSecondary,MUnitsRelation,TransID,ENTRYDATE from WMSminiBIOKARPET_old.dbo.Titems
where WMSminiBIOKARPET_old.dbo.Titems.ItemID = 23272

INSERT INTO WMSminiBIOKARPET.dbo.TitemLOT(LOTID,compid,ItemID,LOTCode,TransID,ENTRYDATE)
select LOTID,compid,ItemID,LOTCode,TransID,ENTRYDATE from WMSminiBIOKARPET_old.dbo.TitemLot
where [WMSminiBIOKARPET_old].[dbo].[TItemLot].ItemID = 2372

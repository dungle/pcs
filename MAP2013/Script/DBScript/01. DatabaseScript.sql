-- DatabaseScript.sql 
-- Created by: Dung.Le

---------------------------------RELEASE PART----------------------------------------------

IF NOT EXISTS (SELECT ParamID FROM sys_Param WHERE Name = 'DBVersion')
 INSERT INTO sys_Param (Name, Value) VALUES('DBVersion', '28-Sep-05')
UPDATE sys_Param SET Value = '05-Oct-2010' WHERE Name = 'DBVersion'
go

---------------------------------DATABASE PART----------------------------------------------

-- drop some not used table from database
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__ITM_Cost__CCNID__65570293]') AND parent_object_id = OBJECT_ID(N'[dbo].[ITM_Cost]'))
ALTER TABLE [dbo].[ITM_Cost] DROP CONSTRAINT [FK__ITM_Cost__CCNID__65570293]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__ITM_Cost__Produc__01D345B0]') AND parent_object_id = OBJECT_ID(N'[dbo].[ITM_Cost]'))
ALTER TABLE [dbo].[ITM_Cost] DROP CONSTRAINT [FK__ITM_Cost__Produc__01D345B0]
GO

/****** Object:  Table [dbo].[ITM_Cost]    Script Date: 06/05/2010 06:20:10 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ITM_Cost]') AND type in (N'U'))
DROP TABLE [dbo].[ITM_Cost]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__ITM_CostC__CCNID__60FC61CA]') AND parent_object_id = OBJECT_ID(N'[dbo].[ITM_CostCenterRate]'))
ALTER TABLE [dbo].[ITM_CostCenterRate] DROP CONSTRAINT [FK__ITM_CostC__CCNID__60FC61CA]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__ITM_CostC__CostC__61F08603]') AND parent_object_id = OBJECT_ID(N'[dbo].[ITM_CostCenterRate]'))
ALTER TABLE [dbo].[ITM_CostCenterRate] DROP CONSTRAINT [FK__ITM_CostC__CostC__61F08603]
GO

/****** Object:  Table [dbo].[ITM_CostCenterRate]    Script Date: 06/05/2010 06:17:56 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ITM_CostCenterRate]') AND type in (N'U'))
DROP TABLE [dbo].[ITM_CostCenterRate]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__ITM_CostD__CCNID__62E4AA3C]') AND parent_object_id = OBJECT_ID(N'[dbo].[ITM_CostDescription]'))
ALTER TABLE [dbo].[ITM_CostDescription] DROP CONSTRAINT [FK__ITM_CostD__CCNID__62E4AA3C]
GO

/****** Object:  Table [dbo].[ITM_CostDescription]    Script Date: 06/05/2010 06:19:28 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ITM_CostDescription]') AND type in (N'U'))
DROP TABLE [dbo].[ITM_CostDescription]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__ITM_CostO__CCNID__18A19C6F]') AND parent_object_id = OBJECT_ID(N'[dbo].[ITM_CostOperation]'))
ALTER TABLE [dbo].[ITM_CostOperation] DROP CONSTRAINT [FK__ITM_CostO__CCNID__18A19C6F]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__ITM_CostO__Produ__05A3D694]') AND parent_object_id = OBJECT_ID(N'[dbo].[ITM_CostOperation]'))
ALTER TABLE [dbo].[ITM_CostOperation] DROP CONSTRAINT [FK__ITM_CostO__Produ__05A3D694]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__ITM_CostO__Routi__1A89E4E1]') AND parent_object_id = OBJECT_ID(N'[dbo].[ITM_CostOperation]'))
ALTER TABLE [dbo].[ITM_CostOperation] DROP CONSTRAINT [FK__ITM_CostO__Routi__1A89E4E1]
GO

/****** Object:  Table [dbo].[ITM_CostOperation]    Script Date: 06/05/2010 05:43:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ITM_CostOperation]') AND type in (N'U'))
DROP TABLE [dbo].[ITM_CostOperation]
GO
	
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__IV_DockTo__Accep__4707859D]') AND parent_object_id = OBJECT_ID(N'[dbo].[IV_DockToStock]'))
ALTER TABLE [dbo].[IV_DockToStock] DROP CONSTRAINT [FK__IV_DockTo__Accep__4707859D]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__IV_DockTo__CCNID__47FBA9D6]') AND parent_object_id = OBJECT_ID(N'[dbo].[IV_DockToStock]'))
ALTER TABLE [dbo].[IV_DockToStock] DROP CONSTRAINT [FK__IV_DockTo__CCNID__47FBA9D6]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__IV_DockTo__Maste__48EFCE0F]') AND parent_object_id = OBJECT_ID(N'[dbo].[IV_DockToStock]'))
ALTER TABLE [dbo].[IV_DockToStock] DROP CONSTRAINT [FK__IV_DockTo__Maste__48EFCE0F]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__IV_DockTo__MfgLi__49E3F248]') AND parent_object_id = OBJECT_ID(N'[dbo].[IV_DockToStock]'))
ALTER TABLE [dbo].[IV_DockToStock] DROP CONSTRAINT [FK__IV_DockTo__MfgLi__49E3F248]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__IV_DockTo__MfgNo__4AD81681]') AND parent_object_id = OBJECT_ID(N'[dbo].[IV_DockToStock]'))
ALTER TABLE [dbo].[IV_DockToStock] DROP CONSTRAINT [FK__IV_DockTo__MfgNo__4AD81681]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__IV_DockTo__MfgNo__4BCC3ABA]') AND parent_object_id = OBJECT_ID(N'[dbo].[IV_DockToStock]'))
ALTER TABLE [dbo].[IV_DockToStock] DROP CONSTRAINT [FK__IV_DockTo__MfgNo__4BCC3ABA]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__IV_DockTo__MfgSQ__4CC05EF3]') AND parent_object_id = OBJECT_ID(N'[dbo].[IV_DockToStock]'))
ALTER TABLE [dbo].[IV_DockToStock] DROP CONSTRAINT [FK__IV_DockTo__MfgSQ__4CC05EF3]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__IV_DockTo__Packi__4DB4832C]') AND parent_object_id = OBJECT_ID(N'[dbo].[IV_DockToStock]'))
ALTER TABLE [dbo].[IV_DockToStock] DROP CONSTRAINT [FK__IV_DockTo__Packi__4DB4832C]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__IV_DockTo__Packi__4EA8A765]') AND parent_object_id = OBJECT_ID(N'[dbo].[IV_DockToStock]'))
ALTER TABLE [dbo].[IV_DockToStock] DROP CONSTRAINT [FK__IV_DockTo__Packi__4EA8A765]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__IV_DockTo__PurLi__4F9CCB9E]') AND parent_object_id = OBJECT_ID(N'[dbo].[IV_DockToStock]'))
ALTER TABLE [dbo].[IV_DockToStock] DROP CONSTRAINT [FK__IV_DockTo__PurLi__4F9CCB9E]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__IV_DockTo__PurNo__5090EFD7]') AND parent_object_id = OBJECT_ID(N'[dbo].[IV_DockToStock]'))
ALTER TABLE [dbo].[IV_DockToStock] DROP CONSTRAINT [FK__IV_DockTo__PurNo__5090EFD7]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__IV_DockTo__PurNo__51851410]') AND parent_object_id = OBJECT_ID(N'[dbo].[IV_DockToStock]'))
ALTER TABLE [dbo].[IV_DockToStock] DROP CONSTRAINT [FK__IV_DockTo__PurNo__51851410]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__IV_DockTo__PurSQ__52793849]') AND parent_object_id = OBJECT_ID(N'[dbo].[IV_DockToStock]'))
ALTER TABLE [dbo].[IV_DockToStock] DROP CONSTRAINT [FK__IV_DockTo__PurSQ__52793849]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__IV_DockTo__Rejec__536D5C82]') AND parent_object_id = OBJECT_ID(N'[dbo].[IV_DockToStock]'))
ALTER TABLE [dbo].[IV_DockToStock] DROP CONSTRAINT [FK__IV_DockTo__Rejec__536D5C82]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__IV_DockTo__Rewor__546180BB]') AND parent_object_id = OBJECT_ID(N'[dbo].[IV_DockToStock]'))
ALTER TABLE [dbo].[IV_DockToStock] DROP CONSTRAINT [FK__IV_DockTo__Rewor__546180BB]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__IV_DockTo__RTVLo__5555A4F4]') AND parent_object_id = OBJECT_ID(N'[dbo].[IV_DockToStock]'))
ALTER TABLE [dbo].[IV_DockToStock] DROP CONSTRAINT [FK__IV_DockTo__RTVLo__5555A4F4]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__IV_DockTo__RTVRe__5649C92D]') AND parent_object_id = OBJECT_ID(N'[dbo].[IV_DockToStock]'))
ALTER TABLE [dbo].[IV_DockToStock] DROP CONSTRAINT [FK__IV_DockTo__RTVRe__5649C92D]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__IV_DockTo__RTVRe__573DED66]') AND parent_object_id = OBJECT_ID(N'[dbo].[IV_DockToStock]'))
ALTER TABLE [dbo].[IV_DockToStock] DROP CONSTRAINT [FK__IV_DockTo__RTVRe__573DED66]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__IV_DockTo__Scrap__5832119F]') AND parent_object_id = OBJECT_ID(N'[dbo].[IV_DockToStock]'))
ALTER TABLE [dbo].[IV_DockToStock] DROP CONSTRAINT [FK__IV_DockTo__Scrap__5832119F]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__IV_DockTo__UseAs__592635D8]') AND parent_object_id = OBJECT_ID(N'[dbo].[IV_DockToStock]'))
ALTER TABLE [dbo].[IV_DockToStock] DROP CONSTRAINT [FK__IV_DockTo__UseAs__592635D8]
GO

/****** Object:  Table [dbo].[IV_DockToStock]    Script Date: 06/05/2010 05:49:20 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[IV_DockToStock]') AND type in (N'U'))
DROP TABLE [dbo].[IV_DockToStock]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__IV_INSRes__Accep__000AF8CF]') AND parent_object_id = OBJECT_ID(N'[dbo].[IV_INSResult]'))
ALTER TABLE [dbo].[IV_INSResult] DROP CONSTRAINT [FK__IV_INSRes__Accep__000AF8CF]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__IV_INSRes__Accep__00FF1D08]') AND parent_object_id = OBJECT_ID(N'[dbo].[IV_INSResult]'))
ALTER TABLE [dbo].[IV_INSResult] DROP CONSTRAINT [FK__IV_INSRes__Accep__00FF1D08]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__IV_INSRes__BinID__01F34141]') AND parent_object_id = OBJECT_ID(N'[dbo].[IV_INSResult]'))
ALTER TABLE [dbo].[IV_INSResult] DROP CONSTRAINT [FK__IV_INSRes__BinID__01F34141]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__IV_INSRes__CCNID__02E7657A]') AND parent_object_id = OBJECT_ID(N'[dbo].[IV_INSResult]'))
ALTER TABLE [dbo].[IV_INSResult] DROP CONSTRAINT [FK__IV_INSRes__CCNID__02E7657A]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__IV_INSRes__Inspe__03DB89B3]') AND parent_object_id = OBJECT_ID(N'[dbo].[IV_INSResult]'))
ALTER TABLE [dbo].[IV_INSResult] DROP CONSTRAINT [FK__IV_INSRes__Inspe__03DB89B3]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__IV_INSRes__InsUM__04CFADEC]') AND parent_object_id = OBJECT_ID(N'[dbo].[IV_INSResult]'))
ALTER TABLE [dbo].[IV_INSResult] DROP CONSTRAINT [FK__IV_INSRes__InsUM__04CFADEC]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__IV_INSRes__Locat__05C3D225]') AND parent_object_id = OBJECT_ID(N'[dbo].[IV_INSResult]'))
ALTER TABLE [dbo].[IV_INSResult] DROP CONSTRAINT [FK__IV_INSRes__Locat__05C3D225]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__IV_INSRes__Maste__06B7F65E]') AND parent_object_id = OBJECT_ID(N'[dbo].[IV_INSResult]'))
ALTER TABLE [dbo].[IV_INSResult] DROP CONSTRAINT [FK__IV_INSRes__Maste__06B7F65E]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__IV_INSRes__Produ__4E3E9311]') AND parent_object_id = OBJECT_ID(N'[dbo].[IV_INSResult]'))
ALTER TABLE [dbo].[IV_INSResult] DROP CONSTRAINT [FK__IV_INSRes__Produ__4E3E9311]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__IV_INSRes__Rejec__08A03ED0]') AND parent_object_id = OBJECT_ID(N'[dbo].[IV_INSResult]'))
ALTER TABLE [dbo].[IV_INSResult] DROP CONSTRAINT [FK__IV_INSRes__Rejec__08A03ED0]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__IV_INSRes__Rejec__09946309]') AND parent_object_id = OBJECT_ID(N'[dbo].[IV_INSResult]'))
ALTER TABLE [dbo].[IV_INSResult] DROP CONSTRAINT [FK__IV_INSRes__Rejec__09946309]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__IV_INSRes__TranT__0A888742]') AND parent_object_id = OBJECT_ID(N'[dbo].[IV_INSResult]'))
ALTER TABLE [dbo].[IV_INSResult] DROP CONSTRAINT [FK__IV_INSRes__TranT__0A888742]
GO

/****** Object:  Table [dbo].[IV_INSResult]    Script Date: 06/05/2010 05:50:18 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[IV_INSResult]') AND type in (N'U'))
DROP TABLE [dbo].[IV_INSResult]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__IV_ItemDe__BinID__0B7CAB7B]') AND parent_object_id = OBJECT_ID(N'[dbo].[IV_ItemDetail]'))
ALTER TABLE [dbo].[IV_ItemDetail] DROP CONSTRAINT [FK__IV_ItemDe__BinID__0B7CAB7B]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__IV_ItemDe__CCNID__0C70CFB4]') AND parent_object_id = OBJECT_ID(N'[dbo].[IV_ItemDetail]'))
ALTER TABLE [dbo].[IV_ItemDetail] DROP CONSTRAINT [FK__IV_ItemDe__CCNID__0C70CFB4]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__IV_ItemDe__Locat__0D64F3ED]') AND parent_object_id = OBJECT_ID(N'[dbo].[IV_ItemDetail]'))
ALTER TABLE [dbo].[IV_ItemDetail] DROP CONSTRAINT [FK__IV_ItemDe__Locat__0D64F3ED]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__IV_ItemDe__Maste__0E591826]') AND parent_object_id = OBJECT_ID(N'[dbo].[IV_ItemDetail]'))
ALTER TABLE [dbo].[IV_ItemDetail] DROP CONSTRAINT [FK__IV_ItemDe__Maste__0E591826]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__IV_ItemDe__Produ__54EB90A0]') AND parent_object_id = OBJECT_ID(N'[dbo].[IV_ItemDetail]'))
ALTER TABLE [dbo].[IV_ItemDetail] DROP CONSTRAINT [FK__IV_ItemDe__Produ__54EB90A0]
GO

/****** Object:  Table [dbo].[IV_ItemDetail]    Script Date: 06/05/2010 05:50:51 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[IV_ItemDetail]') AND type in (N'U'))
DROP TABLE [dbo].[IV_ItemDetail]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__IV_ItemSe__BinID__10416098]') AND parent_object_id = OBJECT_ID(N'[dbo].[IV_ItemSerial]'))
ALTER TABLE [dbo].[IV_ItemSerial] DROP CONSTRAINT [FK__IV_ItemSe__BinID__10416098]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__IV_ItemSe__CCNID__113584D1]') AND parent_object_id = OBJECT_ID(N'[dbo].[IV_ItemSerial]'))
ALTER TABLE [dbo].[IV_ItemSerial] DROP CONSTRAINT [FK__IV_ItemSe__CCNID__113584D1]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__IV_ItemSe__Locat__1229A90A]') AND parent_object_id = OBJECT_ID(N'[dbo].[IV_ItemSerial]'))
ALTER TABLE [dbo].[IV_ItemSerial] DROP CONSTRAINT [FK__IV_ItemSe__Locat__1229A90A]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__IV_ItemSe__Maste__131DCD43]') AND parent_object_id = OBJECT_ID(N'[dbo].[IV_ItemSerial]'))
ALTER TABLE [dbo].[IV_ItemSerial] DROP CONSTRAINT [FK__IV_ItemSe__Maste__131DCD43]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__IV_ItemSe__Produ__5AA469F6]') AND parent_object_id = OBJECT_ID(N'[dbo].[IV_ItemSerial]'))
ALTER TABLE [dbo].[IV_ItemSerial] DROP CONSTRAINT [FK__IV_ItemSe__Produ__5AA469F6]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__IV_ItemSe__TranT__150615B5]') AND parent_object_id = OBJECT_ID(N'[dbo].[IV_ItemSerial]'))
ALTER TABLE [dbo].[IV_ItemSerial] DROP CONSTRAINT [FK__IV_ItemSe__TranT__150615B5]
GO

/****** Object:  Table [dbo].[IV_ItemSerial]    Script Date: 06/05/2010 05:51:00 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[IV_ItemSerial]') AND type in (N'U'))
DROP TABLE [dbo].[IV_ItemSerial]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__IV_LocToL__BinID__5DEAEAF5]') AND parent_object_id = OBJECT_ID(N'[dbo].[IV_LocToLocTransfer]'))
ALTER TABLE [dbo].[IV_LocToLocTransfer] DROP CONSTRAINT [FK__IV_LocToL__BinID__5DEAEAF5]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__IV_LocToL__CCNID__5EDF0F2E]') AND parent_object_id = OBJECT_ID(N'[dbo].[IV_LocToLocTransfer]'))
ALTER TABLE [dbo].[IV_LocToLocTransfer] DROP CONSTRAINT [FK__IV_LocToL__CCNID__5EDF0F2E]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__IV_LocToL__Locat__5FD33367]') AND parent_object_id = OBJECT_ID(N'[dbo].[IV_LocToLocTransfer]'))
ALTER TABLE [dbo].[IV_LocToLocTransfer] DROP CONSTRAINT [FK__IV_LocToL__Locat__5FD33367]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__IV_LocToL__Maste__60C757A0]') AND parent_object_id = OBJECT_ID(N'[dbo].[IV_LocToLocTransfer]'))
ALTER TABLE [dbo].[IV_LocToLocTransfer] DROP CONSTRAINT [FK__IV_LocToL__Maste__60C757A0]
GO

/****** Object:  Table [dbo].[IV_LocToLocTransfer]    Script Date: 06/05/2010 05:51:36 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[IV_LocToLocTransfer]') AND type in (N'U'))
DROP TABLE [dbo].[IV_LocToLocTransfer]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__IV_LocToL__LocTo__19CACAD2]') AND parent_object_id = OBJECT_ID(N'[dbo].[IV_LocToLocTransferDetail]'))
ALTER TABLE [dbo].[IV_LocToLocTransferDetail] DROP CONSTRAINT [FK__IV_LocToL__LocTo__19CACAD2]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__IV_LocToL__Produ__62458BBE]') AND parent_object_id = OBJECT_ID(N'[dbo].[IV_LocToLocTransferDetail]'))
ALTER TABLE [dbo].[IV_LocToLocTransferDetail] DROP CONSTRAINT [FK__IV_LocToL__Produ__62458BBE]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__IV_LocToL__Stock__1BB31344]') AND parent_object_id = OBJECT_ID(N'[dbo].[IV_LocToLocTransferDetail]'))
ALTER TABLE [dbo].[IV_LocToLocTransferDetail] DROP CONSTRAINT [FK__IV_LocToL__Stock__1BB31344]
GO

/****** Object:  Table [dbo].[IV_LocToLocTransferDetail]    Script Date: 06/05/2010 05:51:44 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[IV_LocToLocTransferDetail]') AND type in (N'U'))
DROP TABLE [dbo].[IV_LocToLocTransferDetail]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__IV_LocToL__CCNID__61BB7BD9]') AND parent_object_id = OBJECT_ID(N'[dbo].[IV_LocToLocTransferMaster]'))
ALTER TABLE [dbo].[IV_LocToLocTransferMaster] DROP CONSTRAINT [FK__IV_LocToL__CCNID__61BB7BD9]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__IV_LocToL__DesBi__62AFA012]') AND parent_object_id = OBJECT_ID(N'[dbo].[IV_LocToLocTransferMaster]'))
ALTER TABLE [dbo].[IV_LocToLocTransferMaster] DROP CONSTRAINT [FK__IV_LocToL__DesBi__62AFA012]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__IV_LocToL__DesLo__63A3C44B]') AND parent_object_id = OBJECT_ID(N'[dbo].[IV_LocToLocTransferMaster]'))
ALTER TABLE [dbo].[IV_LocToLocTransferMaster] DROP CONSTRAINT [FK__IV_LocToL__DesLo__63A3C44B]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__IV_LocToL__DesMa__6497E884]') AND parent_object_id = OBJECT_ID(N'[dbo].[IV_LocToLocTransferMaster]'))
ALTER TABLE [dbo].[IV_LocToLocTransferMaster] DROP CONSTRAINT [FK__IV_LocToL__DesMa__6497E884]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__IV_LocToL__Sourc__658C0CBD]') AND parent_object_id = OBJECT_ID(N'[dbo].[IV_LocToLocTransferMaster]'))
ALTER TABLE [dbo].[IV_LocToLocTransferMaster] DROP CONSTRAINT [FK__IV_LocToL__Sourc__658C0CBD]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__IV_LocToL__Sourc__668030F6]') AND parent_object_id = OBJECT_ID(N'[dbo].[IV_LocToLocTransferMaster]'))
ALTER TABLE [dbo].[IV_LocToLocTransferMaster] DROP CONSTRAINT [FK__IV_LocToL__Sourc__668030F6]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__IV_LocToL__Sourc__6774552F]') AND parent_object_id = OBJECT_ID(N'[dbo].[IV_LocToLocTransferMaster]'))
ALTER TABLE [dbo].[IV_LocToLocTransferMaster] DROP CONSTRAINT [FK__IV_LocToL__Sourc__6774552F]
GO

/****** Object:  Table [dbo].[IV_LocToLocTransferMaster]    Script Date: 06/05/2010 05:51:52 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[IV_LocToLocTransferMaster]') AND type in (N'U'))
DROP TABLE [dbo].[IV_LocToLocTransferMaster]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__IV_LotFIF__CCNID__1CA7377D]') AND parent_object_id = OBJECT_ID(N'[dbo].[IV_LotFIFO]'))
ALTER TABLE [dbo].[IV_LotFIFO] DROP CONSTRAINT [FK__IV_LotFIF__CCNID__1CA7377D]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__IV_LotFIF__Produ__6ADAD1BF]') AND parent_object_id = OBJECT_ID(N'[dbo].[IV_LotFIFO]'))
ALTER TABLE [dbo].[IV_LotFIFO] DROP CONSTRAINT [FK__IV_LotFIF__Produ__6ADAD1BF]
GO

/****** Object:  Table [dbo].[IV_LotFIFO]    Script Date: 06/05/2010 05:52:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[IV_LotFIFO]') AND type in (N'U'))
DROP TABLE [dbo].[IV_LotFIFO]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__IV_LotIte__CCNID__1E8F7FEF]') AND parent_object_id = OBJECT_ID(N'[dbo].[IV_LotItem]'))
ALTER TABLE [dbo].[IV_LotItem] DROP CONSTRAINT [FK__IV_LotIte__CCNID__1E8F7FEF]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__IV_LotIte__Produ__6BCEF5F8]') AND parent_object_id = OBJECT_ID(N'[dbo].[IV_LotItem]'))
ALTER TABLE [dbo].[IV_LotItem] DROP CONSTRAINT [FK__IV_LotIte__Produ__6BCEF5F8]
GO

/****** Object:  Table [dbo].[IV_LotItem]    Script Date: 06/05/2010 05:52:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[IV_LotItem]') AND type in (N'U'))
DROP TABLE [dbo].[IV_LotItem]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__IV_Materi__BinID__3631FF56]') AND parent_object_id = OBJECT_ID(N'[dbo].[IV_MaterialIssue]'))
ALTER TABLE [dbo].[IV_MaterialIssue] DROP CONSTRAINT [FK__IV_Materi__BinID__3631FF56]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__IV_Materi__CCNID__3726238F]') AND parent_object_id = OBJECT_ID(N'[dbo].[IV_MaterialIssue]'))
ALTER TABLE [dbo].[IV_MaterialIssue] DROP CONSTRAINT [FK__IV_Materi__CCNID__3726238F]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__IV_Materi__Locat__381A47C8]') AND parent_object_id = OBJECT_ID(N'[dbo].[IV_MaterialIssue]'))
ALTER TABLE [dbo].[IV_MaterialIssue] DROP CONSTRAINT [FK__IV_Materi__Locat__381A47C8]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__IV_Materi__Maste__390E6C01]') AND parent_object_id = OBJECT_ID(N'[dbo].[IV_MaterialIssue]'))
ALTER TABLE [dbo].[IV_MaterialIssue] DROP CONSTRAINT [FK__IV_Materi__Maste__390E6C01]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__IV_Materi__Produ__75586032]') AND parent_object_id = OBJECT_ID(N'[dbo].[IV_MaterialIssue]'))
ALTER TABLE [dbo].[IV_MaterialIssue] DROP CONSTRAINT [FK__IV_Materi__Produ__75586032]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__IV_Materi__SaleO__3AF6B473]') AND parent_object_id = OBJECT_ID(N'[dbo].[IV_MaterialIssue]'))
ALTER TABLE [dbo].[IV_MaterialIssue] DROP CONSTRAINT [FK__IV_Materi__SaleO__3AF6B473]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__IV_Materi__SaleO__3BEAD8AC]') AND parent_object_id = OBJECT_ID(N'[dbo].[IV_MaterialIssue]'))
ALTER TABLE [dbo].[IV_MaterialIssue] DROP CONSTRAINT [FK__IV_Materi__SaleO__3BEAD8AC]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__IV_Materi__Stock__3CDEFCE5]') AND parent_object_id = OBJECT_ID(N'[dbo].[IV_MaterialIssue]'))
ALTER TABLE [dbo].[IV_MaterialIssue] DROP CONSTRAINT [FK__IV_Materi__Stock__3CDEFCE5]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__IV_Materi__WorkO__3DD3211E]') AND parent_object_id = OBJECT_ID(N'[dbo].[IV_MaterialIssue]'))
ALTER TABLE [dbo].[IV_MaterialIssue] DROP CONSTRAINT [FK__IV_Materi__WorkO__3DD3211E]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__IV_Materi__WorkO__3EC74557]') AND parent_object_id = OBJECT_ID(N'[dbo].[IV_MaterialIssue]'))
ALTER TABLE [dbo].[IV_MaterialIssue] DROP CONSTRAINT [FK__IV_Materi__WorkO__3EC74557]
GO

/****** Object:  Table [dbo].[IV_MaterialIssue]    Script Date: 06/05/2010 05:53:19 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[IV_MaterialIssue]') AND type in (N'U'))
DROP TABLE [dbo].[IV_MaterialIssue]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__IV_Materi__BinID__3FBB6990]') AND parent_object_id = OBJECT_ID(N'[dbo].[IV_MaterialReceipt]'))
ALTER TABLE [dbo].[IV_MaterialReceipt] DROP CONSTRAINT [FK__IV_Materi__BinID__3FBB6990]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__IV_Materi__CCNID__40AF8DC9]') AND parent_object_id = OBJECT_ID(N'[dbo].[IV_MaterialReceipt]'))
ALTER TABLE [dbo].[IV_MaterialReceipt] DROP CONSTRAINT [FK__IV_Materi__CCNID__40AF8DC9]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__IV_Materi__Locat__41A3B202]') AND parent_object_id = OBJECT_ID(N'[dbo].[IV_MaterialReceipt]'))
ALTER TABLE [dbo].[IV_MaterialReceipt] DROP CONSTRAINT [FK__IV_Materi__Locat__41A3B202]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__IV_Materi__Maste__4297D63B]') AND parent_object_id = OBJECT_ID(N'[dbo].[IV_MaterialReceipt]'))
ALTER TABLE [dbo].[IV_MaterialReceipt] DROP CONSTRAINT [FK__IV_Materi__Maste__4297D63B]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__IV_Materi__Produ__7DEDA633]') AND parent_object_id = OBJECT_ID(N'[dbo].[IV_MaterialReceipt]'))
ALTER TABLE [dbo].[IV_MaterialReceipt] DROP CONSTRAINT [FK__IV_Materi__Produ__7DEDA633]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__IV_Materi__Purch__44801EAD]') AND parent_object_id = OBJECT_ID(N'[dbo].[IV_MaterialReceipt]'))
ALTER TABLE [dbo].[IV_MaterialReceipt] DROP CONSTRAINT [FK__IV_Materi__Purch__44801EAD]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__IV_Materi__Purch__457442E6]') AND parent_object_id = OBJECT_ID(N'[dbo].[IV_MaterialReceipt]'))
ALTER TABLE [dbo].[IV_MaterialReceipt] DROP CONSTRAINT [FK__IV_Materi__Purch__457442E6]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__IV_Materi__Stock__4668671F]') AND parent_object_id = OBJECT_ID(N'[dbo].[IV_MaterialReceipt]'))
ALTER TABLE [dbo].[IV_MaterialReceipt] DROP CONSTRAINT [FK__IV_Materi__Stock__4668671F]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__IV_Materi__WorkO__475C8B58]') AND parent_object_id = OBJECT_ID(N'[dbo].[IV_MaterialReceipt]'))
ALTER TABLE [dbo].[IV_MaterialReceipt] DROP CONSTRAINT [FK__IV_Materi__WorkO__475C8B58]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__IV_Materi__WorkO__4850AF91]') AND parent_object_id = OBJECT_ID(N'[dbo].[IV_MaterialReceipt]'))
ALTER TABLE [dbo].[IV_MaterialReceipt] DROP CONSTRAINT [FK__IV_Materi__WorkO__4850AF91]
GO

/****** Object:  Table [dbo].[IV_MaterialReceipt]    Script Date: 06/05/2010 05:53:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[IV_MaterialReceipt]') AND type in (N'U'))
DROP TABLE [dbo].[IV_MaterialReceipt]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__IV_Materi__BinID__2354350C]') AND parent_object_id = OBJECT_ID(N'[dbo].[IV_MaterialScrap]'))
ALTER TABLE [dbo].[IV_MaterialScrap] DROP CONSTRAINT [FK__IV_Materi__BinID__2354350C]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__IV_Materi__CCNID__24485945]') AND parent_object_id = OBJECT_ID(N'[dbo].[IV_MaterialScrap]'))
ALTER TABLE [dbo].[IV_MaterialScrap] DROP CONSTRAINT [FK__IV_Materi__CCNID__24485945]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__IV_Materi__Locat__253C7D7E]') AND parent_object_id = OBJECT_ID(N'[dbo].[IV_MaterialScrap]'))
ALTER TABLE [dbo].[IV_MaterialScrap] DROP CONSTRAINT [FK__IV_Materi__Locat__253C7D7E]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__IV_Materi__Maste__2630A1B7]') AND parent_object_id = OBJECT_ID(N'[dbo].[IV_MaterialScrap]'))
ALTER TABLE [dbo].[IV_MaterialScrap] DROP CONSTRAINT [FK__IV_Materi__Maste__2630A1B7]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__IV_Materi__Produ__0777106D]') AND parent_object_id = OBJECT_ID(N'[dbo].[IV_MaterialScrap]'))
ALTER TABLE [dbo].[IV_MaterialScrap] DROP CONSTRAINT [FK__IV_Materi__Produ__0777106D]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__IV_Materi__Stock__2818EA29]') AND parent_object_id = OBJECT_ID(N'[dbo].[IV_MaterialScrap]'))
ALTER TABLE [dbo].[IV_MaterialScrap] DROP CONSTRAINT [FK__IV_Materi__Stock__2818EA29]
GO

/****** Object:  Table [dbo].[IV_MaterialScrap]    Script Date: 06/05/2010 05:53:50 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[IV_MaterialScrap]') AND type in (N'U'))
DROP TABLE [dbo].[IV_MaterialScrap]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__IV_MoveSe__CCNID__3E3D3572]') AND parent_object_id = OBJECT_ID(N'[dbo].[IV_MoveSerial]'))
ALTER TABLE [dbo].[IV_MoveSerial] DROP CONSTRAINT [FK__IV_MoveSe__CCNID__3E3D3572]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__IV_MoveSe__Maste__3F3159AB]') AND parent_object_id = OBJECT_ID(N'[dbo].[IV_MoveSerial]'))
ALTER TABLE [dbo].[IV_MoveSerial] DROP CONSTRAINT [FK__IV_MoveSe__Maste__3F3159AB]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__IV_MoveSe__MoveT__40257DE4]') AND parent_object_id = OBJECT_ID(N'[dbo].[IV_MoveSerial]'))
ALTER TABLE [dbo].[IV_MoveSerial] DROP CONSTRAINT [FK__IV_MoveSe__MoveT__40257DE4]
GO

/****** Object:  Table [dbo].[IV_MoveSerial]    Script Date: 06/05/2010 05:54:13 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[IV_MoveSerial]') AND type in (N'U'))
DROP TABLE [dbo].[IV_MoveSerial]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__IV_MoveTi__BinID__70FDBF69]') AND parent_object_id = OBJECT_ID(N'[dbo].[IV_MoveTicket]'))
ALTER TABLE [dbo].[IV_MoveTicket] DROP CONSTRAINT [FK__IV_MoveTi__BinID__70FDBF69]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__IV_MoveTi__CCNID__71F1E3A2]') AND parent_object_id = OBJECT_ID(N'[dbo].[IV_MoveTicket]'))
ALTER TABLE [dbo].[IV_MoveTicket] DROP CONSTRAINT [FK__IV_MoveTi__CCNID__71F1E3A2]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__IV_MoveTi__Locat__72E607DB]') AND parent_object_id = OBJECT_ID(N'[dbo].[IV_MoveTicket]'))
ALTER TABLE [dbo].[IV_MoveTicket] DROP CONSTRAINT [FK__IV_MoveTi__Locat__72E607DB]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__IV_MoveTi__Maste__73DA2C14]') AND parent_object_id = OBJECT_ID(N'[dbo].[IV_MoveTicket]'))
ALTER TABLE [dbo].[IV_MoveTicket] DROP CONSTRAINT [FK__IV_MoveTi__Maste__73DA2C14]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__IV_MoveTi__TranT__74CE504D]') AND parent_object_id = OBJECT_ID(N'[dbo].[IV_MoveTicket]'))
ALTER TABLE [dbo].[IV_MoveTicket] DROP CONSTRAINT [FK__IV_MoveTi__TranT__74CE504D]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__IV_MoveTi__XFRBi__75C27486]') AND parent_object_id = OBJECT_ID(N'[dbo].[IV_MoveTicket]'))
ALTER TABLE [dbo].[IV_MoveTicket] DROP CONSTRAINT [FK__IV_MoveTi__XFRBi__75C27486]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__IV_MoveTi__XFRCC__76B698BF]') AND parent_object_id = OBJECT_ID(N'[dbo].[IV_MoveTicket]'))
ALTER TABLE [dbo].[IV_MoveTicket] DROP CONSTRAINT [FK__IV_MoveTi__XFRCC__76B698BF]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__IV_MoveTi__XFRLo__77AABCF8]') AND parent_object_id = OBJECT_ID(N'[dbo].[IV_MoveTicket]'))
ALTER TABLE [dbo].[IV_MoveTicket] DROP CONSTRAINT [FK__IV_MoveTi__XFRLo__77AABCF8]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__IV_MoveTi__XFRMa__789EE131]') AND parent_object_id = OBJECT_ID(N'[dbo].[IV_MoveTicket]'))
ALTER TABLE [dbo].[IV_MoveTicket] DROP CONSTRAINT [FK__IV_MoveTi__XFRMa__789EE131]
GO

/****** Object:  Table [dbo].[IV_MoveTicket]    Script Date: 06/05/2010 05:54:26 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[IV_MoveTicket]') AND type in (N'U'))
DROP TABLE [dbo].[IV_MoveTicket]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__IV_MoveTo__MoveT__2BE97B0D]') AND parent_object_id = OBJECT_ID(N'[dbo].[IV_MoveToInspectionDetail]'))
ALTER TABLE [dbo].[IV_MoveToInspectionDetail] DROP CONSTRAINT [FK__IV_MoveTo__MoveT__2BE97B0D]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__IV_MoveTo__Produ__14D10B8B]') AND parent_object_id = OBJECT_ID(N'[dbo].[IV_MoveToInspectionDetail]'))
ALTER TABLE [dbo].[IV_MoveToInspectionDetail] DROP CONSTRAINT [FK__IV_MoveTo__Produ__14D10B8B]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__IV_MoveTo__Stock__2DD1C37F]') AND parent_object_id = OBJECT_ID(N'[dbo].[IV_MoveToInspectionDetail]'))
ALTER TABLE [dbo].[IV_MoveToInspectionDetail] DROP CONSTRAINT [FK__IV_MoveTo__Stock__2DD1C37F]
GO

/****** Object:  Table [dbo].[IV_MoveToInspectionDetail]    Script Date: 06/05/2010 05:54:33 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[IV_MoveToInspectionDetail]') AND type in (N'U'))
DROP TABLE [dbo].[IV_MoveToInspectionDetail]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__IV_MoveTo__BinID__7993056A]') AND parent_object_id = OBJECT_ID(N'[dbo].[IV_MoveToInspectionMaster]'))
ALTER TABLE [dbo].[IV_MoveToInspectionMaster] DROP CONSTRAINT [FK__IV_MoveTo__BinID__7993056A]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__IV_MoveTo__Locat__7A8729A3]') AND parent_object_id = OBJECT_ID(N'[dbo].[IV_MoveToInspectionMaster]'))
ALTER TABLE [dbo].[IV_MoveToInspectionMaster] DROP CONSTRAINT [FK__IV_MoveTo__Locat__7A8729A3]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__IV_MoveTo__Maste__7B7B4DDC]') AND parent_object_id = OBJECT_ID(N'[dbo].[IV_MoveToInspectionMaster]'))
ALTER TABLE [dbo].[IV_MoveToInspectionMaster] DROP CONSTRAINT [FK__IV_MoveTo__Maste__7B7B4DDC]
GO

/****** Object:  Table [dbo].[IV_MoveToInspectionMaster]    Script Date: 06/05/2010 05:54:40 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[IV_MoveToInspectionMaster]') AND type in (N'U'))
DROP TABLE [dbo].[IV_MoveToInspectionMaster]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__IV_MRBRes__BinID__2EC5E7B8]') AND parent_object_id = OBJECT_ID(N'[dbo].[IV_MRBResult]'))
ALTER TABLE [dbo].[IV_MRBResult] DROP CONSTRAINT [FK__IV_MRBRes__BinID__2EC5E7B8]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__IV_MRBRes__CCNID__2FBA0BF1]') AND parent_object_id = OBJECT_ID(N'[dbo].[IV_MRBResult]'))
ALTER TABLE [dbo].[IV_MRBResult] DROP CONSTRAINT [FK__IV_MRBRes__CCNID__2FBA0BF1]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__IV_MRBRes__Locat__30AE302A]') AND parent_object_id = OBJECT_ID(N'[dbo].[IV_MRBResult]'))
ALTER TABLE [dbo].[IV_MRBResult] DROP CONSTRAINT [FK__IV_MRBRes__Locat__30AE302A]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__IV_MRBRes__Maste__31A25463]') AND parent_object_id = OBJECT_ID(N'[dbo].[IV_MRBResult]'))
ALTER TABLE [dbo].[IV_MRBResult] DROP CONSTRAINT [FK__IV_MRBRes__Maste__31A25463]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__IV_MRBRes__Produ__28D80438]') AND parent_object_id = OBJECT_ID(N'[dbo].[IV_MRBResult]'))
ALTER TABLE [dbo].[IV_MRBResult] DROP CONSTRAINT [FK__IV_MRBRes__Produ__28D80438]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__IV_MRBRes__Rewor__338A9CD5]') AND parent_object_id = OBJECT_ID(N'[dbo].[IV_MRBResult]'))
ALTER TABLE [dbo].[IV_MRBResult] DROP CONSTRAINT [FK__IV_MRBRes__Rewor__338A9CD5]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__IV_MRBRes__Rewor__347EC10E]') AND parent_object_id = OBJECT_ID(N'[dbo].[IV_MRBResult]'))
ALTER TABLE [dbo].[IV_MRBResult] DROP CONSTRAINT [FK__IV_MRBRes__Rewor__347EC10E]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__IV_MRBRes__Rewor__3572E547]') AND parent_object_id = OBJECT_ID(N'[dbo].[IV_MRBResult]'))
ALTER TABLE [dbo].[IV_MRBResult] DROP CONSTRAINT [FK__IV_MRBRes__Rewor__3572E547]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__IV_MRBRes__Rewor__36670980]') AND parent_object_id = OBJECT_ID(N'[dbo].[IV_MRBResult]'))
ALTER TABLE [dbo].[IV_MRBResult] DROP CONSTRAINT [FK__IV_MRBRes__Rewor__36670980]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__IV_MRBRes__RTVBi__375B2DB9]') AND parent_object_id = OBJECT_ID(N'[dbo].[IV_MRBResult]'))
ALTER TABLE [dbo].[IV_MRBResult] DROP CONSTRAINT [FK__IV_MRBRes__RTVBi__375B2DB9]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__IV_MRBRes__RTVLo__384F51F2]') AND parent_object_id = OBJECT_ID(N'[dbo].[IV_MRBResult]'))
ALTER TABLE [dbo].[IV_MRBResult] DROP CONSTRAINT [FK__IV_MRBRes__RTVLo__384F51F2]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__IV_MRBRes__RTVRe__3943762B]') AND parent_object_id = OBJECT_ID(N'[dbo].[IV_MRBResult]'))
ALTER TABLE [dbo].[IV_MRBResult] DROP CONSTRAINT [FK__IV_MRBRes__RTVRe__3943762B]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__IV_MRBRes__RTVRe__3A379A64]') AND parent_object_id = OBJECT_ID(N'[dbo].[IV_MRBResult]'))
ALTER TABLE [dbo].[IV_MRBResult] DROP CONSTRAINT [FK__IV_MRBRes__RTVRe__3A379A64]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__IV_MRBRes__RTVRe__3B2BBE9D]') AND parent_object_id = OBJECT_ID(N'[dbo].[IV_MRBResult]'))
ALTER TABLE [dbo].[IV_MRBResult] DROP CONSTRAINT [FK__IV_MRBRes__RTVRe__3B2BBE9D]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__IV_MRBRes__RTVRe__3C1FE2D6]') AND parent_object_id = OBJECT_ID(N'[dbo].[IV_MRBResult]'))
ALTER TABLE [dbo].[IV_MRBResult] DROP CONSTRAINT [FK__IV_MRBRes__RTVRe__3C1FE2D6]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__IV_MRBRes__Scrap__3D14070F]') AND parent_object_id = OBJECT_ID(N'[dbo].[IV_MRBResult]'))
ALTER TABLE [dbo].[IV_MRBResult] DROP CONSTRAINT [FK__IV_MRBRes__Scrap__3D14070F]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__IV_MRBRes__Scrap__3E082B48]') AND parent_object_id = OBJECT_ID(N'[dbo].[IV_MRBResult]'))
ALTER TABLE [dbo].[IV_MRBResult] DROP CONSTRAINT [FK__IV_MRBRes__Scrap__3E082B48]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__IV_MRBRes__Stock__3EFC4F81]') AND parent_object_id = OBJECT_ID(N'[dbo].[IV_MRBResult]'))
ALTER TABLE [dbo].[IV_MRBResult] DROP CONSTRAINT [FK__IV_MRBRes__Stock__3EFC4F81]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__IV_MRBRes__UseAs__3FF073BA]') AND parent_object_id = OBJECT_ID(N'[dbo].[IV_MRBResult]'))
ALTER TABLE [dbo].[IV_MRBResult] DROP CONSTRAINT [FK__IV_MRBRes__UseAs__3FF073BA]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__IV_MRBRes__UseAs__40E497F3]') AND parent_object_id = OBJECT_ID(N'[dbo].[IV_MRBResult]'))
ALTER TABLE [dbo].[IV_MRBResult] DROP CONSTRAINT [FK__IV_MRBRes__UseAs__40E497F3]
GO

/****** Object:  Table [dbo].[IV_MRBResult]    Script Date: 06/05/2010 05:54:52 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[IV_MRBResult]') AND type in (N'U'))
DROP TABLE [dbo].[IV_MRBResult]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__PO_Functi__Carri__62458BBE]') AND parent_object_id = OBJECT_ID(N'[dbo].[PO_FunctionVendorReference]'))
ALTER TABLE [dbo].[PO_FunctionVendorReference] DROP CONSTRAINT [FK__PO_Functi__Carri__62458BBE]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__PO_Functi__CCNID__6339AFF7]') AND parent_object_id = OBJECT_ID(N'[dbo].[PO_FunctionVendorReference]'))
ALTER TABLE [dbo].[PO_FunctionVendorReference] DROP CONSTRAINT [FK__PO_Functi__CCNID__6339AFF7]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__PO_Functi__Curre__642DD430]') AND parent_object_id = OBJECT_ID(N'[dbo].[PO_FunctionVendorReference]'))
ALTER TABLE [dbo].[PO_FunctionVendorReference] DROP CONSTRAINT [FK__PO_Functi__Curre__642DD430]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__PO_Functi__Funct__6521F869]') AND parent_object_id = OBJECT_ID(N'[dbo].[PO_FunctionVendorReference]'))
ALTER TABLE [dbo].[PO_FunctionVendorReference] DROP CONSTRAINT [FK__PO_Functi__Funct__6521F869]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__PO_Functi__Vendo__66161CA2]') AND parent_object_id = OBJECT_ID(N'[dbo].[PO_FunctionVendorReference]'))
ALTER TABLE [dbo].[PO_FunctionVendorReference] DROP CONSTRAINT [FK__PO_Functi__Vendo__66161CA2]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__PO_Functi__Vendo__670A40DB]') AND parent_object_id = OBJECT_ID(N'[dbo].[PO_FunctionVendorReference]'))
ALTER TABLE [dbo].[PO_FunctionVendorReference] DROP CONSTRAINT [FK__PO_Functi__Vendo__670A40DB]
GO

/****** Object:  Table [dbo].[PO_FunctionVendorReference]    Script Date: 06/05/2010 05:56:00 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PO_FunctionVendorReference]') AND type in (N'U'))
DROP TABLE [dbo].[PO_FunctionVendorReference]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__PRO_AOScr__AOScr__4DD47EBD]') AND parent_object_id = OBJECT_ID(N'[dbo].[PRO_AOScrapCost]'))
ALTER TABLE [dbo].[PRO_AOScrapCost] DROP CONSTRAINT [FK__PRO_AOScr__AOScr__4DD47EBD]
GO

/****** Object:  Table [dbo].[PRO_AOScrapCost]    Script Date: 06/05/2010 05:56:27 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PRO_AOScrapCost]') AND type in (N'U'))
DROP TABLE [dbo].[PRO_AOScrapCost]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__PRO_AOScr__AOScr__11BF94B6]') AND parent_object_id = OBJECT_ID(N'[dbo].[PRO_AOScrapDetail]'))
ALTER TABLE [dbo].[PRO_AOScrapDetail] DROP CONSTRAINT [FK__PRO_AOScr__AOScr__11BF94B6]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__PRO_AOScr__Produ__5EAA0504]') AND parent_object_id = OBJECT_ID(N'[dbo].[PRO_AOScrapDetail]'))
ALTER TABLE [dbo].[PRO_AOScrapDetail] DROP CONSTRAINT [FK__PRO_AOScr__Produ__5EAA0504]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__PRO_AOScr__Scrap__13A7DD28]') AND parent_object_id = OBJECT_ID(N'[dbo].[PRO_AOScrapDetail]'))
ALTER TABLE [dbo].[PRO_AOScrapDetail] DROP CONSTRAINT [FK__PRO_AOScr__Scrap__13A7DD28]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__PRO_AOScr__Stock__149C0161]') AND parent_object_id = OBJECT_ID(N'[dbo].[PRO_AOScrapDetail]'))
ALTER TABLE [dbo].[PRO_AOScrapDetail] DROP CONSTRAINT [FK__PRO_AOScr__Stock__149C0161]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__PRO_AOScr__WorkO__1590259A]') AND parent_object_id = OBJECT_ID(N'[dbo].[PRO_AOScrapDetail]'))
ALTER TABLE [dbo].[PRO_AOScrapDetail] DROP CONSTRAINT [FK__PRO_AOScr__WorkO__1590259A]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__PRO_AOScr__WorkO__168449D3]') AND parent_object_id = OBJECT_ID(N'[dbo].[PRO_AOScrapDetail]'))
ALTER TABLE [dbo].[PRO_AOScrapDetail] DROP CONSTRAINT [FK__PRO_AOScr__WorkO__168449D3]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__PRO_AOScr__WORou__17786E0C]') AND parent_object_id = OBJECT_ID(N'[dbo].[PRO_AOScrapDetail]'))
ALTER TABLE [dbo].[PRO_AOScrapDetail] DROP CONSTRAINT [FK__PRO_AOScr__WORou__17786E0C]
GO

/****** Object:  Table [dbo].[PRO_AOScrapDetail]    Script Date: 06/05/2010 05:56:39 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PRO_AOScrapDetail]') AND type in (N'U'))
DROP TABLE [dbo].[PRO_AOScrapDetail]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__PRO_AOScr__CCNID__07220AB2]') AND parent_object_id = OBJECT_ID(N'[dbo].[PRO_AOScrapMaster]'))
ALTER TABLE [dbo].[PRO_AOScrapMaster] DROP CONSTRAINT [FK__PRO_AOScr__CCNID__07220AB2]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__PRO_AOScr__Maste__08162EEB]') AND parent_object_id = OBJECT_ID(N'[dbo].[PRO_AOScrapMaster]'))
ALTER TABLE [dbo].[PRO_AOScrapMaster] DROP CONSTRAINT [FK__PRO_AOScr__Maste__08162EEB]
GO

/****** Object:  Table [dbo].[PRO_AOScrapMaster]    Script Date: 06/05/2010 05:56:46 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PRO_AOScrapMaster]') AND type in (N'U'))
DROP TABLE [dbo].[PRO_AOScrapMaster]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__PRO_Dispa__Dispa__28A2FA0E]') AND parent_object_id = OBJECT_ID(N'[dbo].[PRO_DispatchDetail]'))
ALTER TABLE [dbo].[PRO_DispatchDetail] DROP CONSTRAINT [FK__PRO_Dispa__Dispa__28A2FA0E]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__PRO_Dispa__Labor__29971E47]') AND parent_object_id = OBJECT_ID(N'[dbo].[PRO_DispatchDetail]'))
ALTER TABLE [dbo].[PRO_DispatchDetail] DROP CONSTRAINT [FK__PRO_Dispa__Labor__29971E47]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__PRO_Dispa__Labor__2A8B4280]') AND parent_object_id = OBJECT_ID(N'[dbo].[PRO_DispatchDetail]'))
ALTER TABLE [dbo].[PRO_DispatchDetail] DROP CONSTRAINT [FK__PRO_Dispa__Labor__2A8B4280]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__PRO_Dispa__Labor__2B7F66B9]') AND parent_object_id = OBJECT_ID(N'[dbo].[PRO_DispatchDetail]'))
ALTER TABLE [dbo].[PRO_DispatchDetail] DROP CONSTRAINT [FK__PRO_Dispa__Labor__2B7F66B9]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__PRO_Dispa__Machi__2C738AF2]') AND parent_object_id = OBJECT_ID(N'[dbo].[PRO_DispatchDetail]'))
ALTER TABLE [dbo].[PRO_DispatchDetail] DROP CONSTRAINT [FK__PRO_Dispa__Machi__2C738AF2]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__PRO_Dispa__Machi__2D67AF2B]') AND parent_object_id = OBJECT_ID(N'[dbo].[PRO_DispatchDetail]'))
ALTER TABLE [dbo].[PRO_DispatchDetail] DROP CONSTRAINT [FK__PRO_Dispa__Machi__2D67AF2B]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__PRO_Dispa__Machi__2E5BD364]') AND parent_object_id = OBJECT_ID(N'[dbo].[PRO_DispatchDetail]'))
ALTER TABLE [dbo].[PRO_DispatchDetail] DROP CONSTRAINT [FK__PRO_Dispa__Machi__2E5BD364]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__PRO_Dispa__Produ__7C6F7215]') AND parent_object_id = OBJECT_ID(N'[dbo].[PRO_DispatchDetail]'))
ALTER TABLE [dbo].[PRO_DispatchDetail] DROP CONSTRAINT [FK__PRO_Dispa__Produ__7C6F7215]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__PRO_Dispa__Recei__30441BD6]') AND parent_object_id = OBJECT_ID(N'[dbo].[PRO_DispatchDetail]'))
ALTER TABLE [dbo].[PRO_DispatchDetail] DROP CONSTRAINT [FK__PRO_Dispa__Recei__30441BD6]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__PRO_Dispa__Trans__3138400F]') AND parent_object_id = OBJECT_ID(N'[dbo].[PRO_DispatchDetail]'))
ALTER TABLE [dbo].[PRO_DispatchDetail] DROP CONSTRAINT [FK__PRO_Dispa__Trans__3138400F]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__PRO_Dispa__WorkC__322C6448]') AND parent_object_id = OBJECT_ID(N'[dbo].[PRO_DispatchDetail]'))
ALTER TABLE [dbo].[PRO_DispatchDetail] DROP CONSTRAINT [FK__PRO_Dispa__WorkC__322C6448]
GO

/****** Object:  Table [dbo].[PRO_DispatchDetail]    Script Date: 06/05/2010 05:57:22 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PRO_DispatchDetail]') AND type in (N'U'))
DROP TABLE [dbo].[PRO_DispatchDetail]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__PRO_Dispa__CCNID__5792F321]') AND parent_object_id = OBJECT_ID(N'[dbo].[PRO_DispatchMaster]'))
ALTER TABLE [dbo].[PRO_DispatchMaster] DROP CONSTRAINT [FK__PRO_Dispa__CCNID__5792F321]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__PRO_Dispa__Maste__5887175A]') AND parent_object_id = OBJECT_ID(N'[dbo].[PRO_DispatchMaster]'))
ALTER TABLE [dbo].[PRO_DispatchMaster] DROP CONSTRAINT [FK__PRO_Dispa__Maste__5887175A]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__PRO_Dispa__Produ__07E124C1]') AND parent_object_id = OBJECT_ID(N'[dbo].[PRO_DispatchMaster]'))
ALTER TABLE [dbo].[PRO_DispatchMaster] DROP CONSTRAINT [FK__PRO_Dispa__Produ__07E124C1]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__PRO_Dispa__WorkO__5A6F5FCC]') AND parent_object_id = OBJECT_ID(N'[dbo].[PRO_DispatchMaster]'))
ALTER TABLE [dbo].[PRO_DispatchMaster] DROP CONSTRAINT [FK__PRO_Dispa__WorkO__5A6F5FCC]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__PRO_Dispa__WorkO__5B638405]') AND parent_object_id = OBJECT_ID(N'[dbo].[PRO_DispatchMaster]'))
ALTER TABLE [dbo].[PRO_DispatchMaster] DROP CONSTRAINT [FK__PRO_Dispa__WorkO__5B638405]
GO

/****** Object:  Table [dbo].[PRO_DispatchMaster]    Script Date: 06/05/2010 05:57:36 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PRO_DispatchMaster]') AND type in (N'U'))
DROP TABLE [dbo].[PRO_DispatchMaster]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__PRO_Labor__Emplo__3AC1AA49]') AND parent_object_id = OBJECT_ID(N'[dbo].[PRO_LaborTimeDetail]'))
ALTER TABLE [dbo].[PRO_LaborTimeDetail] DROP CONSTRAINT [FK__PRO_Labor__Emplo__3AC1AA49]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__PRO_Labor__Labor__3BB5CE82]') AND parent_object_id = OBJECT_ID(N'[dbo].[PRO_LaborTimeDetail]'))
ALTER TABLE [dbo].[PRO_LaborTimeDetail] DROP CONSTRAINT [FK__PRO_Labor__Labor__3BB5CE82]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__PRO_Labor__Labor__3CA9F2BB]') AND parent_object_id = OBJECT_ID(N'[dbo].[PRO_LaborTimeDetail]'))
ALTER TABLE [dbo].[PRO_LaborTimeDetail] DROP CONSTRAINT [FK__PRO_Labor__Labor__3CA9F2BB]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__PRO_Labor__Shift__3D9E16F4]') AND parent_object_id = OBJECT_ID(N'[dbo].[PRO_LaborTimeDetail]'))
ALTER TABLE [dbo].[PRO_LaborTimeDetail] DROP CONSTRAINT [FK__PRO_Labor__Shift__3D9E16F4]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__PRO_Labor__WorkO__3E923B2D]') AND parent_object_id = OBJECT_ID(N'[dbo].[PRO_LaborTimeDetail]'))
ALTER TABLE [dbo].[PRO_LaborTimeDetail] DROP CONSTRAINT [FK__PRO_Labor__WorkO__3E923B2D]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__PRO_Labor__WorkO__3F865F66]') AND parent_object_id = OBJECT_ID(N'[dbo].[PRO_LaborTimeDetail]'))
ALTER TABLE [dbo].[PRO_LaborTimeDetail] DROP CONSTRAINT [FK__PRO_Labor__WorkO__3F865F66]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__PRO_Labor__WORou__407A839F]') AND parent_object_id = OBJECT_ID(N'[dbo].[PRO_LaborTimeDetail]'))
ALTER TABLE [dbo].[PRO_LaborTimeDetail] DROP CONSTRAINT [FK__PRO_Labor__WORou__407A839F]
GO

/****** Object:  Table [dbo].[PRO_LaborTimeDetail]    Script Date: 06/05/2010 06:00:25 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PRO_LaborTimeDetail]') AND type in (N'U'))
DROP TABLE [dbo].[PRO_LaborTimeDetail]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__PRO_Labor__CCNID__0CDAE408]') AND parent_object_id = OBJECT_ID(N'[dbo].[PRO_LaborTimeMaster]'))
ALTER TABLE [dbo].[PRO_LaborTimeMaster] DROP CONSTRAINT [FK__PRO_Labor__CCNID__0CDAE408]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__PRO_Labor__Maste__0DCF0841]') AND parent_object_id = OBJECT_ID(N'[dbo].[PRO_LaborTimeMaster]'))
ALTER TABLE [dbo].[PRO_LaborTimeMaster] DROP CONSTRAINT [FK__PRO_Labor__Maste__0DCF0841]
GO

/****** Object:  Table [dbo].[PRO_LaborTimeMaster]    Script Date: 06/05/2010 06:00:31 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PRO_LaborTimeMaster]') AND type in (N'U'))
DROP TABLE [dbo].[PRO_LaborTimeMaster]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__PRO_Machi__Machi__65E11278]') AND parent_object_id = OBJECT_ID(N'[dbo].[PRO_MachineTimeDetail]'))
ALTER TABLE [dbo].[PRO_MachineTimeDetail] DROP CONSTRAINT [FK__PRO_Machi__Machi__65E11278]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__PRO_Machi__Machi__66D536B1]') AND parent_object_id = OBJECT_ID(N'[dbo].[PRO_MachineTimeDetail]'))
ALTER TABLE [dbo].[PRO_MachineTimeDetail] DROP CONSTRAINT [FK__PRO_Machi__Machi__66D536B1]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__PRO_Machi__Shift__67C95AEA]') AND parent_object_id = OBJECT_ID(N'[dbo].[PRO_MachineTimeDetail]'))
ALTER TABLE [dbo].[PRO_MachineTimeDetail] DROP CONSTRAINT [FK__PRO_Machi__Shift__67C95AEA]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__PRO_Machi__WorkO__68BD7F23]') AND parent_object_id = OBJECT_ID(N'[dbo].[PRO_MachineTimeDetail]'))
ALTER TABLE [dbo].[PRO_MachineTimeDetail] DROP CONSTRAINT [FK__PRO_Machi__WorkO__68BD7F23]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__PRO_Machi__WorkO__69B1A35C]') AND parent_object_id = OBJECT_ID(N'[dbo].[PRO_MachineTimeDetail]'))
ALTER TABLE [dbo].[PRO_MachineTimeDetail] DROP CONSTRAINT [FK__PRO_Machi__WorkO__69B1A35C]
GO

/****** Object:  Table [dbo].[PRO_MachineTimeDetail]    Script Date: 06/05/2010 06:02:05 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PRO_MachineTimeDetail]') AND type in (N'U'))
DROP TABLE [dbo].[PRO_MachineTimeDetail]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__PRO_Machi__CCNID__0EC32C7A]') AND parent_object_id = OBJECT_ID(N'[dbo].[PRO_MachineTimeMaster]'))
ALTER TABLE [dbo].[PRO_MachineTimeMaster] DROP CONSTRAINT [FK__PRO_Machi__CCNID__0EC32C7A]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__PRO_Machi__Maste__0FB750B3]') AND parent_object_id = OBJECT_ID(N'[dbo].[PRO_MachineTimeMaster]'))
ALTER TABLE [dbo].[PRO_MachineTimeMaster] DROP CONSTRAINT [FK__PRO_Machi__Maste__0FB750B3]
GO

/****** Object:  Table [dbo].[PRO_MachineTimeMaster]    Script Date: 06/05/2010 06:02:10 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PRO_MachineTimeMaster]') AND type in (N'U'))
DROP TABLE [dbo].[PRO_MachineTimeMaster]
GO

/****** Object:  Table [dbo].[PRO_Machine]    Script Date: 06/05/2010 06:48:34 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PRO_Machine]') AND type in (N'U'))
DROP TABLE [dbo].[PRO_Machine]
GO

/****** Object:  Table [dbo].[PRO_Operation]    Script Date: 06/05/2010 06:02:55 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PRO_Operation]') AND type in (N'U'))
DROP TABLE [dbo].[PRO_Operation]
GO

/****** Object:  Table [dbo].[PRO_OperationStatus]    Script Date: 06/05/2010 06:03:01 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PRO_OperationStatus]') AND type in (N'U'))
DROP TABLE [dbo].[PRO_OperationStatus]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__PRO_Outsi__Outsi__6AA5C795]') AND parent_object_id = OBJECT_ID(N'[dbo].[PRO_OutsideProcessingDetail]'))
ALTER TABLE [dbo].[PRO_OutsideProcessingDetail] DROP CONSTRAINT [FK__PRO_Outsi__Outsi__6AA5C795]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__PRO_Outsi__Party__6B99EBCE]') AND parent_object_id = OBJECT_ID(N'[dbo].[PRO_OutsideProcessingDetail]'))
ALTER TABLE [dbo].[PRO_OutsideProcessingDetail] DROP CONSTRAINT [FK__PRO_Outsi__Party__6B99EBCE]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__PRO_Outsi__Vendo__6C8E1007]') AND parent_object_id = OBJECT_ID(N'[dbo].[PRO_OutsideProcessingDetail]'))
ALTER TABLE [dbo].[PRO_OutsideProcessingDetail] DROP CONSTRAINT [FK__PRO_Outsi__Vendo__6C8E1007]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__PRO_Outsi__WorkO__6D823440]') AND parent_object_id = OBJECT_ID(N'[dbo].[PRO_OutsideProcessingDetail]'))
ALTER TABLE [dbo].[PRO_OutsideProcessingDetail] DROP CONSTRAINT [FK__PRO_Outsi__WorkO__6D823440]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__PRO_Outsi__WorkO__6E765879]') AND parent_object_id = OBJECT_ID(N'[dbo].[PRO_OutsideProcessingDetail]'))
ALTER TABLE [dbo].[PRO_OutsideProcessingDetail] DROP CONSTRAINT [FK__PRO_Outsi__WorkO__6E765879]
GO

/****** Object:  Table [dbo].[PRO_OutsideProcessingDetail]    Script Date: 06/05/2010 06:03:06 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PRO_OutsideProcessingDetail]') AND type in (N'U'))
DROP TABLE [dbo].[PRO_OutsideProcessingDetail]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__PRO_Outsi__CCNID__10AB74EC]') AND parent_object_id = OBJECT_ID(N'[dbo].[PRO_OutsideProcessingMaster]'))
ALTER TABLE [dbo].[PRO_OutsideProcessingMaster] DROP CONSTRAINT [FK__PRO_Outsi__CCNID__10AB74EC]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__PRO_Outsi__Maste__119F9925]') AND parent_object_id = OBJECT_ID(N'[dbo].[PRO_OutsideProcessingMaster]'))
ALTER TABLE [dbo].[PRO_OutsideProcessingMaster] DROP CONSTRAINT [FK__PRO_Outsi__Maste__119F9925]
GO

/****** Object:  Table [dbo].[PRO_OutsideProcessingMaster]    Script Date: 06/05/2010 06:03:28 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PRO_OutsideProcessingMaster]') AND type in (N'U'))
DROP TABLE [dbo].[PRO_OutsideProcessingMaster]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__PRO_WORev__BinID__51A50FA1]') AND parent_object_id = OBJECT_ID(N'[dbo].[PRO_WOReversalDetail]'))
ALTER TABLE [dbo].[PRO_WOReversalDetail] DROP CONSTRAINT [FK__PRO_WORev__BinID__51A50FA1]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__PRO_WORev__Issue__529933DA]') AND parent_object_id = OBJECT_ID(N'[dbo].[PRO_WOReversalDetail]'))
ALTER TABLE [dbo].[PRO_WOReversalDetail] DROP CONSTRAINT [FK__PRO_WORev__Issue__529933DA]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__PRO_WORev__Locat__538D5813]') AND parent_object_id = OBJECT_ID(N'[dbo].[PRO_WOReversalDetail]'))
ALTER TABLE [dbo].[PRO_WOReversalDetail] DROP CONSTRAINT [FK__PRO_WORev__Locat__538D5813]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__PRO_WORev__Produ__35FCF52C]') AND parent_object_id = OBJECT_ID(N'[dbo].[PRO_WOReversalDetail]'))
ALTER TABLE [dbo].[PRO_WOReversalDetail] DROP CONSTRAINT [FK__PRO_WORev__Produ__35FCF52C]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__PRO_WORev__WORev__5575A085]') AND parent_object_id = OBJECT_ID(N'[dbo].[PRO_WOReversalDetail]'))
ALTER TABLE [dbo].[PRO_WOReversalDetail] DROP CONSTRAINT [FK__PRO_WORev__WORev__5575A085]
GO

/****** Object:  Table [dbo].[PRO_WOReversalDetail]    Script Date: 06/05/2010 06:04:42 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PRO_WOReversalDetail]') AND type in (N'U'))
DROP TABLE [dbo].[PRO_WOReversalDetail]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__PRO_WORev__CCNID__416EA7D8]') AND parent_object_id = OBJECT_ID(N'[dbo].[PRO_WOReversalMaster]'))
ALTER TABLE [dbo].[PRO_WOReversalMaster] DROP CONSTRAINT [FK__PRO_WORev__CCNID__416EA7D8]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__PRO_WORev__Maste__4262CC11]') AND parent_object_id = OBJECT_ID(N'[dbo].[PRO_WOReversalMaster]'))
ALTER TABLE [dbo].[PRO_WOReversalMaster] DROP CONSTRAINT [FK__PRO_WORev__Maste__4262CC11]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__PRO_WORev__WorkO__4356F04A]') AND parent_object_id = OBJECT_ID(N'[dbo].[PRO_WOReversalMaster]'))
ALTER TABLE [dbo].[PRO_WOReversalMaster] DROP CONSTRAINT [FK__PRO_WORev__WorkO__4356F04A]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__PRO_WORev__WORou__444B1483]') AND parent_object_id = OBJECT_ID(N'[dbo].[PRO_WOReversalMaster]'))
ALTER TABLE [dbo].[PRO_WOReversalMaster] DROP CONSTRAINT [FK__PRO_WORev__WORou__444B1483]
GO

/****** Object:  Table [dbo].[PRO_WOReversalMaster]    Script Date: 06/05/2010 06:04:48 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PRO_WOReversalMaster]') AND type in (N'U'))
DROP TABLE [dbo].[PRO_WOReversalMaster]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__PRO_WorkO__Compo__2FEF161B]') AND parent_object_id = OBJECT_ID(N'[dbo].[PRO_WorkOrderBomDetail]'))
ALTER TABLE [dbo].[PRO_WorkOrderBomDetail] DROP CONSTRAINT [FK__PRO_WorkO__Compo__2FEF161B]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__PRO_WorkO__Opera__46335CF5]') AND parent_object_id = OBJECT_ID(N'[dbo].[PRO_WorkOrderBomDetail]'))
ALTER TABLE [dbo].[PRO_WorkOrderBomDetail] DROP CONSTRAINT [FK__PRO_WorkO__Opera__46335CF5]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__PRO_WorkO__Stock__4727812E]') AND parent_object_id = OBJECT_ID(N'[dbo].[PRO_WorkOrderBomDetail]'))
ALTER TABLE [dbo].[PRO_WorkOrderBomDetail] DROP CONSTRAINT [FK__PRO_WorkO__Stock__4727812E]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__PRO_WorkO__WorkO__481BA567]') AND parent_object_id = OBJECT_ID(N'[dbo].[PRO_WorkOrderBomDetail]'))
ALTER TABLE [dbo].[PRO_WorkOrderBomDetail] DROP CONSTRAINT [FK__PRO_WorkO__WorkO__481BA567]
GO

/****** Object:  Table [dbo].[PRO_WorkOrderBomDetail]    Script Date: 06/05/2010 06:07:08 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PRO_WorkOrderBomDetail]') AND type in (N'U'))
DROP TABLE [dbo].[PRO_WorkOrderBomDetail]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__PRO_WorkO__CCNID__6F6A7CB2]') AND parent_object_id = OBJECT_ID(N'[dbo].[PRO_WorkOrderBomMaster]'))
ALTER TABLE [dbo].[PRO_WorkOrderBomMaster] DROP CONSTRAINT [FK__PRO_WorkO__CCNID__6F6A7CB2]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__PRO_WorkO__Maste__705EA0EB]') AND parent_object_id = OBJECT_ID(N'[dbo].[PRO_WorkOrderBomMaster]'))
ALTER TABLE [dbo].[PRO_WorkOrderBomMaster] DROP CONSTRAINT [FK__PRO_WorkO__Maste__705EA0EB]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__PRO_WorkO__WorkO__7152C524]') AND parent_object_id = OBJECT_ID(N'[dbo].[PRO_WorkOrderBomMaster]'))
ALTER TABLE [dbo].[PRO_WorkOrderBomMaster] DROP CONSTRAINT [FK__PRO_WorkO__WorkO__7152C524]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__PRO_WorkO__WorkO__7246E95D]') AND parent_object_id = OBJECT_ID(N'[dbo].[PRO_WorkOrderBomMaster]'))
ALTER TABLE [dbo].[PRO_WorkOrderBomMaster] DROP CONSTRAINT [FK__PRO_WorkO__WorkO__7246E95D]
GO

/****** Object:  Table [dbo].[PRO_WorkOrderBomMaster]    Script Date: 06/05/2010 06:07:25 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PRO_WorkOrderBomMaster]') AND type in (N'U'))
DROP TABLE [dbo].[PRO_WorkOrderBomMaster]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__PRO_WOSch__Funct__490FC9A0]') AND parent_object_id = OBJECT_ID(N'[dbo].[PRO_WOScheduleDetail]'))
ALTER TABLE [dbo].[PRO_WOScheduleDetail] DROP CONSTRAINT [FK__PRO_WOSch__Funct__490FC9A0]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__PRO_WOSch__Shift__4A03EDD9]') AND parent_object_id = OBJECT_ID(N'[dbo].[PRO_WOScheduleDetail]'))
ALTER TABLE [dbo].[PRO_WOScheduleDetail] DROP CONSTRAINT [FK__PRO_WOSch__Shift__4A03EDD9]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__PRO_WOSch__WorkC__4AF81212]') AND parent_object_id = OBJECT_ID(N'[dbo].[PRO_WOScheduleDetail]'))
ALTER TABLE [dbo].[PRO_WOScheduleDetail] DROP CONSTRAINT [FK__PRO_WOSch__WorkC__4AF81212]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__PRO_WOSch__WORou__4BEC364B]') AND parent_object_id = OBJECT_ID(N'[dbo].[PRO_WOScheduleDetail]'))
ALTER TABLE [dbo].[PRO_WOScheduleDetail] DROP CONSTRAINT [FK__PRO_WOSch__WORou__4BEC364B]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__PRO_WOSch__WOSch__4CE05A84]') AND parent_object_id = OBJECT_ID(N'[dbo].[PRO_WOScheduleDetail]'))
ALTER TABLE [dbo].[PRO_WOScheduleDetail] DROP CONSTRAINT [FK__PRO_WOSch__WOSch__4CE05A84]
GO

/****** Object:  Table [dbo].[PRO_WOScheduleDetail]    Script Date: 06/05/2010 06:07:42 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PRO_WOScheduleDetail]') AND type in (N'U'))
DROP TABLE [dbo].[PRO_WOScheduleDetail]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__PRO_WOSch__CCNID__009508B4]') AND parent_object_id = OBJECT_ID(N'[dbo].[PRO_WOScheduleMaster]'))
ALTER TABLE [dbo].[PRO_WOScheduleMaster] DROP CONSTRAINT [FK__PRO_WOSch__CCNID__009508B4]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__PRO_WOSch__Maste__01892CED]') AND parent_object_id = OBJECT_ID(N'[dbo].[PRO_WOScheduleMaster]'))
ALTER TABLE [dbo].[PRO_WOScheduleMaster] DROP CONSTRAINT [FK__PRO_WOSch__Maste__01892CED]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__PRO_WOSch__Produ__49AEE81E]') AND parent_object_id = OBJECT_ID(N'[dbo].[PRO_WOScheduleMaster]'))
ALTER TABLE [dbo].[PRO_WOScheduleMaster] DROP CONSTRAINT [FK__PRO_WOSch__Produ__49AEE81E]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__PRO_WOSch__Stock__0371755F]') AND parent_object_id = OBJECT_ID(N'[dbo].[PRO_WOScheduleMaster]'))
ALTER TABLE [dbo].[PRO_WOScheduleMaster] DROP CONSTRAINT [FK__PRO_WOSch__Stock__0371755F]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__PRO_WOSch__WorkO__04659998]') AND parent_object_id = OBJECT_ID(N'[dbo].[PRO_WOScheduleMaster]'))
ALTER TABLE [dbo].[PRO_WOScheduleMaster] DROP CONSTRAINT [FK__PRO_WOSch__WorkO__04659998]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__PRO_WOSch__WorkO__0559BDD1]') AND parent_object_id = OBJECT_ID(N'[dbo].[PRO_WOScheduleMaster]'))
ALTER TABLE [dbo].[PRO_WOScheduleMaster] DROP CONSTRAINT [FK__PRO_WOSch__WorkO__0559BDD1]
GO

/****** Object:  Table [dbo].[PRO_WOScheduleMaster]    Script Date: 06/05/2010 06:07:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PRO_WOScheduleMaster]') AND type in (N'U'))
DROP TABLE [dbo].[PRO_WOScheduleMaster]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__PRO_Compo__WORou__2101D846]') AND parent_object_id = OBJECT_ID(N'[dbo].[PRO_ComponentScrapDetail]'))
ALTER TABLE [dbo].[PRO_ComponentScrapDetail] DROP CONSTRAINT [FK__PRO_Compo__WORou__2101D846]
GO

IF EXISTS (SELECT id FROM syscolumns WHERE object_name(id) = '[PRO_ComponentScrapDetail]'  AND name = '[WORoutingID]')
ALTER TABLE [dbo].[PRO_ComponentScrapDetail] DROP COLUMN [WORoutingID]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__PRO_DCPRe__WORou__27AED5D5]') AND parent_object_id = OBJECT_ID(N'[dbo].[PRO_DCPResultMaster]'))
ALTER TABLE [dbo].[PRO_DCPResultMaster] DROP CONSTRAINT [FK__PRO_DCPRe__WORou__27AED5D5]
GO
IF EXISTS (SELECT id FROM syscolumns WHERE object_name(id) = '[PRO_DCPResultMaster]'  AND name = '[WORoutingID]')
ALTER TABLE [dbo].[PRO_DCPResultMaster] DROP COLUMN [WORoutingID]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__PRO_WORou__Produ__7CC477D0]') AND parent_object_id = OBJECT_ID(N'[dbo].[PRO_WORouting]'))
ALTER TABLE [dbo].[PRO_WORouting] DROP CONSTRAINT [FK__PRO_WORou__Produ__7CC477D0]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__PRO_WORou__WorkC__7DB89C09]') AND parent_object_id = OBJECT_ID(N'[dbo].[PRO_WORouting]'))
ALTER TABLE [dbo].[PRO_WORouting] DROP CONSTRAINT [FK__PRO_WORou__WorkC__7DB89C09]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__PRO_WORou__WorkO__7EACC042]') AND parent_object_id = OBJECT_ID(N'[dbo].[PRO_WORouting]'))
ALTER TABLE [dbo].[PRO_WORouting] DROP CONSTRAINT [FK__PRO_WORou__WorkO__7EACC042]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__PRO_WORou__WorkO__7FA0E47B]') AND parent_object_id = OBJECT_ID(N'[dbo].[PRO_WORouting]'))
ALTER TABLE [dbo].[PRO_WORouting] DROP CONSTRAINT [FK__PRO_WORou__WorkO__7FA0E47B]
GO

/****** Object:  Table [dbo].[PRO_WORouting]    Script Date: 06/05/2010 06:07:34 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PRO_WORouting]') AND type in (N'U'))
DROP TABLE [dbo].[PRO_WORouting]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__SO_Additi__AddCh__25077354]') AND parent_object_id = OBJECT_ID(N'[dbo].[SO_AdditionCharge]'))
ALTER TABLE [dbo].[SO_AdditionCharge] DROP CONSTRAINT [FK__SO_Additi__AddCh__25077354]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__SO_Additi__Reaso__25FB978D]') AND parent_object_id = OBJECT_ID(N'[dbo].[SO_AdditionCharge]'))
ALTER TABLE [dbo].[SO_AdditionCharge] DROP CONSTRAINT [FK__SO_Additi__Reaso__25FB978D]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__SO_Additi__SaleO__26EFBBC6]') AND parent_object_id = OBJECT_ID(N'[dbo].[SO_AdditionCharge]'))
ALTER TABLE [dbo].[SO_AdditionCharge] DROP CONSTRAINT [FK__SO_Additi__SaleO__26EFBBC6]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__SO_Additi__SaleO__27E3DFFF]') AND parent_object_id = OBJECT_ID(N'[dbo].[SO_AdditionCharge]'))
ALTER TABLE [dbo].[SO_AdditionCharge] DROP CONSTRAINT [FK__SO_Additi__SaleO__27E3DFFF]
GO

/****** Object:  Table [dbo].[SO_AdditionCharge]    Script Date: 06/05/2010 06:08:51 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SO_AdditionCharge]') AND type in (N'U'))
DROP TABLE [dbo].[SO_AdditionCharge]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__SO_PackLi__BinID__29CC2871]') AND parent_object_id = OBJECT_ID(N'[dbo].[SO_PackListDetail]'))
ALTER TABLE [dbo].[SO_PackListDetail] DROP CONSTRAINT [FK__SO_PackLi__BinID__29CC2871]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__SO_PackLi__Locat__2AC04CAA]') AND parent_object_id = OBJECT_ID(N'[dbo].[SO_PackListDetail]'))
ALTER TABLE [dbo].[SO_PackListDetail] DROP CONSTRAINT [FK__SO_PackLi__Locat__2AC04CAA]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__SO_PackLi__Maste__2BB470E3]') AND parent_object_id = OBJECT_ID(N'[dbo].[SO_PackListDetail]'))
ALTER TABLE [dbo].[SO_PackListDetail] DROP CONSTRAINT [FK__SO_PackLi__Maste__2BB470E3]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__SO_PackLi__PackL__2CA8951C]') AND parent_object_id = OBJECT_ID(N'[dbo].[SO_PackListDetail]'))
ALTER TABLE [dbo].[SO_PackListDetail] DROP CONSTRAINT [FK__SO_PackLi__PackL__2CA8951C]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__SO_PackLi__Produ__0169315C]') AND parent_object_id = OBJECT_ID(N'[dbo].[SO_PackListDetail]'))
ALTER TABLE [dbo].[SO_PackListDetail] DROP CONSTRAINT [FK__SO_PackLi__Produ__0169315C]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__SO_PackLi__SaleO__2E90DD8E]') AND parent_object_id = OBJECT_ID(N'[dbo].[SO_PackListDetail]'))
ALTER TABLE [dbo].[SO_PackListDetail] DROP CONSTRAINT [FK__SO_PackLi__SaleO__2E90DD8E]
GO

/****** Object:  Table [dbo].[SO_PackListDetail]    Script Date: 06/05/2010 06:09:28 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SO_PackListDetail]') AND type in (N'U'))
DROP TABLE [dbo].[SO_PackListDetail]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__SO_PackLi__Carri__2C538F61]') AND parent_object_id = OBJECT_ID(N'[dbo].[SO_PackListMaster]'))
ALTER TABLE [dbo].[SO_PackListMaster] DROP CONSTRAINT [FK__SO_PackLi__Carri__2C538F61]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__SO_PackLi__Emplo__2D47B39A]') AND parent_object_id = OBJECT_ID(N'[dbo].[SO_PackListMaster]'))
ALTER TABLE [dbo].[SO_PackListMaster] DROP CONSTRAINT [FK__SO_PackLi__Emplo__2D47B39A]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__SO_PackLi__UnitO__2E3BD7D3]') AND parent_object_id = OBJECT_ID(N'[dbo].[SO_PackListMaster]'))
ALTER TABLE [dbo].[SO_PackListMaster] DROP CONSTRAINT [FK__SO_PackLi__UnitO__2E3BD7D3]
GO

/****** Object:  Table [dbo].[SO_PackListMaster]    Script Date: 06/05/2010 06:09:34 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SO_PackListMaster]') AND type in (N'U'))
DROP TABLE [dbo].[SO_PackListMaster]
GO

-- add MultiCompletionNo to PRO_WorkOrderCompletion
IF NOT EXISTS (SELECT id FROM syscolumns WHERE object_name(id) = 'PRO_WorkOrderCompletion'  AND name = 'MultiCompletionNo')
Begin
	ALTER TABLE PRO_WorkOrderCompletion ADD MultiCompletionNo         NVARCHAR(200) NULL
End
go

IF  EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[dbo].[CK_MST_TransactionHistory_MasLocOH]') AND parent_object_id = OBJECT_ID(N'[dbo].[MST_TransactionHistory]'))
ALTER TABLE [dbo].[MST_TransactionHistory] DROP CONSTRAINT [CK_MST_TransactionHistory_MasLocOH]
GO

IF  EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[dbo].[CK_MST_TransactionHistory_MasLocCommit]') AND parent_object_id = OBJECT_ID(N'[dbo].[MST_TransactionHistory]'))
ALTER TABLE [dbo].[MST_TransactionHistory] DROP CONSTRAINT [CK_MST_TransactionHistory_MasLocCommit]
GO

IF  EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[dbo].[CK_MST_TransactionHistory_LocationOH]') AND parent_object_id = OBJECT_ID(N'[dbo].[MST_TransactionHistory]'))
ALTER TABLE [dbo].[MST_TransactionHistory] DROP CONSTRAINT [CK_MST_TransactionHistory_LocationOH]
GO

IF  EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[dbo].[CK_MST_TransactionHistory_LocationCommit]') AND parent_object_id = OBJECT_ID(N'[dbo].[MST_TransactionHistory]'))
ALTER TABLE [dbo].[MST_TransactionHistory] DROP CONSTRAINT [CK_MST_TransactionHistory_LocationCommit]
GO

IF  EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[dbo].[CK_MST_TransactionHistory_BinOH]') AND parent_object_id = OBJECT_ID(N'[dbo].[MST_TransactionHistory]'))
ALTER TABLE [dbo].[MST_TransactionHistory] DROP CONSTRAINT [CK_MST_TransactionHistory_BinOH]
GO

IF  EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[dbo].[CK_MST_TransactionHistory_BinCommit]') AND parent_object_id = OBJECT_ID(N'[dbo].[MST_TransactionHistory]'))
ALTER TABLE [dbo].[MST_TransactionHistory] DROP CONSTRAINT [CK_MST_TransactionHistory_BinCommit]
GO

-- Drop contrains OHQuantity >= 0 and CommitQuantity >= 0 on IV_BinCache
IF  EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[CK_IV_BinCache2]') AND parent_object_id = OBJECT_ID(N'[IV_BinCache]'))
ALTER TABLE [IV_BinCache] DROP CONSTRAINT [CK_IV_BinCache2]
GO
IF  EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[CK_IV_BinCache_Commit]') AND parent_object_id = OBJECT_ID(N'[dbo].[IV_BinCache]'))
ALTER TABLE [dbo].[IV_BinCache] DROP CONSTRAINT [CK_IV_BinCache_Commit]
GO

-- Drop contrains OHQuantity >= 0 and CommitQuantity >= 0 on IV_LocationCache
IF  EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[dbo].[CK_IV_LocationCache_Commit]') AND parent_object_id = OBJECT_ID(N'[IV_LocationCache]'))
ALTER TABLE [IV_LocationCache] DROP CONSTRAINT [CK_IV_LocationCache_Commit]
GO
IF  EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[dbo].[CK_IV_LocationCache2]') AND parent_object_id = OBJECT_ID(N'[IV_LocationCache]'))
ALTER TABLE [IV_LocationCache] DROP CONSTRAINT [CK_IV_LocationCache2]
GO


-- Drop contrains OHQuantity >= 0 and CommitQuantity >= 0 on IV_MasLocCache
IF  EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[dbo].[CK_IV_MasLocCache2]') AND parent_object_id = OBJECT_ID(N'[IV_MasLocCache]'))
ALTER TABLE [IV_MasLocCache] DROP CONSTRAINT [CK_IV_MasLocCache2]
GO
IF  EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[dbo].[CK_IV_MasLocCache_Commit]') AND parent_object_id = OBJECT_ID(N'[IV_MasLocCache]'))
ALTER TABLE [IV_MasLocCache] DROP CONSTRAINT [CK_IV_MasLocCache_Commit]
GO

-- alter some columns in Sys_Menu_Entry to allow longer text
IF EXISTS (SELECT id FROM syscolumns WHERE object_name(id) = 'Sys_Menu_Entry'  AND name = 'Shortcut')
Begin
	ALTER TABLE Sys_Menu_Entry ALTER COLUMN Parent_Shortcut nvarchar(2000)
	ALTER TABLE Sys_Menu_Entry ALTER COLUMN Shortcut nvarchar(2000)
	ALTER TABLE Sys_Menu_Entry ALTER COLUMN Text_CaptionDefault nvarchar(2000)
	ALTER TABLE Sys_Menu_Entry ALTER COLUMN Text_Caption_VI_VN nvarchar(2000)
	ALTER TABLE Sys_Menu_Entry ALTER COLUMN Text_Caption_EN_US nvarchar(2000)
	ALTER TABLE Sys_Menu_Entry ALTER COLUMN Text_Caption_JA_JP nvarchar(2000)
	ALTER TABLE Sys_Menu_Entry ALTER COLUMN Text_Caption_Language_Default nvarchar(2000)
	ALTER TABLE Sys_Menu_Entry ALTER COLUMN [Description] nvarchar(2000)
	ALTER TABLE Sys_Menu_Entry ALTER COLUMN Prefix nvarchar(2000)
	ALTER TABLE Sys_Menu_Entry ALTER COLUMN TransFormat nvarchar(2000)
	ALTER TABLE Sys_Menu_Entry ALTER COLUMN TableName nvarchar(2000)
	ALTER TABLE Sys_Menu_Entry ALTER COLUMN TransNoFieldName nvarchar(2000)
	ALTER TABLE Sys_Menu_Entry ALTER COLUMN ReportID nvarchar(2000)
	
End
go

-- change name of column CheckBom to AllowNegativeQty
IF EXISTS (SELECT id FROM syscolumns WHERE object_name(id) = 'ITM_Product'  AND name = 'CheckBOM')
Begin
	EXEC sp_rename 'ITM_Product.CheckBom', 'AllowNegativeQty', 'COLUMN';
End
go
IF NOT EXISTS (SELECT id FROM syscolumns WHERE object_name(id) = 'ITM_Product'  AND name = 'AllowNegativeQty')
Begin
	ALTER TABLE ITM_Product ADD AllowNegativeQty         bit NULL
End
go

-- add LocationID field into SO_ConfirmShipMaster
IF NOT EXISTS (SELECT id FROM syscolumns WHERE object_name(id) = 'SO_ConfirmShipMaster'  AND name = 'LocationID')
Begin
	ALTER TABLE SO_ConfirmShipMaster ADD LocationID         int NULL
	ALTER TABLE SO_ConfirmShipMaster
	       ADD FOREIGN KEY (LocationID)
	                             REFERENCES MST_Location
End
go
-- add BinID into SO_ConfirmShipMaster
IF NOT EXISTS (SELECT id FROM syscolumns WHERE object_name(id) = 'SO_ConfirmShipMaster'  AND name = 'BinID')
Begin
	ALTER TABLE SO_ConfirmShipMaster ADD BinID         int NULL
	ALTER TABLE SO_ConfirmShipMaster
	       ADD FOREIGN KEY (BinID)
	                             REFERENCES MST_Bin
End
go

-- add LocationID field into SO_InvoiceMaster
IF NOT EXISTS (SELECT id FROM syscolumns WHERE object_name(id) = 'SO_InvoiceMaster'  AND name = 'LocationID')
Begin
	ALTER TABLE SO_InvoiceMaster ADD LocationID         int NULL
	ALTER TABLE SO_InvoiceMaster
	       ADD FOREIGN KEY (LocationID)
	                             REFERENCES MST_Location
End
go
-- add BinID into SO_InvoiceMaster
IF NOT EXISTS (SELECT id FROM syscolumns WHERE object_name(id) = 'SO_InvoiceMaster'  AND name = 'BinID')
Begin
	ALTER TABLE SO_InvoiceMaster ADD BinID         int NULL
	ALTER TABLE SO_InvoiceMaster
	       ADD FOREIGN KEY (BinID)
	                             REFERENCES MST_Bin
End
go

-- increase size of Code in PO_PurchaseOrderMaster
IF EXISTS (SELECT id FROM syscolumns WHERE object_name(id) = 'PO_PurchaseOrderMaster'  AND name = 'code')
Begin
	ALTER TABLE PO_PurchaseOrderMaster ALTER COLUMN Code         nvarchar(400)
End
go

IF EXISTS (SELECT name FROM sys.indexes
            WHERE name = N'IX_PRO_DCPResultMaster_CPOID')
    DROP INDEX IX_PRO_DCPResultMaster_CPOID ON PRO_DCPResultMaster ;
GO
CREATE NONCLUSTERED INDEX IX_PRO_DCPResultMaster_CPOID
ON dbo.PRO_DCPResultMaster (CPOID)
GO

IF EXISTS (SELECT name FROM sys.indexes
            WHERE name = N'IX_MTR_CPO_MRPCycleOptionMasterID')
    DROP INDEX IX_MTR_CPO_MRPCycleOptionMasterID ON MTR_CPO
GO
CREATE NONCLUSTERED INDEX IX_MTR_CPO_MRPCycleOptionMasterID
ON dbo.MTR_CPO (MRPCycleOptionMasterID)
INCLUDE (CPOID,Quantity,StartDate,DueDate,RefMasterID,RefDetailID,RefType,NetAvailableQuantity,
CCNID,ProductID,MasterLocationID,StockUMID,IsMPS,Converted,POGeneratedID,WOGeneratedID,
MPSCycleOptionMasterID,ParentCPOID,DemandQuantity,SupplyQuantity,DCPUpdated,IsSafetyStock)
GO

IF EXISTS (SELECT name FROM sys.indexes
            WHERE name = N'IX_PRO_DCPResultDetail_DCPResultMasterID')
    DROP INDEX IX_PRO_DCPResultDetail_DCPResultMasterID ON PRO_DCPResultDetail
GO
CREATE NONCLUSTERED INDEX IX_PRO_DCPResultDetail_DCPResultMasterID
ON dbo.PRO_DCPResultDetail (DCPResultMasterID)
INCLUDE (DCPResultDetailID)

IF EXISTS (SELECT name FROM sys.indexes
            WHERE name = N'IX_PRO_IssueMaterialDetail_ProductID_WorkOrderDetailID')
    DROP INDEX IX_PRO_IssueMaterialDetail_ProductID_WorkOrderDetailID ON PRO_IssueMaterialDetail ;
GO
CREATE NONCLUSTERED INDEX IX_PRO_IssueMaterialDetail_ProductID_WorkOrderDetailID
ON dbo.PRO_IssueMaterialDetail (ProductID,WorkOrderDetailID)
GO

IF EXISTS (SELECT name FROM sys.indexes
            WHERE name = N'IX_PRO_WorkOrderDetail_StartDate')
    DROP INDEX IX_PRO_WorkOrderDetail_StartDate ON PRO_WorkOrderDetail ;
GO
CREATE NONCLUSTERED INDEX IX_PRO_WorkOrderDetail_StartDate
ON dbo.PRO_WorkOrderDetail (StartDate)
INCLUDE (WorkOrderDetailID,Line,OrderQuantity,DueDate,ProductID)
GO

IF EXISTS (SELECT name FROM sys.indexes
            WHERE name = N'IX_PRO_WorkOrderDetail_Status')
    DROP INDEX IX_PRO_WorkOrderDetail_Status ON PRO_WorkOrderDetail ;
GO
CREATE NONCLUSTERED INDEX IX_PRO_WorkOrderDetail_Status
ON dbo.PRO_WorkOrderDetail ([Status])
INCLUDE ([WorkOrderDetailID],[Line],[OrderQuantity],[DueDate],[StartDate],[ProductID],[WorkOrderMasterID],[StockUMID])
GO

IF EXISTS (SELECT name FROM sys.indexes
            WHERE name = N'IX_PRO_WorkOrderCompletion_WorkOrderDetailID')
    DROP INDEX IX_PRO_WorkOrderCompletion_WorkOrderDetailID ON PRO_WorkOrderCompletion ;
GO
CREATE NONCLUSTERED INDEX IX_PRO_WorkOrderCompletion_WorkOrderDetailID
ON [dbo].[PRO_WorkOrderCompletion] ([WorkOrderDetailID])
INCLUDE ([CompletedQuantity])
GO

IF EXISTS (SELECT name FROM sys.indexes
            WHERE name = N'IX_PRO_WorkOrderDetail_WorkOrderMasterID_Status')
    DROP INDEX IX_PRO_WorkOrderDetail_WorkOrderMasterID_Status ON PRO_WorkOrderDetail ;
GO
CREATE NONCLUSTERED INDEX IX_PRO_WorkOrderDetail_WorkOrderMasterID_Status
ON [dbo].[PRO_WorkOrderDetail] ([WorkOrderMasterID],[Status])
INCLUDE ([WorkOrderDetailID],[Line],[OrderQuantity],[DueDate],[StartDate],[ProductID],[StockUMID])
GO
/*
File:		MenuConfig.sql
Created:	DungLA
Purpose:	All menu entry
*/

---------------------------------MENU PART----------------------------------------------

-- visible Scrap Recording menu
IF EXISTS (SELECT Menu_EntryID FROM Sys_Menu_Entry  WHERE Menu_EntryID = 98)
Begin
	UPDATE Sys_Menu_Entry SET Type = 0 WHERE Menu_EntryID = 98
End
go
IF EXISTS (SELECT Menu_EntryID FROM Sys_Menu_Entry  WHERE Menu_EntryID = 89)
Begin
	UPDATE Sys_Menu_Entry SET Type = 0 WHERE Menu_EntryID = 89
End
go

IF NOT EXISTS (SELECT Menu_EntryID FROM Sys_Menu_Entry  WHERE Text_CaptionDefault like '%Delivery Plan by Hour Report%')
Begin
 SET IDENTITY_INSERT Sys_Menu_Entry ON
INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut], [Menu_EntryID], [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
 VALUES ( 'REPORT', 596, 'RP', 16, 'Planning', 'Planning', 'Planning', 'Planning', 'en_US', 2, NULL, NULL, 0, 2, 0, NULL, NULL, 0, 0, NULL, NULL, NULL )

INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut], [Menu_EntryID], [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
 VALUES ( 'RP', 601, 'RPCA', 17, 'Compare Actual and Standard Capacity', 'Compare Actual and Standard Capacity (vn)', 'Compare Actual and Standard Capacity (en)', 'Compare Actual and Standard Capacity (jp)', 'en_US', 3, 'PCSUtils.Framework.ReportFrame.ViewReport', NULL, 0, NULL, NULL, NULL, NULL, 0, 0, NULL, NULL, '20051219142456233' )

INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut], [Menu_EntryID], [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
 VALUES ( 'RP', 602, 'RPMM', 18, 'Material Management Report', 'Material Management Report', 'Material Management Report', 'Material Management Report', 'en_US', 3, 'PCSUtils.Framework.ReportFrame.ViewReport', NULL, 0, NULL, NULL, NULL, NULL, 0, 0, NULL, NULL, '20051014170759' )

INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut], [Menu_EntryID], [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
 VALUES ( 'RP', 603, 'RPDP', 19, 'Delivery Plan by Hour Report', 'Delivery Plan by Hour Report', 'Delivery Plan by Hour Report', 'Delivery Plan by Hour Report', 'en_US', 3, 'PCSUtils.Framework.ReportFrame.ViewReport', NULL, 0, NULL, NULL, NULL, NULL, 0, 0, NULL, NULL, '20051014171459' )

INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut], [Menu_EntryID], [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
 VALUES ( 'RP', 604, 'RPPR', 20, 'Party Report', 'Party Report', 'Party Report', 'Party Report', 'en_US', 3, 'PCSUtils.Framework.ReportFrame.ViewReport', NULL, 0, NULL, NULL, NULL, NULL, 0, 0, NULL, NULL, '20051109104710467')

INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut], [Menu_EntryID], [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
 VALUES ( 'RP', 605, 'RPPL', 21, 'Production Line and Make Item Reference List', 'Production Line and Make Item Reference List', 'Production Line and Make Item Reference List', 'Production Line and Make Item Reference List', 'en_US', 3, 'PCSUtils.Framework.ReportFrame.ViewReport', NULL, 0, NULL, NULL, NULL, NULL, 0, 0, NULL, NULL, '20051122091908730' )

INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut], [Menu_EntryID], [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
 VALUES ( 'RP', 606, 'RPWCL', 22, 'Work Center List', 'Work Center List', 'Work Center List', 'Work Center List', 'en_US', 3, 'PCSUtils.Framework.ReportFrame.ViewReport', NULL, 0, NULL, NULL, NULL, NULL, 0, 0, NULL, NULL, '20051122094106933' )

INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut], [Menu_EntryID], [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
 VALUES ( 'RP', 607, 'RPWSC', 23, 'Working Scheme', 'Working Scheme', 'Working Scheme', 'Working Scheme', 'en_US', 3, 'PCSUtils.Framework.ReportFrame.ViewReport', NULL, 0, NULL, NULL, NULL, NULL, 0, 0, NULL, NULL, '20051206152944700' )

INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut], [Menu_EntryID], [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
 VALUES ( 'RP', 608, 'RPPLC', 24, 'Production Line Capacity Management', 'Production Line Capacity Management', 'Production Line Capacity Management', 'Production Line Capacity Management', 'en_US', 3, 'PCSUtils.Framework.ReportFrame.ViewReport', NULL, 0, NULL, NULL, NULL, NULL, 0, 0, NULL, NULL, '20051206153412390' )

INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut], [Menu_EntryID], [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
 VALUES ( 'RP', 612, 'RPPROL', 25, 'Production Line', 'Production Line', 'Production Line', 'Production Line', 'en_US', 3, 'PCSUtils.Framework.ReportFrame.ViewReport', NULL, 0, NULL, NULL, NULL, NULL, 0, 0, NULL, NULL, '20051209094541850' )

INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut], [Menu_EntryID], [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
 VALUES ( 'RP', 614, 'RPPLPGM', 26, 'Production Line Progress Management', 'Production Line Progress Management', 'Production Line Progress Management', 'Production Line Progress Management', 'en_US', 3, 'PCSUtils.Framework.ReportFrame.ViewReport', NULL, 0, NULL, NULL, NULL, NULL, 0, 0, NULL, NULL, '20051214171951937' )

INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut], [Menu_EntryID], [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
 VALUES ( 'RP', 615, 'CPASC', 27, 'Compare Actual and Standard Capacity', 'Compare Actual and Standard Capacity', 'Compare Actual and Standard Capacity', 'Compare Actual and Standard Capacity', 'en_US', 3, 'PCSUtils.Framework.ReportFrame.ViewReport', NULL, 0, NULL, NULL, NULL, NULL, 0, 0, NULL, NULL, '20051219142456233' )

INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut], [Menu_EntryID], [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
 VALUES ( 'RP', 616, 'WCR', 28, 'Work Center Report', 'Work Center Report', 'Work Center Report', 'Work Center Report', 'en_US', 3, 'PCSUtils.Framework.ReportFrame.ViewReport', NULL, 0, NULL, NULL, NULL, NULL, 0, 0, NULL, NULL, '20060106101835983' )

INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut], [Menu_EntryID], [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
 VALUES ( 'RP', 617, 'RPDCP', 29, 'Detail Capacity Planning Report', 'Detail Capacity Planning Report', 'Detail Capacity Planning Report', 'Detail Capacity Planning Report', 'en_US', 3, 'PCSUtils.Framework.ReportFrame.ViewReport', NULL, 0, NULL, NULL, NULL, NULL, 0, 0, NULL, NULL, '20060126093222153' )

INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut], [Menu_EntryID], [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
 VALUES ( 'RP', 618, 'NIGURI', 30, 'NIGURI Report', 'NIGURI Report', 'NIGURI Report', 'NIGURI Report', 'en_US', 3, 'PCSUtils.Framework.ReportFrame.ViewReport', NULL, 0, NULL, NULL, NULL, NULL, 0, 0, NULL, NULL, '20060126094430700' )

INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut], [Menu_EntryID], [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
 VALUES ( 'RP', 619, 'PLPGR', 31, 'Production Line Production Progress', 'Production Line Production Progress', 'Production Line Production Progress', 'Production Line Production Progress', 'en_US', 3, 'PCSUtils.Framework.ReportFrame.ViewReport', NULL, 0, NULL, NULL, NULL, NULL, 0, 0, NULL, NULL, '20060207140308233' )

INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut], [Menu_EntryID], [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
 VALUES ( 'RP', 620, 'PLDP', 32, 'Production Line Delivery Progress', 'Production Line Delivery Progress', 'Production Line Delivery Progress', 'Production Line Delivery Progress', 'en_US', 3, 'PCSUtils.Framework.ReportFrame.ViewReport', NULL, 0, NULL, NULL, NULL, NULL, 0, 0, NULL, NULL, '20060214170456030' )

INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut], [Menu_EntryID], [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
 VALUES ( 'RP', 622, 'PLCMP', 33, 'Production Line Capacity and Man Power Management', 'Production Line Capacity and Man Power Management', 'Production Line Capacity and Man Power Management', 'PL Capacity and Man Power', 'en_US', 3, 'PCSUtils.Framework.ReportFrame.ViewReport', NULL, 0, NULL, NULL, NULL, NULL, 0, 0, NULL, NULL, '20060217145400327' )

INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut], [Menu_EntryID], [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
 VALUES ( 'REPORT', 625, 'IVRPT', 34, 'Inventory Reports', 'Inventory Reports', 'Inventory Reports', 'Inventory Reports', 'en_US', 2, NULL, NULL, 0, 2, 0, NULL, NULL, 0, 0, NULL, NULL, NULL )

INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut], [Menu_EntryID], [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
 VALUES ( 'IVRPT', 626, 'TSH', 35, 'Transaction History', 'Transaction History', 'Transaction History', 'Transaction History', 'en_US', 3, 'PCSUtils.Framework.ReportFrame.ViewReport', NULL, 0, NULL, NULL, NULL, NULL, 0, 0, NULL, NULL, '20051216155448810' )

INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut], [Menu_EntryID], [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
 VALUES ( 'IVRPT', 629, 'IVDTL', 38, 'Inventory Detail', 'Inventory Detail', 'Inventory Detail', 'Inventory Detail', 'en_US', 3, 'PCSUtils.Framework.ReportFrame.ViewReport', NULL, 0, NULL, NULL, NULL, NULL, 0, 0, NULL, NULL, '20060121165113763' )

INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut], [Menu_EntryID], [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
 VALUES ( 'IVRPT', 630, 'IVDBP', 39, 'Inventory Detail by Product', 'Inventory Detail by Product', 'Inventory Detail by Product', 'Inventory Detail by Product', 'en_US', 3, 'PCSUtils.Framework.ReportFrame.ViewReport', NULL, 0, NULL, NULL, NULL, NULL, 0, 0, NULL, NULL, '20060123151943030' )

INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut], [Menu_EntryID], [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
 VALUES ( 'IVRPT', 631, 'WMMS', 40, 'Warning Min-Max-Stock Report', 'Warning Min-Max-Stock Report', 'Warning Min-Max-Stock Report', 'Warning Min-Max-Stock Report', 'en_US', 3, 'PCSUtils.Framework.ReportFrame.ViewReport', NULL, 0, NULL, NULL, NULL, NULL, 0, 0, NULL, NULL, '20060124141252233' )

INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut], [Menu_EntryID], [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
 VALUES ( 'REPORT', 633, 'CSTRPT', 41, 'Costing Reports', 'Costing Reports', 'Costing Reports', 'Costing Reports', 'en_US', 2, NULL, NULL, 0, 2, 0, NULL, NULL, 0, 0, NULL, NULL, NULL )

INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut], [Menu_EntryID], [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
 VALUES ( 'CSTRPT', 634, 'ITMSC', 42, 'Item Standard Cost Report', 'Item Standard Cost Report', 'Item Standard Cost Report', 'Item Standard Cost Report', 'en_US', 3, 'PCSUtils.Framework.ReportFrame.ViewReport', NULL, 0, NULL, NULL, NULL, NULL, 0, 0, NULL, NULL, '20060214164834670' )

INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut], [Menu_EntryID], [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
 VALUES ( 'CSTRPT', 635, 'CABE', 43, 'Cost Allocation By Element', 'Cost Allocation By Element', 'Cost Allocation By Element', 'Cost Allocation By Element', 'en_US', 3, 'PCSUtils.Framework.ReportFrame.ViewReport', NULL, 0, NULL, NULL, NULL, NULL, 0, 0, NULL, NULL, '20060228140122293' )

INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut], [Menu_EntryID], [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
 VALUES ( 'REPORT', 636, 'PRPT', 44, 'Product Reports', 'Product Reports', 'Product Reports', 'Product Reports', 'en_US', 3, NULL, NULL, 0, 2, 0, NULL, NULL, 0, 0, NULL, NULL, NULL )

INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut], [Menu_EntryID], [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
 VALUES ( 'PRPT', 637, 'RIRPT', 45, 'Routing Information', 'Routing Information', 'Routing Information', 'Routing Information', 'en_US', 3, 'PCSUtils.Framework.ReportFrame.ViewReport', NULL, 0, NULL, NULL, NULL, NULL, 0, 0, NULL, NULL, '20051214095905607' )

INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut], [Menu_EntryID], [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
 VALUES ( 'PRPT', 638, 'ILRPT', 46, 'List Of Materials', 'List Of Materials', 'List Of Materials', 'List Of Materials', 'en_US', 3, 'PCSUtils.Framework.ReportFrame.ViewReport', NULL, 0, NULL, NULL, NULL, NULL, 0, 0, NULL, NULL, '20051216102010373' )

INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut], [Menu_EntryID], [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
 VALUES ( 'PRPT', 639, 'IWURPT', 47, 'Item Where Used Report', 'Item Where Used Report', 'Item Where Used Report', 'Item Where Used Report', 'en_US', 3, 'PCSUtils.Framework.ReportFrame.ViewReport', NULL, 0, NULL, NULL, NULL, NULL, 0, 0, NULL, NULL, '20051227093844013' )

INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut], [Menu_EntryID], [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
 VALUES ( 'PRPT', 640, 'VSIRPT', 48, 'Vendor and Supply Item', 'Vendor and Supply Item', 'Vendor and Supply Item', 'Vendor and Supply Item', 'en_US', 3, 'PCSUtils.Framework.ReportFrame.ViewReport', NULL, 0, NULL, NULL, NULL, NULL, 0, 0, NULL, NULL, '20060123104959123' )

INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut], [Menu_EntryID], [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
 VALUES ( 'REPORT', 641, 'SRPT', 49, 'Sales Reports', 'Sales Report', 'Sales Report', 'Sales Report', 'en_US', 2, NULL, NULL, 0, 2, 0, NULL, NULL, 0, 0, NULL, NULL, NULL )

INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut], [Menu_EntryID], [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
 VALUES ( 'SRPT', 642, 'DTCS', 50, 'Delivery To Customer Schedule', 'Delivery To Customer Schedule', 'Delivery To Customer Schedule', 'Delivery To Customer Schedule', 'en_US', 3, 'PCSUtils.Framework.ReportFrame.ViewReport', NULL, 0, NULL, NULL, NULL, NULL, 0, 0, NULL, NULL, '20051222150218873' )

INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut], [Menu_EntryID], [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
 VALUES ( 'REPORT', 643, 'PRRPT', 51, 'Procurement Reports', 'Procurement Reports', 'Procurement Reports', 'Procurement Reports', 'en_US', 2, NULL, NULL, 0, 2, 0, NULL, NULL, 0, 0, NULL, NULL, NULL )
 SET IDENTITY_INSERT Sys_Menu_Entry OFF
End

go

IF NOT EXISTS (SELECT Menu_EntryID FROM Sys_Menu_Entry  WHERE Text_CaptionDefault like '%Standard Cost & Actual Cost Comparision%')
Begin
 SET IDENTITY_INSERT Sys_Menu_Entry ON

  INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut], [Menu_EntryID], [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
  VALUES ( 'CSTRPT', 644, 'SAC', 44, 'Standard Cost & Actual Cost Comparision', 'Standard Cost & Actual Cost Comparision', 'Standard Cost & Actual Cost Comparision', 'Standard Cost & Actual Cost Comparision', 'en_US', 3, 'PCSUtils.Framework.ReportFrame.ViewReport', NULL, 0, NULL, NULL, NULL, NULL, 0, 0, NULL, NULL, '20060313085806263')

  INSERT INTO [dbo].[Sys_Right] ([RoleID], [Permission], [Menu_EntryID] ) VALUES ( 1, 31, @@IDENTITY )
 SET IDENTITY_INSERT Sys_Menu_Entry OFF
 End
go


--Update existing reports
IF NOT EXISTS (SELECT * FROM sys_Menu_Entry WHERE  Text_CaptionDefault = 'Computer Planned Order Report' AND Parent_Shortcut = 'RP')
Begin
 UPDATE sys_Menu_Entry SET Parent_Shortcut = 'RP' WHERE Text_CaptionDefault = 'Computer Planned Order Report'
 UPDATE sys_Menu_Entry SET Parent_Shortcut = 'SRPT' WHERE Text_CaptionDefault = 'Schedule Of Local Part In Month'
 UPDATE sys_Menu_Entry SET Parent_Shortcut = 'PRRPT' WHERE Text_CaptionDefault = 'Order Summary'
 UPDATE sys_Menu_Entry SET Parent_Shortcut = 'SRPT' WHERE Text_CaptionDefault = 'Part Order Sheet Multi Vendor Report'
 UPDATE sys_Menu_Entry SET Parent_Shortcut = 'IVRPT' WHERE Text_CaptionDefault = 'In-Out Stock Report'
 UPDATE sys_Menu_Entry SET Parent_Shortcut = 'CSTRPT' WHERE Text_CaptionDefault = 'Actual Cost Related Reports'
End
go

-- Delete Right for 6 existing reports
IF EXISTS (SELECT * FROM sys_Right WHERE RightID = 1888 AND Menu_EntryID = 212)
 DELETE FROM sys_Right WHERE Menu_EntryID in (select Menu_EntryID from sys_Menu_Entry where (Parent_Shortcut = 'RP' or Parent_Shortcut = 'IVRPT'
 Or Parent_Shortcut = 'CSTRPT' or Parent_Shortcut = 'PRRPT' or Parent_Shortcut = 'SRPT'
 Or Parent_Shortcut = 'PRPT') AND reportID IS NULL)
go

-- Move CPO Data Viewer into DCP Menu
IF EXISTS (SELECT * FROM sys_Menu_Entry WHERE Text_CaptionDefault = 'CPO Data Viewer' and Parent_Shortcut='MPS')
 UPDATE sys_Menu_Entry SET Parent_Shortcut = 'DCP' WHERE Text_CaptionDefault = 'CPO Data Viewer'
go


-- Remove Delete Std Cost
IF EXISTS (SELECT * FROM sys_Menu_Entry WHERE Text_Captiondefault like '%Delete Standard Cost%' and Type = 0)
Begin
 DELETE FROM sys_Right WHERE Menu_EntryID IN (SELECT Menu_EntryID FROM sys_menu_entry WHERE Text_CaptionDefault like '%Delete Standard Cost%' and Type = 0)
 UPDATE sys_menu_entry SET Type = 1 WHERE Text_CaptionDefault like '%Delete Standard Cost%' and Type = 0
End
go


IF EXISTS (SELECT * FROM sys_Menu_Entry WHERE Text_Captiondefault like '%Shipping Schedule%' and Type = 0)
Begin
 UPDATE sys_Menu_Entry SET  Text_Captiondefault = 'Purchase Order Receiving Schedule',
 Text_Caption_VI_VN = 'Purchase Order Receiving Schedule (vn)',
 Text_Caption_EN_US = 'Purchase Order Receiving Schedule (en)',
 Text_Caption_JA_JP = 'Purchase Order Receiving Schedule (jp)'
 WHERE Text_Captiondefault like '%Shipping Schedule%'
End
go


IF NOT EXISTS (SELECT Menu_EntryID FROM Sys_Menu_Entry  WHERE Text_CaptionDefault like '%Detail Capacity Planning By Hours Report%')
Begin

  INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut],  [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
  VALUES ( 'RP', 'DCPH', 34, 'Detail Capacity Planning By Hours Report', 'Detail Capacity Planning By Hours Report', 'Detail Capacity Planning By Hours Report', 'Detail Capacity Planning By Hours Report', 'en_US', 3, 'PCSUtils.Framework.ReportFrame.ViewReport', NULL, 0, NULL, NULL, NULL, NULL, 0, 0, NULL, NULL, '20060320102627890')

  INSERT INTO [dbo].[Sys_Right] ([RoleID], [Permission], [Menu_EntryID] ) VALUES ( 1, 31, @@IDENTITY )
 End
go

IF NOT EXISTS (SELECT Menu_EntryID FROM Sys_Menu_Entry  WHERE Text_CaptionDefault like '%Inventory Adjustment Transaction History%')
Begin
  INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut],  [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
  VALUES ( 'IVRPT', 'IATH', 41, 'Inventory Adjustment Transaction History', 'Inventory Adjustment Transaction History', 'Inventory Adjustment Transaction History', 'Inventory Adjustment Transaction History', 'en_US', 3, 'PCSUtils.Framework.ReportFrame.ViewReport', NULL, 0, NULL, NULL, NULL, NULL, 0, 0, NULL, NULL, '20060315104201077')

  INSERT INTO [dbo].[Sys_Right] ([RoleID], [Permission], [Menu_EntryID] ) VALUES ( 1, 31, @@IDENTITY )
End
go


IF EXISTS (SELECT Menu_EntryID FROM Sys_Menu_Entry  WHERE Text_CaptionDefault like 'Inventory Status' AND Type = 1)
Begin
 UPDATE sys_Menu_Entry SET Type = 0, Parent_Shortcut = 'IVRPT', ReportID = '20060106114352030', FormLoad = 'PCSUtils.Framework.ReportFrame.ViewReport'
 WHERE Text_CaptionDefault like 'Inventory Status' AND Type = 1
 INSERT INTO [dbo].[Sys_Right] ([RoleID], [Permission], [Menu_EntryID] ) VALUES ( 1, 31, 209 )
End
go

IF NOT EXISTS (SELECT Menu_EntryID FROM Sys_Menu_Entry  WHERE Text_CaptionDefault like 'Inventory Status Summary by Master Location')
Begin
  INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut],  [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
  VALUES ( 'IVRPT', 'ISBML', 42, 'Inventory Status Summary by Master Location', 'Inventory Status Summary by Master Location', 'Inventory Status Summary by Master Location', 'Inventory Status Summary by Master Location', 'en_US', 3, 'PCSUtils.Framework.ReportFrame.ViewReport', NULL, 0, NULL, NULL, NULL, NULL, 0, 0, NULL, NULL, '20060106112624623')

  INSERT INTO [dbo].[Sys_Right] ([RoleID], [Permission], [Menu_EntryID] ) VALUES ( 1, 31, @@IDENTITY )

End
go

IF NOT EXISTS (SELECT Menu_EntryID FROM Sys_Menu_Entry  WHERE Text_CaptionDefault like 'Production Reports')
Begin
 INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut],  [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
  VALUES ( 'REPORT', 'PRODUCTION', 10, 'Production Reports', 'Production Reports', 'Production Reports', 'Production Reports', 'en_US', 2, NULL, NULL, 0, 2, 0, NULL, NULL, 0, 0, NULL, NULL, NULL )

  INSERT INTO [dbo].[Sys_Right] ([RoleID], [Permission], [Menu_EntryID] ) VALUES ( 1, 31, @@IDENTITY )
End
go

IF NOT EXISTS (SELECT Menu_EntryID FROM Sys_Menu_Entry  WHERE Text_CaptionDefault like 'Issue Material Transaction History')
Begin
  INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut],  [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
  VALUES ( 'PRODUCTION', 'IMTH', 1, 'Issue Material Transaction History', 'Issue Material Transaction History', 'Issue Material Transaction History', 'Issue Material Transaction History', 'en_US', 3, 'PCSUtils.Framework.ReportFrame.ViewReport', NULL, 0, NULL, NULL, NULL, NULL, 0, 0, NULL, NULL, '20060315160033250')

  INSERT INTO [dbo].[Sys_Right] ([RoleID], [Permission], [Menu_EntryID] ) VALUES ( 1, 31, @@IDENTITY )
End
go

-- Misc. Issue Transaction History
IF NOT EXISTS (SELECT Menu_EntryID FROM Sys_Menu_Entry  WHERE Text_CaptionDefault like 'Misc. Issue Transaction History')
Begin
  INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut],  [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
  VALUES ('IVRPT', 'MITH', 43, 'Misc. Issue Transaction History', 'Misc. Issue Transaction History', 'Misc. Issue Transaction History', 'Misc. Issue Transaction History', 'en_US', 3, 'PCSUtils.Framework.ReportFrame.ViewReport', NULL, 0, NULL, NULL, NULL, NULL, 0, 0, NULL, NULL, '20060316092109937')

  INSERT INTO [dbo].[Sys_Right] ([RoleID], [Permission], [Menu_EntryID] ) VALUES ( 1, 31, @@IDENTITY )
End
go

-- Production Line Assessment
IF NOT EXISTS (SELECT Menu_EntryID FROM Sys_Menu_Entry  WHERE Text_CaptionDefault like 'Production Line Assessment')
Begin
  INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut],  [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
  VALUES ('RP', 'PLA', 35, 'Production Line Assessment', 'Production Line Assessment', 'Production Line Assessment', 'Production Line Assessment', 'en_US', 3, 'PCSUtils.Framework.ReportFrame.ViewReport', NULL, 0, NULL, NULL, NULL, NULL, 0, 0, NULL, NULL, '20060315113731200')

  INSERT INTO [dbo].[Sys_Right] ([RoleID], [Permission], [Menu_EntryID] ) VALUES ( 1, 31, @@IDENTITY )
End
go

-- Purchase Order Receipt Transaction History
IF NOT EXISTS (SELECT Menu_EntryID FROM Sys_Menu_Entry  WHERE Text_CaptionDefault like 'Purchase Order Receipt Transaction History')
Begin
  INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut],  [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
  VALUES ('PRRPT', 'PORTH',2 , 'Purchase Order Receipt Transaction History', 'Purchase Order Receipt Transaction History', 'Purchase Order Receipt Transaction History', 'Purchase Order Receipt Transaction History', 'en_US', 3, 'PCSUtils.Framework.ReportFrame.ViewReport', NULL, 0, NULL, NULL, NULL, NULL, 0, 0, NULL, NULL, '20060317104452607')

  INSERT INTO [dbo].[Sys_Right] ([RoleID], [Permission], [Menu_EntryID] ) VALUES ( 1, 31, @@IDENTITY )
End
go

-- Add Material Group
IF NOT EXISTS (SELECT Menu_EntryID FROM Sys_Menu_Entry  WHERE Text_CaptionDefault like 'Material Reports')
Begin
 INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut],  [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
  VALUES ( 'REPORT', 'MATERIAL', 11, 'Material Reports', 'Material Reports', 'Material Reports', 'Material Reports', 'en_US', 2, NULL, NULL, 0, 2, 0, NULL, NULL, 0, 0, NULL, NULL, NULL )

  INSERT INTO [dbo].[Sys_Right] ([RoleID], [Permission], [Menu_EntryID] ) VALUES ( 1, 31, @@IDENTITY )
End
go

-- Recoverable Material Transaction History
IF NOT EXISTS (SELECT Menu_EntryID FROM Sys_Menu_Entry  WHERE Text_CaptionDefault like 'Recoverable Material Transaction History')
Begin
  INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut],  [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
  VALUES ('MATERIAL', 'RMTH', 1 , 'Recoverable Material Transaction History', 'Recoverable Material Transaction History', 'Recoverable Material Transaction History', 'Recoverable Material Transaction History', 'en_US', 3, 'PCSUtils.Framework.ReportFrame.ViewReport', NULL, 0, NULL, NULL, NULL, NULL, 0, 0, NULL, NULL, '20060316154830810')

  INSERT INTO [dbo].[Sys_Right] ([RoleID], [Permission], [Menu_EntryID] ) VALUES ( 1, 31, @@IDENTITY )
End
go


-- Return Goods Receipt Transaction History
IF NOT EXISTS (SELECT Menu_EntryID FROM Sys_Menu_Entry  WHERE Text_CaptionDefault like 'Return Goods Receipt Transaction History')
Begin
  INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut],  [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
  VALUES ('SRPT', 'RGRTH', 3 , 'Return Goods Receipt Transaction History', 'Return Goods Receipt Transaction History', 'Return Goods Receipt Transaction History', 'Return Goods Receipt Transaction History', 'en_US', 3, 'PCSUtils.Framework.ReportFrame.ViewReport', NULL, 0, NULL, NULL, NULL, NULL, 0, 0, NULL, NULL, '20060316112942170')

  INSERT INTO [dbo].[Sys_Right] ([RoleID], [Permission], [Menu_EntryID] ) VALUES ( 1, 31, @@IDENTITY )
End
go

--Return To Vendor Transaction History
IF NOT EXISTS (SELECT Menu_EntryID FROM Sys_Menu_Entry  WHERE Text_CaptionDefault like 'Return To Vendor Transaction History')
Begin
  INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut],  [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
  VALUES ('PRRPT', 'RTVTH', 4 , 'Return To Vendor Transaction History', 'Return To Vendor Transaction History', 'Return To Vendor Transaction History', 'Return To Vendor Transaction History', 'en_US', 3, 'PCSUtils.Framework.ReportFrame.ViewReport', NULL, 0, NULL, NULL, NULL, NULL, 0, 0, NULL, NULL, '20060317095811873')

  INSERT INTO [dbo].[Sys_Right] ([RoleID], [Permission], [Menu_EntryID] ) VALUES ( 1, 31, @@IDENTITY )
End
go

-- Sales Order Commitment Transaction History
IF NOT EXISTS (SELECT Menu_EntryID FROM Sys_Menu_Entry  WHERE Text_CaptionDefault like 'Sales Order Commitment Transaction History')
Begin
  INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut],  [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
  VALUES ('SRPT', 'SOCTH', 5 , 'Sales Order Commitment Transaction History', 'Sales Order Commitment Transaction History', 'Sales Order Commitment Transaction History', 'Sales Order Commitment Transaction History', 'en_US', 3, 'PCSUtils.Framework.ReportFrame.ViewReport', NULL, 0, NULL, NULL, NULL, NULL, 0, 0, NULL, NULL, '20060315102826293')

  INSERT INTO [dbo].[Sys_Right] ([RoleID], [Permission], [Menu_EntryID] ) VALUES ( 1, 31, @@IDENTITY )
End
go

-- Shipping Management Transaction History
IF NOT EXISTS (SELECT Menu_EntryID FROM Sys_Menu_Entry  WHERE Text_CaptionDefault like 'Shipping Management Transaction History')
Begin
  INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut],  [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
  VALUES ('SRPT', 'SMTH', 6 , 'Shipping Management Transaction History', 'Shipping Management Transaction History', 'Shipping Management Transaction History', 'Shipping Management Transaction History', 'en_US', 3, 'PCSUtils.Framework.ReportFrame.ViewReport', NULL, 0, NULL, NULL, NULL, NULL, 0, 0, NULL, NULL, '20060315100920780')

  INSERT INTO [dbo].[Sys_Right] ([RoleID], [Permission], [Menu_EntryID] ) VALUES ( 1, 31, @@IDENTITY )
End
go

-- Vendor Assessment
IF NOT EXISTS (SELECT Menu_EntryID FROM Sys_Menu_Entry  WHERE Text_CaptionDefault like 'Vendor Assessment')
Begin
  INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut],  [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
  VALUES ('RP', 'VA', 36 , 'Vendor Assessment', 'Vendor Assessment', 'Vendor Assessment', 'Vendor Assessment', 'en_US', 3, 'PCSUtils.Framework.ReportFrame.ViewReport', NULL, 0, NULL, NULL, NULL, NULL, 0, 0, NULL, NULL, '20060315113532797')

  INSERT INTO [dbo].[Sys_Right] ([RoleID], [Permission], [Menu_EntryID] ) VALUES ( 1, 31, @@IDENTITY )
End
go


-- Vendor Delivery Assessment
IF NOT EXISTS (SELECT Menu_EntryID FROM Sys_Menu_Entry  WHERE Text_CaptionDefault like 'Vendor Delivery Assessment')
Begin
  INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut],  [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
  VALUES ('RP', 'VDA', 37 , 'Vendor Delivery Assessment', 'Vendor Delivery Assessment', 'Vendor Delivery Assessment', 'Vendor Delivery Assessment', 'en_US', 3, 'PCSUtils.Framework.ReportFrame.ViewReport', NULL, 0, NULL, NULL, NULL, NULL, 0, 0, NULL, NULL, '20060315111928623')

  INSERT INTO [dbo].[Sys_Right] ([RoleID], [Permission], [Menu_EntryID] ) VALUES ( 1, 31, @@IDENTITY )
End
go


-- Work Order Completion Transaction History
IF NOT EXISTS (SELECT Menu_EntryID FROM Sys_Menu_Entry  WHERE Text_CaptionDefault like 'Work Order Completion Transaction History')
Begin
  INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut],  [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
  VALUES ('PRODUCTION', 'WOCTH', 37 , 'Work Order Completion Transaction History', 'Work Order Completion Transaction History', 'Work Order Completion Transaction History', 'Work Order Completion Transaction History', 'en_US', 3, 'PCSUtils.Framework.ReportFrame.ViewReport', NULL, 0, NULL, NULL, NULL, NULL, 0, 0, NULL, NULL, '20060315155943547')

  INSERT INTO [dbo].[Sys_Right] ([RoleID], [Permission], [Menu_EntryID] ) VALUES ( 1, 31, @@IDENTITY )
End
go

-- Move Order Summary to Sale group
IF NOT EXISTS (SELECT * FROM sys_menu_entry where Text_Captiondefault = 'Order Summary' AND Parent_Shortcut = 'SRPT')
 UPDATE Sys_Menu_Entry SET Parent_Shortcut = 'SRPT' WHERE Text_Captiondefault= 'Order Summary'
go


-- Move to Procurement group
IF NOT EXISTS (SELECT * FROM sys_menu_entry where (Text_Captiondefault = 'Schedule of Local Part in Month' 
OR Text_Captiondefault = 'Part Order Sheet Multi Vendor Report') AND Parent_Shortcut =  'PRRPT')
 UPDATE Sys_Menu_Entry SET Parent_Shortcut = 'PRRPT' WHERE (Text_Captiondefault = 'Schedule of Local Part in Month' 
OR Text_Captiondefault = 'Part Order Sheet Multi Vendor Report')
go

-- Outside Processing Management Report
IF NOT EXISTS (SELECT Menu_EntryID FROM Sys_Menu_Entry  WHERE Text_CaptionDefault like 'Outside Processing Management Report')
Begin
  INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut],  [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
  VALUES ('PRRPT', 'OPMR', 37 , 'Outside Processing Management Report', 'Outside Processing Management Report', 'Outside Processing Management Report', 'Outside Processing Management Report', 'en_US', 3, 'PCSUtils.Framework.ReportFrame.ViewReport', NULL, 0, NULL, NULL, NULL, NULL, 0, 0, NULL, NULL, '20060327175240793')

  INSERT INTO [dbo].[Sys_Right] ([RoleID], [Permission], [Menu_EntryID] ) VALUES ( 1, 31, @@IDENTITY )
End
go

-- Change Item List to List Of Materials
IF EXISTS (SELECT Menu_EntryID FROM Sys_Menu_Entry  WHERE Text_CaptionDefault like '%Item List%')
Begin
 UPDATE Sys_Menu_Entry SET Text_CaptionDefault = 'List Of Materials',
			Text_Caption_VI_VN = 'List Of Materials (vn)',
			Text_Caption_EN_US = 'List Of Materials',
			Text_Caption_JA_JP = 'List Of Materials (jp)'
 WHERE Text_Captiondefault like '%Item List%' 
End
go


--Detailed Item Price By Purchase Order Receipt
IF NOT EXISTS (SELECT Menu_EntryID FROM Sys_Menu_Entry  WHERE Text_CaptionDefault like 'Detailed Item Price By Purchase Order Receipt')
Begin
  INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut],  [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
  VALUES ('PRRPT', 'DIBPOR', 6 , 'Detailed Item Price By Purchase Order Receipt', 'Detailed Item Price By Purchase Order Receipt', 'Detailed Item Price By Purchase Order Receipt', 'Detailed Item Price By Purchase Order Receipt', 'en_US', 3, 'PCSUtils.Framework.ReportFrame.ViewReport', NULL, 0, NULL, NULL, NULL, NULL, 0, 0, NULL, NULL, '20060415162029937')

  INSERT INTO [dbo].[Sys_Right] ([RoleID], [Permission], [Menu_EntryID] ) VALUES ( 1, 31, @@IDENTITY )
End
go

-- Move Actual Cost into Product menu
IF NOT EXISTS (SELECT Menu_EntryID FROM Sys_Menu_Entry  WHERE Text_CaptionDefault like 'Actual Cost' AND Parent_Shortcut = 'PSM')
Begin
 UPDATE Sys_Menu_Entry SET Parent_Shortcut = 'PSM' WHERE Text_CaptionDefault like 'Actual Cost'
End
go


--In-Out Stock With Cost Report
IF NOT EXISTS (SELECT Menu_EntryID FROM Sys_Menu_Entry  WHERE Text_CaptionDefault like 'In-Out Stock With Cost Report')
Begin
  INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut],  [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
  VALUES ('IVRPT', 'IOWC', 10 , 'In-Out Stock With Cost Report', 'In-Out Stock With Cost Report', 'In-Out Stock With Cost Report', 'In-Out Stock With Cost Report', 'en_US', 3, 'PCSUtils.Framework.ReportFrame.ViewReport', NULL, 0, NULL, NULL, NULL, NULL, 0, 0, NULL, NULL, '20060417180732373')

  INSERT INTO [dbo].[Sys_Right] ([RoleID], [Permission], [Menu_EntryID] ) VALUES ( 1, 31, @@IDENTITY )
End
go

--User List
IF NOT EXISTS (SELECT Menu_EntryID FROM Sys_Menu_Entry  WHERE Text_CaptionDefault like 'Other Reports')
Begin

 INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut], [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
  VALUES ( 'REPORT', 'ORP', 8, 'Other Reports', 'Other Reports', 'Other Reports', 'Other Reports (JP)', 'en_US', 2, 'nothing', 'nothing', 0, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL )

  INSERT INTO [dbo].[Sys_Right] ([RoleID], [Permission], [Menu_EntryID] ) VALUES ( 1, 31, @@IDENTITY )

  INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut],  [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
  VALUES ('ORP', 'ULR', 10 , 'User List', 'User List', 'User List', 'User List', 'en_US', 3, 'PCSUtils.Framework.ReportFrame.ViewReport', NULL, 0, NULL, NULL, NULL, NULL, 0, 0, NULL, NULL, '20060418145330543')

  INSERT INTO [dbo].[Sys_Right] ([RoleID], [Permission], [Menu_EntryID] ) VALUES ( 1, 31, @@IDENTITY )
End
go


-- PO Management, Import Material Receipt, Receiving Material Receipt
IF NOT EXISTS (SELECT Menu_EntryID FROM Sys_Menu_Entry  WHERE Text_CaptionDefault like 'Receiving Material Report')
Begin
  INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut],  [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
  VALUES ('PRRPT', 'RMP', 38 , 'Receiving Material Report', 'Receiving Material Report', 'Receiving Material Report', 'Receiving Material Report', 'en_US', 3, 'PCSUtils.Framework.ReportFrame.ViewReport', NULL, 0, NULL, NULL, NULL, NULL, 0, 0, NULL, NULL, '20060508114200467')

  INSERT INTO [dbo].[Sys_Right] ([RoleID], [Permission], [Menu_EntryID] ) VALUES ( 1, 31, @@IDENTITY )
 
  INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut],  [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
  VALUES ('PRRPT', 'IMR', 39 , 'Import Material Receipt', 'Import Material Receipt', 'Import Material Receipt', 'Import Material Receipt', 'en_US', 3, 'PCSUtils.Framework.ReportFrame.ViewReport', NULL, 0, NULL, NULL, NULL, NULL, 0, 0, NULL, NULL, '20060415162029937')

  INSERT INTO [dbo].[Sys_Right] ([RoleID], [Permission], [Menu_EntryID] ) VALUES ( 1, 31, @@IDENTITY )

 INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut],  [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
  VALUES ('PRRPT', 'PRMR', 39 , 'Purchase Order Management', 'Purchase Order Management', 'Purchase Order Management', 'Purchase Order Management', 'en_US', 3, 'PCSUtils.Framework.ReportFrame.ViewReport', NULL, 0, NULL, NULL, NULL, NULL, 0, 0, NULL, NULL, '20060421143620590')

  INSERT INTO [dbo].[Sys_Right] ([RoleID], [Permission], [Menu_EntryID] ) VALUES ( 1, 31, @@IDENTITY )
End
go

-- Change title Cost Center Rate menu
IF EXISTS (SELECT Menu_EntryID FROM Sys_Menu_Entry  WHERE Text_CaptionDefault like '%Cost Center Rate%')
 UPDATE Sys_Menu_Entry SET Text_CaptionDefault = 'Cost Center Rate' , Text_Caption_VI_VN= 'Cost Center Rate (vn)',
 Text_Caption_EN_US = 'Cost Center Rate (en)', Text_Caption_JA_JP = 'Cost Center Rate (jp)'
 WHERE Text_CaptionDefault like 'Cost Center Rate'
go


-- Add Production Group Setup menu
IF NOT EXISTS (SELECT Menu_EntryID FROM Sys_Menu_Entry  WHERE Text_CaptionDefault like '%Production Group Setup%')
Begin
 INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut], [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
 VALUES ( 'DCP', 'PGS', 10, 'Production Group Setup', 'Production Group Setup (vn)', 'Production Group Setup (en)', 'Production Group Setup (jp)', 'en_US', 3, 'PCSProduction.DCP.ProductionGroup', 'nothing', 0, 158, 158, NULL, NULL, 0, 0, NULL, NULL, NULL )

 INSERT INTO [dbo].[Sys_Right] ([RoleID], [Permission], [Menu_EntryID] ) VALUES ( 1, 31, @@IDENTITY )

End
go


IF EXISTS (SELECT Menu_EntryID FROM Sys_Menu_Entry  WHERE Text_CaptionDefault like '%Cost Distribution Setup%' And Type = 0)
 UPDATE Sys_Menu_Entry SET Text_CaptionDefault = 'Cost Allocation Setup' , Text_Caption_VI_VN= 'Cost Allocation Setup (vn)',
 Text_Caption_EN_US = 'Cost Allocation Setup (en)', Text_Caption_JA_JP = 'Cost Allocation Setup (jp)'
 WHERE Text_CaptionDefault like '%Cost Distribution Setup%'
go


-- Change Freight to Addtitional Charge
IF EXISTS (SELECT Menu_EntryID FROM Sys_Menu_Entry  WHERE Text_CaptionDefault like '%Freight%' And Type = 0)
 UPDATE Sys_Menu_Entry SET Text_CaptionDefault = 'Additional Charge' , Text_Caption_VI_VN= 'Additional Charge (vn)',
 Text_Caption_EN_US = 'Additional Charge (en)', Text_Caption_JA_JP = 'Additional Charge (jp)'
 WHERE Text_CaptionDefault like '%Freight%'
go

IF NOT EXISTS (SELECT Menu_EntryID FROM Sys_Menu_Entry  WHERE Text_CaptionDefault like '%Additional%' And Type = 0 AND Parent_Shortcut = 'PRM')
 UPDATE Sys_Menu_Entry SET Parent_Shortcut = 'PRM', Button_Caption = 14 WHERE Text_CaptionDefault like '%Additional%' And Type = 0
go


-- Change Release WO to Release/UnRelease WO
IF EXISTS (SELECT Menu_EntryID FROM Sys_Menu_Entry  WHERE Text_CaptionDefault like  '%Release Work Order%' And Type = 0)
 UPDATE Sys_Menu_Entry SET Text_CaptionDefault = 'Release/Unrelease Work Order' , Text_Caption_VI_VN= 'Release/Unrelease Work Order (vn)',
 Text_Caption_EN_US = 'Release/Unrelease Work Order (en)', Text_Caption_JA_JP = 'Release/Unrelease Work Order (jp)'
 WHERE Text_CaptionDefault like '%Release Work Order%' and Type = 0
go

-- Insert report menu
IF NOT EXISTS (SELECT Menu_EntryID FROM Sys_Menu_Entry  WHERE Menu_EntryID = 682)
Begin
SET IDENTITY_INSERT Sys_Menu_Entry ON
INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut], [Menu_EntryID], [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
 VALUES ( 'PRRPT', 682, '20060421180409437', 2, 'Part Order Sheet Multi Vendor Report', 'Part Order Sheet Multi Vendor Report (vn)', 'Part Order Sheet Multi Vendor Report', '20060421180409437', 'en_US', 3, 'PCSUtils.Framework.ReportFrame.ViewReport', 'Description', 0, 0, 0, '0', '0', 0, 0, '0', '0', '20060421180409437' )

INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut], [Menu_EntryID], [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
 VALUES ( 'SRPT', 683, '20060514115948000', 3, 'Sale Transaction History', 'Sale Transaction History (vn)', 'Sale Transaction History', '20060514115948000', 'en_US', 3, 'PCSUtils.Framework.ReportFrame.ViewReport', 'Description', 0, 0, 0, '0', '0', 0, 0, '0', '0', '20060514115948000' )

INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut], [Menu_EntryID], [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
 VALUES ( 'SRPT', 684, '20060516092014340', 4, 'Sale Order Management Report', 'Sale Order Management Report (vn)', 'Sale Order Management Report', '20060516092014340', 'en_US', 3, 'PCSUtils.Framework.ReportFrame.ViewReport', 'Description', 0, 0, 0, '0', '0', 0, 0, '0', '0', '20060516092014340' )

INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut], [Menu_EntryID], [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
 VALUES ( 'PRRPT', 685, '20060605100737827', 5, 'Attachment of Importing-Invoice', 'Attachment of Importing-Invoice (vn)', 'Attachment of Importing-Invoice', '20060605100737827', 'en_US', 3, 'PCSUtils.Framework.ReportFrame.ViewReport', 'Description', 0, 0, 0, '0', '0', 0, 0, '0', '0', '20060605100737827' )

INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut], [Menu_EntryID], [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
 VALUES ( 'PRRPT', 686, '20060605110907733', 6, 'Delivery Evaluation As Items Report', 'Delivery Evaluation As Items Report (vn)', 'Delivery Evaluation As Items Report', '20060605110907733', 'en_US', 3, 'PCSUtils.Framework.ReportFrame.ViewReport', 'Description', 0, 0, 0, '0', '0', 0, 0, '0', '0', '20060605110907733' )

INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut], [Menu_EntryID], [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
 VALUES ( 'SRPT', 687, '20060606141610700', 7, 'Sale Revenue Report (Classified By Customers)', 'Sale Revenue Report (Classified By Customers) (vn)', 'Sale Revenue Report (Classified By Customers)', '20060606141610700', 'en_US', 3, 'PCSUtils.Framework.ReportFrame.ViewReport', 'Description', 0, 0, 0, '0', '0', 0, 0, '0', '0', '20060606141610700' )

INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut], [Menu_EntryID], [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
 VALUES ( 'SRPT', 688, '20060606173621403', 8, 'VAT Report (Classified by Products)', 'VAT Report (Classified by Products) (vn)', 'VAT Report (Classified by Products)', '20060606173621403', 'en_US', 3, 'PCSUtils.Framework.ReportFrame.ViewReport', 'Description', 0, 0, 0, '0', '0', 0, 0, '0', '0', '20060606173621403' )

INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut], [Menu_EntryID], [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
 VALUES ( 'SRPT', 689, '20060609100024060', 9, 'Sale Revenue Report (Classified By Licensor)', 'Sale Revenue Report (vn)', 'Sale Revenue Report (Classified By Licensor)', '20060609100024060', 'en_US', 3, 'PCSUtils.Framework.ReportFrame.ViewReport', 'Description', 0, 0, 0, '0', '0', 0, 0, '0', '0', '20060609100024060' )

INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut], [Menu_EntryID], [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
 VALUES ( 'SRPT', 690, '20060609152315577', 10, 'Sale Revenue And Cost Of Goods Sold Break-down', 'Sale Revenue And Cost Of Goods Sold Break-down', 'Sale Revenue And Cost Of Goods Sold Break-down', '20060609152315577', 'en_US', 3, 'PCSUtils.Framework.ReportFrame.ViewReport', 'Description', 0, 0, 0, '0', '0', 0, 0, '0', '0', '20060609152315577' )

INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut], [Menu_EntryID], [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
 VALUES ( 'SRPT', 691, '20060609152749950', 11, 'Sale Revenue And Cost Of Goods Sold Break-down', 'Sale Revenue And Cost Of Goods Sold Break-down', 'Sale Revenue And Cost Of Goods Sold Break-down', '20060609152749950', 'en_US', 3, 'PCSUtils.Framework.ReportFrame.ViewReport', 'Description', 0, 0, 0, '0', '0', 0, 0, '0', '0', '20060609152749950' )

INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut], [Menu_EntryID], [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
 VALUES ( 'PRRPT', 692, '20060611163303640', 12, 'Delivery Evaluation As Month Report', 'Delivery Evaluation As Month Report (vn)', 'Delivery Evaluation As Month Report', '20060611163303640', 'en_US', 3, 'PCSUtils.Framework.ReportFrame.ViewReport', 'Description', 0, 0, 0, '0', '0', 0, 0, '0', '0', '20060611163303640' )

INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut], [Menu_EntryID], [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
 VALUES ( 'PRRPT', 693, '20060611163856153', 13, 'Purchase Report Import Part As Item', 'Purchase Report Import Part As Item (vn)', 'Purchase Report Import Part As Item', '20060611163856153', 'en_US', 3, 'PCSUtils.Framework.ReportFrame.ViewReport', 'Description', 0, 0, 0, '0', '0', 0, 0, '0', '0', '20060611163856153' )

INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut], [Menu_EntryID], [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
 VALUES ( 'PRRPT', 694, '20060611162954140', 14, 'Delivery Evaluation As Suppliers Report', 'Delivery Evaluation As Suppliers Report (vn)', 'Delivery Evaluation As Suppliers Report', '20060611162954140', 'en_US', 3, 'PCSUtils.Framework.ReportFrame.ViewReport', 'Description', 0, 0, 0, '0', '0', 0, 0, '0', '0', '20060611162954140' )

INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut], [Menu_EntryID], [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
 VALUES ( 'PRRPT', 695, '20060611164200077', 15, 'PO Firm Order', 'PO Firm Order (vn)', 'PO Firm Order', '20060611164200077', 'en_US', 3, 'PCSUtils.Framework.ReportFrame.ViewReport', 'Description', 0, 0, 0, '0', '0', 0, 0, '0', '0', '20060611164200077' )

INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut], [Menu_EntryID], [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
 VALUES ( 'PRRPT', 696, '20060611164542873', 16, 'Purchase Report Import Part By Maker', 'Purchase Report Import Part By Maker (vn)', 'Purchase Report Import Part By Maker', '20060611164542873', 'en_US', 3, 'PCSUtils.Framework.ReportFrame.ViewReport', 'Description', 0, 0, 0, '0', '0', 0, 0, '0', '0', '20060611164542873' )

INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut], [Menu_EntryID], [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
 VALUES ( 'PRRPT', 697, '20060611164724967', 17, 'Purchase Report Import Part By Month', 'Purchase Report Import Part By Month (vn)', 'Purchase Report Import Part By Month', '20060611164724967', 'en_US', 3, 'PCSUtils.Framework.ReportFrame.ViewReport', 'Description', 0, 0, 0, '0', '0', 0, 0, '0', '0', '20060611164724967' )

INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut], [Menu_EntryID], [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
 VALUES ( 'PRRPT', 698, '20060611164829967', 18, 'Purchasing Price Trend In Year Report', 'Purchasing Price Trend In Year Report (vn)', 'Purchasing Price Trend In Year Report', '20060611164829967', 'en_US', 3, 'PCSUtils.Framework.ReportFrame.ViewReport', 'Description', 0, 0, 0, '0', '0', 0, 0, '0', '0', '20060611164829967' )

INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut], [Menu_EntryID], [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
 VALUES ( 'PRRPT', 699, '20060612105404390', 19, 'Import Material Report', 'Import Material Report (vn)', 'Import Material Report', '20060612105404390', 'en_US', 3, 'PCSUtils.Framework.ReportFrame.ViewReport', 'Description', 0, 0, 0, '0', '0', 0, 0, '0', '0', '20060612105404390' )

INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut], [Menu_EntryID], [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
 VALUES ( 'PRRPT', 700, '20060612153634123', 20, 'Order Balance For Import Parts', 'Order Balance For Import Parts (vn)', 'Order Balance For Import Parts', '20060612153634123', 'en_US', 3, 'PCSUtils.Framework.ReportFrame.ViewReport', 'Description', 0, 0, 0, '0', '0', 0, 0, '0', '0', '20060612153634123' )
SET IDENTITY_INSERT Sys_Menu_Entry OFF
End
GO

-- Update Sale Revenue Report caption
IF EXISTS (SELECT Menu_EntryID FROM Sys_Menu_Entry  WHERE Menu_EntryID = 690)
Begin
UPDATE [dbo].[Sys_Menu_Entry] SET Text_CaptionDefault = 'Sale Revenue & CGS Break-down (Classified By Customers)',
								  Text_Caption_VI_VN = 'Sale Revenue & CGS Break-down (Classified By Customers)',
								  Text_Caption_EN_US = 'Sale Revenue & CGS Break-down (Classified By Customers)'
	WHERE Menu_EntryID = 690
UPDATE [dbo].[Sys_Menu_Entry] SET Text_CaptionDefault = 'Sale Revenue & CGS Break-down (Classified By Licensor)',
								  Text_Caption_VI_VN = 'Sale Revenue & CGS Break-down (Classified By Licensor)',
								  Text_Caption_EN_US = 'Sale Revenue & CGS Break-down (Classified By Licensor)'
	WHERE Menu_EntryID = 691
End
go

-- Insert rest report menu by duongna 7-7-2006
IF NOT EXISTS (SELECT Menu_EntryID FROM Sys_Menu_Entry  WHERE Menu_EntryID = 701)
Begin
SET IDENTITY_INSERT Sys_Menu_Entry ON

--cst
INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut], [Menu_EntryID], [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
 VALUES ( 'CSTRPT', 701, '20060704171811640', 21, 'Unallocated Items Report', 'Unallocated Items Report (vn)', 'Unallocated Items Report', 'Unallocated Items Report', 'en_US', 3, 'PCSUtils.Framework.ReportFrame.ViewReport', 'Description', 0, 0, 0, '0', '0', 0, 0, '0', '0', '20060704171811640' )

--iv
INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut], [Menu_EntryID], [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
 VALUES ( 'IVRPT', 702, '20060613181207263', 22, 'Stock Card Report', 'Stock Card Report', 'Stock Card Report', 'Stock Card Report', 'en_US', 3, 'PCSUtils.Framework.ReportFrame.ViewReport', 'Description', 0, 0, 0, '0', '0', 0, 0, '0', '0', '20060613181207263' )

--production
INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut], [Menu_EntryID], [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
 VALUES ( 'PRODUCTION', 703, '20051206152944700', 23, 'Working scheme', 'Working scheme', 'Working scheme', 'Working scheme', 'en_US', 3, 'PCSUtils.Framework.ReportFrame.ViewReport', 'Description', 0, 0, 0, '0', '0', 0, 0, '0', '0', '20051206152944700' )

INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut], [Menu_EntryID], [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
 VALUES ( 'PRODUCTION', 704, '20051214171951937', 24, 'Production Line Progress Management', 'Production Line Progress Management', 'Production Line Progress Management', 'Production Line Progress Management', 'en_US', 3, 'PCSUtils.Framework.ReportFrame.ViewReport', 'Description', 0, 0, 0, '0', '0', 0, 0, '0', '0', '20051214171951937' )

INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut], [Menu_EntryID], [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
 VALUES ( 'PRODUCTION', 705, '20051206153412390', 25, 'Production Line Capacity Management', 'Production Line Capacity Management', 'Production Line Capacity Management', 'Production Line Capacity Management', 'en_US', 3, 'PCSUtils.Framework.ReportFrame.ViewReport', 'Description', 0, 0, 0, '0', '0', 0, 0, '0', '0', '20051206153412390' )

--po
INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut], [Menu_EntryID], [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
 VALUES ( 'PRRPT', 706, '20060421143620590', 26, 'Purchase Order Management', 'Purchase Order Management', 'Purchase Order Management', 'Purchase Order Management', 'en_US', 3, 'PCSUtils.Framework.ReportFrame.ViewReport', 'Description', 0, 0, 0, '0', '0', 0, 0, '0', '0', '20060421143620590' )

INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut], [Menu_EntryID], [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
 VALUES ( 'PRRPT', 707, '20060508114200467', 27, 'Receiving Material Report', 'Receiving Material Report', 'Receiving Material Report', 'Receiving Material Report', 'en_US', 3, 'PCSUtils.Framework.ReportFrame.ViewReport', 'Description', 0, 0, 0, '0', '0', 0, 0, '0', '0', '20060508114200467' )

INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut], [Menu_EntryID], [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
 VALUES ( 'PRRPT', 708, '20060612105404390', 28, 'Import Material Report', 'Import Material Report', 'Import Material Report', 'Import Material Report', 'en_US', 3, 'PCSUtils.Framework.ReportFrame.ViewReport', 'Description', 0, 0, 0, '0', '0', 0, 0, '0', '0', '20060612105404390' )

INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut], [Menu_EntryID], [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
 VALUES ( 'PRRPT', 709, '20060605100737827', 29, 'Attachment of Importing Invoice', 'Attachment of Importing Invoice', 'Attachment of Importing Invoice', 'Attachment of Importing Invoice', 'en_US', 3, 'PCSUtils.Framework.ReportFrame.ViewReport', 'Description', 0, 0, 0, '0', '0', 0, 0, '0', '0', '20060605100737827' )

--so
INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut], [Menu_EntryID], [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
 VALUES ( 'SRPT', 710, '20060621175625340', 30, 'Monthly Sale Report', 'Monthly Sale Report', 'Monthly Sale Report', 'Monthly Sale Report', 'en_US', 3, 'PCSUtils.Framework.ReportFrame.ViewReport', 'Description', 0, 0, 0, '0', '0', 0, 0, '0', '0', '20060621175625340' )

INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut], [Menu_EntryID], [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
 VALUES ( 'SRPT', 711, '20060622112838983', 31, 'Monthly Return Goods Receipt', 'Monthly Return Goods Receipt', 'Monthly Return Goods Receipt', 'Monthly Return Goods Receipt', 'en_US', 3, 'PCSUtils.Framework.ReportFrame.ViewReport', 'Description', 0, 0, 0, '0', '0', 0, 0, '0', '0', '20060622112838983' )

SET IDENTITY_INSERT Sys_Menu_Entry OFF
End

GO

-- Inventory Stock Taking Period, Stock Taking
IF NOT EXISTS (SELECT Menu_EntryID FROM Sys_Menu_Entry  WHERE Text_CaptionDefault like '%Stock Taking%')
Begin
	SET IDENTITY_INSERT Sys_Menu_Entry ON
-- stock taking period
	INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut], [Menu_EntryID], [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
 VALUES ( 'IV', 712, 'ISTP', 4, 'Stock Taking Period', 'Stock Taking Period', 'Stock Taking Period', 'Stock Taking Period', 'en_US', 3, 'PCSMaterials.Inventory.StockTakingPeriod', 'Stock Taking Period', 0, 0, 0, NULL, NULL, 0, 0, NULL, NULL, NULL )
-- stock taking
	INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut], [Menu_EntryID], [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
 VALUES ( 'IV', 713, 'IST', 5, 'Stock Taking', 'Stock Taking', 'Stock Taking', 'Stock Taking', 'en_US', 3, 'PCSMaterials.Inventory.StockTaking', 'Stock Taking', 0, 0, 0, NULL, NULL, 1, 0, NULL, NULL, NULL )
-- insert right
INSERT INTO [dbo].[Sys_Right] ([RoleID], [Permission], [Menu_EntryID] )
 VALUES (1, 31, 713 )

INSERT INTO [dbo].[Sys_Right] ([RoleID], [Permission], [Menu_EntryID] )
 VALUES (1, 31, 712 )
	SET IDENTITY_INSERT Sys_Menu_Entry OFF
End
ELSE
BEGIN
	UPDATE Sys_Menu_Entry
	SET IsTransaction = 1,
	[TableName] = 'IV_StockTakingMaster',
	[TransNoFieldName] = 'Code'
	WHERE Menu_EntryID = 713
END
go
-- insert local receiveing report to menu
IF NOT EXISTS (SELECT Menu_EntryID FROM Sys_Menu_Entry  WHERE Text_CaptionDefault like '%Local Receiving Material Report%')
Begin
	SET IDENTITY_INSERT Sys_Menu_Entry ON
	INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut], [Menu_EntryID], [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
	 VALUES ( 'PRRPT', 714, '20060712103905357', 31, 'Local Receiving Material Report', 'Local Receiving Material Report', 'Local Receiving Material Report', 'Local Receiving Material Report', 'en_US', 3, 'PCSUtils.Framework.ReportFrame.ViewReport', 'Description', 0, 0, 0, '0', '0', 0, 0, '0', '0', '20060712103905357' )
	SET IDENTITY_INSERT Sys_Menu_Entry OFF
End
go
IF NOT EXISTS (SELECT Menu_EntryID FROM Sys_Menu_Entry  WHERE Text_CaptionDefault like '%Manual Production Planning%')
Begin
INSERT INTO sys_menu_entry(Parent_Shortcut,Shortcut,Button_Caption,Text_CaptionDefault,Text_Caption_VI_VN,Text_Caption_EN_US,Text_Caption_JA_JP,Text_Caption_Language_Default,Parent_Child,FormLoad,Description,Type,CollapsedImage,ExpandedImage,Prefix,TransFormat,IsTransaction,IsUserCreated,TableName,TransNoFieldName,ReportID)
SELECT Parent_Shortcut,'DPP' Shortcut,11 Button_Caption,'Manual Production Planning' Text_CaptionDefault,'Manual Production Planning' Text_Caption_VI_VN,'Manual Production Planning' Text_Caption_EN_US,'Manual Production Planning' Text_Caption_JA_JP,'Manual Production Planning' Text_Caption_Language_Default,Parent_Child,'PCSProduction.DCP.ManualProductionPlanning' FormLoad,'nt' Description,Type,CollapsedImage,ExpandedImage,Prefix,TransFormat,IsTransaction,IsUserCreated,TableName,TransNoFieldName,ReportID FROM sys_menu_entry where Text_CaptionDefault='Production group setup'
End
go

-- Onhand Period
IF NOT EXISTS (SELECT Menu_EntryID FROM Sys_Menu_Entry  WHERE Text_CaptionDefault like '%Onhand Period%')
Begin
	SET IDENTITY_INSERT Sys_Menu_Entry ON
-- Onhand Period
	INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut], [Menu_EntryID], [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
 VALUES ( 'IV', 716, 'IOP', 4, 'Onhand Period', 'Onhand Period', 'Onhand Period', 'Onhand Period', 'en_US', 3, 'PCSMaterials.Inventory.OpenAndClosePeriod', 'Onhand Period', 0, 0, 0, NULL, NULL, 0, 0, NULL, NULL, NULL )
-- insert right
INSERT INTO [dbo].[Sys_Right] ([RoleID], [Permission], [Menu_EntryID] )
 VALUES (1, 31, 716 )
	SET IDENTITY_INSERT Sys_Menu_Entry OFF
End
go
-- Import Planning Data
IF NOT EXISTS (SELECT Menu_EntryID FROM Sys_Menu_Entry  WHERE Text_CaptionDefault like '%Import Planning Data%')
Begin
	SET IDENTITY_INSERT Sys_Menu_Entry ON
-- Onhand Period
	INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut], [Menu_EntryID], [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
 VALUES ( 'DCP', 717, 'IPD', 12, 'Import Planning Data', 'Import Planning Data', 'Import Planning Data', 'Import Planning Data', 'en_US', 3, 'PCSProduction.DCP.ImportPlanData', 'Import Planning Data', 0, 0, 0, NULL, NULL, 0, 0, NULL, NULL, NULL )
-- insert right
INSERT INTO [dbo].[Sys_Right] ([RoleID], [Permission], [Menu_EntryID] )
 VALUES (1, 31, 717 )
	SET IDENTITY_INSERT Sys_Menu_Entry OFF
End
go

-- Insert rest report menu by dungla 7-Feb-2007
IF NOT EXISTS (SELECT Menu_EntryID FROM Sys_Menu_Entry  WHERE Menu_EntryID = 718)
BEGIN
	SET IDENTITY_INSERT Sys_Menu_Entry ON
	-- costing report
	INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut], [Menu_EntryID], [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
	VALUES ( 'CSTRPT',718, '20060813185324967',718, 'Actual Cost For Make Item', 'Actual Cost For Make Item', 'Actual Cost For Make Item', 'Actual Cost For Make Item', 'en_US', 3, 'PCSUtils.Framework.ReportFrame.ViewReport', 'Description', 0, 0, 0, '0', '0', 0, 0, '0', '0', '20060813185324967' )
	INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut], [Menu_EntryID], [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
	VALUES ( 'CSTRPT',719, '20060813184515733',719, 'Actual Cost For Make Item(BOD)', 'Actual Cost For Make Item(BOD)', 'Actual Cost For Make Item(BOD)', 'Actual Cost For Make Item(BOD)', 'en_US', 3, 'PCSUtils.Framework.ReportFrame.ViewReport', 'Description', 0, 0, 0, '0', '0', 0, 0, '0', '0', '20060813184515733' )
	INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut], [Menu_EntryID], [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
	VALUES ( 'CSTRPT',720, '20060813184344000',720, 'Actual Cost For Non-Make Item', 'Actual Cost For Non-Make Item', 'Actual Cost For Non-Make Item', 'Actual Cost For Non-Make Item', 'en_US', 3, 'PCSUtils.Framework.ReportFrame.ViewReport', 'Description', 0, 0, 0, '0', '0', 0, 0, '0', '0', '20060813184344000' )
	INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut], [Menu_EntryID], [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
	VALUES ( 'CSTRPT',721, '20060813184807983',721, 'Allocated Expenses Costing', 'Allocated Expenses Costing', 'Allocated Expenses Costing', 'Allocated Expenses Costing', 'en_US', 3, 'PCSUtils.Framework.ReportFrame.ViewReport', 'Description', 0, 0, 0, '0', '0', 0, 0, '0', '0', '20060813184807983' )
	INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut], [Menu_EntryID], [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
	VALUES ( 'CSTRPT',722, '20061201111430357',722, 'Bao Cao Gia Thanh San Xuat Trong Ky', 'Bao Cao Gia Thanh San Xuat Trong Ky', 'Bao Cao Gia Thanh San Xuat Trong Ky', 'Bao Cao Gia Thanh San Xuat Trong Ky', 'en_US', 3, 'PCSUtils.Framework.ReportFrame.ViewReport', 'Description', 0, 0, 0, '0', '0', 0, 0, '0', '0', '20061201111430357' )
	INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut], [Menu_EntryID], [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
	VALUES ( 'CSTRPT',723, '20061208091625000',723, 'Tong Hop Xuat Hang Gia Cong Tai Map-Theo Thang', 'Tong Hop Xuat Hang Gia Cong Tai Map-Theo Thang', 'Tong Hop Xuat Hang Gia Cong Tai Map-Theo Thang', 'Tong Hop Xuat Hang Gia Cong Tai Map-Theo Thang', 'en_US', 3, 'PCSUtils.Framework.ReportFrame.ViewReport', 'Description', 0, 0, 0, '0', '0', 0, 0, '0', '0', '20061208091625000' )
	INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut], [Menu_EntryID], [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
	VALUES ( 'CSTRPT',724, '20060831163624123',724, 'Charge Allocated Into CGS', 'Charge Allocated Into CGS', 'Charge Allocated Into CGS', 'Charge Allocated Into CGS', 'en_US', 3, 'PCSUtils.Framework.ReportFrame.ViewReport', 'Description', 0, 0, 0, '0', '0', 0, 0, '0', '0', '20060831163624123' )
	INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut], [Menu_EntryID], [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
	VALUES ( 'CSTRPT',725, '20060831155504827',725, 'Component Scrap Amount', 'Component Scrap Amount', 'Component Scrap Amount', 'Component Scrap Amount', 'en_US', 3, 'PCSUtils.Framework.ReportFrame.ViewReport', 'Description', 0, 0, 0, '0', '0', 0, 0, '0', '0', '20060831155504827' )
	INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut], [Menu_EntryID], [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
	VALUES ( 'CSTRPT',726, '20060830172610263',726, 'Costing Description', 'Costing Description', 'Costing Description', 'Costing Description', 'en_US', 3, 'PCSUtils.Framework.ReportFrame.ViewReport', 'Description', 0, 0, 0, '0', '0', 0, 0, '0', '0', '20060830172610263' )
	INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut], [Menu_EntryID], [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
	VALUES ( 'CSTRPT',727, '20060810094357890',727, 'Costing Description - BOD', 'Costing Description - BOD', 'Costing Description - BOD', 'Costing Description - BOD', 'en_US', 3, 'PCSUtils.Framework.ReportFrame.ViewReport', 'Description', 0, 0, 0, '0', '0', 0, 0, '0', '0', '20060810094357890' )
	INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut], [Menu_EntryID], [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
	VALUES ( 'RP',728, '20061130163648170',728, 'Detail Capacity Planning By Shift Report', 'Detail Capacity Planning By Shift Report', 'Detail Capacity Planning By Shift Report', 'Detail Capacity Planning By Shift Report', 'en_US', 3, 'PCSUtils.Framework.ReportFrame.ViewReport', 'Description', 0, 0, 0, '0', '0', 0, 0, '0', '0', '20061130163648170' )
	INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut], [Menu_EntryID], [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
	VALUES ( 'CSTRPT',729, '20060831162729467',729, 'DSRate Allocation', 'DSRate Allocation', 'DSRate Allocation', 'DSRate Allocation', 'en_US', 3, 'PCSUtils.Framework.ReportFrame.ViewReport', 'Description', 0, 0, 0, '0', '0', 0, 0, '0', '0', '20060831162729467' )
	INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut], [Menu_EntryID], [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
	VALUES ( 'IVRPT',730, '20061124143451733',730, 'In Out Stock With Cost Classified by CCN', 'In Out Stock With Cost Classified by CCN', 'In Out Stock With Cost Classified by CCN', 'In Out Stock With Cost Classified by CCN', 'en_US', 3, 'PCSUtils.Framework.ReportFrame.ViewReport', 'Description', 0, 0, 0, '0', '0', 0, 0, '0', '0', '20061124143451733' )
	INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut], [Menu_EntryID], [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
	VALUES ( 'IVRPT',731, '20061012153040513',731, 'In-Out Stock With Cost Report By Item', 'In-Out Stock With Cost Report By Item', 'In-Out Stock With Cost Report By Item', 'In-Out Stock With Cost Report By Item', 'en_US', 3, 'PCSUtils.Framework.ReportFrame.ViewReport', 'Description', 0, 0, 0, '0', '0', 0, 0, '0', '0', '20061012153040513' )
	INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut], [Menu_EntryID], [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
	VALUES ( 'IVRPT',732, '20070111102305077',732, 'In-Out-Stock By Type For Accounting-In Month', 'In-Out-Stock By Type For Acc-In Month', 'In-Out-Stock By Type For Acc-In Month', 'In-Out-Stock By Type For Acc-In Month', 'en_US', 3, 'PCSUtils.Framework.ReportFrame.ViewReport', 'Description', 0, 0, 0, '0', '0', 0, 0, '0', '0', '20070111102305077' )
	INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut], [Menu_EntryID], [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
	VALUES ( 'IVRPT',733, '20070104145802280',733, 'In-Out-Stock-Summary By TransactionType-In Month', 'In-Out-Stock-Summary By TransactionType-In Month', 'In-Out-Stock-Summary By TransactionType-In Month', 'In-Out-Stock-Summary By TransactionType-In Month', 'en_US', 3, 'PCSUtils.Framework.ReportFrame.ViewReport', 'Description', 0, 0, 0, '0', '0', 0, 0, '0', '0', '20070104145802280' )
	INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut], [Menu_EntryID], [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
	VALUES ( 'CSTRPT',734, '20060830142153373',734, 'Inventory Adjustment Amount', 'Inventory Adjustment Amount', 'Inventory Adjustment Amount', 'Inventory Adjustment Amount', 'en_US', 3, 'PCSUtils.Framework.ReportFrame.ViewReport', 'Description', 0, 0, 0, '0', '0', 0, 0, '0', '0', '20060830142153373' )
	INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut], [Menu_EntryID], [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
	VALUES ( 'PRRPT',735, '20060929114949327',735, 'Maker Delivery Plan In Month', 'Maker Delivery Plan In Month', 'Maker Delivery Plan In Month', 'Maker Delivery Plan In Month', 'en_US', 3, 'PCSUtils.Framework.ReportFrame.ViewReport', 'Description', 0, 0, 0, '0', '0', 0, 0, '0', '0', '20060929114949327' )
	INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut], [Menu_EntryID], [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
	VALUES ( 'CSTRPT',736, '20060831170216467',736, 'Miscellaneous_DS_Amount', 'Miscellaneous_DS_Amount', 'Miscellaneous_DS_Amount', 'Miscellaneous_DS_Amount', 'en_US', 3, 'PCSUtils.Framework.ReportFrame.ViewReport', 'Description', 0, 0, 0, '0', '0', 0, 0, '0', '0', '20060831170216467' )
	INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut], [Menu_EntryID], [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
	VALUES ( 'CSTRPT',737, '20060831170815623',737, 'Miscellanoeus_Recycle_Amount', 'Miscellanoeus_Recycle_Amount', 'Miscellanoeus_Recycle_Amount', 'Miscellanoeus_Recycle_Amount', 'en_US', 3, 'PCSUtils.Framework.ReportFrame.ViewReport', 'Description', 0, 0, 0, '0', '0', 0, 0, '0', '0', '20060831170815623' )
	INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut], [Menu_EntryID], [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
	VALUES ( 'RP',738, '20061009143001217',738, 'NIGURI 2 Report', 'NIGURI 2 Report', 'NIGURI 2 Report', 'NIGURI 2 Report', 'en_US', 3, 'PCSUtils.Framework.ReportFrame.ViewReport', 'Description', 0, 0, 0, '0', '0', 0, 0, '0', '0', '20061009143001217' )
	INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut], [Menu_EntryID], [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
	VALUES ( 'PRODUCTION',739, '20061109094110013',739, 'Production Order Report', 'Production Order Report', 'Production Order Report', 'Production Order Report', 'en_US', 3, 'PCSUtils.Framework.ReportFrame.ViewReport', 'Description', 0, 0, 0, '0', '0', 0, 0, '0', '0', '20061109094110013' )
	INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut], [Menu_EntryID], [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
	VALUES ( 'CSTRPT',740, '20060831161559530',740, 'Recycle Component Amount', 'Recycle Component Amount', 'Recycle Component Amount', 'Recycle Component Amount', 'en_US', 3, 'PCSUtils.Framework.ReportFrame.ViewReport', 'Description', 0, 0, 0, '0', '0', 0, 0, '0', '0', '20060831161559530' )
	INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut], [Menu_EntryID], [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
	VALUES ( 'CSTRPT',741, '20060907140633060',741, 'Shipping in Month- By Cost Element', 'Shipping in Month- By Cost Element', 'Shipping in Month- By Cost Element', 'Shipping in Month- By Cost Element', 'en_US', 3, 'PCSUtils.Framework.ReportFrame.ViewReport', 'Description', 0, 0, 0, '0', '0', 0, 0, '0', '0', '20060907140633060' )
	INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut], [Menu_EntryID], [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
	VALUES ( 'CSTRPT',742, '20060914090915013',742, 'Shipping In Month- By Product', 'Shipping In Month- By Product', 'Shipping In Month- By Product', 'Shipping In Month- By Product', 'en_US', 3, 'PCSUtils.Framework.ReportFrame.ViewReport', 'Description', 0, 0, 0, '0', '0', 0, 0, '0', '0', '20060914090915013' )
	INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut], [Menu_EntryID], [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
	VALUES ( 'CSTRPT',743, '20060830175726513',743, 'Standard Cost & Actual Cost Comparision - BOD', 'Standard Cost & Actual Cost Comparision - BOD', 'Standard Cost & Actual Cost Comparision - BOD', 'Standard Cost & Actual Cost Comparision - BOD', 'en_US', 3, 'PCSUtils.Framework.ReportFrame.ViewReport', 'Description', 0, 0, 0, '0', '0', 0, 0, '0', '0', '20060830175726513' )
	INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut], [Menu_EntryID], [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
	VALUES ( 'IVRPT',744, '20061228145550153',744, 'Stock Taking Comparision Report', 'Stock Taking Comparision Report', 'Stock Taking Comparision Report', 'Stock Taking Comparision Report', 'en_US', 3, 'PCSUtils.Framework.ReportFrame.ViewReport', 'Description', 0, 0, 0, '0', '0', 0, 0, '0', '0', '20061228145550153' )
	INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut], [Menu_EntryID], [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
	VALUES ( 'IVRPT',745, '20061228090721280',745, 'Supervise Report', 'Supervise Report', 'Supervise Report', 'Supervise Report', 'en_US', 3, 'PCSUtils.Framework.ReportFrame.ViewReport', 'Description', 0, 0, 0, '0', '0', 0, 0, '0', '0', '20061228090721280' )
	INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut], [Menu_EntryID], [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
	VALUES ( 'CSTRPT',746, '20070130105144913',746, 'Tong Hop San Pham Hoan Thanh Theo Nhom San Pham', 'Tong Hop San Pham Hoan Thanh Theo Nhom San Pham', 'Tong Hop San Pham Hoan Thanh Theo Nhom San Pham', 'Tong Hop San Pham Hoan Thanh Theo Nhom San Pham', 'en_US', 3, 'PCSUtils.Framework.ReportFrame.ViewReport', 'Description', 0, 0, 0, '0', '0', 0, 0, '0', '0', '20070130105144913' )
	INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut], [Menu_EntryID], [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
	VALUES ( 'IVRPT',747, '20070124135324740',747, 'Tong Hop Vat Tu Nhap Claim', 'Tong Hop Vat Tu Nhap Claim', 'Tong Hop Vat Tu Nhap Claim', 'Tong Hop Vat Tu Nhap Claim', 'en_US', 3, 'PCSUtils.Framework.ReportFrame.ViewReport', 'Description', 0, 0, 0, '0', '0', 0, 0, '0', '0', '20070124135324740' )
	INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut], [Menu_EntryID], [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
	VALUES ( 'PRRPT',748, '20061205111008657',748, 'Tong Hop VT Nhap Theo KH-Import-By Maker', 'Tong Hop VT Nhap Theo KH-Import-By Maker', 'Tong Hop VT Nhap Theo KH-Import-By Maker', 'Tong Hop VT Nhap Theo KH-Import-By Maker', 'en_US', 3, 'PCSUtils.Framework.ReportFrame.ViewReport', 'Description', 0, 0, 0, '0', '0', 0, 0, '0', '0', '20061205111008657' )
	INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut], [Menu_EntryID], [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
	VALUES ( 'PRRPT',749, '20061205103013000',749, 'Tong Hop VT Nhap Theo KH-Import-Vendor', 'Tong Hop VT Nhap Theo KH-Import-Vendor', 'Tong Hop VT Nhap Theo KH-Import-Vendor', 'Tong Hop VT Nhap Theo KH-Import-Vendor', 'en_US', 3, 'PCSUtils.Framework.ReportFrame.ViewReport', 'Description', 0, 0, 0, '0', '0', 0, 0, '0', '0', '20061205103013000' )
	INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut], [Menu_EntryID], [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
	VALUES ( 'PRRPT',750, '20060916165135890',750, 'Tong Hop Vat Tu Nhap theo KH-Local', 'Tong Hop Vat Tu Nhap theo KH-Local', 'Tong Hop Vat Tu Nhap theo KH-Local', 'Tong Hop Vat Tu Nhap theo KH-Local', 'en_US', 3, 'PCSUtils.Framework.ReportFrame.ViewReport', 'Description', 0, 0, 0, '0', '0', 0, 0, '0', '0', '20060916165135890' )
	INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut], [Menu_EntryID], [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
	VALUES ( 'IVRPT',751, '20070109160603373',751, 'Tong Hop Vat Tu Xuat Ban', 'Tong Hop Vat Tu Xuat Ban', 'Tong Hop Vat Tu Xuat Ban', 'Tong Hop Vat Tu Xuat Ban', 'en_US', 3, 'PCSUtils.Framework.ReportFrame.ViewReport', 'Description', 0, 0, 0, '0', '0', 0, 0, '0', '0', '20070109160603373' )
	INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut], [Menu_EntryID], [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
	VALUES ( 'IVRPT',752, '20070124095155937',752, 'Tong Hop Vat Tu Xuat Chuyen Kho', 'Tong Hop Vat Tu Xuat Chuyen Kho', 'Tong Hop Vat Tu Xuat Chuyen Kho', 'Tong Hop Vat Tu Xuat Chuyen Kho', 'en_US', 3, 'PCSUtils.Framework.ReportFrame.ViewReport', 'Description', 0, 0, 0, '0', '0', 0, 0, '0', '0', '20070124095155937' )
	INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut], [Menu_EntryID], [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
	VALUES ( 'IVRPT',753, '20070109162235140',753, 'Tong Hop Vat Tu Xuat Claim', 'Tong Hop Vat Tu Xuat Claim', 'Tong Hop Vat Tu Xuat Claim', 'Tong Hop Vat Tu Xuat Claim', 'en_US', 3, 'PCSUtils.Framework.ReportFrame.ViewReport', 'Description', 0, 0, 0, '0', '0', 0, 0, '0', '0', '20070109162235140' )
	INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut], [Menu_EntryID], [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
	VALUES ( 'IVRPT',754, '20070108160755950',754, 'Tong Hop Vat Tu Xuat Gia Cong', 'Tong Hop Vat Tu Xuat Gia Cong', 'Tong Hop Vat Tu Xuat Gia Cong', 'Tong Hop Vat Tu Xuat Gia Cong', 'en_US', 3, 'PCSUtils.Framework.ReportFrame.ViewReport', 'Description', 0, 0, 0, '0', '0', 0, 0, '0', '0', '20070108160755950' )
	INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut], [Menu_EntryID], [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
	VALUES ( 'IVRPT',755, '20070109160805327',755, 'Tong Hop Vat Tu Xuat Gia Cong Ngoai', 'Tong Hop Vat Tu Xuat Gia Cong Ngoai', 'Tong Hop Vat Tu Xuat Gia Cong Ngoai', 'Tong Hop Vat Tu Xuat Gia Cong Ngoai', 'en_US', 3, 'PCSUtils.Framework.ReportFrame.ViewReport', 'Description', 0, 0, 0, '0', '0', 0, 0, '0', '0', '20070109160805327' )
	INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut], [Menu_EntryID], [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
	VALUES ( 'IVRPT',756, '20070108150453390',756, 'Tong Hop Vat Tu Xuat Huy', 'Tong Hop Vat Tu Xuat Huy', 'Tong Hop Vat Tu Xuat Huy', 'Tong Hop Vat Tu Xuat Huy', 'en_US', 3, 'PCSUtils.Framework.ReportFrame.ViewReport', 'Description', 0, 0, 0, '0', '0', 0, 0, '0', '0', '20070108150453390' )
	INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut], [Menu_EntryID], [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
	VALUES ( 'IVRPT',757, '20070108172518640',757, 'Tong Hop Vat Tu Xuat Lap Rap Thanh Pham', 'Tong Hop Vat Tu Xuat Lap Rap Thanh Pham', 'Tong Hop Vat Tu Xuat Lap Rap Thanh Pham', 'Tong Hop Vat Tu Xuat Lap Rap Thanh Pham', 'en_US', 3, 'PCSUtils.Framework.ReportFrame.ViewReport', 'Description', 0, 0, 0, '0', '0', 0, 0, '0', '0', '20070108172518640' )
	INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut], [Menu_EntryID], [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
	VALUES ( 'IVRPT',758, '20070117113537670',758, 'Tong Hop Vat Tu Xuat Sang Cong Doan Sau', 'Tong Hop Vat Tu Xuat Sang Cong Doan Sau', 'Tong Hop Vat Tu Xuat Sang Cong Doan Sau', 'Tong Hop Vat Tu Xuat Sang Cong Doan Sau', 'en_US', 3, 'PCSUtils.Framework.ReportFrame.ViewReport', 'Description', 0, 0, 0, '0', '0', 0, 0, '0', '0', '20070117113537670' )
	INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut], [Menu_EntryID], [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
	VALUES ( 'IVRPT',759, '20070109162224733',759, 'Tong Hop Vat Tu Xuat Theo Nha Thau', 'Tong Hop Vat Tu Xuat Theo Nha Thau', 'Tong Hop Vat Tu Xuat Theo Nha Thau', 'Tong Hop Vat Tu Xuat Theo Nha Thau', 'en_US', 3, 'PCSUtils.Framework.ReportFrame.ViewReport', 'Description', 0, 0, 0, '0', '0', 0, 0, '0', '0', '20070109162224733' )
	INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut], [Menu_EntryID], [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
	VALUES ( 'IVRPT',760, '20070108164514187',760, 'Top Hop Vat Tu Nhap Theo Nha Thau', 'Top Hop Vat Tu Nhap Theo Nha Thau', 'Top Hop Vat Tu Nhap Theo Nha Thau', 'Top Hop Vat Tu Nhap Theo Nha Thau', 'en_US', 3, 'PCSUtils.Framework.ReportFrame.ViewReport', 'Description', 0, 0, 0, '0', '0', 0, 0, '0', '0', '20070108164514187' )
	INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut], [Menu_EntryID], [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
	VALUES ( 'PRODUCTION',761, '20061117152107810',761, 'Work Order Complement', 'Work Order Complement', 'Work Order Complement', 'Work Order Complement', 'en_US', 3, 'PCSUtils.Framework.ReportFrame.ViewReport', 'Description', 0, 0, 0, '0', '0', 0, 0, '0', '0', '20061117152107810' )
	-- right
	INSERT INTO [dbo].[Sys_Right] ([RoleID], [Permission], [Menu_EntryID] )VALUES (1, 31, 718 )
	INSERT INTO [dbo].[Sys_Right] ([RoleID], [Permission], [Menu_EntryID] )VALUES (1, 31, 719 )
	INSERT INTO [dbo].[Sys_Right] ([RoleID], [Permission], [Menu_EntryID] )VALUES (1, 31, 720 )
	INSERT INTO [dbo].[Sys_Right] ([RoleID], [Permission], [Menu_EntryID] )VALUES (1, 31, 721 )
	INSERT INTO [dbo].[Sys_Right] ([RoleID], [Permission], [Menu_EntryID] )VALUES (1, 31, 722 )
	INSERT INTO [dbo].[Sys_Right] ([RoleID], [Permission], [Menu_EntryID] )VALUES (1, 31, 723 )
	INSERT INTO [dbo].[Sys_Right] ([RoleID], [Permission], [Menu_EntryID] )VALUES (1, 31, 724 )
	INSERT INTO [dbo].[Sys_Right] ([RoleID], [Permission], [Menu_EntryID] )VALUES (1, 31, 725 )
	INSERT INTO [dbo].[Sys_Right] ([RoleID], [Permission], [Menu_EntryID] )VALUES (1, 31, 726 )
	INSERT INTO [dbo].[Sys_Right] ([RoleID], [Permission], [Menu_EntryID] )VALUES (1, 31, 727 )
	INSERT INTO [dbo].[Sys_Right] ([RoleID], [Permission], [Menu_EntryID] )VALUES (1, 31, 728 )
	INSERT INTO [dbo].[Sys_Right] ([RoleID], [Permission], [Menu_EntryID] )VALUES (1, 31, 729 )
	INSERT INTO [dbo].[Sys_Right] ([RoleID], [Permission], [Menu_EntryID] )VALUES (1, 31, 730 )
	INSERT INTO [dbo].[Sys_Right] ([RoleID], [Permission], [Menu_EntryID] )VALUES (1, 31, 731 )
	INSERT INTO [dbo].[Sys_Right] ([RoleID], [Permission], [Menu_EntryID] )VALUES (1, 31, 732 )
	INSERT INTO [dbo].[Sys_Right] ([RoleID], [Permission], [Menu_EntryID] )VALUES (1, 31, 733 )
	INSERT INTO [dbo].[Sys_Right] ([RoleID], [Permission], [Menu_EntryID] )VALUES (1, 31, 734 )
	INSERT INTO [dbo].[Sys_Right] ([RoleID], [Permission], [Menu_EntryID] )VALUES (1, 31, 735 )
	INSERT INTO [dbo].[Sys_Right] ([RoleID], [Permission], [Menu_EntryID] )VALUES (1, 31, 736 )
	INSERT INTO [dbo].[Sys_Right] ([RoleID], [Permission], [Menu_EntryID] )VALUES (1, 31, 737 )
	INSERT INTO [dbo].[Sys_Right] ([RoleID], [Permission], [Menu_EntryID] )VALUES (1, 31, 738 )
	INSERT INTO [dbo].[Sys_Right] ([RoleID], [Permission], [Menu_EntryID] )VALUES (1, 31, 739 )
	INSERT INTO [dbo].[Sys_Right] ([RoleID], [Permission], [Menu_EntryID] )VALUES (1, 31, 740 )
	INSERT INTO [dbo].[Sys_Right] ([RoleID], [Permission], [Menu_EntryID] )VALUES (1, 31, 741 )
	INSERT INTO [dbo].[Sys_Right] ([RoleID], [Permission], [Menu_EntryID] )VALUES (1, 31, 742 )
	INSERT INTO [dbo].[Sys_Right] ([RoleID], [Permission], [Menu_EntryID] )VALUES (1, 31, 743 )
	INSERT INTO [dbo].[Sys_Right] ([RoleID], [Permission], [Menu_EntryID] )VALUES (1, 31, 744 )
	INSERT INTO [dbo].[Sys_Right] ([RoleID], [Permission], [Menu_EntryID] )VALUES (1, 31, 745 )
	INSERT INTO [dbo].[Sys_Right] ([RoleID], [Permission], [Menu_EntryID] )VALUES (1, 31, 746 )
	INSERT INTO [dbo].[Sys_Right] ([RoleID], [Permission], [Menu_EntryID] )VALUES (1, 31, 747 )
	INSERT INTO [dbo].[Sys_Right] ([RoleID], [Permission], [Menu_EntryID] )VALUES (1, 31, 748 )
	INSERT INTO [dbo].[Sys_Right] ([RoleID], [Permission], [Menu_EntryID] )VALUES (1, 31, 749 )
	INSERT INTO [dbo].[Sys_Right] ([RoleID], [Permission], [Menu_EntryID] )VALUES (1, 31, 750 )
	INSERT INTO [dbo].[Sys_Right] ([RoleID], [Permission], [Menu_EntryID] )VALUES (1, 31, 751 )
	INSERT INTO [dbo].[Sys_Right] ([RoleID], [Permission], [Menu_EntryID] )VALUES (1, 31, 752 )
	INSERT INTO [dbo].[Sys_Right] ([RoleID], [Permission], [Menu_EntryID] )VALUES (1, 31, 753 )
	INSERT INTO [dbo].[Sys_Right] ([RoleID], [Permission], [Menu_EntryID] )VALUES (1, 31, 754 )
	INSERT INTO [dbo].[Sys_Right] ([RoleID], [Permission], [Menu_EntryID] )VALUES (1, 31, 755 )
	INSERT INTO [dbo].[Sys_Right] ([RoleID], [Permission], [Menu_EntryID] )VALUES (1, 31, 756 )
	INSERT INTO [dbo].[Sys_Right] ([RoleID], [Permission], [Menu_EntryID] )VALUES (1, 31, 757 )
	INSERT INTO [dbo].[Sys_Right] ([RoleID], [Permission], [Menu_EntryID] )VALUES (1, 31, 758 )
	INSERT INTO [dbo].[Sys_Right] ([RoleID], [Permission], [Menu_EntryID] )VALUES (1, 31, 759 )
	INSERT INTO [dbo].[Sys_Right] ([RoleID], [Permission], [Menu_EntryID] )VALUES (1, 31, 760 )
	INSERT INTO [dbo].[Sys_Right] ([RoleID], [Permission], [Menu_EntryID] )VALUES (1, 31, 761 )
	SET IDENTITY_INSERT Sys_Menu_Entry OFF
END

go
-- Rename Costing Adjustment menu
IF EXISTS (SELECT Menu_EntryID FROM Sys_Menu_Entry  WHERE Menu_EntryID = 3)
BEGIN
	UPDATE Sys_Menu_Entry SET Text_CaptionDefault = 'Cost Center Rate',
							Text_Caption_VI_VN = 'Cost Center Rate',
							Text_Caption_EN_US = 'Cost Center Rate',
							Text_Caption_JA_JP = 'Cost Center Rate'
	WHERE Menu_EntryID = 3
END
go

-- Product Production Order
IF NOT EXISTS (SELECT Menu_EntryID FROM Sys_Menu_Entry  WHERE Menu_EntryID = 762)
Begin
	SET IDENTITY_INSERT Sys_Menu_Entry ON
-- Onhand Period
	INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut], [Menu_EntryID], [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
 VALUES ( 'DCP', 762, 'PPO', 12, 'Order Of Product In Production Line', 'Order Of Product In Production Line', 'Order Of Product In Production Line', 'Order Of Product In Production Line', 'en_US', 3, 'PCSProduction.DCP.ProductProductionOrder', 'Order Of Product In Production Line', 0, 0, 0, NULL, NULL, 0, 0, NULL, NULL, NULL )
-- insert right
INSERT INTO [dbo].[Sys_Right] ([RoleID], [Permission], [Menu_EntryID] )
 VALUES (1, 31, 762 )
	SET IDENTITY_INSERT Sys_Menu_Entry OFF
End
go

-- change form load of return to vendor
IF EXISTS (SELECT Menu_EntryID FROM Sys_Menu_Entry  WHERE Menu_EntryID = 48)
BEGIN
	UPDATE Sys_Menu_Entry SET FormLoad = 'PCSProcurement.Purchase.POReturnToVendor'
	WHERE Menu_EntryID IN (48, 292)
END
go

-- Approved/Cancel PO Delivery Schedule
IF NOT EXISTS (SELECT Menu_EntryID FROM Sys_Menu_Entry  WHERE Menu_EntryID = 763)
Begin
	SET IDENTITY_INSERT Sys_Menu_Entry ON
-- Onhand Period
	INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut], [Menu_EntryID], [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
 VALUES ( 'PRM', 763, 'POACD', 12, 'Approved/Cancel PO Delivery Schedule', 'Approved/Cancel PO Delivery Schedule', 'Approved/Cancel PO Delivery Schedule', 'Approved/Cancel PO Delivery Schedule', 'en_US', 3, 'PCSProcurement.Purchase.DeliveryApproval', 'Approved/Cancel PO Delivery Schedule', 0, 0, 0, NULL, NULL, 0, 0, NULL, NULL, NULL )
-- insert right
INSERT INTO [dbo].[Sys_Right] ([RoleID], [Permission], [Menu_EntryID] )
 VALUES (1, 31, 763 )
	SET IDENTITY_INSERT Sys_Menu_Entry OFF
End
go

-- DCP Estimate
IF NOT EXISTS (SELECT Menu_EntryID FROM Sys_Menu_Entry  WHERE Menu_EntryID = 764)
Begin
	SET IDENTITY_INSERT Sys_Menu_Entry ON
-- Onhand Period
	INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut], [Menu_EntryID], [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
 VALUES ( 'DCP', 764, 'DCPE', 12, 'DCP Estimation', 'DCP Estimation', 'DCP Estimation', 'DCP Estimation', 'en_US', 3, 'PCSProduction.DCP.DCPEstimate', 'DCP Estimation', 0, 0, 0, NULL, NULL, 0, 0, NULL, NULL, NULL )
-- insert right
INSERT INTO [dbo].[Sys_Right] ([RoleID], [Permission], [Menu_EntryID] )
 VALUES (1, 31, 764 )
	SET IDENTITY_INSERT Sys_Menu_Entry OFF
End
go

-- Delete Estimate PO
IF NOT EXISTS (SELECT Menu_EntryID FROM Sys_Menu_Entry  WHERE Menu_EntryID = 765)
Begin
	SET IDENTITY_INSERT Sys_Menu_Entry ON
-- Onhand Period
	INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut], [Menu_EntryID], [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
 VALUES ( 'PRM', 765, 'DEPO', 12, 'Delete Purchase Orders', 'Delete Purchase Orders', 'Delete Purchase Orders', 'Delete Purchase Orders', 'en_US', 3, 'PCSProcurement.Purchase.DeletePurchaseOrder', 'DeletePurchaseOrder', 0, 0, 0, NULL, NULL, 0, 0, NULL, NULL, NULL )
-- insert right
INSERT INTO [dbo].[Sys_Right] ([RoleID], [Permission], [Menu_EntryID] )
 VALUES (1, 31, 765 )
	SET IDENTITY_INSERT Sys_Menu_Entry OFF
End
go

-- Insert rest report menu by dungla 11-Dec-2007
IF NOT EXISTS (SELECT Menu_EntryID FROM Sys_Menu_Entry WHERE Menu_EntryID = 766)
BEGIN
	SET IDENTITY_INSERT Sys_Menu_Entry ON
	-- costing report
	INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut], [Menu_EntryID], [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
	VALUES ( 'CSTRPT',766, '20071115163146983',766, 'In Out Stock Report Group By BinType', 'In Out Stock Report Group By BinType', 'In Out Stock Report Group By BinType', 'In Out Stock Report Group By BinType', 'en_US', 3, 'PCSUtils.Framework.ReportFrame.ViewReport', 'Description', 0, 0, 0, '0', '0', 0, 0, '0', '0', '20071115163146983' )
	INSERT INTO [dbo].[Sys_Menu_Entry] ([Parent_Shortcut], [Menu_EntryID], [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID] )
	VALUES ( 'PRODUCTION',767, '20071107153743287',767, 'Bao Cao Tong Hop San Xuat', 'Bao Cao Tong Hop San Xuat', 'Bao Cao Tong Hop San Xuat', 'Bao Cao Tong Hop San Xuat', 'en_US', 3, 'PCSUtils.Framework.ReportFrame.ViewReport', 'Description', 0, 0, 0, '0', '0', 0, 0, '0', '0', '20071107153743287' )
	-- right
	INSERT INTO [dbo].[Sys_Right] ([RoleID], [Permission], [Menu_EntryID] )VALUES (1, 31, 766 )
	INSERT INTO [dbo].[Sys_Right] ([RoleID], [Permission], [Menu_EntryID] )VALUES (1, 31, 767 )
	SET IDENTITY_INSERT Sys_Menu_Entry OFF
END
go

-- Insert menu for Approve PO
IF NOT EXISTS (SELECT Menu_EntryID FROM Sys_Menu_Entry WHERE Menu_EntryID = 768)
BEGIN
	SET IDENTITY_INSERT Sys_Menu_Entry ON
	-- Multi work order completion
	INSERT [dbo].[Sys_Menu_Entry] ([Parent_Shortcut], [Menu_EntryID], [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID])
	VALUES (N'WO', 768, N'MULTIWOCOMP', 217, N'Multi Work Order Completion', N'Multi Work Order Completion (VN)', N'Multi Work Order Completion (EN)', N'Multi Work Order Completion (JP', N'en_US', 3, N'PCSProduction.WorkOrder.MultiCompletion', N'nothing', 0, 44, 44, N'', N'yyyyMMdd###', 1, 0, N'PRO_WorkOrderCompletion', N'TransNo', NULL)
	-- MPS ReCompany
	INSERT [dbo].[Sys_Menu_Entry] ([Parent_Shortcut], [Menu_EntryID], [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID])
	VALUES (N'MPS', 769, N'MPSReCompany', 1, N'MPSReCompany', N'Lập kế hoạch sản xuất(Company)', N'MPSReCompany', N'MPSReCompany', N'en_US', 3, N'PCSMaterials.Mps.MPSRegenerationForKMPCompany', N'nothing', 0, 116, 116, NULL, NULL, 0, 0, NULL, NULL, NULL)
	-- Update PO
	INSERT [dbo].[Sys_Menu_Entry] ([Parent_Shortcut], [Menu_EntryID], [Shortcut], [Button_Caption], [Text_CaptionDefault], [Text_Caption_VI_VN], [Text_Caption_EN_US], [Text_Caption_JA_JP], [Text_Caption_Language_Default], [Parent_Child], [FormLoad], [Description], [Type], [CollapsedImage], [ExpandedImage], [Prefix], [TransFormat], [IsTransaction], [IsUserCreated], [TableName], [TransNoFieldName], [ReportID])
	VALUES (N'PRM', 770, N'UpdatePO', 12, N'Update Purchase Orders', N'Update Purchase Orders', N'Update Purchase Orders', N'Update Purchase Orders', N'en_US', 3, N'PCSProcurement.Purchase.UpdatePurchaseOrder', N'UpdatePurchaseOrder', 0, 0, 0, NULL, NULL, 0, 0, NULL, NULL, NULL)
	-- right
	INSERT INTO [dbo].[Sys_Right] ([RoleID], [Permission], [Menu_EntryID] )VALUES (1, 31, 768 )
	INSERT INTO [dbo].[Sys_Right] ([RoleID], [Permission], [Menu_EntryID] )VALUES (1, 31, 769 )
	INSERT INTO [dbo].[Sys_Right] ([RoleID], [Permission], [Menu_EntryID] )VALUES (1, 31, 770 )
	SET IDENTITY_INSERT Sys_Menu_Entry OFF
END
go

IF EXISTS (SELECT Menu_EntryID FROM Sys_Menu_Entry WHERE Menu_EntryID = 46)
BEGIN
	UPDATE Sys_Menu_Entry SET FormLoad = 'PCSProcurement.Purchase.PurchaseOrderReceipts'
	WHERE Menu_EntryID = 46
END
go
IF EXISTS (SELECT Menu_EntryID FROM Sys_Menu_Entry WHERE Menu_EntryID = 768)
BEGIN
	UPDATE Sys_Menu_Entry SET FormLoad = 'PCSProduction.WorkOrder.MultiCompletion',
	TransFormat = 'yyyyMMdd####',
	IsTransaction = 1,
	TableName = 'PRO_WorkOrderCompletion',
	TransNoFieldName = 'WOCompletionNo'
	WHERE Menu_EntryID = 768
END
GO

-- set form load of right assignment to the new form
IF EXISTS (SELECT Menu_EntryID FROM Sys_Menu_Entry WHERE Menu_EntryID = 63)
BEGIN
	UPDATE Sys_Menu_Entry SET FormLoad = 'PCSUtils.Admin.RightAssignment'
	WHERE Menu_EntryID = 63
END
go
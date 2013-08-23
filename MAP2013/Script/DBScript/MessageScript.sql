/*
File:		MessageScript.sql
Created:	DungLA
Purpose:	All message in PCS
*/

---------------------------------MESSAGE PART----------------------------------------------


DELETE FROM sys_Error_Msg WHERE code = 1506
go
INSERT INTO sys_Error_Msg(code,msgDefault,msgVn,msgEn,msgJp,description) VALUES(
1506, 
N'CAN_NOT_READ_EXCEL_FILE', 
N'Can not read Excel file (vn)', 
N'Can not read Excel file', 
N'Can not read Excel file (jp)',
N'Can not read Excel file')
go


DELETE FROM sys_Error_Msg WHERE code = 1507
go
INSERT INTO sys_Error_Msg(code,msgDefault,msgVn,msgEn,msgJp,description) VALUES(
1507, 
N'ALLOCATION_EXCEL_FILE_NOT_CORRECT', 
N'The format of Allocation Excel file is not correct (vn)', 
N'The format of Allocation Excel file is not correct', 
N'The format of Allocation Excel file is not correct (jp)',
N'The format of Allocation Excel file is not correct')
go

DELETE FROM sys_Error_Msg WHERE code = 1508
go
INSERT INTO sys_Error_Msg(code,msgDefault,msgVn,msgEn,msgJp,description) VALUES(
1508, 
N'PRODUCT_COST_ELEMENT_NOT_FOUND', 
N'Some Products or Cost Elements not found (vn)', 
N'Some Products or Cost Elements not found', 
N'Some Products or Cost Elements not found (jp)',
N'Some Products or Cost Elements not found')
go

DELETE FROM sys_Error_Msg WHERE code = 1509
go
INSERT INTO sys_Error_Msg(code,msgDefault,msgVn,msgEn,msgJp,description) VALUES(
1509, 
N'MESSAGE_RECYCLED_HAS_MORE_THAN_ONE_DESTINATION', 
N'Can not view Recycled Material Slip cause this transaction has more than one destinations (vn)', 
N'Can not view Recycled Material Slip cause this transaction has more than one destinations', 
N'Can not view Recycled Material Slip cause this transaction has more than one destinations (jp)',
N'Can not view Recycled Material Slip cause this transaction has more than one destinations')
go

DELETE FROM sys_Error_Msg WHERE code = 1510
go
INSERT INTO sys_Error_Msg(code,msgDefault,msgVn,msgEn,msgJp,description) VALUES(
1510, 
N'MESSAGE_CANNOT_EDIT_FIELD_WHEN_USE_TEMPLATE', 
N'Can not edit Properties of Fields because this report uses Template File (vn)', 
N'Can not edit Properties of Fields because this report uses Template File', 
N'Can not edit Properties of Fields because this report uses Template File (jp)',
N'Can not edit Properties of Fields because this report uses Template File')
go

DELETE FROM sys_Error_Msg WHERE code = 1511
go
INSERT INTO sys_Error_Msg(code,msgDefault,msgVn,msgEn,msgJp,description) VALUES(
1511, 
N'MESSAGE_DCOPTION_VERSION_HAS_EXIST', 
N'The Version already existed in this Planning Period, please enter another one (vn)', 
N'The Version already existed in this Planning Period, please enter another one', 
N'The Version already existed in this Planning Period, please enter another one (jp)',
N'The Version already existed in this Planning Period, please enter another one')
go

DELETE FROM sys_Error_Msg WHERE code = 1505
go
INSERT INTO sys_Error_Msg(code,msgDefault,msgVn,msgEn,msgJp,description) VALUES(
1505, 
N'MESSAGE_CANNOT_ROLL_UP', 
N'@ process is terminated by user and not finished successfully (vn)', 
N'@ process is terminated by user and not finished successfully', 
N'@ process is terminated by user and not finished successfully (jp)',
N'@ process is terminated by user and not finished successfully')
go


DELETE FROM sys_Error_Msg WHERE code = 1512
go
INSERT INTO sys_Error_Msg(code,msgDefault,msgVn,msgEn,msgJp,description) VALUES(
1512, 
N'MESSAGE_PRODUCT_HAS_DELIVERY_SCHEDULE', 
N'This Primary Vendor was set Receiving Schedule, please re-set Receiving Schedule before changing', 
N'This Primary Vendor was set Receiving Schedule, please re-set Receiving Schedule before changing', 
N'This Primary Vendor was set Receiving Schedule, please re-set Receiving Schedule before changing',
N'This Primary Vendor was set Receiving Schedule, please re-set Receiving Schedule before changing')
go

DELETE FROM sys_Error_Msg WHERE code = 1513
go
INSERT INTO sys_Error_Msg(code,msgDefault,msgVn,msgEn,msgJp,description) VALUES(
1513, 
N'MESSAGE_SO_HAS_BEEN_RELEASED', 
N'This @ was released, you can not edit or delete it(vn)', 
N'This @ was released, you can not edit or delete it', 
N'This @ was released, you can not edit or delete it(jp)',
N'This @ was released, you can not edit or delete it')
go

DELETE FROM sys_Error_Msg WHERE code = 1514
go
INSERT INTO sys_Error_Msg(code,msgDefault,msgVn,msgEn,msgJp,description) VALUES(
1514, 
N'MESSAGE_SO_HAS_BEEN_RELEASED', 
N'The selected Work Center does not belong to the selected Production Line (vn)', 
N'The selected Work Center does not belong to the selected Production Line', 
N'The selected Work Center does not belong to the selected Production Line (jp)',
N'The selected Work Center does not belong to the selected Production Line')
go

DELETE FROM sys_Error_Msg WHERE code = 1515
go
INSERT INTO sys_Error_Msg(code,msgDefault,msgVn,msgEn,msgJp,description) VALUES(
1515, 
N'MESSAGE_AT_LEAST_WC_INVALID', 
N'At least one selected Work Center does not belong to the selected Production Line (vn)', 
N'At least one selected Work Center does not belong to the selected Production Line', 
N'At least one selected Work Center does not belong to the selected Production Line (jp)',
N'At least one selected Work Center does not belong to the selected Production Line')
go


DELETE FROM sys_Error_Msg WHERE code = 1516
go
INSERT INTO sys_Error_Msg(code,msgDefault,msgVn,msgEn,msgJp,description) VALUES(
1516, 
N'MESSAGE_PLS_CLEAR_RELATE_FIRST', 
N'You must clear @ before clear @ (vn)', 
N'You must clear @ before clear @', 
N'You must clear @ before clear @ (jp)',
N'You must clear @ before clear @')
go

DELETE FROM sys_Error_Msg WHERE code = 1517
go
INSERT INTO sys_Error_Msg(code,msgDefault,msgVn,msgEn,msgJp,description) VALUES(
1517, 
N'MESSAGE_CONFIRM_BEFORE_SAVE_DATA', 
N'Are you sure you want to save this transaction (vn)', 
N'Are you sure you want to save this transaction', 
N'Are you sure you want to save this transaction (jp)',
N'Are you sure you want to save this transaction')
go

DELETE FROM sys_Error_Msg WHERE code = 1518
go
INSERT INTO sys_Error_Msg(code,msgDefault,msgVn,msgEn,msgJp,description) VALUES(
1518, 
N'MESSAGE_SELECT_DCP_OR_MPS_BEFORE_CONVERT_WO', 
N'Please select a Plan Type before converting data to Work Order (vn)', 
N'Please select a Plan Type before converting data to Work Order', 
N'Please select a Plan Type before converting data to Work Order (jp)',
N'Please select a Plan Type before converting data to Work Order')
go

DELETE FROM sys_Error_Msg WHERE code = 1513
go
INSERT INTO sys_Error_Msg(code,msgDefault,msgVn,msgEn,msgJp,description) VALUES(
1513, 
N'MESSAGE_SO_HAS_BEEN_RELEASED', 
N'This @ was commited, you can not edit or delete it (vn)', 
N'This @ was commited, you can not edit or delete it', 
N'This @ was commited, you can not edit or delete it (jp)',
N'This @ was commited, you can not edit or delete it')
go

DELETE FROM sys_Error_Msg WHERE code = 1125
go
INSERT INTO sys_Error_Msg(code,msgDefault,msgVn,msgEn,msgJp,description) VALUES(
1125, 
N'MESSAGE_NOT_ENOUGH_QUANTITY', 
N'Delivery Quantity must be smaller than Available Quantity (vn)', 
N'Delivery Quantity must be smaller than Available Quantity', 
N'Delivery Quantity must be smaller than Available Quantity (jp)',
N'Delivery Quantity must be smaller than Available Quantity')
go

DELETE FROM sys_Error_Msg WHERE code = 1401
go
INSERT INTO sys_Error_Msg(code,msgDefault,msgVn,msgEn,msgJp,description) VALUES(
1401, 
N'MESSAGE_AVAILABLE_QTY_MUST_GREATER_THAN_ZERO', 
N'There is no Available Quantity in inventory (vn)', 
N'There is no Available Quantity in inventory', 
N'There is no Available Quantity in inventory (jp)',
N'There is no Available Quantity in inventory')
go

DELETE FROM sys_Error_Msg WHERE code = 1519
go
INSERT INTO sys_Error_Msg(code,msgDefault,msgVn,msgEn,msgJp,description) VALUES(
1519, 
N'MESSAGE_SO_CAN_NOT_CHANGE_ITEM', 
N'This Product can not be changed because its @ has Delivery Schedule (vn)', 
N'This Product can not be changed because its @ has Delivery Schedule', 
N'This Product can not be changed because its @ has Delivery Schedule (jp)',
N'This Product can not be changed because its @ has Delivery Schedule')
go

DELETE FROM sys_Error_Msg WHERE code = 1257
go
INSERT INTO sys_Error_Msg(code,msgDefault,msgVn,msgEn,msgJp,description) VALUES(
1257, 
N'MSG_WOISSUE_MATERIAL_COMMIT_QTY_DONOT_MEET_AVAILABLE_QTY', 
N'Total Commit Quantity of this Product exceeds the Available Quantity (vn)', 
N'Total Commit Quantity of this Product exceeds the Available Quantity', 
N'Total Commit Quantity of this Product exceeds the Available Quantity (jp)',
N'Total Commit Quantity of this Product exceeds the Available Quantity')
go

DELETE FROM sys_Error_Msg WHERE code = 1520
go
INSERT INTO sys_Error_Msg(code,msgDefault,msgVn,msgEn,msgJp,description) VALUES(
1520, 
N'MESSAGE_CANNOT_CHANGE_PRODUCTION_LINE', 
N'Can not change Production Line because some Work Order Lines was released (vn)', 
N'Can not change Production Line because some Work Order Lines was released', 
N'Can not change Production Line because some Work Order Lines was released (jp)',
N'Can not change Production Line because some Work Order Lines was released')
go

DELETE FROM sys_Error_Msg WHERE code = 1121
go
INSERT INTO sys_Error_Msg(code,msgDefault,msgVn,msgEn,msgJp,description) VALUES(
1121, 
N'MESSAGE_CANCELCOMMIT_ATLISTITEMCHECK', 
N'At least one item must be checked (vn)', 
N'At least one item must be checked', 
N'At least one item must be checked (jp)',
N'At least one item must be checked')
go


DELETE FROM sys_Error_Msg WHERE code = 1521
go
INSERT INTO sys_Error_Msg(code,msgDefault,msgVn,msgEn,msgJp,description) VALUES(
1521, 
N'PRODUCT_COST_ELEMENT_NOT_CORRECT', 
N'The combination of Deparment, Production Line and Product Group is not correct (vn)', 
N'The combination of Deparment, Production Line and Product Group is not correct', 
N'The combination of Deparment, Production Line and Product Group is not correct (jp)',
N'The combination of Deparment, Production Line and Product Group is not correct')
go

DELETE FROM sys_Error_Msg WHERE code = 1522
go
INSERT INTO sys_Error_Msg(code,msgDefault,msgVn,msgEn,msgJp,description) VALUES(
1522, 
N'NOT_FOUND', 
N'@ is not found in the system (vn)', 
N'@ is not found in the system', 
N'@ is not found in the system (jp)',
N'@ is not found in the system')
go

DELETE FROM sys_Error_Msg WHERE code = 1523
go
INSERT INTO sys_Error_Msg(code,msgDefault,msgVn,msgEn,msgJp,description) VALUES(
1523, 
N'MESSAGE_MUST_BE_UNIQUE', 
N'@ must be unique (vn)', 
N'@ must be unique', 
N'@ must be unique (jp)',
N'@ must be unique')
go

DELETE FROM sys_Error_Msg WHERE code = 1524
go
INSERT INTO sys_Error_Msg(code,msgDefault,msgVn,msgEn,msgJp,description) VALUES(
1524, 
N'MESSAGE_CPO_CAN_NOT_CONVERT', 
N'You can not convert this item, because it has not been saved in to database. (vn)', 
N'You can not convert this item, because it has not been saved in to database. ', 
N'You can not convert this item, because it has not been saved in to database. (jp)',
N'You can not convert this item, because it has not been saved in to database. ')
go

DELETE FROM sys_Error_Msg WHERE code = 1525
go
INSERT INTO sys_Error_Msg(code,msgDefault,msgVn,msgEn,msgJp,description) VALUES(
1525, 
N'MESSAGE_PRODUCT_WITH_MULTIPLE_TAXRATE_IN_INVOICE', 
N'@ of a Product are not identical in this Invoice. Please correct before continuing. (vn)', 
N'@ of a Product are not identical in this Invoice. Please correct before continuing. ', 
N'@ of a Product are not identical in this Invoice. Please correct before continuing. (jp)',
N'@ of a Product are not identical in this Invoice. Please correct before continuing. ')
go

DELETE FROM sys_Error_Msg WHERE code = 1526
go
INSERT INTO sys_Error_Msg(code,msgDefault,msgVn,msgEn,msgJp,description) VALUES(
1526, 
N'MESSAGE_CAN_NOT_COMMIT', 
N'You should select smaller or equal than 400 rows per a commission times. (vn)', 
N'You should select smaller or equal than 400 rows per a commission times.', 
N'You should select smaller or equal than 400 rows per a commission times. (jp)',
N'You should select smaller or equal than 400 rows per a commission times. ')
go

DELETE FROM sys_Error_Msg WHERE code = 1527
go
INSERT INTO sys_Error_Msg(code,msgDefault,msgVn,msgEn,msgJp,description) VALUES(
1527, 
N'MESSAGE_NOT_ENOUGH_QUANTITY_OF_COMPONENT_TO_COMPLETE', 
N'There is not enough quantiy of @ component to complete. (vn)', 
N'There is not enough quantiy of @ component to complete. ', 
N'There is not enough quantiy of @ component to complete. (jp)',
N'There is not enough quantiy of @ component to complete. ')
go

DELETE FROM sys_Error_Msg WHERE code = 1528
go
INSERT INTO sys_Error_Msg(code,msgDefault,msgVn,msgEn,msgJp,description) VALUES(
1528, 
N'MESSAGE_ADDITION_CHARGE_CHANGE_RETURN_NO', 
N'Do you really want to change Return No.(vn)', 
N'Do you really want to change Return No.', 
N'Do you really want to change Return No.(jp)',
N'Do you really want to change Return No.')
go

DELETE FROM sys_Error_Msg WHERE code = 1529
go
INSERT INTO sys_Error_Msg(code,msgDefault,msgVn,msgEn,msgJp,description) VALUES(
1529, 
N'MESSAGE_YOU_MUST_SETUP_WORKING_TIME', 
N'You must setup working time for this production line (vn)', 
N'You must setup working time for this production line', 
N'You must setup working time for this production line (jp)',
N'You must setup working time for this production line')
go

DELETE FROM sys_Error_Msg WHERE code = 1530
go
INSERT INTO sys_Error_Msg(code,msgDefault,msgVn,msgEn,msgJp,description) VALUES(
1530, 
N'MESSAGE_UPDATE_BEGIN_FOR_REPORT_SUCCESS', 
N'Successfull update begin stock for DCP Report (vn)', 
N'Successfull update begin stock for DCP Report', 
N'Successfull update begin stock for DCP Report (jp)',
N'Successfull update begin stock for DCP Report')
go

DELETE FROM sys_Error_Msg WHERE code = 1531
go
INSERT INTO sys_Error_Msg(code,msgDefault,msgVn,msgEn,msgJp,description) VALUES(
1531, 
N'MESSAGE_CONFIRM_BEFORE_SAVE_WOCOMPLETION', 
N'The Post Date is @. Are you sure want you want to save transaction (vn)', 
N'The Post Date is @. Are you sure want you want to save transaction', 
N'The Post Date is @. Are you sure want you want to save transaction (jp)',
N'The Post Date is @. Are you sure want you want to save transaction')
go

DELETE FROM sys_Error_Msg WHERE code = 1532
go
INSERT INTO sys_Error_Msg(code,msgDefault,msgVn,msgEn,msgJp,description) VALUES(
1532, 
N'MESSAGE_NOT_HAS_COMPONENT_TO_COMPLETE', 
N'This Item does not has any component to completion. Please check BOM of Item (vn)', 
N'This Item does not has any component to completion. Please check BOM of Item', 
N'This Item does not has any component to completion. Please check BOM of Item (jp)',
N'This Item does not has any component to completion. Please check BOM of Item')
go

DELETE FROM sys_Error_Msg WHERE code = 1533
go
INSERT INTO sys_Error_Msg(code,msgDefault,msgVn,msgEn,msgJp,description) VALUES(
1533, 
N'MESSAGE_DIFFERENT_ZERO', 
N'Setup Pair must be different zero(vn)', 
N'Setup Pair must be different zero', 
N'Setup Pair must be different zero(jp)',
N'Setup Pair must be different zero')

DELETE FROM sys_Error_Msg WHERE code = 1534
go
INSERT INTO sys_Error_Msg(code,msgDefault,msgVn,msgEn,msgJp,description) VALUES(
1534, 
N'MESSAGE_CAN_NOT_EDIT_POSTDATE', 
N'The postdate must be greater than or equal @(vn)', 
N'The postdate must be greater than or equal @', 
N'The postdate must be greater than or equal @(jp)',
N'The postdate must be greater than or equal @')

DELETE FROM sys_Error_Msg WHERE code = 1535
go
INSERT INTO sys_Error_Msg(code,msgDefault,msgVn,msgEn,msgJp,description) VALUES(
1535, 
N'MESSAGE_STOCK_TAKING_BETWEEN_IN', 
N'The Stock Taking Date must be between in [@] and [@]', 
N'The Stock Taking Date must be between in [@] and [@]', 
N'The Stock Taking Date must be between in [@] and [@]',
N'The Stock Taking Date must be between in [@] and [@]')

DELETE FROM sys_Error_Msg WHERE code = 1536
go
INSERT INTO sys_Error_Msg(code,msgDefault,msgVn,msgEn,msgJp,description) VALUES(
1536, 
N'MESSAGE_SOMETHING_CLOSED', 
N'You are unable to change [@], because it was closed', 
N'You are unable to change [@], because it was closed', 
N'You are unable to change [@], because it was closed',
N'You are unable to change [@], because it was closed')

DELETE FROM sys_Error_Msg WHERE code = 1537
go
INSERT INTO sys_Error_Msg(code,msgDefault,msgVn,msgEn,msgJp,description) VALUES(
1537, 
N'MESSAGE_SELECT_SHEET', 
N'Please select a sheet first', 
N'Please select a sheet first', 
N'Please select a sheet first',
N'Please select a sheet first')

DELETE FROM sys_Error_Msg WHERE code = 1538
go
INSERT INTO sys_Error_Msg(code,msgDefault,msgVn,msgEn,msgJp,description) VALUES(
1538, 
N'MESSAGE_ERROR_WORKCENTER', 
N'Error Work Center: @', 
N'Error Work Center: @', 
N'Error Work Center: @',
N'Error Work Center: @')

DELETE FROM sys_Error_Msg WHERE code = 1539
go
INSERT INTO sys_Error_Msg(code,msgDefault,msgVn,msgEn,msgJp,description) VALUES(
1539, 
N'MESSAGE_INVALID_POTYPE', 
N'Invalid PO to receive @. Valid PO must have type @', 
N'Invalid PO to receive @. Valid PO must have type @', 
N'Invalid PO to receive @. Valid PO must have type @',
N'Invalid PO to receive @. Valid PO must have type @')

DELETE FROM sys_Error_Msg WHERE code = 1540
go
INSERT INTO sys_Error_Msg(code,msgDefault,msgVn,msgEn,msgJp,description) VALUES(
1540, 
N'MESSAGE_CANNOT_DELETE_RECEIPT', 
N'Cannot delete this transaction because product was returned to vendor (@) (vn)', 
N'Cannot delete this transaction because product was returned to vendor (@)', 
N'Cannot delete this transaction because product was returned to vendor (@) (jp)',
N'Cannot delete this transaction because product was returned to vendor')
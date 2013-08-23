using System;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

using System.Text;
using PCSComUtils.Common;
using PCSComUtils.DataAccess;

using PCSComUtils.PCSExc;

namespace PCSComProduct.Items.DS
{
	
	public class ITM_ProductDS 
	{
		public const int NUMBER_EMPTY_VALUE = -1;

		public ITM_ProductDS()
		{
		}

		private const string THIS = "PCSComProduct.Items.DS.ITM_ProductDS";


		/// <summary>
		/// Update LTVariableTime property of product after updating routing info and return LTVariableTime value
		/// </summary>
		/// <param name="piProductID"></param>
		/// <returns>LTVariableTime value</returns>
		/// <author> Tuan TQ, 09 Jan, 2006. Apply proposal number: 3339</author>
		public decimal UpdateLTVariableTimeAndReturn(int piProductID)
		{
			//prepare value for parameters
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;

			string strSql = "UPDATE ITM_Product";
			strSql += " SET LTVariableTime = ";
			strSql += " (";
			strSql += " SELECT Case ITM_Routing.Pacer";
			strSql += " When '" + PacerEnum.L.ToString() + "' then ISNULL(ITM_Routing.LaborRunTime, 0) + ISNULL(ITM_Routing.LaborSetupTime, 0)";
			strSql += " When '" + PacerEnum.M.ToString() + "'  then ISNULL(ITM_Routing.MachineRunTime, 0) + ISNULL(ITM_Routing.MachineSetupTime, 0)";
			strSql += " When '" + PacerEnum.B.ToString() + "'  then ISNULL(ITM_Routing.LaborRunTime, 0) + ISNULL(ITM_Routing.LaborSetupTime, 0) + ISNULL(ITM_Routing.MachineRunTime, 0) + ISNULL(ITM_Routing.MachineSetupTime, 0)";
			strSql += " End as LeadTime";
			strSql += " FROM  ITM_Routing ";
			strSql += " INNER JOIN MST_WorkCenter ON ITM_Routing.WorkCenterID = MST_WorkCenter.WorkCenterID";

			strSql += " WHERE ITM_Routing.ProductID = " + piProductID.ToString();
			strSql += " AND MST_WorkCenter.IsMain = 1";
			strSql += " )";
			strSql += " WHERE  ProductID = " + piProductID.ToString() + ";";
			strSql += " SELECT ";
			strSql += " Case ITM_Routing.Pacer";
			strSql += " When '" + PacerEnum.L.ToString() + "' then ISNULL(ITM_Routing.LaborRunTime, 0) + ISNULL(ITM_Routing.LaborSetupTime, 0)";
			strSql += " When '" + PacerEnum.M.ToString() + "'  then ISNULL(ITM_Routing.MachineRunTime, 0) + ISNULL(ITM_Routing.MachineSetupTime, 0)";
			strSql += " When '" + PacerEnum.B.ToString() + "'  then ISNULL(ITM_Routing.LaborRunTime, 0) + ISNULL(ITM_Routing.LaborSetupTime, 0) + ISNULL(ITM_Routing.MachineRunTime, 0) + ISNULL(ITM_Routing.MachineSetupTime, 0)";
			strSql += " End as LeadTime";
			strSql += " FROM ITM_Routing";
			strSql += " INNER JOIN MST_WorkCenter ON ITM_Routing.WorkCenterID = MST_WorkCenter.WorkCenterID";

			strSql += " WHERE ITM_Routing.ProductID = " + piProductID.ToString();

			strSql += " AND MST_WorkCenter.IsMain = 1";

			Utils utils = new Utils();
			oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
			ocmdPCS = new OleDbCommand(strSql, oconPCS);

			ocmdPCS.Connection.Open();

			object objResult = ocmdPCS.ExecuteScalar();

			if (objResult != null)
			{
				return decimal.Parse(objResult.ToString());
			}
			else
			{
				return decimal.Zero;
			}

		}


		//**************************************************************************              
		///    <Description>
		///       This method uses to add data to ITM_Product
		///    </Description>
		///    <Inputs>
		///        ITM_ProductVO       
		///    </Inputs>
		///    <Outputs>
		///       newly inserted primarkey value
		///    </Outputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <Authors>
		///       HungLa
		///    </Authors>
		///    <History>
		///       Tuesday, January 25, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public void Add(object pobjObjectVO)
		{
			const string METHOD_NAME = THIS + ".Add()";

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				ITM_ProductVO objObject = (ITM_ProductVO) pobjObjectVO;
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);

				strSql = "INSERT INTO ITM_Product("
					+ ITM_ProductTable.CODE_FLD + ","
					+ ITM_ProductTable.REVISION_FLD + ","
					+ ITM_ProductTable.DESCRIPTION_FLD + ","
					+ ITM_ProductTable.SETUPDATE_FLD + ","
					+ ITM_ProductTable.VAT_FLD + ","
					+ ITM_ProductTable.IMPORTTAX_FLD + ","
					+ ITM_ProductTable.EXPORTTAX_FLD + ","
					+ ITM_ProductTable.SPECIALTAX_FLD + ","
					+ ITM_ProductTable.MAKEITEM_FLD + ","
					+ ITM_ProductTable.PARTNUMBER_FLD + ","
					+ ITM_ProductTable.OTHERINFO1_FLD + ","
					+ ITM_ProductTable.OTHERINFO2_FLD + ","
					+ ITM_ProductTable.LENGTH_FLD + ","
					+ ITM_ProductTable.WIDTH_FLD + ","
					+ ITM_ProductTable.HEIGHT_FLD + ","
					+ ITM_ProductTable.WEIGHT_FLD + ","
					+ ITM_ProductTable.FINISHEDGOODS_FLD + ","
					+ ITM_ProductTable.SHELFLIFE_FLD + ","
					+ ITM_ProductTable.LOTCONTROL_FLD + ","
					+ ITM_ProductTable.QASTATUS_FLD + ","
					+ ITM_ProductTable.STOCK_FLD + ","
					+ ITM_ProductTable.PLANTYPE_FLD + ","
					+ ITM_ProductTable.AUTOCONVERSION_FLD + ","
					+ ITM_ProductTable.ORDERQUANTITY_FLD + ","
					+ ITM_ProductTable.LTREQUISITION_FLD + ","
					+ ITM_ProductTable.LTSAFETYSTOCK_FLD + ","
					+ ITM_ProductTable.ORDERQUANTITYMULTIPLE_FLD + ","
					+ ITM_ProductTable.SCRAPPERCENT_FLD + ","
					+ ITM_ProductTable.MINIMUMSTOCK_FLD + ","
					+ ITM_ProductTable.MAXIMUMSTOCK_FLD + ","
					+ ITM_ProductTable.CONVERSIONTOLERANCE_FLD + ","
					+ ITM_ProductTable.VOUCHERTOLERANCE_FLD + ","
					+ ITM_ProductTable.RECEIVETOLERANCE_FLD + ","
					+ ITM_ProductTable.ISSUESIZE_FLD + ","
					+ ITM_ProductTable.LTFIXEDTIME_FLD + ","
					+ ITM_ProductTable.LTVARIABLETIME_FLD + ","
					+ ITM_ProductTable.LTDOCKTOSTOCK_FLD + ","
					+ ITM_ProductTable.LTORDERPREPARE_FLD + ","
					+ ITM_ProductTable.LTSHIPPINGPREPARE_FLD + ","
					+ ITM_ProductTable.LTSALESATP_FLD + ","
					+ ITM_ProductTable.SHIPTOLERANCEID_FLD + ","
					+ ITM_ProductTable.BUYERID_FLD + ","
					+ ITM_ProductTable.BOMDESCRIPTION_FLD + ","
					+ ITM_ProductTable.BOMINCREMENT_FLD + ","
					+ ITM_ProductTable.ROUTINGDESCRIPTION_FLD + ","
					+ ITM_ProductTable.CREATEDATETIME_FLD + ","
					+ ITM_ProductTable.UPDATEDATETIME_FLD + ","
					+ ITM_ProductTable.COSTMETHOD_FLD + ","
					+ ITM_ProductTable.ROUTINGINCREMENT_FLD + ","
					+ ITM_ProductTable.CCNID_FLD + ","
					+ ITM_ProductTable.CATEGORYID_FLD + ","
					+ ITM_ProductTable.COSTCENTERID_FLD + ","
					+ ITM_ProductTable.DELETEREASONID_FLD + ","
					+ ITM_ProductTable.DELIVERYPOLICYID_FLD + ","
					+ ITM_ProductTable.FORMATCODEID_FLD + ","
					+ ITM_ProductTable.FREIGHTCLASSID_FLD + ","
					+ ITM_ProductTable.HAZARDID_FLD + ","
					+ ITM_ProductTable.ORDERPOLICYID_FLD + ","
					+ ITM_ProductTable.ORDERRULEID_FLD + ","
					+ ITM_ProductTable.SOURCEID_FLD + ","
					+ ITM_ProductTable.STOCKUMID_FLD + ","
					+ ITM_ProductTable.SELLINGUMID_FLD + ","
					+ ITM_ProductTable.HEIGHTUMID_FLD + ","
					+ ITM_ProductTable.WIDTHUMID_FLD + ","
					+ ITM_ProductTable.LENGTHUMID_FLD + ","
					+ ITM_ProductTable.BUYINGUMID_FLD + ","
					+ ITM_ProductTable.WEIGHTUMID_FLD + ","
					+ ITM_ProductTable.LOTSIZE_FLD + ","
					+ ITM_ProductTable.MASTERLOCATIONID_FLD + ","
					+ ITM_ProductTable.LOCATIONID_FLD + ","
					+ ITM_ProductTable.BINID_FLD + ","
					+ ITM_ProductTable.PRIMARYVENDORID_FLD + ","
					+ ITM_ProductTable.VENDORLOCATIONID_FLD + ","
					+ ITM_ProductTable.SAFETYSTOCK_FLD + ","
					+ ITM_ProductTable.AGCID_FLD + ","
					+ ITM_ProductTable.PARTNAMEVN_FLD + ","
					+ ITM_ProductTable.INVENTORID_FLD + ","
					+ ITM_ProductTable.LICENSEFEE_FLD + ","
					+ ITM_ProductTable.LISTPRICE_FLD + ","
					+ ITM_ProductTable.VENDORCURRENCYID_FLD + ","
					+ ITM_ProductTable.QUANTITYSET_FLD + ","
					+ ITM_ProductTable.TAXCODE_FLD + ","
					+ ITM_ProductTable.MINPRODUCE_FLD + ","
					+ ITM_ProductTable.MAXPRODUCE_FLD + ","
					+ ITM_ProductTable.PRODUCTTYPEID_FLD + ","
					+ ITM_ProductTable.MAXROUNDUPTOMIN_FLD + ","
					+ ITM_ProductTable.MAXROUNDUPTOMULTIPLE_FLD + ","
					+ ITM_ProductTable.ACADJUSTMENTMASTERID_FLD + ","
					+ ITM_ProductTable.REGISTEREDCODE_FLD + ","
					+ ITM_ProductTable.ORDERPOINT_FLD + ")"
					+ " VALUES(?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.CODE_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[ITM_ProductTable.CODE_FLD].Value = objObject.Code;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.REVISION_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[ITM_ProductTable.REVISION_FLD].Value = objObject.Revision;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.DESCRIPTION_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[ITM_ProductTable.DESCRIPTION_FLD].Value = objObject.Description;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.SETUPDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[ITM_ProductTable.SETUPDATE_FLD].Value = objObject.SetupDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.VAT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[ITM_ProductTable.VAT_FLD].Value = objObject.VAT;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.IMPORTTAX_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[ITM_ProductTable.IMPORTTAX_FLD].Value = objObject.ImportTax;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.EXPORTTAX_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[ITM_ProductTable.EXPORTTAX_FLD].Value = objObject.ExportTax;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.SPECIALTAX_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[ITM_ProductTable.SPECIALTAX_FLD].Value = objObject.SpecialTax;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.MAKEITEM_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[ITM_ProductTable.MAKEITEM_FLD].Value = objObject.MakeItem;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.PARTNUMBER_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[ITM_ProductTable.PARTNUMBER_FLD].Value = objObject.PartNumber;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.OTHERINFO1_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[ITM_ProductTable.OTHERINFO1_FLD].Value = objObject.OtherInfo1;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.OTHERINFO2_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[ITM_ProductTable.OTHERINFO2_FLD].Value = objObject.OtherInfo2;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.LENGTH_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[ITM_ProductTable.LENGTH_FLD].Value = objObject.Length;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.WIDTH_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[ITM_ProductTable.WIDTH_FLD].Value = objObject.Width;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.HEIGHT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[ITM_ProductTable.HEIGHT_FLD].Value = objObject.Height;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.WEIGHT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[ITM_ProductTable.WEIGHT_FLD].Value = objObject.Weight;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.FINISHEDGOODS_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[ITM_ProductTable.FINISHEDGOODS_FLD].Value = objObject.FinishedGoods;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.SHELFLIFE_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[ITM_ProductTable.SHELFLIFE_FLD].Value = objObject.ShelfLife;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.LOTCONTROL_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[ITM_ProductTable.LOTCONTROL_FLD].Value = objObject.LotControl;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.QASTATUS_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[ITM_ProductTable.QASTATUS_FLD].Value = objObject.QAStatus;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.STOCK_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[ITM_ProductTable.STOCK_FLD].Value = objObject.Stock;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.PLANTYPE_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[ITM_ProductTable.PLANTYPE_FLD].Value = objObject.PlanType;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.AUTOCONVERSION_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[ITM_ProductTable.AUTOCONVERSION_FLD].Value = objObject.AutoConversion;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.ORDERQUANTITY_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[ITM_ProductTable.ORDERQUANTITY_FLD].Value = objObject.OrderQuantity;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.LTREQUISITION_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[ITM_ProductTable.LTREQUISITION_FLD].Value = objObject.LTRequisition;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.LTSAFETYSTOCK_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[ITM_ProductTable.LTSAFETYSTOCK_FLD].Value = objObject.LTSafetyStock;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.ORDERQUANTITYMULTIPLE_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[ITM_ProductTable.ORDERQUANTITYMULTIPLE_FLD].Value = objObject.OrderQuantityMultiple;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.SCRAPPERCENT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[ITM_ProductTable.SCRAPPERCENT_FLD].Value = objObject.ScrapPercent;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.MINIMUMSTOCK_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[ITM_ProductTable.MINIMUMSTOCK_FLD].Value = objObject.MinimumStock;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.MAXIMUMSTOCK_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[ITM_ProductTable.MAXIMUMSTOCK_FLD].Value = objObject.MaximumStock;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.CONVERSIONTOLERANCE_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[ITM_ProductTable.CONVERSIONTOLERANCE_FLD].Value = objObject.ConversionTolerance;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.VOUCHERTOLERANCE_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[ITM_ProductTable.VOUCHERTOLERANCE_FLD].Value = objObject.VoucherTolerance;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.RECEIVETOLERANCE_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[ITM_ProductTable.RECEIVETOLERANCE_FLD].Value = objObject.ReceiveTolerance;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.ISSUESIZE_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[ITM_ProductTable.ISSUESIZE_FLD].Value = objObject.IssueSize;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.LTFIXEDTIME_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[ITM_ProductTable.LTFIXEDTIME_FLD].Value = objObject.LTFixedTime;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.LTVARIABLETIME_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[ITM_ProductTable.LTVARIABLETIME_FLD].Value = objObject.LTVariableTime;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.LTDOCKTOSTOCK_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[ITM_ProductTable.LTDOCKTOSTOCK_FLD].Value = objObject.LTDocToStock;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.LTORDERPREPARE_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[ITM_ProductTable.LTORDERPREPARE_FLD].Value = objObject.LTOrderPrepare;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.LTSHIPPINGPREPARE_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[ITM_ProductTable.LTSHIPPINGPREPARE_FLD].Value = objObject.LTShippingPrepare;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.LTSALESATP_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[ITM_ProductTable.LTSALESATP_FLD].Value = objObject.LTSalesATP;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.SHIPTOLERANCEID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[ITM_ProductTable.SHIPTOLERANCEID_FLD].Value = objObject.ShipToleranceID;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.BUYERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[ITM_ProductTable.BUYERID_FLD].Value = objObject.BuyerID;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.BOMDESCRIPTION_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[ITM_ProductTable.BOMDESCRIPTION_FLD].Value = objObject.BOMDescription;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.BOMINCREMENT_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[ITM_ProductTable.BOMINCREMENT_FLD].Value = objObject.BomIncrement;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.ROUTINGDESCRIPTION_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[ITM_ProductTable.ROUTINGDESCRIPTION_FLD].Value = objObject.RoutingDescription;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.CREATEDATETIME_FLD, OleDbType.Date));
				ocmdPCS.Parameters[ITM_ProductTable.CREATEDATETIME_FLD].Value = objObject.CreateDateTime;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.UPDATEDATETIME_FLD, OleDbType.Date));
				ocmdPCS.Parameters[ITM_ProductTable.UPDATEDATETIME_FLD].Value = objObject.UpdateDateTime;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.COSTMETHOD_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[ITM_ProductTable.COSTMETHOD_FLD].Value = objObject.CostMethod;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.ROUTINGINCREMENT_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[ITM_ProductTable.ROUTINGINCREMENT_FLD].Value = objObject.RoutingIncrement;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[ITM_ProductTable.CCNID_FLD].Value = objObject.CCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.CATEGORYID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[ITM_ProductTable.CATEGORYID_FLD].Value = objObject.CategoryID;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.COSTCENTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[ITM_ProductTable.COSTCENTERID_FLD].Value = objObject.CostCenterID;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.DELETEREASONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[ITM_ProductTable.DELETEREASONID_FLD].Value = objObject.DeleteReasonID;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.DELIVERYPOLICYID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[ITM_ProductTable.DELIVERYPOLICYID_FLD].Value = objObject.DeliveryPolicyID;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.FORMATCODEID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[ITM_ProductTable.FORMATCODEID_FLD].Value = objObject.FormatCodeID;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.FREIGHTCLASSID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[ITM_ProductTable.FREIGHTCLASSID_FLD].Value = objObject.FreightClassID;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.HAZARDID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[ITM_ProductTable.HAZARDID_FLD].Value = objObject.HazardID;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.ORDERPOLICYID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[ITM_ProductTable.ORDERPOLICYID_FLD].Value = objObject.OrderPolicyID;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.ORDERRULEID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[ITM_ProductTable.ORDERRULEID_FLD].Value = objObject.OrderRuleID;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.SOURCEID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[ITM_ProductTable.SOURCEID_FLD].Value = objObject.SourceID;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.STOCKUMID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[ITM_ProductTable.STOCKUMID_FLD].Value = objObject.StockUMID;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.SELLINGUMID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[ITM_ProductTable.SELLINGUMID_FLD].Value = objObject.SellingUMID;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.HEIGHTUMID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[ITM_ProductTable.HEIGHTUMID_FLD].Value = objObject.HeightUMID;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.WIDTHUMID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[ITM_ProductTable.WIDTHUMID_FLD].Value = objObject.WidthUMID;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.LENGTHUMID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[ITM_ProductTable.LENGTHUMID_FLD].Value = objObject.LengthUMID;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.BUYINGUMID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[ITM_ProductTable.BUYINGUMID_FLD].Value = objObject.BuyingUMID;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.WEIGHTUMID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[ITM_ProductTable.WEIGHTUMID_FLD].Value = objObject.WeightUMID;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.LOTSIZE_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[ITM_ProductTable.LOTSIZE_FLD].Value = objObject.LotSize;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.MASTERLOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[ITM_ProductTable.MASTERLOCATIONID_FLD].Value = objObject.MasterLocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.LOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[ITM_ProductTable.LOCATIONID_FLD].Value = objObject.LocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.BINID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[ITM_ProductTable.BINID_FLD].Value = objObject.BinID;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.PRIMARYVENDORID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[ITM_ProductTable.PRIMARYVENDORID_FLD].Value = objObject.PrimaryVendorID;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.VENDORLOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[ITM_ProductTable.VENDORLOCATIONID_FLD].Value = objObject.VendorLocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.SAFETYSTOCK_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[ITM_ProductTable.SAFETYSTOCK_FLD].Value = objObject.SafetyStock;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.AGCID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[ITM_ProductTable.AGCID_FLD].Value = objObject.AGCID;

				//Begin_ Added by Tuan TQ
				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.PARTNAMEVN_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[ITM_ProductTable.PARTNAMEVN_FLD].Value = objObject.PartNameVN;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.INVENTORID_FLD, OleDbType.Integer));
				if (objObject.Inventor > 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.INVENTORID_FLD].Value = objObject.Inventor;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.INVENTORID_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.LICENSEFEE_FLD, OleDbType.Decimal));
				if (objObject.LicenseFee >= 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.LICENSEFEE_FLD].Value = objObject.LicenseFee;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.LICENSEFEE_FLD].Value = DBNull.Value;
				}

				//Added:  Nov 03, 2005
				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.LISTPRICE_FLD, OleDbType.Decimal));
				if (objObject.ListPrice >= 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.LISTPRICE_FLD].Value = objObject.ListPrice;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.LISTPRICE_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.VENDORCURRENCYID_FLD, OleDbType.Integer));
				if (objObject.VendorCurrencyID > 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.VENDORCURRENCYID_FLD].Value = objObject.VendorCurrencyID;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.VENDORCURRENCYID_FLD].Value = DBNull.Value;
				}
				//End Added:  Nov 03, 2005

				//Added:  Nov 08, 2005
				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.QUANTITYSET_FLD, OleDbType.Decimal));
				if (objObject.QuantitySet >= 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.QUANTITYSET_FLD].Value = objObject.QuantitySet;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.QUANTITYSET_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.TAXCODE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[ITM_ProductTable.TAXCODE_FLD].Value = objObject.TaxCode;

				//End Added:  Nov 08, 2005

				//Added:  01 Mar, 2006
				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.MINPRODUCE_FLD, OleDbType.Decimal));
				if (objObject.MinProduce >= 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.MINPRODUCE_FLD].Value = objObject.MinProduce;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.MINPRODUCE_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.MAXPRODUCE_FLD, OleDbType.Decimal));
				if (objObject.MaxProduce >= 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.MAXPRODUCE_FLD].Value = objObject.MaxProduce;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.MAXPRODUCE_FLD].Value = DBNull.Value;
				}
				//End

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.PRODUCTTYPEID_FLD, OleDbType.Integer));
				if (objObject.ProductTypeId > 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.PRODUCTTYPEID_FLD].Value = objObject.ProductTypeId;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.PRODUCTTYPEID_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.MAXROUNDUPTOMIN_FLD, OleDbType.Decimal));
				if (objObject.MaxRoundUpToMin >= 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.MAXROUNDUPTOMIN_FLD].Value = objObject.MaxRoundUpToMin;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.MAXROUNDUPTOMIN_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.MAXROUNDUPTOMULTIPLE_FLD, OleDbType.Decimal));
				if (objObject.MaxRoundUpToMultiple >= 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.MAXROUNDUPTOMULTIPLE_FLD].Value = objObject.MaxRoundUpToMultiple;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.MAXROUNDUPTOMULTIPLE_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.ACADJUSTMENTMASTERID_FLD, OleDbType.Integer));
				if (objObject.ACAdjustmentMasterID > 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.ACADJUSTMENTMASTERID_FLD].Value = objObject.ACAdjustmentMasterID;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.ACADJUSTMENTMASTERID_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.REGISTEREDCODE_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[ITM_ProductTable.REGISTEREDCODE_FLD].Value = objObject.RegisteredCode;
				//End_ Added by Tuan TQ

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.ORDERPOINT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[ITM_ProductTable.ORDERPOINT_FLD].Value = objObject.OrderPoint;

				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();
				ocmdPCS.ExecuteNonQuery();
			}
			catch (OleDbException ex)
			{
				if (ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_KEYCODE)
				{
					throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);
				}
				else
				{
					throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
				}
			}

			catch (InvalidOperationException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}
			catch (Exception ex)
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}

			finally
			{
				if (oconPCS != null)
				{
					if (oconPCS.State != ConnectionState.Closed)
					{
						oconPCS.Close();
					}
				}
			}
		}

		//**************************************************************************              
		///    <Description>
		///       This method uses to delete data from ITM_Product
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       void
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       HungLa
		///    </Authors>
		///    <History>
		///       09-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public void Delete(int pintID)
		{
			const string METHOD_NAME = THIS + ".Delete()";
			string strSql = String.Empty;
			strSql = "DELETE " + ITM_ProductTable.TABLE_NAME + " WHERE  " + "ProductID" + "=" + pintID.ToString();
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				ocmdPCS.ExecuteNonQuery();
				ocmdPCS = null;

			}
			catch (OleDbException ex)
			{
				if (ex.Errors[1].NativeError == ErrorCode.SQLCASCADE_PREVENT_KEYCODE)
				{
					throw new PCSDBException(ErrorCode.CASCADE_DELETE_PREVENT, METHOD_NAME, ex);
				}
				else
				{
					throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
				}
			}

			catch (Exception ex)
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}


			finally
			{
				if (oconPCS != null)
				{
					if (oconPCS.State != ConnectionState.Closed)
					{
						oconPCS.Close();
					}
				}
			}
		}

		//**************************************************************************              
		///    <Description>
		///       This method uses to get data from ITM_Product
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       ITM_ProductVO
		///    </Outputs>
		///    <Returns>
		///       ITM_ProductVO
		///    </Returns>
		///    <Authors>
		///       HungLa
		///    </Authors>
		///    <History>
		///       Tuesday, January 25, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public bool IsActualCost(int pintID)
		{
			const string METHOD_NAME = THIS + ".GetObjectVO()";
			DataSet dstPCS = new DataSet();

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				strSql = "SELECT COUNT(*) "
					+ " FROM " + ITM_ProductTable.TABLE_NAME
					+ " WHERE " + ITM_ProductTable.PRODUCTID_FLD + " = " + pintID.ToString()
					+ " AND " + ITM_ProductTable.COSTMETHOD_FLD + " = '0'";
				//CostMethod = 0 ==> Actual Cost

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				int intResult = int.Parse(ocmdPCS.ExecuteScalar().ToString());
				if (intResult > 0)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			catch (OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}

			catch (Exception ex)
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}

			finally
			{
				if (oconPCS != null)
				{
					if (oconPCS.State != ConnectionState.Closed)
					{
						oconPCS.Close();
					}
				}
			}
		}

		/// <summary>
		/// Check if product has been set Deleivery Schedule
		/// </summary>
		/// <param name="pintID"></param>
		/// <returns></returns>
		/// <author>Tuan TQ. 31 Mar, 2006</author>
		public bool HasVendorDeliverySchedule(int pintID)
		{
			const string METHOD_NAME = THIS + ".HasVendorDeliverySchedule()";

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			string strSql = "SELECT Count(ProductID)";
			strSql += " FROM PO_VendorDeliverySchedule";
			strSql += " WHERE ProductID = " + pintID.ToString();

			Utils utils = new Utils();
			oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
			ocmdPCS = new OleDbCommand(strSql, oconPCS);

			ocmdPCS.Connection.Open();
			int intResult = int.Parse(ocmdPCS.ExecuteScalar().ToString());
			if (intResult > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}


		public object GetObjectVO(int pintID)
		{
			const string METHOD_NAME = THIS + ".GetObjectVO()";
			DataSet dstPCS = new DataSet();

			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				strSql = "SELECT "
					+ ITM_ProductTable.PRODUCTID_FLD + ","
					+ ITM_ProductTable.CODE_FLD + ","
					+ ITM_ProductTable.REVISION_FLD + ","
					+ ITM_ProductTable.DESCRIPTION_FLD + ","
					+ ITM_ProductTable.SETUPDATE_FLD + ","
					+ ITM_ProductTable.VAT_FLD + ","
					+ ITM_ProductTable.IMPORTTAX_FLD + ","
					+ ITM_ProductTable.EXPORTTAX_FLD + ","
					+ ITM_ProductTable.SPECIALTAX_FLD + ","
					+ ITM_ProductTable.MAKEITEM_FLD + ","
					+ ITM_ProductTable.PARTNUMBER_FLD + ","
					+ ITM_ProductTable.OTHERINFO1_FLD + ","
					+ ITM_ProductTable.OTHERINFO2_FLD + ","
					+ ITM_ProductTable.LENGTH_FLD + ","
					+ ITM_ProductTable.WIDTH_FLD + ","
					+ ITM_ProductTable.HEIGHT_FLD + ","
					+ ITM_ProductTable.WEIGHT_FLD + ","
					+ ITM_ProductTable.FINISHEDGOODS_FLD + ","
					+ ITM_ProductTable.SHELFLIFE_FLD + ","
					+ ITM_ProductTable.LOTCONTROL_FLD + ","
					+ ITM_ProductTable.QASTATUS_FLD + ","
					+ ITM_ProductTable.STOCK_FLD + ","
					+ ITM_ProductTable.PLANTYPE_FLD + ","
					+ ITM_ProductTable.AUTOCONVERSION_FLD + ","
					+ ITM_ProductTable.ORDERQUANTITY_FLD + ","
					+ ITM_ProductTable.LTREQUISITION_FLD + ","
					+ ITM_ProductTable.LTSAFETYSTOCK_FLD + ","
					+ ITM_ProductTable.ORDERQUANTITYMULTIPLE_FLD + ","
					+ ITM_ProductTable.SCRAPPERCENT_FLD + ","
					+ ITM_ProductTable.MINIMUMSTOCK_FLD + ","
					+ ITM_ProductTable.MAXIMUMSTOCK_FLD + ","
					+ ITM_ProductTable.CONVERSIONTOLERANCE_FLD + ","
					+ ITM_ProductTable.VOUCHERTOLERANCE_FLD + ","
					+ ITM_ProductTable.RECEIVETOLERANCE_FLD + ","
					+ ITM_ProductTable.ISSUESIZE_FLD + ","
					+ ITM_ProductTable.LTFIXEDTIME_FLD + ","
					+ ITM_ProductTable.LTVARIABLETIME_FLD + ","
					+ ITM_ProductTable.LTDOCKTOSTOCK_FLD + ","
					+ ITM_ProductTable.LTORDERPREPARE_FLD + ","
					+ ITM_ProductTable.LTSHIPPINGPREPARE_FLD + ","
					+ ITM_ProductTable.LTSALESATP_FLD + ","
					+ ITM_ProductTable.SHIPTOLERANCEID_FLD + ","
					+ ITM_ProductTable.BUYERID_FLD + ","
					+ ITM_ProductTable.BOMDESCRIPTION_FLD + ","
					+ ITM_ProductTable.BOMINCREMENT_FLD + ","
					+ ITM_ProductTable.ROUTINGDESCRIPTION_FLD + ","
					+ ITM_ProductTable.CREATEDATETIME_FLD + ","
					+ ITM_ProductTable.UPDATEDATETIME_FLD + ","
					+ ITM_ProductTable.COSTMETHOD_FLD + ","
					+ ITM_ProductTable.ROUTINGINCREMENT_FLD + ","
					+ ITM_ProductTable.CCNID_FLD + ","
					+ ITM_ProductTable.CATEGORYID_FLD + ","
					+ ITM_ProductTable.COSTCENTERID_FLD + ","
					+ ITM_ProductTable.DELETEREASONID_FLD + ","
					+ ITM_ProductTable.DELIVERYPOLICYID_FLD + ","
					+ ITM_ProductTable.FORMATCODEID_FLD + ","
					+ ITM_ProductTable.FREIGHTCLASSID_FLD + ","
					+ ITM_ProductTable.HAZARDID_FLD + ","
					+ ITM_ProductTable.ORDERPOLICYID_FLD + ","
					+ ITM_ProductTable.ORDERRULEID_FLD + ","
					+ ITM_ProductTable.SOURCEID_FLD + ","
					+ ITM_ProductTable.STOCKUMID_FLD + ","
					+ ITM_ProductTable.SELLINGUMID_FLD + ","
					+ ITM_ProductTable.HEIGHTUMID_FLD + ","
					+ ITM_ProductTable.WIDTHUMID_FLD + ","
					+ ITM_ProductTable.LENGTHUMID_FLD + ","
					+ ITM_ProductTable.BUYINGUMID_FLD + ","
					+ ITM_ProductTable.WEIGHTUMID_FLD + ","
					+ ITM_ProductTable.LOTSIZE_FLD + ","
					+ ITM_ProductTable.MASTERLOCATIONID_FLD + ","
					+ ITM_ProductTable.LOCATIONID_FLD + ","
					+ ITM_ProductTable.BINID_FLD + ","
					+ ITM_ProductTable.PRIMARYVENDORID_FLD + ","
					+ ITM_ProductTable.VENDORLOCATIONID_FLD + ","
					+ ITM_ProductTable.SAFETYSTOCK_FLD + ","
					+ ITM_ProductTable.AGCID_FLD + ","
					+ ITM_ProductTable.PARTNAMEVN_FLD + ","
					+ ITM_ProductTable.INVENTORID_FLD + ","
					+ ITM_ProductTable.LICENSEFEE_FLD + ","
					+ ITM_ProductTable.LISTPRICE_FLD + ","
					+ ITM_ProductTable.VENDORCURRENCYID_FLD + ","
					+ ITM_ProductTable.QUANTITYSET_FLD + ","
					+ ITM_ProductTable.TAXCODE_FLD + ","
					+ ITM_ProductTable.MINPRODUCE_FLD + ","
					+ ITM_ProductTable.MAXPRODUCE_FLD + ","
					+ ITM_ProductTable.PRODUCTTYPEID_FLD + ", "
					// added: dungla 15-02-2005
					+ ITM_ProductTable.PRODUCTGROUPID_FLD + ", "
					+ ITM_ProductTable.PRODUCTIONLINEID_FLD + ", "
					+ ITM_ProductTable.COSTCENTERRATEMASTERID_FLD + ", "
					// end added: dungla 15-02-2005
					+ ITM_ProductTable.MAXROUNDUPTOMIN_FLD + ", "
					+ ITM_ProductTable.MAXROUNDUPTOMULTIPLE_FLD + ", "
					+ ITM_ProductTable.ACADJUSTMENTMASTERID_FLD + ", "
					+ ITM_ProductTable.REGISTEREDCODE_FLD + ", "
					+ ITM_ProductTable.ORDERPOINT_FLD
					+ " FROM " + ITM_ProductTable.TABLE_NAME
					+ " WHERE " + ITM_ProductTable.PRODUCTID_FLD + " = " + pintID.ToString();

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				ITM_ProductVO objObject = new ITM_ProductVO();

				while (odrPCS.Read())
				{
					objObject.ProductID = int.Parse(odrPCS[ITM_ProductTable.PRODUCTID_FLD].ToString().Trim());
					objObject.Code = odrPCS[ITM_ProductTable.CODE_FLD].ToString().Trim();
					objObject.Revision = odrPCS[ITM_ProductTable.REVISION_FLD].ToString().Trim();
					objObject.Description = odrPCS[ITM_ProductTable.DESCRIPTION_FLD].ToString().Trim();
					if (odrPCS[ITM_ProductTable.SETUPDATE_FLD] != DBNull.Value)
						objObject.SetupDate = DateTime.Parse(odrPCS[ITM_ProductTable.SETUPDATE_FLD].ToString().Trim());
					if (odrPCS[ITM_ProductTable.VAT_FLD] != DBNull.Value)
						objObject.VAT = float.Parse(odrPCS[ITM_ProductTable.VAT_FLD].ToString().Trim());
					if (odrPCS[ITM_ProductTable.IMPORTTAX_FLD] != DBNull.Value)
						objObject.ImportTax = float.Parse(odrPCS[ITM_ProductTable.IMPORTTAX_FLD].ToString().Trim());
					if (odrPCS[ITM_ProductTable.EXPORTTAX_FLD] != DBNull.Value)
						objObject.ExportTax = float.Parse(odrPCS[ITM_ProductTable.EXPORTTAX_FLD].ToString().Trim());
					if (odrPCS[ITM_ProductTable.SPECIALTAX_FLD] != DBNull.Value)
						objObject.SpecialTax = float.Parse(odrPCS[ITM_ProductTable.SPECIALTAX_FLD].ToString().Trim());
					if (odrPCS[ITM_ProductTable.MAKEITEM_FLD] != DBNull.Value)
						objObject.MakeItem = bool.Parse(odrPCS[ITM_ProductTable.MAKEITEM_FLD].ToString().Trim());
					objObject.PartNumber = odrPCS[ITM_ProductTable.PARTNUMBER_FLD].ToString().Trim();
					objObject.OtherInfo1 = odrPCS[ITM_ProductTable.OTHERINFO1_FLD].ToString().Trim();
					objObject.OtherInfo2 = odrPCS[ITM_ProductTable.OTHERINFO2_FLD].ToString().Trim();
					if (odrPCS[ITM_ProductTable.LENGTH_FLD] != DBNull.Value)
						objObject.Length = Decimal.Parse(odrPCS[ITM_ProductTable.LENGTH_FLD].ToString().Trim());
					if (odrPCS[ITM_ProductTable.WIDTH_FLD] != DBNull.Value)
						objObject.Width = Decimal.Parse(odrPCS[ITM_ProductTable.WIDTH_FLD].ToString().Trim());
					if (odrPCS[ITM_ProductTable.HEIGHT_FLD] != DBNull.Value)
						objObject.Height = Decimal.Parse(odrPCS[ITM_ProductTable.HEIGHT_FLD].ToString().Trim());
					if (odrPCS[ITM_ProductTable.WEIGHT_FLD] != DBNull.Value)
						objObject.Weight = Decimal.Parse(odrPCS[ITM_ProductTable.WEIGHT_FLD].ToString().Trim());
					if (odrPCS[ITM_ProductTable.FINISHEDGOODS_FLD] != DBNull.Value)
						objObject.FinishedGoods = bool.Parse(odrPCS[ITM_ProductTable.FINISHEDGOODS_FLD].ToString().Trim());
					if (odrPCS[ITM_ProductTable.SHELFLIFE_FLD] != DBNull.Value)
						objObject.ShelfLife = Decimal.Parse(odrPCS[ITM_ProductTable.SHELFLIFE_FLD].ToString().Trim());
					if (odrPCS[ITM_ProductTable.LOTCONTROL_FLD] != DBNull.Value)
						objObject.LotControl = bool.Parse(odrPCS[ITM_ProductTable.LOTCONTROL_FLD].ToString().Trim());
					if (odrPCS[ITM_ProductTable.QASTATUS_FLD] != DBNull.Value)
						objObject.QAStatus = int.Parse(odrPCS[ITM_ProductTable.QASTATUS_FLD].ToString().Trim());
					if (odrPCS[ITM_ProductTable.STOCK_FLD] != DBNull.Value)
						objObject.Stock = bool.Parse(odrPCS[ITM_ProductTable.STOCK_FLD].ToString().Trim());
					if (odrPCS[ITM_ProductTable.PLANTYPE_FLD] != DBNull.Value)
						objObject.PlanType = int.Parse(odrPCS[ITM_ProductTable.PLANTYPE_FLD].ToString().Trim());
					if (odrPCS[ITM_ProductTable.AUTOCONVERSION_FLD] != DBNull.Value)
						objObject.AutoConversion = bool.Parse(odrPCS[ITM_ProductTable.AUTOCONVERSION_FLD].ToString().Trim());
					if (odrPCS[ITM_ProductTable.ORDERQUANTITY_FLD] != DBNull.Value)
						objObject.OrderQuantity = Decimal.Parse(odrPCS[ITM_ProductTable.ORDERQUANTITY_FLD].ToString().Trim());
					if (odrPCS[ITM_ProductTable.LTREQUISITION_FLD] != DBNull.Value)
						objObject.LTRequisition = Decimal.Parse(odrPCS[ITM_ProductTable.LTREQUISITION_FLD].ToString().Trim());
					if (odrPCS[ITM_ProductTable.LTSAFETYSTOCK_FLD] != DBNull.Value)
						objObject.LTSafetyStock = Decimal.Parse(odrPCS[ITM_ProductTable.LTSAFETYSTOCK_FLD].ToString().Trim());
					if (odrPCS[ITM_ProductTable.ORDERQUANTITYMULTIPLE_FLD] != DBNull.Value)
						objObject.OrderQuantityMultiple = Decimal.Parse(odrPCS[ITM_ProductTable.ORDERQUANTITYMULTIPLE_FLD].ToString().Trim());
					if (odrPCS[ITM_ProductTable.SCRAPPERCENT_FLD] != DBNull.Value)
						objObject.ScrapPercent = Decimal.Parse(odrPCS[ITM_ProductTable.SCRAPPERCENT_FLD].ToString().Trim());
					if (odrPCS[ITM_ProductTable.MINIMUMSTOCK_FLD] != DBNull.Value)
						objObject.MinimumStock = Decimal.Parse(odrPCS[ITM_ProductTable.MINIMUMSTOCK_FLD].ToString().Trim());
					if (odrPCS[ITM_ProductTable.MAXIMUMSTOCK_FLD] != DBNull.Value)
						objObject.MaximumStock = Decimal.Parse(odrPCS[ITM_ProductTable.MAXIMUMSTOCK_FLD].ToString().Trim());
					if (odrPCS[ITM_ProductTable.CONVERSIONTOLERANCE_FLD] != DBNull.Value)
						objObject.ConversionTolerance = Decimal.Parse(odrPCS[ITM_ProductTable.CONVERSIONTOLERANCE_FLD].ToString().Trim());
					if (odrPCS[ITM_ProductTable.VOUCHERTOLERANCE_FLD] != DBNull.Value)
						objObject.VoucherTolerance = Decimal.Parse(odrPCS[ITM_ProductTable.VOUCHERTOLERANCE_FLD].ToString().Trim());
					if (odrPCS[ITM_ProductTable.RECEIVETOLERANCE_FLD] != DBNull.Value)
						objObject.ReceiveTolerance = Decimal.Parse(odrPCS[ITM_ProductTable.RECEIVETOLERANCE_FLD].ToString().Trim());
					if (odrPCS[ITM_ProductTable.ISSUESIZE_FLD] != DBNull.Value)
						objObject.IssueSize = Decimal.Parse(odrPCS[ITM_ProductTable.ISSUESIZE_FLD].ToString().Trim());
					if (odrPCS[ITM_ProductTable.LTFIXEDTIME_FLD] != DBNull.Value)
						objObject.LTFixedTime = Decimal.Parse(odrPCS[ITM_ProductTable.LTFIXEDTIME_FLD].ToString().Trim());
					if (odrPCS[ITM_ProductTable.LTVARIABLETIME_FLD] != DBNull.Value)
						objObject.LTVariableTime = Decimal.Parse(odrPCS[ITM_ProductTable.LTVARIABLETIME_FLD].ToString().Trim());
					if (odrPCS[ITM_ProductTable.LTDOCKTOSTOCK_FLD] != DBNull.Value)
						objObject.LTDocToStock = Decimal.Parse(odrPCS[ITM_ProductTable.LTDOCKTOSTOCK_FLD].ToString().Trim());
					if (odrPCS[ITM_ProductTable.LTORDERPREPARE_FLD] != DBNull.Value)
						objObject.LTOrderPrepare = Decimal.Parse(odrPCS[ITM_ProductTable.LTORDERPREPARE_FLD].ToString().Trim());
					if (odrPCS[ITM_ProductTable.LTSHIPPINGPREPARE_FLD] != DBNull.Value)
						objObject.LTShippingPrepare = Decimal.Parse(odrPCS[ITM_ProductTable.LTSHIPPINGPREPARE_FLD].ToString().Trim());
					if (odrPCS[ITM_ProductTable.LTSALESATP_FLD] != DBNull.Value)
						objObject.LTSalesATP = Decimal.Parse(odrPCS[ITM_ProductTable.LTSALESATP_FLD].ToString().Trim());
					if (odrPCS[ITM_ProductTable.SHIPTOLERANCEID_FLD] != DBNull.Value)
						objObject.ShipToleranceID = int.Parse(odrPCS[ITM_ProductTable.SHIPTOLERANCEID_FLD].ToString().Trim());
					if (odrPCS[ITM_ProductTable.BUYERID_FLD] != DBNull.Value)
						objObject.BuyerID = int.Parse(odrPCS[ITM_ProductTable.BUYERID_FLD].ToString().Trim());
					objObject.BOMDescription = odrPCS[ITM_ProductTable.BOMDESCRIPTION_FLD].ToString().Trim();
					if (odrPCS[ITM_ProductTable.BOMINCREMENT_FLD] != DBNull.Value)
						objObject.BomIncrement = int.Parse(odrPCS[ITM_ProductTable.BOMINCREMENT_FLD].ToString().Trim());
					objObject.RoutingDescription = odrPCS[ITM_ProductTable.ROUTINGDESCRIPTION_FLD].ToString().Trim();
					if (odrPCS[ITM_ProductTable.CREATEDATETIME_FLD] != DBNull.Value)
						objObject.CreateDateTime = DateTime.Parse(odrPCS[ITM_ProductTable.CREATEDATETIME_FLD].ToString().Trim());
					if (odrPCS[ITM_ProductTable.UPDATEDATETIME_FLD] != DBNull.Value)
						objObject.UpdateDateTime = DateTime.Parse(odrPCS[ITM_ProductTable.UPDATEDATETIME_FLD].ToString().Trim());
					if (odrPCS[ITM_ProductTable.COSTMETHOD_FLD] != DBNull.Value)
						objObject.CostMethod = int.Parse(odrPCS[ITM_ProductTable.COSTMETHOD_FLD].ToString().Trim());
					if (odrPCS[ITM_ProductTable.ROUTINGINCREMENT_FLD] != DBNull.Value)
						objObject.RoutingIncrement = int.Parse(odrPCS[ITM_ProductTable.ROUTINGINCREMENT_FLD].ToString().Trim());
					if (odrPCS[ITM_ProductTable.CCNID_FLD] != DBNull.Value)
						objObject.CCNID = int.Parse(odrPCS[ITM_ProductTable.CCNID_FLD].ToString().Trim());
					if (odrPCS[ITM_ProductTable.CATEGORYID_FLD] != DBNull.Value)
						objObject.CategoryID = int.Parse(odrPCS[ITM_ProductTable.CATEGORYID_FLD].ToString().Trim());
					if (odrPCS[ITM_ProductTable.COSTCENTERID_FLD] != DBNull.Value)
						objObject.CostCenterID = int.Parse(odrPCS[ITM_ProductTable.COSTCENTERID_FLD].ToString().Trim());
					if (odrPCS[ITM_ProductTable.DELETEREASONID_FLD] != DBNull.Value)
						objObject.DeleteReasonID = int.Parse(odrPCS[ITM_ProductTable.DELETEREASONID_FLD].ToString().Trim());
					if (odrPCS[ITM_ProductTable.DELIVERYPOLICYID_FLD] != DBNull.Value)
						objObject.DeliveryPolicyID = int.Parse(odrPCS[ITM_ProductTable.DELIVERYPOLICYID_FLD].ToString().Trim());
					if (odrPCS[ITM_ProductTable.FORMATCODEID_FLD] != DBNull.Value)
						objObject.FormatCodeID = int.Parse(odrPCS[ITM_ProductTable.FORMATCODEID_FLD].ToString().Trim());
					if (odrPCS[ITM_ProductTable.FREIGHTCLASSID_FLD] != DBNull.Value)
						objObject.FreightClassID = int.Parse(odrPCS[ITM_ProductTable.FREIGHTCLASSID_FLD].ToString().Trim());
					if (odrPCS[ITM_ProductTable.HAZARDID_FLD] != DBNull.Value)
						objObject.HazardID = int.Parse(odrPCS[ITM_ProductTable.HAZARDID_FLD].ToString().Trim());
					if (odrPCS[ITM_ProductTable.ORDERPOLICYID_FLD] != DBNull.Value)
						objObject.OrderPolicyID = int.Parse(odrPCS[ITM_ProductTable.ORDERPOLICYID_FLD].ToString().Trim());
					if (odrPCS[ITM_ProductTable.ORDERRULEID_FLD] != DBNull.Value)
						objObject.OrderRuleID = int.Parse(odrPCS[ITM_ProductTable.ORDERRULEID_FLD].ToString().Trim());
					if (odrPCS[ITM_ProductTable.SOURCEID_FLD] != DBNull.Value)
						objObject.SourceID = int.Parse(odrPCS[ITM_ProductTable.SOURCEID_FLD].ToString().Trim());
					if (odrPCS[ITM_ProductTable.STOCKUMID_FLD] != DBNull.Value)
						objObject.StockUMID = int.Parse(odrPCS[ITM_ProductTable.STOCKUMID_FLD].ToString().Trim());
					if (odrPCS[ITM_ProductTable.SELLINGUMID_FLD] != DBNull.Value)
						objObject.SellingUMID = int.Parse(odrPCS[ITM_ProductTable.SELLINGUMID_FLD].ToString().Trim());
					if (odrPCS[ITM_ProductTable.HEIGHTUMID_FLD] != DBNull.Value)
						objObject.HeightUMID = int.Parse(odrPCS[ITM_ProductTable.HEIGHTUMID_FLD].ToString().Trim());
					if (odrPCS[ITM_ProductTable.WIDTHUMID_FLD] != DBNull.Value)
						objObject.WidthUMID = int.Parse(odrPCS[ITM_ProductTable.WIDTHUMID_FLD].ToString().Trim());
					if (odrPCS[ITM_ProductTable.LENGTHUMID_FLD] != DBNull.Value)
						objObject.LengthUMID = int.Parse(odrPCS[ITM_ProductTable.LENGTHUMID_FLD].ToString().Trim());
					if (odrPCS[ITM_ProductTable.BUYINGUMID_FLD] != DBNull.Value)
						objObject.BuyingUMID = int.Parse(odrPCS[ITM_ProductTable.BUYINGUMID_FLD].ToString().Trim());
					if (odrPCS[ITM_ProductTable.WEIGHTUMID_FLD] != DBNull.Value)
						objObject.WeightUMID = int.Parse(odrPCS[ITM_ProductTable.WEIGHTUMID_FLD].ToString().Trim());
					if (odrPCS[ITM_ProductTable.LOTSIZE_FLD] != DBNull.Value)
						objObject.LotSize = int.Parse(odrPCS[ITM_ProductTable.LOTSIZE_FLD].ToString().Trim());
					if (odrPCS[ITM_ProductTable.MASTERLOCATIONID_FLD] != DBNull.Value)
						objObject.MasterLocationID = int.Parse(odrPCS[ITM_ProductTable.MASTERLOCATIONID_FLD].ToString().Trim());
					if (odrPCS[ITM_ProductTable.LOCATIONID_FLD] != DBNull.Value)
						objObject.LocationID = int.Parse(odrPCS[ITM_ProductTable.LOCATIONID_FLD].ToString().Trim());

					if (odrPCS[ITM_ProductTable.BINID_FLD] != DBNull.Value)
						objObject.BinID = int.Parse(odrPCS[ITM_ProductTable.BINID_FLD].ToString().Trim());

					if (odrPCS[ITM_ProductTable.PRIMARYVENDORID_FLD] != DBNull.Value)
						objObject.PrimaryVendorID = int.Parse(odrPCS[ITM_ProductTable.PRIMARYVENDORID_FLD].ToString().Trim());

					if (odrPCS[ITM_ProductTable.VENDORLOCATIONID_FLD] != DBNull.Value)
						objObject.VendorLocationID = int.Parse(odrPCS[ITM_ProductTable.VENDORLOCATIONID_FLD].ToString().Trim());

					if (odrPCS[ITM_ProductTable.SAFETYSTOCK_FLD] != DBNull.Value)
						objObject.SafetyStock = Decimal.Parse(odrPCS[ITM_ProductTable.SAFETYSTOCK_FLD].ToString().Trim());

					if (odrPCS[ITM_ProductTable.AGCID_FLD] != DBNull.Value)
						objObject.AGCID = int.Parse(odrPCS[ITM_ProductTable.AGCID_FLD].ToString().Trim());

					//Begin_ Added by Tuan TQ 2005-09-22
					objObject.RegisteredCode = odrPCS[ITM_ProductTable.REGISTEREDCODE_FLD].ToString().Trim();
					objObject.PartNameVN = odrPCS[ITM_ProductTable.PARTNAMEVN_FLD].ToString().Trim();
					objObject.TaxCode = odrPCS[ITM_ProductTable.TAXCODE_FLD].ToString().Trim();

					if (odrPCS[ITM_ProductTable.LICENSEFEE_FLD] != DBNull.Value)
						objObject.LicenseFee = decimal.Parse(odrPCS[ITM_ProductTable.LICENSEFEE_FLD].ToString().Trim());

					if (odrPCS[ITM_ProductTable.INVENTORID_FLD] != DBNull.Value)
						objObject.Inventor = int.Parse(odrPCS[ITM_ProductTable.INVENTORID_FLD].ToString().Trim());

					if (odrPCS[ITM_ProductTable.PRODUCTTYPEID_FLD] != DBNull.Value)
						objObject.ProductTypeId = int.Parse(odrPCS[ITM_ProductTable.PRODUCTTYPEID_FLD].ToString().Trim());

					if (odrPCS[ITM_ProductTable.LISTPRICE_FLD] != DBNull.Value)
						objObject.ListPrice = Decimal.Parse(odrPCS[ITM_ProductTable.LISTPRICE_FLD].ToString().Trim());

					if (odrPCS[ITM_ProductTable.VENDORCURRENCYID_FLD] != DBNull.Value)
						objObject.VendorCurrencyID = int.Parse(odrPCS[ITM_ProductTable.VENDORCURRENCYID_FLD].ToString().Trim());

					if (odrPCS[ITM_ProductTable.QUANTITYSET_FLD] != DBNull.Value)
						objObject.QuantitySet = Decimal.Parse(odrPCS[ITM_ProductTable.QUANTITYSET_FLD].ToString().Trim());

					//End_ Added by Tuan TQ 2005-09-22

					//Added by Tuan TQ. 01 Mar, 2006
					if (odrPCS[ITM_ProductTable.MINPRODUCE_FLD] != DBNull.Value)
						objObject.MinProduce = Decimal.Parse(odrPCS[ITM_ProductTable.MINPRODUCE_FLD].ToString().Trim());

					if (odrPCS[ITM_ProductTable.MAXPRODUCE_FLD] != DBNull.Value)
						objObject.MaxProduce = Decimal.Parse(odrPCS[ITM_ProductTable.MAXPRODUCE_FLD].ToString().Trim());
					//End added

					//HACK: added by Tuan TQ. 17 May, 2006
					if (odrPCS[ITM_ProductTable.MAXROUNDUPTOMIN_FLD] != DBNull.Value)
					{
						objObject.MaxRoundUpToMin = Decimal.Parse(odrPCS[ITM_ProductTable.MAXROUNDUPTOMIN_FLD].ToString().Trim());
					}

					if (odrPCS[ITM_ProductTable.MAXROUNDUPTOMULTIPLE_FLD] != DBNull.Value)
					{
						objObject.MaxRoundUpToMultiple = Decimal.Parse(odrPCS[ITM_ProductTable.MAXROUNDUPTOMULTIPLE_FLD].ToString().Trim());
					}

					if (odrPCS[ITM_ProductTable.ACADJUSTMENTMASTERID_FLD] != DBNull.Value)
					{
						objObject.ACAdjustmentMasterID = int.Parse(odrPCS[ITM_ProductTable.ACADJUSTMENTMASTERID_FLD].ToString().Trim());
					}
					//end hack

					if (odrPCS[ITM_ProductTable.ORDERPOINT_FLD] != DBNull.Value)
						objObject.OrderPoint = Decimal.Parse(odrPCS[ITM_ProductTable.ORDERPOINT_FLD].ToString().Trim());

					// added: dungla 15-02-2005
					try
					{
						objObject.CostCenterRateMasterID = int.Parse(odrPCS[ITM_ProductTable.COSTCENTERRATEMASTERID_FLD].ToString().Trim());
					}
					catch
					{
					}

					try
					{
						objObject.ProductGroupID = int.Parse(odrPCS[ITM_ProductTable.PRODUCTGROUPID_FLD].ToString().Trim());
					}
					catch
					{
					}

					try
					{
						objObject.ProductionLineID = int.Parse(odrPCS[ITM_ProductTable.PRODUCTIONLINEID_FLD].ToString().Trim());
					}
					catch
					{
					}
					// end added: dungla 15-02-2005
				}

				return objObject;
			}
			catch (OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}

			catch (Exception ex)
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}

			finally
			{
				if (oconPCS != null)
				{
					if (oconPCS.State != ConnectionState.Closed)
					{
						oconPCS.Close();
					}
				}
			}
		}

		//**************************************************************************              
		///    <Description>
		///    </Description>
		///    <Inputs>
		///    </Inputs>
		///    <Outputs>
		///    </Outputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <Authors>
		///       TuanDm
		///    </Authors>
		///    <History>
		///       Thursday, Mar 23, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public object GetObjectVOForBOM(int pintID)
		{
			const string METHOD_NAME = THIS + ".GetObjectVOForBOM()";
			DataSet dstPCS = new DataSet();

			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				strSql = "SELECT "
					+ ITM_ProductTable.PRODUCTID_FLD + ","
					+ ITM_ProductTable.CODE_FLD + ","
					+ ITM_ProductTable.REVISION_FLD + ","
					+ ITM_ProductTable.DESCRIPTION_FLD + ","
					+ ITM_ProductTable.BOMDESCRIPTION_FLD + ","
					+ ITM_ProductTable.MAKEITEM_FLD + ","
					+ ITM_ProductTable.CCNID_FLD + ","
					+ ITM_ProductTable.BOMINCREMENT_FLD
					+ " FROM " + ITM_ProductTable.TABLE_NAME
					+ " WHERE " + ITM_ProductTable.PRODUCTID_FLD + " = " + pintID.ToString();

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				ITM_ProductVO objObject = new ITM_ProductVO();

				while (odrPCS.Read())
				{
					objObject.ProductID = int.Parse(odrPCS[ITM_ProductTable.PRODUCTID_FLD].ToString().Trim());
					objObject.Code = odrPCS[ITM_ProductTable.CODE_FLD].ToString().Trim();
					objObject.Revision = odrPCS[ITM_ProductTable.REVISION_FLD].ToString().Trim();
					objObject.Description = odrPCS[ITM_ProductTable.DESCRIPTION_FLD].ToString().Trim();
					objObject.BOMDescription = odrPCS[ITM_ProductTable.BOMDESCRIPTION_FLD].ToString().Trim();
					objObject.CCNID = int.Parse(odrPCS[ITM_ProductTable.CCNID_FLD].ToString().Trim());
					if (odrPCS[ITM_ProductTable.BOMINCREMENT_FLD] != DBNull.Value)
						objObject.BomIncrement = int.Parse(odrPCS[ITM_ProductTable.BOMINCREMENT_FLD].ToString().Trim());
					if (odrPCS[ITM_ProductTable.MAKEITEM_FLD] != DBNull.Value)
					{
						objObject.MakeItem = bool.Parse(odrPCS[ITM_ProductTable.MAKEITEM_FLD].ToString());
					}
				}
				return objObject;
			}
			catch (OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}

			catch (Exception ex)
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}

			finally
			{
				if (oconPCS != null)
				{
					if (oconPCS.State != ConnectionState.Closed)
					{
						oconPCS.Close();
					}
				}
			}
		}

		//**************************************************************************              
		///    <Description>
		///    </Description>
		///    <Inputs>
		///    </Inputs>
		///    <Outputs>
		///    </Outputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <Authors>
		///       TuanDm
		///    </Authors>
		///    <History>
		///       Thursday, Mar 23, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public object GetObjectVOForBOMByCode(string pstrCode)
		{
			const string METHOD_NAME = THIS + ".GetObjectVOForBOM()";
			DataSet dstPCS = new DataSet();

			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				strSql = "SELECT "
					+ ITM_ProductTable.PRODUCTID_FLD + ","
					+ ITM_ProductTable.CODE_FLD + ","
					+ ITM_ProductTable.REVISION_FLD + ","
					+ ITM_ProductTable.DESCRIPTION_FLD + ","
					+ ITM_ProductTable.BOMDESCRIPTION_FLD + ","
					+ ITM_ProductTable.MAKEITEM_FLD + ","
					+ ITM_ProductTable.BOMINCREMENT_FLD
					+ " FROM " + ITM_ProductTable.TABLE_NAME
					+ " WHERE " + ITM_ProductTable.CODE_FLD + " = '" + pstrCode.ToString() + "'";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				ITM_ProductVO objObject = new ITM_ProductVO();

				while (odrPCS.Read())
				{
					objObject.ProductID = int.Parse(odrPCS[ITM_ProductTable.PRODUCTID_FLD].ToString().Trim());
					objObject.Code = odrPCS[ITM_ProductTable.CODE_FLD].ToString().Trim();
					objObject.Revision = odrPCS[ITM_ProductTable.REVISION_FLD].ToString().Trim();
					objObject.Description = odrPCS[ITM_ProductTable.DESCRIPTION_FLD].ToString().Trim();
					objObject.BOMDescription = odrPCS[ITM_ProductTable.BOMDESCRIPTION_FLD].ToString().Trim();
					if (odrPCS[ITM_ProductTable.BOMINCREMENT_FLD] != DBNull.Value)
						objObject.BomIncrement = int.Parse(odrPCS[ITM_ProductTable.BOMINCREMENT_FLD].ToString().Trim());
					if (odrPCS[ITM_ProductTable.MAKEITEM_FLD] != DBNull.Value)
					{
						objObject.MakeItem = bool.Parse(odrPCS[ITM_ProductTable.MAKEITEM_FLD].ToString());
					}
				}
				return objObject;
			}
			catch (OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}

			catch (Exception ex)
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}

			finally
			{
				if (oconPCS != null)
				{
					if (oconPCS.State != ConnectionState.Closed)
					{
						oconPCS.Close();
					}
				}
			}
		}

		//**************************************************************************              
		///    <Description>
		///       This method uses to get data from ITM_Product
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       ITM_ProductVO
		///    </Outputs>
		///    <Returns>
		///       ITM_ProductVO
		///    </Returns>
		///    <Authors>
		///       HungLa
		///    </Authors>
		///    <History>
		///       Tuesday, January 25, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public DataRow GetProductCostCenter(int pintID)
		{
			const string METHOD_NAME = THIS + ".GetObjectVO()";
			DataSet dstPCS = new DataSet();
			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				strSql = "SELECT "
					+ "P." + ITM_ProductTable.COSTCENTERID_FLD + ","
					+ "CC." + ITM_CostCenterTable.CODE_FLD
					+ " FROM " + ITM_ProductTable.TABLE_NAME + " P "
					+ " LEFT JOIN " + ITM_CostCenterTable.TABLE_NAME + " CC ON CC.CostCenterID=P.CostCenterID"
					+ " WHERE " + ITM_ProductTable.PRODUCTID_FLD + " = " + pintID.ToString();

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, ITM_ProductTable.TABLE_NAME);

				return dstPCS.Tables[0].Rows[0];
			}
			catch (OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}

			catch (Exception ex)
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}

			finally
			{
				if (oconPCS != null)
				{
					if (oconPCS.State != ConnectionState.Closed)
					{
						oconPCS.Close();
					}
				}
			}
		}

		//**************************************************************************              
		///    <Description>
		///       This method uses to get data from ITM_Product
		///    </Description>
		///    <Inputs>
		///        ID       
		///    </Inputs>
		///    <Outputs>
		///       ITM_ProductVO
		///    </Outputs>
		///    <Returns>
		///       ITM_ProductVO
		///    </Returns>
		///    <Authors>
		///       HungLa
		///    </Authors>
		///    <History>
		///       Tuesday, January 25, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public object GetProductInfo(int pintID)
		{
			const string METHOD_NAME = THIS + ".GetProductInfo()";
			DataSet dstPCS = new DataSet();

			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = "SELECT "
					+ ITM_ProductTable.PRODUCTID_FLD + ","
					+ ITM_ProductTable.CODE_FLD + ","
					+ ITM_ProductTable.REVISION_FLD + ","
					+ ITM_ProductTable.DESCRIPTION_FLD + ","
					+ ITM_ProductTable.SETUPDATE_FLD + ","
					+ ITM_ProductTable.VAT_FLD + ","
					+ ITM_ProductTable.IMPORTTAX_FLD + ","
					+ ITM_ProductTable.EXPORTTAX_FLD + ","
					+ ITM_ProductTable.SPECIALTAX_FLD + ","
					+ ITM_ProductTable.MAKEITEM_FLD + ","
					+ ITM_ProductTable.PARTNUMBER_FLD + ","
					+ ITM_ProductTable.OTHERINFO1_FLD + ","
					+ ITM_ProductTable.OTHERINFO2_FLD + ","
					+ ITM_ProductTable.LENGTH_FLD + ","
					+ ITM_ProductTable.WIDTH_FLD + ","
					+ ITM_ProductTable.HEIGHT_FLD + ","
					+ ITM_ProductTable.WEIGHT_FLD + ","
					+ ITM_ProductTable.FINISHEDGOODS_FLD + ","
					+ ITM_ProductTable.SHELFLIFE_FLD + ","
					+ ITM_ProductTable.LOTCONTROL_FLD + ","
					+ ITM_ProductTable.QASTATUS_FLD + ","
					+ ITM_ProductTable.STOCK_FLD + ","
					+ ITM_ProductTable.PLANTYPE_FLD + ","
					+ ITM_ProductTable.AUTOCONVERSION_FLD + ","
					+ ITM_ProductTable.ORDERQUANTITY_FLD + ","
					+ ITM_ProductTable.LTREQUISITION_FLD + ","
					+ ITM_ProductTable.LTSAFETYSTOCK_FLD + ","
					+ ITM_ProductTable.ORDERQUANTITYMULTIPLE_FLD + ","
					+ ITM_ProductTable.SCRAPPERCENT_FLD + ","
					+ ITM_ProductTable.MINIMUMSTOCK_FLD + ","
					+ ITM_ProductTable.MAXIMUMSTOCK_FLD + ","
					+ ITM_ProductTable.CONVERSIONTOLERANCE_FLD + ","
					+ ITM_ProductTable.VOUCHERTOLERANCE_FLD + ","
					+ ITM_ProductTable.RECEIVETOLERANCE_FLD + ","
					+ ITM_ProductTable.ISSUESIZE_FLD + ","
					+ ITM_ProductTable.LTFIXEDTIME_FLD + ","
					+ ITM_ProductTable.LTVARIABLETIME_FLD + ","
					+ ITM_ProductTable.LTDOCKTOSTOCK_FLD + ","
					+ ITM_ProductTable.LTORDERPREPARE_FLD + ","
					+ ITM_ProductTable.LTSHIPPINGPREPARE_FLD + ","
					+ ITM_ProductTable.LTSALESATP_FLD + ","
					+ ITM_ProductTable.SHIPTOLERANCEID_FLD + ","
					+ ITM_ProductTable.BUYERID_FLD + ","
					+ ITM_ProductTable.BOMDESCRIPTION_FLD + ","
					+ ITM_ProductTable.BOMINCREMENT_FLD + ","
					+ ITM_ProductTable.ROUTINGDESCRIPTION_FLD + ","
					+ ITM_ProductTable.CREATEDATETIME_FLD + ","
					+ ITM_ProductTable.UPDATEDATETIME_FLD + ","
					+ ITM_ProductTable.COSTMETHOD_FLD + ","
					+ ITM_ProductTable.ROUTINGINCREMENT_FLD + ","
					+ ITM_ProductTable.CCNID_FLD + ","
					+ ITM_ProductTable.CATEGORYID_FLD + ","
					+ ITM_ProductTable.COSTCENTERID_FLD + ","
					+ ITM_ProductTable.DELETEREASONID_FLD + ","
					+ ITM_ProductTable.DELIVERYPOLICYID_FLD + ","
					+ ITM_ProductTable.FORMATCODEID_FLD + ","
					+ ITM_ProductTable.FREIGHTCLASSID_FLD + ","
					+ ITM_ProductTable.HAZARDID_FLD + ","
					+ ITM_ProductTable.ORDERPOLICYID_FLD + ","
					+ ITM_ProductTable.ORDERRULEID_FLD + ","
					+ ITM_ProductTable.SOURCEID_FLD + ","
					+ ITM_ProductTable.STOCKUMID_FLD + ","
					+ ITM_ProductTable.SELLINGUMID_FLD + ","
					+ ITM_ProductTable.HEIGHTUMID_FLD + ","
					+ ITM_ProductTable.WIDTHUMID_FLD + ","
					+ ITM_ProductTable.LENGTHUMID_FLD + ","
					+ ITM_ProductTable.BUYINGUMID_FLD + ","
					+ ITM_ProductTable.WEIGHTUMID_FLD + ","
					+ ITM_ProductTable.LOTSIZE_FLD + ","
					+ ITM_ProductTable.MASTERLOCATIONID_FLD + ","
					+ ITM_ProductTable.LOCATIONID_FLD + ","
					+ ITM_ProductTable.BINID_FLD + ","
					+ ITM_ProductTable.PRIMARYVENDORID_FLD + ","
					+ ITM_ProductTable.VENDORLOCATIONID_FLD + ","
					+ ITM_ProductTable.SAFETYSTOCK_FLD + ","
					+ ITM_ProductTable.AGCID_FLD + ","
					+ ITM_ProductTable.PARTNAMEVN_FLD + ","
					+ ITM_ProductTable.INVENTORID_FLD + ","
					+ ITM_ProductTable.LICENSEFEE_FLD + ","
					+ ITM_ProductTable.LISTPRICE_FLD + ","
					+ ITM_ProductTable.TAXCODE_FLD + ","
					+ ITM_ProductTable.VENDORCURRENCYID_FLD + ","
					+ ITM_ProductTable.QUANTITYSET_FLD + ","
					+ ITM_ProductTable.MINPRODUCE_FLD + ","
					+ ITM_ProductTable.MAXPRODUCE_FLD + ","
					+ ITM_ProductTable.PRODUCTTYPEID_FLD + ","
					+ ITM_ProductTable.PICTURE_FLD + ","
					+ ITM_ProductTable.SETUPPAIR_FLD + ","
					+ ITM_ProductTable.MAXROUNDUPTOMIN_FLD + ", "
					+ ITM_ProductTable.MAXROUNDUPTOMULTIPLE_FLD + ", "
					+ ITM_ProductTable.ACADJUSTMENTMASTERID_FLD + ", "
					+ ITM_ProductTable.REGISTEREDCODE_FLD + ", "
					+ ITM_ProductTable.AVEG_FLD + ", "
					+ ITM_ProductTable.MASSORDER_FLD + ", "
					+ ITM_ProductTable.STOCKTAKINGCODE_FLD + ", "
                    + ITM_ProductTable.ALLOWNEGATIVEQTY_FLD + ", "
					+ ITM_ProductTable.ORDERPOINT_FLD
					+ " FROM " + ITM_ProductTable.TABLE_NAME
					+ " WHERE " + ITM_ProductTable.PRODUCTID_FLD + "=" + pintID;

				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				ITM_ProductVO objObject = new ITM_ProductVO();

				while (odrPCS.Read())
				{
					if (odrPCS[ITM_ProductTable.PRODUCTID_FLD] != DBNull.Value)
					{
						objObject.ProductID = int.Parse(odrPCS[ITM_ProductTable.PRODUCTID_FLD].ToString().Trim());
					}
					objObject.Code = odrPCS[ITM_ProductTable.CODE_FLD].ToString().Trim();
					objObject.Revision = odrPCS[ITM_ProductTable.REVISION_FLD].ToString().Trim();
					objObject.Description = odrPCS[ITM_ProductTable.DESCRIPTION_FLD].ToString().Trim();
					objObject.SetupDate = DateTime.Parse(odrPCS[ITM_ProductTable.SETUPDATE_FLD].ToString().Trim());
					if (odrPCS[ITM_ProductTable.VAT_FLD] != DBNull.Value)
					{
						objObject.VAT = float.Parse(odrPCS[ITM_ProductTable.VAT_FLD].ToString().Trim());
					}
					else
					{
						objObject.VAT = NUMBER_EMPTY_VALUE;
					}
					if (odrPCS[ITM_ProductTable.IMPORTTAX_FLD] != DBNull.Value)
					{
						objObject.ImportTax = float.Parse(odrPCS[ITM_ProductTable.IMPORTTAX_FLD].ToString().Trim());
					}
					else
					{
						objObject.ImportTax = NUMBER_EMPTY_VALUE;
					}
					if (odrPCS[ITM_ProductTable.EXPORTTAX_FLD] != DBNull.Value)
					{
						objObject.ExportTax = float.Parse(odrPCS[ITM_ProductTable.EXPORTTAX_FLD].ToString().Trim());
					}
					else
					{
						objObject.ExportTax = NUMBER_EMPTY_VALUE;
					}
					if (odrPCS[ITM_ProductTable.SPECIALTAX_FLD] != DBNull.Value)
					{
						objObject.SpecialTax = float.Parse(odrPCS[ITM_ProductTable.SPECIALTAX_FLD].ToString().Trim());
					}
					else
					{
						objObject.SpecialTax = NUMBER_EMPTY_VALUE;
					}

					if (odrPCS[ITM_ProductTable.MAKEITEM_FLD] != DBNull.Value)
					{
						objObject.MakeItem = bool.Parse(odrPCS[ITM_ProductTable.MAKEITEM_FLD].ToString().Trim());
					}
					objObject.PartNumber = odrPCS[ITM_ProductTable.PARTNUMBER_FLD].ToString().Trim();
					objObject.OtherInfo1 = odrPCS[ITM_ProductTable.OTHERINFO1_FLD].ToString().Trim();
					objObject.OtherInfo2 = odrPCS[ITM_ProductTable.OTHERINFO2_FLD].ToString().Trim();

					if (odrPCS[ITM_ProductTable.LENGTH_FLD] != DBNull.Value)
					{
						objObject.Length = Decimal.Parse(odrPCS[ITM_ProductTable.LENGTH_FLD].ToString().Trim());
					}
					else
					{
						objObject.Length = NUMBER_EMPTY_VALUE;
					}
					if (odrPCS[ITM_ProductTable.WIDTH_FLD] != DBNull.Value)
					{
						objObject.Width = Decimal.Parse(odrPCS[ITM_ProductTable.WIDTH_FLD].ToString().Trim());
					}
					else
					{
						objObject.Width = NUMBER_EMPTY_VALUE;
					}
					if (odrPCS[ITM_ProductTable.HEIGHT_FLD] != DBNull.Value)
					{
						objObject.Height = Decimal.Parse(odrPCS[ITM_ProductTable.HEIGHT_FLD].ToString().Trim());
					}
					else
					{
						objObject.Height = NUMBER_EMPTY_VALUE;
					}
					if (odrPCS[ITM_ProductTable.WEIGHT_FLD] != DBNull.Value)
					{
						objObject.Weight = Decimal.Parse(odrPCS[ITM_ProductTable.WEIGHT_FLD].ToString().Trim());
					}
					else
					{
						objObject.Weight = NUMBER_EMPTY_VALUE;
					}
					if (odrPCS[ITM_ProductTable.FINISHEDGOODS_FLD] != DBNull.Value)
					{
						objObject.FinishedGoods = bool.Parse(odrPCS[ITM_ProductTable.FINISHEDGOODS_FLD].ToString().Trim());
					}
					if (odrPCS[ITM_ProductTable.SHELFLIFE_FLD] != DBNull.Value)
					{
						objObject.ShelfLife = Decimal.Parse(odrPCS[ITM_ProductTable.SHELFLIFE_FLD].ToString().Trim());
					}
					else
					{
						objObject.ShelfLife = NUMBER_EMPTY_VALUE;
					}
					if (odrPCS[ITM_ProductTable.LOTCONTROL_FLD] != DBNull.Value)
					{
						objObject.LotControl = bool.Parse(odrPCS[ITM_ProductTable.LOTCONTROL_FLD].ToString().Trim());
					}
					if (odrPCS[ITM_ProductTable.QASTATUS_FLD] != DBNull.Value && odrPCS[ITM_ProductTable.QASTATUS_FLD].ToString().Trim() != String.Empty)
					{
						objObject.QAStatus = int.Parse(odrPCS[ITM_ProductTable.QASTATUS_FLD].ToString().Trim());
					}
					if (odrPCS[ITM_ProductTable.STOCK_FLD] != DBNull.Value)
					{
						objObject.Stock = bool.Parse(odrPCS[ITM_ProductTable.STOCK_FLD].ToString().Trim());
					}

					if (odrPCS[ITM_ProductTable.PLANTYPE_FLD] != DBNull.Value && odrPCS[ITM_ProductTable.PLANTYPE_FLD].ToString().Trim() != String.Empty)
					{
						objObject.PlanType = int.Parse(odrPCS[ITM_ProductTable.PLANTYPE_FLD].ToString().Trim());
					}

					if (odrPCS[ITM_ProductTable.AUTOCONVERSION_FLD] != DBNull.Value)
					{
						objObject.AutoConversion = bool.Parse(odrPCS[ITM_ProductTable.AUTOCONVERSION_FLD].ToString().Trim());
					}
					if (odrPCS[ITM_ProductTable.ORDERQUANTITY_FLD] != DBNull.Value)
					{
						objObject.OrderQuantity = Decimal.Parse(odrPCS[ITM_ProductTable.ORDERQUANTITY_FLD].ToString().Trim());
					}
					else
					{
						objObject.OrderQuantity = (decimal) NUMBER_EMPTY_VALUE;
					}
					if (odrPCS[ITM_ProductTable.LTREQUISITION_FLD] != DBNull.Value)
					{
						objObject.LTRequisition = Decimal.Parse(odrPCS[ITM_ProductTable.LTREQUISITION_FLD].ToString().Trim());
					}
					else
					{
						objObject.LTRequisition = (decimal) NUMBER_EMPTY_VALUE;
					}
					if (odrPCS[ITM_ProductTable.LTSAFETYSTOCK_FLD] != DBNull.Value)
					{
						objObject.LTSafetyStock = Decimal.Parse(odrPCS[ITM_ProductTable.LTSAFETYSTOCK_FLD].ToString().Trim());
					}
					else
					{
						objObject.LTSafetyStock = (decimal) NUMBER_EMPTY_VALUE;
					}
					if (odrPCS[ITM_ProductTable.ORDERQUANTITYMULTIPLE_FLD] != DBNull.Value)
					{
						objObject.OrderQuantityMultiple = Decimal.Parse(odrPCS[ITM_ProductTable.ORDERQUANTITYMULTIPLE_FLD].ToString().Trim());
					}
					else
					{
						objObject.OrderQuantityMultiple = (decimal) NUMBER_EMPTY_VALUE;
					}
					if (odrPCS[ITM_ProductTable.SCRAPPERCENT_FLD] != DBNull.Value)
					{
						objObject.ScrapPercent = Decimal.Parse(odrPCS[ITM_ProductTable.SCRAPPERCENT_FLD].ToString().Trim());
					}
					else
					{
						objObject.ScrapPercent = (decimal) NUMBER_EMPTY_VALUE;
					}
					if (odrPCS[ITM_ProductTable.MINIMUMSTOCK_FLD] != DBNull.Value)
					{
						objObject.MinimumStock = Decimal.Parse(odrPCS[ITM_ProductTable.MINIMUMSTOCK_FLD].ToString().Trim());
					}
					else
					{
						objObject.MinimumStock = (decimal) NUMBER_EMPTY_VALUE;
					}
					if (odrPCS[ITM_ProductTable.MAXIMUMSTOCK_FLD] != DBNull.Value)
					{
						objObject.MaximumStock = Decimal.Parse(odrPCS[ITM_ProductTable.MAXIMUMSTOCK_FLD].ToString().Trim());
					}
					else
					{
						objObject.MaximumStock = (decimal) NUMBER_EMPTY_VALUE;
					}
					if (odrPCS[ITM_ProductTable.CONVERSIONTOLERANCE_FLD] != DBNull.Value)
					{
						objObject.ConversionTolerance = Decimal.Parse(odrPCS[ITM_ProductTable.CONVERSIONTOLERANCE_FLD].ToString().Trim());
					}
					else
					{
						objObject.ConversionTolerance = (decimal) NUMBER_EMPTY_VALUE;
					}
					if (odrPCS[ITM_ProductTable.VOUCHERTOLERANCE_FLD] != DBNull.Value)
					{
						objObject.VoucherTolerance = Decimal.Parse(odrPCS[ITM_ProductTable.VOUCHERTOLERANCE_FLD].ToString().Trim());
					}
					else
					{
						objObject.VoucherTolerance = (decimal) NUMBER_EMPTY_VALUE;
					}
					if (odrPCS[ITM_ProductTable.RECEIVETOLERANCE_FLD] != DBNull.Value)
					{
						objObject.ReceiveTolerance = Decimal.Parse(odrPCS[ITM_ProductTable.RECEIVETOLERANCE_FLD].ToString().Trim());
					}
					else
					{
						objObject.ReceiveTolerance = (decimal) NUMBER_EMPTY_VALUE;
					}
					if (odrPCS[ITM_ProductTable.ISSUESIZE_FLD] != DBNull.Value)
					{
						objObject.IssueSize = Decimal.Parse(odrPCS[ITM_ProductTable.ISSUESIZE_FLD].ToString().Trim());
					}
					else
					{
						objObject.IssueSize = (decimal) NUMBER_EMPTY_VALUE;
					}
					if (odrPCS[ITM_ProductTable.LTFIXEDTIME_FLD] != DBNull.Value)
					{
						objObject.LTFixedTime = Decimal.Parse(odrPCS[ITM_ProductTable.LTFIXEDTIME_FLD].ToString().Trim());
					}
					else
					{
						objObject.LTFixedTime = (decimal) NUMBER_EMPTY_VALUE;
					}
					if (odrPCS[ITM_ProductTable.LTVARIABLETIME_FLD] != DBNull.Value)
					{
						objObject.LTVariableTime = Decimal.Parse(odrPCS[ITM_ProductTable.LTVARIABLETIME_FLD].ToString().Trim());
					}
					else
					{
						objObject.LTVariableTime = (decimal) NUMBER_EMPTY_VALUE;
					}
					if (odrPCS[ITM_ProductTable.LTDOCKTOSTOCK_FLD] != DBNull.Value)
					{
						objObject.LTDocToStock = Decimal.Parse(odrPCS[ITM_ProductTable.LTDOCKTOSTOCK_FLD].ToString().Trim());
					}
					else
					{
						objObject.LTDocToStock = (decimal) NUMBER_EMPTY_VALUE;
					}
					if (odrPCS[ITM_ProductTable.LTORDERPREPARE_FLD] != DBNull.Value)
					{
						objObject.LTOrderPrepare = Decimal.Parse(odrPCS[ITM_ProductTable.LTORDERPREPARE_FLD].ToString().Trim());
					}
					else
					{
						objObject.LTOrderPrepare = (decimal) NUMBER_EMPTY_VALUE;
					}
					if (odrPCS[ITM_ProductTable.LTSHIPPINGPREPARE_FLD] != DBNull.Value)
					{
						objObject.LTShippingPrepare = Decimal.Parse(odrPCS[ITM_ProductTable.LTSHIPPINGPREPARE_FLD].ToString().Trim());
					}
					else
					{
						objObject.LTShippingPrepare = (decimal) NUMBER_EMPTY_VALUE;
					}
					if (odrPCS[ITM_ProductTable.LTSALESATP_FLD] != DBNull.Value)
					{
						objObject.LTSalesATP = Decimal.Parse(odrPCS[ITM_ProductTable.LTSALESATP_FLD].ToString().Trim());
					}
					else
					{
						objObject.LTSalesATP = (decimal) NUMBER_EMPTY_VALUE;
					}

					if (odrPCS[ITM_ProductTable.SHIPTOLERANCEID_FLD] != DBNull.Value && odrPCS[ITM_ProductTable.SHIPTOLERANCEID_FLD].ToString().Trim() != String.Empty)
					{
						objObject.ShipToleranceID = int.Parse(odrPCS[ITM_ProductTable.SHIPTOLERANCEID_FLD].ToString().Trim());
					}

					if (odrPCS[ITM_ProductTable.BUYERID_FLD] != DBNull.Value && odrPCS[ITM_ProductTable.BUYERID_FLD].ToString().Trim() != String.Empty)
					{
						objObject.BuyerID = int.Parse(odrPCS[ITM_ProductTable.BUYERID_FLD].ToString().Trim());
					}

					objObject.BOMDescription = odrPCS[ITM_ProductTable.BOMDESCRIPTION_FLD].ToString().Trim();
					if (odrPCS[ITM_ProductTable.BOMINCREMENT_FLD] != DBNull.Value && odrPCS[ITM_ProductTable.BOMINCREMENT_FLD].ToString().Trim() != String.Empty)
					{
						objObject.BomIncrement = int.Parse(odrPCS[ITM_ProductTable.BOMINCREMENT_FLD].ToString().Trim());
					}

					objObject.RoutingDescription = odrPCS[ITM_ProductTable.ROUTINGDESCRIPTION_FLD].ToString().Trim();
					if (odrPCS[ITM_ProductTable.CREATEDATETIME_FLD] != DBNull.Value && odrPCS[ITM_ProductTable.CREATEDATETIME_FLD].ToString().Trim() != String.Empty)
					{
						objObject.CreateDateTime = DateTime.Parse(odrPCS[ITM_ProductTable.CREATEDATETIME_FLD].ToString().Trim());
					}

					if (odrPCS[ITM_ProductTable.UPDATEDATETIME_FLD] != DBNull.Value && odrPCS[ITM_ProductTable.UPDATEDATETIME_FLD].ToString().Trim() != String.Empty)
					{
						objObject.UpdateDateTime = DateTime.Parse(odrPCS[ITM_ProductTable.UPDATEDATETIME_FLD].ToString().Trim());
					}

					if (odrPCS[ITM_ProductTable.COSTMETHOD_FLD] != DBNull.Value && odrPCS[ITM_ProductTable.COSTMETHOD_FLD].ToString().Trim() != String.Empty)
					{
						objObject.CostMethod = int.Parse(odrPCS[ITM_ProductTable.COSTMETHOD_FLD].ToString().Trim());
					}

					if (odrPCS[ITM_ProductTable.ROUTINGINCREMENT_FLD] != DBNull.Value && odrPCS[ITM_ProductTable.ROUTINGINCREMENT_FLD].ToString().Trim() != String.Empty)
					{
						objObject.RoutingIncrement = int.Parse(odrPCS[ITM_ProductTable.ROUTINGINCREMENT_FLD].ToString().Trim());
					}

					if (odrPCS[ITM_ProductTable.CCNID_FLD] != DBNull.Value && odrPCS[ITM_ProductTable.CCNID_FLD].ToString().Trim() != String.Empty)
					{
						objObject.CCNID = int.Parse(odrPCS[ITM_ProductTable.CCNID_FLD].ToString().Trim());
					}

					if (odrPCS[ITM_ProductTable.CATEGORYID_FLD] != DBNull.Value && odrPCS[ITM_ProductTable.CATEGORYID_FLD].ToString().Trim() != String.Empty)
					{
						objObject.CategoryID = int.Parse(odrPCS[ITM_ProductTable.CATEGORYID_FLD].ToString().Trim());
					}

					if (odrPCS[ITM_ProductTable.COSTCENTERID_FLD] != DBNull.Value && odrPCS[ITM_ProductTable.COSTCENTERID_FLD].ToString().Trim() != String.Empty)
					{
						objObject.CostCenterID = int.Parse(odrPCS[ITM_ProductTable.COSTCENTERID_FLD].ToString().Trim());
					}

					if (odrPCS[ITM_ProductTable.DELETEREASONID_FLD] != DBNull.Value && odrPCS[ITM_ProductTable.DELETEREASONID_FLD].ToString().Trim() != String.Empty)
					{
						objObject.DeleteReasonID = int.Parse(odrPCS[ITM_ProductTable.DELETEREASONID_FLD].ToString().Trim());
					}

					if (odrPCS[ITM_ProductTable.DELIVERYPOLICYID_FLD] != DBNull.Value && odrPCS[ITM_ProductTable.DELIVERYPOLICYID_FLD].ToString().Trim() != String.Empty)
					{
						objObject.DeliveryPolicyID = int.Parse(odrPCS[ITM_ProductTable.DELIVERYPOLICYID_FLD].ToString().Trim());
					}

					if (odrPCS[ITM_ProductTable.FORMATCODEID_FLD] != DBNull.Value && odrPCS[ITM_ProductTable.FORMATCODEID_FLD].ToString().Trim() != String.Empty)
					{
						objObject.FormatCodeID = int.Parse(odrPCS[ITM_ProductTable.FORMATCODEID_FLD].ToString().Trim());
					}

					if (odrPCS[ITM_ProductTable.FREIGHTCLASSID_FLD] != DBNull.Value && odrPCS[ITM_ProductTable.FREIGHTCLASSID_FLD].ToString().Trim() != String.Empty)
					{
						objObject.FreightClassID = int.Parse(odrPCS[ITM_ProductTable.FREIGHTCLASSID_FLD].ToString().Trim());
					}

					if (odrPCS[ITM_ProductTable.HAZARDID_FLD] != DBNull.Value && odrPCS[ITM_ProductTable.HAZARDID_FLD].ToString().Trim() != String.Empty)
					{
						objObject.HazardID = int.Parse(odrPCS[ITM_ProductTable.HAZARDID_FLD].ToString().Trim());
					}

					if (odrPCS[ITM_ProductTable.ORDERPOLICYID_FLD] != DBNull.Value && odrPCS[ITM_ProductTable.ORDERPOLICYID_FLD].ToString().Trim() != String.Empty)
					{
						objObject.OrderPolicyID = int.Parse(odrPCS[ITM_ProductTable.ORDERPOLICYID_FLD].ToString().Trim());
					}

					if (odrPCS[ITM_ProductTable.ORDERRULEID_FLD] != DBNull.Value && odrPCS[ITM_ProductTable.ORDERRULEID_FLD].ToString().Trim() != String.Empty)
					{
						objObject.OrderRuleID = int.Parse(odrPCS[ITM_ProductTable.ORDERRULEID_FLD].ToString().Trim());
					}

					if (odrPCS[ITM_ProductTable.SOURCEID_FLD] != DBNull.Value && odrPCS[ITM_ProductTable.SOURCEID_FLD].ToString().Trim() != String.Empty)
					{
						objObject.SourceID = int.Parse(odrPCS[ITM_ProductTable.SOURCEID_FLD].ToString().Trim());
					}

					if (odrPCS[ITM_ProductTable.STOCKUMID_FLD] != DBNull.Value && odrPCS[ITM_ProductTable.STOCKUMID_FLD].ToString().Trim() != String.Empty)
					{
						objObject.StockUMID = int.Parse(odrPCS[ITM_ProductTable.STOCKUMID_FLD].ToString().Trim());
					}

					if (odrPCS[ITM_ProductTable.SELLINGUMID_FLD] != DBNull.Value && odrPCS[ITM_ProductTable.SELLINGUMID_FLD].ToString().Trim() != String.Empty)
					{
						objObject.SellingUMID = int.Parse(odrPCS[ITM_ProductTable.SELLINGUMID_FLD].ToString().Trim());
					}

					if (odrPCS[ITM_ProductTable.HEIGHTUMID_FLD] != DBNull.Value && odrPCS[ITM_ProductTable.HEIGHTUMID_FLD].ToString().Trim() != String.Empty)
					{
						objObject.HeightUMID = int.Parse(odrPCS[ITM_ProductTable.HEIGHTUMID_FLD].ToString().Trim());
					}

					if (odrPCS[ITM_ProductTable.WIDTHUMID_FLD] != DBNull.Value && odrPCS[ITM_ProductTable.WIDTHUMID_FLD].ToString().Trim() != String.Empty)
					{
						objObject.WidthUMID = int.Parse(odrPCS[ITM_ProductTable.WIDTHUMID_FLD].ToString().Trim());
					}

					if (odrPCS[ITM_ProductTable.LENGTHUMID_FLD] != DBNull.Value && odrPCS[ITM_ProductTable.LENGTHUMID_FLD].ToString().Trim() != String.Empty)
					{
						objObject.LengthUMID = int.Parse(odrPCS[ITM_ProductTable.LENGTHUMID_FLD].ToString().Trim());
					}

					if (odrPCS[ITM_ProductTable.BUYINGUMID_FLD] != DBNull.Value && odrPCS[ITM_ProductTable.BUYINGUMID_FLD].ToString().Trim() != String.Empty)
					{
						objObject.BuyingUMID = int.Parse(odrPCS[ITM_ProductTable.BUYINGUMID_FLD].ToString().Trim());
					}

					if (odrPCS[ITM_ProductTable.WEIGHTUMID_FLD] != DBNull.Value && odrPCS[ITM_ProductTable.WEIGHTUMID_FLD].ToString().Trim() != String.Empty)
					{
						objObject.WeightUMID = int.Parse(odrPCS[ITM_ProductTable.WEIGHTUMID_FLD].ToString().Trim());
					}

					if (odrPCS[ITM_ProductTable.LOTSIZE_FLD] != DBNull.Value && odrPCS[ITM_ProductTable.LOTSIZE_FLD].ToString().Trim() != String.Empty)
					{
						objObject.LotSize = int.Parse(odrPCS[ITM_ProductTable.LOTSIZE_FLD].ToString().Trim());
					}
					else
					{
						objObject.LotSize = NUMBER_EMPTY_VALUE;
					}

					if (odrPCS[ITM_ProductTable.MASTERLOCATIONID_FLD] != DBNull.Value && odrPCS[ITM_ProductTable.MASTERLOCATIONID_FLD].ToString().Trim() != String.Empty)
					{
						objObject.MasterLocationID = int.Parse(odrPCS[ITM_ProductTable.MASTERLOCATIONID_FLD].ToString().Trim());
					}

					if (odrPCS[ITM_ProductTable.LOCATIONID_FLD] != DBNull.Value && odrPCS[ITM_ProductTable.LOCATIONID_FLD].ToString().Trim() != String.Empty)
					{
						objObject.LocationID = int.Parse(odrPCS[ITM_ProductTable.LOCATIONID_FLD].ToString().Trim());
					}

					if (odrPCS[ITM_ProductTable.BINID_FLD] != DBNull.Value && odrPCS[ITM_ProductTable.BINID_FLD].ToString().Trim() != String.Empty)
					{
						objObject.BinID = int.Parse(odrPCS[ITM_ProductTable.BINID_FLD].ToString().Trim());
					}

					if (odrPCS[ITM_ProductTable.PRIMARYVENDORID_FLD] != DBNull.Value && odrPCS[ITM_ProductTable.PRIMARYVENDORID_FLD].ToString().Trim() != String.Empty)
					{
						objObject.PrimaryVendorID = int.Parse(odrPCS[ITM_ProductTable.PRIMARYVENDORID_FLD].ToString().Trim());
					}

					if (odrPCS[ITM_ProductTable.VENDORLOCATIONID_FLD] != DBNull.Value && odrPCS[ITM_ProductTable.VENDORLOCATIONID_FLD].ToString().Trim() != String.Empty)
					{
						objObject.VendorLocationID = int.Parse(odrPCS[ITM_ProductTable.VENDORLOCATIONID_FLD].ToString().Trim());
					}

					if (odrPCS[ITM_ProductTable.SAFETYSTOCK_FLD] != DBNull.Value && odrPCS[ITM_ProductTable.SAFETYSTOCK_FLD].ToString().Trim() != String.Empty)
					{
						objObject.SafetyStock = Decimal.Parse(odrPCS[ITM_ProductTable.SAFETYSTOCK_FLD].ToString().Trim());
					}
					else
					{
						objObject.SafetyStock = (decimal) NUMBER_EMPTY_VALUE;
					}

					if (odrPCS[ITM_ProductTable.AGCID_FLD] != DBNull.Value && odrPCS[ITM_ProductTable.AGCID_FLD].ToString().Trim() != String.Empty)
					{
						objObject.AGCID = int.Parse(odrPCS[ITM_ProductTable.AGCID_FLD].ToString().Trim());
					}

					//Begin_ Added by Tuan TQ 2005-09-22
					objObject.RegisteredCode = odrPCS[ITM_ProductTable.REGISTEREDCODE_FLD].ToString().Trim();
					objObject.PartNameVN = odrPCS[ITM_ProductTable.PARTNAMEVN_FLD].ToString().Trim();
					objObject.TaxCode = odrPCS[ITM_ProductTable.TAXCODE_FLD].ToString().Trim();
					objObject.SetUpPair = odrPCS[ITM_ProductTable.SETUPPAIR_FLD].ToString().Trim();

					if (odrPCS[ITM_ProductTable.LICENSEFEE_FLD] != DBNull.Value)
					{
						objObject.LicenseFee = decimal.Parse(odrPCS[ITM_ProductTable.LICENSEFEE_FLD].ToString().Trim());
					}
					else
					{
						objObject.LicenseFee = (decimal) NUMBER_EMPTY_VALUE;
					}

					if (odrPCS[ITM_ProductTable.INVENTORID_FLD] != DBNull.Value)
						objObject.Inventor = int.Parse(odrPCS[ITM_ProductTable.INVENTORID_FLD].ToString().Trim());

					if (odrPCS[ITM_ProductTable.PRODUCTTYPEID_FLD] != DBNull.Value)
						objObject.ProductTypeId = int.Parse(odrPCS[ITM_ProductTable.PRODUCTTYPEID_FLD].ToString().Trim());

					if (odrPCS[ITM_ProductTable.LISTPRICE_FLD] != DBNull.Value)
					{
						objObject.ListPrice = decimal.Parse(odrPCS[ITM_ProductTable.LISTPRICE_FLD].ToString().Trim());
					}
					else
					{
						objObject.ListPrice = (decimal) NUMBER_EMPTY_VALUE;
					}

					if (odrPCS[ITM_ProductTable.QUANTITYSET_FLD] != DBNull.Value)
					{
						objObject.QuantitySet = decimal.Parse(odrPCS[ITM_ProductTable.QUANTITYSET_FLD].ToString().Trim());
					}
					else
					{
						objObject.QuantitySet = (decimal) NUMBER_EMPTY_VALUE;
					}

					if (odrPCS[ITM_ProductTable.VENDORCURRENCYID_FLD] != DBNull.Value)
						objObject.VendorCurrencyID = int.Parse(odrPCS[ITM_ProductTable.VENDORCURRENCYID_FLD].ToString().Trim());

					//End_ Added by Tuan TQ 2005-09-22

					if (odrPCS[ITM_ProductTable.MINPRODUCE_FLD] != DBNull.Value)
					{
						objObject.MinProduce = decimal.Parse(odrPCS[ITM_ProductTable.MINPRODUCE_FLD].ToString().Trim());
					}
					else
					{
						objObject.MinProduce = (decimal) NUMBER_EMPTY_VALUE;
					}

					if (odrPCS[ITM_ProductTable.MAXPRODUCE_FLD] != DBNull.Value)
					{
						objObject.MaxProduce = decimal.Parse(odrPCS[ITM_ProductTable.MAXPRODUCE_FLD].ToString().Trim());
					}
					else
					{
						objObject.MaxProduce = (decimal) NUMBER_EMPTY_VALUE;
					}

					//HACK: added by Tuan TQ. 17 May, 2006
					if (odrPCS[ITM_ProductTable.MAXROUNDUPTOMIN_FLD] != DBNull.Value)
					{
						objObject.MaxRoundUpToMin = Decimal.Parse(odrPCS[ITM_ProductTable.MAXROUNDUPTOMIN_FLD].ToString().Trim());
					}
					else
					{
						objObject.MaxRoundUpToMin = (decimal) NUMBER_EMPTY_VALUE;
					}

					if (odrPCS[ITM_ProductTable.MAXROUNDUPTOMULTIPLE_FLD] != DBNull.Value)
					{
						objObject.MaxRoundUpToMultiple = Decimal.Parse(odrPCS[ITM_ProductTable.MAXROUNDUPTOMULTIPLE_FLD].ToString().Trim());
					}
					else
					{
						objObject.MaxRoundUpToMultiple = (decimal) NUMBER_EMPTY_VALUE;
					}

					if (odrPCS[ITM_ProductTable.ACADJUSTMENTMASTERID_FLD] != DBNull.Value)
					{
						objObject.ACAdjustmentMasterID = int.Parse(odrPCS[ITM_ProductTable.ACADJUSTMENTMASTERID_FLD].ToString().Trim());
					}
					else
					{
						objObject.ACAdjustmentMasterID = NUMBER_EMPTY_VALUE;
					}
					//end hack

					if (odrPCS[ITM_ProductTable.ORDERPOINT_FLD] != DBNull.Value)
					{
						objObject.OrderPoint = Decimal.Parse(odrPCS[ITM_ProductTable.ORDERPOINT_FLD].ToString().Trim());
					}
					else
					{
						objObject.OrderPoint = (decimal) NUMBER_EMPTY_VALUE;
					}
					try
					{
						// convert byte array to bitmap
						byte[] content = (byte[]) odrPCS[ITM_ProductTable.PICTURE_FLD];
						MemoryStream stream = new MemoryStream(content);
						objObject.Picture = new Bitmap(stream);
					}
					catch{}
					try
					{
						objObject.AVEG = Convert.ToBoolean(odrPCS[ITM_ProductTable.AVEG_FLD]);
					}
					catch{}
					try
					{
						objObject.MassOrder = Convert.ToBoolean(odrPCS[ITM_ProductTable.MASSORDER_FLD]);
					}
					catch{}                   
					try
					{
						objObject.AllowNegativeQty = Convert.ToBoolean(odrPCS[ITM_ProductTable.ALLOWNEGATIVEQTY_FLD]);
					}
                    catch { objObject.AllowNegativeQty = false; }
					objObject.StockTakingCode = odrPCS[ITM_ProductTable.STOCKTAKINGCODE_FLD].ToString().Trim();
				}
				return objObject;
			}
			catch (OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}

			catch (Exception ex)
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}

			finally
			{
				if (oconPCS != null)
				{
					if (oconPCS.State != ConnectionState.Closed)
					{
						oconPCS.Close();
					}
				}
			}
		}


		public object GetObjectVO(string pstrCode)
		{
			const string METHOD_NAME = THIS + ".GetObjectVO()";
			DataSet dstPCS = new DataSet();

			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				strSql = "SELECT "
					+ ITM_ProductTable.PRODUCTID_FLD + ","
					+ ITM_ProductTable.CODE_FLD
					+ " FROM " + ITM_ProductTable.TABLE_NAME
					+ " WHERE " + ITM_ProductTable.CODE_FLD + "='" + pstrCode + "'";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				ITM_ProductVO objObject = new ITM_ProductVO();

				while (odrPCS.Read())
				{
					objObject.ProductID = int.Parse(odrPCS[ITM_ProductTable.PRODUCTID_FLD].ToString().Trim());
					objObject.Code = odrPCS[ITM_ProductTable.CODE_FLD].ToString().Trim();

				}
				return objObject;
			}
			catch (OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}

			catch (Exception ex)
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}

			finally
			{
				if (oconPCS != null)
				{
					if (oconPCS.State != ConnectionState.Closed)
					{
						oconPCS.Close();
					}
				}
			}
		}

		public object GetObjectVO(string pstrCode, string pstrDescription, string pstrRevision)
		{
			const string METHOD_NAME = THIS + ".GetObjectVO()";
			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				strSql = "SELECT "
					+ ITM_ProductTable.PRODUCTID_FLD + ","
					+ ITM_ProductTable.CODE_FLD + ","
					+ ITM_ProductTable.REVISION_FLD + ","
					+ ITM_ProductTable.DESCRIPTION_FLD + ","
					+ ITM_ProductTable.SETUPDATE_FLD + ","
					+ ITM_ProductTable.VAT_FLD + ","
					+ ITM_ProductTable.IMPORTTAX_FLD + ","
					+ ITM_ProductTable.EXPORTTAX_FLD + ","
					+ ITM_ProductTable.SPECIALTAX_FLD + ","
					+ ITM_ProductTable.MAKEITEM_FLD + ","
					+ ITM_ProductTable.PARTNUMBER_FLD + ","
					+ ITM_ProductTable.OTHERINFO1_FLD + ","
					+ ITM_ProductTable.OTHERINFO2_FLD + ","
					+ ITM_ProductTable.LENGTH_FLD + ","
					+ ITM_ProductTable.WIDTH_FLD + ","
					+ ITM_ProductTable.HEIGHT_FLD + ","
					+ ITM_ProductTable.WEIGHT_FLD + ","
					+ ITM_ProductTable.FINISHEDGOODS_FLD + ","
					+ ITM_ProductTable.SHELFLIFE_FLD + ","
					+ ITM_ProductTable.LOTCONTROL_FLD + ","
					+ ITM_ProductTable.QASTATUS_FLD + ","
					+ ITM_ProductTable.STOCK_FLD + ","
					+ ITM_ProductTable.PLANTYPE_FLD + ","
					+ ITM_ProductTable.AUTOCONVERSION_FLD + ","
					+ ITM_ProductTable.ORDERQUANTITY_FLD + ","
					+ ITM_ProductTable.LTREQUISITION_FLD + ","
					+ ITM_ProductTable.LTSAFETYSTOCK_FLD + ","
					+ ITM_ProductTable.ORDERQUANTITYMULTIPLE_FLD + ","
					+ ITM_ProductTable.SCRAPPERCENT_FLD + ","
					+ ITM_ProductTable.MINIMUMSTOCK_FLD + ","
					+ ITM_ProductTable.MAXIMUMSTOCK_FLD + ","
					+ ITM_ProductTable.CONVERSIONTOLERANCE_FLD + ","
					+ ITM_ProductTable.VOUCHERTOLERANCE_FLD + ","
					+ ITM_ProductTable.RECEIVETOLERANCE_FLD + ","
					+ ITM_ProductTable.ISSUESIZE_FLD + ","
					+ ITM_ProductTable.LTFIXEDTIME_FLD + ","
					+ ITM_ProductTable.LTVARIABLETIME_FLD + ","
					+ ITM_ProductTable.LTDOCKTOSTOCK_FLD + ","
					+ ITM_ProductTable.LTORDERPREPARE_FLD + ","
					+ ITM_ProductTable.LTSHIPPINGPREPARE_FLD + ","
					+ ITM_ProductTable.LTSALESATP_FLD + ","
					+ ITM_ProductTable.SHIPTOLERANCEID_FLD + ","
					+ ITM_ProductTable.BUYERID_FLD + ","
					+ ITM_ProductTable.BOMDESCRIPTION_FLD + ","
					+ ITM_ProductTable.BOMINCREMENT_FLD + ","
					+ ITM_ProductTable.ROUTINGDESCRIPTION_FLD + ","
					+ ITM_ProductTable.CREATEDATETIME_FLD + ","
					+ ITM_ProductTable.UPDATEDATETIME_FLD + ","
					+ ITM_ProductTable.COSTMETHOD_FLD + ","
					+ ITM_ProductTable.ROUTINGINCREMENT_FLD + ","
					+ ITM_ProductTable.CCNID_FLD + ","
					+ ITM_ProductTable.CATEGORYID_FLD + ","
					+ ITM_ProductTable.COSTCENTERID_FLD + ","
					+ ITM_ProductTable.DELETEREASONID_FLD + ","
					+ ITM_ProductTable.DELIVERYPOLICYID_FLD + ","
					+ ITM_ProductTable.FORMATCODEID_FLD + ","
					+ ITM_ProductTable.FREIGHTCLASSID_FLD + ","
					+ ITM_ProductTable.HAZARDID_FLD + ","
					+ ITM_ProductTable.ORDERPOLICYID_FLD + ","
					+ ITM_ProductTable.ORDERRULEID_FLD + ","
					+ ITM_ProductTable.SOURCEID_FLD + ","
					+ ITM_ProductTable.STOCKUMID_FLD + ","
					+ ITM_ProductTable.SELLINGUMID_FLD + ","
					+ ITM_ProductTable.HEIGHTUMID_FLD + ","
					+ ITM_ProductTable.WIDTHUMID_FLD + ","
					+ ITM_ProductTable.LENGTHUMID_FLD + ","
					+ ITM_ProductTable.BUYINGUMID_FLD + ","
					+ ITM_ProductTable.WEIGHTUMID_FLD + ","
					+ ITM_ProductTable.LOTSIZE_FLD + ","
					+ ITM_ProductTable.MASTERLOCATIONID_FLD + ","
					+ ITM_ProductTable.LOCATIONID_FLD + ","
					+ ITM_ProductTable.BINID_FLD + ","
					+ ITM_ProductTable.PRIMARYVENDORID_FLD + ","
					+ ITM_ProductTable.VENDORLOCATIONID_FLD + ","
					+ ITM_ProductTable.SAFETYSTOCK_FLD + ","
					+ ITM_ProductTable.AGCID_FLD + ","
					+ ITM_ProductTable.PARTNAMEVN_FLD + ","
					+ ITM_ProductTable.INVENTORID_FLD + ","
					+ ITM_ProductTable.LICENSEFEE_FLD + ","
					+ ITM_ProductTable.LISTPRICE_FLD + ","
					+ ITM_ProductTable.VENDORCURRENCYID_FLD + ","
					+ ITM_ProductTable.QUANTITYSET_FLD + ","
					+ ITM_ProductTable.TAXCODE_FLD + ","
					+ ITM_ProductTable.MINPRODUCE_FLD + ","
					+ ITM_ProductTable.MAXPRODUCE_FLD + ","
					+ ITM_ProductTable.PRODUCTTYPEID_FLD + ", "
					// added: dungla 15-02-2005
					+ ITM_ProductTable.PRODUCTGROUPID_FLD + ", "
					+ ITM_ProductTable.PRODUCTIONLINEID_FLD + ", "
					+ ITM_ProductTable.COSTCENTERRATEMASTERID_FLD + ", "
					// end added: dungla 15-02-2005
					+ ITM_ProductTable.MAXROUNDUPTOMIN_FLD + ", "
					+ ITM_ProductTable.MAXROUNDUPTOMULTIPLE_FLD + ", "
					+ ITM_ProductTable.ACADJUSTMENTMASTERID_FLD + ", "
					+ ITM_ProductTable.REGISTEREDCODE_FLD + ", "
					+ ITM_ProductTable.ORDERPOINT_FLD
					+ " FROM " + ITM_ProductTable.TABLE_NAME
					+ " WHERE " + ITM_ProductTable.CODE_FLD + " = ?"
					+ " AND " + ITM_ProductTable.DESCRIPTION_FLD + " = ?"
					+ " AND " + ITM_ProductTable.REVISION_FLD + " = ?";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Parameters.Add(ITM_ProductTable.CODE_FLD, pstrCode);
				ocmdPCS.Parameters.Add(ITM_ProductTable.DESCRIPTION_FLD, pstrDescription);
				ocmdPCS.Parameters.Add(ITM_ProductTable.REVISION_FLD, pstrRevision);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				ITM_ProductVO objObject = new ITM_ProductVO();

				while (odrPCS.Read())
				{
					objObject.ProductID = int.Parse(odrPCS[ITM_ProductTable.PRODUCTID_FLD].ToString().Trim());
					objObject.Code = odrPCS[ITM_ProductTable.CODE_FLD].ToString().Trim();
					objObject.Revision = odrPCS[ITM_ProductTable.REVISION_FLD].ToString().Trim();
					objObject.Description = odrPCS[ITM_ProductTable.DESCRIPTION_FLD].ToString().Trim();
					if (odrPCS[ITM_ProductTable.SETUPDATE_FLD] != DBNull.Value)
						objObject.SetupDate = DateTime.Parse(odrPCS[ITM_ProductTable.SETUPDATE_FLD].ToString().Trim());
					if (odrPCS[ITM_ProductTable.VAT_FLD] != DBNull.Value)
						objObject.VAT = float.Parse(odrPCS[ITM_ProductTable.VAT_FLD].ToString().Trim());
					if (odrPCS[ITM_ProductTable.IMPORTTAX_FLD] != DBNull.Value)
						objObject.ImportTax = float.Parse(odrPCS[ITM_ProductTable.IMPORTTAX_FLD].ToString().Trim());
					if (odrPCS[ITM_ProductTable.EXPORTTAX_FLD] != DBNull.Value)
						objObject.ExportTax = float.Parse(odrPCS[ITM_ProductTable.EXPORTTAX_FLD].ToString().Trim());
					if (odrPCS[ITM_ProductTable.SPECIALTAX_FLD] != DBNull.Value)
						objObject.SpecialTax = float.Parse(odrPCS[ITM_ProductTable.SPECIALTAX_FLD].ToString().Trim());
					if (odrPCS[ITM_ProductTable.MAKEITEM_FLD] != DBNull.Value)
						objObject.MakeItem = bool.Parse(odrPCS[ITM_ProductTable.MAKEITEM_FLD].ToString().Trim());
					objObject.PartNumber = odrPCS[ITM_ProductTable.PARTNUMBER_FLD].ToString().Trim();
					objObject.OtherInfo1 = odrPCS[ITM_ProductTable.OTHERINFO1_FLD].ToString().Trim();
					objObject.OtherInfo2 = odrPCS[ITM_ProductTable.OTHERINFO2_FLD].ToString().Trim();
					if (odrPCS[ITM_ProductTable.LENGTH_FLD] != DBNull.Value)
						objObject.Length = Decimal.Parse(odrPCS[ITM_ProductTable.LENGTH_FLD].ToString().Trim());
					if (odrPCS[ITM_ProductTable.WIDTH_FLD] != DBNull.Value)
						objObject.Width = Decimal.Parse(odrPCS[ITM_ProductTable.WIDTH_FLD].ToString().Trim());
					if (odrPCS[ITM_ProductTable.HEIGHT_FLD] != DBNull.Value)
						objObject.Height = Decimal.Parse(odrPCS[ITM_ProductTable.HEIGHT_FLD].ToString().Trim());
					if (odrPCS[ITM_ProductTable.WEIGHT_FLD] != DBNull.Value)
						objObject.Weight = Decimal.Parse(odrPCS[ITM_ProductTable.WEIGHT_FLD].ToString().Trim());
					if (odrPCS[ITM_ProductTable.FINISHEDGOODS_FLD] != DBNull.Value)
						objObject.FinishedGoods = bool.Parse(odrPCS[ITM_ProductTable.FINISHEDGOODS_FLD].ToString().Trim());
					if (odrPCS[ITM_ProductTable.SHELFLIFE_FLD] != DBNull.Value)
						objObject.ShelfLife = Decimal.Parse(odrPCS[ITM_ProductTable.SHELFLIFE_FLD].ToString().Trim());
					if (odrPCS[ITM_ProductTable.LOTCONTROL_FLD] != DBNull.Value)
						objObject.LotControl = bool.Parse(odrPCS[ITM_ProductTable.LOTCONTROL_FLD].ToString().Trim());
					if (odrPCS[ITM_ProductTable.QASTATUS_FLD] != DBNull.Value)
						objObject.QAStatus = int.Parse(odrPCS[ITM_ProductTable.QASTATUS_FLD].ToString().Trim());
					if (odrPCS[ITM_ProductTable.STOCK_FLD] != DBNull.Value)
						objObject.Stock = bool.Parse(odrPCS[ITM_ProductTable.STOCK_FLD].ToString().Trim());
					if (odrPCS[ITM_ProductTable.PLANTYPE_FLD] != DBNull.Value)
						objObject.PlanType = int.Parse(odrPCS[ITM_ProductTable.PLANTYPE_FLD].ToString().Trim());
					if (odrPCS[ITM_ProductTable.AUTOCONVERSION_FLD] != DBNull.Value)
						objObject.AutoConversion = bool.Parse(odrPCS[ITM_ProductTable.AUTOCONVERSION_FLD].ToString().Trim());
					if (odrPCS[ITM_ProductTable.ORDERQUANTITY_FLD] != DBNull.Value)
						objObject.OrderQuantity = Decimal.Parse(odrPCS[ITM_ProductTable.ORDERQUANTITY_FLD].ToString().Trim());
					if (odrPCS[ITM_ProductTable.LTREQUISITION_FLD] != DBNull.Value)
						objObject.LTRequisition = Decimal.Parse(odrPCS[ITM_ProductTable.LTREQUISITION_FLD].ToString().Trim());
					if (odrPCS[ITM_ProductTable.LTSAFETYSTOCK_FLD] != DBNull.Value)
						objObject.LTSafetyStock = Decimal.Parse(odrPCS[ITM_ProductTable.LTSAFETYSTOCK_FLD].ToString().Trim());
					if (odrPCS[ITM_ProductTable.ORDERQUANTITYMULTIPLE_FLD] != DBNull.Value)
						objObject.OrderQuantityMultiple = Decimal.Parse(odrPCS[ITM_ProductTable.ORDERQUANTITYMULTIPLE_FLD].ToString().Trim());
					if (odrPCS[ITM_ProductTable.SCRAPPERCENT_FLD] != DBNull.Value)
						objObject.ScrapPercent = Decimal.Parse(odrPCS[ITM_ProductTable.SCRAPPERCENT_FLD].ToString().Trim());
					if (odrPCS[ITM_ProductTable.MINIMUMSTOCK_FLD] != DBNull.Value)
						objObject.MinimumStock = Decimal.Parse(odrPCS[ITM_ProductTable.MINIMUMSTOCK_FLD].ToString().Trim());
					if (odrPCS[ITM_ProductTable.MAXIMUMSTOCK_FLD] != DBNull.Value)
						objObject.MaximumStock = Decimal.Parse(odrPCS[ITM_ProductTable.MAXIMUMSTOCK_FLD].ToString().Trim());
					if (odrPCS[ITM_ProductTable.CONVERSIONTOLERANCE_FLD] != DBNull.Value)
						objObject.ConversionTolerance = Decimal.Parse(odrPCS[ITM_ProductTable.CONVERSIONTOLERANCE_FLD].ToString().Trim());
					if (odrPCS[ITM_ProductTable.VOUCHERTOLERANCE_FLD] != DBNull.Value)
						objObject.VoucherTolerance = Decimal.Parse(odrPCS[ITM_ProductTable.VOUCHERTOLERANCE_FLD].ToString().Trim());
					if (odrPCS[ITM_ProductTable.RECEIVETOLERANCE_FLD] != DBNull.Value)
						objObject.ReceiveTolerance = Decimal.Parse(odrPCS[ITM_ProductTable.RECEIVETOLERANCE_FLD].ToString().Trim());
					if (odrPCS[ITM_ProductTable.ISSUESIZE_FLD] != DBNull.Value)
						objObject.IssueSize = Decimal.Parse(odrPCS[ITM_ProductTable.ISSUESIZE_FLD].ToString().Trim());
					if (odrPCS[ITM_ProductTable.LTFIXEDTIME_FLD] != DBNull.Value)
						objObject.LTFixedTime = Decimal.Parse(odrPCS[ITM_ProductTable.LTFIXEDTIME_FLD].ToString().Trim());
					if (odrPCS[ITM_ProductTable.LTVARIABLETIME_FLD] != DBNull.Value)
						objObject.LTVariableTime = Decimal.Parse(odrPCS[ITM_ProductTable.LTVARIABLETIME_FLD].ToString().Trim());
					if (odrPCS[ITM_ProductTable.LTDOCKTOSTOCK_FLD] != DBNull.Value)
						objObject.LTDocToStock = Decimal.Parse(odrPCS[ITM_ProductTable.LTDOCKTOSTOCK_FLD].ToString().Trim());
					if (odrPCS[ITM_ProductTable.LTORDERPREPARE_FLD] != DBNull.Value)
						objObject.LTOrderPrepare = Decimal.Parse(odrPCS[ITM_ProductTable.LTORDERPREPARE_FLD].ToString().Trim());
					if (odrPCS[ITM_ProductTable.LTSHIPPINGPREPARE_FLD] != DBNull.Value)
						objObject.LTShippingPrepare = Decimal.Parse(odrPCS[ITM_ProductTable.LTSHIPPINGPREPARE_FLD].ToString().Trim());
					if (odrPCS[ITM_ProductTable.LTSALESATP_FLD] != DBNull.Value)
						objObject.LTSalesATP = Decimal.Parse(odrPCS[ITM_ProductTable.LTSALESATP_FLD].ToString().Trim());
					if (odrPCS[ITM_ProductTable.SHIPTOLERANCEID_FLD] != DBNull.Value)
						objObject.ShipToleranceID = int.Parse(odrPCS[ITM_ProductTable.SHIPTOLERANCEID_FLD].ToString().Trim());
					if (odrPCS[ITM_ProductTable.BUYERID_FLD] != DBNull.Value)
						objObject.BuyerID = int.Parse(odrPCS[ITM_ProductTable.BUYERID_FLD].ToString().Trim());
					objObject.BOMDescription = odrPCS[ITM_ProductTable.BOMDESCRIPTION_FLD].ToString().Trim();
					if (odrPCS[ITM_ProductTable.BOMINCREMENT_FLD] != DBNull.Value)
						objObject.BomIncrement = int.Parse(odrPCS[ITM_ProductTable.BOMINCREMENT_FLD].ToString().Trim());
					objObject.RoutingDescription = odrPCS[ITM_ProductTable.ROUTINGDESCRIPTION_FLD].ToString().Trim();
					if (odrPCS[ITM_ProductTable.CREATEDATETIME_FLD] != DBNull.Value)
						objObject.CreateDateTime = DateTime.Parse(odrPCS[ITM_ProductTable.CREATEDATETIME_FLD].ToString().Trim());
					if (odrPCS[ITM_ProductTable.UPDATEDATETIME_FLD] != DBNull.Value)
						objObject.UpdateDateTime = DateTime.Parse(odrPCS[ITM_ProductTable.UPDATEDATETIME_FLD].ToString().Trim());
					if (odrPCS[ITM_ProductTable.COSTMETHOD_FLD] != DBNull.Value)
						objObject.CostMethod = int.Parse(odrPCS[ITM_ProductTable.COSTMETHOD_FLD].ToString().Trim());
					if (odrPCS[ITM_ProductTable.ROUTINGINCREMENT_FLD] != DBNull.Value)
						objObject.RoutingIncrement = int.Parse(odrPCS[ITM_ProductTable.ROUTINGINCREMENT_FLD].ToString().Trim());
					if (odrPCS[ITM_ProductTable.CCNID_FLD] != DBNull.Value)
						objObject.CCNID = int.Parse(odrPCS[ITM_ProductTable.CCNID_FLD].ToString().Trim());
					if (odrPCS[ITM_ProductTable.CATEGORYID_FLD] != DBNull.Value)
						objObject.CategoryID = int.Parse(odrPCS[ITM_ProductTable.CATEGORYID_FLD].ToString().Trim());
					if (odrPCS[ITM_ProductTable.COSTCENTERID_FLD] != DBNull.Value)
						objObject.CostCenterID = int.Parse(odrPCS[ITM_ProductTable.COSTCENTERID_FLD].ToString().Trim());
					if (odrPCS[ITM_ProductTable.DELETEREASONID_FLD] != DBNull.Value)
						objObject.DeleteReasonID = int.Parse(odrPCS[ITM_ProductTable.DELETEREASONID_FLD].ToString().Trim());
					if (odrPCS[ITM_ProductTable.DELIVERYPOLICYID_FLD] != DBNull.Value)
						objObject.DeliveryPolicyID = int.Parse(odrPCS[ITM_ProductTable.DELIVERYPOLICYID_FLD].ToString().Trim());
					if (odrPCS[ITM_ProductTable.FORMATCODEID_FLD] != DBNull.Value)
						objObject.FormatCodeID = int.Parse(odrPCS[ITM_ProductTable.FORMATCODEID_FLD].ToString().Trim());
					if (odrPCS[ITM_ProductTable.FREIGHTCLASSID_FLD] != DBNull.Value)
						objObject.FreightClassID = int.Parse(odrPCS[ITM_ProductTable.FREIGHTCLASSID_FLD].ToString().Trim());
					if (odrPCS[ITM_ProductTable.HAZARDID_FLD] != DBNull.Value)
						objObject.HazardID = int.Parse(odrPCS[ITM_ProductTable.HAZARDID_FLD].ToString().Trim());
					if (odrPCS[ITM_ProductTable.ORDERPOLICYID_FLD] != DBNull.Value)
						objObject.OrderPolicyID = int.Parse(odrPCS[ITM_ProductTable.ORDERPOLICYID_FLD].ToString().Trim());
					if (odrPCS[ITM_ProductTable.ORDERRULEID_FLD] != DBNull.Value)
						objObject.OrderRuleID = int.Parse(odrPCS[ITM_ProductTable.ORDERRULEID_FLD].ToString().Trim());
					if (odrPCS[ITM_ProductTable.SOURCEID_FLD] != DBNull.Value)
						objObject.SourceID = int.Parse(odrPCS[ITM_ProductTable.SOURCEID_FLD].ToString().Trim());
					if (odrPCS[ITM_ProductTable.STOCKUMID_FLD] != DBNull.Value)
						objObject.StockUMID = int.Parse(odrPCS[ITM_ProductTable.STOCKUMID_FLD].ToString().Trim());
					if (odrPCS[ITM_ProductTable.SELLINGUMID_FLD] != DBNull.Value)
						objObject.SellingUMID = int.Parse(odrPCS[ITM_ProductTable.SELLINGUMID_FLD].ToString().Trim());
					if (odrPCS[ITM_ProductTable.HEIGHTUMID_FLD] != DBNull.Value)
						objObject.HeightUMID = int.Parse(odrPCS[ITM_ProductTable.HEIGHTUMID_FLD].ToString().Trim());
					if (odrPCS[ITM_ProductTable.WIDTHUMID_FLD] != DBNull.Value)
						objObject.WidthUMID = int.Parse(odrPCS[ITM_ProductTable.WIDTHUMID_FLD].ToString().Trim());
					if (odrPCS[ITM_ProductTable.LENGTHUMID_FLD] != DBNull.Value)
						objObject.LengthUMID = int.Parse(odrPCS[ITM_ProductTable.LENGTHUMID_FLD].ToString().Trim());
					if (odrPCS[ITM_ProductTable.BUYINGUMID_FLD] != DBNull.Value)
						objObject.BuyingUMID = int.Parse(odrPCS[ITM_ProductTable.BUYINGUMID_FLD].ToString().Trim());
					if (odrPCS[ITM_ProductTable.WEIGHTUMID_FLD] != DBNull.Value)
						objObject.WeightUMID = int.Parse(odrPCS[ITM_ProductTable.WEIGHTUMID_FLD].ToString().Trim());
					if (odrPCS[ITM_ProductTable.LOTSIZE_FLD] != DBNull.Value)
						objObject.LotSize = int.Parse(odrPCS[ITM_ProductTable.LOTSIZE_FLD].ToString().Trim());
					if (odrPCS[ITM_ProductTable.MASTERLOCATIONID_FLD] != DBNull.Value)
						objObject.MasterLocationID = int.Parse(odrPCS[ITM_ProductTable.MASTERLOCATIONID_FLD].ToString().Trim());
					if (odrPCS[ITM_ProductTable.LOCATIONID_FLD] != DBNull.Value)
						objObject.LocationID = int.Parse(odrPCS[ITM_ProductTable.LOCATIONID_FLD].ToString().Trim());

					if (odrPCS[ITM_ProductTable.BINID_FLD] != DBNull.Value)
						objObject.BinID = int.Parse(odrPCS[ITM_ProductTable.BINID_FLD].ToString().Trim());

					if (odrPCS[ITM_ProductTable.PRIMARYVENDORID_FLD] != DBNull.Value)
						objObject.PrimaryVendorID = int.Parse(odrPCS[ITM_ProductTable.PRIMARYVENDORID_FLD].ToString().Trim());

					if (odrPCS[ITM_ProductTable.VENDORLOCATIONID_FLD] != DBNull.Value)
						objObject.VendorLocationID = int.Parse(odrPCS[ITM_ProductTable.VENDORLOCATIONID_FLD].ToString().Trim());

					if (odrPCS[ITM_ProductTable.SAFETYSTOCK_FLD] != DBNull.Value)
						objObject.SafetyStock = Decimal.Parse(odrPCS[ITM_ProductTable.SAFETYSTOCK_FLD].ToString().Trim());

					if (odrPCS[ITM_ProductTable.AGCID_FLD] != DBNull.Value)
						objObject.AGCID = int.Parse(odrPCS[ITM_ProductTable.AGCID_FLD].ToString().Trim());

					//Begin_ Added by Tuan TQ 2005-09-22
					objObject.RegisteredCode = odrPCS[ITM_ProductTable.REGISTEREDCODE_FLD].ToString().Trim();
					objObject.PartNameVN = odrPCS[ITM_ProductTable.PARTNAMEVN_FLD].ToString().Trim();
					objObject.TaxCode = odrPCS[ITM_ProductTable.TAXCODE_FLD].ToString().Trim();

					if (odrPCS[ITM_ProductTable.LICENSEFEE_FLD] != DBNull.Value)
						objObject.LicenseFee = decimal.Parse(odrPCS[ITM_ProductTable.LICENSEFEE_FLD].ToString().Trim());

					if (odrPCS[ITM_ProductTable.INVENTORID_FLD] != DBNull.Value)
						objObject.Inventor = int.Parse(odrPCS[ITM_ProductTable.INVENTORID_FLD].ToString().Trim());

					if (odrPCS[ITM_ProductTable.PRODUCTTYPEID_FLD] != DBNull.Value)
						objObject.ProductTypeId = int.Parse(odrPCS[ITM_ProductTable.PRODUCTTYPEID_FLD].ToString().Trim());

					if (odrPCS[ITM_ProductTable.LISTPRICE_FLD] != DBNull.Value)
						objObject.ListPrice = Decimal.Parse(odrPCS[ITM_ProductTable.LISTPRICE_FLD].ToString().Trim());

					if (odrPCS[ITM_ProductTable.VENDORCURRENCYID_FLD] != DBNull.Value)
						objObject.VendorCurrencyID = int.Parse(odrPCS[ITM_ProductTable.VENDORCURRENCYID_FLD].ToString().Trim());

					if (odrPCS[ITM_ProductTable.QUANTITYSET_FLD] != DBNull.Value)
						objObject.QuantitySet = Decimal.Parse(odrPCS[ITM_ProductTable.QUANTITYSET_FLD].ToString().Trim());

					//End_ Added by Tuan TQ 2005-09-22

					//Added by Tuan TQ. 01 Mar, 2006
					if (odrPCS[ITM_ProductTable.MINPRODUCE_FLD] != DBNull.Value)
						objObject.MinProduce = Decimal.Parse(odrPCS[ITM_ProductTable.MINPRODUCE_FLD].ToString().Trim());

					if (odrPCS[ITM_ProductTable.MAXPRODUCE_FLD] != DBNull.Value)
						objObject.MaxProduce = Decimal.Parse(odrPCS[ITM_ProductTable.MAXPRODUCE_FLD].ToString().Trim());
					//End added

					//HACK: added by Tuan TQ. 17 May, 2006
					if (odrPCS[ITM_ProductTable.MAXROUNDUPTOMIN_FLD] != DBNull.Value)
					{
						objObject.MaxRoundUpToMin = Decimal.Parse(odrPCS[ITM_ProductTable.MAXROUNDUPTOMIN_FLD].ToString().Trim());
					}

					if (odrPCS[ITM_ProductTable.MAXROUNDUPTOMULTIPLE_FLD] != DBNull.Value)
					{
						objObject.MaxRoundUpToMultiple = Decimal.Parse(odrPCS[ITM_ProductTable.MAXROUNDUPTOMULTIPLE_FLD].ToString().Trim());
					}

					if (odrPCS[ITM_ProductTable.ACADJUSTMENTMASTERID_FLD] != DBNull.Value)
					{
						objObject.ACAdjustmentMasterID = int.Parse(odrPCS[ITM_ProductTable.ACADJUSTMENTMASTERID_FLD].ToString().Trim());
					}
					//end hack

					if (odrPCS[ITM_ProductTable.ORDERPOINT_FLD] != DBNull.Value)
						objObject.OrderPoint = Decimal.Parse(odrPCS[ITM_ProductTable.ORDERPOINT_FLD].ToString().Trim());

					// added: dungla 15-02-2005
					try
					{
						objObject.CostCenterRateMasterID = int.Parse(odrPCS[ITM_ProductTable.COSTCENTERRATEMASTERID_FLD].ToString().Trim());
					}
					catch
					{
					}

					try
					{
						objObject.ProductGroupID = int.Parse(odrPCS[ITM_ProductTable.PRODUCTGROUPID_FLD].ToString().Trim());
					}
					catch
					{
					}

					try
					{
						objObject.ProductionLineID = int.Parse(odrPCS[ITM_ProductTable.PRODUCTIONLINEID_FLD].ToString().Trim());
					}
					catch
					{
					}
					// end added: dungla 15-02-2005
				}

				return objObject;
			}
			catch (OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}

			catch (Exception ex)
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}

			finally
			{
				if (oconPCS != null)
				{
					if (oconPCS.State != ConnectionState.Closed)
					{
						oconPCS.Close();
					}
				}
			}
		}


		//**************************************************************************              
		///    <Description>
		///       This method uses to update data to ITM_Product
		///    </Description>
		///    <Inputs>
		///       ITM_ProductVO       
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       HungLa
		///    </Authors>
		///    <History>
		///       09-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public void Update(object pobjObjecVO)
		{
			const string METHOD_NAME = THIS + ".Update()";

			ITM_ProductVO objObject = (ITM_ProductVO) pobjObjecVO;


			//prepare value for parameters
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql = "UPDATE ITM_Product SET "
					+ ITM_ProductTable.CODE_FLD + "=   ?" + ","
					+ ITM_ProductTable.REVISION_FLD + "=   ?" + ","
					+ ITM_ProductTable.DESCRIPTION_FLD + "=   ?" + ","
					+ ITM_ProductTable.SETUPDATE_FLD + "=   ?" + ","
					+ ITM_ProductTable.VAT_FLD + "=   ?" + ","
					+ ITM_ProductTable.IMPORTTAX_FLD + "=   ?" + ","
					+ ITM_ProductTable.EXPORTTAX_FLD + "=   ?" + ","
					+ ITM_ProductTable.SPECIALTAX_FLD + "=   ?" + ","
					+ ITM_ProductTable.MAKEITEM_FLD + "=   ?" + ","
					+ ITM_ProductTable.PARTNUMBER_FLD + "=   ?" + ","
					+ ITM_ProductTable.OTHERINFO1_FLD + "=   ?" + ","
					+ ITM_ProductTable.OTHERINFO2_FLD + "=   ?" + ","
					+ ITM_ProductTable.LENGTH_FLD + "=   ?" + ","
					+ ITM_ProductTable.WIDTH_FLD + "=   ?" + ","
					+ ITM_ProductTable.HEIGHT_FLD + "=   ?" + ","
					+ ITM_ProductTable.WEIGHT_FLD + "=   ?" + ","
					+ ITM_ProductTable.FINISHEDGOODS_FLD + "=   ?" + ","
					+ ITM_ProductTable.SHELFLIFE_FLD + "=   ?" + ","
					+ ITM_ProductTable.LOTCONTROL_FLD + "=   ?" + ","
					+ ITM_ProductTable.QASTATUS_FLD + "=   ?" + ","
					+ ITM_ProductTable.STOCK_FLD + "=   ?" + ","
					+ ITM_ProductTable.PLANTYPE_FLD + "=   ?" + ","
					+ ITM_ProductTable.AUTOCONVERSION_FLD + "=   ?" + ","
					+ ITM_ProductTable.ORDERQUANTITY_FLD + "=   ?" + ","
					+ ITM_ProductTable.LTREQUISITION_FLD + "=   ?" + ","
					+ ITM_ProductTable.LTSAFETYSTOCK_FLD + "=   ?" + ","
					+ ITM_ProductTable.ORDERQUANTITYMULTIPLE_FLD + "=   ?" + ","
					+ ITM_ProductTable.SCRAPPERCENT_FLD + "=   ?" + ","
					+ ITM_ProductTable.MINIMUMSTOCK_FLD + "=   ?" + ","
					+ ITM_ProductTable.MAXIMUMSTOCK_FLD + "=   ?" + ","
					+ ITM_ProductTable.CONVERSIONTOLERANCE_FLD + "=   ?" + ","
					+ ITM_ProductTable.VOUCHERTOLERANCE_FLD + "=   ?" + ","
					+ ITM_ProductTable.RECEIVETOLERANCE_FLD + "=   ?" + ","
					+ ITM_ProductTable.ISSUESIZE_FLD + "=   ?" + ","
					+ ITM_ProductTable.LTFIXEDTIME_FLD + "=   ?" + ","
					+ ITM_ProductTable.LTVARIABLETIME_FLD + "=   ?" + ","
					+ ITM_ProductTable.LTDOCKTOSTOCK_FLD + "=   ?" + ","
					+ ITM_ProductTable.LTORDERPREPARE_FLD + "=   ?" + ","
					+ ITM_ProductTable.LTSHIPPINGPREPARE_FLD + "=   ?" + ","
					+ ITM_ProductTable.LTSALESATP_FLD + "=   ?" + ","
					+ ITM_ProductTable.SHIPTOLERANCEID_FLD + "=   ?" + ","
					+ ITM_ProductTable.BUYERID_FLD + "=   ?" + ","
					+ ITM_ProductTable.BOMDESCRIPTION_FLD + "=   ?" + ","
					+ ITM_ProductTable.BOMINCREMENT_FLD + "=   ?" + ","
					+ ITM_ProductTable.ROUTINGDESCRIPTION_FLD + "=   ?" + ","
					+ ITM_ProductTable.CREATEDATETIME_FLD + "=   ?" + ","
					+ ITM_ProductTable.UPDATEDATETIME_FLD + "=   ?" + ","
					+ ITM_ProductTable.COSTMETHOD_FLD + "=   ?" + ","
					+ ITM_ProductTable.ROUTINGINCREMENT_FLD + "=   ?" + ","
					+ ITM_ProductTable.CCNID_FLD + "=   ?" + ","
					+ ITM_ProductTable.CATEGORYID_FLD + "=   ?" + ","
					+ ITM_ProductTable.COSTCENTERID_FLD + "=   ?" + ","
					+ ITM_ProductTable.DELETEREASONID_FLD + "=   ?" + ","
					+ ITM_ProductTable.DELIVERYPOLICYID_FLD + "=   ?" + ","
					+ ITM_ProductTable.FORMATCODEID_FLD + "=   ?" + ","
					+ ITM_ProductTable.FREIGHTCLASSID_FLD + "=   ?" + ","
					+ ITM_ProductTable.HAZARDID_FLD + "=   ?" + ","
					+ ITM_ProductTable.ORDERPOLICYID_FLD + "=   ?" + ","
					+ ITM_ProductTable.ORDERRULEID_FLD + "=   ?" + ","
					+ ITM_ProductTable.SOURCEID_FLD + "=   ?" + ","
					+ ITM_ProductTable.STOCKUMID_FLD + "=   ?" + ","
					+ ITM_ProductTable.SELLINGUMID_FLD + "=   ?" + ","
					+ ITM_ProductTable.HEIGHTUMID_FLD + "=   ?" + ","
					+ ITM_ProductTable.WIDTHUMID_FLD + "=   ?" + ","
					+ ITM_ProductTable.LENGTHUMID_FLD + "=   ?" + ","
					+ ITM_ProductTable.BUYINGUMID_FLD + "=   ?" + ","
					+ ITM_ProductTable.WEIGHTUMID_FLD + "=   ?" + ","
					+ ITM_ProductTable.LOTSIZE_FLD + "=   ?" + ","
					+ ITM_ProductTable.MASTERLOCATIONID_FLD + "=   ?" + ","
					+ ITM_ProductTable.LOCATIONID_FLD + "=   ?" + ","
					+ ITM_ProductTable.BINID_FLD + "=   ?" + ","
					+ ITM_ProductTable.PRIMARYVENDORID_FLD + "=   ?" + ","
					+ ITM_ProductTable.VENDORLOCATIONID_FLD + "=   ?" + ","
					+ ITM_ProductTable.SAFETYSTOCK_FLD + "=   ?" + ","
					+ ITM_ProductTable.AGCID_FLD + "=   ?" + ","
					+ ITM_ProductTable.PARTNAMEVN_FLD + " =?, "
					+ ITM_ProductTable.INVENTORID_FLD + " =?, "
					+ ITM_ProductTable.LICENSEFEE_FLD + " =?, "
					+ ITM_ProductTable.LISTPRICE_FLD + " = ?, "
					+ ITM_ProductTable.VENDORCURRENCYID_FLD + " = ?,"
					+ ITM_ProductTable.QUANTITYSET_FLD + " =?, "
					+ ITM_ProductTable.TAXCODE_FLD + " =?, "
					+ ITM_ProductTable.MINPRODUCE_FLD + " =?, "
					+ ITM_ProductTable.MAXPRODUCE_FLD + " =?, "
					+ ITM_ProductTable.PRODUCTTYPEID_FLD + " =?, "
					+ ITM_ProductTable.MAXROUNDUPTOMIN_FLD + " =?, "
					+ ITM_ProductTable.MAXROUNDUPTOMULTIPLE_FLD + " =?, "
					+ ITM_ProductTable.ACADJUSTMENTMASTERID_FLD + " =?, "
					+ ITM_ProductTable.REGISTEREDCODE_FLD + " =?, "
					+ ITM_ProductTable.ORDERPOINT_FLD + "= ?"
					+ " WHERE " + ITM_ProductTable.PRODUCTID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.CODE_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[ITM_ProductTable.CODE_FLD].Value = objObject.Code;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.REVISION_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[ITM_ProductTable.REVISION_FLD].Value = objObject.Revision;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.DESCRIPTION_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[ITM_ProductTable.DESCRIPTION_FLD].Value = objObject.Description;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.SETUPDATE_FLD, OleDbType.Date));
				ocmdPCS.Parameters[ITM_ProductTable.SETUPDATE_FLD].Value = objObject.SetupDate;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.VAT_FLD, OleDbType.Decimal));
				if (objObject.VAT >= 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.VAT_FLD].Value = objObject.VAT;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.VAT_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.IMPORTTAX_FLD, OleDbType.Decimal));
				if (objObject.ImportTax >= 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.IMPORTTAX_FLD].Value = objObject.ImportTax;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.IMPORTTAX_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.EXPORTTAX_FLD, OleDbType.Decimal));
				if (objObject.ExportTax >= 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.EXPORTTAX_FLD].Value = objObject.ExportTax;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.EXPORTTAX_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.SPECIALTAX_FLD, OleDbType.Decimal));
				if (objObject.SpecialTax >= 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.SPECIALTAX_FLD].Value = objObject.SpecialTax;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.SPECIALTAX_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.MAKEITEM_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[ITM_ProductTable.MAKEITEM_FLD].Value = objObject.MakeItem;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.PARTNUMBER_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[ITM_ProductTable.PARTNUMBER_FLD].Value = objObject.PartNumber;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.OTHERINFO1_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[ITM_ProductTable.OTHERINFO1_FLD].Value = objObject.OtherInfo1;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.OTHERINFO2_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[ITM_ProductTable.OTHERINFO2_FLD].Value = objObject.OtherInfo2;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.LENGTH_FLD, OleDbType.Decimal));
				if (objObject.Length >= 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.LENGTH_FLD].Value = objObject.Length;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.LENGTH_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.WIDTH_FLD, OleDbType.Decimal));
				if (objObject.Width >= 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.WIDTH_FLD].Value = objObject.Width;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.WIDTH_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.HEIGHT_FLD, OleDbType.Decimal));
				if (objObject.Height >= 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.HEIGHT_FLD].Value = objObject.Height;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.HEIGHT_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.WEIGHT_FLD, OleDbType.Decimal));
				if (objObject.Weight >= 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.WEIGHT_FLD].Value = objObject.Weight;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.WEIGHT_FLD].Value = DBNull.Value;
				}


				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.FINISHEDGOODS_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[ITM_ProductTable.FINISHEDGOODS_FLD].Value = objObject.FinishedGoods;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.SHELFLIFE_FLD, OleDbType.Decimal));
				if (objObject.ShelfLife >= 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.SHELFLIFE_FLD].Value = objObject.ShelfLife;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.SHELFLIFE_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.LOTCONTROL_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[ITM_ProductTable.LOTCONTROL_FLD].Value = objObject.LotControl;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.QASTATUS_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[ITM_ProductTable.QASTATUS_FLD].Value = objObject.QAStatus;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.STOCK_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[ITM_ProductTable.STOCK_FLD].Value = objObject.Stock;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.PLANTYPE_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[ITM_ProductTable.PLANTYPE_FLD].Value = objObject.PlanType;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.AUTOCONVERSION_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[ITM_ProductTable.AUTOCONVERSION_FLD].Value = objObject.AutoConversion;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.ORDERQUANTITY_FLD, OleDbType.Decimal));
				if (objObject.OrderQuantity >= 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.ORDERQUANTITY_FLD].Value = objObject.OrderQuantity;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.ORDERQUANTITY_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.LTREQUISITION_FLD, OleDbType.Decimal));
				if (objObject.LTRequisition >= 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.LTREQUISITION_FLD].Value = objObject.LTRequisition;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.LTREQUISITION_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.LTSAFETYSTOCK_FLD, OleDbType.Decimal));
				if (objObject.LTSafetyStock >= 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.LTSAFETYSTOCK_FLD].Value = objObject.LTSafetyStock;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.LTSAFETYSTOCK_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.ORDERQUANTITYMULTIPLE_FLD, OleDbType.Decimal));
				if (objObject.OrderQuantityMultiple >= 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.ORDERQUANTITYMULTIPLE_FLD].Value = objObject.OrderQuantityMultiple;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.ORDERQUANTITYMULTIPLE_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.SCRAPPERCENT_FLD, OleDbType.Decimal));
				if (objObject.ScrapPercent >= 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.SCRAPPERCENT_FLD].Value = objObject.ScrapPercent;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.SCRAPPERCENT_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.MINIMUMSTOCK_FLD, OleDbType.Decimal));
				if (objObject.MinimumStock >= 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.MINIMUMSTOCK_FLD].Value = objObject.MinimumStock;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.MINIMUMSTOCK_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.MAXIMUMSTOCK_FLD, OleDbType.Decimal));
				if (objObject.MaximumStock >= 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.MAXIMUMSTOCK_FLD].Value = objObject.MaximumStock;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.MAXIMUMSTOCK_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.CONVERSIONTOLERANCE_FLD, OleDbType.Decimal));
				if (objObject.ConversionTolerance >= 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.CONVERSIONTOLERANCE_FLD].Value = objObject.ConversionTolerance;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.CONVERSIONTOLERANCE_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.VOUCHERTOLERANCE_FLD, OleDbType.Decimal));
				if (objObject.VoucherTolerance >= 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.VOUCHERTOLERANCE_FLD].Value = objObject.VoucherTolerance;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.VOUCHERTOLERANCE_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.RECEIVETOLERANCE_FLD, OleDbType.Decimal));
				if (objObject.ReceiveTolerance >= 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.RECEIVETOLERANCE_FLD].Value = objObject.ReceiveTolerance;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.RECEIVETOLERANCE_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.ISSUESIZE_FLD, OleDbType.Decimal));
				if (objObject.IssueSize >= 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.ISSUESIZE_FLD].Value = objObject.IssueSize;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.ISSUESIZE_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.LTFIXEDTIME_FLD, OleDbType.Decimal));
				if (objObject.LTFixedTime >= 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.LTFIXEDTIME_FLD].Value = objObject.LTFixedTime;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.LTFIXEDTIME_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.LTVARIABLETIME_FLD, OleDbType.Decimal));
				if (objObject.LTVariableTime >= 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.LTVARIABLETIME_FLD].Value = objObject.LTVariableTime;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.LTVARIABLETIME_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.LTDOCKTOSTOCK_FLD, OleDbType.Decimal));
				if (objObject.LTDocToStock >= 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.LTDOCKTOSTOCK_FLD].Value = objObject.LTDocToStock;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.LTDOCKTOSTOCK_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.LTORDERPREPARE_FLD, OleDbType.Decimal));
				if (objObject.LTOrderPrepare >= 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.LTORDERPREPARE_FLD].Value = objObject.LTOrderPrepare;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.LTORDERPREPARE_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.LTSHIPPINGPREPARE_FLD, OleDbType.Decimal));
				if (objObject.LTShippingPrepare >= 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.LTSHIPPINGPREPARE_FLD].Value = objObject.LTShippingPrepare;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.LTSHIPPINGPREPARE_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.LTSALESATP_FLD, OleDbType.Decimal));
				if (objObject.LTSalesATP >= 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.LTSALESATP_FLD].Value = objObject.LTSalesATP;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.LTSALESATP_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.SHIPTOLERANCEID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[ITM_ProductTable.SHIPTOLERANCEID_FLD].Value = objObject.ShipToleranceID;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.BUYERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[ITM_ProductTable.BUYERID_FLD].Value = objObject.BuyerID;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.BOMDESCRIPTION_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[ITM_ProductTable.BOMDESCRIPTION_FLD].Value = objObject.BOMDescription;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.BOMINCREMENT_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[ITM_ProductTable.BOMINCREMENT_FLD].Value = objObject.BomIncrement;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.ROUTINGDESCRIPTION_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[ITM_ProductTable.ROUTINGDESCRIPTION_FLD].Value = objObject.RoutingDescription;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.CREATEDATETIME_FLD, OleDbType.Date));
				ocmdPCS.Parameters[ITM_ProductTable.CREATEDATETIME_FLD].Value = objObject.CreateDateTime;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.UPDATEDATETIME_FLD, OleDbType.Date));
				ocmdPCS.Parameters[ITM_ProductTable.UPDATEDATETIME_FLD].Value = objObject.UpdateDateTime;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.COSTMETHOD_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[ITM_ProductTable.COSTMETHOD_FLD].Value = objObject.CostMethod;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.ROUTINGINCREMENT_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[ITM_ProductTable.ROUTINGINCREMENT_FLD].Value = objObject.RoutingIncrement;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.CCNID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[ITM_ProductTable.CCNID_FLD].Value = objObject.CCNID;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.CATEGORYID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[ITM_ProductTable.CATEGORYID_FLD].Value = objObject.CategoryID;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.COSTCENTERID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[ITM_ProductTable.COSTCENTERID_FLD].Value = objObject.CostCenterID;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.DELETEREASONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[ITM_ProductTable.DELETEREASONID_FLD].Value = objObject.DeleteReasonID;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.DELIVERYPOLICYID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[ITM_ProductTable.DELIVERYPOLICYID_FLD].Value = objObject.DeliveryPolicyID;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.FORMATCODEID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[ITM_ProductTable.FORMATCODEID_FLD].Value = objObject.FormatCodeID;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.FREIGHTCLASSID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[ITM_ProductTable.FREIGHTCLASSID_FLD].Value = objObject.FreightClassID;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.HAZARDID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[ITM_ProductTable.HAZARDID_FLD].Value = objObject.HazardID;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.ORDERPOLICYID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[ITM_ProductTable.ORDERPOLICYID_FLD].Value = objObject.OrderPolicyID;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.ORDERRULEID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[ITM_ProductTable.ORDERRULEID_FLD].Value = objObject.OrderRuleID;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.SOURCEID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[ITM_ProductTable.SOURCEID_FLD].Value = objObject.SourceID;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.STOCKUMID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[ITM_ProductTable.STOCKUMID_FLD].Value = objObject.StockUMID;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.SELLINGUMID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[ITM_ProductTable.SELLINGUMID_FLD].Value = objObject.SellingUMID;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.HEIGHTUMID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[ITM_ProductTable.HEIGHTUMID_FLD].Value = objObject.HeightUMID;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.WIDTHUMID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[ITM_ProductTable.WIDTHUMID_FLD].Value = objObject.WidthUMID;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.LENGTHUMID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[ITM_ProductTable.LENGTHUMID_FLD].Value = objObject.LengthUMID;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.BUYINGUMID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[ITM_ProductTable.BUYINGUMID_FLD].Value = objObject.BuyingUMID;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.WEIGHTUMID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[ITM_ProductTable.WEIGHTUMID_FLD].Value = objObject.WeightUMID;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.LOTSIZE_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[ITM_ProductTable.LOTSIZE_FLD].Value = objObject.LotSize;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.MASTERLOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[ITM_ProductTable.MASTERLOCATIONID_FLD].Value = objObject.MasterLocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.LOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[ITM_ProductTable.LOCATIONID_FLD].Value = objObject.LocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.BINID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[ITM_ProductTable.BINID_FLD].Value = objObject.BinID;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.PRIMARYVENDORID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[ITM_ProductTable.PRIMARYVENDORID_FLD].Value = objObject.PrimaryVendorID;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.VENDORLOCATIONID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[ITM_ProductTable.VENDORLOCATIONID_FLD].Value = objObject.VendorLocationID;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.SAFETYSTOCK_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[ITM_ProductTable.SAFETYSTOCK_FLD].Value = objObject.SafetyStock;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.AGCID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[ITM_ProductTable.AGCID_FLD].Value = objObject.AGCID;

				//Begin_ Added by Tuan TQ
				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.PARTNAMEVN_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[ITM_ProductTable.PARTNAMEVN_FLD].Value = objObject.PartNameVN;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.INVENTORID_FLD, OleDbType.Integer));
				if (objObject.Inventor > 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.INVENTORID_FLD].Value = objObject.Inventor;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.INVENTORID_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.LICENSEFEE_FLD, OleDbType.Decimal));
				if (objObject.LicenseFee >= 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.LICENSEFEE_FLD].Value = objObject.LicenseFee;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.LICENSEFEE_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.LISTPRICE_FLD, OleDbType.Decimal));
				if (objObject.ListPrice >= 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.LISTPRICE_FLD].Value = objObject.ListPrice;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.LISTPRICE_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.VENDORCURRENCYID_FLD, OleDbType.Integer));
				if (objObject.VendorCurrencyID > 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.VENDORCURRENCYID_FLD].Value = objObject.VendorCurrencyID;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.VENDORCURRENCYID_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.QUANTITYSET_FLD, OleDbType.Decimal));
				if (objObject.QuantitySet >= 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.QUANTITYSET_FLD].Value = objObject.QuantitySet;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.QUANTITYSET_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.TAXCODE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[ITM_ProductTable.TAXCODE_FLD].Value = objObject.TaxCode;

				//Added:  01 Mar, 2006
				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.MINPRODUCE_FLD, OleDbType.Decimal));
				if (objObject.MinProduce >= 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.MINPRODUCE_FLD].Value = objObject.MinProduce;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.MINPRODUCE_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.MAXPRODUCE_FLD, OleDbType.Decimal));
				if (objObject.MaxProduce >= 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.MAXPRODUCE_FLD].Value = objObject.MaxProduce;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.MAXPRODUCE_FLD].Value = DBNull.Value;
				}
				//End

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.PRODUCTTYPEID_FLD, OleDbType.Integer));
				if (objObject.ProductTypeId > 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.PRODUCTTYPEID_FLD].Value = objObject.ProductTypeId;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.PRODUCTTYPEID_FLD].Value = DBNull.Value;
				}
				//End_ Added by Tuan TQ

				//HACK: added by Tuan TQ. 17 May, 2006
				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.MAXROUNDUPTOMIN_FLD, OleDbType.Decimal));
				if (objObject.MaxRoundUpToMin >= 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.MAXROUNDUPTOMIN_FLD].Value = objObject.MaxRoundUpToMin;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.MAXROUNDUPTOMIN_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.MAXROUNDUPTOMULTIPLE_FLD, OleDbType.Decimal));
				if (objObject.MaxRoundUpToMultiple >= 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.MAXROUNDUPTOMULTIPLE_FLD].Value = objObject.MaxRoundUpToMultiple;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.MAXROUNDUPTOMULTIPLE_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.ACADJUSTMENTMASTERID_FLD, OleDbType.Integer));
				if (objObject.ACAdjustmentMasterID > 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.ACADJUSTMENTMASTERID_FLD].Value = objObject.ACAdjustmentMasterID;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.ACADJUSTMENTMASTERID_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.REGISTEREDCODE_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[ITM_ProductTable.REGISTEREDCODE_FLD].Value = objObject.RegisteredCode;
				//End hack

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.ORDERPOINT_FLD, OleDbType.Decimal));
				ocmdPCS.Parameters[ITM_ProductTable.ORDERPOINT_FLD].Value = objObject.OrderPoint;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.PRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[ITM_ProductTable.PRODUCTID_FLD].Value = objObject.ProductID;

				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();
				ocmdPCS.ExecuteNonQuery();
			}
			catch (OleDbException ex)
			{
				if (ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_KEYCODE)
				{
					throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);
				}
				else
				{
					throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
				}
			}

			catch (InvalidOperationException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}
			catch (Exception ex)
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}

			finally
			{
				if (oconPCS != null)
				{
					if (oconPCS.State != ConnectionState.Closed)
					{
						oconPCS.Close();
					}
				}
			}

		}

		//**************************************************************************              
		///    <Description>
		///       This method uses to update Bom's data to ITM_Product
		///    </Description>
		///    <Inputs>
		///       ITM_ProductVO       
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       TuanDm
		///    </Authors>
		///    <History>
		///       23-Mar-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public void UpdateForBom(object pobjObjecVO)
		{
			const string METHOD_NAME = THIS + ".UpdateForBom()";

			ITM_ProductVO objObject = (ITM_ProductVO) pobjObjecVO;

			//prepare value for parameters
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql = "UPDATE ITM_Product SET "
					+ ITM_ProductTable.BOMDESCRIPTION_FLD + "=   ?" + ","
					+ ITM_ProductTable.BOMINCREMENT_FLD + "=   ?"
					+ " WHERE " + ITM_ProductTable.PRODUCTID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.BOMDESCRIPTION_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[ITM_ProductTable.BOMDESCRIPTION_FLD].Value = objObject.BOMDescription;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.BOMINCREMENT_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[ITM_ProductTable.BOMINCREMENT_FLD].Value = objObject.BomIncrement;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.PRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[ITM_ProductTable.PRODUCTID_FLD].Value = objObject.ProductID;

				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();
				ocmdPCS.ExecuteNonQuery();
			}
			catch (OleDbException ex)
			{
				if (ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_KEYCODE)
				{
					throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);
				}
				else
				{
					throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
				}
			}

			catch (InvalidOperationException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}
			catch (Exception ex)
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}
			finally
			{
				if (oconPCS != null)
				{
					if (oconPCS.State != ConnectionState.Closed)
					{
						oconPCS.Close();
					}
				}
			}
		}


		//**************************************************************************              
		///    <Description>
		///       This method uses to update data to ITM_Product
		///    </Description>
		///    <Inputs>
		///       ITM_ProductVO       
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       HungLa
		///    </Authors>
		///    <History>
		///       09-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public void UpdateCostCenter(int pintProductID, int pintCostCenterID)
		{
			const string METHOD_NAME = THIS + ".UpdateCostCenter()";

			//prepare value for parameters
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql = "UPDATE ITM_Product SET "
					+ ITM_ProductTable.COSTCENTERID_FLD + "=   ?"
					+ " WHERE " + ITM_ProductTable.PRODUCTID_FLD + "= ?";

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.COSTCENTERID_FLD, OleDbType.Integer));
				if (pintCostCenterID > 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.COSTCENTERID_FLD].Value = pintCostCenterID;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.COSTCENTERID_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.PRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[ITM_ProductTable.PRODUCTID_FLD].Value = pintProductID;


				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();
				ocmdPCS.ExecuteNonQuery();
			}
			catch (OleDbException ex)
			{
				if (ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_KEYCODE)
				{
					throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);
				}
				else
				{
					throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
				}
			}
			catch (InvalidOperationException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}
			catch (Exception ex)
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}
			finally
			{
				if (oconPCS != null)
				{
					if (oconPCS.State != ConnectionState.Closed)
					{
						oconPCS.Close();
					}
				}
			}
		}


		//**************************************************************************              
		///    <Description>
		///       This method uses to update data to ITM_Product
		///    </Description>
		///    <Inputs>
		///       ITM_ProductVO       
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       HungLa
		///    </Authors>
		///    <History>
		///       09-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public void UpdateProductInfo(object pobjObjecVO)
		{
			const string METHOD_NAME = THIS + ".Update()";

			ITM_ProductVO objObject = (ITM_ProductVO) pobjObjecVO;


			//prepare value for parameters
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				//Set the update 
				objObject.UpdateDateTime = GetDatabaseDate();

				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				strSql = "UPDATE ITM_Product SET "
					+ ITM_ProductTable.CODE_FLD + "=   ?" + ","
					+ ITM_ProductTable.REVISION_FLD + "=   ?" + ","
					+ ITM_ProductTable.DESCRIPTION_FLD + "=   ?" + ","
					+ ITM_ProductTable.SETUPDATE_FLD + "=   ?" + ","
					+ ITM_ProductTable.VAT_FLD + "=   ?" + ","
					+ ITM_ProductTable.IMPORTTAX_FLD + "=   ?" + ","
					+ ITM_ProductTable.EXPORTTAX_FLD + "=   ?" + ","
					+ ITM_ProductTable.SPECIALTAX_FLD + "=   ?" + ","
					+ ITM_ProductTable.MAKEITEM_FLD + "=   ?" + ","
					+ ITM_ProductTable.PARTNUMBER_FLD + "=   ?" + ","
					+ ITM_ProductTable.OTHERINFO1_FLD + "=   ?" + ","
					+ ITM_ProductTable.OTHERINFO2_FLD + "=   ?" + ","
					+ ITM_ProductTable.LENGTH_FLD + "=   ?" + ","
					+ ITM_ProductTable.WIDTH_FLD + "=   ?" + ","
					+ ITM_ProductTable.HEIGHT_FLD + "=   ?" + ","
					+ ITM_ProductTable.WEIGHT_FLD + "=   ?" + ","
					+ ITM_ProductTable.FINISHEDGOODS_FLD + "=   ?" + ","
					+ ITM_ProductTable.SHELFLIFE_FLD + "=   ?" + ","
					+ ITM_ProductTable.LOTCONTROL_FLD + "=   ?" + ","
					+ ITM_ProductTable.QASTATUS_FLD + "=   ?" + ","
					+ ITM_ProductTable.STOCK_FLD + "=   ?" + ","
					+ ITM_ProductTable.PLANTYPE_FLD + "=   ?" + ","
					+ ITM_ProductTable.AUTOCONVERSION_FLD + "=   ?" + ","
					+ ITM_ProductTable.ORDERQUANTITY_FLD + "=   ?" + ","
					+ ITM_ProductTable.LTREQUISITION_FLD + "=   ?" + ","
					+ ITM_ProductTable.LTSAFETYSTOCK_FLD + "=   ?" + ","
					+ ITM_ProductTable.ORDERQUANTITYMULTIPLE_FLD + "=   ?" + ","
					+ ITM_ProductTable.SCRAPPERCENT_FLD + "=   ?" + ","
					+ ITM_ProductTable.MINIMUMSTOCK_FLD + "=   ?" + ","
					+ ITM_ProductTable.MAXIMUMSTOCK_FLD + "=   ?" + ","
					+ ITM_ProductTable.CONVERSIONTOLERANCE_FLD + "=   ?" + ","
					+ ITM_ProductTable.VOUCHERTOLERANCE_FLD + "=   ?" + ","
					+ ITM_ProductTable.RECEIVETOLERANCE_FLD + "=   ?" + ","
					+ ITM_ProductTable.ISSUESIZE_FLD + "=   ?" + ","
					+ ITM_ProductTable.LTFIXEDTIME_FLD + "=   ?" + ","
					+ ITM_ProductTable.LTVARIABLETIME_FLD + "=   ?" + ","
					+ ITM_ProductTable.LTDOCKTOSTOCK_FLD + "=   ?" + ","
					+ ITM_ProductTable.LTORDERPREPARE_FLD + "=   ?" + ","
					+ ITM_ProductTable.LTSHIPPINGPREPARE_FLD + "=   ?" + ","
					+ ITM_ProductTable.LTSALESATP_FLD + "=   ?" + ","
					+ ITM_ProductTable.SHIPTOLERANCEID_FLD + "=   ?" + ","
					+ ITM_ProductTable.BUYERID_FLD + "=   ?" + ","
					+ ITM_ProductTable.BOMDESCRIPTION_FLD + "=   ?" + ","
					+ ITM_ProductTable.BOMINCREMENT_FLD + "=   ?" + ","
					+ ITM_ProductTable.ROUTINGDESCRIPTION_FLD + "=   ?" + ","
					//+ ITM_ProductTable.CREATEDATETIME_FLD + "=   ?" + ","
					+ ITM_ProductTable.UPDATEDATETIME_FLD + "=   ?" + ","
					+ ITM_ProductTable.COSTMETHOD_FLD + "=   ?" + ","
					+ ITM_ProductTable.ROUTINGINCREMENT_FLD + "=   ?" + ","
					+ ITM_ProductTable.CCNID_FLD + "=   ?" + ","
					+ ITM_ProductTable.CATEGORYID_FLD + "=   ?" + ","
					+ ITM_ProductTable.COSTCENTERID_FLD + "=   ?" + ","
					+ ITM_ProductTable.DELETEREASONID_FLD + "=   ?" + ","
					+ ITM_ProductTable.DELIVERYPOLICYID_FLD + "=   ?" + ","
					+ ITM_ProductTable.FORMATCODEID_FLD + "=   ?" + ","
					+ ITM_ProductTable.FREIGHTCLASSID_FLD + "=   ?" + ","
					+ ITM_ProductTable.HAZARDID_FLD + "=   ?" + ","
					+ ITM_ProductTable.ORDERPOLICYID_FLD + "=   ?" + ","
					+ ITM_ProductTable.ORDERRULEID_FLD + "=   ?" + ","
					+ ITM_ProductTable.SOURCEID_FLD + "=   ?" + ","
					+ ITM_ProductTable.STOCKUMID_FLD + "=   ?" + ","
					+ ITM_ProductTable.SELLINGUMID_FLD + "=   ?" + ","
					+ ITM_ProductTable.HEIGHTUMID_FLD + "=   ?" + ","
					+ ITM_ProductTable.WIDTHUMID_FLD + "=   ?" + ","
					+ ITM_ProductTable.LENGTHUMID_FLD + "=   ?" + ","
					+ ITM_ProductTable.BUYINGUMID_FLD + "=   ?" + ","
					+ ITM_ProductTable.WEIGHTUMID_FLD + "=   ?" + ","
					+ ITM_ProductTable.LOTSIZE_FLD + "=   ?" + ","
					+ ITM_ProductTable.MASTERLOCATIONID_FLD + "=   ?" + ","
					+ ITM_ProductTable.LOCATIONID_FLD + "=   ?" + ","
					+ ITM_ProductTable.BINID_FLD + "=   ?" + ","
					+ ITM_ProductTable.PRIMARYVENDORID_FLD + "=   ?" + ","
					+ ITM_ProductTable.VENDORLOCATIONID_FLD + "=   ?" + ","
					+ ITM_ProductTable.SAFETYSTOCK_FLD + "=   ?" + ","
					+ ITM_ProductTable.AGCID_FLD + "=   ?" + ","
					+ ITM_ProductTable.PARTNAMEVN_FLD + " =?, "
					+ ITM_ProductTable.INVENTORID_FLD + " =?, "
					+ ITM_ProductTable.LICENSEFEE_FLD + " =?, "
					+ ITM_ProductTable.LISTPRICE_FLD + " =?, "
					+ ITM_ProductTable.VENDORCURRENCYID_FLD + " =?, "
					+ ITM_ProductTable.QUANTITYSET_FLD + " =?, "
					+ ITM_ProductTable.TAXCODE_FLD + " =?, "
					+ ITM_ProductTable.MINPRODUCE_FLD + " =?, "
					+ ITM_ProductTable.MAXPRODUCE_FLD + " =?, "
					+ ITM_ProductTable.PRODUCTTYPEID_FLD + " =?, "
					+ ITM_ProductTable.PICTURE_FLD + " =?, "
					+ ITM_ProductTable.SETUPPAIR_FLD + " =?, "
					+ ITM_ProductTable.MAXROUNDUPTOMIN_FLD + " =?, "
					+ ITM_ProductTable.MAXROUNDUPTOMULTIPLE_FLD + " =?, "
					+ ITM_ProductTable.ACADJUSTMENTMASTERID_FLD + " =?, "
					+ ITM_ProductTable.REGISTEREDCODE_FLD + " =?, "
					+ ITM_ProductTable.AVEG_FLD + " =?, "
					+ ITM_ProductTable.MASSORDER_FLD + " =?, "
					+ ITM_ProductTable.STOCKTAKINGCODE_FLD + " =?, "
                    + ITM_ProductTable.ALLOWNEGATIVEQTY_FLD + " =?, "
					+ ITM_ProductTable.ORDERPOINT_FLD + "=  ?"
					+ " WHERE " + ITM_ProductTable.PRODUCTID_FLD + "= ?";


				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.CODE_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[ITM_ProductTable.CODE_FLD].Value = objObject.Code;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.REVISION_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[ITM_ProductTable.REVISION_FLD].Value = objObject.Revision;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.DESCRIPTION_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[ITM_ProductTable.DESCRIPTION_FLD].Value = objObject.Description;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.SETUPDATE_FLD, OleDbType.Date));
				if (objObject.SetupDate == DateTime.MinValue)
				{
					ocmdPCS.Parameters[ITM_ProductTable.SETUPDATE_FLD].Value = DBNull.Value;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.SETUPDATE_FLD].Value = objObject.SetupDate;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.VAT_FLD, OleDbType.Decimal));
				if (objObject.VAT >= 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.VAT_FLD].Value = objObject.VAT;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.VAT_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.IMPORTTAX_FLD, OleDbType.Decimal));
				if (objObject.ImportTax >= 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.IMPORTTAX_FLD].Value = objObject.ImportTax;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.IMPORTTAX_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.EXPORTTAX_FLD, OleDbType.Decimal));
				if (objObject.ExportTax >= 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.EXPORTTAX_FLD].Value = objObject.ExportTax;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.EXPORTTAX_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.SPECIALTAX_FLD, OleDbType.Decimal));
				if (objObject.SpecialTax >= 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.SPECIALTAX_FLD].Value = objObject.SpecialTax;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.SPECIALTAX_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.MAKEITEM_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[ITM_ProductTable.MAKEITEM_FLD].Value = objObject.MakeItem;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.PARTNUMBER_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[ITM_ProductTable.PARTNUMBER_FLD].Value = objObject.PartNumber;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.OTHERINFO1_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[ITM_ProductTable.OTHERINFO1_FLD].Value = objObject.OtherInfo1;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.OTHERINFO2_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[ITM_ProductTable.OTHERINFO2_FLD].Value = objObject.OtherInfo2;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.LENGTH_FLD, OleDbType.Decimal));
				if (objObject.Length >= 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.LENGTH_FLD].Value = objObject.Length;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.LENGTH_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.WIDTH_FLD, OleDbType.Decimal));
				if (objObject.Width >= 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.WIDTH_FLD].Value = objObject.Width;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.WIDTH_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.HEIGHT_FLD, OleDbType.Decimal));
				if (objObject.Height >= 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.HEIGHT_FLD].Value = objObject.Height;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.HEIGHT_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.WEIGHT_FLD, OleDbType.Decimal));
				if (objObject.Weight >= 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.WEIGHT_FLD].Value = objObject.Weight;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.WEIGHT_FLD].Value = DBNull.Value;
				}


				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.FINISHEDGOODS_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[ITM_ProductTable.FINISHEDGOODS_FLD].Value = objObject.FinishedGoods;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.SHELFLIFE_FLD, OleDbType.Decimal));
				if (objObject.ShelfLife >= 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.SHELFLIFE_FLD].Value = objObject.ShelfLife;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.SHELFLIFE_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.LOTCONTROL_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[ITM_ProductTable.LOTCONTROL_FLD].Value = objObject.LotControl;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.QASTATUS_FLD, OleDbType.Integer));
				if (objObject.QAStatus == 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.QASTATUS_FLD].Value = DBNull.Value;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.QASTATUS_FLD].Value = objObject.QAStatus;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.STOCK_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[ITM_ProductTable.STOCK_FLD].Value = objObject.Stock;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.PLANTYPE_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[ITM_ProductTable.PLANTYPE_FLD].Value = objObject.PlanType;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.AUTOCONVERSION_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[ITM_ProductTable.AUTOCONVERSION_FLD].Value = objObject.AutoConversion;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.ORDERQUANTITY_FLD, OleDbType.Decimal));
				if (objObject.OrderQuantity >= 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.ORDERQUANTITY_FLD].Value = objObject.OrderQuantity;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.ORDERQUANTITY_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.LTREQUISITION_FLD, OleDbType.Decimal));
				if (objObject.LTRequisition >= 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.LTREQUISITION_FLD].Value = objObject.LTRequisition;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.LTREQUISITION_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.LTSAFETYSTOCK_FLD, OleDbType.Decimal));
				if (objObject.LTSafetyStock >= 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.LTSAFETYSTOCK_FLD].Value = objObject.LTSafetyStock;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.LTSAFETYSTOCK_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.ORDERQUANTITYMULTIPLE_FLD, OleDbType.Decimal));
				if (objObject.OrderQuantityMultiple >= 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.ORDERQUANTITYMULTIPLE_FLD].Value = objObject.OrderQuantityMultiple;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.ORDERQUANTITYMULTIPLE_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.SCRAPPERCENT_FLD, OleDbType.Decimal));
				if (objObject.ScrapPercent >= 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.SCRAPPERCENT_FLD].Value = objObject.ScrapPercent;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.SCRAPPERCENT_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.MINIMUMSTOCK_FLD, OleDbType.Decimal));
				if (objObject.MinimumStock >= 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.MINIMUMSTOCK_FLD].Value = objObject.MinimumStock;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.MINIMUMSTOCK_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.MAXIMUMSTOCK_FLD, OleDbType.Decimal));
				if (objObject.MaximumStock >= 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.MAXIMUMSTOCK_FLD].Value = objObject.MaximumStock;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.MAXIMUMSTOCK_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.CONVERSIONTOLERANCE_FLD, OleDbType.Decimal));
				if (objObject.ConversionTolerance >= 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.CONVERSIONTOLERANCE_FLD].Value = objObject.ConversionTolerance;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.CONVERSIONTOLERANCE_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.VOUCHERTOLERANCE_FLD, OleDbType.Decimal));
				if (objObject.VoucherTolerance >= 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.VOUCHERTOLERANCE_FLD].Value = objObject.VoucherTolerance;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.VOUCHERTOLERANCE_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.RECEIVETOLERANCE_FLD, OleDbType.Decimal));
				if (objObject.ReceiveTolerance >= 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.RECEIVETOLERANCE_FLD].Value = objObject.ReceiveTolerance;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.RECEIVETOLERANCE_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.ISSUESIZE_FLD, OleDbType.Decimal));
				if (objObject.IssueSize >= 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.ISSUESIZE_FLD].Value = objObject.IssueSize;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.ISSUESIZE_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.LTFIXEDTIME_FLD, OleDbType.Decimal));
				if (objObject.LTFixedTime >= 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.LTFIXEDTIME_FLD].Value = objObject.LTFixedTime;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.LTFIXEDTIME_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.LTVARIABLETIME_FLD, OleDbType.Decimal));
				if (objObject.LTVariableTime >= 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.LTVARIABLETIME_FLD].Value = objObject.LTVariableTime;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.LTVARIABLETIME_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.LTDOCKTOSTOCK_FLD, OleDbType.Decimal));
				if (objObject.LTDocToStock >= 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.LTDOCKTOSTOCK_FLD].Value = objObject.LTDocToStock;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.LTDOCKTOSTOCK_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.LTORDERPREPARE_FLD, OleDbType.Decimal));
				if (objObject.LTOrderPrepare >= 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.LTORDERPREPARE_FLD].Value = objObject.LTOrderPrepare;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.LTORDERPREPARE_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.LTSHIPPINGPREPARE_FLD, OleDbType.Decimal));
				if (objObject.LTShippingPrepare >= 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.LTSHIPPINGPREPARE_FLD].Value = objObject.LTShippingPrepare;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.LTSHIPPINGPREPARE_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.LTSALESATP_FLD, OleDbType.Decimal));
				if (objObject.LTSalesATP >= 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.LTSALESATP_FLD].Value = objObject.LTSalesATP;
				}
				else

				{
					ocmdPCS.Parameters[ITM_ProductTable.LTSALESATP_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.SHIPTOLERANCEID_FLD, OleDbType.Integer));
				if (objObject.ShipToleranceID == 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.SHIPTOLERANCEID_FLD].Value = DBNull.Value;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.SHIPTOLERANCEID_FLD].Value = objObject.ShipToleranceID;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.BUYERID_FLD, OleDbType.Integer));
				if (objObject.BuyerID == 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.BUYERID_FLD].Value = DBNull.Value;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.BUYERID_FLD].Value = objObject.BuyerID;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.BOMDESCRIPTION_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[ITM_ProductTable.BOMDESCRIPTION_FLD].Value = objObject.BOMDescription;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.BOMINCREMENT_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[ITM_ProductTable.BOMINCREMENT_FLD].Value = objObject.BomIncrement;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.ROUTINGDESCRIPTION_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[ITM_ProductTable.ROUTINGDESCRIPTION_FLD].Value = objObject.RoutingDescription;

				/*
				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.CREATEDATETIME_FLD, OleDbType.Date));
				ocmdPCS.Parameters[ITM_ProductTable.CREATEDATETIME_FLD].Value = objObject.CreateDateTime;
				*/

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.UPDATEDATETIME_FLD, OleDbType.Date));
				ocmdPCS.Parameters[ITM_ProductTable.UPDATEDATETIME_FLD].Value = objObject.UpdateDateTime;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.COSTMETHOD_FLD, OleDbType.Integer));
				if (objObject.CostMethod >= 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.COSTMETHOD_FLD].Value = objObject.CostMethod;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.COSTMETHOD_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.ROUTINGINCREMENT_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[ITM_ProductTable.ROUTINGINCREMENT_FLD].Value = objObject.RoutingIncrement;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.CCNID_FLD, OleDbType.Integer));
				if (objObject.CCNID == 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.CCNID_FLD].Value = DBNull.Value;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.CCNID_FLD].Value = objObject.CCNID;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.CATEGORYID_FLD, OleDbType.Integer));
				if (objObject.CategoryID == 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.CATEGORYID_FLD].Value = DBNull.Value;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.CATEGORYID_FLD].Value = objObject.CategoryID;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.COSTCENTERID_FLD, OleDbType.Integer));
				if (objObject.CostCenterID == 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.COSTCENTERID_FLD].Value = DBNull.Value;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.COSTCENTERID_FLD].Value = objObject.CostCenterID;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.DELETEREASONID_FLD, OleDbType.Integer));
				if (objObject.DeleteReasonID == 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.DELETEREASONID_FLD].Value = DBNull.Value;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.DELETEREASONID_FLD].Value = objObject.DeleteReasonID;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.DELIVERYPOLICYID_FLD, OleDbType.Integer));
				if (objObject.DeliveryPolicyID == 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.DELIVERYPOLICYID_FLD].Value = DBNull.Value;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.DELIVERYPOLICYID_FLD].Value = objObject.DeliveryPolicyID;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.FORMATCODEID_FLD, OleDbType.Integer));
				if (objObject.FormatCodeID == 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.FORMATCODEID_FLD].Value = DBNull.Value;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.FORMATCODEID_FLD].Value = objObject.FormatCodeID;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.FREIGHTCLASSID_FLD, OleDbType.Integer));
				if (objObject.FreightClassID == 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.FREIGHTCLASSID_FLD].Value = DBNull.Value;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.FREIGHTCLASSID_FLD].Value = objObject.FreightClassID;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.HAZARDID_FLD, OleDbType.Integer));
				if (objObject.HazardID == 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.HAZARDID_FLD].Value = DBNull.Value;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.HAZARDID_FLD].Value = objObject.HazardID;
				}
				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.ORDERPOLICYID_FLD, OleDbType.Integer));
				if (objObject.OrderPolicyID == 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.ORDERPOLICYID_FLD].Value = DBNull.Value;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.ORDERPOLICYID_FLD].Value = objObject.OrderPolicyID;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.ORDERRULEID_FLD, OleDbType.Integer));
				if (objObject.OrderRuleID == 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.ORDERRULEID_FLD].Value = DBNull.Value;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.ORDERRULEID_FLD].Value = objObject.OrderRuleID;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.SOURCEID_FLD, OleDbType.Integer));
				if (objObject.SourceID == 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.SOURCEID_FLD].Value = DBNull.Value;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.SOURCEID_FLD].Value = objObject.SourceID;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.STOCKUMID_FLD, OleDbType.Integer));
				if (objObject.StockUMID == 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.STOCKUMID_FLD].Value = DBNull.Value;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.STOCKUMID_FLD].Value = objObject.StockUMID;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.SELLINGUMID_FLD, OleDbType.Integer));
				if (objObject.SellingUMID == 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.SELLINGUMID_FLD].Value = DBNull.Value;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.SELLINGUMID_FLD].Value = objObject.SellingUMID;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.HEIGHTUMID_FLD, OleDbType.Integer));
				if (objObject.HeightUMID != 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.HEIGHTUMID_FLD].Value = objObject.HeightUMID;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.HEIGHTUMID_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.WIDTHUMID_FLD, OleDbType.Integer));
				if (objObject.WidthUMID != 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.WIDTHUMID_FLD].Value = objObject.WidthUMID;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.WIDTHUMID_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.LENGTHUMID_FLD, OleDbType.Integer));
				if (objObject.LengthUMID != 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.LENGTHUMID_FLD].Value = objObject.LengthUMID;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.LENGTHUMID_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.BUYINGUMID_FLD, OleDbType.Integer));
				if (objObject.BuyingUMID != 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.BUYINGUMID_FLD].Value = objObject.BuyingUMID;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.BUYINGUMID_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.WEIGHTUMID_FLD, OleDbType.Integer));
				if (objObject.WeightUMID != 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.WEIGHTUMID_FLD].Value = objObject.WeightUMID;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.WEIGHTUMID_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.LOTSIZE_FLD, OleDbType.Integer));
				if (objObject.LotSize >= 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.LOTSIZE_FLD].Value = objObject.LotSize;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.LOTSIZE_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.MASTERLOCATIONID_FLD, OleDbType.Integer));
				if (objObject.MasterLocationID != 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.MASTERLOCATIONID_FLD].Value = objObject.MasterLocationID;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.MASTERLOCATIONID_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.LOCATIONID_FLD, OleDbType.Integer));
				if (objObject.LocationID != 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.LOCATIONID_FLD].Value = objObject.LocationID;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.LOCATIONID_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.BINID_FLD, OleDbType.Integer));

				if (objObject.BinID != 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.BINID_FLD].Value = objObject.BinID;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.BINID_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.PRIMARYVENDORID_FLD, OleDbType.Integer));
				if (objObject.PrimaryVendorID != 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.PRIMARYVENDORID_FLD].Value = objObject.PrimaryVendorID;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.PRIMARYVENDORID_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.VENDORLOCATIONID_FLD, OleDbType.Integer));
				if (objObject.VendorLocationID != 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.VENDORLOCATIONID_FLD].Value = objObject.VendorLocationID;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.VENDORLOCATIONID_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.SAFETYSTOCK_FLD, OleDbType.Decimal));
				if (objObject.SafetyStock >= 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.SAFETYSTOCK_FLD].Value = objObject.SafetyStock;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.SAFETYSTOCK_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.AGCID_FLD, OleDbType.Integer));

				if (objObject.AGCID > 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.AGCID_FLD].Value = objObject.AGCID;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.AGCID_FLD].Value = DBNull.Value;
				}

				//Begin_ Added by Tuan TQ
				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.PARTNAMEVN_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[ITM_ProductTable.PARTNAMEVN_FLD].Value = objObject.PartNameVN;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.INVENTORID_FLD, OleDbType.Integer));
				if (objObject.Inventor > 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.INVENTORID_FLD].Value = objObject.Inventor;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.INVENTORID_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.LICENSEFEE_FLD, OleDbType.Decimal));
				if (objObject.LicenseFee >= 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.LICENSEFEE_FLD].Value = objObject.LicenseFee;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.LICENSEFEE_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.LISTPRICE_FLD, OleDbType.Decimal));
				if (objObject.ListPrice >= 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.LISTPRICE_FLD].Value = objObject.ListPrice;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.LISTPRICE_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.VENDORCURRENCYID_FLD, OleDbType.Integer));
				if (objObject.VendorCurrencyID > 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.VENDORCURRENCYID_FLD].Value = objObject.VendorCurrencyID;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.VENDORCURRENCYID_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.QUANTITYSET_FLD, OleDbType.Decimal));
				if (objObject.QuantitySet >= 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.QUANTITYSET_FLD].Value = objObject.QuantitySet;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.QUANTITYSET_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.TAXCODE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[ITM_ProductTable.TAXCODE_FLD].Value = objObject.TaxCode;

				//Added:  01 Mar, 2006
				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.MINPRODUCE_FLD, OleDbType.Decimal));
				if (objObject.MinProduce >= 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.MINPRODUCE_FLD].Value = objObject.MinProduce;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.MINPRODUCE_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.MAXPRODUCE_FLD, OleDbType.Decimal));
				if (objObject.MaxProduce >= 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.MAXPRODUCE_FLD].Value = objObject.MaxProduce;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.MAXPRODUCE_FLD].Value = DBNull.Value;
				}
				//End

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.PRODUCTTYPEID_FLD, OleDbType.Integer));
				if (objObject.ProductTypeId > 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.PRODUCTTYPEID_FLD].Value = objObject.ProductTypeId;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.PRODUCTTYPEID_FLD].Value = DBNull.Value;
				}
				//End_ Added by Tuan TQ

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.PICTURE_FLD, OleDbType.Binary));
				if (objObject.Picture != null)
				{
					// convert bitmap to byte array in order to store to db
					Bitmap image = objObject.Picture;
					MemoryStream stream = new MemoryStream();
					image.Save(stream, ImageFormat.Bmp);
					byte[] bytContent = stream.ToArray();
					ocmdPCS.Parameters[ITM_ProductTable.PICTURE_FLD].Value = bytContent;
					ocmdPCS.Parameters[ITM_ProductTable.PICTURE_FLD].Size = bytContent.Length;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.PICTURE_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.SETUPPAIR_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[ITM_ProductTable.SETUPPAIR_FLD].Value = objObject.SetUpPair;

				//HACK: added by Tuan TQ. 17 May, 2006
				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.MAXROUNDUPTOMIN_FLD, OleDbType.Decimal));
				if (objObject.MaxRoundUpToMin >= 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.MAXROUNDUPTOMIN_FLD].Value = objObject.MaxRoundUpToMin;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.MAXROUNDUPTOMIN_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.MAXROUNDUPTOMULTIPLE_FLD, OleDbType.Decimal));
				if (objObject.MaxRoundUpToMultiple >= 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.MAXROUNDUPTOMULTIPLE_FLD].Value = objObject.MaxRoundUpToMultiple;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.MAXROUNDUPTOMULTIPLE_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.ACADJUSTMENTMASTERID_FLD, OleDbType.Integer));
				if (objObject.ACAdjustmentMasterID > 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.ACADJUSTMENTMASTERID_FLD].Value = objObject.ACAdjustmentMasterID;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.ACADJUSTMENTMASTERID_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.REGISTEREDCODE_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[ITM_ProductTable.REGISTEREDCODE_FLD].Value = objObject.RegisteredCode;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.AVEG_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[ITM_ProductTable.AVEG_FLD].Value = objObject.AVEG;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.MASSORDER_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[ITM_ProductTable.MASSORDER_FLD].Value = objObject.MassOrder;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.STOCKTAKINGCODE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[ITM_ProductTable.STOCKTAKINGCODE_FLD].Value = objObject.StockTakingCode;
				//End hack
                ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.ALLOWNEGATIVEQTY_FLD, OleDbType.Boolean));
                ocmdPCS.Parameters[ITM_ProductTable.ALLOWNEGATIVEQTY_FLD].Value = objObject.AllowNegativeQty;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.ORDERPOINT_FLD, OleDbType.Decimal));
				if (objObject.OrderPoint >= 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.ORDERPOINT_FLD].Value = objObject.OrderPoint;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.ORDERPOINT_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.PRODUCTID_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[ITM_ProductTable.PRODUCTID_FLD].Value = objObject.ProductID;

				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();
				ocmdPCS.ExecuteNonQuery();
			}
			catch (OleDbException ex)
			{
				if (ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_KEYCODE
					|| ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_UNIQUE_KEYCODE)
				{
					throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);
				}
				else if (ex.Errors[1].NativeError == ErrorCode.SQL_ARITHMETRIC_OVERFLOW)
				{
					throw new PCSDBException(ErrorCode.ERROR_NUMBER_OVERFLOW, METHOD_NAME, ex);
				}
				else
				{
					throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
				}
			}

			catch (InvalidOperationException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}
			catch (Exception ex)
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}
			finally
			{
				if (oconPCS != null)
				{
					if (oconPCS.State != ConnectionState.Closed)
					{
						oconPCS.Close();
					}
				}
			}

		}


		//**************************************************************************              
		///    <Description>
		///       This method uses to get all data from ITM_Product
		///    </Description>
		///    <Inputs>
		///               
		///    </Inputs>
		///    <Outputs>
		///       DataSet
		///    </Outputs>
		///    <Returns>
		///       DataSet
		///    </Returns>
		///    <Authors>
		///       HungLa
		///    </Authors>
		///    <History>
		///       Tuesday, January 25, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public DataSet List()
		{
			const string METHOD_NAME = THIS + ".List()";
			DataSet dstPCS = new DataSet();

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;

				strSql = "SELECT "
					+ ITM_ProductTable.PRODUCTID_FLD + ","
					+ ITM_ProductTable.CODE_FLD + ","
					+ ITM_ProductTable.REVISION_FLD + ","
					+ ITM_ProductTable.DESCRIPTION_FLD + ","
					+ ITM_ProductTable.SETUPDATE_FLD + ","
					+ ITM_ProductTable.VAT_FLD + ","
					+ ITM_ProductTable.IMPORTTAX_FLD + ","
					+ ITM_ProductTable.EXPORTTAX_FLD + ","
					+ ITM_ProductTable.SPECIALTAX_FLD + ","
					+ ITM_ProductTable.MAKEITEM_FLD + ","
					+ ITM_ProductTable.PARTNUMBER_FLD + ","
					+ ITM_ProductTable.OTHERINFO1_FLD + ","
					+ ITM_ProductTable.OTHERINFO2_FLD + ","
					+ ITM_ProductTable.LENGTH_FLD + ","
					+ ITM_ProductTable.WIDTH_FLD + ","
					+ ITM_ProductTable.HEIGHT_FLD + ","
					+ ITM_ProductTable.WEIGHT_FLD + ","
					+ ITM_ProductTable.FINISHEDGOODS_FLD + ","
					+ ITM_ProductTable.SHELFLIFE_FLD + ","
					+ ITM_ProductTable.LOTCONTROL_FLD + ","
					+ ITM_ProductTable.QASTATUS_FLD + ","
					+ ITM_ProductTable.STOCK_FLD + ","
					+ ITM_ProductTable.PLANTYPE_FLD + ","
					+ ITM_ProductTable.AUTOCONVERSION_FLD + ","
					+ ITM_ProductTable.ORDERQUANTITY_FLD + ","
					+ ITM_ProductTable.LTREQUISITION_FLD + ","
					+ ITM_ProductTable.LTSAFETYSTOCK_FLD + ","
					+ ITM_ProductTable.ORDERQUANTITYMULTIPLE_FLD + ","
					+ ITM_ProductTable.SCRAPPERCENT_FLD + ","
					+ ITM_ProductTable.MINIMUMSTOCK_FLD + ","
					+ ITM_ProductTable.MAXIMUMSTOCK_FLD + ","
					+ ITM_ProductTable.CONVERSIONTOLERANCE_FLD + ","
					+ ITM_ProductTable.VOUCHERTOLERANCE_FLD + ","
					+ ITM_ProductTable.RECEIVETOLERANCE_FLD + ","
					+ ITM_ProductTable.ISSUESIZE_FLD + ","
					+ ITM_ProductTable.LTFIXEDTIME_FLD + ","
					+ ITM_ProductTable.LTVARIABLETIME_FLD + ","
					+ ITM_ProductTable.LTDOCKTOSTOCK_FLD + ","
					+ ITM_ProductTable.LTORDERPREPARE_FLD + ","
					+ ITM_ProductTable.LTSHIPPINGPREPARE_FLD + ","
					+ ITM_ProductTable.LTSALESATP_FLD + ","
					+ ITM_ProductTable.SHIPTOLERANCEID_FLD + ","
					+ ITM_ProductTable.BUYERID_FLD + ","
					+ ITM_ProductTable.BOMDESCRIPTION_FLD + ","
					+ ITM_ProductTable.BOMINCREMENT_FLD + ","
					+ ITM_ProductTable.ROUTINGDESCRIPTION_FLD + ","
					+ ITM_ProductTable.CREATEDATETIME_FLD + ","
					+ ITM_ProductTable.UPDATEDATETIME_FLD + ","
					+ ITM_ProductTable.COSTMETHOD_FLD + ","
					+ ITM_ProductTable.ROUTINGINCREMENT_FLD + ","
					+ ITM_ProductTable.CCNID_FLD + ","
					+ ITM_ProductTable.CATEGORYID_FLD + ","
					+ ITM_ProductTable.COSTCENTERID_FLD + ","
					+ ITM_ProductTable.DELETEREASONID_FLD + ","
					+ ITM_ProductTable.DELIVERYPOLICYID_FLD + ","
					+ ITM_ProductTable.FORMATCODEID_FLD + ","
					+ ITM_ProductTable.FREIGHTCLASSID_FLD + ","
					+ ITM_ProductTable.HAZARDID_FLD + ","
					+ ITM_ProductTable.ORDERPOLICYID_FLD + ","
					+ ITM_ProductTable.ORDERRULEID_FLD + ","
					+ ITM_ProductTable.SOURCEID_FLD + ","
					+ ITM_ProductTable.STOCKUMID_FLD + ","
					+ ITM_ProductTable.SELLINGUMID_FLD + ","
					+ ITM_ProductTable.HEIGHTUMID_FLD + ","
					+ ITM_ProductTable.WIDTHUMID_FLD + ","
					+ ITM_ProductTable.LENGTHUMID_FLD + ","
					+ ITM_ProductTable.BUYINGUMID_FLD + ","
					+ ITM_ProductTable.WEIGHTUMID_FLD + ","
					+ ITM_ProductTable.LOTSIZE_FLD + ","
					+ ITM_ProductTable.MASTERLOCATIONID_FLD + ","
					+ ITM_ProductTable.LOCATIONID_FLD + ","
					+ ITM_ProductTable.BINID_FLD + ","
					+ ITM_ProductTable.PRIMARYVENDORID_FLD + ","
					+ ITM_ProductTable.VENDORLOCATIONID_FLD + ","
					+ ITM_ProductTable.SAFETYSTOCK_FLD + ","
					+ ITM_ProductTable.AGCID_FLD + ","
					+ ITM_ProductTable.PARTNAMEVN_FLD + ","
					+ ITM_ProductTable.INVENTORID_FLD + ","
					+ ITM_ProductTable.LICENSEFEE_FLD + ","
					+ ITM_ProductTable.LISTPRICE_FLD + ","
					+ ITM_ProductTable.VENDORCURRENCYID_FLD + ","
					+ ITM_ProductTable.QUANTITYSET_FLD + ","
					+ ITM_ProductTable.TAXCODE_FLD + ","
					+ ITM_ProductTable.MINPRODUCE_FLD + ","
					+ ITM_ProductTable.MAXPRODUCE_FLD + ","
					+ ITM_ProductTable.PRODUCTTYPEID_FLD + ", "
					+ ITM_ProductTable.MAXROUNDUPTOMIN_FLD + ", "
					+ ITM_ProductTable.MAXROUNDUPTOMULTIPLE_FLD + ", "
					+ ITM_ProductTable.ACADJUSTMENTMASTERID_FLD + ", "
					+ ITM_ProductTable.REGISTEREDCODE_FLD + ", "
					+ ITM_ProductTable.ORDERPOINT_FLD
					+ " FROM " + ITM_ProductTable.TABLE_NAME;
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, ITM_ProductTable.TABLE_NAME);

				return dstPCS;
			}
			catch (OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}
			catch (Exception ex)
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}

			finally
			{
				if (oconPCS != null)
				{
					if (oconPCS.State != ConnectionState.Closed)
					{
						oconPCS.Close();
					}
				}
			}
		}

		//**************************************************************************              
		///    <Description>
		///       This method uses to get all data from ITM_Product
		///    </Description>
		///    <Inputs>
		///               
		///    </Inputs>
		///    <Outputs>
		///       DataSet
		///    </Outputs>
		///    <Returns>
		///       DataSet
		///    </Returns>
		///    <Authors>
		///       HungLa
		///    </Authors>
		///    <History>
		///       Tuesday, January 25, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public DataTable ListForCombo()
		{
			const string METHOD_NAME = THIS + ".ListForCombo()";
			DataSet dstPCS = new DataSet();

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = "SELECT "
					+ ITM_ProductTable.PRODUCTID_FLD + ","
					+ ITM_ProductTable.CODE_FLD + ","
					+ ITM_ProductTable.REVISION_FLD + ","
					+ ITM_ProductTable.DESCRIPTION_FLD + ","
					+ ITM_ProductTable.STOCKUMID_FLD + ","
					+ ITM_ProductTable.BUYINGUMID_FLD + ","
					+ ITM_ProductTable.SELLINGUMID_FLD
					+ " FROM " + ITM_ProductTable.TABLE_NAME;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, ITM_ProductTable.TABLE_NAME);

				return dstPCS.Tables[0];
			}
			catch (OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}
			catch (Exception ex)
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}

			finally
			{
				if (oconPCS != null)
				{
					if (oconPCS.State != ConnectionState.Closed)
					{
						oconPCS.Close();
					}
				}
			}
		}

		/// <summary>
		/// LoadItemInfo
		/// </summary>
		/// <param name="pintProductID"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Thursday, June 23 2005</date>
		public DataTable LoadItemInfo(int pintProductID)
		{
			const string METHOD_NAME = THIS + ".ListForCombo()";
			const string PRO = "Product";
			const string CAPTION_UM = "UM";
			const string UM = "UnitOfMeasure";
			DataSet dstPCS = new DataSet();

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = "SELECT "
					+ PRO + "." + ITM_ProductTable.CODE_FLD + ","
					+ PRO + "." + ITM_ProductTable.REVISION_FLD + ","
					+ PRO + "." + ITM_ProductTable.DESCRIPTION_FLD + ","
					+ UM + "." + MST_UnitOfMeasureTable.CODE_FLD + Constants.WHITE_SPACE + CAPTION_UM
					+ " FROM " + ITM_ProductTable.TABLE_NAME + Constants.WHITE_SPACE + PRO
					+ " INNER JOIN " + MST_UnitOfMeasureTable.TABLE_NAME + Constants.WHITE_SPACE + UM
					+ " ON " + PRO + "." + ITM_ProductTable.STOCKUMID_FLD + " = " + UM + "." + MST_UnitOfMeasureTable.UNITOFMEASUREID_FLD
					+ " WHERE " + ITM_ProductTable.PRODUCTID_FLD + " = " + pintProductID;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, ITM_ProductTable.TABLE_NAME);

				return dstPCS.Tables[0];
			}
			catch (OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}
			catch (Exception ex)
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}

			finally
			{
				if (oconPCS != null)
				{
					if (oconPCS.State != ConnectionState.Closed)
					{
						oconPCS.Close();
					}
				}
			}
		}

		//**************************************************************************              
		///    <Description>
		///       This method uses to update a DataSet
		///    </Description>
		///    <Inputs>
		///        DataSet       
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       HungLa
		///    </Authors>
		///    <History>
		///       Tuesday, January 25, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public void UpdateDataSet(DataSet pData)
		{
			const string METHOD_NAME = THIS + ".UpdateDataSet()";
			string strSql;
			OleDbConnection oconPCS = null;
			OleDbCommandBuilder odcbPCS;
			OleDbDataAdapter odadPCS = new OleDbDataAdapter();

			try
			{
				strSql = "SELECT "
					+ ITM_ProductTable.PRODUCTID_FLD + ","
					+ ITM_ProductTable.CODE_FLD + ","
					+ ITM_ProductTable.REVISION_FLD + ","
					+ ITM_ProductTable.DESCRIPTION_FLD + ","
					+ ITM_ProductTable.SETUPDATE_FLD + ","
					+ ITM_ProductTable.VAT_FLD + ","
					+ ITM_ProductTable.IMPORTTAX_FLD + ","
					+ ITM_ProductTable.EXPORTTAX_FLD + ","
					+ ITM_ProductTable.SPECIALTAX_FLD + ","
					+ ITM_ProductTable.MAKEITEM_FLD + ","
					+ ITM_ProductTable.PARTNUMBER_FLD + ","
					+ ITM_ProductTable.OTHERINFO1_FLD + ","
					+ ITM_ProductTable.OTHERINFO2_FLD + ","
					+ ITM_ProductTable.LENGTH_FLD + ","
					+ ITM_ProductTable.WIDTH_FLD + ","
					+ ITM_ProductTable.HEIGHT_FLD + ","
					+ ITM_ProductTable.WEIGHT_FLD + ","
					+ ITM_ProductTable.FINISHEDGOODS_FLD + ","
					+ ITM_ProductTable.SHELFLIFE_FLD + ","
					+ ITM_ProductTable.LOTCONTROL_FLD + ","
					+ ITM_ProductTable.QASTATUS_FLD + ","
					+ ITM_ProductTable.STOCK_FLD + ","
					+ ITM_ProductTable.PLANTYPE_FLD + ","
					+ ITM_ProductTable.AUTOCONVERSION_FLD + ","
					+ ITM_ProductTable.ORDERQUANTITY_FLD + ","
					+ ITM_ProductTable.LTREQUISITION_FLD + ","
					+ ITM_ProductTable.LTSAFETYSTOCK_FLD + ","
					+ ITM_ProductTable.ORDERQUANTITYMULTIPLE_FLD + ","
					+ ITM_ProductTable.SCRAPPERCENT_FLD + ","
					+ ITM_ProductTable.MINIMUMSTOCK_FLD + ","
					+ ITM_ProductTable.MAXIMUMSTOCK_FLD + ","
					+ ITM_ProductTable.CONVERSIONTOLERANCE_FLD + ","
					+ ITM_ProductTable.VOUCHERTOLERANCE_FLD + ","
					+ ITM_ProductTable.RECEIVETOLERANCE_FLD + ","
					+ ITM_ProductTable.ISSUESIZE_FLD + ","
					+ ITM_ProductTable.LTFIXEDTIME_FLD + ","
					+ ITM_ProductTable.LTVARIABLETIME_FLD + ","
					+ ITM_ProductTable.LTDOCKTOSTOCK_FLD + ","
					+ ITM_ProductTable.LTORDERPREPARE_FLD + ","
					+ ITM_ProductTable.LTSHIPPINGPREPARE_FLD + ","
					+ ITM_ProductTable.LTSALESATP_FLD + ","
					+ ITM_ProductTable.SHIPTOLERANCEID_FLD + ","
					+ ITM_ProductTable.BUYERID_FLD + ","
					+ ITM_ProductTable.BOMDESCRIPTION_FLD + ","
					+ ITM_ProductTable.BOMINCREMENT_FLD + ","
					+ ITM_ProductTable.ROUTINGDESCRIPTION_FLD + ","
					+ ITM_ProductTable.CREATEDATETIME_FLD + ","
					+ ITM_ProductTable.UPDATEDATETIME_FLD + ","
					+ ITM_ProductTable.COSTMETHOD_FLD + ","
					+ ITM_ProductTable.ROUTINGINCREMENT_FLD + ","
					+ ITM_ProductTable.CCNID_FLD + ","
					+ ITM_ProductTable.CATEGORYID_FLD + ","
					+ ITM_ProductTable.COSTCENTERID_FLD + ","
					+ ITM_ProductTable.DELETEREASONID_FLD + ","
					+ ITM_ProductTable.DELIVERYPOLICYID_FLD + ","
					+ ITM_ProductTable.FORMATCODEID_FLD + ","
					+ ITM_ProductTable.FREIGHTCLASSID_FLD + ","
					+ ITM_ProductTable.HAZARDID_FLD + ","
					+ ITM_ProductTable.ORDERPOLICYID_FLD + ","
					+ ITM_ProductTable.ORDERRULEID_FLD + ","
					+ ITM_ProductTable.SOURCEID_FLD + ","
					+ ITM_ProductTable.STOCKUMID_FLD + ","
					+ ITM_ProductTable.SELLINGUMID_FLD + ","
					+ ITM_ProductTable.HEIGHTUMID_FLD + ","
					+ ITM_ProductTable.WIDTHUMID_FLD + ","
					+ ITM_ProductTable.LENGTHUMID_FLD + ","
					+ ITM_ProductTable.BUYINGUMID_FLD + ","
					+ ITM_ProductTable.WEIGHTUMID_FLD + ","
					+ ITM_ProductTable.LOTSIZE_FLD + ","
					+ ITM_ProductTable.MASTERLOCATIONID_FLD + ","
					+ ITM_ProductTable.LOCATIONID_FLD + ","
					+ ITM_ProductTable.BINID_FLD + ","
					+ ITM_ProductTable.PRIMARYVENDORID_FLD + ","
					+ ITM_ProductTable.VENDORLOCATIONID_FLD + ","
					+ ITM_ProductTable.SAFETYSTOCK_FLD + ","
					+ ITM_ProductTable.AGCID_FLD + ","
					+ ITM_ProductTable.PARTNAMEVN_FLD + ","
					+ ITM_ProductTable.INVENTORID_FLD + ","
					+ ITM_ProductTable.LICENSEFEE_FLD + ","
					+ ITM_ProductTable.LISTPRICE_FLD + ","
					+ ITM_ProductTable.VENDORCURRENCYID_FLD + ","
					+ ITM_ProductTable.QUANTITYSET_FLD + ","
					+ ITM_ProductTable.TAXCODE_FLD + ","
					+ ITM_ProductTable.MINPRODUCE_FLD + ","
					+ ITM_ProductTable.MAXPRODUCE_FLD + ","
					+ ITM_ProductTable.PRODUCTTYPEID_FLD + ", "
					+ ITM_ProductTable.MAXROUNDUPTOMIN_FLD + ", "
					+ ITM_ProductTable.MAXROUNDUPTOMULTIPLE_FLD + ", "
					+ ITM_ProductTable.ACADJUSTMENTMASTERID_FLD + ", "
					+ ITM_ProductTable.REGISTEREDCODE_FLD + ", "
					+ ITM_ProductTable.ORDERPOINT_FLD
					+ "  FROM " + ITM_ProductTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				odadPCS.SelectCommand = new OleDbCommand(strSql, oconPCS);
				odcbPCS = new OleDbCommandBuilder(odadPCS);
				pData.EnforceConstraints = false;
				odadPCS.Update(pData, ITM_ProductTable.TABLE_NAME);

			}
			catch (OleDbException ex)
			{
				if (ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_KEYCODE)
				{
					throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);
				}
				else if (ex.Errors[1].NativeError == ErrorCode.SQLCASCADE_PREVENT_KEYCODE)
				{
					throw new PCSDBException(ErrorCode.CASCADE_DELETE_PREVENT, METHOD_NAME, ex);
				}

				else
				{
					throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
				}
			}

			catch (InvalidOperationException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}
			catch (Exception ex)
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}

			finally
			{
				if (oconPCS != null)
				{
					if (oconPCS.State != ConnectionState.Closed)
					{
						oconPCS.Close();
					}
				}
			}
		}

		public string GetACAdjustCodeByID(int pintID)
		{
			const string METHOD_NAME = THIS + ".GetACAdjustCodeByID()";

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = "SELECT "
					+ cst_ACAdjustmentMasterTable.CODE_FLD
					+ " FROM " + cst_ACAdjustmentMasterTable.TABLE_NAME
					+ " WHERE " + cst_ACAdjustmentMasterTable.ACADJUSTMENTMASTERID_FLD + " = " + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				object objValue = ocmdPCS.ExecuteScalar();

				if (objValue != null)
				{
					return objValue.ToString();
				}

				return string.Empty;
			}
			catch (OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}

			catch (Exception ex)
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}

			finally
			{
				if (oconPCS != null)
				{
					if (oconPCS.State != ConnectionState.Closed)
					{
						oconPCS.Close();
					}
				}
			}
			return string.Empty;
		}


		public string GetProductCodeByID(int pintID)
		{
			const string METHOD_NAME = THIS + ".GetObjectVO()";
			DataSet dstPCS = new DataSet();

			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = "SELECT "
					+ ITM_ProductTable.CODE_FLD
					+ " FROM " + ITM_ProductTable.TABLE_NAME
					+ " WHERE " + ITM_ProductTable.PRODUCTID_FLD + "=" + pintID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				ITM_ProductVO objObject = new ITM_ProductVO();

				while (odrPCS.Read())
				{
					objObject.Code = odrPCS[ITM_ProductTable.CODE_FLD].ToString().Trim();
					return objObject.Code;
				}
			}
			catch (OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}

			catch (Exception ex)
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}

			finally
			{
				if (oconPCS != null)
				{
					if (oconPCS.State != ConnectionState.Closed)
					{
						oconPCS.Close();
					}
				}
			}
			return string.Empty;
		}

		//**************************************************************************              
		///    <Description>
		///       Return the Database Date
		///    </Description>
		///    <Inputs>
		///              
		///    </Inputs>
		///    <Outputs>
		///       Database Date
		///    </Outputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <Authors>
		///       HungLa
		///    </Authors>
		///    <History>
		///       Tuesday, January 25, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		private DateTime GetDatabaseDate()
		{
			const string METHOD_NAME = THIS + ".GetSystemDate()";

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				strSql = " SELECT  getdate() ";
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				return (DateTime) ocmdPCS.ExecuteScalar();

			}
			catch (OleDbException ex)
			{
				if (ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_KEYCODE)
				{
					throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);
				}
				else
				{
					throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
				}
			}

			catch (InvalidOperationException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}
			catch (Exception ex)
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}

			finally
			{
				if (oconPCS != null)
				{
					if (oconPCS.State != ConnectionState.Closed)
					{
						oconPCS.Close();
					}
				}
			}

		}

		//**************************************************************************              
		///    <Description>
		///       This method uses to add data to ITM_Product
		///    </Description>
		///    <Inputs>
		///        ITM_ProductVO       
		///    </Inputs>
		///    <Outputs>
		///       newly inserted primarkey value
		///    </Outputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <Authors>
		///       HungLa
		///    </Authors>
		///    <History>
		///       Tuesday, January 25, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public int AddAndReturnID(object pobjObjectVO)
		{
			const string METHOD_NAME = THIS + ".Add()";

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				ITM_ProductVO objObject = (ITM_ProductVO) pobjObjectVO;

				//Get the Database Date
				objObject.CreateDateTime = GetDatabaseDate();

				string strSql = String.Empty;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand("", oconPCS);

				strSql = "INSERT INTO ITM_Product("
					+ ITM_ProductTable.CODE_FLD + ","
					+ ITM_ProductTable.REVISION_FLD + ","
					+ ITM_ProductTable.DESCRIPTION_FLD + ","
					+ ITM_ProductTable.SETUPDATE_FLD + ","
					+ ITM_ProductTable.VAT_FLD + ","
					+ ITM_ProductTable.IMPORTTAX_FLD + ","
					+ ITM_ProductTable.EXPORTTAX_FLD + ","
					+ ITM_ProductTable.SPECIALTAX_FLD + ","
					+ ITM_ProductTable.MAKEITEM_FLD + ","
					+ ITM_ProductTable.PARTNUMBER_FLD + ","
					+ ITM_ProductTable.OTHERINFO1_FLD + ","
					+ ITM_ProductTable.OTHERINFO2_FLD + ","
					+ ITM_ProductTable.LENGTH_FLD + ","
					+ ITM_ProductTable.WIDTH_FLD + ","
					+ ITM_ProductTable.HEIGHT_FLD + ","
					+ ITM_ProductTable.WEIGHT_FLD + ","
					+ ITM_ProductTable.FINISHEDGOODS_FLD + ","
					+ ITM_ProductTable.SHELFLIFE_FLD + ","
					+ ITM_ProductTable.LOTCONTROL_FLD + ","
					+ ITM_ProductTable.QASTATUS_FLD + ","
					+ ITM_ProductTable.STOCK_FLD + ","
					+ ITM_ProductTable.PLANTYPE_FLD + ","
					+ ITM_ProductTable.AUTOCONVERSION_FLD + ","
					+ ITM_ProductTable.ORDERQUANTITY_FLD + ","
					+ ITM_ProductTable.LTREQUISITION_FLD + ","
					+ ITM_ProductTable.LTSAFETYSTOCK_FLD + ","
					+ ITM_ProductTable.ORDERQUANTITYMULTIPLE_FLD + ","
					+ ITM_ProductTable.SCRAPPERCENT_FLD + ","
					+ ITM_ProductTable.MINIMUMSTOCK_FLD + ","
					+ ITM_ProductTable.MAXIMUMSTOCK_FLD + ","
					+ ITM_ProductTable.CONVERSIONTOLERANCE_FLD + ","
					+ ITM_ProductTable.VOUCHERTOLERANCE_FLD + ","
					+ ITM_ProductTable.RECEIVETOLERANCE_FLD + ","
					+ ITM_ProductTable.ISSUESIZE_FLD + ","
					+ ITM_ProductTable.LTFIXEDTIME_FLD + ","
					+ ITM_ProductTable.LTVARIABLETIME_FLD + ","
					+ ITM_ProductTable.LTDOCKTOSTOCK_FLD + ","
					+ ITM_ProductTable.LTORDERPREPARE_FLD + ","
					+ ITM_ProductTable.LTSHIPPINGPREPARE_FLD + ","
					+ ITM_ProductTable.LTSALESATP_FLD + ","
					+ ITM_ProductTable.SHIPTOLERANCEID_FLD + ","
					+ ITM_ProductTable.BUYERID_FLD + ","
					+ ITM_ProductTable.BOMDESCRIPTION_FLD + ","
					+ ITM_ProductTable.BOMINCREMENT_FLD + ","
					+ ITM_ProductTable.ROUTINGDESCRIPTION_FLD + ","
					+ ITM_ProductTable.CREATEDATETIME_FLD + ","
					+ ITM_ProductTable.UPDATEDATETIME_FLD + ","
					+ ITM_ProductTable.COSTMETHOD_FLD + ","
					+ ITM_ProductTable.ROUTINGINCREMENT_FLD + ","
					+ ITM_ProductTable.CCNID_FLD + ","
					+ ITM_ProductTable.CATEGORYID_FLD + ","
					+ ITM_ProductTable.COSTCENTERID_FLD + ","
					+ ITM_ProductTable.DELETEREASONID_FLD + ","
					+ ITM_ProductTable.DELIVERYPOLICYID_FLD + ","
					+ ITM_ProductTable.FORMATCODEID_FLD + ","
					+ ITM_ProductTable.FREIGHTCLASSID_FLD + ","
					+ ITM_ProductTable.HAZARDID_FLD + ","
					+ ITM_ProductTable.ORDERPOLICYID_FLD + ","
					+ ITM_ProductTable.ORDERRULEID_FLD + ","
					+ ITM_ProductTable.SOURCEID_FLD + ","
					+ ITM_ProductTable.STOCKUMID_FLD + ","
					+ ITM_ProductTable.SELLINGUMID_FLD + ","
					+ ITM_ProductTable.HEIGHTUMID_FLD + ","
					+ ITM_ProductTable.WIDTHUMID_FLD + ","
					+ ITM_ProductTable.LENGTHUMID_FLD + ","
					+ ITM_ProductTable.BUYINGUMID_FLD + ","
					+ ITM_ProductTable.WEIGHTUMID_FLD + ","
					+ ITM_ProductTable.LOTSIZE_FLD + ","
					+ ITM_ProductTable.MASTERLOCATIONID_FLD + ","
					+ ITM_ProductTable.LOCATIONID_FLD + ","
					+ ITM_ProductTable.BINID_FLD + ","
					+ ITM_ProductTable.PRIMARYVENDORID_FLD + ","
					+ ITM_ProductTable.VENDORLOCATIONID_FLD + ","
					+ ITM_ProductTable.SAFETYSTOCK_FLD + ","
					+ ITM_ProductTable.AGCID_FLD + ","
					+ ITM_ProductTable.PARTNAMEVN_FLD + ","
					+ ITM_ProductTable.INVENTORID_FLD + ","
					+ ITM_ProductTable.LICENSEFEE_FLD + ","
					+ ITM_ProductTable.LISTPRICE_FLD + ","
					+ ITM_ProductTable.VENDORCURRENCYID_FLD + ","
					+ ITM_ProductTable.QUANTITYSET_FLD + ","
					+ ITM_ProductTable.TAXCODE_FLD + ","
					+ ITM_ProductTable.MINPRODUCE_FLD + ","
					+ ITM_ProductTable.MAXPRODUCE_FLD + ","
					+ ITM_ProductTable.PRODUCTTYPEID_FLD + ","
					+ ITM_ProductTable.PICTURE_FLD + ","
					+ ITM_ProductTable.SETUPPAIR_FLD + ","
					+ ITM_ProductTable.MAXROUNDUPTOMIN_FLD + ", "
					+ ITM_ProductTable.MAXROUNDUPTOMULTIPLE_FLD + ", "
					+ ITM_ProductTable.ACADJUSTMENTMASTERID_FLD + ", "
					+ ITM_ProductTable.REGISTEREDCODE_FLD + ", "
					+ ITM_ProductTable.AVEG_FLD + ", "
					+ ITM_ProductTable.MASSORDER_FLD + ", "
					+ ITM_ProductTable.STOCKTAKINGCODE_FLD + ", "
                    + ITM_ProductTable.ALLOWNEGATIVEQTY_FLD + ", "
					+ ITM_ProductTable.ORDERPOINT_FLD + ")"
					+ "VALUES(?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?,?)";

				strSql += " ; Select @@IDENTITY as NEWID";

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.CODE_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[ITM_ProductTable.CODE_FLD].Value = objObject.Code;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.REVISION_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[ITM_ProductTable.REVISION_FLD].Value = objObject.Revision;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.DESCRIPTION_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[ITM_ProductTable.DESCRIPTION_FLD].Value = objObject.Description;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.SETUPDATE_FLD, OleDbType.Date));
				if (objObject.SetupDate == DateTime.MinValue)
				{
					ocmdPCS.Parameters[ITM_ProductTable.SETUPDATE_FLD].Value = DBNull.Value;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.SETUPDATE_FLD].Value = objObject.SetupDate;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.VAT_FLD, OleDbType.Decimal));
				if (objObject.VAT >= 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.VAT_FLD].Value = objObject.VAT;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.VAT_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.IMPORTTAX_FLD, OleDbType.Decimal));
				if (objObject.ImportTax >= 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.IMPORTTAX_FLD].Value = objObject.ImportTax;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.IMPORTTAX_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.EXPORTTAX_FLD, OleDbType.Decimal));
				if (objObject.ExportTax >= 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.EXPORTTAX_FLD].Value = objObject.ExportTax;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.EXPORTTAX_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.SPECIALTAX_FLD, OleDbType.Decimal));
				if (objObject.SpecialTax >= 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.SPECIALTAX_FLD].Value = objObject.SpecialTax;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.SPECIALTAX_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.MAKEITEM_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[ITM_ProductTable.MAKEITEM_FLD].Value = objObject.MakeItem;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.PARTNUMBER_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[ITM_ProductTable.PARTNUMBER_FLD].Value = objObject.PartNumber;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.OTHERINFO1_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[ITM_ProductTable.OTHERINFO1_FLD].Value = objObject.OtherInfo1;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.OTHERINFO2_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[ITM_ProductTable.OTHERINFO2_FLD].Value = objObject.OtherInfo2;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.LENGTH_FLD, OleDbType.Decimal));
				if (objObject.Length >= 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.LENGTH_FLD].Value = objObject.Length;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.LENGTH_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.WIDTH_FLD, OleDbType.Decimal));
				if (objObject.Width >= 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.WIDTH_FLD].Value = objObject.Width;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.WIDTH_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.HEIGHT_FLD, OleDbType.Decimal));
				if (objObject.Height >= 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.HEIGHT_FLD].Value = objObject.Height;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.HEIGHT_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.WEIGHT_FLD, OleDbType.Decimal));
				if (objObject.Weight >= 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.WEIGHT_FLD].Value = objObject.Weight;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.WEIGHT_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.FINISHEDGOODS_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[ITM_ProductTable.FINISHEDGOODS_FLD].Value = objObject.FinishedGoods;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.SHELFLIFE_FLD, OleDbType.Decimal));
				if (objObject.ShelfLife >= 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.SHELFLIFE_FLD].Value = objObject.ShelfLife;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.SHELFLIFE_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.LOTCONTROL_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[ITM_ProductTable.LOTCONTROL_FLD].Value = objObject.LotControl;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.QASTATUS_FLD, OleDbType.Integer));
				if (objObject.QAStatus == 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.QASTATUS_FLD].Value = DBNull.Value;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.QASTATUS_FLD].Value = objObject.QAStatus;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.STOCK_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[ITM_ProductTable.STOCK_FLD].Value = objObject.Stock;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.PLANTYPE_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[ITM_ProductTable.PLANTYPE_FLD].Value = objObject.PlanType;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.AUTOCONVERSION_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[ITM_ProductTable.AUTOCONVERSION_FLD].Value = objObject.AutoConversion;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.ORDERQUANTITY_FLD, OleDbType.Decimal));
				if (objObject.OrderQuantity >= 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.ORDERQUANTITY_FLD].Value = objObject.OrderQuantity;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.ORDERQUANTITY_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.LTREQUISITION_FLD, OleDbType.Decimal));
				if (objObject.LTRequisition >= 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.LTREQUISITION_FLD].Value = objObject.LTRequisition;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.LTREQUISITION_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.LTSAFETYSTOCK_FLD, OleDbType.Decimal));
				if (objObject.LTSafetyStock >= 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.LTSAFETYSTOCK_FLD].Value = objObject.LTSafetyStock;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.LTSAFETYSTOCK_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.ORDERQUANTITYMULTIPLE_FLD, OleDbType.Decimal));
				if (objObject.OrderQuantityMultiple >= 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.ORDERQUANTITYMULTIPLE_FLD].Value = objObject.OrderQuantityMultiple;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.ORDERQUANTITYMULTIPLE_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.SCRAPPERCENT_FLD, OleDbType.Decimal));
				if (objObject.ScrapPercent >= 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.SCRAPPERCENT_FLD].Value = objObject.ScrapPercent;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.SCRAPPERCENT_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.MINIMUMSTOCK_FLD, OleDbType.Decimal));
				if (objObject.MinimumStock >= 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.MINIMUMSTOCK_FLD].Value = objObject.MinimumStock;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.MINIMUMSTOCK_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.MAXIMUMSTOCK_FLD, OleDbType.Decimal));
				if (objObject.MaximumStock >= 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.MAXIMUMSTOCK_FLD].Value = objObject.MaximumStock;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.MAXIMUMSTOCK_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.CONVERSIONTOLERANCE_FLD, OleDbType.Decimal));
				if (objObject.ConversionTolerance >= 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.CONVERSIONTOLERANCE_FLD].Value = objObject.ConversionTolerance;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.CONVERSIONTOLERANCE_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.VOUCHERTOLERANCE_FLD, OleDbType.Decimal));
				if (objObject.VoucherTolerance >= 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.VOUCHERTOLERANCE_FLD].Value = objObject.VoucherTolerance;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.VOUCHERTOLERANCE_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.RECEIVETOLERANCE_FLD, OleDbType.Decimal));
				if (objObject.ReceiveTolerance >= 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.RECEIVETOLERANCE_FLD].Value = objObject.ReceiveTolerance;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.RECEIVETOLERANCE_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.ISSUESIZE_FLD, OleDbType.Decimal));
				if (objObject.IssueSize >= 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.ISSUESIZE_FLD].Value = objObject.IssueSize;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.ISSUESIZE_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.LTFIXEDTIME_FLD, OleDbType.Decimal));
				if (objObject.LTFixedTime >= 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.LTFIXEDTIME_FLD].Value = objObject.LTFixedTime;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.LTFIXEDTIME_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.LTVARIABLETIME_FLD, OleDbType.Decimal));
				if (objObject.LTVariableTime >= 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.LTVARIABLETIME_FLD].Value = objObject.LTVariableTime;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.LTVARIABLETIME_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.LTDOCKTOSTOCK_FLD, OleDbType.Decimal));
				if (objObject.LTDocToStock >= 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.LTDOCKTOSTOCK_FLD].Value = objObject.LTDocToStock;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.LTDOCKTOSTOCK_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.LTORDERPREPARE_FLD, OleDbType.Decimal));
				if (objObject.LTOrderPrepare >= 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.LTORDERPREPARE_FLD].Value = objObject.LTOrderPrepare;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.LTORDERPREPARE_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.LTSHIPPINGPREPARE_FLD, OleDbType.Decimal));
				if (objObject.LTShippingPrepare >= 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.LTSHIPPINGPREPARE_FLD].Value = objObject.LTShippingPrepare;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.LTSHIPPINGPREPARE_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.LTSALESATP_FLD, OleDbType.Decimal));
				if (objObject.LTSalesATP >= 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.LTSALESATP_FLD].Value = objObject.LTSalesATP;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.LTSALESATP_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.SHIPTOLERANCEID_FLD, OleDbType.Integer));
				if (objObject.ShipToleranceID == 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.SHIPTOLERANCEID_FLD].Value = DBNull.Value;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.SHIPTOLERANCEID_FLD].Value = objObject.ShipToleranceID;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.BUYERID_FLD, OleDbType.Integer));
				if (objObject.BuyerID == 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.BUYERID_FLD].Value = DBNull.Value;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.BUYERID_FLD].Value = objObject.BuyerID;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.BOMDESCRIPTION_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[ITM_ProductTable.BOMDESCRIPTION_FLD].Value = objObject.BOMDescription;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.BOMINCREMENT_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[ITM_ProductTable.BOMINCREMENT_FLD].Value = objObject.BomIncrement;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.ROUTINGDESCRIPTION_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[ITM_ProductTable.ROUTINGDESCRIPTION_FLD].Value = objObject.RoutingDescription;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.CREATEDATETIME_FLD, OleDbType.Date));
				ocmdPCS.Parameters[ITM_ProductTable.CREATEDATETIME_FLD].Value = objObject.CreateDateTime;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.UPDATEDATETIME_FLD, OleDbType.Date));
				ocmdPCS.Parameters[ITM_ProductTable.UPDATEDATETIME_FLD].Value = DBNull.Value; //objObject.UpdateDateTime;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.COSTMETHOD_FLD, OleDbType.Integer));
				if (objObject.CostMethod >= 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.COSTMETHOD_FLD].Value = objObject.CostMethod;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.COSTMETHOD_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.ROUTINGINCREMENT_FLD, OleDbType.Integer));
				ocmdPCS.Parameters[ITM_ProductTable.ROUTINGINCREMENT_FLD].Value = objObject.RoutingIncrement;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.CCNID_FLD, OleDbType.Integer));
				if (objObject.CCNID == 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.CCNID_FLD].Value = DBNull.Value;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.CCNID_FLD].Value = objObject.CCNID;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.CATEGORYID_FLD, OleDbType.Integer));
				if (objObject.CategoryID == 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.CATEGORYID_FLD].Value = DBNull.Value;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.CATEGORYID_FLD].Value = objObject.CategoryID;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.COSTCENTERID_FLD, OleDbType.Integer));
				if (objObject.CostCenterID == 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.COSTCENTERID_FLD].Value = DBNull.Value;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.COSTCENTERID_FLD].Value = objObject.CostCenterID;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.DELETEREASONID_FLD, OleDbType.Integer));
				if (objObject.DeleteReasonID == 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.DELETEREASONID_FLD].Value = DBNull.Value;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.DELETEREASONID_FLD].Value = objObject.DeleteReasonID;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.DELIVERYPOLICYID_FLD, OleDbType.Integer));
				if (objObject.DeliveryPolicyID == 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.DELIVERYPOLICYID_FLD].Value = DBNull.Value;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.DELIVERYPOLICYID_FLD].Value = objObject.DeliveryPolicyID;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.FORMATCODEID_FLD, OleDbType.Integer));
				if (objObject.FormatCodeID == 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.FORMATCODEID_FLD].Value = DBNull.Value;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.FORMATCODEID_FLD].Value = objObject.FormatCodeID;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.FREIGHTCLASSID_FLD, OleDbType.Integer));
				if (objObject.FreightClassID == 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.FREIGHTCLASSID_FLD].Value = DBNull.Value;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.FREIGHTCLASSID_FLD].Value = objObject.FreightClassID;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.HAZARDID_FLD, OleDbType.Integer));
				if (objObject.HazardID == 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.HAZARDID_FLD].Value = DBNull.Value;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.HAZARDID_FLD].Value = objObject.HazardID;
				}
				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.ORDERPOLICYID_FLD, OleDbType.Integer));
				if (objObject.OrderPolicyID == 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.ORDERPOLICYID_FLD].Value = DBNull.Value;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.ORDERPOLICYID_FLD].Value = objObject.OrderPolicyID;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.ORDERRULEID_FLD, OleDbType.Integer));
				if (objObject.OrderRuleID == 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.ORDERRULEID_FLD].Value = DBNull.Value;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.ORDERRULEID_FLD].Value = objObject.OrderRuleID;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.SOURCEID_FLD, OleDbType.Integer));
				if (objObject.SourceID == 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.SOURCEID_FLD].Value = DBNull.Value;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.SOURCEID_FLD].Value = objObject.SourceID;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.STOCKUMID_FLD, OleDbType.Integer));
				if (objObject.StockUMID == 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.STOCKUMID_FLD].Value = DBNull.Value;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.STOCKUMID_FLD].Value = objObject.StockUMID;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.SELLINGUMID_FLD, OleDbType.Integer));
				if (objObject.SellingUMID == 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.SELLINGUMID_FLD].Value = DBNull.Value;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.SELLINGUMID_FLD].Value = objObject.SellingUMID;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.HEIGHTUMID_FLD, OleDbType.Integer));
				if (objObject.HeightUMID != 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.HEIGHTUMID_FLD].Value = objObject.HeightUMID;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.HEIGHTUMID_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.WIDTHUMID_FLD, OleDbType.Integer));
				if (objObject.WidthUMID != 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.WIDTHUMID_FLD].Value = objObject.WidthUMID;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.WIDTHUMID_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.LENGTHUMID_FLD, OleDbType.Integer));
				if (objObject.LengthUMID != 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.LENGTHUMID_FLD].Value = objObject.LengthUMID;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.LENGTHUMID_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.BUYINGUMID_FLD, OleDbType.Integer));
				if (objObject.BuyingUMID != 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.BUYINGUMID_FLD].Value = objObject.BuyingUMID;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.BUYINGUMID_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.WEIGHTUMID_FLD, OleDbType.Integer));
				if (objObject.WeightUMID != 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.WEIGHTUMID_FLD].Value = objObject.WeightUMID;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.WEIGHTUMID_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.LOTSIZE_FLD, OleDbType.Integer));
				if (objObject.LotSize >= 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.LOTSIZE_FLD].Value = objObject.LotSize;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.LOTSIZE_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.MASTERLOCATIONID_FLD, OleDbType.Integer));
				if (objObject.MasterLocationID != 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.MASTERLOCATIONID_FLD].Value = objObject.MasterLocationID;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.MASTERLOCATIONID_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.LOCATIONID_FLD, OleDbType.Integer));
				if (objObject.LocationID != 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.LOCATIONID_FLD].Value = objObject.LocationID;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.LOCATIONID_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.BINID_FLD, OleDbType.Integer));

				if (objObject.BinID != 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.BINID_FLD].Value = objObject.BinID;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.BINID_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.PRIMARYVENDORID_FLD, OleDbType.Integer));
				if (objObject.PrimaryVendorID != 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.PRIMARYVENDORID_FLD].Value = objObject.PrimaryVendorID;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.PRIMARYVENDORID_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.VENDORLOCATIONID_FLD, OleDbType.Integer));
				if (objObject.VendorLocationID != 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.VENDORLOCATIONID_FLD].Value = objObject.VendorLocationID;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.VENDORLOCATIONID_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.SAFETYSTOCK_FLD, OleDbType.Decimal));
				if (objObject.SafetyStock >= 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.SAFETYSTOCK_FLD].Value = objObject.SafetyStock;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.SAFETYSTOCK_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.AGCID_FLD, OleDbType.Integer));
				if (objObject.AGCID > 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.AGCID_FLD].Value = objObject.AGCID;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.AGCID_FLD].Value = DBNull.Value;
				}

				//Begin_ Added by Tuan TQ
				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.PARTNAMEVN_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[ITM_ProductTable.PARTNAMEVN_FLD].Value = objObject.PartNameVN;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.INVENTORID_FLD, OleDbType.Integer));
				if (objObject.Inventor > 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.INVENTORID_FLD].Value = objObject.Inventor;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.INVENTORID_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.LICENSEFEE_FLD, OleDbType.Decimal));
				if (objObject.LicenseFee >= 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.LICENSEFEE_FLD].Value = objObject.LicenseFee;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.LICENSEFEE_FLD].Value = DBNull.Value;
				}

				//Added:  Nov 03, 2005
				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.LISTPRICE_FLD, OleDbType.Decimal));
				if (objObject.ListPrice >= 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.LISTPRICE_FLD].Value = objObject.ListPrice;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.LISTPRICE_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.VENDORCURRENCYID_FLD, OleDbType.Integer));
				if (objObject.VendorCurrencyID > 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.VENDORCURRENCYID_FLD].Value = objObject.VendorCurrencyID;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.VENDORCURRENCYID_FLD].Value = DBNull.Value;
				}
				//End Added:  Nov 03, 2005

				//Added:  Nov 08, 2005
				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.QUANTITYSET_FLD, OleDbType.Decimal));
				if (objObject.QuantitySet >= 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.QUANTITYSET_FLD].Value = objObject.QuantitySet;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.QUANTITYSET_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.TAXCODE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[ITM_ProductTable.TAXCODE_FLD].Value = objObject.TaxCode;

				//End Added:  Nov 08, 2005

				//Added:  01 Mar, 2006
				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.MINPRODUCE_FLD, OleDbType.Decimal));
				if (objObject.MinProduce >= 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.MINPRODUCE_FLD].Value = objObject.MinProduce;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.MINPRODUCE_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.MAXPRODUCE_FLD, OleDbType.Decimal));
				if (objObject.MaxProduce >= 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.MAXPRODUCE_FLD].Value = objObject.MaxProduce;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.MAXPRODUCE_FLD].Value = DBNull.Value;
				}
				//End

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.PRODUCTTYPEID_FLD, OleDbType.Integer));
				if (objObject.ProductTypeId > 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.PRODUCTTYPEID_FLD].Value = objObject.ProductTypeId;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.PRODUCTTYPEID_FLD].Value = DBNull.Value;
				}
				//End_ Added by Tuan TQ

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.PICTURE_FLD, OleDbType.Binary));
				if (objObject.Picture != null)
				{
					// convert bitmap to byte array in order to store to db
					Bitmap image = objObject.Picture;
					MemoryStream stream = new MemoryStream();
					image.Save(stream, ImageFormat.Bmp);
					byte[] bytContent = stream.ToArray();
					ocmdPCS.Parameters[ITM_ProductTable.PICTURE_FLD].Value = bytContent;
					ocmdPCS.Parameters[ITM_ProductTable.PICTURE_FLD].Size = bytContent.Length;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.PICTURE_FLD].Value = DBNull.Value;
				}
				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.SETUPPAIR_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[ITM_ProductTable.SETUPPAIR_FLD].Value = objObject.SetUpPair;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.MAXROUNDUPTOMIN_FLD, OleDbType.Decimal));
				if (objObject.MaxRoundUpToMin >= 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.MAXROUNDUPTOMIN_FLD].Value = objObject.MaxRoundUpToMin;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.MAXROUNDUPTOMIN_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.MAXROUNDUPTOMULTIPLE_FLD, OleDbType.Decimal));
				if (objObject.MaxRoundUpToMultiple >= 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.MAXROUNDUPTOMULTIPLE_FLD].Value = objObject.MaxRoundUpToMultiple;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.MAXROUNDUPTOMULTIPLE_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.ACADJUSTMENTMASTERID_FLD, OleDbType.Integer));
				if (objObject.ACAdjustmentMasterID > 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.ACADJUSTMENTMASTERID_FLD].Value = objObject.ACAdjustmentMasterID;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.ACADJUSTMENTMASTERID_FLD].Value = DBNull.Value;
				}

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.REGISTEREDCODE_FLD, OleDbType.WChar));
				ocmdPCS.Parameters[ITM_ProductTable.REGISTEREDCODE_FLD].Value = objObject.RegisteredCode;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.AVEG_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[ITM_ProductTable.AVEG_FLD].Value = objObject.AVEG;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.MASSORDER_FLD, OleDbType.Boolean));
				ocmdPCS.Parameters[ITM_ProductTable.MASSORDER_FLD].Value = objObject.MassOrder;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.STOCKTAKINGCODE_FLD, OleDbType.VarWChar));
				ocmdPCS.Parameters[ITM_ProductTable.STOCKTAKINGCODE_FLD].Value = objObject.StockTakingCode;

                ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.ALLOWNEGATIVEQTY_FLD, OleDbType.Boolean));
                ocmdPCS.Parameters[ITM_ProductTable.ALLOWNEGATIVEQTY_FLD].Value = objObject.AllowNegativeQty;

				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.ORDERPOINT_FLD, OleDbType.Decimal));
				if (objObject.OrderPoint >= 0)
				{
					ocmdPCS.Parameters[ITM_ProductTable.ORDERPOINT_FLD].Value = objObject.OrderPoint;
				}
				else
				{
					ocmdPCS.Parameters[ITM_ProductTable.ORDERPOINT_FLD].Value = DBNull.Value;
				}

				ocmdPCS.CommandText = strSql;
				ocmdPCS.Connection.Open();

				return int.Parse(ocmdPCS.ExecuteScalar().ToString());
				//ocmdPCS.ExecuteNonQuery();	

			}
			catch (OleDbException ex)
			{
				if (ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_KEYCODE
					|| ex.Errors[1].NativeError == ErrorCode.SQLDUPLICATE_UNIQUE_KEYCODE)
				{
					throw new PCSDBException(ErrorCode.DUPLICATE_KEY, METHOD_NAME, ex);
				}
				else
				{
					throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
				}
			}

			catch (InvalidOperationException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}
			catch (Exception ex)
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}

			finally
			{
				if (oconPCS != null)
				{
					if (oconPCS.State != ConnectionState.Closed)
					{
						oconPCS.Close();
					}
				}
			}
		}

		//**************************************************************************              
		///    <Description>
		///       This method uses to return ProductID
		///    </Description>
		///    <Inputs>
		///        Code      
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       void
		///    </Returns>
		///    <Authors>
		///       HungLa
		///    </Authors>
		///    <History>
		///       Tuesday, January 25, 2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public int GetProductIDByCode(string pstrCode)
		{
			const string METHOD_NAME = THIS + ".GetProductIDByCode()";
			DataSet dstPCS = new DataSet();

			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = "SELECT "
					+ ITM_ProductTable.PRODUCTID_FLD
					+ " FROM " + ITM_ProductTable.TABLE_NAME
					+ " WHERE " + ITM_ProductTable.CODE_FLD + "='" + pstrCode + "'";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				object objReturnValue = ocmdPCS.ExecuteScalar();
				if (objReturnValue == null)
				{
					return 0;
				}
				else
				{
					return int.Parse(objReturnValue.ToString());
				}

			}
			catch (OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}

			catch (Exception ex)
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}

			finally
			{
				if (oconPCS != null)
				{
					if (oconPCS.State != ConnectionState.Closed)
					{
						oconPCS.Close();
					}
				}
			}
		}

		public int GetProductIDByDescription(string pstrDescription)
		{
			const string METHOD_NAME = THIS + ".GetProductIDByCode()";
			DataSet dstPCS = new DataSet();

			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = "SELECT "
					+ ITM_ProductTable.PRODUCTID_FLD
					+ " FROM " + ITM_ProductTable.TABLE_NAME
					+ " WHERE " + ITM_ProductTable.DESCRIPTION_FLD + "='" + pstrDescription + "'";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				object objReturnValue = ocmdPCS.ExecuteScalar();
				if (objReturnValue == null)
				{
					return 0;
				}
				else
				{
					return int.Parse(objReturnValue.ToString());
				}

			}
			catch (OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}

			catch (Exception ex)
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}

			finally
			{
				if (oconPCS != null)
				{
					if (oconPCS.State != ConnectionState.Closed)
					{
						oconPCS.Close();
					}
				}
			}

		}

		/// <summary>
		/// LoadProductByProductID
		/// </summary>
		/// <param name="pintProductID"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Tuesday, August 16 2005</date>
		public DataSet LoadProductByProductID(int pintProductID)
		{
			const string METHOD_NAME = THIS + ".LoadProductByProductID()";
			DataSet dstPCS = new DataSet();

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = "SELECT "
					+ ITM_ProductTable.PRODUCTID_FLD + ","
					+ ITM_ProductTable.CODE_FLD + ","
					+ ITM_ProductTable.REVISION_FLD + ","
					+ ITM_ProductTable.DESCRIPTION_FLD
					+ " FROM " + ITM_ProductTable.TABLE_NAME
					+ " WHERE " + ITM_ProductTable.PRODUCTID_FLD + " = " + pintProductID.ToString();
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, ITM_ProductTable.TABLE_NAME);

				return dstPCS;
			}
			catch (OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}
			catch (Exception ex)
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}

			finally
			{
				if (oconPCS != null)
				{
					if (oconPCS.State != ConnectionState.Closed)
					{
						oconPCS.Close();
					}
				}
			}
		}

		/// <summary>
		/// This method is used to get the Product information
		/// based on the Work Order Detail ID
		/// </summary>
		/// <param name="pintID"></param>
		/// <returns></returns>
		public object GetProductInfoByWorkOrderDetailID(int pintID)
		{
			const string METHOD_NAME = THIS + ".GetProductInfoByWorkOrderDetailID()";
			DataSet dstPCS = new DataSet();

			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = "SELECT "
					+ ITM_ProductTable.PRODUCTID_FLD + ","
					+ ITM_ProductTable.CODE_FLD + ","
					+ ITM_ProductTable.REVISION_FLD + ","
					+ ITM_ProductTable.DESCRIPTION_FLD
					+ " FROM " + ITM_ProductTable.TABLE_NAME
					+ " WHERE " + ITM_ProductTable.PRODUCTID_FLD
					+ "   in (SELECT " + PRO_WorkOrderDetailTable.PRODUCTID_FLD
					+ "       FROM " + PRO_WorkOrderDetailTable.TABLE_NAME
					+ "       WHERE " + PRO_WorkOrderDetailTable.WORKORDERDETAILID_FLD + "=" + pintID
					+ "      )";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				ITM_ProductVO objObject = new ITM_ProductVO();

				while (odrPCS.Read())
				{
					objObject.ProductID = int.Parse(odrPCS[ITM_ProductTable.PRODUCTID_FLD].ToString());
					objObject.Code = odrPCS[ITM_ProductTable.CODE_FLD].ToString().Trim();
					objObject.Revision = odrPCS[ITM_ProductTable.REVISION_FLD].ToString().Trim();
					objObject.Description = odrPCS[ITM_ProductTable.DESCRIPTION_FLD].ToString().Trim();
				}
				return objObject;
			}
			catch (OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}

			catch (Exception ex)
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}

			finally
			{
				if (oconPCS != null)
				{
					if (oconPCS.State != ConnectionState.Closed)
					{
						oconPCS.Close();
					}
				}
			}
		}


		/// <summary>
		/// Get Order policy day for Item by ID
		/// </summary>
		/// <param name="pintProductID"></param>
		/// <returns></returns>
		public int GetOrderPolicyForItem(int pintProductID)
		{
			const string METHOD_NAME = THIS + ".GetOrderPolicyForItem()";
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = "SELECT (SELECT " + ITM_OrderPolicyTable.ORDERPOLICYDAYS_FLD + " FROM " + ITM_OrderPolicyTable.TABLE_NAME + " WHERE " + ITM_OrderPolicyTable.ORDERPOLICYID_FLD
					+ " = " + ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.ORDERPOLICYID_FLD + ")"
					+ " FROM " + ITM_ProductTable.TABLE_NAME
					+ " WHERE " + ITM_ProductTable.PRODUCTID_FLD + "='" + pintProductID + "'";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				object objReturnValue = ocmdPCS.ExecuteScalar();
				if (objReturnValue == null || objReturnValue == DBNull.Value)
				{
					return -1;
				}
				else
				{
					return int.Parse(objReturnValue.ToString());
				}
			}
			catch (OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}
			catch (Exception ex)
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}
			finally
			{
				if (oconPCS != null)
				{
					if (oconPCS.State != ConnectionState.Closed)
					{
						oconPCS.Close();
					}
				}
			}
		}


		/// <summary>
		/// Get Order policy day for Item by ID
		/// </summary>
		/// <param name="pintProductID"></param>
		/// <returns></returns>
		public DataRow GetOrderPolicyByProductID(int pintProductID)
		{
			const string METHOD_NAME = THIS + ".GetOrderPolicyForItem()";
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = "SELECT " + ITM_OrderPolicyTable.TABLE_NAME + ".*"
					+ " FROM " + ITM_ProductTable.TABLE_NAME
					+ " INNER JOIN " + ITM_OrderPolicyTable.TABLE_NAME + " ON " + ITM_OrderPolicyTable.TABLE_NAME + "." + ITM_OrderPolicyTable.ORDERPOLICYID_FLD + "=" + ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.ORDERPOLICYID_FLD
					+ " WHERE " + ITM_ProductTable.PRODUCTID_FLD + "='" + pintProductID + "'";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				OleDbDataAdapter odapPCS = new OleDbDataAdapter(ocmdPCS);
				DataSet dstData = new DataSet();
				ocmdPCS.Connection.Open();

				odapPCS.Fill(dstData);
				if (dstData.Tables[0].Rows.Count > 0)
				{
					return dstData.Tables[0].Rows[0];
				}
				else
				{
					return null;
				}
			}
			catch (OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}
			catch (Exception ex)
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}
			finally
			{
				if (oconPCS != null)
				{
					if (oconPCS.State != ConnectionState.Closed)
					{
						oconPCS.Close();
					}
				}
			}
		}

		/// <summary>
		/// Get Order policy day for Item by ID
		/// </summary>
		/// <param name="pstrProductIDs"></param>
		/// <returns></returns>
		public DataTable GetOrderPolicyByProducts(StringBuilder pstrProductIDs)
		{
			const string METHOD_NAME = THIS + ".GetOrderPolicyForItem()";
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = "SELECT " + ITM_OrderPolicyTable.TABLE_NAME + ".*, ProductID"
					+ " FROM " + ITM_ProductTable.TABLE_NAME
					+ " INNER JOIN " + ITM_OrderPolicyTable.TABLE_NAME + " ON " + ITM_OrderPolicyTable.TABLE_NAME + "." + ITM_OrderPolicyTable.ORDERPOLICYID_FLD + "=" + ITM_ProductTable.TABLE_NAME + "." + ITM_ProductTable.ORDERPOLICYID_FLD
					+ " WHERE " + ITM_ProductTable.PRODUCTID_FLD + " in " + pstrProductIDs;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				OleDbDataAdapter odapPCS = new OleDbDataAdapter(ocmdPCS);
				DataSet dstData = new DataSet();
				ocmdPCS.Connection.Open();

				odapPCS.Fill(dstData);
				return dstData.Tables[0];
			}
			catch (OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}
			catch (Exception ex)
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}
			finally
			{
				if (oconPCS != null)
				{
					if (oconPCS.State != ConnectionState.Closed)
					{
						oconPCS.Close();
					}
				}
			}
		}


		public object GetObjectVOForMaterialReceipt(int pintProductID)
		{
			const string METHOD_NAME = THIS + ".GetObjectVOForMaterialReceipt()";
			DataSet dstPCS = new DataSet();

			OleDbDataReader odrPCS = null;
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				strSql = "SELECT "
					+ ITM_ProductTable.PRODUCTID_FLD + ","
					+ ITM_ProductTable.CODE_FLD + ","
					+ ITM_ProductTable.REVISION_FLD + ","
					+ ITM_ProductTable.DESCRIPTION_FLD + ","
					+ ITM_ProductTable.STOCKUMID_FLD
					+ " FROM " + ITM_ProductTable.TABLE_NAME
					+ " WHERE " + ITM_ProductTable.PRODUCTID_FLD + "=" + pintProductID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();
				odrPCS = ocmdPCS.ExecuteReader();

				ITM_ProductVO objObject = new ITM_ProductVO();

				while (odrPCS.Read())
				{
					objObject.ProductID = int.Parse(odrPCS[ITM_ProductTable.PRODUCTID_FLD].ToString().Trim());
					objObject.Code = odrPCS[ITM_ProductTable.CODE_FLD].ToString().Trim();
					objObject.Description = odrPCS[ITM_ProductTable.DESCRIPTION_FLD].ToString().Trim();
					objObject.Revision = odrPCS[ITM_ProductTable.REVISION_FLD].ToString().Trim();
					objObject.StockUMID = int.Parse(odrPCS[ITM_ProductTable.STOCKUMID_FLD].ToString());

				}
				return objObject;
			}
			catch (OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}

			catch (Exception ex)
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}

			finally
			{
				if (oconPCS != null)
				{
					if (oconPCS.State != ConnectionState.Closed)
					{
						oconPCS.Close();
					}
				}
			}
		}

		public DataSet GetProductType()
		{
			const string METHOD_NAME = THIS + ".GetProductType()";
			DataSet dstPCS = new DataSet();

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = "SELECT "
					+ ITM_ProductTypeTable.PRODUCTTYPEID_FLD + ","
					+ ITM_ProductTypeTable.CODE_FLD + ","
					+ ITM_ProductTypeTable.DESCRIPTION_FLD
					+ " FROM " + ITM_ProductTypeTable.TABLE_NAME;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, ITM_ProductTypeTable.TABLE_NAME);

				return dstPCS;
			}
			catch (OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}
			catch (Exception ex)
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}

			finally
			{
				if (oconPCS != null)
				{
					if (oconPCS.State != ConnectionState.Closed)
					{
						oconPCS.Close();
					}
				}
			}
		}

		public DataSet GetMRPItems(string pstrVendorID, string pstrCategoryID, string pstrModel, string pstrProductID)
		{
			const string METHOD_NAME = THIS + ".GetMRPItems()";
			DataSet dstPCS = new DataSet();

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = "SELECT "
					+ ITM_ProductTable.PRODUCTID_FLD + ","
					+ ITM_ProductTable.CODE_FLD + ","
					+ ITM_ProductTable.REVISION_FLD + ","
					+ ITM_ProductTable.DESCRIPTION_FLD + ","
					+ ITM_ProductTable.STOCKUMID_FLD + ","
					+ ITM_ProductTable.LTFIXEDTIME_FLD + ","
					+ ITM_ProductTable.LTDOCKTOSTOCK_FLD + ","
					+ ITM_ProductTable.LTREQUISITION_FLD + ","
					+ ITM_ProductTable.LTVARIABLETIME_FLD + ","
					+ ITM_ProductTable.LTSAFETYSTOCK_FLD + ","
					+ ITM_ProductTable.LTORDERPREPARE_FLD + ","
					+ ITM_ProductTable.LTSHIPPINGPREPARE_FLD + ","
					+ ITM_ProductTable.SAFETYSTOCK_FLD + ","
					+ ITM_ProductTable.LOTSIZE_FLD + ","
					+ ITM_ProductTable.ORDERQUANTITY_FLD + ","
					+ ITM_ProductTable.ORDERQUANTITYMULTIPLE_FLD + ","
					+ ITM_ProductTable.SCRAPPERCENT_FLD + ","
					+ ITM_ProductTable.MASTERLOCATIONID_FLD + ","
					+ ITM_ProductTable.LOCATIONID_FLD + ","
					+ ITM_ProductTable.BINID_FLD + ","
					+ ITM_ProductTable.LTSALESATP_FLD
					+ " FROM " + ITM_ProductTable.TABLE_NAME
					+ " WHERE " + ITM_ProductTable.PLANTYPE_FLD + "=1"
					+ " and " + ITM_ProductTable.MAKEITEM_FLD + "<>" + 1.ToString();
				if (pstrVendorID != null && pstrVendorID != string.Empty)
					strSql += " AND PrimaryVendorID IN (" + pstrVendorID + ")";
				if (pstrCategoryID != null && pstrCategoryID != string.Empty)
					strSql += " AND CategoryID IN (" + pstrCategoryID + ")";
				if (pstrModel != null && pstrModel != string.Empty)
					strSql += " AND Revision IN (" + pstrModel + ")";
				if (pstrProductID != null && pstrProductID != string.Empty)
					strSql += " AND ProductID IN (" + pstrProductID + ")";
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, ITM_ProductTable.TABLE_NAME);

				return dstPCS;
			}
			catch (OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}
			catch (Exception ex)
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}

			finally
			{
				if (oconPCS != null)
				{
					if (oconPCS.State != ConnectionState.Closed)
					{
						oconPCS.Close();
					}
				}
			}
		}
		public DataSet GetMRPItems(int pintPlanType)
		{
			const string METHOD_NAME = THIS + ".ListForCombo()";
			DataSet dstPCS = new DataSet();

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = "SELECT "
					+ ITM_ProductTable.PRODUCTID_FLD + ","
					+ ITM_ProductTable.CODE_FLD + ","
					+ ITM_ProductTable.REVISION_FLD + ","
					+ ITM_ProductTable.DESCRIPTION_FLD + ","
					+ ITM_ProductTable.STOCKUMID_FLD + ","
					+ ITM_ProductTable.LTFIXEDTIME_FLD + ","
					+ ITM_ProductTable.LTDOCKTOSTOCK_FLD + ","
					+ ITM_ProductTable.LTREQUISITION_FLD + ","
					+ ITM_ProductTable.LTVARIABLETIME_FLD + ","
					+ ITM_ProductTable.LTSAFETYSTOCK_FLD + ","
					+ ITM_ProductTable.LTORDERPREPARE_FLD + ","
					+ ITM_ProductTable.LTSHIPPINGPREPARE_FLD + ","
					+ ITM_ProductTable.SAFETYSTOCK_FLD + ","
					+ ITM_ProductTable.LOTSIZE_FLD + ","
					+ ITM_ProductTable.ORDERQUANTITY_FLD + ","
					+ ITM_ProductTable.ORDERQUANTITYMULTIPLE_FLD + ","
					+ ITM_ProductTable.SCRAPPERCENT_FLD + ","
					+ ITM_ProductTable.MASTERLOCATIONID_FLD + ","
					+ ITM_ProductTable.LOCATIONID_FLD + ","
					+ ITM_ProductTable.BINID_FLD + ","
					+ ITM_ProductTable.LTSALESATP_FLD
					+ " FROM " + ITM_ProductTable.TABLE_NAME
					+ " WHERE " + ITM_ProductTable.PLANTYPE_FLD + "=" + pintPlanType
					+ " and " + ITM_ProductTable.MAKEITEM_FLD + "<>" + 1.ToString();
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, ITM_ProductTable.TABLE_NAME);

				return dstPCS;
			}
			catch (OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}
			catch (Exception ex)
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}

			finally
			{
				if (oconPCS != null)
				{
					if (oconPCS.State != ConnectionState.Closed)
					{
						oconPCS.Close();
					}
				}
			}
		}

		public DataSet GetMRPItems(int pintPlanType, int pintMasLocID)
		{
			return null;
		}

		/// <summary>
		/// List all items in CCN
		/// </summary>
		/// <param name="pintCCNID">CCN ID</param>
		/// <returns>All Items</returns>
		public DataTable List(int pintCCNID)
		{
			const string METHOD_NAME = THIS + ".List()";
			DataSet dstPCS = new DataSet();

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;

				strSql = "SELECT "
					+ ITM_ProductTable.PRODUCTID_FLD + ","
					+ ITM_ProductTable.CODE_FLD + ","
					+ ITM_ProductTable.REVISION_FLD + ","
					+ ITM_ProductTable.DESCRIPTION_FLD + ","
					+ ITM_ProductTable.CATEGORYID_FLD + ","
					+ ITM_ProductTable.MAKEITEM_FLD
					+ " FROM " + ITM_ProductTable.TABLE_NAME
					+ " WHERE " + ITM_ProductTable.CCNID_FLD + "=" + pintCCNID;
				// for debug only, remove it when release
				//+ " AND ITM_Product.ProductID IN (1055,1017, 1054)";
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS, ITM_ProductTable.TABLE_NAME);

				return dstPCS.Tables[0];
			}
			catch (OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}
			catch (Exception ex)
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}

			finally
			{
				if (oconPCS != null)
				{
					if (oconPCS.State != ConnectionState.Closed)
					{
						oconPCS.Close();
					}
				}
			}
		}

		#region HACK: Tuan TQ 04 Apr, 2006

		public DataTable GetCostMethod()
		{
			const string METHOD_NAME = THIS + ".GetCostMethod()";
			DataTable dtbResult = new DataTable(enm_CostMethodTable.TABLE_NAME);

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = "SELECT	CAST(Code as int) as Code,";
				strSql += " [Description]";
				strSql += " FROM enm_CostMethod";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dtbResult);

				return dtbResult;
			}
			catch (OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}
			catch (Exception ex)
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}
			finally
			{
				if (oconPCS != null)
				{
					if (oconPCS.State != ConnectionState.Closed)
					{
						oconPCS.Close();
					}
				}
			}
		}

		#endregion

		#region // HACK: DuongNA 2005-10-20

		public void ListByIDs(DataSet pdstData, string pstrIDs)
		{
			const string METHOD_NAME = THIS + ".ListByIDs()";

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;

				strSql = "SELECT "
					+ ITM_ProductTable.PRODUCTID_FLD + ","
					+ ITM_ProductTable.CODE_FLD + ","
					+ ITM_ProductTable.REVISION_FLD + ","
					+ ITM_ProductTable.DESCRIPTION_FLD + ","
					+ ITM_ProductTable.SETUPDATE_FLD + ","
					+ ITM_ProductTable.VAT_FLD + ","
					+ ITM_ProductTable.IMPORTTAX_FLD + ","
					+ ITM_ProductTable.EXPORTTAX_FLD + ","
					+ ITM_ProductTable.SPECIALTAX_FLD + ","
					+ ITM_ProductTable.MAKEITEM_FLD + ","
					+ ITM_ProductTable.PARTNUMBER_FLD + ","
					+ ITM_ProductTable.OTHERINFO1_FLD + ","
					+ ITM_ProductTable.OTHERINFO2_FLD + ","
					+ ITM_ProductTable.LENGTH_FLD + ","
					+ ITM_ProductTable.WIDTH_FLD + ","
					+ ITM_ProductTable.HEIGHT_FLD + ","
					+ ITM_ProductTable.WEIGHT_FLD + ","
					+ ITM_ProductTable.FINISHEDGOODS_FLD + ","
					+ ITM_ProductTable.SHELFLIFE_FLD + ","
					+ ITM_ProductTable.LOTCONTROL_FLD + ","
					+ ITM_ProductTable.QASTATUS_FLD + ","
					+ ITM_ProductTable.STOCK_FLD + ","
					+ ITM_ProductTable.PLANTYPE_FLD + ","
					+ ITM_ProductTable.AUTOCONVERSION_FLD + ","
					+ ITM_ProductTable.ORDERQUANTITY_FLD + ","
					+ ITM_ProductTable.LTREQUISITION_FLD + ","
					+ ITM_ProductTable.LTSAFETYSTOCK_FLD + ","
					+ ITM_ProductTable.ORDERQUANTITYMULTIPLE_FLD + ","
					+ ITM_ProductTable.SCRAPPERCENT_FLD + ","
					+ ITM_ProductTable.MINIMUMSTOCK_FLD + ","
					+ ITM_ProductTable.MAXIMUMSTOCK_FLD + ","
					+ ITM_ProductTable.CONVERSIONTOLERANCE_FLD + ","
					+ ITM_ProductTable.VOUCHERTOLERANCE_FLD + ","
					+ ITM_ProductTable.RECEIVETOLERANCE_FLD + ","
					+ ITM_ProductTable.ISSUESIZE_FLD + ","
					+ ITM_ProductTable.LTFIXEDTIME_FLD + ","
					+ ITM_ProductTable.LTVARIABLETIME_FLD + ","
					+ ITM_ProductTable.LTDOCKTOSTOCK_FLD + ","
					+ ITM_ProductTable.LTORDERPREPARE_FLD + ","
					+ ITM_ProductTable.LTSHIPPINGPREPARE_FLD + ","
					+ ITM_ProductTable.LTSALESATP_FLD + ","
					+ ITM_ProductTable.SHIPTOLERANCEID_FLD + ","
					+ ITM_ProductTable.BUYERID_FLD + ","
					+ ITM_ProductTable.BOMDESCRIPTION_FLD + ","
					+ ITM_ProductTable.BOMINCREMENT_FLD + ","
					+ ITM_ProductTable.ROUTINGDESCRIPTION_FLD + ","
					+ ITM_ProductTable.CREATEDATETIME_FLD + ","
					+ ITM_ProductTable.UPDATEDATETIME_FLD + ","
					+ ITM_ProductTable.COSTMETHOD_FLD + ","
					+ ITM_ProductTable.ROUTINGINCREMENT_FLD + ","
					+ ITM_ProductTable.CCNID_FLD + ","
					+ ITM_ProductTable.CATEGORYID_FLD + ","
					+ ITM_ProductTable.COSTCENTERID_FLD + ","
					+ ITM_ProductTable.DELETEREASONID_FLD + ","
					+ ITM_ProductTable.DELIVERYPOLICYID_FLD + ","
					+ ITM_ProductTable.FORMATCODEID_FLD + ","
					+ ITM_ProductTable.FREIGHTCLASSID_FLD + ","
					+ ITM_ProductTable.HAZARDID_FLD + ","
					+ ITM_ProductTable.ORDERPOLICYID_FLD + ","
					+ ITM_ProductTable.ORDERRULEID_FLD + ","
					+ ITM_ProductTable.SOURCEID_FLD + ","
					+ ITM_ProductTable.STOCKUMID_FLD + ","
					+ ITM_ProductTable.SELLINGUMID_FLD + ","
					+ ITM_ProductTable.HEIGHTUMID_FLD + ","
					+ ITM_ProductTable.WIDTHUMID_FLD + ","
					+ ITM_ProductTable.LENGTHUMID_FLD + ","
					+ ITM_ProductTable.BUYINGUMID_FLD + ","
					+ ITM_ProductTable.WEIGHTUMID_FLD + ","
					+ ITM_ProductTable.LOTSIZE_FLD + ","
					+ ITM_ProductTable.MASTERLOCATIONID_FLD + ","
					+ ITM_ProductTable.LOCATIONID_FLD + ","
					+ ITM_ProductTable.BINID_FLD + ","
					+ ITM_ProductTable.PRIMARYVENDORID_FLD + ","
					+ ITM_ProductTable.VENDORLOCATIONID_FLD + ","
					+ ITM_ProductTable.SAFETYSTOCK_FLD + ","
					+ ITM_ProductTable.AGCID_FLD + ","
					+ ITM_ProductTable.PARTNAMEVN_FLD + ","
					+ ITM_ProductTable.INVENTORID_FLD + ","
					+ ITM_ProductTable.LICENSEFEE_FLD + ","
					+ ITM_ProductTable.LISTPRICE_FLD + ","
					+ ITM_ProductTable.VENDORCURRENCYID_FLD + ","
					+ ITM_ProductTable.QUANTITYSET_FLD + ","
					+ ITM_ProductTable.TAXCODE_FLD + ","
					+ ITM_ProductTable.MINPRODUCE_FLD + ","
					+ ITM_ProductTable.MAXPRODUCE_FLD + ","
					+ ITM_ProductTable.PRODUCTTYPEID_FLD + ", "
					+ ITM_ProductTable.MAXROUNDUPTOMIN_FLD + ", "
					+ ITM_ProductTable.MAXROUNDUPTOMULTIPLE_FLD + ", "
					+ ITM_ProductTable.ACADJUSTMENTMASTERID_FLD + ", "
					+ ITM_ProductTable.REGISTEREDCODE_FLD + ", "
					+ ITM_ProductTable.ORDERPOINT_FLD
					+ " FROM " + ITM_ProductTable.TABLE_NAME
					+ " WHERE " + ITM_ProductTable.PRODUCTID_FLD + " IN (" + pstrIDs + ")";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);

				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(pdstData, ITM_ProductTable.TABLE_NAME);

				return;
			}
			catch (OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}
			catch (Exception ex)
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}
			finally
			{
				if (oconPCS != null)
				{
					if (oconPCS.State != ConnectionState.Closed)
					{
						oconPCS.Close();
					}
				}
			}
		}

		#endregion // END: DuongNA 2005-10-20

		#region HACKED: Thachnn: 01 / 03 / 2006 : add DB function to create data for ItemInformationReport

		/// <summary>
		/// Get report data for specific Product.
		/// When execute the SQL string, data adapter will return 5 table, Meta, Stock,BOm, Routing, StandardCost.
		/// We will name these table with provided names (get from Parameters).
		/// <author>Thachhnn: 01 03 2006 </author>
		/// </summary>
		/// <param name="pnProductID"Which Product to get info></param>
		/// <param name="pstrMetaDataTableName"></param>
		/// <param name="pstrStockStatusTableName"></param>
		/// <param name="pstrBOMTableName"></param>
		/// <param name="pstrStandardCostTableName"></param>
		/// <returns>DataSet with multiple datatable. This dataset will contain MetaDataTable, StockStatus, BOM, Routing, StandardCost</returns>		
		public DataSet GetItemInformationData(int pnProductID,
		                                      string pstrMetaDataTableName, string pstrStockStatusTableName, string pstrBOMTableName, string pstrRoutingTableName, string pstrStandardCostTableName)
		{
			const string METHOD_NAME = THIS + ".GetItemInformationData()";
			DataSet dstPCS = new DataSet();

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				// TODO: Thachnn: iimlement the SQL script to get data
				string strSql =
					" 					Declare @pstrProductID as int " +
						" Declare @OKBinType as int " +
						" Declare @NGBinType as int " +
						" Declare @DSBinType as int " +
						" Declare @BFBinType as int " +
						"  " +
						" Set @pstrProductID = " + pnProductID + " " +
						" Set @OKBinType = 1 " +
						" Set @NGBinType = 2 " +
						" Set @DSBinType = 3 " +
						" Set @BFBinType = 4 " +
						"  " +
						"  " +
						" /*********************************************************************************************************/ " +
						"  " +
						" SELECT   " +
						" product.ProductID, " +
						" product.Code as PartNo,  " +
						" product.Description as PartName,  " +
						" product.Revision as Model,  " +
						" product.SetupDate as SetupDate,  " +
						" product.StockTakingCode,  " +
						"  " +
						"  " +
						" product.MakeItem as MakeItem,  " +
						" stockUM.Code AS StockUnit,  " +
						" buyingUM.Code as BuyingUnit, " +
						" sellingUM.Code as SellingUnit, " +
						" costmethod.Description as CostMethod,  " +
						" masloc.Name + ' (' + masloc.Code + ')' AS MasterLocation, " +
						" location.Name + ' (' + location.Code + ')' AS Location,  " +
						" bin.Name + ' (' + bin.Code + ')' as Bin,  " +
						" product.ShelfLife as ShelfLife, " +
						"  " +
						" category.Code as Category, " +
						" source.Code as Source, " +
						" hazard.Description as Hazard, " +
						" deletereason.Description as DeleteReason, " +
						" producttype.Code as Type,  " +
						" product.TaxCode as TaxCode, " +
						" product.QuantitySet as QuantitySet, " +
						"  " +
						" inventor.Code as Licensor, " +
						" product.LicenseFee as LicenseFee, " +
						" product.OtherInfo1 as CustomsCode,  " +
						" product.OtherInfo2 as Specification, " +
						"  " +
						"  " +
						" product.Length as Length,  " +
						" lengthUM.Code AS LengthUM,  " +
						" product.Height as Height,  " +
						" heightUM.Code AS HeightUM,  " +
						" product.Width as Width,  " +
						" widthUM.Code AS WidthUM,  " +
						" product.Weight as Weight, 	 " +
						" weightUM.Code AS WeightUM,  " +
						"  " +
						"  " +
						" Case product.PlanType  " +
						"   when 1 then 'MRP' " +
						"   when 2 then 'MPS'  " +
						" End as Product_PlanType,  " +
						" delpolicy.Description AS DeliveryPolicy, " +
						" orderpolicy.Description OrderPolicy, " +
						" buyer.Name + ' (' + buyer.Code + ')' AS Buyer,  " +
						"  " +
						" vendor.Name + ' (' + vendor.Code + ')' AS VendorName,  " +
						" partyloc.Description + ' (' + partyloc.Code + ')' AS VendorLocation, " +
						"  " +
						" product.OrderQuantity as OrderQuantity,  " +
						" product.OrderQuantityMultiple as QuantityMultiple,  " +
						"  " +
						" product.ScrapPercent as ScrapPercent,  " +
						" product.SafetyStock as SafetyStock,  " +
						" product.MaximumStock as MaxStock,  " +
						" product.MinimumStock as MinStock,  " +
						" product.ListPrice as PurchasingPrice, " +
						" currency.Code as Currency, " +
						"  " +
						" product.LTFixedTime as LTFixed,  " +
						" product.LTDockToStock as LTDocToStock,  " +
						" product.LTVariableTime as LTVariable,  " +
						" product.LTSalesATP as LTSalesATP, " +
						" product.MinProduce, " +
						" product.MaxProduce, " +
						" product.VAT as VAT,  " +
						" product.ImportTax as ImportTax,  " +
						" product.ExportTax as ExportTax, " +
						" product.Picture as Picture " +
						"  " +
						" /* IGNORE */ " +
						" /*  " +
						" product.OtherInfo1 as Product_OtherInfo1,  " +
						" product.OtherInfo2 as Product_OtherInfo2,  " +
						" product.LTOrderPrepare as Product_LTOrderPrepare,  " +
						" product.LTShippingPrepare as Product_LTShippingPrepare,  " +
						" product.LotControl as Product_LotControl,  " +
						" product.SpecialTax as Product_SpecialTax,   " +
						"  " +
						" product.LTRequisition as Product_LTRequisition,  " +
						" product.LTSafetyStock as Product_LTSafetyStock,  " +
						" product.OrderPoint as Product_OrderPoint,  " +
						" product.FinishedGoods as Product_FinishedGoods,  " +
						" product.QAStatus as Product_QAStatus,  " +
						" product.Stock as Product_Stock,  " +
						"  " +
						" product.AutoConversion as Product_AutoConversion,  " +
						" product.ConversionTolerance as Product_ConversionTolerance,  " +
						" product.VoucherTolerance as Product_VoucherTolerance,  " +
						" product.ReceiveTolerance as Product_ReceiveTolerance,  " +
						" product.IssueSize as Product_IssueSize,  " +
						" product.BOMDescription as Product_BOMDescription,  " +
						" product.BomIncrement as Product_BomIncrement,  " +
						" product.CreateDateTime as Product_CreateDateTime,  " +
						" product.RoutingIncrement as Product_RoutingIncrement,  " +
						" product.LotSize as Product_LotSize, " +
						" stockUM.Description AS UM_Desc,  " +
						" */	/* END IGNORE */ " +
						"  " +
						" FROM   	(select * from ITM_Product where ProductID = @pstrProductID) product " +
						" 	LEFT JOIN ITM_Buyer buyer ON product.BuyerID = buyer.BuyerID  " +
						" 	LEFT JOIN MST_Party vendor ON product.PrimaryVendorID = vendor.PartyID  " +
						" 	LEFT JOIN MST_PartyLocation partyloc ON product.VendorLocationID = partyloc.PartyLocationID  " +
						" 	LEFT JOIN MST_UnitOfMeasure stockUM ON product.StockUMID = stockUM.UnitOfMeasureID  " +
						" 	LEFT JOIN MST_UnitOfMeasure buyingUM ON product.BuyingUMID = buyingUM.UnitOfMeasureID  " +
						" 	LEFT JOIN MST_UnitOfMeasure sellingUM ON product.SellingUMID = sellingUM.UnitOfMeasureID  " +
						" 	LEFT JOIN MST_UnitOfMeasure lengthUM ON product.LengthUMID = lengthUM.UnitOfMeasureID  " +
						" 	LEFT JOIN MST_UnitOfMeasure widthUM ON product.WidthUMID = widthUM.UnitOfMeasureID  " +
						" 	LEFT JOIN MST_UnitOfMeasure heightUM ON product.HeightUMID = heightUM.UnitOfMeasureID  " +
						" 	LEFT JOIN MST_UnitOfMeasure weightUM ON product.WeightUMID = weightUM.UnitOfMeasureID  " +
						" 	LEFT JOIN MST_MasterLocation masloc ON product.MasterLocationID = masloc.MasterLocationID  " +
						" 	LEFT JOIN MST_Location location ON product.LocationID = location.LocationID " +
						" 	LEFT JOIN ITM_OrderPolicy orderpolicy ON product.OrderPolicyID = orderpolicy.OrderPolicyID " +
						" 	LEFT JOIN ITM_DeliveryPolicy delpolicy ON product.DeliveryPolicyID = delpolicy.DeliveryPolicyID " +
						" 	LEFT JOIN MST_BIN bin ON product.BinID = bin.BinID " +
						" 	LEFT JOIN ITM_Category as category on product.CategoryID = category.CategoryID " +
						" 	LEFT JOIN ITM_Source as source on product.SourceID = source.SourceID " +
						" 	LEFT JOIN ITM_Hazard as hazard on product.HazardID = hazard.HazardID " +
						" 	LEFT JOIN ITM_DeleteReason as deletereason on product.DeletereasonID = Deletereason.DeletereasonID " +
						" 	LEFT JOIN ITM_ProductType as producttype on product.ProductTypeID = producttype.ProductTypeID " +
						" 	LEFT JOIN MST_Party as inventor on product.InventorID = inventor.PartyID " +
						" 	LEFT JOIN MST_Currency as currency on product.VendorCurrencyID = currency.CurrencyID " +
						"  LEFT JOIN ENM_CostMethod as costmethod on product.CostMethod = costmethod.Code " +
						"  " +
						"  " +
						"  " +
						"  " +
						"  " +
						"  " +
						" /*********************************************************************************************************/ " +
						"  " +
						"  " +
						"  " +
						"  " +
						" select " +
						" MasterLocation, " +
						" Location, " +
						" /* Bin, */ " +
						" ProductID, " +
						" Sum(BinOKQty) as BinOKQty, " +
						" Sum(BinBufferQty) as BinBufferQty, " +
						" Sum(BinNGQty) as BinNGQty, " +
						" Sum(BinDestroyQty) as BinDestroyQty " +
						"  " +
						" from  " +
						" ( /* begin innertable */ " +
						"  " +
						" /********** OK BIN Type **********************************/ " +
						" SELECT   " +
						" MST_MasterLocation.Code AS MasterLocation,  " +
						" MST_Location.Code AS Location,  " +
						" MST_BIN.Code as Bin, " +
						" IV_BinCache.ProductID, " +
						" IV_BinCache.OHQuantity as BinOKQty, " +
						" NULL as BinBufferQty, " +
						" NULL as BinNGQty, " +
						" NULL as BinDestroyQty " +
						"   " +
						" FROM    IV_BinCache INNER JOIN MST_BIN  " +
						" ON IV_BinCache.BinID = MST_BIN.BinID " +
						" INNER JOIN MST_Location  " +
						" ON MST_BIN.LocationID = MST_Location.LocationID  " +
						" INNER JOIN MST_MasterLocation  " +
						" ON MST_Location.MasterLocationID = MST_MasterLocation.MasterLocationID " +
						"   " +
						" WHERE MST_BIN.BinTypeID = @OKBinType " +
						"   " +
						" UNION  " +
						"   " +
						" /********** NG BIN Type **********************************/ " +
						" SELECT  MST_MasterLocation.Code AS MasterLocation,  " +
						"  MST_Location.Code AS Location,  " +
						"  MST_BIN.Code as Bin, " +
						"         IV_BinCache.ProductID, " +
						"  NULL as BinOKQty, " +
						"  NULL as BinBufferQty, " +
						"  IV_BinCache.OHQuantity as BinNGQty, " +
						"  NULL as BinDestroyQty " +
						"   " +
						" FROM    IV_BinCache  " +
						"  INNER JOIN MST_BIN ON IV_BinCache.BinID = MST_BIN.BinID " +
						"  INNER JOIN MST_Location ON MST_BIN.LocationID = MST_Location.LocationID  " +
						"  INNER JOIN MST_MasterLocation ON MST_Location.MasterLocationID = MST_MasterLocation.MasterLocationID " +
						"   " +
						" WHERE MST_BIN.BinTypeID = @NGBinType " +
						"   " +
						" UNION  " +
						"   " +
						" /********** DS BIN Type **********************************/ " +
						" SELECT  MST_MasterLocation.Code AS MST_MasterLocationCode,  " +
						"  MST_Location.Code AS MST_LocationCode,  " +
						"  MST_BIN.Code as MST_BINCode, " +
						"         IV_BinCache.ProductID, " +
						"  NULL as BinOKQty, " +
						"  NULL as BinBufferQty, " +
						"  NULL as BinNGQty, " +
						"  IV_BinCache.OHQuantity as BinDestroyQty " +
						"   " +
						" FROM    IV_BinCache  " +
						"  INNER JOIN MST_BIN ON IV_BinCache.BinID = MST_BIN.BinID " +
						"  INNER JOIN MST_Location ON MST_BIN.LocationID = MST_Location.LocationID  " +
						"  INNER JOIN MST_MasterLocation ON MST_Location.MasterLocationID = MST_MasterLocation.MasterLocationID " +
						"   " +
						" WHERE MST_BIN.BinTypeID = @DSBinType " +
						"   " +
						" UNION " +
						"   " +
						" /********** BF BIN Type **********************************/ " +
						" SELECT  MST_MasterLocation.Code AS MST_MasterLocationCode,  " +
						"  MST_Location.Code AS MST_LocationCode,  " +
						"  MST_BIN.Code as MST_BINCode, " +
						"         IV_BinCache.ProductID, " +
						"  NULL as BinOKQty, " +
						"  IV_BinCache.OHQuantity as BinBufferQty, " +
						"  NULL as BinNGQty, " +
						"  NULL as BinDestroyQty " +
						"   " +
						" FROM    IV_BinCache  " +
						"  INNER JOIN MST_BIN ON IV_BinCache.BinID = MST_BIN.BinID " +
						"  INNER JOIN MST_Location ON MST_BIN.LocationID = MST_Location.LocationID  " +
						"  INNER JOIN MST_MasterLocation ON MST_Location.MasterLocationID = MST_MasterLocation.MasterLocationID " +
						"   " +
						" WHERE MST_BIN.BinTypeID = @BFBinType " +
						"   " +
						" ) /* end innertable */ as INNERTABLE " +
						"  " +
						" where ProductID = @pstrProductID " +
						"  " +
						" Group by " +
						" MasterLocation, " +
						" Location, " +
						" /* Bin, */ " +
						" ProductID " +
						"  " +
						"  " +
						"  " +
						"  " +
						"  " +
						"  " +
						"  " +
						"  " +
						"  " +
						"  " +
						"  " +
						" /*********************************************************************************************************/ " +
						" /* BOM */ " +
						" SELECT  bom.ProductID,  " +
						" 	bom.COmponentID as ComponentID, " +
						" 	product.Code as PartNo,  " +
						" 	product.Description as PartName,  " +
						" 	product.Revision as Model,  " +
						" 	um.Code as StockUM, " +
						" 	category.Code as Category, " +
						" 	party.Code + ': ' + party.Name as Vendor,  " +
						" 	bom.Quantity as Quantity,  " +
						" 	bom.LeadTimeOffset as LTOffset,  " +
						" 	bom.Shrink as Shrink, " +
						" 	location.Code as Location,  " +
						" /*  " +
						" 	bom.EffectiveBeginDate as BOM_EffectiveBeginDate,  " +
						" 	bom.EffectiveEndDate as BOM_EffectiveEndDate,  " +
						" 	bom.EffectiveEndDay as BOM_EffectiveEndDay,  " +
						" 	bom.EffectiveBeginDay as BOM_EffectiveBeginDay,  " +
						" 	bom.Alternative as BOM_Alternative,  " +
						" 	bom.Line as BOM_Line, " +
						" 	party.Address as Party_Address, " +
						" 	routing.Step as Routing_Step, " +
						" 	vendoritem.VendorItem + '; ' + vendoritem.VendorItemRevision as VendorItem_VendorItem  " +
						" */ " +
						"  " +
						" 	isnull(availablequantity.Quantity,0.0) as AvailableQty " +
						"  " +
						"  " +
						" FROM  ITM_BOM bom  " +
						" LEFT JOIN ITM_Routing routing ON bom.RoutingID = routing.RoutingID  " +
						" LEFT JOIN (select * from ITM_Product where ProductID in  " +
						" 		(select ComponentID from ITM_Bom where ProductID = @pstrProductID)) product  " +
						" 	ON bom.ComponentID = product.ProductID  " +
						" LEFT JOIN MST_Party party ON product.PrimaryVendorID = party.PartyID " +
						" LEFT JOIN PO_ItemVendorReference vendoritem ON vendoritem.ProductID = product.ProductID AND vendoritem.PartyID = party.PartyID " +
						" LEFT JOIN MST_UnitOfMeasure um ON product.StockUMID = um.UnitOfMeasureID " +
						" LEFT JOIN ITM_Category as category on product.CategoryID = category.CategoryID " +
						" LEFT JOIN MST_Location as location on product.LocationID = location.LocationID " +
						" LEFT JOIN  " +
						" ( " +
						" 	SELECT   " +
						" 	IV_LocationCache.ProductID, " +
						" 	IV_LocationCache.LocationID, " +
						" 	IV_LocationCache.OHQuantity as Quantity " +
						" 	FROM    IV_LocationCache  " +
						" ) " +
						" as availablequantity  " +
						" on product.ProductID = availablequantity.ProductID " +
						" and  location.locationID = availablequantity.locationID " +
						"  " +
						"  " +
						" where bom.ProductID = @pstrProductID " +
						" order by [PartName] " +
						"  " +
						"  " +
						"  " +
						"  " +
						"  " +
						"  " +
						"  " +
						" /*********************************************************************************************************/ " +
						"  " +
						" /* Routing */ " +
						" SELECT  routing.ProductID, " +
						" routing.Step as RoutingStep,  " +
						" case routing.Type " +
						"   when 0 then 'Inside' " +
						"   when 1 then 'Outside' " +
						" end as Type,  " +
						" wc.Code as WorkCenter, " +
						" func.code as FunctionCode, " +
						" func.Description as FunctionName, " +
						" routing.MachineRunTime as MachineRun,  " +
						" routing.Machines as NoOfMachine,  " +
						" routing.LaborRunTime as LaborRun,  " +
						" routing.CrewSize as CrewSize,  " +
						" routing.Pacer, " +
						"  " +
						" CASE " +
						" WHEN routing.Type = 1 THEN routing.FixLT " +
						" WHEN routing.Type = 0 AND routing.Pacer = 'L' THEN routing.LaborRunTime  " +
						" WHEN routing.Type = 0 AND routing.Pacer = 'M' THEN routing.MachineRunTime  " +
						" WHEN routing.Type = 0 AND routing.Pacer = 'B' THEN routing.LaborRunTime + routing.MachineRunTime  " +
						" END as LeadTime, " +
						"  " +
						" party.Code  + ': ' + party.Name AS Vendor " +
						"  " +
						"  " +
						" FROM    ITM_Routing routing " +
						" 	LEFT JOIN MST_WorkCenter wc ON routing.WorkCenterID = wc.WorkCenterID  " +
						" 	LEFT JOIN MST_Function func ON routing.FunctionID = func.FunctionID  " +
						" 	LEFT JOIN ITM_RoutingStatus routingStatus ON routing.RoutingStatusID = routingStatus.RoutingStatusID " +
						" 	LEFT JOIN ITM_CostCenter laborCC ON routing.LaborCostCenterID = laborCC.CostCenterID " +
						" 	LEFT JOIN ITM_CostCenter machineCC ON routing.MachineCostCenterID = machineCC.CostCenterID " +
						" 	LEFT JOIN MST_Party party ON routing.PartyID = party.PartyID " +
						"  " +
						" where routing.ProductID = @pstrProductID " +
						"  " +
						" /*********************************************************************************************************/ " +
						"  " +
						"  " +
						"  " +
						" /*********************************************************************************************************/ " +
						" /* STANDARD COSTING */ " +
						" select  " +
						" cost.ProductID,  " +
						" element.Code as CostElement, " +
						" cost.Cost as Cost " +
						" from STD_CostElement element  " +
						" left join CST_STDItemCost cost  " +
						" on element.CostElementID = cost.CostElementID " +
						" where cost.ProductID = @pstrProductID " +
						"  " +
						" order by element.Code " +
						"  " +
						"  " +
						"  " +
						"  " +
						"  " +
						"  " +
						"  ";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dstPCS);

				dstPCS.Tables[0].TableName = pstrMetaDataTableName;
				dstPCS.Tables[1].TableName = pstrStockStatusTableName;
				dstPCS.Tables[2].TableName = pstrBOMTableName;
				dstPCS.Tables[3].TableName = pstrRoutingTableName;
				dstPCS.Tables[4].TableName = pstrStandardCostTableName;

				return dstPCS;
			}
			catch (OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}
			catch (IndexOutOfRangeException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}
			catch (Exception ex)
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}

			finally
			{
				if (oconPCS != null)
				{
					if (oconPCS.State != ConnectionState.Closed)
					{
						oconPCS.Close();
					}
				}
			}
		}

		#endregion ENDHACKED: Thachnn: 01 / 03 / 2006 : add DB function to create data for ItemInformationReport

		public string GetCategoryCodeByProductID(int pintProductId)
		{
			const string METHOD_NAME = THIS + ".GetCategoryCodeByProductID()";

			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;

				strSql = "SELECT ITM_Category.Code FROM ITM_Category JOIN ITM_Product"
					+ " ON ITM_Category.CategoryID = ITM_Product.CategoryID"
					+ " WHERE ITM_Product.ProductID = " + pintProductId;
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Connection.Open();

				object objResult = ocmdPCS.ExecuteScalar();
				if (objResult != null && objResult != DBNull.Value)
					return objResult.ToString();
				else
					return string.Empty;
			}
			catch (OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}
			catch (Exception ex)
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}

			finally
			{
				if (oconPCS != null)
				{
					if (oconPCS.State != ConnectionState.Closed)
					{
						oconPCS.Close();
					}
				}
			}
		}

		public bool CheckUniqueStockTakingCode(int pintProductID, string pstrStockTakingCode)
		{
			const string METHOD_NAME = THIS + ".CheckUniqueStockTakingCode()";
			
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = String.Empty;
				strSql = "SELECT ProductID "
					+ " FROM " + ITM_ProductTable.TABLE_NAME
					+ " WHERE " + ITM_ProductTable.STOCKTAKINGCODE_FLD + " = ?";
				
				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				ocmdPCS.Parameters.Add(new OleDbParameter(ITM_ProductTable.STOCKTAKINGCODE_FLD, OleDbType.VarWChar)).Value = pstrStockTakingCode;
				ocmdPCS.Connection.Open();

				DataTable dtbData = new DataTable();
				OleDbDataAdapter odadPCS = new OleDbDataAdapter(ocmdPCS);
				odadPCS.Fill(dtbData);
				bool blnResult = false;
				if (dtbData.Rows.Count == 0)
					blnResult = true;
				else if (dtbData.Rows.Count > 1)
					blnResult = false;
				else if (dtbData.Rows.Count == 1)
				{
					// check productID
					if (pintProductID.Equals(Convert.ToInt32(dtbData.Rows[0]["ProductID"])))
						blnResult = true;
					else
						blnResult = false;
				}
				return blnResult;
			}
			catch (OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}

			catch (Exception ex)
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}

			finally
			{
				if (oconPCS != null)
				{
					if (oconPCS.State != ConnectionState.Closed)
					{
						oconPCS.Close();
					}
				}
			}
		}

		public DataRow GetItemInfo(int pintProductID)
		{
			const string METHOD_NAME = THIS + ".GetItemInfo()";
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = "SELECT P.ProductID,"
					+ " C.Code AS ITM_CategoryCode, P.Code AS PartNumber, P.Description AS PartName"
					+ " , P.Revision AS Model, U.Code AS UM, P.StockUMID"
					+ " FROM ITM_Product P LEFT JOIN ITM_Category C ON P.CategoryID = C.CategoryID"
					+ " JOIN MST_UnitOfMeasure U ON P.StockUMID = U.UnitOfMeasureID"
					+ " WHERE P.ProductID = " + pintProductID;

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				OleDbDataAdapter odapPCS = new OleDbDataAdapter(ocmdPCS);
				DataTable dtbData = new DataTable();
				ocmdPCS.Connection.Open();

				odapPCS.Fill(dtbData);
				return dtbData.Rows[0];
			}
			catch (OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}
			catch (Exception ex)
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}
			finally
			{
				if (oconPCS != null)
				{
					if (oconPCS.State != ConnectionState.Closed)
					{
						oconPCS.Close();
					}
				}
			}
		}

		public DataTable ListOutsideItem()
		{
			const string METHOD_NAME = THIS + ".ListOutsideItem()";
			OleDbConnection oconPCS = null;
			OleDbCommand ocmdPCS = null;
			try
			{
				string strSql = "SELECT ProductID, StockUMID, ISNULL(SafetyStock,0) SafetyStock,"
					+ " MasterLocationID, LocationID, BinID"
					+ " FROM " + ITM_ProductTable.TABLE_NAME
					+ " WHERE " + ITM_ProductTable.MAKEITEM_FLD + "= 1"
					+ " AND " + ITM_ProductTable.PRIMARYVENDORID_FLD + " IS NOT NULL";

				Utils utils = new Utils();
				oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
				ocmdPCS = new OleDbCommand(strSql, oconPCS);
				OleDbDataAdapter odapPCS = new OleDbDataAdapter(ocmdPCS);
				DataTable dtbData = new DataTable();
				ocmdPCS.Connection.Open();

				odapPCS.Fill(dtbData);
				return dtbData;
			}
			catch (OleDbException ex)
			{
				throw new PCSDBException(ErrorCode.ERROR_DB, METHOD_NAME, ex);
			}
			catch (Exception ex)
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}
			finally
			{
				if (oconPCS != null)
				{
					if (oconPCS.State != ConnectionState.Closed)
					{
						oconPCS.Close();
					}
				}
			}
		}
	}
}
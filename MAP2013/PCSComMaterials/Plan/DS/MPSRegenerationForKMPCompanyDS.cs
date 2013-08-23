using System;
using System.Data;
using System.Data.OleDb;
using PCSComUtils.Common;
using PCSComUtils.DataAccess;

using PCSComUtils.PCSExc;

namespace PCSComMaterials.Plan.DS
{
    public class MPSRegenerationForKMPCompanyDS
    {
        private const string THIS = "PCSComMaterials.Plan.DS.MPSRegenerationForKMPCompanyDS";
        public DataSet GetList(int iMPSCycleOptionMasterIDID)
        {
            const string METHOD_NAME = THIS + ".GetList()";
            var dstPCS = new DataSet();

            OleDbConnection oconPCS = null;
            OleDbCommand ocmdPCS = null;
            try
            {
                string strSql = " select MRPCycleOptionMasterID,CP.ProductID,"
                              + "      Quantity,CP.DueDate,DemandQuantity "
                              + "      ,SupplyQuantity,NetAvailableQuantity "
                              + "      ,P.Code AS ProductCode,P.Description AS ProductName "
                              + "  from MTR_CPO AS CP "
                              + "  LEFT JOIN ITM_Product AS P ON P.ProductID=CP.ProductID "
                              + "  Where MPSCycleOptionMasterID=" + iMPSCycleOptionMasterIDID
                              + " Order By CP.DueDate ";

                oconPCS = new OleDbConnection(Utils.Instance.OleDbConnectionString);
                ocmdPCS = new OleDbCommand(strSql, oconPCS);
                ocmdPCS.Connection.Open();

                var odadPCS = new OleDbDataAdapter(ocmdPCS);
                odadPCS.Fill(dstPCS, ITM_BOMTable.TABLE_NAME);

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
    }
}

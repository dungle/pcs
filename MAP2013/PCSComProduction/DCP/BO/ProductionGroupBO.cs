using System.Data;
using PCSComProduction.DCP.DS;


namespace PCSComProduction.DCP.BO
{
	public class ProductionGroupBO
	{		
		public void UpdateDataSet(DataSet pdstData)
		{
			PRO_PGProductDS dsProductGroup = new PRO_PGProductDS();
			dsProductGroup.UpdateDataSet(pdstData);
		}
		#region ProductionLineBO's members
		
		/// <summary>
		/// Get list of Work center which belong to specific Production Line
		/// </summary>
		/// <param name="pintProductionLineID"></param>
		/// <returns></returns>
	
		public DataTable GetAllData()
		{
			PRO_PGProductDS dsProductGroup = new PRO_PGProductDS();
			return dsProductGroup.GetAllData();
		}		
		#endregion ProductionLineBO's members
	}
}

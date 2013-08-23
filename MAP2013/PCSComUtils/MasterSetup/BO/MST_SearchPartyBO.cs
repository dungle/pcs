using System.Data;
using PCSComUtils.MasterSetup.DS;


namespace PCSComUtils.MasterSetup.BO
{
    public  class MST_SearchPartyBO
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strTable"></param>
        /// <returns></returns>
        public int GetRowCount(string strTable,string strClause)
        {
            MST_SearchPartyDS obj = new MST_SearchPartyDS();
            return obj.GetRowCount(strTable,strClause);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strTable"></param>
        /// <param name="strClause"></param>
        /// <param name="iCurrentPage"></param>
        /// <param name="iCount"></param>
        /// <returns></returns>
        public DataSet GetList(string strTable,string strKey,bool check ,string strClause, int iCurrentPage, int iCount)
        {
            MST_SearchPartyDS obj = new MST_SearchPartyDS();
            return obj.GetList(strTable, strKey,check, strClause, iCurrentPage, iCount);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strTable"></param>
        /// <returns></returns>
        public DataSet GetListField(string strTable)
        {
            MST_SearchPartyDS obj = new MST_SearchPartyDS();
            return obj.GetListField(strTable);
        }
    }
}

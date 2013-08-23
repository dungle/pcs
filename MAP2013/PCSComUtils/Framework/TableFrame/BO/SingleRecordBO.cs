using System;
using System.Data;
using PCSComUtils.PCSExc;
using PCSComUtils.Framework.TableFrame.DS;

namespace PCSComUtils.Framework.TableFrame.BO
{
	/// <summary>
	/// Summary description for SingleRecordBO.
	/// </summary>
	public class SingleRecordBO
	{
		private const string THIS = "PCSComUtils.Framework.TableFrame.BO.SingleRecordBO";
		public SingleRecordBO()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		#region IObjectBO Members

		//**************************************************************************
		///    <Description>
		///       This method checks business rule and call Add() method of DS class 
		///    </Description>
		///    <Inputs>
		///       Value object    
		///    </Inputs>
		///    <Outputs>
		///      N/A   
		///    </Outputs>
		///    <Returns>
		///      void
		///    </Returns>
		///    <Authors>
		///       NgocHT
		///    </Authors>
		///    <History>
		///     27-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public void Add(object pObjectDetail)
		{
			// TODO:  Add SingleRecordBO.Add implementation
		}

		//**************************************************************************
		///    <Description>
		///       This method checks business rule and call Add() method of DS class 
		///    </Description>
		///    <Inputs>
		///       Value object    
		///    </Inputs>
		///    <Outputs>
		///      N/A   
		///    </Outputs>
		///    <Returns>
		///      void
		///    </Returns>
		///    <Authors>
		///       NgocHT
		///    </Authors>
		///    <History>
		///     27-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public void Delete(object pObjectVO)
		{
			// TODO:  Add SingleRecordBO.Delete implementation
		}

		//**************************************************************************
		///    <Description>
		///       This method checks business rule and call Add() method of DS class 
		///    </Description>
		///    <Inputs>
		///       Value object    
		///    </Inputs>
		///    <Outputs>
		///      N/A   
		///    </Outputs>
		///    <Returns>
		///      void
		///    </Returns>
		///    <Authors>
		///       NgocHT
		///    </Authors>
		///    <History>
		///     27-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public object GetObjectVO(int pintID, string VOclass)
		{
			// TODO:  Add SingleRecordBO.GetObjectVO implementation
			return null;
		}

		//**************************************************************************
		///    <Description>
		///       This method checks business rule and call Add() method of DS class 
		///    </Description>
		///    <Inputs>
		///       Value object    
		///    </Inputs>
		///    <Outputs>
		///      N/A   
		///    </Outputs>
		///    <Returns>
		///      void
		///    </Returns>
		///    <Authors>
		///       NgocHT
		///    </Authors>
		///    <History>
		///     27-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public void UpdateDataSet(DataSet dstData)
		{
			// TODO:  Add SingleRecordBO.UpdateDataSet implementation
		}

		//**************************************************************************
		///    <Description>
		///       This method checks business rule and call Add() method of DS class 
		///    </Description>
		///    <Inputs>
		///       Value object    
		///    </Inputs>
		///    <Outputs>
		///      N/A   
		///    </Outputs>
		///    <Returns>
		///      void
		///    </Returns>
		///    <Authors>
		///       NgocHT
		///    </Authors>
		///    <History>
		///     27-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public void Update(object pObjectDetail)
		{
			// TODO:  Add SingleRecordBO.Update implementation
		}

		#endregion

		//**************************************************************************
		///    <Description>
		///       This method use to get data from specified table
		///    </Description>
		///    <Inputs>
		///       TableName, FromField, FilterField1, FilterField2
		///    </Inputs>
		///    <Outputs>
		///      DataTable
		///    </Outputs>
		///    <Returns>
		///      DataTable
		///    </Returns>
		///    <Authors>
		///       DungLA
		///    </Authors>
		///    <History>
		///     02-Feb-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
	
		public DataTable GetDataFromTable(string pstrTableName, string pstrFromField, string pstrFilterField1, string pstrFilterField2)
		{
			try
			{
				sys_TableDS dsTable = new sys_TableDS();
				return dsTable.GetDataFromTable(pstrTableName, pstrFromField, pstrFilterField1, pstrFilterField2);
			}
			catch (PCSException ex)
			{
				throw ex.CauseException;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}

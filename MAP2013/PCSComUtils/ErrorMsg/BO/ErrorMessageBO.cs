using System;
using System.Data;
using PCSComUtils.ErrorMsg.DS;
using PCSComUtils.PCSExc;
using PCSComUtils.Common;

namespace PCSComUtils.ErrorMsg.BO
{
	/// <summary>
	/// Summary description for ErrorMessageBO.
	/// </summary>
	public class ErrorMessageBO
	{
		// private static DataSet dstErrorMessage = List();
		public ErrorMessageBO()
		{
			//
			// TODO: Add constructor logic here
			//
			
		}

		public void Add(object pobjObjectVO)
		{
			
		}

		public void Delete(object pobjObjectVO)
		{
			
		}

		public object GetObjectVO(int pintID,string VOClass)
		{
			return null;
		}

		public void Update(Object pobjObjecVO)
		{

		}

		public 	void UpdateDataSet(DataSet dstData){
		throw new Exception();
		}     
		
	
		public static DataSet List()
		{
			try
			{
				Sys_Error_MsgDS sys_Error_MsgDS = new Sys_Error_MsgDS();
				return sys_Error_MsgDS.List();
			}
			catch (PCSDBException ex)
			{
				throw ex;
			}
		}

		//**************************************************************************              
		///    <Description>
		///       This method uses to show message
		///    </Description>
		///    <Inputs>
		///        pintMessageCode : The code of Message       
		///    </Inputs>
		///    <Outputs>
		///       DialogResult 
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       TuanDM
		///    </Authors>
		///    <History>
		///       December 30, 2004
		///       Hoang Trung Son - iSphere software
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public static string GetErrorMessage(int pintCode, KindOfLanguage penumKindOfLanguage)
		{
			string strMessage = string.Empty;
			DataRow[] dr = ErrorMessage.dsErrorMessage.Select(Sys_Error_MsgTable.CODE_FLD + " = " + pintCode.ToString());
			switch (penumKindOfLanguage)
			{
				case KindOfLanguage.English:
					strMessage = dr[0][Sys_Error_MsgTable.MSGEN_FLD].ToString();
					break;
				case KindOfLanguage.VietNamese:
					strMessage = dr[0][Sys_Error_MsgTable.MSGVN_FLD].ToString();
					break;
				case KindOfLanguage.Japanese:
					strMessage = dr[0][Sys_Error_MsgTable.MSGJP_FLD].ToString();
					break;
				case KindOfLanguage.Default:
					strMessage = dr[0][Sys_Error_MsgTable.MSGDEFAULT_FLD].ToString();
					break;
			}
			return strMessage;
		}

		public static string GetErrorMessage(int pintCode)
		{
			string strMessage = string.Empty;
			DataRow[] dr = ErrorMessage.dsErrorMessage.Select("Code = " + pintCode);
            if (dr.Length > 0)
		        strMessage = dr[0][Sys_Error_MsgTable.MSGEN_FLD].ToString().Trim();
			return strMessage;
		}
	}
}

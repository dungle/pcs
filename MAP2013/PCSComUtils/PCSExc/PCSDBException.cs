using System;


namespace PCSComUtils.PCSExc
{
	/// <summary>
	/// Summary description for PCSDBException.
	/// </summary>
	
	public class PCSDBException : PCSException
	{
		public PCSDBException()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public PCSDBException(int code, string method, Exception ex)
		{
			mCode = code;
			mMethod = method;
			CauseException = ex;
		}
	}
}

using System;
using System.Collections;

namespace PCSComUtils.PCSExc
{
	/// <summary>
	/// Summary description for PCSBOException.
	/// </summary>
	
	public class PCSBOException : PCSException
	{
		public PCSBOException()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public PCSBOException(int code, string method, Exception ex)
		{
			mCode = code;
			mMethod = method;
			CauseException = ex;
		}

		public PCSBOException(int code, string method, Exception ex, Hashtable hash)
		{
			mCode = code;
			mMethod = method;
			CauseException = ex;
			Hash = hash;
		}
	}
}

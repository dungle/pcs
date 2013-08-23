using System;
using System.Collections;
using PCSComUtils.ErrorMsg.DS;

namespace PCSComUtils.PCSExc
{
	/// <summary>
	/// Summary description for PCSException.
	/// </summary>
	
	public class PCSException : Exception
	{
		public int mCode;
		public string mMethod;
		public Exception CauseException;
		public Hashtable Hash;

		public PCSException()
		{
			//
			// TODO: Add constructor logic here
			//
			Hash = new Hashtable();
		}

		
		/// <summary>
		/// constructor for PCSException class
		/// </summary>
		/// <param name="code">Error code</param>
		/// <param name="method">Method name which generates this error</param>
		/// <param name="ex">.net exception</param>
		public PCSException(int code, string method, Exception ex)
		{
			mCode = code;
			mMethod = method;
			CauseException = ex;
			Hash = new Hashtable();
		}

		public string getErrorMessage(int code, KindOfLanguage kindoflanguage)
		{
			return new Sys_Error_MsgDS().GetErrorMessage(code, kindoflanguage);
		}
	}
}

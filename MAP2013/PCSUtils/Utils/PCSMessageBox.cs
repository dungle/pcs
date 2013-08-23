// using System;
using System.Windows.Forms;
using PCSComUtils.Common;
using PCSComUtils.ErrorMsg.BO;
// using PCSComUtils.PCSExc;
using PCSUtils.Log;

namespace PCSUtils.Utils
{
	/// <summary>
	/// Summary description for PCSMessageBox.
	/// </summary>
	public class PCSMessageBox
	{
		private const string MESSAGE_SYSTEM_ERROR = "Can not connect to database, please check your network connection and make sure your sever is running";
		private const string ICON_REPLACE = "@";
		private const string METHOD_NAME = "PCSUtils.Utils.PCSMessageBox.Show()";
		private const string SPACE = " OR Lack of MessageCode: ";
		public static DialogResult Show(int pintMessageCode)
		{
			try
			{
				string strMessage = ErrorMessageBO.GetErrorMessage(pintMessageCode);
				return MessageBox.Show(strMessage, Constants.APPLICATION_NAME,MessageBoxButtons.OK,MessageBoxIcon.Information);
			}
			catch 
			{
				// log message.
				try
				{
					Logger.LogMessage(MESSAGE_SYSTEM_ERROR + SPACE + pintMessageCode, METHOD_NAME, Level.ERROR);
				}
				catch
				{
				}

				return MessageBox.Show(MESSAGE_SYSTEM_ERROR , Constants.APPLICATION_NAME,MessageBoxButtons.OK,MessageBoxIcon.Error);
			}
		}
		public static DialogResult Show(int pintBaseCodeMessage,int pintExtendCodeMessage)
		{
			const string OR = " OR ";
			try
			{
				string strBaseCodeMessage = ErrorMessageBO.GetErrorMessage(pintBaseCodeMessage);
				string strExtendCodeMessage = ErrorMessageBO.GetErrorMessage(pintExtendCodeMessage);
				string strMessage = strBaseCodeMessage.Replace(ICON_REPLACE,strExtendCodeMessage);
				return MessageBox.Show(strMessage, Constants.APPLICATION_NAME,MessageBoxButtons.OK,MessageBoxIcon.Error);
			}
			catch 
			{ 
				// log message.
				try
				{
					Logger.LogMessage(MESSAGE_SYSTEM_ERROR + SPACE + pintBaseCodeMessage, METHOD_NAME, Level.ERROR);
				}
				catch
				{
				}
				return MessageBox.Show(MESSAGE_SYSTEM_ERROR, Constants.APPLICATION_NAME,MessageBoxButtons.OK,MessageBoxIcon.Error);
			}
		}
		public static DialogResult Show(int pintMessageCode, MessageBoxButtons pobjButton)
		{
			try
			{
				string strMessage = ErrorMessageBO.GetErrorMessage(pintMessageCode);
				return MessageBox.Show(strMessage, Constants.APPLICATION_NAME,pobjButton,MessageBoxIcon.Information);
			}
			catch
			{
				// log message.
				try
				{
					Logger.LogMessage(MESSAGE_SYSTEM_ERROR + SPACE + pintMessageCode, METHOD_NAME, Level.ERROR);
				}
				catch
				{
				}

				return MessageBox.Show(MESSAGE_SYSTEM_ERROR , Constants.APPLICATION_NAME,MessageBoxButtons.OK,MessageBoxIcon.Error);
			}
		}
		public static DialogResult Show(int pintMessageCode,MessageBoxIcon pobjMessageBoxIcon)
		{
			try
			{
				string strMessage = ErrorMessageBO.GetErrorMessage(pintMessageCode);
				return MessageBox.Show(strMessage, Constants.APPLICATION_NAME,MessageBoxButtons.OK,pobjMessageBoxIcon);
			}
			catch
			{
				// log message.
				try
				{
					Logger.LogMessage(MESSAGE_SYSTEM_ERROR + SPACE + pintMessageCode, METHOD_NAME, Level.ERROR);
				}
				catch
				{
				}

				return MessageBox.Show(MESSAGE_SYSTEM_ERROR , Constants.APPLICATION_NAME,MessageBoxButtons.OK,MessageBoxIcon.Error);
			}
		}
		public static DialogResult Show(int pintMessageCode, MessageBoxButtons pobjButton, MessageBoxIcon pobjIcon)
		{
			try
			{
				string strMessage = ErrorMessageBO.GetErrorMessage(pintMessageCode);
				return MessageBox.Show(strMessage, Constants.APPLICATION_NAME,pobjButton, pobjIcon);
			}
			catch
			{
				// log message.
				try
				{
					Logger.LogMessage(MESSAGE_SYSTEM_ERROR + SPACE + pintMessageCode, METHOD_NAME, Level.ERROR);
				}
				catch
				{
				}

				return MessageBox.Show(MESSAGE_SYSTEM_ERROR, Constants.APPLICATION_NAME,MessageBoxButtons.OK,MessageBoxIcon.Error);
			}
		}
		public static DialogResult Show(int pintMessageCode, MessageBoxButtons pobjButton, MessageBoxIcon pobjIcon, MessageBoxDefaultButton pobjDefaultButton)
		{
			try
			{
				string strMessage = ErrorMessageBO.GetErrorMessage(pintMessageCode);
				return MessageBox.Show(strMessage, Constants.APPLICATION_NAME,pobjButton, pobjIcon, pobjDefaultButton);
			}
			catch
			{
				// log message.
				try
				{
					Logger.LogMessage(MESSAGE_SYSTEM_ERROR + SPACE + pintMessageCode, METHOD_NAME, Level.ERROR);
				}
				catch
				{
				}

				return MessageBox.Show(MESSAGE_SYSTEM_ERROR, Constants.APPLICATION_NAME,MessageBoxButtons.OK,MessageBoxIcon.Error);
			}
		}
		public static DialogResult Show(string pstrMessage)
		{
			return Show(pstrMessage, MessageBoxButtons.OK);
		}
        public static DialogResult Show(string pstrMessage, MessageBoxButtons pobjButton)
        {
            return Show(pstrMessage, pobjButton, MessageBoxIcon.Warning);
        }
        public static DialogResult Show(string pstrMessage, MessageBoxButtons pobjButton, MessageBoxIcon pobjIcon)
        {
            return Show(pstrMessage, pobjButton, pobjIcon, MessageBoxDefaultButton.Button1);
        }
        public static DialogResult Show(string pstrMessage, MessageBoxButtons pobjButton, MessageBoxIcon pobjIcon, MessageBoxDefaultButton pobjDefaultButton)
        {
            return MessageBox.Show(pstrMessage, Constants.APPLICATION_NAME, pobjButton, pobjIcon, pobjDefaultButton);
        }
		public static DialogResult Show(int pintMessageCode, MessageBoxButtons pobjButton, MessageBoxIcon pobjIcon, MessageBoxDefaultButton pobjDefaultButton, params string[] pstrParams)
		{
			const string OLD_CHAR = "@";
			try
			{
				string strMessage = ErrorMessageBO.GetErrorMessage(pintMessageCode);
				for(int i = 0; i < pstrParams.Length; i++)
				{
					int intIndex = strMessage.IndexOf(OLD_CHAR);
					strMessage = strMessage.Remove(intIndex,1);
					strMessage = strMessage.Insert(intIndex,pstrParams[i]);
					
				}
				return MessageBox.Show(strMessage, Constants.APPLICATION_NAME,pobjButton, pobjIcon, pobjDefaultButton);
			}
			catch
			{
				// log message.
				try
				{
					Logger.LogMessage(MESSAGE_SYSTEM_ERROR + SPACE + pintMessageCode, METHOD_NAME, Level.ERROR);
				}
				catch
				{
				}

				return MessageBox.Show(MESSAGE_SYSTEM_ERROR, Constants.APPLICATION_NAME,MessageBoxButtons.OK,MessageBoxIcon.Error);
			}
		}
		/// <summary>
		/// Show PCSMessage with parameters
		/// </summary>
		/// <param name="pintMessageCode"></param>
		/// <param name="pobjIcon"></param>
		/// <param name="pstrParams"></param>
		/// <returns></returns>
		/// <author>TuanDM 2005 - 14 - 10</author>
		public static DialogResult Show(int pintMessageCode, MessageBoxIcon pobjIcon, string[] pstrParams)
		{
			const string OLD_CHAR = "@";
			try
			{
				string strMessage = ErrorMessageBO.GetErrorMessage(pintMessageCode);
				for(int i = 0; i < pstrParams.Length; i++)
				{
					int intIndex = strMessage.IndexOf(OLD_CHAR);
					strMessage = strMessage.Remove(intIndex, OLD_CHAR.Length);
					strMessage = strMessage.Insert(intIndex, pstrParams[i]);
				}
				return MessageBox.Show(strMessage, Constants.APPLICATION_NAME, MessageBoxButtons.OK, pobjIcon);
			}
			catch
			{
				// log message.
				try
				{
					Logger.LogMessage(MESSAGE_SYSTEM_ERROR + SPACE + pintMessageCode, METHOD_NAME, Level.ERROR);
				}
				catch
				{
				}

				return MessageBox.Show(MESSAGE_SYSTEM_ERROR, Constants.APPLICATION_NAME,MessageBoxButtons.OK,MessageBoxIcon.Error);
			}
		}

	}
}

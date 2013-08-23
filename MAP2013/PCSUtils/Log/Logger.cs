/*
 * Sample Logger Usage
 * Logger.LogMessage("Message to log.", "MethodName()", Level.INFO);
 * TODO: Ask HieuLN to add constant for TAB_SEPARATOR and named it as LOG_SEPERATOR
 * TODO: Checkout Level.cs then add comment for the structor
 * */

using System;
using System.IO;
using System.Text;
using System.Security;
using PCSComUtils.PCSExc;
using PCSComUtils.Common;
using PCSUtils.Utils;

namespace PCSUtils.Log
{
	//**************************************************************************              
	///    <summary>
	///       Managing class to provide the interface for and control
	///       application logging. It utilizes the logging objects in
	///       PCSComUtils.Log to perform the actual logging as configured.
	///    </summary>
	///    <Inputs>
	///       
	///    </Inputs>
	///    <Outputs>
	///       
	///    </Outputs>
	///    <Returns>
	///       
	///    </Returns>
	///    <Authors>
	///       DungLA
	///    </Authors>
	///    <History>
	///       31-Dec-2004
	///    </History>
	///    <Notes>
	///    </Notes>
	//**************************************************************************
	public class Logger
	{
		/// <summary>
		/// Default log file name
		/// </summary>
		private const string LOG_FILENAME = @"PCS";
		/// <summary>
		/// Default log file extension
		/// </summary>
		private const string LOG_FILE_EXTENSION = ".log";
		/// <summary>
		/// Default log directory
		/// </summary>
		private static string LOG_DIR = System.Windows.Forms.Application.StartupPath + @"\Log\";
		/// <summary>
		/// Log file full path.
		/// </summary>
		private static string LogPath = string.Empty;

		private const string LINE_SEPARATOR = "*************************************************";
		private const string TAB_SEPARATOR = "|";
		private const string DATETIME_FORMAT = "yyyyMMdd";
		private const string THIS = "PCSUtils.Log.Logger";

		#region Public Properties
		/// <summary>
		/// Default constructor.
		/// </summary>
		public Logger()
		{
			
		}
		
		#endregion

		#region Public Static Methods
		
		//**************************************************************************              
		///    <summary>
		///       Log an Exception
		///    </summary>
		///    <Inputs>
		///       Exception to log.
		///       Error level.
		///    </Inputs>
		///    <Outputs>
		///       N/A
		///    </Outputs>
		///    <Returns>
		///       N/A
		///    </Returns>
		///    <Authors>
		///       DungLA
		///    </Authors>
		///    <History>
		///       31-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public static void LogMessage(Exception ex, string pstrMethod, Level pLevel)
		{
			const string METHOD_NAME = THIS + ".LogMessage()";
			FileStream fileStream = null;
			StreamWriter streamWriter = null;
			StringBuilder message = new StringBuilder();
			string strCreatedTime = DateTime.Now.ToString(DATETIME_FORMAT);
			string strFileName = string.Empty;

			try
			{
				// define the log file name "PCSYYYYMMDD.log"
				strFileName = LOG_FILENAME + strCreatedTime + LOG_FILE_EXTENSION;
				
                // determine whether the directory exists.
				if (!Directory.Exists(LOG_DIR))
				{
					//try to create new directory
					Directory.CreateDirectory(LOG_DIR);
				}

				// set the log path
				// log path = "C:\PCS\Log\logfile.log"				
				LogPath = LOG_DIR + strFileName;
				/// HACKED: Thachnn: 15/11/2005: remove readonly atrribute if any
				try
				{
					System.IO.File.SetAttributes(LogPath,FileAttributes.Archive);			
				}
				catch
				{
					/// if can't remove readonly attribute, nothing happen
					/// We don't produce any error of this action
				}
				/// ENDHACKED: Thachnn

				// Open the file if it exist; otherwise, a new file should be created.
				// by using the Append mode, we no need to catch FileNotFoundException
				// open file with Read shared in order to give other processes can read the file
				// while logging.
				fileStream = new FileStream(LogPath, FileMode.Append, FileAccess.Write, FileShare.Read);
				
				streamWriter = new StreamWriter(fileStream);

				if (ex != null)
				{ 
					// create the message
					message.Append(DateTime.Now.ToString()).Append(TAB_SEPARATOR).Append(pstrMethod).Append(TAB_SEPARATOR).Append(ex.Source).Append(TAB_SEPARATOR)
						.Append(pLevel.ToString()).Append(TAB_SEPARATOR).Append(ex.ToString());
				}
				else
				{
					// create the message with empty content
					message.Append(DateTime.Now.ToString()).Append(TAB_SEPARATOR).Append(pstrMethod).Append(TAB_SEPARATOR).Append(pLevel.ToString());
				}

				// force the write to the underlying file
				streamWriter.WriteLine(message.ToString());
				streamWriter.WriteLine(LINE_SEPARATOR);
				streamWriter.Flush();
			}
			catch (IOException exLog)
			{
				// the directory specified by path is read-only or is not empty.
				throw new PCSException(ErrorCode.LOG_IO_EXCEPTION, METHOD_NAME, exLog);
			}
			catch (SecurityException exLog)
			{
				// the caller does not have the required permission.
				throw new PCSException(ErrorCode.LOG_SECURITY_EXCEPTION, METHOD_NAME, exLog);
			}
			catch (UnauthorizedAccessException exLog)
			{
				// the access requested is not permitted by the operating system for the
				// specified path, such as when access is Write or ReadWrite and the file or
				// directory is set for read-only process.
				throw new PCSException(ErrorCode.LOG_UNAUTHORIZED, METHOD_NAME, exLog);
			}
			catch (Exception exLog)
			{
				// unhandled exception.
				throw new PCSException(ErrorCode.LOG_EXCEPTION, METHOD_NAME, exLog);
			}
			finally
			{
				if (streamWriter != null)
					streamWriter.Close();
			}
		}

		//**************************************************************************              
		///    <summary>
		///       Log a Message
		///    </summary>
		///    <Inputs>
		///       Message to log.
		///       Error level.
		///    </Inputs>
		///    <Outputs>
		///       N/A
		///    </Outputs>
		///    <Returns>
		///       N/A
		///    </Returns>
		///    <Authors>
		///       DungLA
		///    </Authors>
		///    <History>
		///       31-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public static void LogMessage(string pstrMsg, string pstrMethod, Level pLevel)
		{
			const string METHOD_NAME = THIS + ".LogMessage()";
			FileStream fileStream = null;
			StreamWriter streamWriter = null;
			StringBuilder message = new StringBuilder();
			string strCreatedTime = DateTime.Now.ToString(DATETIME_FORMAT);
			string strFileName = string.Empty;

			try
			{
				// define the log file name "PCSYYYYMMDD.log"
				strFileName = LOG_FILENAME + strCreatedTime + LOG_FILE_EXTENSION;
				
				// determine whether the directory exists.
				if (!Directory.Exists(LOG_DIR))
				{
					// try to create new directory
					Directory.CreateDirectory(LOG_DIR);
				}

				// set the log path
				// log path = "C:\PCS\Log\logfile.log"
				LogPath = LOG_DIR + strFileName;

				// Open the file if it exist; otherwise, a new file should be created.
				// by using the Append mode, we no need to catch FileNotFoundException
				fileStream = new FileStream(LogPath, FileMode.Append, FileAccess.Write);

				streamWriter = new StreamWriter(fileStream);

				// create the message
				message.Append(DateTime.Now.ToString()).Append(TAB_SEPARATOR).Append(pstrMethod).Append(TAB_SEPARATOR).Append(pLevel.ToString()).Append(TAB_SEPARATOR).Append(pstrMsg);

				// force the write to the underlying file
				streamWriter.WriteLine(message.ToString());
				streamWriter.WriteLine(LINE_SEPARATOR);
				streamWriter.Flush();
			}
			catch (IOException exLog)
			{
				// the directory specified by path is read-only or is not empty.
				throw new PCSBOException(ErrorCode.LOG_IO_EXCEPTION, METHOD_NAME, exLog);
			}
			catch (SecurityException exLog)
			{
				// the caller does not have the required permission.
				throw new PCSBOException(ErrorCode.LOG_SECURITY_EXCEPTION, METHOD_NAME, exLog);
			}
			catch (UnauthorizedAccessException exLog)
			{
				// the access requested is not permitted by the operating system for the
				// specified path, such as when access is Write or ReadWrite and the file or
				// directory is set for read-only process.
				throw new PCSBOException(ErrorCode.LOG_UNAUTHORIZED, METHOD_NAME, exLog);
			}
			catch (Exception exLog)
			{
				// unhandled exception.
				throw new PCSBOException(ErrorCode.LOG_EXCEPTION, METHOD_NAME, exLog);
			}
			finally
			{
				if (streamWriter != null)
					streamWriter.Close();
			}
		}
		#endregion
	}
}

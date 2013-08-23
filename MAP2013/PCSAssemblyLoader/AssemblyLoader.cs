using System;
using System.Reflection;

namespace PCSAssemblyLoader
{
	/// <summary>
	/// We need an interface in order to remoting
	/// </summary>
	public interface IAssemblyLoader
	{
		object Invoke(string pstrMethod, object[] pobjParameters);
	}
	/// <summary>
	/// Summary description for AssemblyLoader.
	/// </summary>
	public class AssemblyLoader : MarshalByRefObject
	{
		public const string THIS = "PCSUtils.Framework.ReportFrame.AssemblyLoader";
		private const BindingFlags bfi = BindingFlags.Instance | BindingFlags.Public | BindingFlags.CreateInstance;
		public AssemblyLoader()
		{}

		//**************************************************************************              
		///    <Description>
		///       Load the specified assembly to AppDomain
		///    </Description>
		///    <Inputs>
		///       assembly file name
		///    </Inputs>
		///    <Outputs>
		///       Loaded assembly
		///    </Outputs>
		///    <Returns>
		///       Assembly
		///    </Returns>
		///    <Authors>
		///       DungLA
		///    </Authors>
		///    <History>
		///       24-Jan-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public Type[] Load(string pstrAssemblyFile)
		{
			Assembly objAssembly = null;
			try
			{
				objAssembly = Assembly.Load(pstrAssemblyFile);
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return objAssembly.GetTypes();
		}
		//**************************************************************************              
		///    <Description>
		///       Create an remote interface of loader class to creating
		///       live class in new domain
		///    </Description>
		///    <Inputs>
		///       assembly file name, type name
		///    </Inputs>
		///    <Outputs>
		///       IAssemblyLoader
		///    </Outputs>
		///    <Returns>
		///       IAssemblyLoader
		///    </Returns>
		///    <Authors>
		///       DungLA
		///    </Authors>
		///    <History>
		///       25-Jan-2005
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public IAssemblyLoader Create(string pstrAssemblyFile, string pstrTypeName)
		{
			return (IAssemblyLoader)Activator.CreateInstanceFrom(pstrAssemblyFile,
				pstrTypeName, false, bfi, null, null, null, null, null).Unwrap();
		}
	}
}

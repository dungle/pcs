using System;
using System.ComponentModel;
using System.Collections;
using System.Diagnostics;
using System.Windows.Forms;
using PCSComUtils.Admin.DS;
using PCSComUtils.Framework.ReportFrame.DS;

namespace PCSUtils.Utils
{
	/// <summary>
	/// Override the .NET MenuItem Class
	/// Add some helpful properties to build the report list context menu more easyly and clearly
	/// </summary>
	public class ReportMenuItem : MenuItem
	{
		private Sys_PrintConfigurationVO mPrintConfigurationVO;
		private Form mContainerForm;
		private Sys_Menu_EntryVO mMenuVO;

		public Sys_PrintConfigurationVO PrintConfigurationVO
		{
			get
			{
				return mPrintConfigurationVO;
			}
			set
			{
				mPrintConfigurationVO = value;
			}
		}
		public Form ContainerForm
		{
			get
			{
				return mContainerForm;
			}
			set
			{
				mContainerForm = value;
			}
		}

		public Sys_Menu_EntryVO MenuVO
		{
			get
			{
				return mMenuVO;
			}
			set
			{
				mMenuVO = value;
			}
		}

	}

	#region // HACK: DEL Remove autogenerate code SonHT 2005-11-25

//	/// <summary>
//	/// Summary description for ReportMenuItem.
//	/// </summary>
//	public class ReportMenuItem : System.ComponentModel.Component
//	{
//		/// <summary>
//		/// Required designer variable.
//		/// </summary>
//		private System.ComponentModel.Container components = null;
//
//		public ReportMenuItem(System.ComponentModel.IContainer container)
//		{
//			///
//			/// Required for Windows.Forms Class Composition Designer support
//			///
//			container.Add(this);
//			InitializeComponent();
//
//			//
//			// TODO: Add any constructor code after InitializeComponent call
//			//
//		}
//
//		public ReportMenuItem()
//		{
//			///
//			/// Required for Windows.Forms Class Composition Designer support
//			///
//			InitializeComponent();
//
//			//
//			// TODO: Add any constructor code after InitializeComponent call
//			//
//		}
//
//		/// <summary> 
//		/// Clean up any resources being used.
//		/// </summary>
//		protected override void Dispose( bool disposing )
//		{
//			if( disposing )
//			{
//				if(components != null)
//				{
//					components.Dispose();
//				}
//			}
//			base.Dispose( disposing );
//		}
//
//
//		#region Component Designer generated code
//		/// <summary>
//		/// Required method for Designer support - do not modify
//		/// the contents of this method with the code editor.
//		/// </summary>
//		private void InitializeComponent()
//		{
//			components = new System.ComponentModel.Container();
//		}
//		#endregion
//	}
	#endregion // END: DEL SonHT 2005-11-25

}

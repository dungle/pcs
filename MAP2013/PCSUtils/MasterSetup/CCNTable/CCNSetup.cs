using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace PCSUtils.MasterSetup.CCNTable
{
	/// <summary>
	/// Summary description for CCNSetup.
	/// </summary>
	public class CCNSetup : System.Windows.Forms.Form
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public CCNSetup()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			// 
			// CCNSetup
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(292, 273);
			this.Name = "CCNSetup";
			this.Text = "CCNSetup";
			this.Load += new System.EventHandler(this.CCNSetup_Load);

		}
		#endregion

		private void CCNSetup_Load(object sender, System.EventArgs e)
		{
		// Code Inserted Automatically

		#region Code Inserted Automatically

		this.Cursor = Cursors.WaitCursor;

		#endregion Code Inserted Automatically

		
		

		// Code Inserted Automatically

		#region Code Inserted Automatically

		this.Cursor = Cursors.Default;

		#endregion Code Inserted Automatically

		
		}
	}
}

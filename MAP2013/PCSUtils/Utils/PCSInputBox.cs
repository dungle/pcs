using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using PCSComUtils.Common;

namespace PCSUtils.Utils
{

	#region InputBox return result

	/// <summary>
	/// Class used to store the result of an InputBox.Show message.
	/// </summary>
	public class PCSInputBoxResult 
	{
		public DialogResult ReturnCode;
		public object Value;
	}

	#endregion

	/// <summary>
	/// Summary description for InputBox.
	/// </summary>
	public class PCSInputBox
	{
		#region Private Windows Contols and Constructor

		// Create a new instance of the form.
		private static Form frmInputDialog;
		private static Label lblPrompt;
		private static Button btnOK;
		private static Button btnCancel;
		private static C1.Win.C1Input.C1NumericEdit txtInput;
		
		public PCSInputBox()
		{
		}

		#endregion

		#region Private Variables

		private static string mformCaption = string.Empty;
		private static string mformPrompt = string.Empty;
		private static PCSInputBoxResult moutputResponse = new PCSInputBoxResult();
		private static object mdefaultValue = DBNull.Value;
		private static int mxPos = -1;
		private static int myPos = -1;
		private static bool mInputNegative = false;

		#endregion

		#region Windows Form code

		private static void InitializeComponent()
		{
			// Create a new instance of the form.
			frmInputDialog = new Form();
			lblPrompt = new Label();
			btnOK = new Button();
			btnCancel = new Button();
			txtInput = new C1.Win.C1Input.C1NumericEdit();
			frmInputDialog.SuspendLayout();
			// 
			// lblPrompt
			// 
			lblPrompt.Anchor = ((AnchorStyles)((((AnchorStyles.Top | AnchorStyles.Bottom) | AnchorStyles.Left) | AnchorStyles.Right)));
			lblPrompt.BackColor = SystemColors.Control;
			lblPrompt.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, ((Byte)(0)));
			lblPrompt.Location = new Point(12, 9);
			lblPrompt.Name = "lblPrompt";
			lblPrompt.Size = new Size(302, 82);
			lblPrompt.TabIndex = 3;
			// 
			// btnOK
			// 
			btnOK.DialogResult = DialogResult.OK;
			btnOK.FlatStyle = FlatStyle.System;
			btnOK.Location = new Point(326, 8);
			btnOK.Name = "btnOK";
			btnOK.Size = new Size(64, 24);
			btnOK.TabIndex = 1;
			btnOK.Text = "&OK";
			btnOK.Click += new EventHandler(btnOK_Click);
			// 
			// btnCancel
			// 
			btnCancel.DialogResult = DialogResult.Cancel;
			btnCancel.FlatStyle = FlatStyle.System;
			btnCancel.Location = new Point(326, 40);
			btnCancel.Name = "btnCancel";
			btnCancel.Size = new Size(64, 24);
			btnCancel.TabIndex = 2;
			btnCancel.Text = "&Cancel";
			btnCancel.Click += new EventHandler(btnCancel_Click);
			// 
			// txtInput
			// 
			txtInput.Location = new Point(8, 100);
			txtInput.Name = "txtInput";
			txtInput.Size = new Size(379, 20);
			txtInput.TabIndex = 0;
			txtInput.Text = "";
			// 
			// InputBoxDialog
			// 
			frmInputDialog.AutoScaleBaseSize = new Size(5, 13);
			frmInputDialog.ClientSize = new Size(398, 128);
			frmInputDialog.Controls.Add(txtInput);
			frmInputDialog.Controls.Add(btnCancel);
			frmInputDialog.Controls.Add(btnOK);
			frmInputDialog.Controls.Add(lblPrompt);
			frmInputDialog.FormBorderStyle = FormBorderStyle.FixedDialog;
			frmInputDialog.MaximizeBox = false;
			frmInputDialog.MinimizeBox = false;
			frmInputDialog.Name = "InputBoxDialog";
			frmInputDialog.ResumeLayout(false);
		}

		#endregion

		#region Private function, InputBox Form move and change size

		static private bool LoadForm()
		{
			const string THIS = "PCSUtils.Utils.PCSInputBox";

			try
			{

				//Set form security
				Security objSecurity = new Security();
				frmInputDialog.Name = THIS;
				if (objSecurity.SetRightForUserOnForm(frmInputDialog, SystemProperty.UserName) == 0)
				{
					// You don't have the right to view this item
					PCSMessageBox.Show(ErrorCode.MESSAGE_YOU_HAVE_NO_RIGHT_TO_VIEW, MessageBoxIcon.Warning);					
					return false;
				}

				OutputResponse.ReturnCode = DialogResult.Ignore;
				OutputResponse.Value = DBNull.Value;

				txtInput.Value = mdefaultValue;
				txtInput.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.None;
				txtInput.FormatType = C1.Win.C1Input.FormatTypeEnum.CustomFormat;
				txtInput.CustomFormat = Constants.DECIMAL_NUMBERFORMAT;

				//Not allow input negative
				if(!mInputNegative)
				{
					txtInput.NumericInputKeys = ((C1.Win.C1Input.NumericInputKeyFlags)((((C1.Win.C1Input.NumericInputKeyFlags.F9 | C1.Win.C1Input.NumericInputKeyFlags.Plus) 
						| C1.Win.C1Input.NumericInputKeyFlags.Decimal) 
						| C1.Win.C1Input.NumericInputKeyFlags.X)));
				}

				lblPrompt.Text = mformPrompt;
				frmInputDialog.Text = mformCaption;
				frmInputDialog.AcceptButton = btnOK;
				frmInputDialog.CancelButton = btnCancel;

				// Retrieve the working rectangle from the Screen class
				// using the PrimaryScreen and the WorkingArea properties.
				System.Drawing.Rectangle workingRectangle = Screen.PrimaryScreen.WorkingArea;

				if((mxPos >= 0 && mxPos < workingRectangle.Width-100) && (myPos >= 0 && myPos < workingRectangle.Height-100))
				{
					frmInputDialog.StartPosition = FormStartPosition.Manual;
					frmInputDialog.Location = new System.Drawing.Point(mxPos, myPos);
				}
				else
				{
					frmInputDialog.StartPosition = FormStartPosition.CenterScreen;
				}

				string PrompText = lblPrompt.Text;

				int n = 0;
				int Index = 0;
				while(PrompText.IndexOf("\n",Index) > -1)
				{
					Index = PrompText.IndexOf("\n",Index)+1;
					n++;
				}

				if( n == 0 )
					n = 1;

				System.Drawing.Point Txt = txtInput.Location; 
				Txt.Y = Txt.Y + (n*4);
				txtInput.Location = Txt; 
				System.Drawing.Size form = frmInputDialog.Size; 
				form.Height = form.Height + (n*4);
				frmInputDialog.Size = form; 

				txtInput.SelectionStart = 0;
				txtInput.SelectionLength = txtInput.Text.Length;
				txtInput.Focus();

				return true;
			}
			catch
			{
				return false;
			}
		}

		#endregion

		#region Button control click event

		static private void btnOK_Click(object sender, System.EventArgs e)
		{
			try
			{
				if(txtInput.ValueIsDbNull || txtInput.Text == string.Empty)
				{
					OutputResponse.ReturnCode = DialogResult.Cancel;
					OutputResponse.Value = DBNull.Value;
					return;
				}
				OutputResponse.ReturnCode = DialogResult.OK;
				OutputResponse.Value = txtInput.Value;
				frmInputDialog.Dispose();
			}
			catch
			{}
		}

		static private void btnCancel_Click(object sender, System.EventArgs e)
		{
			try
			{
				OutputResponse.ReturnCode = DialogResult.Cancel;
				OutputResponse.Value = DBNull.Value;
				frmInputDialog.Dispose();			
			}
			catch
			{}
		}

		#endregion

		#region Public Static Show functions

		static public PCSInputBoxResult Show(string pstrPrompt)
		{
			try
			{
				InitializeComponent();
				FormPrompt = pstrPrompt;

				// Display the form as a modal dialog box.
				if(LoadForm())
				{
					frmInputDialog.ShowDialog();
				}
				return OutputResponse;
			}		
			catch
			{
				return OutputResponse;
			}
		}

		static public PCSInputBoxResult Show(string pstrPrompt, string pstrTitle)
		{
			try
			{
				InitializeComponent();

				FormCaption = pstrTitle;
				FormPrompt = pstrPrompt;

				// Display the form as a modal dialog box.				
				if(LoadForm())
				{
					frmInputDialog.ShowDialog();
				}
				return OutputResponse;
			}
			catch
			{
				return OutputResponse;
			}
		}

		static public PCSInputBoxResult Show(string pstrPrompt, string pstrTitle, bool pblnInputNegative)
		{
			try
			{
				InitializeComponent();

				InputNegative = pblnInputNegative;
				FormCaption = pstrTitle;
				FormPrompt = pstrPrompt;

				// Display the form as a modal dialog box.				
				if(LoadForm())
				{
					frmInputDialog.ShowDialog();
				}

				return OutputResponse;
			}
			catch
			{
				return OutputResponse;
			}
		}

		static public PCSInputBoxResult Show(string pstrPrompt, string pstrTitle, string pstrDefault)
		{
			try
			{
				InitializeComponent();

				FormCaption = pstrTitle;
				FormPrompt = pstrPrompt;
				DefaultValue = pstrDefault;

				// Display the form as a modal dialog box.				
				if(LoadForm())
				{
					frmInputDialog.ShowDialog();
				}
				return OutputResponse;
			}
			catch
			{
				return OutputResponse;
			}

		}
		
		static public PCSInputBoxResult Show(string pstrPrompt, string pstrTitle, string pstrDefault, bool pblnInputNegative)
		{
			try
			{
				InitializeComponent();

				InputNegative = pblnInputNegative;
				FormCaption = pstrTitle;
				FormPrompt = pstrPrompt;
				DefaultValue = pstrDefault;

				// Display the form as a modal dialog box.				
				if(LoadForm())
				{
					frmInputDialog.ShowDialog();
				}
				return OutputResponse;
			}
			catch
			{
				return OutputResponse;
			}

		}

		static public PCSInputBoxResult Show(string pstrPrompt, string pstrTitle, string pstrDefault, int pintXPos, int pintYPos)
		{
			try
			{
				InitializeComponent();
				FormCaption = pstrTitle;
				FormPrompt = pstrPrompt;
				DefaultValue = pstrDefault;
				XPosition = pintXPos;
				YPosition = pintYPos;

				// Display the form as a modal dialog box.				
				if(LoadForm())
				{
					frmInputDialog.ShowDialog();
				}
				return OutputResponse;
			}
			catch
			{
				return OutputResponse;
			}
		}

		#endregion

		#region Private Properties
		
		/// <summary>
		///  property FormCaption
		/// </summary>
		static private string FormCaption
		{
			set
			{
				mformCaption = value;
			}
			get
			{
				return mformCaption;
			}
		}

		static private bool InputNegative
		{
			set{ mInputNegative = value;}
			get{ return mInputNegative;}
		}
		
		/// <summary>
		/// property FormPrompt
		/// </summary>
		static private string FormPrompt
		{
			set{mformPrompt = value;}
			get{ return mformPrompt; }
		}

		static private PCSInputBoxResult OutputResponse
		{
			get
			{
				return moutputResponse;
			}
			set
			{
				moutputResponse = value;
			}
		} // property InputResponse
		
		static private string DefaultValue
		{
			set
			{
				mdefaultValue = value;
			}
		} // property DefaultValue

		static private int XPosition
		{
			set
			{
				if( value >= 0 )
					mxPos = value;
			}
		} // property XPos

		static private int YPosition
		{
			set
			{
				if( value >= 0 )
					myPos = value;
			}
		} // property YPos

		#endregion 
	}
}
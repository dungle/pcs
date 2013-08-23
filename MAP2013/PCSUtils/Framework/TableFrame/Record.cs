using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;


namespace PCSUtils.Framework.TableFrame
{
	/// <summary>
	/// Summary description for frmRecord.
	/// </summary>
	public class frmRecord : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button bntHelp;
		private System.Windows.Forms.Button bntEdit;
		private System.Windows.Forms.Button bntSave;
		private System.Windows.Forms.Button bntDelete;
		private System.Windows.Forms.Button bntAdd;
		private System.Windows.Forms.TextBox textBox5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.CheckBox checkBox1;
		private System.Windows.Forms.ComboBox comboBox1;
		private System.Windows.Forms.DateTimePicker dateTimePicker1;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textBox3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button btn_Close;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmRecord()
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
			this.bntHelp = new System.Windows.Forms.Button();
			this.bntEdit = new System.Windows.Forms.Button();
			this.bntSave = new System.Windows.Forms.Button();
			this.bntDelete = new System.Windows.Forms.Button();
			this.bntAdd = new System.Windows.Forms.Button();
			this.textBox5 = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
			this.label4 = new System.Windows.Forms.Label();
			this.textBox3 = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.btn_Close = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// bntHelp
			// 
			this.bntHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.bntHelp.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.bntHelp.Location = new System.Drawing.Point(347, 191);
			this.bntHelp.Name = "bntHelp";
			this.bntHelp.Size = new System.Drawing.Size(65, 20);
			this.bntHelp.TabIndex = 83;
			this.bntHelp.Text = "&Help";
			this.bntHelp.Click += new System.EventHandler(this.bntHelp_Click);
			// 
			// bntEdit
			// 
			this.bntEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.bntEdit.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.bntEdit.Location = new System.Drawing.Point(146, 191);
			this.bntEdit.Name = "bntEdit";
			this.bntEdit.Size = new System.Drawing.Size(65, 20);
			this.bntEdit.TabIndex = 82;
			this.bntEdit.Text = "&Edit";
			this.bntEdit.Click += new System.EventHandler(this.bntEdit_Click);
			// 
			// bntSave
			// 
			this.bntSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.bntSave.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.bntSave.Location = new System.Drawing.Point(79, 191);
			this.bntSave.Name = "bntSave";
			this.bntSave.Size = new System.Drawing.Size(65, 20);
			this.bntSave.TabIndex = 81;
			this.bntSave.Text = "&Save";
			this.bntSave.Click += new System.EventHandler(this.bntSave_Click);
			// 
			// bntDelete
			// 
			this.bntDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.bntDelete.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.bntDelete.Location = new System.Drawing.Point(213, 191);
			this.bntDelete.Name = "bntDelete";
			this.bntDelete.Size = new System.Drawing.Size(65, 20);
			this.bntDelete.TabIndex = 80;
			this.bntDelete.Text = "&Delete";
			this.bntDelete.Click += new System.EventHandler(this.bntDelete_Click);
			// 
			// bntAdd
			// 
			this.bntAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.bntAdd.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.bntAdd.Location = new System.Drawing.Point(12, 191);
			this.bntAdd.Name = "bntAdd";
			this.bntAdd.Size = new System.Drawing.Size(65, 20);
			this.bntAdd.TabIndex = 79;
			this.bntAdd.Text = "&Add";
			this.bntAdd.Click += new System.EventHandler(this.bntAdd_Click);
			// 
			// textBox5
			// 
			this.textBox5.Location = new System.Drawing.Point(92, 140);
			this.textBox5.Name = "textBox5";
			this.textBox5.Size = new System.Drawing.Size(199, 20);
			this.textBox5.TabIndex = 97;
			this.textBox5.Text = "Key Field";
			// 
			// label6
			// 
			this.label6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.label6.Location = new System.Drawing.Point(6, 140);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(86, 21);
			this.label6.TabIndex = 96;
			this.label6.Text = "Column name 7";
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(92, 116);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(199, 20);
			this.textBox1.TabIndex = 93;
			this.textBox1.Text = "Key Field";
			// 
			// label5
			// 
			this.label5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.label5.Location = new System.Drawing.Point(6, 116);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(86, 21);
			this.label5.TabIndex = 92;
			this.label5.Text = "Column name 6";
			// 
			// checkBox1
			// 
			this.checkBox1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.checkBox1.Location = new System.Drawing.Point(92, 92);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(106, 14);
			this.checkBox1.TabIndex = 91;
			this.checkBox1.Text = "checkBox1";
			// 
			// comboBox1
			// 
			this.comboBox1.ItemHeight = 13;
			this.comboBox1.Location = new System.Drawing.Point(92, 60);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(90, 21);
			this.comboBox1.TabIndex = 90;
			this.comboBox1.Text = "comboBox1";
			// 
			// dateTimePicker1
			// 
			this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.dateTimePicker1.Location = new System.Drawing.Point(92, 12);
			this.dateTimePicker1.Name = "dateTimePicker1";
			this.dateTimePicker1.Size = new System.Drawing.Size(90, 20);
			this.dateTimePicker1.TabIndex = 89;
			// 
			// label4
			// 
			this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.label4.Location = new System.Drawing.Point(6, 64);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(86, 21);
			this.label4.TabIndex = 88;
			this.label4.Text = "Column name 4";
			// 
			// textBox3
			// 
			this.textBox3.Location = new System.Drawing.Point(92, 36);
			this.textBox3.Name = "textBox3";
			this.textBox3.Size = new System.Drawing.Size(90, 20);
			this.textBox3.TabIndex = 87;
			this.textBox3.Text = "textBox3";
			// 
			// label2
			// 
			this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.label2.Location = new System.Drawing.Point(6, 92);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(86, 21);
			this.label2.TabIndex = 85;
			this.label2.Text = "Column name 5";
			// 
			// label1
			// 
			this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.label1.Location = new System.Drawing.Point(6, 12);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(86, 21);
			this.label1.TabIndex = 84;
			this.label1.Text = "Column name 1";
			// 
			// label3
			// 
			this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.label3.Location = new System.Drawing.Point(6, 36);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(86, 21);
			this.label3.TabIndex = 86;
			this.label3.Text = "Column name 2";
			// 
			// btn_Close
			// 
			this.btn_Close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btn_Close.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btn_Close.Location = new System.Drawing.Point(280, 191);
			this.btn_Close.Name = "btn_Close";
			this.btn_Close.Size = new System.Drawing.Size(65, 20);
			this.btn_Close.TabIndex = 80;
			this.btn_Close.Text = "&Close";
			this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
			// 
			// frmRecord
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(424, 225);
			this.Controls.Add(this.textBox5);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.textBox3);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.checkBox1);
			this.Controls.Add(this.comboBox1);
			this.Controls.Add(this.dateTimePicker1);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.bntHelp);
			this.Controls.Add(this.bntEdit);
			this.Controls.Add(this.bntSave);
			this.Controls.Add(this.bntDelete);
			this.Controls.Add(this.bntAdd);
			this.Controls.Add(this.btn_Close);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmRecord";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "table name ";
			this.Load += new System.EventHandler(this.frmRecord_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void frmRecord_Load(object sender, System.EventArgs e)
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

		private void btn_Close_Click(object sender, System.EventArgs e)
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

		private void bntSave_Click(object sender, System.EventArgs e)
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

		private void bntEdit_Click(object sender, System.EventArgs e)
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

		private void bntDelete_Click(object sender, System.EventArgs e)
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

		private void bntHelp_Click(object sender, System.EventArgs e)
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

		private void bntAdd_Click(object sender, System.EventArgs e)
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

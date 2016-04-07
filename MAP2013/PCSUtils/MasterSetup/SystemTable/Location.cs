using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using C1.Win.C1List;
using PCSComUtils.Common;
using PCSComUtils.Common.BO;
using PCSComUtils.MasterSetup.BO;
using PCSComUtils.MasterSetup.DS;
using PCSComUtils.PCSExc;
using PCSUtils.Log;
using PCSUtils.Utils;

namespace PCSUtils.MasterSetup.SystemTable
{
	/// <summary>
	/// Summary description for Location.
	/// </summary>
	public class Location : Form
	{
		#region Declaration
		
		#region Form controls

		private Button btnClose;
		private Button btnHelp;
		private Button btnAdd;
		private Button btnSave;
		private Button btnDelete;
		private Button btnEdit;
		private Label lblCountry;
		private TextBox txtZIP;
		private Label lblZIP;
		private TextBox txtState;
		private Label lblState;
		private Label lblCity;
		private TextBox txtAddress;
		private Label lblAddress;
		private Button btnContact;
		private TextBox txtDescription;
		private Label lblDescription;
		private Label lblCode;
		private TextBox txtPartyCode;
		private TextBox txtLocationCode;

		#endregion

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private Container components = null;

		private const string THIS = "PCSUtils.MasterSetup.SystemTable.Location";
		private MST_PartyVO voParty;
		private MST_PartyLocationVO voPartyLocation = new MST_PartyLocationVO();
		private EnumAction mFormMode = EnumAction.Default;
		private DataTable tblCityData;
		private Button btnSearchCode;
		UtilsBO boUtils = new UtilsBO();
		LocationBO boLocation = new LocationBO();
		private Label lblParty;
		private int intCountryID = 0;
		private C1.Win.C1List.C1Combo cboCity;
		private C1.Win.C1List.C1Combo cboCountry;

		private int intCityID = 0;
		private bool blnDataIsValid = false;
		
		#endregion Declaration

		#region Properties
		
		public int DefaultCountryID
		{
			set { intCountryID = value;}
		}
		public int DefaultCityID
		{
			set { intCityID = value;}
		}
		

		public EnumAction FormMode
		{
			get { return mFormMode; }
			set { mFormMode = value; }
		}

		public Location()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		public Location(MST_PartyVO pvoParty)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			voParty = pvoParty;
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
		
		#endregion Properties

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Location));
            this.txtPartyCode = new System.Windows.Forms.TextBox();
            this.lblParty = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnHelp = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.lblCountry = new System.Windows.Forms.Label();
            this.txtZIP = new System.Windows.Forms.TextBox();
            this.lblZIP = new System.Windows.Forms.Label();
            this.txtState = new System.Windows.Forms.TextBox();
            this.lblState = new System.Windows.Forms.Label();
            this.lblCity = new System.Windows.Forms.Label();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.lblAddress = new System.Windows.Forms.Label();
            this.btnContact = new System.Windows.Forms.Button();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblCode = new System.Windows.Forms.Label();
            this.txtLocationCode = new System.Windows.Forms.TextBox();
            this.btnSearchCode = new System.Windows.Forms.Button();
            this.cboCity = new C1.Win.C1List.C1Combo();
            this.cboCountry = new C1.Win.C1List.C1Combo();
            ((System.ComponentModel.ISupportInitialize)(this.cboCity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboCountry)).BeginInit();
            this.SuspendLayout();
            // 
            // txtPartyCode
            // 
            resources.ApplyResources(this.txtPartyCode, "txtPartyCode");
            this.txtPartyCode.Name = "txtPartyCode";
            this.txtPartyCode.ReadOnly = true;
            // 
            // lblParty
            // 
            this.lblParty.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblParty.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.lblParty, "lblParty");
            this.lblParty.Name = "lblParty";
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.Name = "btnClose";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnHelp
            // 
            resources.ApplyResources(this.btnHelp, "btnHelp");
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // btnAdd
            // 
            resources.ApplyResources(this.btnAdd, "btnAdd");
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnSave
            // 
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.Name = "btnSave";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnDelete
            // 
            resources.ApplyResources(this.btnDelete, "btnDelete");
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnEdit
            // 
            resources.ApplyResources(this.btnEdit, "btnEdit");
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // lblCountry
            // 
            this.lblCountry.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblCountry.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.lblCountry, "lblCountry");
            this.lblCountry.Name = "lblCountry";
            // 
            // txtZIP
            // 
            resources.ApplyResources(this.txtZIP, "txtZIP");
            this.txtZIP.Name = "txtZIP";
            this.txtZIP.Enter += new System.EventHandler(this.OnEnterControl);
            this.txtZIP.Leave += new System.EventHandler(this.OnLeaveControl);
            // 
            // lblZIP
            // 
            this.lblZIP.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblZIP.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.lblZIP, "lblZIP");
            this.lblZIP.Name = "lblZIP";
            // 
            // txtState
            // 
            resources.ApplyResources(this.txtState, "txtState");
            this.txtState.Name = "txtState";
            this.txtState.Enter += new System.EventHandler(this.OnEnterControl);
            this.txtState.Leave += new System.EventHandler(this.OnLeaveControl);
            // 
            // lblState
            // 
            this.lblState.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblState.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.lblState, "lblState");
            this.lblState.Name = "lblState";
            // 
            // lblCity
            // 
            this.lblCity.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblCity.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.lblCity, "lblCity");
            this.lblCity.Name = "lblCity";
            // 
            // txtAddress
            // 
            resources.ApplyResources(this.txtAddress, "txtAddress");
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Enter += new System.EventHandler(this.OnEnterControl);
            this.txtAddress.Leave += new System.EventHandler(this.OnLeaveControl);
            // 
            // lblAddress
            // 
            this.lblAddress.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblAddress.ForeColor = System.Drawing.Color.Maroon;
            resources.ApplyResources(this.lblAddress, "lblAddress");
            this.lblAddress.Name = "lblAddress";
            // 
            // btnContact
            // 
            resources.ApplyResources(this.btnContact, "btnContact");
            this.btnContact.Name = "btnContact";
            this.btnContact.Click += new System.EventHandler(this.btnContact_Click);
            // 
            // txtDescription
            // 
            resources.ApplyResources(this.txtDescription, "txtDescription");
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Enter += new System.EventHandler(this.OnEnterControl);
            this.txtDescription.Leave += new System.EventHandler(this.OnLeaveControl);
            // 
            // lblDescription
            // 
            this.lblDescription.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblDescription.ForeColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.lblDescription, "lblDescription");
            this.lblDescription.Name = "lblDescription";
            // 
            // lblCode
            // 
            this.lblCode.ForeColor = System.Drawing.Color.Maroon;
            resources.ApplyResources(this.lblCode, "lblCode");
            this.lblCode.Name = "lblCode";
            // 
            // txtLocationCode
            // 
            resources.ApplyResources(this.txtLocationCode, "txtLocationCode");
            this.txtLocationCode.Name = "txtLocationCode";
            this.txtLocationCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtLocationCode_KeyDown);
            this.txtLocationCode.Validating += new System.ComponentModel.CancelEventHandler(this.txtLocationCode_Validating);
            // 
            // btnSearchCode
            // 
            resources.ApplyResources(this.btnSearchCode, "btnSearchCode");
            this.btnSearchCode.Name = "btnSearchCode";
            this.btnSearchCode.Click += new System.EventHandler(this.btnSearchCode_Click);
            // 
            // cboCity
            // 
            this.cboCity.AddItemSeparator = ';';
            resources.ApplyResources(this.cboCity, "cboCity");
            this.cboCity.CaptionHeight = 17;
            this.cboCity.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.cboCity.ColumnCaptionHeight = 17;
            this.cboCity.ColumnFooterHeight = 17;
            this.cboCity.ComboStyle = C1.Win.C1List.ComboStyleEnum.DropdownList;
            this.cboCity.DeadAreaBackColor = System.Drawing.Color.Empty;
            this.cboCity.EditorBackColor = System.Drawing.SystemColors.Window;
            this.cboCity.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboCity.EditorForeColor = System.Drawing.SystemColors.WindowText;
            this.cboCity.Images.Add(((System.Drawing.Image)(resources.GetObject("cboCity.Images"))));
            this.cboCity.ItemHeight = 15;
            this.cboCity.MatchEntryTimeout = ((long)(2000));
            this.cboCity.MaxDropDownItems = ((short)(5));
            this.cboCity.MaxLength = 32767;
            this.cboCity.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.cboCity.Name = "cboCity";
            this.cboCity.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.cboCity.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.cboCity.Enter += new System.EventHandler(this.OnEnterControl);
            this.cboCity.Leave += new System.EventHandler(this.OnLeaveControl);
            this.cboCity.PropBag = resources.GetString("cboCity.PropBag");
            // 
            // cboCountry
            // 
            this.cboCountry.AddItemSeparator = ';';
            resources.ApplyResources(this.cboCountry, "cboCountry");
            this.cboCountry.CaptionHeight = 17;
            this.cboCountry.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.cboCountry.ColumnCaptionHeight = 17;
            this.cboCountry.ColumnFooterHeight = 17;
            this.cboCountry.ComboStyle = C1.Win.C1List.ComboStyleEnum.DropdownList;
            this.cboCountry.DeadAreaBackColor = System.Drawing.Color.Empty;
            this.cboCountry.EditorBackColor = System.Drawing.SystemColors.Window;
            this.cboCountry.EditorFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboCountry.EditorForeColor = System.Drawing.SystemColors.WindowText;
            this.cboCountry.Images.Add(((System.Drawing.Image)(resources.GetObject("cboCountry.Images"))));
            this.cboCountry.ItemHeight = 15;
            this.cboCountry.MatchEntryTimeout = ((long)(2000));
            this.cboCountry.MaxDropDownItems = ((short)(5));
            this.cboCountry.MaxLength = 32767;
            this.cboCountry.MouseCursor = System.Windows.Forms.Cursors.Default;
            this.cboCountry.Name = "cboCountry";
            this.cboCountry.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None;
            this.cboCountry.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.cboCountry.SelectedValueChanged += new System.EventHandler(this.cboCountry_SelectedIndexChanged);
            this.cboCountry.Enter += new System.EventHandler(this.OnEnterControl);
            this.cboCountry.Leave += new System.EventHandler(this.OnLeaveControl);
            this.cboCountry.PropBag = resources.GetString("cboCountry.PropBag");
            // 
            // Location
            // 
            resources.ApplyResources(this, "$this");
            this.CancelButton = this.btnClose;
            this.Controls.Add(this.cboCity);
            this.Controls.Add(this.cboCountry);
            this.Controls.Add(this.btnSearchCode);
            this.Controls.Add(this.txtLocationCode);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.txtPartyCode);
            this.Controls.Add(this.txtAddress);
            this.Controls.Add(this.txtZIP);
            this.Controls.Add(this.txtState);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.lblCode);
            this.Controls.Add(this.btnContact);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.lblParty);
            this.Controls.Add(this.lblCity);
            this.Controls.Add(this.lblAddress);
            this.Controls.Add(this.lblCountry);
            this.Controls.Add(this.lblZIP);
            this.Controls.Add(this.lblState);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "Location";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.Location_Closing);
            this.Load += new System.EventHandler(this.Location_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Location_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.cboCity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboCountry)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		#region Private Methods
		private void InitComboBoxs()
		{
			try
			{
				// get all city first
				tblCityData = boUtils.ListAllCity();
				DataTable tblCountry = boUtils.ListCountry();
				FormControlComponents.PutDataIntoC1ComboBox(cboCountry, tblCountry, MST_CountryTable.CODE_FLD, MST_CountryTable.COUNTRYID_FLD, MST_CountryTable.TABLE_NAME, true);
				//cboCountry.Items.Insert(0, string.Empty);
				if (intCountryID > 0)
				{
					cboCountry.SelectedValue = intCountryID;
					if (intCityID > 0)
						cboCity.SelectedValue = intCityID;
					else
						cboCity.SelectedIndex = -1;
				}
				else
					cboCountry.SelectedIndex = -1;
			}
			catch (PCSException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		private void SwitchFormMode()
		{
			try
			{
				switch (mFormMode)
				{
					
					case EnumAction.Default:
						foreach (Control objControl in this.Controls)
						{
							if ((objControl is TextBox) || (objControl is ComboBox) 
								|| (objControl is C1Combo) || (objControl is CheckBox))
							{
								if (objControl != txtLocationCode)
									objControl.Enabled = false;
							}
						}
						btnAdd.Enabled = true;
						btnSave.Enabled = false;
						if (voPartyLocation.PartyLocationID > 0)
						{
							btnEdit.Enabled = true;
							btnContact.Enabled = true;
							btnDelete.Enabled = true;
						}
						else
						{
							btnContact.Enabled = false;
							btnEdit.Enabled = false;
							btnDelete.Enabled = false;
						}
						btnSearchCode.Enabled = true;
						
						break;

					default:
						foreach (Control objControl in this.Controls)
						{
							if ((objControl is TextBox) || (objControl is ComboBox) 
								|| (objControl is C1Combo) || (objControl is CheckBox))
								objControl.Enabled = true;
						}
						txtPartyCode.Enabled = false;
						txtLocationCode.Enabled = true && (mFormMode != EnumAction.Default);
						btnSearchCode.Enabled = false;
						btnContact.Enabled = false;
						btnAdd.Enabled = false;
						btnSave.Enabled = true;
						btnEdit.Enabled = false;
						btnDelete.Enabled = false;
						
						break;					
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		private void LoadDataToControl(MST_PartyLocationVO pvoPartyLocation)
		{
			try
			{
				if ((pvoPartyLocation != null) && (pvoPartyLocation.PartyLocationID > 0))
				{ 
					// load all data from VO to controls
					txtLocationCode.Text = pvoPartyLocation.Code;
					txtDescription.Text = pvoPartyLocation.Description;
					txtAddress.Text = pvoPartyLocation.Address;
					cboCountry.SelectedValue = pvoPartyLocation.CountryID;
					cboCity.SelectedValue = pvoPartyLocation.CityID;
					txtState.Text = pvoPartyLocation.State;
					txtZIP.Text = pvoPartyLocation.ZipPost;
				}
				else
				{
					// clear data in form
					foreach (Control objControl in this.Controls)
					{
						if (objControl is TextBox)
						{
							objControl.Text = string.Empty;
						}
						
						if (objControl is ComboBox)
						{
							((ComboBox)objControl).SelectedIndex = -1;
						}

						if (objControl is C1.Win.C1List.C1Combo)
						{
							((C1.Win.C1List.C1Combo)objControl).SelectedIndex = -1;							
						}
					}
				}

				txtPartyCode.Text = voParty.Code;
				txtPartyCode.Tag = voParty;
			}
			catch (PCSException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		private bool SaveData()
		{
            voPartyLocation.PartyID = voParty.PartyID;
            voPartyLocation.Code = txtLocationCode.Text.Trim();
            voPartyLocation.Description = txtDescription.Text.Trim();
            voPartyLocation.Address = txtAddress.Text.Trim();
            try
            {
                voPartyLocation.CountryID = int.Parse(cboCountry.SelectedValue.ToString().Trim());
            }
            catch { }
            try
            {
                voPartyLocation.CityID = int.Parse(cboCity.SelectedValue.ToString().Trim());
            }
            catch { }
            voPartyLocation.State = txtState.Text.Trim();
            voPartyLocation.ZipPost = txtZIP.Text.Trim();
            switch (mFormMode)
            {
                case EnumAction.Add:
                    voPartyLocation.PartyLocationID = boLocation.AddReturnID(voPartyLocation);
                    break;
                case EnumAction.Edit:
                    boLocation.Update(voPartyLocation);
                    break;
            }
            return true;
        }
		private void PutDataToCityCombo(DataRow[] pdrowData)
		{
			try
			{
				// clear data source and related info first
				cboCity.DataSource = null;
				DataTable tblData = new DataTable(MST_CityTable.TABLE_NAME);
				tblData.Columns.Add(MST_CityTable.CITYID_FLD);
				tblData.Columns.Add(MST_CityTable.CODE_FLD);
				tblData.Columns.Add(MST_CityTable.NAME_FLD);
				foreach (DataRow drowTemp in pdrowData)
				{
					DataRow drowNew = tblData.NewRow();
					drowNew[MST_CityTable.CITYID_FLD] = drowTemp[MST_CityTable.CITYID_FLD];
					drowNew[MST_CityTable.CODE_FLD] = drowTemp[MST_CityTable.CODE_FLD];
					drowNew[MST_CityTable.NAME_FLD] = drowTemp[MST_CityTable.NAME_FLD];
					tblData.Rows.Add(drowNew);
				}
				FormControlComponents.PutDataIntoC1ComboBox(cboCity, tblData, MST_CityTable.CODE_FLD, MST_CityTable.CITYID_FLD, MST_CityTable.TABLE_NAME, true);
				//cboCity.Items.Insert(0, string.Empty);
			}
			catch (PCSException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		private void OnEnterControl(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".OnEnterControl()";
			try
			{
				FormControlComponents.OnEnterControl(sender, e);
			}
			catch (Exception ex)
			{
				// displays the error message.
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
				// log message.
				try
				{
					Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}
		private void OnLeaveControl(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".OnLeaveControl()";
			try
			{
				FormControlComponents.OnLeaveControl(sender, e);
			}
			catch (Exception ex)
			{
				// displays the error message.
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
				// log message.
				try
				{
					Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}
		
		/// <summary>
		/// Call this method to PartyLocation open search form 
		/// </summary>
		/// <param name="pstrMethodName"></param>
		/// Method that called this method
		/// <param name="pblnAlwaysShowDialog"></param>
		/// 1: always show open search form
		/// 0: other else
		private bool SelectLocationCode(string pstrMethodName, bool pblnAlwaysShowDialog)
		{
			try				
			{	
				Hashtable htCondition = new Hashtable();
				htCondition.Add(MST_PartyLocationTable.PARTYID_FLD, voParty.PartyID);				
				
				//Call OpenSearchForm for selecting	
				DataRowView drwResult = FormControlComponents.OpenSearchForm(MST_PartyLocationTable.TABLE_NAME, MST_PartyLocationTable.CODE_FLD , txtLocationCode.Text, htCondition, pblnAlwaysShowDialog);

				// If matched searching condition, fill values to form's controls
				if(drwResult != null)
				{
					FillData(drwResult.Row);
					//Reset modify status
					txtLocationCode.Modified = false;
				}
				else if(!pblnAlwaysShowDialog)
				{
					txtLocationCode.Focus();
					return false;
				}			
	
				return true;
			}
			catch (PCSException ex)
			{
				throw new PCSException(ex.mCode, pstrMethodName, ex);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message, ex);
			}
		}
        private void FillData(DataRow pdrowData)
		{
			try
			{
				// get MST_PartyVO object
				voPartyLocation = (MST_PartyLocationVO)boLocation.GetObjectVO(int.Parse(pdrowData[MST_PartyLocationTable.PARTYLOCATIONID_FLD].ToString()), string.Empty);				
				// load data to form
				LoadDataToControl(voPartyLocation);
				// switching form mode
				SwitchFormMode();
			}
			catch (PCSException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}		

		private void txtLocationCode_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtLocationCode_KeyDown()";
			try
			{
				if (e.KeyCode == Keys.F4 && btnSearchCode.Enabled)
				{
					SelectLocationCode(METHOD_NAME, true);
				}
			}
			catch (Exception ex)
			{
				// displays the error message.
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
				// log message.
				try
				{
					Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}
	
		private void txtLocationCode_Validating(object sender, CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".txtLocationCode_Validating()";
			
			try
			{
				//Exit immediately if BIN is empty
				if(mFormMode != EnumAction.Default) return;
				if(txtLocationCode.Text.Length == 0)
				{
					voPartyLocation = new MST_PartyLocationVO();
					voPartyLocation.PartyLocationID = 0;
					LoadDataToControl(voPartyLocation);
					SwitchFormMode();
					return;
				}
				else if(!txtLocationCode.Modified)
				{
					return;
				}

				e.Cancel = !SelectLocationCode(METHOD_NAME, false);
			}
			catch (PCSException ex)
			{
				PCSMessageBox.Show(ex.mCode, MessageBoxIcon.Error);
				try
				{
					Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
				}
			}
			catch (Exception ex)
			{
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxIcon.Error);
				try
				{
					Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxIcon.Error);
				}
			}
		}		

		#endregion

		#region Events Processing

		private void Location_Load(object sender, EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".Location_Load()";
			try
			{
				this.Name = THIS;
				//Set form security
				Security objSecurity = new Security();
				if (objSecurity.SetRightForUserOnForm(this, SystemProperty.UserName) == 0)
				{
					// You don't have the right to view this item
					PCSMessageBox.Show(ErrorCode.MESSAGE_YOU_HAVE_NO_RIGHT_TO_VIEW, MessageBoxIcon.Warning);
					this.Close();
					// Code Inserted Automatically
#region Code Inserted Automatically
this.Cursor = Cursors.Default;
#endregion Code Inserted Automatically

					return;
				}

				// initial combo box
				InitComboBoxs();
				// switch form mode based on FormAction
				SwitchFormMode();
				if (mFormMode == EnumAction.Edit)
				{
					LoadDataToControl(voPartyLocation);
				}
				else
				{
					LoadDataToControl(null);
				}				
				txtLocationCode.Enabled = true;
			}
			catch (PCSException ex)
			{
				// displays the error message.
				PCSMessageBox.Show(ex.mCode, MessageBoxButtons.OK, MessageBoxIcon.Error);
				// log message.
				try
				{
					Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			catch (Exception ex)
			{
				// displays the error message.
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
				// log message.
				try
				{
					Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}

			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.Default;

			#endregion Code Inserted Automatically

			
		}

		private void btnSearchCode_Click(object sender, EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".btnSearchCode_Click()";
			try
			{
				SelectLocationCode(METHOD_NAME, true);
			}
			catch (PCSException ex)
			{
				// displays the error message.
				PCSMessageBox.Show(ex.mCode, MessageBoxButtons.OK, MessageBoxIcon.Error);
				// log message.
				try
				{
					Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			catch (Exception ex)
			{
				// displays the error message.
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
				// log message.
				try
				{
					Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}

			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.Default;

			#endregion Code Inserted Automatically

			
		}

		private void btnContact_Click(object sender, EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".btnContact_Click()";
			try
			{
				if(voPartyLocation.PartyLocationID <= 0) 
{
// Code Inserted Automatically
#region Code Inserted Automatically
this.Cursor = Cursors.Default;
#endregion Code Inserted Automatically

																																													return;
}

				SystemTable.Contacts frmContact = new SystemTable.Contacts(voParty,  voPartyLocation);
				frmContact.FormMode = EnumAction.Default;
				frmContact.ShowDialog();
			}
			catch (PCSException ex)
			{
				// displays the error message.
				PCSMessageBox.Show(ex.mCode, MessageBoxButtons.OK, MessageBoxIcon.Error);
				// log message.
				try
				{
					Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			catch (Exception ex)
			{
				// displays the error message.
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
				// log message.
				try
				{
					Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}

			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.Default;

			#endregion Code Inserted Automatically

			
		}

		private void btnAdd_Click(object sender, EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".btnAdd_Click()";
			try
			{
				// turn form to ADD mode
				mFormMode = EnumAction.Add;
				// switching form mode
				SwitchFormMode();
				// reset form
				LoadDataToControl(null);
				txtLocationCode.Text = boUtils.GetNoByMask(MST_PartyLocationTable.TABLE_NAME, MST_PartyLocationTable.CODE_FLD, boUtils.GetDBDate(), string.Empty);
				txtLocationCode.Focus();
			}
			catch (PCSException ex)
			{
				// displays the error message.
				PCSMessageBox.Show(ex.mCode, MessageBoxButtons.OK, MessageBoxIcon.Error);
				// log message.
				try
				{
					Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			catch (Exception ex)
			{
				// displays the error message.
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
				// log message.
				try
				{
					Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}

			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.Default;

			#endregion Code Inserted Automatically

			
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".btnSave_Click()";
			try
			{
				// validate data
				if (FormControlComponents.CheckMandatory(txtLocationCode))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxButtons.OK, MessageBoxIcon.Error);
					txtLocationCode.Focus();
					txtLocationCode.SelectAll();
					blnDataIsValid = false;
					return;
				}

				if (FormControlComponents.CheckMandatory(txtAddress))
				{
					PCSMessageBox.Show(ErrorCode.MANDATORY_INVALID, MessageBoxButtons.OK, MessageBoxIcon.Error);
					txtAddress.Focus();
					txtAddress.SelectAll();
					blnDataIsValid = false;
					return;
				}

				if (SaveData())
				{
					// display successful message
					PCSMessageBox.Show(ErrorCode.MESSAGE_AFTER_SAVE_DATA, MessageBoxButtons.OK, MessageBoxIcon.Information);
					// turn to DEFAULT mode
					mFormMode = EnumAction.Default;
					// switching form mode
					SwitchFormMode();
					blnDataIsValid = true;
				}
				else
				{
					// display error message
					PCSMessageBox.Show(ErrorCode.MESSAGE_CANNOT_SAVE_TO_DB, MessageBoxButtons.OK, MessageBoxIcon.Error);
					txtLocationCode.Focus();
					txtLocationCode.SelectAll();
					blnDataIsValid = false;
				}
			}
			catch (PCSException ex)
			{
				// displays the error message.
				PCSMessageBox.Show(ex.mCode, MessageBoxButtons.OK, MessageBoxIcon.Error);
				// log message.
				try
				{
					Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				txtLocationCode.Focus();
				txtLocationCode.SelectAll();
				blnDataIsValid = false;
			}
			catch (Exception ex)
			{
				// displays the error message.
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
				// log message.
				try
				{
					Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				txtLocationCode.Focus();
				txtLocationCode.SelectAll();
				blnDataIsValid = false;
			}
		}
        private void btnEdit_Click(object sender, EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".btnEdit_Click()";
			try
			{
				if(voPartyLocation.PartyLocationID <= 0) 
{
// Code Inserted Automatically
#region Code Inserted Automatically
this.Cursor = Cursors.Default;
#endregion Code Inserted Automatically

																																													return;
}
				// turn form to EDIT mode
				mFormMode = EnumAction.Edit;
				// switching form mode
				SwitchFormMode();
				txtLocationCode.Focus();
			}
			catch (PCSException ex)
			{
				// displays the error message.
				PCSMessageBox.Show(ex.mCode, MessageBoxButtons.OK, MessageBoxIcon.Error);
				// log message.
				try
				{
					Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			catch (Exception ex)
			{
				// displays the error message.
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
				// log message.
				try
				{
					Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}

			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.Default;

			#endregion Code Inserted Automatically

			
		}

		private void btnDelete_Click(object sender, EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			const string METHOD_NAME = THIS + ".btnDelete_Click()";
			try
			{
				if(voPartyLocation.PartyLocationID <= 0) 
{
// Code Inserted Automatically
#region Code Inserted Automatically
this.Cursor = Cursors.Default;
#endregion Code Inserted Automatically

																																													return;
}
				// display confirm message
				DialogResult dlgResult = PCSMessageBox.Show(ErrorCode.MESSAGE_DELETE_RECORD, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
				if (dlgResult == DialogResult.Yes)
				{
					// do delete action
					boLocation.Delete(voPartyLocation);
					// clear data of vo object
					voPartyLocation = new MST_PartyLocationVO();
					LoadDataToControl(voPartyLocation);
					// set form mode to DEFAULT
					mFormMode = EnumAction.Default;
					// switching form mode
					SwitchFormMode();					
				}
				txtLocationCode.Focus();
			}
			catch (PCSException ex)
			{
				// displays the error message.
				PCSMessageBox.Show(ex.mCode, MessageBoxButtons.OK, MessageBoxIcon.Error);
				// log message.
				try
				{
					Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			catch (Exception ex)
			{
				// displays the error message.
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
				// log message.
				try
				{
					Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}

			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.Default;

			#endregion Code Inserted Automatically

			
		}

		private void btnHelp_Click(object sender, EventArgs e)
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

		private void btnClose_Click(object sender, EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			Close();

			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.Default;

			#endregion Code Inserted Automatically

			
		}

		private void Location_Closing(object sender, CancelEventArgs e)
		{
			const string METHOD_NAME = THIS + ".Party_Closing()";
			try
			{
				// if form mode is not DEFAULT then display confirm message
				if (mFormMode != EnumAction.Default)
				{
					DialogResult dlgResult = PCSMessageBox.Show(ErrorCode.MESSAGE_QUESTION_STORE_INTO_DATABASE, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
					switch (dlgResult)
					{
						case DialogResult.Yes:
							try
							{
								btnSave_Click(sender, e);
								e.Cancel = !blnDataIsValid;
							}
							catch
							{
								e.Cancel = true;
							}
							break;
						case DialogResult.No:
							e.Cancel = false;
							break;
						case DialogResult.Cancel:
							e.Cancel = true;
							break;
					}
				}
			}
			catch (PCSException ex)
			{
				// displays the error message.
				PCSMessageBox.Show(ex.mCode, MessageBoxButtons.OK, MessageBoxIcon.Error);
				// log message.
				try
				{
					Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			catch (Exception ex)
			{
				// displays the error message.
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
				// log message.
				try
				{
					Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		private void cboCountry_SelectedIndexChanged(object sender, EventArgs e)
		{
			const string METHOD_NAME = THIS + ".cboCountry_SelectedValueChanged()";
			try
			{
				if(cboCountry.SelectedIndex <=0 ) return;

				int intCountryID = int.Parse(cboCountry.SelectedValue.ToString());
				if (tblCityData.Rows.Count > 0)
				{ 
					// select data for city combo
					DataRow[] drowData = tblCityData.Select(MST_CityTable.COUNTRYID_FLD + Constants.EQUAL + intCountryID.ToString());
					// put data into City combo
					PutDataToCityCombo(drowData);
				}
			}
			catch (PCSException ex)
			{
				// displays the error message.
				PCSMessageBox.Show(ex.mCode, MessageBoxButtons.OK, MessageBoxIcon.Error);
				// log message.
				try
				{
					Logger.LogMessage(ex.CauseException, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			catch (Exception ex)
			{
				// displays the error message.
				PCSMessageBox.Show(ErrorCode.OTHER_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
				// log message.
				try
				{
					Logger.LogMessage(ex, METHOD_NAME, Level.ERROR);
				}
				catch
				{
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}
		
		private void Location_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			
		}
		#endregion
		
	}
}

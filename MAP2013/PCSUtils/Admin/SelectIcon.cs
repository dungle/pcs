using System;
using System.Windows.Forms;
using PCSComUtils.Common;
using PCSComUtils.PCSExc;
using PCSUtils.Log;
using PCSUtils.Utils;
using PCSComUtils.DataContext;

namespace PCSUtils.Admin
{
	/// <summary>
	/// This form will display list of icons for selecting
	/// Designed by: Hung LA.
	/// Implemented by: Duong NA.	
	/// </summary>	
	public class SelectIcon : System.Windows.Forms.Form
	{
		#region Declaration
		
		#region System Generated

        private System.Windows.Forms.ListView lvwIcons;
		private System.Windows.Forms.RadioButton radCollapsed;
		private System.Windows.Forms.RadioButton radExpanded;
		private System.Windows.Forms.PictureBox picCollapsed;
		private System.Windows.Forms.PictureBox picExpanded;
		private System.Windows.Forms.Label lblLabelMenuEntry;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.TextBox txtMenuName;
		
		#endregion System Generated

		private const string THIS = "PCSUtils.Admin.SelectIcon";		
		private const int IDX_IMAGE_EXPANDED_FOLDER = 0;
		private const int IDX_IMAGE_FOLDER = 1;
		private const int IDX_IMAGE_FORM = 2;
        private ImageList imglTreeNode;
		
		private bool mIsGroup;

		#endregion Declaration

		#region Constructor, Destructor
		
		public SelectIcon()
		{		
			InitializeComponent();
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
		
		
		#endregion Constructor, Destructor

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectIcon));
            this.lvwIcons = new System.Windows.Forms.ListView();
            this.lblLabelMenuEntry = new System.Windows.Forms.Label();
            this.radCollapsed = new System.Windows.Forms.RadioButton();
            this.radExpanded = new System.Windows.Forms.RadioButton();
            this.picCollapsed = new System.Windows.Forms.PictureBox();
            this.picExpanded = new System.Windows.Forms.PictureBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.txtMenuName = new System.Windows.Forms.TextBox();
            this.imglTreeNode = new System.Windows.Forms.ImageList(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.picCollapsed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picExpanded)).BeginInit();
            this.SuspendLayout();
            // 
            // lvwIcons
            // 
            this.lvwIcons.FullRowSelect = true;
            this.lvwIcons.HideSelection = false;
            this.lvwIcons.LargeImageList = this.imglTreeNode;
            resources.ApplyResources(this.lvwIcons, "lvwIcons");
            this.lvwIcons.MultiSelect = false;
            this.lvwIcons.Name = "lvwIcons";
            this.lvwIcons.SmallImageList = this.imglTreeNode;
            this.lvwIcons.UseCompatibleStateImageBehavior = false;
            this.lvwIcons.SelectedIndexChanged += new System.EventHandler(this.lvwIcons_SelectedIndexChanged);
            this.lvwIcons.DoubleClick += new System.EventHandler(this.lvwIcons_DoubleClick);
            // 
            // lblLabelMenuEntry
            // 
            resources.ApplyResources(this.lblLabelMenuEntry, "lblLabelMenuEntry");
            this.lblLabelMenuEntry.Name = "lblLabelMenuEntry";
            // 
            // radCollapsed
            // 
            this.radCollapsed.ForeColor = System.Drawing.Color.Maroon;
            resources.ApplyResources(this.radCollapsed, "radCollapsed");
            this.radCollapsed.Name = "radCollapsed";
            // 
            // radExpanded
            // 
            this.radExpanded.Checked = true;
            this.radExpanded.ForeColor = System.Drawing.Color.Maroon;
            resources.ApplyResources(this.radExpanded, "radExpanded");
            this.radExpanded.Name = "radExpanded";
            this.radExpanded.TabStop = true;
            // 
            // picCollapsed
            // 
            resources.ApplyResources(this.picCollapsed, "picCollapsed");
            this.picCollapsed.Name = "picCollapsed";
            this.picCollapsed.TabStop = false;
            // 
            // picExpanded
            // 
            resources.ApplyResources(this.picExpanded, "picExpanded");
            this.picExpanded.Name = "picExpanded";
            this.picExpanded.TabStop = false;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.SystemColors.Control;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.btnOk, "btnOk");
            this.btnOk.Name = "btnOk";
            this.btnOk.UseVisualStyleBackColor = false;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // txtMenuName
            // 
            resources.ApplyResources(this.txtMenuName, "txtMenuName");
            this.txtMenuName.Name = "txtMenuName";
            this.txtMenuName.ReadOnly = true;
            this.txtMenuName.TabStop = false;
            // 
            // imglTreeNode
            // 
            this.imglTreeNode.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imglTreeNode.ImageStream")));
            this.imglTreeNode.TransparentColor = System.Drawing.Color.Transparent;
            this.imglTreeNode.Images.SetKeyName(0, "");
            this.imglTreeNode.Images.SetKeyName(1, "");
            this.imglTreeNode.Images.SetKeyName(2, "");
            this.imglTreeNode.Images.SetKeyName(3, "");
            this.imglTreeNode.Images.SetKeyName(4, "");
            this.imglTreeNode.Images.SetKeyName(5, "");
            this.imglTreeNode.Images.SetKeyName(6, "");
            this.imglTreeNode.Images.SetKeyName(7, "");
            this.imglTreeNode.Images.SetKeyName(8, "");
            this.imglTreeNode.Images.SetKeyName(9, "");
            this.imglTreeNode.Images.SetKeyName(10, "");
            this.imglTreeNode.Images.SetKeyName(11, "");
            this.imglTreeNode.Images.SetKeyName(12, "");
            this.imglTreeNode.Images.SetKeyName(13, "");
            this.imglTreeNode.Images.SetKeyName(14, "");
            this.imglTreeNode.Images.SetKeyName(15, "");
            this.imglTreeNode.Images.SetKeyName(16, "");
            this.imglTreeNode.Images.SetKeyName(17, "");
            this.imglTreeNode.Images.SetKeyName(18, "");
            this.imglTreeNode.Images.SetKeyName(19, "");
            this.imglTreeNode.Images.SetKeyName(20, "");
            this.imglTreeNode.Images.SetKeyName(21, "");
            this.imglTreeNode.Images.SetKeyName(22, "");
            this.imglTreeNode.Images.SetKeyName(23, "");
            this.imglTreeNode.Images.SetKeyName(24, "");
            this.imglTreeNode.Images.SetKeyName(25, "");
            this.imglTreeNode.Images.SetKeyName(26, "");
            this.imglTreeNode.Images.SetKeyName(27, "");
            this.imglTreeNode.Images.SetKeyName(28, "");
            this.imglTreeNode.Images.SetKeyName(29, "");
            this.imglTreeNode.Images.SetKeyName(30, "");
            this.imglTreeNode.Images.SetKeyName(31, "");
            this.imglTreeNode.Images.SetKeyName(32, "");
            this.imglTreeNode.Images.SetKeyName(33, "");
            this.imglTreeNode.Images.SetKeyName(34, "");
            this.imglTreeNode.Images.SetKeyName(35, "");
            this.imglTreeNode.Images.SetKeyName(36, "");
            this.imglTreeNode.Images.SetKeyName(37, "");
            this.imglTreeNode.Images.SetKeyName(38, "");
            this.imglTreeNode.Images.SetKeyName(39, "");
            this.imglTreeNode.Images.SetKeyName(40, "");
            this.imglTreeNode.Images.SetKeyName(41, "");
            this.imglTreeNode.Images.SetKeyName(42, "");
            this.imglTreeNode.Images.SetKeyName(43, "");
            this.imglTreeNode.Images.SetKeyName(44, "");
            this.imglTreeNode.Images.SetKeyName(45, "");
            this.imglTreeNode.Images.SetKeyName(46, "");
            this.imglTreeNode.Images.SetKeyName(47, "");
            this.imglTreeNode.Images.SetKeyName(48, "");
            this.imglTreeNode.Images.SetKeyName(49, "");
            this.imglTreeNode.Images.SetKeyName(50, "");
            this.imglTreeNode.Images.SetKeyName(51, "");
            this.imglTreeNode.Images.SetKeyName(52, "");
            this.imglTreeNode.Images.SetKeyName(53, "");
            this.imglTreeNode.Images.SetKeyName(54, "");
            this.imglTreeNode.Images.SetKeyName(55, "");
            this.imglTreeNode.Images.SetKeyName(56, "");
            this.imglTreeNode.Images.SetKeyName(57, "");
            this.imglTreeNode.Images.SetKeyName(58, "");
            this.imglTreeNode.Images.SetKeyName(59, "");
            this.imglTreeNode.Images.SetKeyName(60, "");
            this.imglTreeNode.Images.SetKeyName(61, "");
            this.imglTreeNode.Images.SetKeyName(62, "");
            this.imglTreeNode.Images.SetKeyName(63, "");
            this.imglTreeNode.Images.SetKeyName(64, "");
            this.imglTreeNode.Images.SetKeyName(65, "");
            this.imglTreeNode.Images.SetKeyName(66, "");
            this.imglTreeNode.Images.SetKeyName(67, "");
            this.imglTreeNode.Images.SetKeyName(68, "");
            this.imglTreeNode.Images.SetKeyName(69, "");
            this.imglTreeNode.Images.SetKeyName(70, "");
            this.imglTreeNode.Images.SetKeyName(71, "");
            this.imglTreeNode.Images.SetKeyName(72, "");
            this.imglTreeNode.Images.SetKeyName(73, "");
            this.imglTreeNode.Images.SetKeyName(74, "");
            this.imglTreeNode.Images.SetKeyName(75, "");
            this.imglTreeNode.Images.SetKeyName(76, "");
            this.imglTreeNode.Images.SetKeyName(77, "");
            this.imglTreeNode.Images.SetKeyName(78, "");
            this.imglTreeNode.Images.SetKeyName(79, "");
            this.imglTreeNode.Images.SetKeyName(80, "");
            this.imglTreeNode.Images.SetKeyName(81, "");
            this.imglTreeNode.Images.SetKeyName(82, "");
            this.imglTreeNode.Images.SetKeyName(83, "");
            this.imglTreeNode.Images.SetKeyName(84, "");
            this.imglTreeNode.Images.SetKeyName(85, "");
            this.imglTreeNode.Images.SetKeyName(86, "");
            this.imglTreeNode.Images.SetKeyName(87, "");
            this.imglTreeNode.Images.SetKeyName(88, "");
            this.imglTreeNode.Images.SetKeyName(89, "");
            this.imglTreeNode.Images.SetKeyName(90, "");
            this.imglTreeNode.Images.SetKeyName(91, "");
            this.imglTreeNode.Images.SetKeyName(92, "");
            this.imglTreeNode.Images.SetKeyName(93, "");
            this.imglTreeNode.Images.SetKeyName(94, "");
            this.imglTreeNode.Images.SetKeyName(95, "");
            this.imglTreeNode.Images.SetKeyName(96, "");
            this.imglTreeNode.Images.SetKeyName(97, "");
            this.imglTreeNode.Images.SetKeyName(98, "");
            this.imglTreeNode.Images.SetKeyName(99, "");
            this.imglTreeNode.Images.SetKeyName(100, "");
            this.imglTreeNode.Images.SetKeyName(101, "");
            this.imglTreeNode.Images.SetKeyName(102, "");
            this.imglTreeNode.Images.SetKeyName(103, "");
            this.imglTreeNode.Images.SetKeyName(104, "");
            this.imglTreeNode.Images.SetKeyName(105, "");
            this.imglTreeNode.Images.SetKeyName(106, "");
            this.imglTreeNode.Images.SetKeyName(107, "");
            this.imglTreeNode.Images.SetKeyName(108, "");
            this.imglTreeNode.Images.SetKeyName(109, "");
            this.imglTreeNode.Images.SetKeyName(110, "");
            this.imglTreeNode.Images.SetKeyName(111, "");
            this.imglTreeNode.Images.SetKeyName(112, "");
            this.imglTreeNode.Images.SetKeyName(113, "");
            this.imglTreeNode.Images.SetKeyName(114, "");
            this.imglTreeNode.Images.SetKeyName(115, "");
            this.imglTreeNode.Images.SetKeyName(116, "");
            this.imglTreeNode.Images.SetKeyName(117, "");
            this.imglTreeNode.Images.SetKeyName(118, "");
            this.imglTreeNode.Images.SetKeyName(119, "");
            this.imglTreeNode.Images.SetKeyName(120, "");
            this.imglTreeNode.Images.SetKeyName(121, "");
            this.imglTreeNode.Images.SetKeyName(122, "");
            this.imglTreeNode.Images.SetKeyName(123, "");
            this.imglTreeNode.Images.SetKeyName(124, "");
            this.imglTreeNode.Images.SetKeyName(125, "");
            this.imglTreeNode.Images.SetKeyName(126, "");
            this.imglTreeNode.Images.SetKeyName(127, "");
            this.imglTreeNode.Images.SetKeyName(128, "");
            this.imglTreeNode.Images.SetKeyName(129, "");
            this.imglTreeNode.Images.SetKeyName(130, "");
            this.imglTreeNode.Images.SetKeyName(131, "");
            this.imglTreeNode.Images.SetKeyName(132, "");
            this.imglTreeNode.Images.SetKeyName(133, "");
            this.imglTreeNode.Images.SetKeyName(134, "");
            this.imglTreeNode.Images.SetKeyName(135, "");
            this.imglTreeNode.Images.SetKeyName(136, "");
            this.imglTreeNode.Images.SetKeyName(137, "");
            this.imglTreeNode.Images.SetKeyName(138, "");
            this.imglTreeNode.Images.SetKeyName(139, "");
            this.imglTreeNode.Images.SetKeyName(140, "");
            this.imglTreeNode.Images.SetKeyName(141, "");
            this.imglTreeNode.Images.SetKeyName(142, "");
            this.imglTreeNode.Images.SetKeyName(143, "");
            this.imglTreeNode.Images.SetKeyName(144, "");
            this.imglTreeNode.Images.SetKeyName(145, "");
            this.imglTreeNode.Images.SetKeyName(146, "");
            this.imglTreeNode.Images.SetKeyName(147, "");
            this.imglTreeNode.Images.SetKeyName(148, "");
            this.imglTreeNode.Images.SetKeyName(149, "");
            this.imglTreeNode.Images.SetKeyName(150, "");
            this.imglTreeNode.Images.SetKeyName(151, "");
            this.imglTreeNode.Images.SetKeyName(152, "");
            this.imglTreeNode.Images.SetKeyName(153, "");
            this.imglTreeNode.Images.SetKeyName(154, "");
            this.imglTreeNode.Images.SetKeyName(155, "");
            this.imglTreeNode.Images.SetKeyName(156, "");
            this.imglTreeNode.Images.SetKeyName(157, "");
            this.imglTreeNode.Images.SetKeyName(158, "");
            this.imglTreeNode.Images.SetKeyName(159, "");
            this.imglTreeNode.Images.SetKeyName(160, "");
            this.imglTreeNode.Images.SetKeyName(161, "");
            this.imglTreeNode.Images.SetKeyName(162, "");
            this.imglTreeNode.Images.SetKeyName(163, "1284266860_industry.ico");
            this.imglTreeNode.Images.SetKeyName(164, "1284266908_deliverables.ico");
            this.imglTreeNode.Images.SetKeyName(165, "1284266860_industry.ico");
            this.imglTreeNode.Images.SetKeyName(166, "1284266882_industry.ico");
            this.imglTreeNode.Images.SetKeyName(167, "1284266899_desktop_computer.ico");
            this.imglTreeNode.Images.SetKeyName(168, "1284266908_deliverables.ico");
            this.imglTreeNode.Images.SetKeyName(169, "1284266917_shopping_cart.ico");
            this.imglTreeNode.Images.SetKeyName(170, "1284267060_dedicated_server.ico");
            this.imglTreeNode.Images.SetKeyName(171, "1284267069_receptionist.ico");
            this.imglTreeNode.Images.SetKeyName(172, "1284267094_rigid_dump_truck.ico");
            this.imglTreeNode.Images.SetKeyName(173, "1284267104_software_development.ico");
            this.imglTreeNode.Images.SetKeyName(174, "1284267112_local_network.ico");
            this.imglTreeNode.Images.SetKeyName(175, "1284267119_electricity.ico");
            this.imglTreeNode.Images.SetKeyName(176, "1284267134_industry.ico");
            this.imglTreeNode.Images.SetKeyName(177, "1284267179_schedule_scan.ico");
            this.imglTreeNode.Images.SetKeyName(178, "1284267202_calendar_year.ico");
            this.imglTreeNode.Images.SetKeyName(179, "1284267209_briefcase.ico");
            this.imglTreeNode.Images.SetKeyName(180, "1284267224_data_transport.ico");
            this.imglTreeNode.Images.SetKeyName(181, "1284267251_air_compressor.ico");
            this.imglTreeNode.Images.SetKeyName(182, "1284267258_aerial_platform.ico");
            this.imglTreeNode.Images.SetKeyName(183, "1284267299_calendar.ico");
            this.imglTreeNode.Images.SetKeyName(184, "1284267306_truck.ico");
            this.imglTreeNode.Images.SetKeyName(185, "1284267313_hijack.ico");
            this.imglTreeNode.Images.SetKeyName(186, "1284267319_demographic.ico");
            this.imglTreeNode.Images.SetKeyName(187, "1284267328_objects.ico");
            this.imglTreeNode.Images.SetKeyName(188, "1284267359_insource.ico");
            this.imglTreeNode.Images.SetKeyName(189, "1284267369_primitives.ico");
            this.imglTreeNode.Images.SetKeyName(190, "1284267377_autoresponse.ico");
            this.imglTreeNode.Images.SetKeyName(191, "1284267505_truck.ico");
            this.imglTreeNode.Images.SetKeyName(192, "1284267614_money_bag.ico");
            this.imglTreeNode.Images.SetKeyName(193, "1284267621_money.ico");
            this.imglTreeNode.Images.SetKeyName(194, "1284267635_company.ico");
            this.imglTreeNode.Images.SetKeyName(195, "1284267801_vault.ico");
            this.imglTreeNode.Images.SetKeyName(196, "1284267809_interact.ico");
            this.imglTreeNode.Images.SetKeyName(197, "1284267817_people.ico");
            this.imglTreeNode.Images.SetKeyName(198, "1284267829_school.ico");
            this.imglTreeNode.Images.SetKeyName(199, "1284267850_05_phonebook.ico");
            this.imglTreeNode.Images.SetKeyName(200, "1284267858_02_calendar.ico");
            this.imglTreeNode.Images.SetKeyName(201, "1284267864_06_calculator.ico");
            this.imglTreeNode.Images.SetKeyName(202, "1284267873_Gear.ico");
            this.imglTreeNode.Images.SetKeyName(203, "1284267987_Delivery.ico");
            this.imglTreeNode.Images.SetKeyName(204, "1284267994_Home.ico");
            this.imglTreeNode.Images.SetKeyName(205, "1284268028_Money_Bag.ico");
            this.imglTreeNode.Images.SetKeyName(206, "1284268040_Calendar.ico");
            this.imglTreeNode.Images.SetKeyName(207, "1284268257_destroyer.ico");
            this.imglTreeNode.Images.SetKeyName(208, "1284268327_calendar.ico");
            this.imglTreeNode.Images.SetKeyName(209, "1284268347_Run.ico");
            this.imglTreeNode.Images.SetKeyName(210, "1284268356_Search.ico");
            this.imglTreeNode.Images.SetKeyName(211, "1284268398_Settings.ico");
            this.imglTreeNode.Images.SetKeyName(212, "1284268429_packing.ico");
            this.imglTreeNode.Images.SetKeyName(213, "1284268436_Add-Male-User.ico");
            this.imglTreeNode.Images.SetKeyName(214, "1284268446_Remove-Male-User.ico");
            this.imglTreeNode.Images.SetKeyName(215, "1284268452_Accept-Male-User.ico");
            this.imglTreeNode.Images.SetKeyName(216, "1284268498_Process-Accept.ico");
            this.imglTreeNode.Images.SetKeyName(217, "1284268527_binary-tree.ico");
            this.imglTreeNode.Images.SetKeyName(218, "1284268630_earning-statements.ico");
            this.imglTreeNode.Images.SetKeyName(219, "1284268638_event.ico");
            this.imglTreeNode.Images.SetKeyName(220, "1284268645_autoship.ico");
            this.imglTreeNode.Images.SetKeyName(221, "1284268664_contact.ico");
            this.imglTreeNode.Images.SetKeyName(222, "1284268743_pie_chart.ico");
            this.imglTreeNode.Images.SetKeyName(223, "1284268754_data_transport.ico");
            this.imglTreeNode.Images.SetKeyName(224, "1284268833_taxes.ico");
            this.imglTreeNode.Images.SetKeyName(225, "1284268842_our_process_2.ico");
            this.imglTreeNode.Images.SetKeyName(226, "1284268855_pie_chart.ico");
            this.imglTreeNode.Images.SetKeyName(227, "1284268886_support_belt.ico");
            this.imglTreeNode.Images.SetKeyName(228, "1284269035_button_blue_repeat.ico");
            this.imglTreeNode.Images.SetKeyName(229, "1284269234_gnome-mime-application-vnd.ms-powerpoint.ico");
            this.imglTreeNode.Images.SetKeyName(230, "1284269274_emblem-system.ico");
            this.imglTreeNode.Images.SetKeyName(231, "1284269281_ark.ico");
            this.imglTreeNode.Images.SetKeyName(232, "1284269471_United-States-Flag.ico");
            this.imglTreeNode.Images.SetKeyName(233, "1284269693_BuildingManagement.ico");
            this.imglTreeNode.Images.SetKeyName(234, "1284269755_exit.ico");
            this.imglTreeNode.Images.SetKeyName(235, "1284269789_Japan.ico");
            this.imglTreeNode.Images.SetKeyName(236, "1284269810_klipper.ico");
            this.imglTreeNode.Images.SetKeyName(237, "1284269910_cabinet.ico");
            this.imglTreeNode.Images.SetKeyName(238, "1284269919_cashbox.ico");
            this.imglTreeNode.Images.SetKeyName(239, "1284269982_gear.ico");
            this.imglTreeNode.Images.SetKeyName(240, "1284270042_content-reorder.ico");
            this.imglTreeNode.Images.SetKeyName(241, "1284270075_paste.ico");
            this.imglTreeNode.Images.SetKeyName(242, "1284270081_catalog.ico");
            this.imglTreeNode.Images.SetKeyName(243, "1284270091_options.ico");
            this.imglTreeNode.Images.SetKeyName(244, "1284270122_organization.ico");
            this.imglTreeNode.Images.SetKeyName(245, "1284270198_Delivery.ico");
            this.imglTreeNode.Images.SetKeyName(246, "1284270208_Business.ico");
            this.imglTreeNode.Images.SetKeyName(247, "1284270216_Time.ico");
            this.imglTreeNode.Images.SetKeyName(248, "1284270325_gear_wheel.ico");
            this.imglTreeNode.Images.SetKeyName(249, "1284270332_magnifying_glass.ico");
            this.imglTreeNode.Images.SetKeyName(250, "1284270338_lock.ico");
            this.imglTreeNode.Images.SetKeyName(251, "1284270361_library.ico");
            this.imglTreeNode.Images.SetKeyName(252, "1284270441_distributor-report.ico");
            this.imglTreeNode.Images.SetKeyName(253, "1284270455_Package-Download.ico");
            this.imglTreeNode.Images.SetKeyName(254, "1284270462_reports.ico");
            this.imglTreeNode.Images.SetKeyName(255, "1284270489_delivery.ico");
            this.imglTreeNode.Images.SetKeyName(256, "1284270589_industry.ico");
            this.imglTreeNode.Images.SetKeyName(257, "1284270598_local_network.ico");
            this.imglTreeNode.Images.SetKeyName(258, "1284270628_cabinet.ico");
            this.imglTreeNode.Images.SetKeyName(259, "1284270652_objects.ico");
            this.imglTreeNode.Images.SetKeyName(260, "1284270666_agency.ico");
            this.imglTreeNode.Images.SetKeyName(261, "1284270673_data_management.ico");
            this.imglTreeNode.Images.SetKeyName(262, "1284270681_open_safety_box.ico");
            this.imglTreeNode.Images.SetKeyName(263, "1284270688_calendar.ico");
            this.imglTreeNode.Images.SetKeyName(264, "1284270759_truck.ico");
            this.imglTreeNode.Images.SetKeyName(265, "1284270778_merge_cells.ico");
            this.imglTreeNode.Images.SetKeyName(266, "1284270831_tablet.ico");
            this.imglTreeNode.Images.SetKeyName(267, "1284271010_line_chart.ico");
            this.imglTreeNode.Images.SetKeyName(268, "1284271020_pie_chart.ico");
            this.imglTreeNode.Images.SetKeyName(269, "1284271035_stock_task.ico");
            this.imglTreeNode.Images.SetKeyName(270, "1284271100_kontact_date.ico");
            this.imglTreeNode.Images.SetKeyName(271, "1284271142_preferences-contact-list.ico");
            this.imglTreeNode.Images.SetKeyName(272, "1284271242_file-roller.ico");
            this.imglTreeNode.Images.SetKeyName(273, "1284271290_stock_task-assigned-to.ico");
            this.imglTreeNode.Images.SetKeyName(274, "1284271368_gnome-settings-default-applications.ico");
            this.imglTreeNode.Images.SetKeyName(275, "1284271546_Todo.ico");
            this.imglTreeNode.Images.SetKeyName(276, "1284272015_config-date.ico");
            this.imglTreeNode.Images.SetKeyName(277, "1284272042_gnome-mime-application-vnd.sun.xml.calc.template.ico");
            this.imglTreeNode.Images.SetKeyName(278, "1284272081_gnome-help.ico");
            this.imglTreeNode.Images.SetKeyName(279, "1284272158_system-switch-user.ico");
            this.imglTreeNode.Images.SetKeyName(280, "1284272175_system-config-boot.ico");
            this.imglTreeNode.Images.SetKeyName(281, "1284272209_start-here-kubuntu.ico");
            this.imglTreeNode.Images.SetKeyName(282, "1284272217_gtk-dialog-warning.ico");
            this.imglTreeNode.Images.SetKeyName(283, "1284272359_gtk-save-as.ico");
            this.imglTreeNode.Images.SetKeyName(284, "1284272400_stock_task-recurring.ico");
            this.imglTreeNode.Images.SetKeyName(285, "1284272455_evolution-tasks.ico");
            this.imglTreeNode.Images.SetKeyName(286, "1284272485_preferences-certificates.ico");
            this.imglTreeNode.Images.SetKeyName(287, "1284272568_gtk-edit.ico");
            this.imglTreeNode.Images.SetKeyName(288, "1284272607_crack-attack.ico");
            this.imglTreeNode.Images.SetKeyName(289, "1284272624_gtk-execute.ico");
            this.imglTreeNode.Images.SetKeyName(290, "1284272845_kde-folder.ico");
            this.imglTreeNode.Images.SetKeyName(291, "1284272875_system-shutdown.ico");
            this.imglTreeNode.Images.SetKeyName(292, "1284272976_emblem-system.ico");
            this.imglTreeNode.Images.SetKeyName(293, "1284273003_filesave.ico");
            this.imglTreeNode.Images.SetKeyName(294, "1284273052_xfce-edit.ico");
            this.imglTreeNode.Images.SetKeyName(295, "1284273491_error.ico");
            this.imglTreeNode.Images.SetKeyName(296, "Check16.png");
            // 
            // SelectIcon
            // 
            this.AcceptButton = this.btnOk;
            resources.ApplyResources(this, "$this");
            this.CancelButton = this.btnCancel;
            this.Controls.Add(this.txtMenuName);
            this.Controls.Add(this.radExpanded);
            this.Controls.Add(this.radCollapsed);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.picExpanded);
            this.Controls.Add(this.picCollapsed);
            this.Controls.Add(this.lblLabelMenuEntry);
            this.Controls.Add(this.lvwIcons);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SelectIcon";
            this.Load += new System.EventHandler(this.SelectIcon_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picCollapsed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picExpanded)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion
				
		#region Class's Method And Properties

        public Sys_Menu_Entry MenuEntry { get; set; }
		

		public bool IsGroup
		{
			set { mIsGroup = value; }
			get {return mIsGroup; }
		}

		#endregion Class's Method

		#region Event Processing
	
		private void SelectIcon_Load(object sender, System.EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			// Code Inserted Automatically
			const string METHOD_NAME = THIS + ".SelectIcon_Load";
			try
			{
				const string STR_NODE = "";
				
				#region Security
				//Set authorization for user
				Security objSecurity = new Security();
				this.Name = THIS;
				if(objSecurity.SetRightForUserOnForm(this, SystemProperty.UserName) == 0)
				{
					// You don't have the right to view this item
					this.Hide();
					PCSMessageBox.Show(ErrorCode.MESSAGE_YOU_HAVE_NO_RIGHT_TO_VIEW, MessageBoxIcon.Warning);
					this.Close();					
					// Code Inserted Automatically
                    #region Code Inserted Automatically
                    this.Cursor = Cursors.Default;
                    #endregion Code Inserted Automatically

					return;
				}
				#endregion

				for (int i = 0; i < imglTreeNode.Images.Count; i++)
				{
					lvwIcons.Items.Add(STR_NODE,i);
				}
                if (MenuEntry == null)
				{
					this.Close();
					return;
				}
                int nCollapsed = MenuEntry.CollapsedImage.GetValueOrDefault(0) - 1;
                int nExpanded = MenuEntry.ExpandedImage.GetValueOrDefault(0) - 1;

				if (mIsGroup)
				{
					if (nCollapsed < 0)
					{
						nCollapsed = IDX_IMAGE_FOLDER;
					}
					if (nExpanded < 0)
					{
						nExpanded = IDX_IMAGE_EXPANDED_FOLDER;
					}
				}
				else
				{
					if (nCollapsed < 0)
					{
						nCollapsed = IDX_IMAGE_FORM;
					}
					if (nExpanded < 0)
					{
						nExpanded = IDX_IMAGE_FORM;
					}
				}
				picCollapsed.Image = imglTreeNode.Images[nCollapsed];
				picExpanded.Image = imglTreeNode.Images[nExpanded];

                txtMenuName.Text = MenuEntry.Text_CaptionDefault;
				if (!mIsGroup)
				{
					radCollapsed.Enabled = radExpanded.Enabled = false;
				}
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
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
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
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
				}
			}

			// Code Inserted Automatically	

			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.Default;

			#endregion Code Inserted Automatically

			
		}

		
		private void lvwIcons_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			const string METHOD_NAME = THIS + ".lvwIcons_SelectedIndexChanged";
			try
			{
				if (lvwIcons.SelectedIndices.Count == 0)
				{
					return;
				}
				if (mIsGroup)
				{
					if (radCollapsed.Checked)
					{
						picCollapsed.Image = imglTreeNode.Images[lvwIcons.SelectedIndices[0]];
                        MenuEntry.CollapsedImage = lvwIcons.SelectedIndices[0] + 1;
					}
					else
					{
						picExpanded.Image = imglTreeNode.Images[lvwIcons.SelectedIndices[0]];
                        MenuEntry.ExpandedImage = lvwIcons.SelectedIndices[0] + 1;
					}
				}
				else
				{
					picExpanded.Image = imglTreeNode.Images[lvwIcons.SelectedIndices[0]];
					picCollapsed.Image = imglTreeNode.Images[lvwIcons.SelectedIndices[0]];
                    MenuEntry.ExpandedImage = MenuEntry.CollapsedImage = lvwIcons.SelectedIndices[0] + 1;
				}
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
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
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
					PCSMessageBox.Show(ErrorCode.LOG_EXCEPTION, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
				}
			}
		}

		private void btnOk_Click(object sender, System.EventArgs e)
		{
			// Code Inserted Automatically

			#region Code Inserted Automatically

			this.Cursor = Cursors.WaitCursor;

			#endregion Code Inserted Automatically

			
			DialogResult = DialogResult.OK;
			this.Close();

			#region Code Inserted Automatically

			this.Cursor = Cursors.Default;

			#endregion Code Inserted Automatically

			
		}

		private void btnCancel_Click(object sender, System.EventArgs e)
		{		
			// Code Inserted Automatically
		
			#region Code Inserted Automatically
		
			this.Cursor = Cursors.WaitCursor;
		
			#endregion Code Inserted Automatically
		
					
			// Code Inserted Automatically		
			DialogResult = DialogResult.Cancel;
			this.Close();			
		
			// Code Inserted Automatically
		
			#region Code Inserted Automatically
		
			this.Cursor = Cursors.Default;
		
			#endregion Code Inserted Automatically
		
			
		}

		private void lvwIcons_DoubleClick(object sender, System.EventArgs e)
		{
			btnOk_Click(null,null);
		}

		#endregion Event Processing
	}
}
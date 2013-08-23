using System;


namespace PCSComSale.Order.DS
{
	
	[Serializable]
	public class SO_CommitInventoryMasterVO
	{
		private int mCommitInventoryMasterID;
		private string mCommitmentNo;
		private DateTime mCommitDate;
		private int mSaleOrderMasterID;
		private int mEmployeeID;
		private string mUsername;

		public int CommitInventoryMasterID
		{
			set { mCommitInventoryMasterID = value; }
			get { return mCommitInventoryMasterID; }
		}
		public string CommitmentNo
		{
			set { mCommitmentNo = value; }
			get { return mCommitmentNo; }
		}
		public DateTime CommitDate
		{
			set { mCommitDate = value; }
			get { return mCommitDate; }
		}
		public int SaleOrderMasterID
		{
			set { mSaleOrderMasterID = value; }
			get { return mSaleOrderMasterID; }
		}
		public int EmployeeID
		{
			set { mEmployeeID = value; }
			get { return mEmployeeID; }
		}

		public string Username
		{
			get { return mUsername; }
			set { mUsername = value; }
		}
	}
}

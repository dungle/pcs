using System;
using System.Data;
namespace PCSComSale.Order.DS{
	[Serializable]
	public class SO_CustomerItemRefMasterVO
	{
		private int	mCustomerItemRefMasterID;
		private int	mCCNID;
		private int	mPartyID;

		public int	CustomerItemRefMasterID
		{	set { mCustomerItemRefMasterID = value; }
			get { return mCustomerItemRefMasterID; }
		}
		public int	CCNID
		{	set { mCCNID = value; }
			get { return mCCNID; }
		}
		public int	PartyID
		{	set { mPartyID = value; }
			get { return mPartyID; }
		}
	}
}

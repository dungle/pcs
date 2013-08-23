using System;
using System.Data;
namespace PCSComMaterials.Inventory.DS{
	[Serializable]
	public class IV_OnhandPeriodVO
	{
		private int	mOnhandPeriodID;
		private string	mCode;
		private DateTime	mEffectDate;
		private bool	mStatus;

		public int	OnhandPeriodID
		{	set { mOnhandPeriodID = value; }
			get { return mOnhandPeriodID; }
		}
		public string	Code
		{	set { mCode = value; }
			get { return mCode; }
		}
		public DateTime	EffectDate
		{	set { mEffectDate = value; }
			get { return mEffectDate; }
		}
		public bool	Status
		{	set { mStatus = value; }
			get { return mStatus; }
		}
	}
}

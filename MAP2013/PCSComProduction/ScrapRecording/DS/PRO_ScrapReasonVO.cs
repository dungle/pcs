using System;
using System.Data;

namespace PCSComProduction.ScrapRecording.DS
{
	[Serializable]
	public class PRO_ScrapReasonVO
	{
		private int mScrapReasonID;
		private string mScrapReasonDesc;

		public int ScrapReasonID
		{
			set { mScrapReasonID = value; }
			get { return mScrapReasonID; }
		}
		public string ScrapReasonDesc
		{
			set { mScrapReasonDesc = value; }
			get { return mScrapReasonDesc; }
		}
	}
}

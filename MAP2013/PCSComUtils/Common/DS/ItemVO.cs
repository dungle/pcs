using System;


namespace PCSComUtils.Common.DS
{
    public class ItemVO
    {
        private int mValues;
        private string mDescription;

        public ItemVO(int iValues, string Des)
        {
            mValues = iValues;
            mDescription = Des;

        }
        public int Values
        {
            set { mValues = value; }
            get { return mValues; }
        }
        public string Description
        {
            set { mDescription = value; }
            get { return mDescription; }
        }
    }
}

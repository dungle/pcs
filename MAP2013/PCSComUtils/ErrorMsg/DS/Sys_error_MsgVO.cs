namespace PCSComUtils.ErrorMsg.DS
{
    public class Sys_Error_MsgVO
    {
        private int mError_MsgID;
        private int mCode;
        private string mMsgDefault;
        private string mMsgVn;
        private string mMsgEn;
        private string mMsgJp;
        private string mDescription;

        public int Error_MsgID
        {
            set { mError_MsgID = value; }
            get { return mError_MsgID; }
        }
        public int Code
        {
            set { mCode = value; }
            get { return mCode; }
        }
        public string MsgDefault
        {
            set { mMsgDefault = value; }
            get { return mMsgDefault; }
        }
        public string MsgVn
        {
            set { mMsgVn = value; }
            get { return mMsgVn; }
        }
        public string MsgEn
        {
            set { mMsgEn = value; }
            get { return mMsgEn; }
        }
        public string MsgJp
        {
            set { mMsgJp = value; }
            get { return mMsgJp; }
        }
        public string Description
        {
            set { mDescription = value; }
            get { return mDescription; }
        }
    }
}
namespace PCSUtils.Log
{
	//**************************************************************************              
	///    <Description>
	///       Defines the set of levels constants recognised by the system.
	///    </Description>
	///    <Inputs>
	///       
	///    </Inputs>
	///    <Outputs>
	///       
	///    </Outputs>
	///    <Returns>
	///       
	///    </Returns>
	///    <Authors>
	///       DungLA
	///    </Authors>
	///    <History>
	///       31-Dec-2004
	///    </History>
	///    <Notes>
	///    </Notes>
	//**************************************************************************
	public sealed class LevelConstants
	{
		#region Class Constants

		public const string OFF = "OFF";
		public const string ALL = "ALL";
		public const string ERROR = "ERROR";
		public const string INFO = "INFORMATION";
		public const string DEBUG = "DEBUG";

		#endregion
	}
	//**************************************************************************              
	///    <Description>
	///       Defines the set of levels recognised by the system.
	///       The predefined set of levels recognised by the system are
	///       OFF, ERROR, INFO and ALL.
	///       The Level class is sealed. This class cannot inherited.
	///    </Description>
	///    <Inputs>
	///       
	///    </Inputs>
	///    <Outputs>
	///       
	///    </Outputs>
	///    <Returns>
	///       
	///    </Returns>
	///    <Authors>
	///       DungLA
	///    </Authors>
	///    <History>
	///       31-Dec-2004
	///    </History>
	///    <Notes>
	///    </Notes>
	//**************************************************************************
	public sealed class Level
	{
		#region Readonly Static Member Variables

		/// <summary>
		/// The <c>OFF</c> level designates the lowest level possible
		/// </summary>
		public readonly static Level OFF = new Level(int.MinValue, LevelConstants.OFF);

		/// <summary>
		/// The <c>ERROR</c> level designates error events that might still allow the application to continue running.
		/// </summary>
		public readonly static Level ERROR = new Level(1, LevelConstants.ERROR);

		/// <summary>
		/// The <c>INFO</c> level designates informational messages that highlight the progress of the application at coarse-grained level.
		/// </summary>
		public readonly static Level INFO = new Level(2, LevelConstants.INFO);

		/// <summary>
		/// The <c>DEBUG</c> level designates fine-grained informational events that are most useful to debug an application.
		/// </summary>
		public readonly static Level DEBUG = new Level(3, LevelConstants.DEBUG);

		/// <summary>
		/// The <c>ALL</c> level designates the highest level than all the rest
		/// </summary>
		public readonly static Level ALL = new Level(int.MaxValue, LevelConstants.ALL);

		#endregion

		#region Member Variables

		private int mValue;
		private string mLevelName;

		#endregion

		#region Constructors

		//**************************************************************************              
		///    <Description>
		///       Default constructor, initialize level code and level name
		///    </Description>
		///    <Inputs>
		///       int Level code, string Level name
		///    </Inputs>
		///    <Outputs>
		///       
		///    </Outputs>
		///    <Returns>
		///       
		///    </Returns>
		///    <Authors>
		///       DungLA
		///    </Authors>
		///    <History>
		///       31-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public Level(int pnLevel, string pstrLevelName)
		{
			mValue = pnLevel;
			mLevelName = pstrLevelName;
		}

		#endregion

		#region Implement Method from Object

		//**************************************************************************              
		///    <Description>
		///       Return Level name
		///    </Description>
		///    <Inputs>
		///       
		///    </Inputs>
		///    <Outputs>
		///       string
		///    </Outputs>
		///    <Returns>
		///       Level name
		///    </Returns>
		///    <Authors>
		///       DungLA
		///    </Authors>
		///    <History>
		///       31-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public override string ToString()
		{
			return this.mLevelName;
		}

		//**************************************************************************              
		///    <Description>
		///       Return level code
		///    </Description>
		///    <Inputs>
		///       
		///    </Inputs>
		///    <Outputs>
		///       int
		///    </Outputs>
		///    <Returns>
		///       level code
		///    </Returns>
		///    <Authors>
		///       DungLA
		///    </Authors>
		///    <History>
		///       31-Dec-2004
		///    </History>
		///    <Notes>
		///    </Notes>
		//**************************************************************************
		public override int GetHashCode()
		{
			return this.mValue;
		}



		#endregion

		/// <summary>
		/// The name of this level. Read-only property
		/// </summary>
		public string Name
		{
			get { return mLevelName; }
		}

		/// <summary>
		/// The value of this level. Read-only property
		/// </summary>
		public int Value
		{
			get { return mValue; }
		}
	}
}

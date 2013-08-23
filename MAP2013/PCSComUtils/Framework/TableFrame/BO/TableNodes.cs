using System;
using System.Collections;
using PCSComUtils.PCSExc;
using PCSComUtils.Common;
using PCSComUtils.Framework.TableFrame.DS;

namespace PCSComUtils.Framework.TableFrame.BO
{
	[Serializable]
	public class TableNodes
	{
		private int mParent;
		private int mCurrent;
		private object obj;
		public TableNodes(int mParent,int mCurrent,object obj)
		{
			this.mParent = mParent;
			this.mCurrent = mCurrent;
			this.obj = obj;
		}
		public int Parent
		{
			get {return mParent;}
			set {mParent = value;}
		}
		public int Current
		{
			get {return mCurrent;}
			set {mCurrent = value;}
		}
		public object OBJ
		{
			get {return obj;}
			set {obj = value;}
		}
	}

	public interface IuTreeView 
	{
		ArrayList CreateNodes();
	}

	public class uTreeView 
	{
		public uTreeView()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public ArrayList CreateNodes()
		{
			const string METHOD_NAME = "CreatNodes()";
			int intParent = 0;
			int intCurrent = 1;
			int intChild = 1000;
			TableManagementBO boTableManagement = new TableManagementBO();
			ArrayList arr = new ArrayList();

			try
			{	
				ArrayList arrTable = boTableManagement.GetAllTable();
				ArrayList arrGroup = boTableManagement.GetAllGroup();
				ArrayList arrAnd = boTableManagement.GetAllTableAndGroup();

				foreach (sys_TableGroupVO voTableGroup in arrGroup)
				{
					TableNodes node = new TableNodes(intParent,intCurrent,voTableGroup);
					arr.Add(node);
					int index = 0;
					foreach (sys_TableAndGroupVO voTableAndGroup in arrAnd)
					{
						if (voTableGroup.TableGroupID == voTableAndGroup.TableGroupID)
						{
							sys_TableVO voSysTable = (sys_TableVO)arrTable[index];
							node = new TableNodes(intCurrent,intChild,voSysTable);
							arr.Add(node);
						}
						intChild++;
						index++;
					}
					intCurrent++;
				}
				return arr;
			}
			catch (Exception ex)
			{
				throw new PCSDBException(ErrorCode.OTHER_ERROR, METHOD_NAME, ex);
			}
		}
	
	}
}

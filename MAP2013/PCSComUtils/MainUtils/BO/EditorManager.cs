using System.Windows.Forms;

namespace PCSComUtils.MainUtils.BO
{
	/// <summary>for EditorManager.
	/// </summary>
	public class EditorManager
	{
		private Form form;

		public EditorManager(Form form)
		{
			this.form = form;
		}

		/// <summary>
		/// Set focus on next control
		/// </summary>
		/// <param name="sender">current control</param>
		/// <returns>Focused control</returns>
		public Control SetNextFocus(object sender)
		{
			System.Collections.IEnumerator enumerator = form.Controls.GetEnumerator();
			int nextTabIndex = -1;
			Control nextControl = (Control)sender;
			while(enumerator.MoveNext())
			{
				int tabIndex = ((Control)enumerator.Current).TabIndex;
				if(((Control)sender).TabIndex < tabIndex)
				{
					if(nextTabIndex == -1 || tabIndex < nextTabIndex)
					{
						nextTabIndex = tabIndex;
						nextControl = (Control)enumerator.Current;
					}
				}
			}
			nextControl.Focus();
			if(nextControl.GetType() == typeof(System.Windows.Forms.TextBox))
			{
				((TextBox)nextControl).SelectAll();
			}
			return nextControl;
		}

		/// <summary>
		/// Set focus on previous control
		/// </summary>
		/// <param name="sender">current control</param>
		/// <returns>previous control</returns>
		public Control SetPreviousFocus(object sender)
		{
			System.Collections.IEnumerator enumerator = form.Controls.GetEnumerator();
			int previousTabIndex = -1;
			Control previousControl = (Control)sender;
			while(enumerator.MoveNext())
			{
				int tabIndex = ((Control)enumerator.Current).TabIndex;
				if(((Control)sender).TabIndex > tabIndex)
				{
					if(previousTabIndex == -1 || tabIndex > previousTabIndex)
					{
						previousTabIndex = tabIndex;
						previousControl = (Control)enumerator.Current;
					}
				}
			}
			previousControl.Focus();
			if(previousControl.GetType() == typeof(System.Windows.Forms.TextBox))
			{
				((TextBox)previousControl).SelectAll();
			}
			return previousControl;
		}


		/// <summary>
		/// Move to next text box of form
		/// </summary>
		/// <param name="sender">current textbox</param>
		public void MoveOnNextTextBox(object sender)
		{
			Control nextControl = this.SetNextFocus(sender);
			if(nextControl.GetType() == typeof(System.Windows.Forms.TextBox))
			{
				// Move the selection pointer to the end of the text of the control.
				((TextBox)nextControl).Select(nextControl.Text.Length, 0);
			}
		}

		/// <summary>
		/// Move back to previous textbox of form
		/// </summary>
		/// <param name="sender">Current textbox</param>
		public void MoveOnPreviousTextBox(object sender)
		{
			Control previousControl = this.SetPreviousFocus(sender);
			if(previousControl.GetType() == typeof(System.Windows.Forms.TextBox))
			{
				// Move the selection pointer to the end of the text of the control.
				((TextBox)previousControl).Select(previousControl.Text.Length, 0);
			}
		}
		#region IObjectBO Members

		public void Add(object pObjectDetail)
		{
			// TODO:  Add EditorManager.Add implementation
		}

		public void Delete(object pObjectVO)
		{
			// TODO:  Add EditorManager.Delete implementation
		}

		public object GetObjectVO(int pintID, string VOclass)
		{
			// TODO:  Add EditorManager.GetObjectVO implementation
			return null;
		}

		public void UpdateDataSet(System.Data.DataSet dstData)
		{
			// TODO:  Add EditorManager.UpdateDataSet implementation
		}

		public void Update(object pObjectDetail)
		{
			// TODO:  Add EditorManager.Update implementation
		}

		#endregion
	}
}

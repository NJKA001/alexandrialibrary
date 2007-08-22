#region License (MIT)
/***************************************************************************
 *  Copyright (C) 2007 Dan Poage
 ****************************************************************************/

/*  THIS FILE IS LICENSED UNDER THE MIT LICENSE AS OUTLINED IMMEDIATELY BELOW: 
 *
 *  Permission is hereby granted, free of charge, to any person obtaining a
 *  copy of this software and associated documentation files (the "Software"),  
 *  to deal in the Software without restriction, including without limitation  
 *  the rights to use, copy, modify, merge, publish, distribute, sublicense,  
 *  and/or sell copies of the Software, and to permit persons to whom the  
 *  Software is furnished to do so, subject to the following conditions:
 *
 *  The above copyright notice and this permission notice shall be included in 
 *  all copies or substantial portions of the Software.
 *
 *  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR 
 *  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
 *  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE 
 *  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER 
 *  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING 
 *  FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
 *  DEALINGS IN THE SOFTWARE.
 */
#endregion

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

using Alexandria.Client.Controllers;

namespace Alexandria.Client
{
	public partial class About : Form
	{
		#region Constructors
		public About()
		{
			InitializeComponent();
		}
		#endregion
		
		#region Private Fields
		private PluginController pluginController;
		#endregion

		#region Public Methods
		[CLSCompliant(false)]
		public void Initialize(PluginController pluginController)
		{
			this.pluginController = pluginController;

			string license = Alexandria.Client.Properties.Resources.MIT_License;
			license = license.Replace("\\n", "\r\n");

			this.VersionTextBox.Text = Assembly.GetExecutingAssembly().GetName().Version.ToString();
			this.LicenseTextBox.Text = license;
			int i = 0;
			foreach(PluginInfo plugin in pluginController.GetPluginInfo())
			{
				if (plugin.Bitmap != null)
				ImageList.Images.Add(plugin.Bitmap);

				ListViewItem item = new ListViewItem(new string[]{plugin.Title, plugin.Version.ToString()} , i);
				item.ToolTipText = plugin.Description;
				PluginListView.Items.Add(item);
				i++;
			}
		}
		#endregion

		#region Private Event Methods
		private void OKButton_Click(object sender, EventArgs e)
		{
			Close();
		}
		#endregion
	}
}
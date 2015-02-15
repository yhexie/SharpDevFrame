using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Frame.Core.Pad;

namespace Frame.GUI.Pad
{
  public  class SDCatalog: UserControl, IPadContent
	{
		public Control Control {
			get {
				return this;
			}
		}
        public void RedrawContent()
        {
        }
        TreeView treeView = new TreeView();
        public SDCatalog()
        {
            treeView.Dock = DockStyle.Fill;
            this.Controls.Add(treeView);
        }
    }
}

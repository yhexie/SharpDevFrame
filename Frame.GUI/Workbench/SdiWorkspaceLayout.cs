using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Diagnostics;
using System.CodeDom.Compiler;
using System.Windows.Forms;
using System.Reflection;

using ICSharpCode.Core;
using WeifenLuo.WinFormsUI;
using Frame.Core.Pad;
using Frame.Core.ViewContent;
using Frame.Core.Workbench;
using Frame.GUI.Pad;
using Frame.GUI.Common;
using WeifenLuo.WinFormsUI.Docking;

namespace Frame.GUI.WorkBench
{
	/// <summary>
	/// This is the a Workspace with a single document interface.
	/// </summary>
	public class SdiWorkbenchLayout : IWorkbenchLayout
	{
        DefaultWorkbench wbForm;

        const string _layoutFile = "LayoutConfigFile.xml";
        DockPanel dockPanel;
        Dictionary<string, PadContentWrapper> contentHash = new Dictionary<string, PadContentWrapper>();
        // prevent setting ActiveContent to null when application loses focus (e.g. because of context menu popup)
        DockContent lastActiveContent;
		
		[System.Runtime.InteropServices.DllImport("User32.dll")]
		public static extern bool LockWindowUpdate(IntPtr hWnd);

        public SdiWorkbenchLayout() { }

        #region Implementation of IWorkbenchLayout

        public IWorkbenchWindow ActiveWorkbenchwindow
        {
            get
            {
                if (dockPanel == null || dockPanel.ActiveDocument == null) // || dockPanel.ActiveDocument.IsDisposed
                {
                    return null;
                }
                return dockPanel.ActiveDocument as IWorkbenchWindow;
            }
        }

        public object ActiveContent
        {
            get
            {
                DockContent activeContent;
                if (dockPanel == null)
                {
                    activeContent = lastActiveContent;
                }
                else
                {
                    activeContent = dockPanel.ActiveContent as DockContent ?? lastActiveContent;
                }
                lastActiveContent = activeContent;

                if (activeContent == null) // || activeContent.IsDisposed
                {
                    return null;
                }
                if (activeContent is IWorkbenchWindow)
                {
                    return ((IWorkbenchWindow)activeContent).ActiveViewContent;
                }

                if (activeContent is PadContentWrapper)
                {
                    return ((PadContentWrapper)activeContent).PadContent;
                }

                return activeContent;
            }
        }

        public void ShowPad(PadDescriptor content, bool bActivatedIt)
        {
            if (content == null)
            {
                return;
            }
            if (!contentHash.ContainsKey(content.Class))
            {
                DockContent newContent = CreateContent(content);
                newContent.Show(dockPanel);
            }
            else
            {
                contentHash[content.Class].Show();
                if (bActivatedIt)
                    contentHash[content.Class].PanelPane.Activate();
            }
        }

        public void Attach(IWorkbench workbench)
        {
            wbForm = (DefaultWorkbench)workbench;
            wbForm.SuspendLayout();
            wbForm.IsMdiContainer = true;

            dockPanel = new WeifenLuo.WinFormsUI.Docking.DockPanel();
            dockPanel.ActiveAutoHideContent = null;

            WeifenLuo.WinFormsUI.Docking.DockPanelSkin dockPanelSkin1 = new WeifenLuo.WinFormsUI.Docking.DockPanelSkin();
            WeifenLuo.WinFormsUI.Docking.AutoHideStripSkin autoHideStripSkin1 = new WeifenLuo.WinFormsUI.Docking.AutoHideStripSkin();
            WeifenLuo.WinFormsUI.Docking.DockPanelGradient dockPanelGradient1 = new WeifenLuo.WinFormsUI.Docking.DockPanelGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient1 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.DockPaneStripSkin dockPaneStripSkin1 = new WeifenLuo.WinFormsUI.Docking.DockPaneStripSkin();
            WeifenLuo.WinFormsUI.Docking.DockPaneStripGradient dockPaneStripGradient1 = new WeifenLuo.WinFormsUI.Docking.DockPaneStripGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient2 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.DockPanelGradient dockPanelGradient2 = new WeifenLuo.WinFormsUI.Docking.DockPanelGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient3 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.DockPaneStripToolWindowGradient dockPaneStripToolWindowGradient1 = new WeifenLuo.WinFormsUI.Docking.DockPaneStripToolWindowGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient4 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient5 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.DockPanelGradient dockPanelGradient3 = new WeifenLuo.WinFormsUI.Docking.DockPanelGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient6 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient7 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            this.dockPanel = new WeifenLuo.WinFormsUI.Docking.DockPanel();
            this.dockPanel.ActiveAutoHideContent = null;
            this.dockPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dockPanel.DockBackColor = System.Drawing.SystemColors.Control;
            this.dockPanel.Location = new System.Drawing.Point(0, 49);
            this.dockPanel.Name = "dockPanel";
            this.dockPanel.Size = new System.Drawing.Size(657, 264);

            dockPanelGradient1.EndColor = System.Drawing.SystemColors.ControlLight;
            dockPanelGradient1.StartColor = System.Drawing.SystemColors.ControlLight;
            autoHideStripSkin1.DockStripGradient = dockPanelGradient1;
            tabGradient1.EndColor = System.Drawing.SystemColors.Control;
            tabGradient1.StartColor = System.Drawing.SystemColors.Control;
            tabGradient1.TextColor = System.Drawing.SystemColors.ControlDarkDark;
            autoHideStripSkin1.TabGradient = tabGradient1;
            dockPanelSkin1.AutoHideStripSkin = autoHideStripSkin1;
            tabGradient2.EndColor = System.Drawing.SystemColors.ControlLightLight;
            tabGradient2.StartColor = System.Drawing.SystemColors.ControlLightLight;
            tabGradient2.TextColor = System.Drawing.SystemColors.ControlText;
            dockPaneStripGradient1.ActiveTabGradient = tabGradient2;
            dockPanelGradient2.EndColor = System.Drawing.SystemColors.Control;
            dockPanelGradient2.StartColor = System.Drawing.SystemColors.Control;
            dockPaneStripGradient1.DockStripGradient = dockPanelGradient2;
            tabGradient3.EndColor = System.Drawing.SystemColors.ControlLight;
            tabGradient3.StartColor = System.Drawing.SystemColors.ControlLight;
            tabGradient3.TextColor = System.Drawing.SystemColors.ControlText;
            dockPaneStripGradient1.InactiveTabGradient = tabGradient3;
            dockPaneStripSkin1.DocumentGradient = dockPaneStripGradient1;
            tabGradient4.EndColor = System.Drawing.SystemColors.ActiveCaption;
            tabGradient4.LinearGradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            tabGradient4.StartColor = System.Drawing.SystemColors.GradientActiveCaption;
            tabGradient4.TextColor = System.Drawing.SystemColors.ActiveCaptionText;
            dockPaneStripToolWindowGradient1.ActiveCaptionGradient = tabGradient4;
            tabGradient5.EndColor = System.Drawing.SystemColors.Control;
            tabGradient5.StartColor = System.Drawing.SystemColors.Control;
            tabGradient5.TextColor = System.Drawing.SystemColors.ControlText;
            dockPaneStripToolWindowGradient1.ActiveTabGradient = tabGradient5;
            dockPanelGradient3.EndColor = System.Drawing.SystemColors.ControlLight;
            dockPanelGradient3.StartColor = System.Drawing.SystemColors.ControlLight;
            dockPaneStripToolWindowGradient1.DockStripGradient = dockPanelGradient3;
            tabGradient6.EndColor = System.Drawing.SystemColors.GradientInactiveCaption;
            tabGradient6.LinearGradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            tabGradient6.StartColor = System.Drawing.SystemColors.GradientInactiveCaption;
            tabGradient6.TextColor = System.Drawing.SystemColors.ControlText;
            dockPaneStripToolWindowGradient1.InactiveCaptionGradient = tabGradient6;
            tabGradient7.EndColor = System.Drawing.Color.Transparent;
            tabGradient7.StartColor = System.Drawing.Color.Transparent;
            tabGradient7.TextColor = System.Drawing.SystemColors.ControlDarkDark;
            dockPaneStripToolWindowGradient1.InactiveTabGradient = tabGradient7;
            dockPaneStripSkin1.ToolWindowGradient = dockPaneStripToolWindowGradient1;
            dockPanelSkin1.DockPaneStripSkin = dockPaneStripSkin1;
            this.dockPanel.Skin = dockPanelSkin1;

            dockPanel.Dock = DockStyle.Fill;
            wbForm.Controls.Add(dockPanel);
            wbForm.Controls.Add(wbForm.TopToolStrip);//添加工具条
            wbForm.Controls.Add(wbForm.TopMenu);//添加主菜单

            LoadConfiguration();

            ShowPads();
            //ShowViewContents();
            RedrawAllComponents();

            wbForm.ResumeLayout(false);
        }

        public void Detach()
        {
            StoreConfiguration();

            DetachPadContents(true);
            DetachViewContents(true);

            try
            {
                if (dockPanel != null)
                {
                    dockPanel.Dispose();
                    dockPanel = null;
                }
            }
            catch (Exception e)
            {
                MessageService.ShowError(e);
            }
            if (contentHash != null)
            {
                contentHash.Clear();
            }

            wbForm.Controls.Clear();
        }

        public void ShowPad(PadDescriptor content)
        {
            ShowPad(content, false);
        }

		public IWorkbenchWindow ShowView(IViewContent content)
		{
			if (content.WorkbenchWindow is SdiWorkspaceWindow) {
				SdiWorkspaceWindow oldSdiWindow = (SdiWorkspaceWindow)content.WorkbenchWindow;
				if (!oldSdiWindow.IsDisposed) {
					oldSdiWindow.Show(dockPanel);
					return oldSdiWindow;
				}
			}
			if (!content.Control.Visible) {
				content.Control.Visible = true;
			}
			content.Control.Dock = DockStyle.Fill;
			SdiWorkspaceWindow sdiWorkspaceWindow = new SdiWorkspaceWindow(content);
			if (dockPanel != null) {
				sdiWorkspaceWindow.Show(dockPanel);
			}
			
			return sdiWorkspaceWindow;
		}

        public void RedrawAllComponents()
        {
            // redraw correct pad content names (language changed).
            foreach (PadDescriptor padDescriptor in ((IWorkbench)wbForm).PadContentCollection)
            {
                DockContent c = contentHash[padDescriptor.Class];
                if (c != null)
                {
                    c.Text = StringParser.Parse(padDescriptor.Title);
                }
            }
            //RedrawMainMenu();
        }

        public void LoadConfiguration()
        {
            if (dockPanel != null)
            {
                string configPath = FileUtility.Combine(PropertyService.ConfigDirectory, "layouts", _layoutFile);

                if (!File.Exists(configPath))
                    return;

                LockWindowUpdate(wbForm.Handle);
                try
                {
                    dockPanel.LoadFromXml(configPath, new DeserializeDockContent(GetContent));
                }
                catch { }
                finally
                {
                    LockWindowUpdate(IntPtr.Zero);
                }
            }
        }

        public void StoreConfiguration()
        {
            try
            {
                if (dockPanel != null)
                {
                    string configPath = Path.Combine(PropertyService.ConfigDirectory, "layouts");
                    if (!Directory.Exists(configPath))
                        Directory.CreateDirectory(configPath);
                    dockPanel.SaveAsXml(Path.Combine(configPath, _layoutFile));
                }
            }
            catch (Exception e)
            {
                MessageService.ShowError(e);
            }
        }

        #endregion

        #region helper methods

        DockContent GetContent(string padTypeName)
        {
            if (padTypeName.StartsWith("ViewContent|"))
            {
                string filePath = padTypeName.Substring("ViewContent|".Length);
                if (File.Exists(filePath))
                {
                    IWorkbenchWindow contentWindow = FileService.OpenFile(filePath);
                    if (contentWindow is DockContent)
                        return (DockContent)contentWindow;
                    else
                        return null;
                }
                else
                    return null;
            }

            foreach (PadDescriptor content in WorkbenchSingleton.Workbench.PadContentCollection)
            {
                if (content.Class == padTypeName)
                {
                    return CreateContent(content);
                }
            }
            return null;
        }

        void ShowPads()
        {
            foreach (PadDescriptor content in WorkbenchSingleton.Workbench.PadContentCollection)
            {
                if (!contentHash.ContainsKey(content.Class))
                {
                    ShowPad(content);
                }
            }
            // ShowPads could create new pads if new addins have been installed, so we
            // need to call AllowInitialize here instead of in LoadLayoutConfiguration
            foreach (PadContentWrapper content in contentHash.Values)
            {
                content.AllowInitialize();
            }
        }

        void ShowViewContents()
        {
            foreach (IViewContent content in WorkbenchSingleton.Workbench.ViewContentCollection)
            {
                ShowView(content);
            }
        }

        void DetachPadContents(bool dispose)
        {
            foreach (PadContentWrapper padContentWrapper in contentHash.Values)
            {
                padContentWrapper.allowInitialize = false;
            }
            foreach (PadDescriptor content in ((DefaultWorkbench)wbForm).PadContentCollection)
            {
                try
                {
                    PadContentWrapper padContentWrapper = contentHash[content.Class];
                    padContentWrapper.DockPanel = null;
                    if (dispose)
                    {
                        padContentWrapper.DetachContent();
                        padContentWrapper.Dispose();
                    }
                }
                catch (Exception e) { MessageService.ShowError(e); }
            }
            if (dispose)
            {
                contentHash.Clear();
            }
        }

        void DetachViewContents(bool dispose)
        {
            foreach (IViewContent viewContent in WorkbenchSingleton.Workbench.ViewContentCollection)
            {
                try
                {
                    SdiWorkspaceWindow f = (SdiWorkspaceWindow)viewContent.WorkbenchWindow;
                    f.DockPanel = null;
                    if (dispose)
                    {
                        viewContent.WorkbenchWindow = null;
                        f.DetachContent();
                        f.Dispose();
                    }
                }
                catch (Exception e) { MessageService.ShowError(e); }
            }
        }

        PadContentWrapper CreateContent(PadDescriptor content)
        {
            if (contentHash.ContainsKey(content.Class))
            {
                return contentHash[content.Class];
            }
            ICSharpCode.Core.Properties properties = (ICSharpCode.Core.Properties)PropertyService.Get("Workspace.ViewMementos", new ICSharpCode.Core.Properties());

            PadContentWrapper newContent = new PadContentWrapper(content);
            if (content.Icon != null)
            {
                newContent.Icon = IconService.GetIcon(content.Icon);
            }
            newContent.Text = StringParser.Parse(content.Title);//不支持中文？
            contentHash[content.Class] = newContent;
            return newContent;
        }

        #endregion

    }
}

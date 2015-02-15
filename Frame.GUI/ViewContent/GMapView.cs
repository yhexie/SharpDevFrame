using System;
using System.Collections.Generic;
using System.Text;
using Frame.Core.ViewContent;
using System.Windows.Forms;

namespace Frame.GUI.ViewContent
{
    public class MapViewDisplayBinding : IDisplayBinding
    {
        public virtual bool CanCreateContentForFile(string fileName)
        {
            return true;
        }

        public virtual IViewContent CreateContentForFile(string fileName)
        {
            GMapViewWrapper b2 = new GMapViewWrapper();

            //b2.Load(fileName);
            return b2;
        }

    }
	
   public class GMapViewWrapper : AbstractViewContent
    {
       ucMap ucmap;
       public override Control Control
       {
           get
           {
               return ucmap;
           }
       }

       
       public GMapViewWrapper()
       {
           ucmap = new ucMap();
           ucmap.Dock = DockStyle.Fill;
           TitleChanged(null, null);
       }
       void TitleChanged(object sender, EventArgs e)
       {
           string title = "地图文档";
           if (title != null)
               title = title.Trim();
           if (title == null || title.Length == 0)
               TitleName = "MapView";
           else
               TitleName = title;
       }

       public override void Load(string fileName)
       {
           
       }

       public override void Dispose()
       {
           ucmap.Dispose();
           ucmap = null;
       }
    }
}

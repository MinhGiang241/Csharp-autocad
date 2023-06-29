using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Internal;
using Autodesk.AutoCAD.Runtime;

using AcAp = Autodesk.AutoCAD.ApplicationServices.Application;

[assembly: CommandClass(typeof(AutoCAD.Init))]

namespace AutoCAD
{
    public class Init
    {
        private Boolean IsSavedFile()
        {
            int result = Convert.ToInt16(AcAp.GetSystemVariable("DWGTITLED"));

            if (result != 0) {
                return true;
            };

            return false;
        }

       

        [CommandMethod("ctestCmmand")]
        public void cmdFirstCommand()
        {
            Document doc = AcAp.DocumentManager.MdiActiveDocument;
            var db = doc.Database;
            var ed = doc.Editor;
        
            if (IsSavedFile())
            {
                ed.WriteMessage("\n File đả lưu");
            } 
            else
            {
                ed.WriteMessage("\n File chưa lưu");

            }

           
        }


        [CommandMethod("MGVersion")]

        public void CadVersion()
        {
            Editor ed = AcAp.DocumentManager.MdiActiveDocument.Editor;

            switch (AcAp.DocumentManager.MdiActiveDocument.Database.LastSavedAsVersion)
            {
                case (Autodesk.AutoCAD.DatabaseServices.DwgVersion.AC1032):
                    ed.WriteMessage("\n Autocad 2018");
                    break;
                case (Autodesk.AutoCAD.DatabaseServices.DwgVersion.AC1027):
                    ed.WriteMessage("\n Autocad 2013");
                    break;
                case (Autodesk.AutoCAD.DatabaseServices.DwgVersion.AC1024):
                    ed.WriteMessage("\n Autocad 2010");
                    break;
                case (Autodesk.AutoCAD.DatabaseServices.DwgVersion.AC1021):
                    ed.WriteMessage("\n Autocad 2007");
                    break;
                default:
                    ed.WriteMessage("\n Autocad khác");
                    break;
            }
        }

     
    }
}

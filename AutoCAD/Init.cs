﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
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

        [CommandMethod("LoginName")]
        public void showLoginName()
        {
            Editor ed = AcAp.DocumentManager.MdiActiveDocument.Editor;
            string loginName = AcAp.GetSystemVariable("loginName").ToString();
            ed.WriteMessage(loginName);

        }

        [CommandMethod("trycat")]
        public void trycatchfunc()
        {
            try {
                throw new Autodesk.AutoCAD.Runtime.Exception(ErrorStatus.AnonymousEntry,"không chấp nhân");
                // đếm số bản vẽ đang mở

            } catch (Autodesk.AutoCAD.Runtime.Exception ex) {
                Application.ShowAlertDialog("lỗi khi giá trị bằng 9 " + ex.Message);
                
            }
        }

        [CommandMethod("createnewCad")]
        public void newCad()
        {
            DocumentCollection docCol = Application.DocumentManager;

            Document doc1 = docCol.Add(@"C:\Users\minhg\AppData\Local\Autodesk\AutoCAD 2022\R24.1\enu\Template\acadiso.dwt");

            docCol.MdiActiveDocument = doc1;
        }
        //DatabaseService
        [CommandMethod("mgScale")]
        public void scale()
        {
            Database db = Application.DocumentManager.MdiActiveDocument.Database;

            db.Ltscale = 100;
        }
        [CommandMethod("getObjecId")]
        public void getObjecID()
        {
            Database db = Application.DocumentManager.MdiActiveDocument.Database;
            Document doc = Application.DocumentManager.MdiActiveDocument;
            var ed = doc.Editor;

            ObjectId layerId = db.LayerTableId;

            bool iErr=  layerId.IsErased;

            Transaction trans = db.TransactionManager.StartTransaction();

            LayerTable layertb = trans.GetObject(layerId, OpenMode.ForRead) as LayerTable;

            int count = 0;

            foreach(ObjectId ob in layertb)
            {
                count += 1;
                ed.WriteMessage("hh: "+ ob.OldId.ToString());
            }
            // đóng transaction
            trans.Commit();

            ed.WriteMessage("số lượng layer trong bản vẽ là : " + count);




        }

    }
}

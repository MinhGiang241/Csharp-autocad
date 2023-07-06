using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.Runtime;
using System;

[assembly: CommandClass(typeof(AutoCAD.Draw))]

namespace AutoCAD
{
    public class Draw
    {
        [CommandMethod("drLine")]
        public void DrawLine()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            Database db = HostApplicationServices.WorkingDatabase;
            Transaction trans = db.TransactionManager.StartTransaction();

            try
            {

                // nơi vẽ 
                ObjectId blockId = db.CurrentSpaceId;
                BlockTableRecord curSpace = trans.GetObject(blockId, OpenMode.ForWrite) as BlockTableRecord;

                Point3d point1 = new Point3d(0, 0, 0);
                Point3d point2 = new Point3d(10, 10, 0);

                Line linePb = new Line(point1, point2);
                curSpace.AppendEntity(linePb);

                trans.AddNewlyCreatedDBObject(linePb, true);

            }
            catch (Autodesk.AutoCAD.Runtime.Exception ex)
            {
                Application.ShowAlertDialog("Tạo line không thành công lỗi: " + ex.Message);
            }
            trans.Commit();

        }

        [CommandMethod("CRPoint")]
        public void GetPoint()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            PromptPointOptions prPointOption = new PromptPointOptions("Click chuột để chọn điểm");
            prPointOption.AllowArbitraryInput = false;
            prPointOption.AllowNone = true;

            PromptPointResult prPointResult1 = ed.GetPoint(prPointOption);
            if (prPointResult1.Status != PromptStatus.OK) return;

            //hiện đường line
            prPointOption.BasePoint = prPointResult1.Value;
            prPointOption.UseBasePoint = true;

            PromptPointResult prPointResult2 = ed.GetPoint(prPointOption);
            if (prPointResult1.Status != PromptStatus.OK) return;

            DrawLine(prPointResult1.Value, prPointResult2.Value);
            ed.WriteMessage("\nPoint1: " + prPointResult1.Value.ToString() + "\nPoint2: " + prPointResult2.Value.ToString());


        }

        [CommandMethod("getdistance")]
        public void GetDistance()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            Database db = HostApplicationServices.WorkingDatabase;

            PromptDistanceOptions prDistancOptions = new PromptDistanceOptions("\n Chọn hoặc nhập khoảng cách: ");

            prDistancOptions.AllowArbitraryInput = false;
            prDistancOptions.AllowNone = true;
            prDistancOptions.AllowNegative = false;
            prDistancOptions.AllowZero = false;
            prDistancOptions.DefaultValue = 0;
            prDistancOptions.Only2d = true;
            prDistancOptions.UseDefaultValue = true;
            prDistancOptions.Keywords.Add("YES");
            prDistancOptions.Keywords.Add("NO");

            PromptDoubleResult prDis = ed.GetDistance(prDistancOptions);

            if (prDis.Status == PromptStatus.Keyword)
            {
                ed.WriteMessage("\nKeyword người dùng đã chọn là: " + prDis.StringResult);
            }
            else
            {
                ed.WriteMessage("\n Khoảng cách là: " + Math.Round(prDis.Value, db.Luprec, MidpointRounding.AwayFromZero));

            };



        }

        [CommandMethod("getObject")]
        public void GetObjject()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            Database db = HostApplicationServices.WorkingDatabase;

            PromptEntityOptions prEntiryOptions = new PromptEntityOptions("\nChọn 1 đường thẳng");
            prEntiryOptions.AllowNone = true;
            prEntiryOptions.AllowObjectOnLockedLayer = true;
            prEntiryOptions.SetRejectMessage("\nĐối tượng không phải đường thẳng");
            prEntiryOptions.AddAllowedClass(typeof(Line), true);

            PromptEntityResult prEntityResult = ed.GetEntity(prEntiryOptions);
            if (prEntityResult.Status != PromptStatus.OK) return;

            Transaction trans = db.TransactionManager.StartTransaction();
            Line lineObj = trans.GetObject(prEntityResult.ObjectId, OpenMode.ForRead) as Line;

            ed.WriteMessage("\n Chiều dài line là: " + Math.Round(lineObj.Length, db.Luprec, MidpointRounding.AwayFromZero));

            trans.Commit();
        }

        [CommandMethod("TotalLength")]
        public void GetObjjects()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            Database db = HostApplicationServices.WorkingDatabase;

            PromptSelectionOptions prSelect = new PromptSelectionOptions();
            prSelect.AllowDuplicates = false;
            prSelect.MessageForAdding = "\n Chọn tập hợp dường thẳng";
            prSelect.MessageForRemoval = "\n Chọn tập hợp dường thẳng để loại bỏ";
            prSelect.RejectObjectsFromNonCurrentSpace = false;
            prSelect.RejectObjectsOnLockedLayers = true;
            prSelect.RejectPaperspaceViewport = true;

            TypedValue[] typedValues = new TypedValue[1];
            typedValues[0] = new TypedValue((int)DxfCode.Start, "LINE");
            SelectionFilter selFilter = new SelectionFilter(typedValues);

            PromptSelectionResult prSelResult = ed.GetSelection(prSelect, selFilter);
            if (prSelResult.Status != PromptStatus.OK) return;

            Transaction trans = db.TransactionManager.StartTransaction();

            try
            {
                SelectionSet selSet = prSelResult.Value;
                Line lineObj;
                double lengTotal = 0.0;

                foreach (SelectedObject line in selSet)
                {
                    lineObj = trans.GetObject(line.ObjectId, OpenMode.ForRead) as Line;

                    lengTotal += lineObj.Length;

                }

                ed.WriteMessage("\n Tổng chiểu dài các line là: " + Math.Round(lengTotal, db.Luprec, MidpointRounding.AwayFromZero));

            }
            catch (Autodesk.AutoCAD.Runtime.Exception ex)
            {
                Application.ShowAlertDialog("Lỗi tính tổng chiều dài: " + ex.Message);
            }
            trans.Commit();

        }

        #region methodHelp

        public void DrawLine(Point3d point1, Point3d point2)
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            Database db = HostApplicationServices.WorkingDatabase;
            Transaction trans = db.TransactionManager.StartTransaction();

            try
            {

                // nơi vẽ 
                ObjectId blockId = db.CurrentSpaceId;
                BlockTableRecord curSpace = trans.GetObject(blockId, OpenMode.ForWrite) as BlockTableRecord;

                Line linePb = new Line(point1, point2);
                curSpace.AppendEntity(linePb);

                trans.AddNewlyCreatedDBObject(linePb, true);

            }
            catch (Autodesk.AutoCAD.Runtime.Exception ex)
            {
                Application.ShowAlertDialog("Tạo line không thành công lỗi: " + ex.Message);
            }
            trans.Commit();
        }
        #endregion
    }
}

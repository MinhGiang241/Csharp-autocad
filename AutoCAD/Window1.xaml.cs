using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using System;
using System.Windows;

namespace AutoCAD
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }


        private String GetLayerName()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            Database db = HostApplicationServices.WorkingDatabase;
            Transaction trans = db.TransactionManager.StartTransaction();
            ObjectId layerId = db.LayerTableId;
            LayerTable layertb = trans.GetObject(layerId, OpenMode.ForRead) as LayerTable;
            string layerName = "";
            foreach (ObjectId ob in layertb)
            {
                LayerTableRecord layer = trans.GetObject(ob, OpenMode.ForRead) as LayerTableRecord;
                layerName += "\n" + layer.Name;
            }

            trans.Commit();
            return layerName;
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void btnLayerName_Click(object sender, RoutedEventArgs e)
        {
            this.layerName.Text = GetLayerName();
        }
    }
}

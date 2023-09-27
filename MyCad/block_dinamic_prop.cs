using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.Runtime;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCad
{
    public class block_dinamic_prop
    {
        [CommandMethod("Elem")]
        public void TestCommand()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor ed = doc.Editor;

            Transaction tr = db.TransactionManager.StartTransaction();

            BlockTable bt = (BlockTable)tr.GetObject(db.BlockTableId, OpenMode.ForWrite);
            ed.WriteMessage(bt["prz_podl"] + "");

            BlockTableRecord btr = tr.GetObject(bt["prz_podl"], OpenMode.ForWrite) as BlockTableRecord;

            Point3d point = new Point3d(0, 0, 0);
            BlockReference br = new BlockReference(point, btr.Id);
            br.BlockTableRecord = btr.Id;

            DynamicBlockReferencePropertyCollection properties = br.DynamicBlockReferencePropertyCollection;
            for (int i = 0; i < properties.Count; i++)
            {
                DynamicBlockReferenceProperty property = properties[i];
                if (property.PropertyName == "par_l")
                {
                    ed.WriteMessage(property.Value + "");
                    property.Value = 500.0;
                }
            }
            tr.Commit();
        }
    }
}

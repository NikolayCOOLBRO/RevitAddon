using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Analysis;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using RevitAddonPanelEditor.Command.Marking.Collector;
using RevitAddonPanelEditor.Command.Marking.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAddonPanelEditor.Command.Marking
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    public class MarkingCommand : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var uidoc = commandData.Application.ActiveUIDocument;

            Document doc = uidoc.Document;


            Reference elemRef = uidoc.Selection.PickObject(ObjectType.Element);

            // Получение элемента из ссылки
            FamilyInstance familyInstance = doc.GetElement(elemRef) as FamilyInstance;

            // Получение списка геометрии элемента
            GeometryElement geomElem = familyInstance.get_Geometry(new Options());

            // Получение списка поверхностей элемента
            List<Face> surfaces = new List<Face>();
            foreach (GeometryObject geomObj in geomElem)
            {
                if (geomObj is GeometryInstance)
                {
                    GeometryElement instGeom = (geomObj as GeometryInstance).GetInstanceGeometry();
                    foreach (GeometryObject instObj in instGeom)
                    {
                        if (instObj is Solid)
                        {
                            Solid solid = instObj as Solid;
                            foreach (Face face in solid.Faces)
                            {
                                surfaces.Add(face);
                            }
                        }
                        else if (instObj is Face)
                        {
                            surfaces.Add(instObj as Face);
                        }
                    }
                }
                else if (geomObj is Solid)
                {
                    Solid solid = geomObj as Solid;
                    foreach (Face face in solid.Faces)
                    {
                        surfaces.Add(face);
                    }
                }
                else if (geomObj is Face)
                {
                    surfaces.Add(geomObj as Face);
                }
            }

            // Вывод количества поверхностей элемента

            surfaces.Shuffle();

            var surface = FindFrontFace(surfaces);

            int index = -1;
            
            for (int i = 0; i < surfaces.Count; i++)
            {
                if (surface.GraphicsStyleId.Equals(surface.GraphicsStyleId))
                {
                    index = i;
                    break;
                }
            }

            TaskDialog.Show("Поверхности", "Элемент содержит " + index.ToString());

            /*
            var app = commandData.Application;

            var docs = app.ActiveUIDocument.Document;

            var family = new FilteredElementCollector(docs)
                                        .OfClass(typeof(FamilyInstance))
                                        .ToElements()
                                        .Where(item => item.Name.Equals("1000х6000"))
                                        .OfType<FamilyInstance>()
                                        .ToList();

            var collector = CollectDivide(family);

            GetCurve(family[0]);


            TaskDialog.Show("Скрипт выполнился", "Промааркировалось");

            transaction.Commit();
            */
            return Result.Succeeded;
        }

        private List<CollecttorFamilyInstaceAndHost> CollectDivide(List<FamilyInstance> familyInstances)
        {
            var result = new List<CollecttorFamilyInstaceAndHost>();

            foreach (var item in familyInstances)
            {
                var host = item.Host.Id.ToString();
                var finder = result.Find(i => i.Host.Equals(host));

                if (finder != null)
                {
                    finder.Objects.Add(item);
                }
                else
                {
                    var collector = new CollecttorFamilyInstaceAndHost(host);
                    collector.Objects.Add(item);
                    result.Add(collector);
                    item.GetTransform().Origin.GetLength();
                }
            }

            return result;
        }

        private Face FindFrontFace(List<Face> faces)
        {
            // Определяем направление взгляда наблюдателя
            XYZ viewDir = new XYZ(0, 0, 1);

            Face frontFace = null;
            double maxDot = double.NegativeInfinity;

            // Находим лицевую сторону путем вычисления косинуса между вектором нормали поверхности и направлением взгляда
            foreach (Face face in faces)
            {
                XYZ normal = face.ComputeNormal(new UV());
                double dot = normal.DotProduct(viewDir);

                if (dot > maxDot)
                {
                    maxDot = dot;
                    frontFace = face;
                }
            }

            return frontFace;
        }


        private void GetCurve(FamilyInstance familyInstance)
        {
            // Получение списка геометрии элемента
            GeometryElement geomElem = familyInstance.get_Geometry(new Options());

            // Получение списка кривых поверхностей
            var curves = new List<CurveLoop>();
            foreach (GeometryObject geomObj in geomElem)
            {
                if (geomObj is GeometryInstance)
                {
                    GeometryElement instGeom = (geomObj as GeometryInstance).GetInstanceGeometry();
                    foreach (GeometryObject instObj in instGeom)
                    {
                        if (instObj is Solid)
                        {
                            Solid solid = instObj as Solid;
                            foreach (Face face in solid.Faces)
                            {
                                var faceCurves = face.GetEdgesAsCurveLoops();
                                if (faceCurves != null)
                                {
                                    curves.AddRange(faceCurves);
                                }
                            }
                        }
                        else if (instObj is Face)
                        {
                            var faceCurves = (instObj as Face).GetEdgesAsCurveLoops();
                            if (faceCurves != null)
                            {
                                curves.AddRange(faceCurves);
                            }
                        }
                    }
                }
                else if (geomObj is Solid)
                {
                    Solid solid = geomObj as Solid;
                    foreach (Face face in solid.Faces)
                    {
                        var faceCurves = face.GetEdgesAsCurveLoops();
                        if (faceCurves != null)
                        {
                            curves.AddRange(faceCurves);
                        }
                    }
                }
                else if (geomObj is Face)
                {
                    var faceCurves = (geomObj as Face).GetEdgesAsCurveLoops();
                    if (faceCurves != null)
                    {
                        curves.AddRange(faceCurves);
                    }
                }
            }

            // Вывод количества кривых составляющих элемента
            TaskDialog.Show("Кривые составляющие поверхности", "Элемент содержит " + curves.Count.ToString() + " кривых");
        }

        private void SetMarkCombination(List<Combination> combinations)
        {
            foreach (var combination in combinations)
            {
                foreach (var item in combination.Elements)
                {
                    item.Element.LookupParameter("ADSK_Зона").Set(combination.GlobalCombination.ToString());
                }
            }
        }

        
    }
}

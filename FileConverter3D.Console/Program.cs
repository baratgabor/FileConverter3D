using System.Diagnostics;
using System.IO;

namespace FileConverter3D.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            //var source = @"C:\Users\barat\Desktop\3D Mesh Sample Files\StanfordBunnyTriangulated.obj";
            //var source = @"C:\Users\barat\Desktop\3D Mesh Sample Files\maya.obj";
            //var source = @"C:\Users\barat\Desktop\testCopy.stl";

            var source = @"C:\Users\barat\Desktop\3D Mesh Sample Files\maya.obj";
            var target = @"C:\Users\barat\Desktop\test";

            var model = FileConverter3D.Import.ObjAscii(source);

            Debug.WriteLine("Model surface area: " + FileConverter3D.Analyze.CalculateSurfaceArea(model));
            Debug.WriteLine("Model volume: " + FileConverter3D.Analyze.CalculateVolume(model));
            Debug.WriteLine("Is point in mesh: " + FileConverter3D.Analyze.Intersect(model, new Vector3(-0.4f,0,0)));

            FileConverter3D.Transform.GetModelTransform()
                .AddScale(2, 2, 2)
                .AddTranslation(5, 10, 0)
                .AddRotation(47, 72, 18)
                .Apply(model);

            Debug.WriteLine("Model surface area: " + FileConverter3D.Analyze.CalculateSurfaceArea(model));
            Debug.WriteLine("Model volume: " + FileConverter3D.Analyze.CalculateVolume(model));
            Debug.WriteLine("Is point in mesh: " + FileConverter3D.Analyze.Intersect(model, new Vector3(-0.4f, 0, 0)));

            if (File.Exists(target + ".obj"))
                File.Delete(target + ".obj");
            FileConverter3D.Export.ObjAscii(model, target + ".obj");

            if (File.Exists(target + ".stl"))
                File.Delete(target + ".stl");
            FileConverter3D.Export.StlBinary(model, target + ".stl");
        }
    }
}

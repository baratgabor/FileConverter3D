using System.IO;

namespace FileConverter3D.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            //var source = @"C:\Users\barat\Desktop\3D Mesh Sample Files\StanfordBunnyTriangulated.obj";
            //var source = @"C:\Users\barat\Desktop\3D Mesh Sample Files\maya.obj";
            var source = @"C:\Users\barat\Desktop\testCopy.stl";
            var target = @"C:\Users\barat\Desktop\test.stl";

            if (File.Exists(target))
                File.Delete(target);

            FileConverter3D.Export.StlBinary(
                FileConverter3D.Import.StlBinary(source),
                target
            );
        }
    }
}

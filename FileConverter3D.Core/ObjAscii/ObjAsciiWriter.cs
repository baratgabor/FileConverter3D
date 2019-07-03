using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FileConverter3D.ObjAscii
{
    public class ObjAsciiWriter : IDataWriter<string>
    {
        protected const string Header = "# Produced by Leaky.FileConverter3D";

        public void Write(Stream destination, IEnumerable<string> data)
        {
            using (var writer = new StreamWriter(destination))
            {
                writer.WriteLine(Header);

                foreach (var item in data)
                    writer.WriteLine(item);
            }
        }
    }
}

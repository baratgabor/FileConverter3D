using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FileConverter3D.ObjAscii
{
    class ValueToObjAsciiWriter : IFileWriter<IValue>, IValueVisitor
    {
        protected readonly StringBuilder _builder;

        public void Write(IEnumerable<IValue> data, Stream destination)
        {
            
        }

        public void Visit(Vertex vertex)
        {
            throw new System.NotImplementedException();
        }

        public void Visit(Normal normal)
        {
            throw new System.NotImplementedException();
        }

        public void Visit(TextureCoord textureCoord)
        {
            throw new System.NotImplementedException();
        }

        public void Visit(Face face)
        {
            throw new System.NotImplementedException();
        }
    }
}

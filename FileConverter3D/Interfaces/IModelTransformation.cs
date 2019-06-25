using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileConverter3D
{
    public interface IModelTransformation
    {
        IModel Transform(IModel model);
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CHCHALC.Services
{
    public interface IImageResizer
    {
        byte[] ResizeImage(byte[] imageData, float maxWidth);
    }
}

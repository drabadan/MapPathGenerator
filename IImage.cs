using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace MapPathGenerator
{
    public interface IImage
    {
        string MapHeader { get; set; }
        BitmapImage MapImage { get; set; }
    }
}

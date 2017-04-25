using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace MapPathGenerator
{
    public class MainWindowViewModel : DependencyObject
    {
        public IEnumerable<ImageViewModel> Maps { get; set; }

        public MainWindowViewModel()
        {
            Maps = new List<ImageViewModel>()
            {
                LoadImageViewModel("Felucca", @"\Img\Felucca.bmp"),
                LoadImageViewModel("Malas", @"\Img\Malas.bmp"),
                LoadImageViewModel("Tokuno", @"\Img\Tokuno.bmp"),
                LoadImageViewModel("TerMur", @"\Img\TerMur.bmp")
            };
        }

        private ImageViewModel LoadImageViewModel(string header, string imageFilePath)
        {            
            try
            {
                ImageViewModel model = new ImageViewModel(header, new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + imageFilePath)));
                return model;
            }
            catch (Exception) { }

            return null;            
        }

    }
}

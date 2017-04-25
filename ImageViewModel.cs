using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MapPathGenerator
{
    public class ImageViewModel : DependencyObject, IImage
    {        
        public ObservableCollection<MapImageModel> DataCollection
        {
            get { return (ObservableCollection<MapImageModel>)GetValue(DataCollectionProperty); }
            set { SetValue(DataCollectionProperty, value); }
        }        
        public static readonly DependencyProperty DataCollectionProperty =
            DependencyProperty.Register("DataCollection", typeof(ObservableCollection<MapImageModel>), typeof(ImageViewModel), new PropertyMetadata(null));

        public BitmapImage MapImage { get; set; }
        public string MapHeader { get; set; }

        public ImageViewModel(string header, BitmapImage mapImage)
        {
            DataCollection = new ObservableCollection<MapImageModel>();

            MapHeader = header;
            MapImage = mapImage;

            try
            {
                using (StreamReader file = File.OpenText(AppDomain.CurrentDomain.BaseDirectory + @"\Waypoints\" + header + ".json"))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    DataCollection = (ObservableCollection<MapImageModel>)serializer.Deserialize(file, typeof(ObservableCollection<MapImageModel>));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }            
        }
        
        public void MapImage_OnMouseClick(object sender, MouseButtonEventArgs e)
        {
            var point = e.GetPosition((Image)sender);
            DataCollection.Add(new MapImageModel(DataCollection.Count, point));
            SaveItems();
        }

        public void Rectangle_OnMouseClick(object sender, MouseButtonEventArgs e)
        {
            var grid = (Grid)sender;
            var children = grid.Children;

            MapImageModel model = null;
            foreach (var child in children)
                if (child is Rectangle)
                    model = (MapImageModel)((Rectangle)child).DataContext;

            DataCollection.Remove(model);
        }
        
        private void SaveItems()
        {
            try
            {
                using (StreamWriter file = File.CreateText(AppDomain.CurrentDomain.BaseDirectory + @"\Waypoints\" + MapHeader + ".json"))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(file, DataCollection);
                }
            }catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MapPathGenerator
{
    public class MapImageModel
    {
        const double _rectSize = 20.0;

        [JsonConstructor]
        public MapImageModel(int index, Point point)
            :this(index, Math.Round(point.X), Math.Round(point.Y))
        {
        }

        public MapImageModel(int index, double x, double y)
            :this(index, new Rect(x - (_rectSize/2), y - (_rectSize/2), _rectSize, _rectSize), new Point(x, y))
        {
        }

        public MapImageModel(int index, Rect rect, Point point)
        {
            Index = index;
            Rect = rect;
            Point = point;
        }
        [JsonProperty]
        public int Index { get; set; }

        [JsonIgnore]
        public Rect Rect { get; set; }

        [JsonProperty]
        public Point Point { get; set; }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Collections;

namespace MapPathGenerator
{
    public static class PointsSaver
    {
        public static Dictionary<string, List<Point>> PointsDictionary { get; private set; } = new Dictionary<string, List<Point>>() {
            { "Felucca", new List<Point>() },
            { "Malas", new List<Point>() },
            { "Tokuno", new List<Point>() },
            { "TerMur", new List<Point>() }
        };

        public static void AddPoint(string mapHeader, Point point)
        {
            if (!PointsDictionary.Keys.Contains(mapHeader))
            {
                MessageBox.Show("There is no such map in a dictionary, contact the developer");
                return;
            }

            PointsDictionary[mapHeader].Add(point);
            try
            {
                SavePoints(mapHeader);
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static void SaveAllPoints()
        {
            foreach (string key in PointsDictionary.Keys)
                SavePoints(key);
        }

        private static void SavePoints(string fileName)
        {
            using (StreamWriter file = File.CreateText(AppDomain.CurrentDomain.BaseDirectory + @"\Waypoints\" + fileName + ".json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, PointsDictionary[fileName]);
            }
        }
        
    }
}

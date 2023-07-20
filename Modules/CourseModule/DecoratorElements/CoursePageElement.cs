﻿namespace SmartEdu.Modules.CourseModule.DecoratorElements
{
    public struct Coord
    {
        public Coord() 
        {
            x = 0;
            y = 0;
        }

        public Coord(string coords) : this()
        {
            SetCoords(coords);
        }

        public int x;
        public int y;

        public string GetCoords() 
        {
            return x.ToString() + ";" + y.ToString();
        }

        public void SetCoords(string coords) 
        {
            int[] arr = coords.Split(';').Cast<int>().ToArray();
            x = arr[0];
            y = arr[1];
        }
    }

    public class CoursePageElement : BaseEntity
    {
        private Coord _coord;

        public int CourseId { get; set; }

        public int CoursePageId { get; set; }

        public string? Discriminator { get; set; }

        public string Coords 
        {
            get
            {
                return _coord.GetCoords();
            }
            set
            {
                _coord.SetCoords(value);
            }
        }
    }
}

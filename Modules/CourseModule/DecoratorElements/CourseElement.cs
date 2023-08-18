using SmartEdu.Modules.CourseModule.Core;

namespace SmartEdu.Modules.CourseModule.DecoratorElements
{
    /// <summary>
    /// Element's coords
    /// </summary>
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

        /// <summary>
        /// Get coords like x;y
        /// </summary>
        /// <returns></returns>
        public string GetCoords() 
        {
            return x.ToString() + ";" + y.ToString();
        }

        /// <summary>
        /// Set coords from like x;y
        /// </summary>
        /// <param name="coords"></param>
        public void SetCoords(string coords) 
        {
            int[] arr = coords.Split(';').Cast<int>().ToArray();
            x = arr[0];
            y = arr[1];
        }
    }

    /// <summary>
    /// Page's element
    /// </summary>
    public class CourseElement : BaseEntity
    {
        private Coord _coord;

        public int ExerciseId { get; set; }

        public int ExercisePageId { get; set; }

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

        /// <summary>
        /// Save element in db
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public async static Task Save(CourseElement element)
        {
            using (var context = new ApplicationContext())
            {
                await context.CoursePageElements.AddAsync(element);
                await context.SaveChangesAsync();
            }
        }
    }
}

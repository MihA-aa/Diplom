using System.Collections.Generic;
using Course.WEB.Models.Entities.Graphics.Enums;

namespace Course.WEB.Models.Entities.Graphics
{
    public class Projection
    {
        public Projection()
        {
            Points = new List<Point3D>();
        }

        public int Id { get; set; }

        public Shapes Shape { get; set; }

        public virtual ICollection<Point3D> Points { get; set; }

        public string Comparer { get; set; }
    }
}
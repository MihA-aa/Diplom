using System.Collections.Generic;

namespace Course.WEB.Models.Entities.Graphics
{
    public class GraphicTask
    {
        public GraphicTask()
        {
            Projections = new List<Projection>();
        }

        public int Id { get; set; }

        public string Condition { get; set; }

        public virtual ICollection<Projection> Projections { get; set; }
    }
}
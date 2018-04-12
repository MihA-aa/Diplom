using Course.WEB.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.WEB.Models.Entities
{ 
    public class Topic : IGetProperties
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Название не может быть пустым")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "Название должно быть больше 3 и меньше 60 символов")]
        [Display(Name = "Название темы")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Описание не может быть пустым")]
        [StringLength(1000, MinimumLength = 1, ErrorMessage = "Описание должно быть больше 1 и меньше 500 символов")]
        [Display(Name = "Описание темы")]
        public string Description { get; set; }
        public decimal PeriodicityOfDemand { get; set; }
        [Range(typeof(decimal), "0", "10", ErrorMessage = "Значение должно быть в пределе от {1} до {2}.")]
        [Display(Name = "Планируемая сложность темы")]
        public decimal AverageComplexity { get; set; }
        public int CourseId { get; set; }
        public virtual Course Course { get; set; }
        public string CreatorId { get; set; }
        public virtual ApplicationUser Creator { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
        public Topic()
        {
            Tasks = new List<Task>();
        }

        public Dictionary<string, string> GetProperties()
        {
            Dictionary<string, string> DictProperties = new Dictionary<string, string>
            {
                {"Название", this.Name},
                {"Средняя сложность", Convert.ToString(this.AverageComplexity)},
                {"Курс", this.Course.Name}
            };
            return DictProperties;
        }
    }
}

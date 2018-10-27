using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Course.WEB.Models.Interfaces;

namespace Course.WEB.Models.Entities
{
    public class Discipline : IGetProperties
    {
        public Discipline()
        {
            Courses = new List<Course>();
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "Название не может быть пустым")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "Название должно быть больше 3 и меньше 60 символов")]
        [Display(Name = "Название дисциплины")]

        public string Name { get; set; }

        [Required(ErrorMessage = "Описание не может быть пустым")]
        [StringLength(1000, MinimumLength = 1, ErrorMessage = "Описание должно быть больше 1 и меньше 500 символов")]
        [Display(Name = "Описание дисциплины")]

        public string Description { get; set; }

        public virtual ICollection<Course> Courses { get; set; }

        public string CreatorId { get; set; }

        public virtual ApplicationUser Creator { get; set; }

        public Dictionary<string, string> GetProperties()
        {
            Dictionary<string, string> dictProperties = new Dictionary<string, string>
            {
                { "Название", Name }
            };
            return dictProperties;
        }
    }
}

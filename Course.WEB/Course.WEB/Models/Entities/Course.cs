using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Course.WEB.Models.Interfaces;

namespace Course.WEB.Models.Entities
{
    public class Course : IGetProperties
    {
        public Course()
        {
            Topics = new List<Topic>();
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "Название не может быть пустым")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "Название должно быть больше 3 и меньше 60 символов")]
        [Display(Name = "Название курса")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Описание не может быть пустым")]
        [StringLength(1000, MinimumLength = 1, ErrorMessage = "Описание должно быть больше 1 и меньше 500 символов")]
        [Display(Name = "Описание курса")]
        public string Description { get; set; }

        public int? DisciplineId { get; set; }

        public virtual Discipline Discipline { get; set; }

        public virtual ICollection<Topic> Topics { get; set; }

        public string CreatorId { get; set; }

        public virtual ApplicationUser Creator { get; set; }

        public Dictionary<string, string> GetProperties()
        {
            Dictionary<string, string> dictProperties = new Dictionary<string, string>
            {
                { "Название", Name },
                { "Дисциплина", Discipline.Name }
            };

            return dictProperties;
        }
    }
}

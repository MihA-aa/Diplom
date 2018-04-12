using Course.WEB.Models;
using Course.WEB.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Course.WEB.Models.Entities
{
    public class Course: IGetProperties
    {
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

        //public virtual ICollection<ClientProfile> Students { get; set; }
        public Course()
        {
            //Students = new List<ClientProfile>();
            Topics = new List<Topic>();
        }

        public Dictionary<string, string> GetProperties()
        {
            Dictionary<string, string> DictProperties = new Dictionary<string, string>
            {
                {"Название", this.Name},
                {"Дисциплина", this.Discipline.Name}
            };
            //PropertyInfo[] properties = typeof(Course).GetProperties();
            //foreach (PropertyInfo property in properties)
            //{
            //    DictProperties.Add(property.Name, Convert.ToString(property.GetValue(this, null)));
            //}
            
            return DictProperties;
        }
    }
}

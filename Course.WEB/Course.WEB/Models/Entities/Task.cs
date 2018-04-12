﻿using Course.WEB.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.WEB.Models.Entities
{
    public class Task : IGetProperties
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Название не может быть пустым")]
        [StringLength(40, MinimumLength = 3, ErrorMessage = "Название должно быть больше 3 и меньше 40 символов")]
        [Display(Name = "Название задачи")]
        public string Name { get; set; }

        [Range(typeof(decimal), "0", "10", ErrorMessage ="Значение должно быть в пределе от {1} до {2}.")]
        [Display(Name = "Планируемая сложность задачи")]
        public decimal PlannedComplexity { get; set; }

        public decimal AverageComplexity { get; set; }

        [Range(typeof(int), "0", "3600", ErrorMessage = "Значение должно быть в пределе от {1} до {2}.")]
        [Display(Name = "Планируемое время решения задачи (секунд)")]
        public int PlannedTime { get; set; }

        public int AverageTime { get; set; }

        public decimal PeriodicityOfVisiting { get; set; }

        public decimal PeriodicityOfRequirement { get; set; }

        [Required(ErrorMessage = "Ответ не может быть пустым")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Ответ должно быть больше 1 и меньше 100 символов")]
        [Display(Name = "Ответ")]
        public string Answer { get; set; }

        [Required(ErrorMessage = "Условие не может быть пустым")]
        [StringLength(500, MinimumLength = 3, ErrorMessage = "Условие должно быть больше 3 и меньше 500 символов")]
        [Display(Name = "Условие задачи")]
        public string Condition { get; set; }

        public int TopicId { get; set; }

        public virtual Topic Topic { get; set; }

        public string CreatorId { get; set; }

        public virtual ApplicationUser Creator { get; set; }

        public Dictionary<string, string> GetProperties()
        {
            Dictionary<string, string> DictProperties = new Dictionary<string, string>
            {
                {"Id", Convert.ToString(this.Id)},
                {"Название", this.Name},
                {"Тема", this.Topic.Name},
                {"Условие", Convert.ToString(this.Condition)},
                {"Планируемая сложность", Convert.ToString(this.PlannedComplexity)},
                {"Средняя сложность", Convert.ToString(this.AverageComplexity)},
                {"Планируемое время решения (секунд)", Convert.ToString(this.PlannedTime)},
                {"Среднее время решения (секунд)", Convert.ToString(this.AverageTime)},
                {"Частота востребования в курсах", Convert.ToString(this.PeriodicityOfRequirement)},
                {"Частота посещения", Convert.ToString(this.PeriodicityOfVisiting)},
                {"Ответ", Convert.ToString(this.Answer)},
            };
            return DictProperties;
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyCompany.Domain.Entities
{
    public class NewsItem : EntityBase
    {
        [Required(ErrorMessage = "Заполните название новости")]
        [Display(Name = "Название новости")]
        public override string Title { get; set; }

        [Display(Name = "Полное описание новости")]
        public override string Text { get; set; }

    }
}

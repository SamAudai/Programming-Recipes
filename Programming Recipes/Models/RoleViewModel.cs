using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Programming_Recipes.Models
{
    public class RoleViewModel
    {
        public string Id { get; set; }

        [DisplayName("إسم المجموعة")]
        public string Name { get; set; }
    }
}
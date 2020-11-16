using Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Models.TimesheetModels
{
    public class ProductModelForCreate
    {
       

        [StringLength(255)]
        public string description { get; set; }

        [StringLength(255)]
        public string status { get; set; }

        [StringLength(255)]
        public string title { get; set; }

        public virtual Team team { get; set; }
        public virtual long team_id { get; set; }

        public IEnumerable<SelectListItem> teams { get; set; }
    }
}
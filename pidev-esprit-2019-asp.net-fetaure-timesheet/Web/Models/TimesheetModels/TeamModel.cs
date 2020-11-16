using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.Models.TimesheetModels
{
    public class TeamModel
    {
        public long id { get; set; }

        public DateTime? creationDateTime { get; set; }

        [StringLength(255)]
        public string departementEnum { get; set; }

        [StringLength(255)]
        public string teamName { get; set; }

        public long? manager_id { get; set; }
    }
}
namespace Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("pidev.ticket")]
    public partial class Ticket
    {
        public long id { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DateBegin { get; set; }

        [Column(TypeName = "date")]
        public DateTime? dateEnd { get; set; }

        [StringLength(255)]
        public string description { get; set; }

        public long? duration { get; set; }

        public long? estimatedHours { get; set; }

        public int sprint { get; set; }

        [StringLength(255)]
        public string status { get; set; }

        [StringLength(255)]
        public string title { get; set; }

        public long? employee_id { get; set; }

        public long? project_id { get; set; }

        public virtual Project project { get; set; }

        public virtual user user { get; set; }
    }
}

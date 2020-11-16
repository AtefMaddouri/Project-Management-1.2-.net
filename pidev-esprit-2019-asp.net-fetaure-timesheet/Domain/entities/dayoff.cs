namespace Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("pidev.dayoff")]
    public partial class dayoff
    {
        public long id { get; set; }

        [Column(TypeName = "date")]
        public DateTime? dayDate { get; set; }

        [StringLength(255)]
        public string typeDayOFF { get; set; }

        public long? month_id { get; set; }

        public virtual month month { get; set; }
    }
}

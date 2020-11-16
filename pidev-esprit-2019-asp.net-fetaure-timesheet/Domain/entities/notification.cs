namespace Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("pidev.notification")]
    public partial class notification
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long id { get; set; }

        [StringLength(255)]
        public string Title { get; set; }

        public DateTime? creationDateTime { get; set; }

        [StringLength(255)]
        public string description { get; set; }

        [StringLength(255)]
        public string forUserHavingRole { get; set; }

        [StringLength(255)]
        public string notifType { get; set; }
    }
}

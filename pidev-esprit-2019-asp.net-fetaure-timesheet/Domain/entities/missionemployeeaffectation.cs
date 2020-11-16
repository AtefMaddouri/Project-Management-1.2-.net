namespace Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("pidev.missionemployeeaffectation")]
    public partial class missionemployeeaffectation
    {
        [Key]
        [Column(Order = 0, TypeName = "date")]
        public DateTime beginsAt { get; set; }

        [Key]
        [Column(Order = 1, TypeName = "date")]
        public DateTime endsAt { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long idEmployee { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long idMission { get; set; }

        [StringLength(255)]
        public string note { get; set; }

        public virtual mission mission { get; set; }

        public virtual user user { get; set; }
    }
}

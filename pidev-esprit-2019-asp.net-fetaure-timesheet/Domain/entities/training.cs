namespace Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("pidev.training")]
    public partial class training
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public training()
        {
            employee_training = new HashSet<employee_training>();
            centreformations = new HashSet<centreformation>();
        }

        public int id { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        public int duree { get; set; }

        [Column(TypeName = "bit")]
        public bool? ischecked { get; set; }

        [StringLength(255)]
        public string specification { get; set; }

        [StringLength(255)]
        public string type { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<employee_training> employee_training { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<centreformation> centreformations { get; set; }
    }
}

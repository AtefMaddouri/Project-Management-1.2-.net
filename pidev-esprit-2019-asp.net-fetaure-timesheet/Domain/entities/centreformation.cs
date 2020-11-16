namespace Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("pidev.centreformation")]
    public partial class centreformation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public centreformation()
        {
            trainings = new HashSet<training>();
        }

        public int id { get; set; }

        [StringLength(255)]
        public string adresse { get; set; }

        public int capaite { get; set; }

        [Column(TypeName = "bit")]
        public bool? isDispo { get; set; }

        [StringLength(255)]
        public string libelle { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<training> trainings { get; set; }
    }
}

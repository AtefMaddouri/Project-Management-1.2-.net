namespace Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("pidev.publication")]
    public partial class publication
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public publication()
        {
            commentaires = new HashSet<commentaire>();
        }

        [Key]
        public int idPub { get; set; }

        [StringLength(255)]
        public string commentPub { get; set; }

        public int numberLikePub { get; set; }

        [StringLength(255)]
        public string subjectPub { get; set; }

        public long? employee_id { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<commentaire> commentaires { get; set; }

        public virtual user user { get; set; }
    }
}

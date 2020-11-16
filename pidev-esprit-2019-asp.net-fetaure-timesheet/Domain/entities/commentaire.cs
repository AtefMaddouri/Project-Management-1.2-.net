namespace Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("pidev.commentaire")]
    public partial class commentaire
    {
        [Key]
        public long idCom { get; set; }

        [StringLength(255)]
        public string contenu { get; set; }

        public int? publication_idPub { get; set; }

        public virtual publication publication { get; set; }
    }
}

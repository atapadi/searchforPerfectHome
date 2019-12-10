namespace SearchPerfectHome.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PropertyImage")]
    public partial class PropertyImage
    {
        public int Id { get; set; }

        public int PropertyId { get; set; }

        [StringLength(255)]
        public string Name { get; set; }

        public int? Size { get; set; }

        [StringLength(255)]
        public string ImagePath { get; set; }

        public DateTime? LastUpdated { get; set; }

        public virtual Property Property { get; set; }
    }
}

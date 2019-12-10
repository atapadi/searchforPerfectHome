namespace SearchPerfectHome.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("USERS")]
    public partial class USER
    {
        public int Id { get; set; }

        [Required]
        [StringLength(128)]
        public string UserId { get; set; }

        [StringLength(128)]
        public string UserName { get; set; }

        public string Interests { get; set; }

        [StringLength(255)]
        public string Status { get; set; }

        [StringLength(255)]
        public string UserImage { get; set; }

        [StringLength(128)]
        public string University { get; set; }

        [StringLength(128)]
        public string Degree { get; set; }

        public DateTime LastUpdated { get; set; }

        [StringLength(256)]
        public string UserEmail { get; set; }

        public bool IsFirstTimeRegister { get; set; }
    }
}

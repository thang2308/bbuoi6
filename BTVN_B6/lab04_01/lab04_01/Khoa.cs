namespace lab04_01
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Khoa")]
    public partial class Khoa
    {
        [Key]
        [StringLength(50)]
        public string MaKhoa { get; set; }

        [StringLength(100)]
        public string TenKhoa { get; set; }
    }
}

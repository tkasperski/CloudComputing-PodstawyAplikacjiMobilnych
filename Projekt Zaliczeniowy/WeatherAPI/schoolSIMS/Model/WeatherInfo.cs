using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace schoolSIMS.Model
{
    [Table("weatherinfo")]
    public partial class WeatherInfo
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("temperature")]
        [StringLength(50)]
        public string Temperature { get; set; }
        [Column("desc_temp")]
        [StringLength(50)]
        public string DescTemp { get; set; }
        [Column("humidity")]
        [StringLength(20)]
        public string Humidity { get; set; }
        [Column("wind")]
        [StringLength(50)]
        public string Wind { get; set; }
        [Column("gauge")]
        [StringLength(50)]
        public string Gauge { get; set; }
        [Column("cloudiness")]
        [StringLength(50)]
        public string Cloudiness { get; set; }
        [Column("date_created", TypeName = "date")]
        public DateTime? DateCreated { get; set; }
        [StringLength(50)]
        public string City { get; set; }
    }
}

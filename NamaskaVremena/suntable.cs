namespace NamaskaVremena
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("suntableJonkoping")]
    public partial class suntable
    {
        [Key]
        [Column(TypeName = "date")]
        public DateTime date { get; set; }

        public TimeSpan sunrise { get; set; }
        public TimeSpan farj { get; set; }
        public TimeSpan isha { get; set; }

        public TimeSpan sunset { get; set; }
    }
}

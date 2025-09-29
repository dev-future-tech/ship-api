using System.ComponentModel.DataAnnotations.Schema;

namespace Ships.Models
{
    [Table("officers")]
    public class Officer
    {
        [Column("officer_id")]
        public int OfficerId { get; set; }
        public String? OfficerName { get; set; }
        public String? Rank { get; set; }
    }
}
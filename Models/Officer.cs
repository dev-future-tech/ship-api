using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MySecureWebApi.Models;

[Table("officers")]
public class Officer
{
    [Column("officer_id")]
    public int OfficerId { get; init; }
    
    [Column("officer_name")]
    [MaxLength(50)]
    public string? OfficerName { get; set; }
    
    public int OfficerRankId { get; set; }

    public virtual Rank OfficerRank { get; set; } =  null!;
}

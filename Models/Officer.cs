using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MySecureWebApi.Models;

[Table("officers")]
public class Officer(string officerName)
{
    [Column("officer_id")]
    public int OfficerId { get; init; }
    
    [Column("officer_name")]
    [MaxLength(50)]
    public string OfficerName { get; set; } = officerName;

    [Column("officer_rank")]
    [MaxLength(35)]
    public string? Rank { get; set; }
}

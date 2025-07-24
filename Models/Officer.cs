using System.ComponentModel.DataAnnotations.Schema;

namespace MySecureWebApi.Models;

[Table("officers")]
public class Officer(string officerName)
{
    [Column("officer_id")]
    public int OfficerId { get; init; }
    [Column("officer_name")]
    public string OfficerName { get; set; } = officerName;

    [Column("officer_rank")]
    public string? Rank { get; set; }
}

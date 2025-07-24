using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MySecureWebApi.Models;

[Table("ranks")]
public class Rank(string rankName)
{
    [Column("rank_id")]
    public int RankId { get; init; }

    [Column("rank_name")]
    [MaxLength(25)]
    public string RankName { get; set; } = rankName;
}

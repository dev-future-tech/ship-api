using System.ComponentModel.DataAnnotations.Schema;

namespace MySecureWebApi.Models;

[Table("ranks")]
public class Rank
{
    [Column("rank_id")]
    public int RankId { get; set; }

    [Column("rank_name")]
    public string? RankName { get; set; }

}

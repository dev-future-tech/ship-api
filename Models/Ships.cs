using System.ComponentModel.DataAnnotations.Schema;

namespace MySecureWebApi.Models;

[Table("ships")]
public class Ship
{
    [Column("ship_id")]
    public int ShipId { get; set; }

    [Column("ship_name")]
    public string? ShipName { get; set; }

    [Column("registry")]
    public string? Registry { get; set; }
}

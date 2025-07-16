using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ships.Models
{
    [Table("ships")]
    public class Ship
    {
        [Column("ship_id")]
        public int ShipId { get; set; }

        [Column("ship_name")]
        public string ShipName { get; set; }
    }
}
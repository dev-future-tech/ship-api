using MySecureWebApi.Models;

namespace MySecureWebApi.DTOs;

public class OfficerResponseDto()
{
    public int OfficerId { set; get; }
    public required string OfficerName { set; get; }
    public Rank? OfficerRank { get; set; }
}

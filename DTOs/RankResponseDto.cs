namespace MySecureWebApi.DTOs;

public class RankResponseDto(int rankId, string rankName)
{
    public int Id { get; init; } = rankId;
    public string RankName { get; init; } = rankName;
}
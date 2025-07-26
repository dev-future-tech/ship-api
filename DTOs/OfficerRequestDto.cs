namespace MySecureWebApi.DTOs;

public class OfficerRequestDto(string officerName, string officerRank)
{
    public int Id { get; set; }
    public string OfficerName { get; set; } = officerName;
    public string OfficerRank { get; set; } = officerRank;
}

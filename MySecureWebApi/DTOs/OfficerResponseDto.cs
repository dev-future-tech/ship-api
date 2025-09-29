namespace Ships.DTOs
{
    public class OfficerResponseDto
    {
        public int OfficerId { set; get; }
        public required String OfficerName { set; get; }

        public String? OfficerRank { get; set; }

    }
}
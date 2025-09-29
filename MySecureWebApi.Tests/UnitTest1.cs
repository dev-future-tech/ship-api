namespace MySecureWebApi.Tests;

using Ships.DTOs;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        var request = new ShipRequestDto
        {
            Id = 1,
            Name = "Gargantuan",
            Registration = "NCC-5432"
        };
        
        Assert.NotNull(request);
        
    }
}

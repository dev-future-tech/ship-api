using Ships.DTOs;

namespace Ships.Services
{
    public interface IShipService
    {
        Task<IEnumerable<ShipResponseDto>> GetAllShipsAsync();
        Task<ShipResponseDto> GetShipByIdAsync(int id);
        Task AddShipAsync(ShipRequestDto productDto);
        Task UpdateShipAsync(int id, ShipRequestDto productDto);
        Task DeleteShipAsync(int id);
    }
}
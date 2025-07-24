using MySecureWebApi.DTOs;
using MySecureWebApi.Models;
using MySecureWebApi.Repositories;

namespace MySecureWebApi.Services;

public class ShipService(IShipRepository shipRepository) : IShipService
{
    public async Task AddShipAsync(ShipRequestDto shiptDto)
    {
        var ship = new Ship
        {
            ShipId = shiptDto.Id,
            ShipName = shiptDto.Name,
            Registry = shiptDto.Registration
        };

        await shipRepository.AddAsync(ship);
    }

    public async Task DeleteShipAsync(int id)
    {
        var ship = shipRepository.GetByIdAsync(id);

        if (ship == null)
            throw new KeyNotFoundException("Ship not found");

        await shipRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<ShipResponseDto>> GetAllShipsAsync()
    {
        var ships = await shipRepository.GetAllAsync();
        return ships.Select(ship => new ShipResponseDto
        {
            Id = ship.ShipId,
            Name = ship.ShipName,
            Registration = ship.Registry
        });
    }

    public async Task<ShipResponseDto> GetShipByIdAsync(int id)
    {
        var ship = await shipRepository.GetByIdAsync(id);

        if (ship == null)
            throw new KeyNotFoundException("Ship not found");

        return new ShipResponseDto
        {
            Id = ship.ShipId,
            Name = ship.ShipName,
            Registration = ship.Registry
        };
    }

    public async Task UpdateShipAsync(int id, ShipRequestDto shipDto)
    {
        var ship = await shipRepository.GetByIdAsync(id);

        if (ship == null)
            throw new KeyNotFoundException("Ship not found");

        ship.ShipName = shipDto.Name;

        await shipRepository.UpdateAsync(ship);

    }
}

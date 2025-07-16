using Ships.Models;
using Ships.DTOs;
using Ships.Repositories;

namespace Ships.Services
{
    public class ShipService : IShipService
    {
        private readonly IShipRepository _shipRepository;

        public ShipService(IShipRepository shipRepository)
        {
            _shipRepository = shipRepository;
        }

        public async Task AddShipAsync(ShipRequestDto shiptDto)
        {
            var ship = new Ship
            {
                ShipId = shiptDto.Id,
                ShipName = shiptDto.Name
            };

            await _shipRepository.AddAsync(ship);
        }

        public async Task DeleteShipAsync(int id)
        {
            var ship = _shipRepository.GetByIdAsync(id);

            if (ship == null)
                throw new KeyNotFoundException("Ship not found");

            await _shipRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<ShipResponseDto>> GetAllShipsAsync()
        {
            var ships = await _shipRepository.GetAllAsync();
            return ships.Select(p => new ShipResponseDto
            {
                Id = p.ShipId,
                Name = p.ShipName
            });
        }

        public async Task<ShipResponseDto> GetShipByIdAsync(int id)
        {
            var ship = await _shipRepository.GetByIdAsync(id);

            if (ship == null)
                throw new KeyNotFoundException("Ship not found");

            return new ShipResponseDto
            {
                Id = ship.ShipId,
                Name = ship.ShipName
            };
        }

        public async Task UpdateShipAsync(int id, ShipRequestDto shipDto)
        {
            var ship = await _shipRepository.GetByIdAsync(id);

            if (ship == null)
                throw new KeyNotFoundException("Ship not found");

            ship.ShipName = shipDto.Name;

            await _shipRepository.UpdateAsync(ship);

        }
    }
}
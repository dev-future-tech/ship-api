using Ships.Models;
using Ships.DTOs;
using Ships.Repositories;

namespace Ships.Services
{
    public class OfficerService : IOfficerService
    {
        private readonly IOfficerRepository _officerRepository;

        public OfficerService(IOfficerRepository officerRepository)
        {
            _officerRepository = officerRepository;
        }

        public async Task AddOfficerAsync(OfficerRequestDto officerRequestDto)
        {
            var officer = new Officer
            {
                OfficerId = officerRequestDto.Id,
                OfficerName = officerRequestDto.OfficerName,
                Rank = officerRequestDto.OfficerRank
            };
            await _officerRepository.AddAsync(officer);
        }

        public async Task DeleteOfficerAsync(int id)
        {
            var officer = _officerRepository.GetByIdAsync(id);

            if (officer == null)
                throw new KeyNotFoundException("Officer nit found");

            await _officerRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<OfficerResponseDto>> GetAllOfficersAsync()
        {
            var officers = await _officerRepository.GetAllAsync();

            return officers.Select(p => new OfficerResponseDto
            {
                OfficerId = p.OfficerId,
                OfficerName = p.OfficerName,
                OfficerRank = p.Rank
            });
        }

        public async Task<OfficerResponseDto> GetOfficerByIdAsync(int id)
        {
            var officer = await _officerRepository.GetByIdAsync(id);

            if (officer == null)
                throw new KeyNotFoundException("Officer not found");
            return new OfficerResponseDto
            {
                OfficerId = officer.OfficerId,
                OfficerName = officer.OfficerName,
                OfficerRank = officer.Rank
            };
        }

        public async Task UpdateOfficerAsync(int id, OfficerRequestDto officerRequestDto)
        {
            var officer = await _officerRepository.GetByIdAsync(id);

            if (officer == null)
                throw new KeyNotFoundException("Officer not found");

            officer.OfficerName = officerRequestDto.OfficerName;
            officer.Rank = officerRequestDto.OfficerRank;

            await _officerRepository.UpdateAsync(officer);
        }
    }
}
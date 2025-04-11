using DisneyBattle.BLL.Interfaces;
using DisneyBattle.DAL;
using DisneyBattle.DAL.Entities;
using DisneyBattle.DAL.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

    namespace DisneyBattle.BLL.Services;
public class CombatService : IService<Combat>
{
    private readonly IUnitOfWork _unitOfWork;

    public CombatService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Combat>> GetAllAsync()
    {
        return await _unitOfWork.Combats.GetAllAsync();
    }

    public async Task<Combat> GetByIdAsync(int id)
    {
        return await _unitOfWork.Combats.GetByIdAsync(id);
    }

    public async Task AddAsync(Combat entity)
    {
        await _unitOfWork.Combats.AddAsync(entity);
    }

    public async Task UpdateAsync(Combat entity)
    {
        await _unitOfWork.Combats.UpdateAsync(entity);
    }

    public async Task DeleteAsync(int id)
    {
        await _unitOfWork.Combats.DeleteAsync(id);
    }
}

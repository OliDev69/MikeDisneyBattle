using DisneyBattle.BLL.Interfaces;
using DisneyBattle.DAL;
using DisneyBattle.DAL.Entities;
using DisneyBattle.DAL.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

    namespace DisneyBattle.BLL.Services;
public class EquipesPersonnageService : IService<EquipesPersonnage>
{
    private readonly IUnitOfWork _unitOfWork;

    public EquipesPersonnageService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<EquipesPersonnage>> GetAllAsync()
    {
        return await _unitOfWork.EquipesPersonnages.GetAllAsync();
    }

    public async Task<EquipesPersonnage> GetByIdAsync(int id)
    {
        return await _unitOfWork.EquipesPersonnages.GetByIdAsync(id);
    }

    public async Task AddAsync(EquipesPersonnage entity)
    {
        await _unitOfWork.EquipesPersonnages.AddAsync(entity);
    }

    public async Task UpdateAsync(EquipesPersonnage entity)
    {
        await _unitOfWork.EquipesPersonnages.UpdateAsync(entity);
    }

    public async Task DeleteAsync(int id)
    {
        await _unitOfWork.EquipesPersonnages.DeleteAsync(id);
    }
}

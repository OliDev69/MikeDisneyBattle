using DisneyBattle.BLL.Interfaces;
using DisneyBattle.DAL;
using DisneyBattle.DAL.Entities;
using DisneyBattle.DAL.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

    namespace DisneyBattle.BLL.Services;
public class TypePersonnageService : IService<TypePersonnage>
{
    private readonly IUnitOfWork _unitOfWork;

    public TypePersonnageService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<TypePersonnage>> GetAllAsync()
    {
        return await _unitOfWork.TypesPersonnages.GetAllAsync();
    }

    public async Task<TypePersonnage> GetByIdAsync(int id)
    {
        return await _unitOfWork.TypesPersonnages.GetByIdAsync(id);
    }

    public async Task AddAsync(TypePersonnage entity)
    {
        await _unitOfWork.TypesPersonnages.AddAsync(entity);
    }

    public async Task UpdateAsync(TypePersonnage entity)
    {
        await _unitOfWork.TypesPersonnages.UpdateAsync(entity);
    }

    public async Task DeleteAsync(int id)
    {
        await _unitOfWork.TypesPersonnages.DeleteAsync(id);
    }
}

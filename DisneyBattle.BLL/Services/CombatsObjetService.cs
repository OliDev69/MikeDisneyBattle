using DisneyBattle.BLL.Interfaces;
using DisneyBattle.DAL;
using DisneyBattle.DAL.Entities;
using DisneyBattle.DAL.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

    namespace DisneyBattle.BLL.Services;
public class CombatsObjetService : IService<CombatsObjet>
{
    private readonly IUnitOfWork _unitOfWork;

    public CombatsObjetService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<CombatsObjet>> GetAllAsync()
    {
        return await _unitOfWork.CombatsObjets.GetAllAsync();
    }

    public async Task<CombatsObjet> GetByIdAsync(int id)
    {
        return await _unitOfWork.CombatsObjets.GetByIdAsync(id);
    }

    public async Task AddAsync(CombatsObjet entity)
    {
        await _unitOfWork.CombatsObjets.AddAsync(entity);
    }

    public async Task UpdateAsync(CombatsObjet entity)
    {
        await _unitOfWork.CombatsObjets.UpdateAsync(entity);
    }

    public async Task DeleteAsync(int id)
    {
        await _unitOfWork.CombatsObjets.DeleteAsync(id);
    }
}

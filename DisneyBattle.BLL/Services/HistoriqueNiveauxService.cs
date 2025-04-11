using DisneyBattle.BLL.Interfaces;
using DisneyBattle.DAL;
using DisneyBattle.DAL.Entities;
using DisneyBattle.DAL.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

    namespace DisneyBattle.BLL.Services;
public class HistoriqueNiveauxService : IService<HistoriqueNiveaux>
{
    private readonly IUnitOfWork _unitOfWork;

    public HistoriqueNiveauxService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<HistoriqueNiveaux>> GetAllAsync()
    {
        return await _unitOfWork.HistoriqueNiveaux.GetAllAsync();
    }

    public async Task<HistoriqueNiveaux> GetByIdAsync(int id)
    {
        return await _unitOfWork.HistoriqueNiveaux.GetByIdAsync(id);
    }

    public async Task AddAsync(HistoriqueNiveaux entity)
    {
        await _unitOfWork.HistoriqueNiveaux.AddAsync(entity);
    }

    public async Task UpdateAsync(HistoriqueNiveaux entity)
    {
        await _unitOfWork.HistoriqueNiveaux.UpdateAsync(entity);
    }

    public async Task DeleteAsync(int id)
    {
        await _unitOfWork.HistoriqueNiveaux.DeleteAsync(id);
    }
}

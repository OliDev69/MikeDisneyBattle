using DisneyBattle.BLL.Interfaces;
using DisneyBattle.DAL;
using DisneyBattle.DAL.Entities;
using DisneyBattle.DAL.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

    namespace DisneyBattle.BLL.Services;
public class EquipeService : IService<Equipe>
{
    private readonly IUnitOfWork _unitOfWork;

    public EquipeService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Equipe>> GetAllAsync()
    {
        return await _unitOfWork.Equipes.GetAllAsync();
    }

    public async Task<Equipe> GetByIdAsync(int id)
    {
        return await _unitOfWork.Equipes.GetByIdAsync(id);
    }

    public async Task AddAsync(Equipe entity)
    {
        await _unitOfWork.Equipes.AddAsync(entity);
    }

    public async Task UpdateAsync(Equipe entity)
    {
        await _unitOfWork.Equipes.UpdateAsync(entity);
    }

    public async Task DeleteAsync(int id)
    {
        await _unitOfWork.Equipes.DeleteAsync(id);
    }
}

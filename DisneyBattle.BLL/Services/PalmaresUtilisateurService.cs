using DisneyBattle.BLL.Interfaces;
using DisneyBattle.DAL;
using DisneyBattle.DAL.Entities;
using DisneyBattle.DAL.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

    namespace DisneyBattle.BLL.Services;
public class PalmaresUtilisateurService : IService<PalmaresUtilisateur>
{
    private readonly IUnitOfWork _unitOfWork;

    public PalmaresUtilisateurService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<PalmaresUtilisateur>> GetAllAsync()
    {
        return await _unitOfWork.PalmaresUtilisateurs.GetAllAsync();
    }

    public async Task<PalmaresUtilisateur> GetByIdAsync(int id)
    {
        return await _unitOfWork.PalmaresUtilisateurs.GetByIdAsync(id);
    }

    public async Task AddAsync(PalmaresUtilisateur entity)
    {
        await _unitOfWork.PalmaresUtilisateurs.AddAsync(entity);
    }

    public async Task UpdateAsync(PalmaresUtilisateur entity)
    {
        await _unitOfWork.PalmaresUtilisateurs.UpdateAsync(entity);
    }

    public async Task DeleteAsync(int id)
    {
        await _unitOfWork.PalmaresUtilisateurs.DeleteAsync(id);
    }
}

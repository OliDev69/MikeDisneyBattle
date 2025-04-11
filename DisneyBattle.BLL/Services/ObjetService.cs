using DisneyBattle.BLL.Interfaces;
using DisneyBattle.DAL;
using DisneyBattle.DAL.Entities;
using DisneyBattle.DAL.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

    namespace DisneyBattle.BLL.Services;
public class ObjetService : IService<Objet>
{
    private readonly IUnitOfWork _unitOfWork;

    public ObjetService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Objet>> GetAllAsync()
    {
        return await _unitOfWork.Objets.GetAllAsync();
    }

    public async Task<Objet> GetByIdAsync(int id)
    {
        return await _unitOfWork.Objets.GetByIdAsync(id);
    }

    public async Task AddAsync(Objet entity)
    {
        await _unitOfWork.Objets.AddAsync(entity);
    }

    public async Task UpdateAsync(Objet entity)
    {
        await _unitOfWork.Objets.UpdateAsync(entity);
    }

    public async Task DeleteAsync(int id)
    {
        await _unitOfWork.Objets.DeleteAsync(id);
    }
}

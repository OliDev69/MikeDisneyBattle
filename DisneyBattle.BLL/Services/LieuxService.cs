using DisneyBattle.BLL.Interfaces;
using DisneyBattle.DAL;
using DisneyBattle.DAL.Entities;
using DisneyBattle.DAL.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

    namespace DisneyBattle.BLL.Services;
public class LieuxService : IService<Lieux>
{
    private readonly IUnitOfWork _unitOfWork;

    public LieuxService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Lieux>> GetAllAsync()
    {
        return await _unitOfWork.Lieux.GetAllAsync();
    }

    public async Task<Lieux> GetByIdAsync(int id)
    {
        return await _unitOfWork.Lieux.GetByIdAsync(id);
    }

    public async Task AddAsync(Lieux entity)
    {
        await _unitOfWork.Lieux.AddAsync(entity);
    }

    public async Task UpdateAsync(Lieux entity)
    {
        await _unitOfWork.Lieux.UpdateAsync(entity);
    }

    public async Task DeleteAsync(int id)
    {
        await _unitOfWork.Lieux.DeleteAsync(id);
    }
}

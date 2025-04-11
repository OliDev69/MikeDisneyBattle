using DisneyBattle.BLL.Interfaces;
using DisneyBattle.DAL;
using DisneyBattle.DAL.Entities;
using DisneyBattle.DAL.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

    namespace DisneyBattle.BLL.Services;
public class PointsFaibleService : IService<PointsFaible>
{
    private readonly IUnitOfWork _unitOfWork;

    public PointsFaibleService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<PointsFaible>> GetAllAsync()
    {
        return await _unitOfWork.PointsFaibles.GetAllAsync();
    }

    public async Task<PointsFaible> GetByIdAsync(int id)
    {
        return await _unitOfWork.PointsFaibles.GetByIdAsync(id);
    }

    public async Task AddAsync(PointsFaible entity)
    {
        await _unitOfWork.PointsFaibles.AddAsync(entity);
    }

    public async Task UpdateAsync(PointsFaible entity)
    {
        await _unitOfWork.PointsFaibles.UpdateAsync(entity);
    }

    public async Task DeleteAsync(int id)
    {
        await _unitOfWork.PointsFaibles.DeleteAsync(id);
    }
}

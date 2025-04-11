using DisneyBattle.BLL.Interfaces;
using DisneyBattle.BLL.Models;
using DisneyBattle.DAL;
using DisneyBattle.DAL.Entities;
using DisneyBattle.DAL.Interfaces;
using Mapster;
using System.Collections.Generic;
using System.Threading.Tasks;

    namespace DisneyBattle.BLL.Services;
public class PersonnageService : IService<PersonnageModel>
{
    private readonly IUnitOfWork _unitOfWork;

    public PersonnageService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<PersonnageModel>> GetAllAsync()
    {
        return (await _unitOfWork.Personnages.GetAllAsync()).Select(m=>m.Adapt<PersonnageModel>());
    }

    public async Task<PersonnageModel> GetByIdAsync(int id)
    {
        return (await _unitOfWork.Personnages.GetByIdAsync(id)).Adapt<PersonnageModel>();
    }

    public async Task AddAsync(PersonnageModel entity)
    {
        await _unitOfWork.Personnages.AddAsync(entity.Adapt<Personnage>());
    }

    public async Task UpdateAsync(PersonnageModel entity)
    {
        await _unitOfWork.Personnages.UpdateAsync(entity.Adapt<Personnage>());
    }

    public async Task DeleteAsync(int id)
    {
        await _unitOfWork.Personnages.DeleteAsync(id);
    }
}

using DisneyBattle.BLL.Interfaces;
using DisneyBattle.BLL.Models;
using DisneyBattle.DAL;
using DisneyBattle.DAL.Entities;
using DisneyBattle.DAL.Interfaces;
using MapsterMapper;
using System.Collections.Generic;
using System.Threading.Tasks;

    namespace DisneyBattle.BLL.Services;
public class UtilisateurService : IUserService 
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public UtilisateurService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<Models.UtilisateurModel>> GetAllAsync()
    {
        return (await _unitOfWork.Utilisateurs.GetAllAsync()).Select(m=> _mapper.Map<UtilisateurModel>(m));
    }

    public async Task<Models.UtilisateurModel> GetByIdAsync(int id)
    {
        return _mapper.Map <Models.UtilisateurModel > (await _unitOfWork.Utilisateurs.GetByIdAsync(id));
    }

    public async Task AddAsync(Models.UtilisateurModel entity)
    {
        DAL.Entities.UtilisateurModel u = _mapper.Map<DAL.Entities.UtilisateurModel>(entity);
        await _unitOfWork.Utilisateurs.AddAsync(u);
    }

    public async Task UpdateAsync(Models.UtilisateurModel entity)
    {
        DAL.Entities.UtilisateurModel u = _mapper.Map<DAL.Entities.UtilisateurModel>(entity);
        await _unitOfWork.Utilisateurs.UpdateAsync(u);
    }

    public async Task DeleteAsync(int id)
    {
        await _unitOfWork.Utilisateurs.DeleteAsync(id);
    }

    public Models.UtilisateurModel? Authenticate(string username, string password)
    {
        DAL.Entities.UtilisateurModel? u = _unitOfWork.Utilisateurs.Authenticate(username, password);
        return u == null ? default(Models.UtilisateurModel) : _mapper.Map<Models.UtilisateurModel>(u);
    }

    public bool Checkrefresh(string access_Token, string refresh_Token)
    {
        return (_unitOfWork.Utilisateurs.Checkrefresh(access_Token,refresh_Token));
    }

    public Models.UtilisateurModel? GetByEmail(string email)
    {
        DAL.Entities.UtilisateurModel? u = _unitOfWork.Utilisateurs.GetByEmail(email);
        return u == null ? default(Models.UtilisateurModel) : _mapper.Map<Models.UtilisateurModel>(u);
    }

}

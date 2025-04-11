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

    public async Task<IEnumerable<UtilisateurModel>> GetAllAsync()
    {
        return (await _unitOfWork.Utilisateurs.GetAllAsync()).Select(m=> _mapper.Map<UtilisateurModel>(m));
    }

    public async Task<UtilisateurModel> GetByIdAsync(int id)
    {
        return _mapper.Map < UtilisateurModel > (await _unitOfWork.Utilisateurs.GetByIdAsync(id));
    }

    public async Task AddAsync(UtilisateurModel entity)
    {
        Utilisateur u = _mapper.Map<Utilisateur>(entity);
        await _unitOfWork.Utilisateurs.AddAsync(u);
    }

    public async Task UpdateAsync(UtilisateurModel entity)
    {
        Utilisateur u = _mapper.Map<Utilisateur>(entity);
        await _unitOfWork.Utilisateurs.UpdateAsync(u);
    }

    public async Task DeleteAsync(int id)
    {
        await _unitOfWork.Utilisateurs.DeleteAsync(id);
    }

    public UtilisateurModel? Authenticate(string username, string password)
    {
        Utilisateur? u = _unitOfWork.Utilisateurs.Authenticate(username, password);
        return u == null ? default(UtilisateurModel) : _mapper.Map<UtilisateurModel>(u);
    }

    public bool Checkrefresh(string access_Token, string refresh_Token)
    {
        return (_unitOfWork.Utilisateurs.Checkrefresh(access_Token,refresh_Token));
    }

    public UtilisateurModel? GetByEmail(string email)
    {
        Utilisateur? u = _unitOfWork.Utilisateurs.GetByEmail(email);
        return u == null ? default(UtilisateurModel) : _mapper.Map<UtilisateurModel>(u);
    }

}

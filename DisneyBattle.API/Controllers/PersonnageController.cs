using DisneyBattle.BLL.Interfaces;
using DisneyBattle.BLL.Models;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DisneyBattle.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonnageController : ControllerBase
    {
        private readonly IService<PersonnageModel> _personnageService;
        private readonly IMapper _mapper;

        public PersonnageController(IService<PersonnageModel> personnageService, IMapper mapper)
        {
            _personnageService = personnageService;
            _mapper = mapper;
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
           return Ok(await _personnageService.GetAllAsync());
        }
    }
}

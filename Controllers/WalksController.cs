using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.CustomActionFilters;
using NZWalksAPI.Models.Domain;
using NZWalksAPI.Models.DTO;
using NZWalksAPI.Repositories;

namespace NZWalksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalkRepository walkRepository;
        private readonly ILogger<WalksController> logger;

        public WalksController(IMapper mapper, IWalkRepository walkRepository, ILogger<WalksController> logger)
        {
            this.mapper = mapper;
            this.walkRepository = walkRepository;
            this.logger = logger;
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDTO addWalkRequestDTO)
        {
            
            var walk = mapper.Map<Walk>(addWalkRequestDTO);
            walk = await walkRepository.CreateAsync(walk);

            var walkDTO = mapper.Map<WalkDTO>(walk);
            return CreatedAtAction(nameof(GetById), new { id = walk.Id }, walkDTO);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery, [FromQuery] string? sortBy, [FromQuery] bool?isAscending,
                                                [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 1000)
        {
                var Walks = await walkRepository.GetAllAsync(filterOn, filterQuery, sortBy, isAscending ?? true, pageNumber, pageSize);
                var walkDTO = mapper.Map<List<WalkDTO>>(Walks);
                return Ok(walkDTO);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var walk = await walkRepository.GetByIdAsync(id);
            if (walk == null)
            {
                return NotFound();
            }
            var walkDTO = mapper.Map<WalkDTO>(walk);
            return Ok(walkDTO);
        }

        [HttpPut("{id}")]
        [ValidateModel]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateWalkRequestDTO updateWalkRequestDTO)
        {
            
            var walk = mapper.Map<Walk>(updateWalkRequestDTO);
            walk = await walkRepository.UpdateAsync(id, walk);
            var walkDTO = mapper.Map<WalkDTO>(walk);
            return CreatedAtAction(nameof(GetById), new { id = walk.Id }, walkDTO);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var walk = await walkRepository.DeleteAsync(id);
            var walkDTO = mapper.Map<WalkDTO>(walk);
            return Ok(walkDTO);
        }
        
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalksAPI.CustomActionFilters;
using NZWalksAPI.Data;
using NZWalksAPI.Models.Domain;
using NZWalksAPI.Models.DTO;
using NZWalksAPI.Repositories;
using System.Text.Json;

namespace NZWalksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;
        private readonly ILogger<RegionsController> logger;

        public RegionsController(IRegionRepository regionRepository, IMapper mapper, ILogger<RegionsController> logger)
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;
            this.logger = logger;
        }

        [HttpGet]
        //[Authorize(Roles = "Reader,Writer")]
        public async Task<IActionResult> GetAll()
        {

            var regions = await regionRepository.GetAllAsync();
            logger.LogInformation($"Finished GetAllRegions request with data: {JsonSerializer.Serialize(regions)}");
            //var regionDTO = new List<RegionDTO>();
            //foreach (var region in regions)
            //{
            //    regionDTO.Add(new RegionDTO()
            //    {
            //        Id = region.Id,
            //        Code = region.Code,
            //        Name = region.Name,
            //        RegionImageUrl = region.RegionImageUrl,
            //    });
            //}
            var regionDTO = mapper.Map<List<RegionDTO>>(regions);
            return Ok(regionDTO);
        }

        [HttpGet]
        [Route("{id}")]
        //[Authorize(Roles = "Reader,Writer")]
        public async Task<IActionResult> GetById([FromRoute] Guid id) {
            var region = await regionRepository.GetByIdAsync(id);
            if(region == null)
            {
                return NotFound();
            }
            //var regionDTO = new RegionDTO()
            //{
            //    Id = region.Id,
            //    Code = region.Code,
            //    Name = region.Name,
            //    RegionImageUrl = region.RegionImageUrl
            //};
            var regionDTO = mapper.Map<RegionDTO>(region);
            return Ok(regionDTO);
        }

        [HttpPost]
        [ValidateModel]
        //[Authorize(Roles = "Writer")]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDTO addRegionRequestDTO)
        {
            //var region = new Region
            //{
            //    Code = addRegionRequestDTO.Code,
            //    Name = addRegionRequestDTO.Name,
            //    RegionImageUrl = addRegionRequestDTO.RegionImageUrl
            //};
            var region = mapper.Map<Region>(addRegionRequestDTO);
            region = await regionRepository.CreateAsync(region);

            //var regionDTO = new RegionDTO()
            //{
            //    Id = region.Id,
            //    Code = region.Code,
            //    Name = region.Name,
            //    RegionImageUrl = region.RegionImageUrl
            //};
            var regionDTO = mapper.Map<RegionDTO>(region);
            return CreatedAtAction(nameof(GetById), new { id = region.Id }, regionDTO);

        }

        [HttpPut("{id}")]
        [ValidateModel]
        //[Authorize(Roles = "Writer")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateRegionRequestDTO updateRegionRequestDTO)
        {

            //var region = new Region
            //{
            //    Code = updateRegionRequestDTO.Code,
            //    Name = updateRegionRequestDTO.Name,
            //    RegionImageUrl = updateRegionRequestDTO.RegionImageUrl
            //};
            var region = mapper.Map<Region>(updateRegionRequestDTO);
            region = await regionRepository.UpdateAsync(id, region);
            if(region == null)
            {
                return NotFound();
            }
            
            //var regionDTO = new RegionDTO()
            //{
            //    Id = region.Id,
            //    Code = region.Code,
            //    Name = region.Name,
            //    RegionImageUrl = region.RegionImageUrl
            //};
            var regionDTO = mapper.Map<RegionDTO>(region);
            return CreatedAtAction(nameof(GetById), new { id = region.Id }, regionDTO);
        }

        [HttpDelete("{id}")]
        //[Authorize(Roles = "Writer")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var region = await regionRepository.DeleteAsync(id);
            var regionDTO = mapper.Map<RegionDTO>(region);
            return Ok(regionDTO);
        }
    }
}

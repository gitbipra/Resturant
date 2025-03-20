using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Resturant_api.Data;
using Resturant_api.Model.Domain;
using Resturant_api.Model.DTO;
using Resturant_api.Repository;

namespace Resturant_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IMenuRepository _menuRepository;
        private readonly IMapper _mapper;

        public MenuController(IMenuRepository menuRepository, IMapper mapper)
        {
            this._menuRepository = menuRepository;
            this._mapper = mapper;
        }

        //Get All Items
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var FoodMenu = await _menuRepository.GetAllAsync();

                var MenusDto = _mapper.Map<List<MenuDto>>(FoodMenu);

                return Ok(MenusDto);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Get By Items Id
        [HttpGet]
        [Route("GetByMenuId/{MenuId:Guid}")]
        public async Task<IActionResult> GetByMenuId([FromRoute] Guid MenuId) 
        {
            try
            {
                var MenuDomain = await _menuRepository.GetByMenuIdAsnyc(MenuId);
                if (MenuDomain == null)
                {
                    return NotFound();
                }
                var menuDtos = _mapper.Map<MenuDto>(MenuDomain);
                return Ok(menuDtos);
            }
            catch (Exception)
            {
                throw;
            }   
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([FromBody] AddMenuRequestDto menuRequestDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //map dto to domain model
                    var menuDomaiModel = _mapper.Map<Menu>(menuRequestDto);
                    menuDomaiModel = await _menuRepository.CreateAsnyc(menuDomaiModel);

                    //Map domain model to dto
                    var menuDto = _mapper.Map<MenuDto>(menuDomaiModel);

                    return CreatedAtAction(nameof(GetByMenuId), new { MenuId = menuDto.MenuId }, menuDto);

                }
                else { 
                    return BadRequest();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPut]
        [Route("UpdateMenuItem/{MenuId:Guid}")]

        public async Task<IActionResult> UpdateMenuItem([FromRoute] Guid MenuId, [FromBody] UpdateMenuRequestDto updateMenuRequestDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var MenuDomainModel = _mapper.Map<Menu>(updateMenuRequestDto);
                    MenuDomainModel = await _menuRepository.UpdateAsnyc(MenuId, MenuDomainModel);

                    if (MenuDomainModel == null)
                    {
                        return NotFound();
                    }

                    //Convert Domain To Dto
                    var menuDtos = _mapper.Map<MenuDto>(MenuDomainModel);

                    return Ok(menuDtos);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception)
            {
               throw;
            }
        }

        [HttpDelete]
        [Route("DeleteMenuItem/{MenuId:Guid}")]
        public async Task<IActionResult> DeleteMenuItem([FromRoute] Guid MenuId)
        {
            var menuDomainModel = await _menuRepository.DeleteByMenuIdAsync(MenuId);

            if (menuDomainModel == null)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}

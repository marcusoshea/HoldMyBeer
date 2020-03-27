using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using System.IdentityModel.Tokens.Jwt;
using WebApi.Helpers;
using Microsoft.Extensions.Options;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using WebApi.Services;
using WebApi.Entities;
using WebApi.Models.Beers;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("pokeapi/[controller]")]
    public class BeersController : ControllerBase
    {
        private IBeerService _beerService;
        private IMapper _mapper;

        public BeersController(
            IBeerService beerService,
            IMapper mapper)
        {
            _beerService = beerService;
            _mapper = mapper;
        }

        [HttpGet("{id}/{beerId}")]
        public IActionResult GetById(int id, int beerId)
        {
            if (User.Identity.Name != id.ToString())
            {
                return BadRequest(new { message = "Attempted to access a different user" });
            }
            var beer = _beerService.GetById(int.Parse(User.Identity.Name), beerId);
            var model = _mapper.Map<BeerModel>(beer);
            return Ok(model);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var beers = _beerService.GetAll(int.Parse(User.Identity.Name));
            var model = _mapper.Map<IList<BeerModel>>(beers);
            return Ok(model);
        }

        [HttpPost("add/{id}")]
        public IActionResult Add(int id, [FromBody]AddBeerModel model)
        {
            // map model to entity
            var beer = _mapper.Map<Beer>(model);

            if (User.Identity.Name == null)
            {
                return BadRequest(new { message = "User token error" });
            }
            if (User.Identity.Name != id.ToString())
            {
                return BadRequest(new { message = "Attempted to add beer to a different user" });
            }
            
            beer.UserId = int.Parse(User.Identity.Name);

            try
            {
                // create Beer
                _beerService.Add(beer);
                return Ok();
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("update/{id}")]
        public IActionResult Update(int id, [FromBody]UpdateBeerModel model)
        {
            var beer = _mapper.Map<Beer>(model);

            if (User.Identity.Name == null)
            {
                return BadRequest(new { message = "User token error" });
            }
            if (User.Identity.Name != id.ToString())
            {
                return BadRequest(new { message = "Attempted to update beer of wrong user" });
            }
            beer.UserId = int.Parse(User.Identity.Name);

            try
            {
                // update beer 
                _beerService.Update(beer);
                return Ok();
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        // DELETE api/4/5
        [HttpDelete("{id}/{beerId}")]
        public void Delete(int id, int beerId)
        {
            if (User.Identity.Name == id.ToString())
            {
                _beerService.Delete(beerId, id);
            }
        }
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.DTOs;
using Talabat.APIs.Errors;
using Talabat.Core.Entities;
using Talabat.Repository;

namespace Talabat.APIs.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BasketController : ControllerBase
	{
		private readonly IBasketRepository _basketRepository;
		private readonly IMapper _mapper;

		public BasketController(IBasketRepository basketRepository , IMapper mapper)
        {
			_basketRepository = basketRepository;
			_mapper = mapper;
		}

		[HttpGet("{id}")]//api/Basket/id

		public async Task<ActionResult<CustomerBasket>> GetBasket(string id)
		{
			var basket = await _basketRepository.GetBasketAsync(id);
			// if null means that basket id is expired and return empty object
			return Ok(basket ?? new CustomerBasket(id)); 
		}

		[HttpPost] //api/Basket
		public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasketDto basket)
		{
			var mappedCustomerBasket =  _mapper.Map<CustomerBasketDto,CustomerBasket>(basket);
			var createdOrUpdatedBasket = await _basketRepository.UpdateBasketAsync(mappedCustomerBasket);
			if (createdOrUpdatedBasket == null) return BadRequest(new ApiResponse(400));
			return Ok(createdOrUpdatedBasket);
		}


		[HttpDelete] //api/Basket?id

		public async Task DeleteBasket(string id)
		{
			await _basketRepository.DeleteBasketAsync(id);
		}
    }
}

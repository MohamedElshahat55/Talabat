﻿using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Repository
{
	public class BasketRepository : IBasketRepository
	{
		private readonly IDatabase _database;

		public BasketRepository(IConnectionMultiplexer redis )
        {
			_database = redis.GetDatabase();
		}

		public async Task<CustomerBasket?> GetBasketAsync(string basketId)
		{
			var basket = await _database.StringGetAsync(basketId);
			return basket.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(basket);
		}

		public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket)
		{
			var createdOrUpdate = await _database.StringSetAsync(basket.Id,JsonSerializer.Serialize(basket));
			if(createdOrUpdate is false) return null;
			return await GetBasketAsync(basket.Id);
		}
        public async Task<bool> DeleteBasketAsync(string basketId)
		{
			return await _database.KeyDeleteAsync(basketId);
		}

	}
}

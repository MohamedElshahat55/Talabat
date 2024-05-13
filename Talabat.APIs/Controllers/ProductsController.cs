using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.DTOs;
using Talabat.APIs.Errors;
using Talabat.APIs.Helpers;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Specifications;

namespace Talabat.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IGenericRepository<ProductCategory> _categoryRepo;
        private readonly IGenericRepository<ProductBrand> _brandRepo;
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Product> productRepo,
            IGenericRepository<ProductCategory> categoryRepo,
            IGenericRepository<ProductBrand> brandRepo, IMapper mapper)
        {
            _productRepo = productRepo;
            _categoryRepo = categoryRepo;
            _brandRepo = brandRepo;
            _mapper = mapper;
        }
        // /api/Products
        
        [HttpGet]
		public async Task<ActionResult<IReadOnlyList<Pagination<ProductReturnDto>>>> GetAllProducts([FromQuery] ProductSpecParams Params)
        {
            var spec = new ProductWithBrandAndCategorySpecifications(Params);
            var products = await _productRepo.GetAllWithSpecAsync(spec);
            var MappedProduct = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductReturnDto>>(products);
            var CountSpec = new ProductWithFilterationForCountAsync(Params);
            var Count = await _productRepo.GetCountWithSpecAsync(CountSpec);
            return Ok(new Pagination<ProductReturnDto>(Params.PageIndex,Params.pageSize,MappedProduct, Count));
        }
        // /api/Products/id
        [HttpGet("id")]
        [ProducesResponseType(typeof(ProductReturnDto), 200)]
        [ProducesResponseType(typeof(ApiResponse), 404)]
        public async Task<ActionResult<ProductReturnDto>> GetById(int id)
        {
            var spec = new ProductWithBrandAndCategorySpecifications(id);
            var product = await _productRepo.GetByIdWithSpecAsync(spec);
            var Mappedproduct = _mapper.Map<Product, ProductReturnDto>(product);
            if (Mappedproduct == null)
            {
                return NotFound(new ApiResponse(404));
            }
            return Ok(Mappedproduct);
        }

        // /api/Products/Categories
        [HttpGet("Categories")]
        public async Task<ActionResult<IReadOnlyList<ProductCategory>>> GetAllCategory()
        {
            var categories = await _categoryRepo.GetAllAsync();
            return Ok(categories);
        }

        // /api/Products/Brands
        [HttpGet("Brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetAllBrands()
        {
            var brands = await _brandRepo.GetAllAsync();
            return Ok(brands);
        }

    }
}

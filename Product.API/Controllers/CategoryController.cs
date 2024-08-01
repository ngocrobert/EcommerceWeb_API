using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Product.Core;
using Product.Infrastructure;

namespace Product.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public CategoryController(IUnitOfWork Uow, IMapper mapper)
        {
            _uow = Uow;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var allcategory = await _uow.CategoryRepository.GetAllAsync();
            if(allcategory != null)
            {
                //var res = allcategory.Select(x => new CategoryDto
                //{
                //    Id = x.Id,
                //    Name = x.Name,
                //    Description = x.Description
                //});
                //return Ok(allcategory);
                var res = _mapper.Map<IReadOnlyList<Category>, IReadOnlyList<ListCategoryDto>>(allcategory);
                //var res = _mapper.Map<Category, ListCategoryDto>((Category)allcategory);
                return Ok(res);
            }
                return BadRequest("Not Found");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _uow.CategoryRepository.GetAsync(id);
            if (category == null)
                //return Ok(category);
                return BadRequest($"Not found this id = [{id}]");
            return Ok(_mapper.Map<Category, ListCategoryDto>(category));

        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(CategoryDto categoryDto)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    //var newCategory = new Category
                    //{
                    //    Name = categoryDto.Name,
                    //    Description = categoryDto.Description
                    //};
                    var newCategory = _mapper.Map<Category>(categoryDto);
                    await _uow.CategoryRepository.AddAsync(newCategory);
                    return Ok(newCategory);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
            //await _uow.CategoryRepository.AddAsync(category);
            //return Ok(category);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, CategoryDto categoryDto)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    var exist_category = await _uow.CategoryRepository.GetAsync(id);
                    if (exist_category != null)
                    {
                        //exist_category.Name = categoryDto.Name;
                        //exist_category.Description = categoryDto.Description;
                        _mapper.Map(categoryDto, exist_category);
                    }
                    await _uow.CategoryRepository.UpdateAsync(id,exist_category);
                    return Ok(exist_category);
                }
                return BadRequest($"Category id [{id}] Not Found");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                var exist_category = await _uow.CategoryRepository.GetAsync(id);
                if(exist_category != null)
                {
                    await _uow.CategoryRepository.DeleteAsync(id);
                    return Ok($"This category [{exist_category.Name}] Is deleted");
                }
                return BadRequest($"Category id [{id}] Not Found");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PCLabs.EntityDesign.Api.DataAccess;
using PCLabs.EntityDesign.Api.Orders.Models;

namespace PCLabs.EntityDesign.Api.Orders.Endpoints
{
    [ApiController]
    public class GetByID : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetByID(AppDbContext dbContext,
                       IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDto>> Get(int id, CancellationToken cancellationToken)
        {
            if (id <= 0) return BadRequest();

            var order = await _dbContext.Orders
                .Include(x=>x.Items)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            if (order is null) return NotFound();

            var response = _mapper.Map<OrderDto>(order);

            return Ok(response);
        }
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PCLabs.EntityDesign.Api.DataAccess;
using PCLabs.EntityDesign.Api.Orders.Models;
using PCLabs.EntityDesign.Domain.Contracts;
using PCLabs.EntityDesign.Domain.Orders;

namespace PCLabs.EntityDesign.Api.Orders.Endpoints
{
    [ApiController]
    public class Create : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IDocumentNoGenerator _documentNoGenerator;
        private readonly IDateTime _dateTimeProvider;

        public Create(AppDbContext dbContext,
                      IMapper mapper,
                      IDocumentNoGenerator documentNoGenerator, IDateTime dateTimeProvider)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _documentNoGenerator = documentNoGenerator;
            _dateTimeProvider = dateTimeProvider;
        }

        [HttpPost("/orders")]
        public async Task<ActionResult<OrderDto>> Post(OrderCreateDto createDto, CancellationToken cancellationToken)
        {
            if (createDto is null) return BadRequest();

            var orderNo = await _documentNoGenerator.GetNewOrderNo();
            var customer = new Customer(createDto.FirstName, createDto.LastName, createDto.Email);
            var address = new Address(createDto.Street, createDto.City, createDto.PostalCode, createDto.Country);
            var order = new Order(_dateTimeProvider, orderNo, customer, address);

            _dbContext.Orders.Add(order);
            await _dbContext.SaveChangesAsync();

            var response = _mapper.Map<OrderDto>(order);

            return Ok(response);
        }
    }
}

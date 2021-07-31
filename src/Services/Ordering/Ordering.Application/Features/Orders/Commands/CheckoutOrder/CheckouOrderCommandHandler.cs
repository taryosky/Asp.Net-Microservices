using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using MediatR;

using Microsoft.Extensions.Logging;

using Ordering.Application.Contracts.Infrastructure;
using Ordering.Application.Contracts.Persistence;
using Ordering.Application.Models;
using Ordering.Domain.Entities;

namespace Ordering.Application.Features.Orders.Commands.CheckoutOrder
{
    class CheckouOrderCommandHandler : IRequestHandler<CheckoutOrderCommand, int>
    {
        private readonly IOrderRepository _orderRepo;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly ILogger<CheckouOrderCommandHandler> _logger;

        public CheckouOrderCommandHandler(IOrderRepository orderRepo, IMapper mapper, IEmailService emailService, ILogger<CheckouOrderCommandHandler> logger)
        {
            _orderRepo = orderRepo;
            _mapper = mapper;
            _emailService = emailService;
            _logger = logger;
        }

        public async Task<int> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
        {
            var orderEntity = _mapper.Map<Order>(request);
            var newOrder = await _orderRepo.AddAsync(orderEntity);

            _logger.LogInformation($"Order {newOrder.Id} is successfully created");
            await SendEmail(newOrder);
            return newOrder.Id;
        }

        private async Task SendEmail(Order order)
        {
            var email = new Email()
            {
                To = "taryosky@gmail.com",
                Body = $"Order was c reated",
                Subject = "Order Placed"
            };

            try
            {
                await _emailService.SendEmail(email);
            }catch(Exception ex)
            {
                _logger.LogError($"Order {order.Id} faild dure to an error with the mail service {ex.Message}");
            }
        }
    }
}

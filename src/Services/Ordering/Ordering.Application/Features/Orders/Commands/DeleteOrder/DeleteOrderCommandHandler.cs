using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.Extensions.Logging;

using Ordering.Application.Contracts.Persistence;
using Ordering.Application.Exceptions;
using Ordering.Domain.Entities;

namespace Ordering.Application.Features.Orders.Commands.DeleteOrder
{
    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand>
    {
        private readonly IOrderRepository _orderRepo;
        private readonly ILogger<DeleteOrderCommandHandler> _logger;

        public DeleteOrderCommandHandler(IOrderRepository orderRepo, ILogger<DeleteOrderCommandHandler> logger)
        {
            _orderRepo = orderRepo;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var orderToDelete = await _orderRepo.GetByIdAsync(request.Id);
            if(orderToDelete == null)
            {
                _logger.LogError("Order with order id {0} does not exist", request.Id);
                throw new NotFoundException(nameof(Order), request.Id);
            }
            await _orderRepo.DeleteAsync(orderToDelete);
            _logger.LogInformation("order with id {0} successfully deleted", orderToDelete.Id);
            return Unit.Value;
        }
    }
}

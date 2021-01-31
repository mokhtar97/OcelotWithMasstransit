using ApiGateway.Messages;
using MassTransit;
using Microsoft.Extensions.Logging;
using Service2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
 
namespace Service2
{
    public class EventConsumer :
        IConsumer<EmployeeVacation>
    {
        ILogger<EmployeeVacation> _logger;
        private readonly ApplicationDbContext applicationDbContext;

        public EventConsumer(ILogger<EmployeeVacation> logger,ApplicationDbContext applicationDbContext)
        {
            _logger = logger;
            this.applicationDbContext = applicationDbContext;
        }

        public async Task Consume(ConsumeContext<EmployeeVacation> context)
        {
            applicationDbContext.vacations.Add(new Vacation
            {
                EmployeeName = context.Message.EmployeeName,
                Date=context.Message.Date
            });
            applicationDbContext.SaveChanges();

            _logger.LogInformation("Value: {Value}", context.Message.EmployeeName);
        }
    }
}

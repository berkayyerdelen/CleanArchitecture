using System;
using System.Threading;
using System.Threading.Tasks;
using Core.Domains.Category.Queries.GetCategoryList;
using Hangfire;
using MediatR;

namespace Core.Comman.HangFire
{
    public class RecurringJob
    {
        private readonly IMediator _mediator;
        private readonly CancellationToken _cancellationToken;
        public RecurringJob(IMediator mediator)
        {
            _mediator = mediator;
            Hangfire.RecurringJob.AddOrUpdate(() => ProcessRecurringJob(), Cron.Hourly);
        }
        public async Task ProcessRecurringJob()
        {
            await _mediator.Send(new GetCategoryListQuery(), _cancellationToken);
        }
    }
}
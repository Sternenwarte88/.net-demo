using System;

using Domain;

using MediatR;

using Persistence;

namespace Application.Activities.Commands;

public class DeleteActivity
{
    public class Command : IRequest
    {
        public required string Id { get; set; }
    }

    public class Handler(AppDbContext context) : IRequestHandler<Command>
    {
        public async Task Handle(Command request, CancellationToken cancellationToken)
        {
            Activity? activitiy = await context.Activities.FindAsync([request.Id], cancellationToken);

            if (activitiy == null)
            {
                throw new Exception("Cannot find Activity");
            }



            context.Remove(activitiy.Id);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}

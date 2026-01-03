using System;

using AutoMapper;

using Domain;

using MediatR;

using Persistence;

namespace Application.Activities.Commands;

public class EditActivity
{
    public class Command : IRequest
    {
        public required Activity Activity { get; set; }
    }

    public class Handler(AppDbContext context, IMapper mapper) : IRequestHandler<Command>
    {
        public async Task Handle(Command request, CancellationToken cancellationToken)
        {
            Activity? activitiy = await context.Activities.FindAsync([request.Activity.Id], cancellationToken);

            if (activitiy == null)
            {
                throw new Exception("Cannot find Activity");
            }

            mapper.Map(request.Activity, activitiy);

            await context.SaveChangesAsync(cancellationToken);
        }
    }
}

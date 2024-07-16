using MediatR;
using Microsoft.EntityFrameworkCore;
using RL.Backend.Exceptions;
using RL.Backend.Models;
using RL.Data;
using RL.Data.DataModels;

namespace RL.Backend.Commands.Handlers.Plans;

public class AssignUsersToPlanProcedureCommandHandler : IRequestHandler<AssignUsersToPlanProcedureCommand, ApiResponse<Unit>>
{
    private readonly RLContext _context;

    public AssignUsersToPlanProcedureCommandHandler(RLContext context)
    {
        _context = context;
    }

    public async Task<ApiResponse<Unit>> Handle(AssignUsersToPlanProcedureCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.PlanId < 1)
                return ApiResponse<Unit>.Fail(new BadRequestException("Invalid PlanId"));
            if (request.ProcedureId < 1)
                return ApiResponse<Unit>.Fail(new BadRequestException("Invalid ProcedureId"));

            var plan = await _context.Plans
                .Include(p => p.PlanProcedures)
                .FirstOrDefaultAsync(p => p.PlanId == request.PlanId);
            var procedure = await _context.Procedures.FirstOrDefaultAsync(p => p.ProcedureId == request.ProcedureId);
            
            if (plan is null)
                return ApiResponse<Unit>.Fail(new NotFoundException($"PlanId: {request.PlanId} not found"));
            if (procedure is null)
                return ApiResponse<Unit>.Fail(new NotFoundException($"ProcedureId: {request.ProcedureId} not found"));
            var usrCnt = request.UserIds.Count();
            for (int i = 0; i < usrCnt; i++) {
                var user = await _context.Users.FirstOrDefaultAsync(p => p.UserId == request.UserIds[i]);
                if (user is null)
                    return ApiResponse<Unit>.Fail(new NotFoundException($"UserId: {request.UserIds[i]} not found"));
            }
            //Already has the procedure, so just succeed
            if (plan.PlanProcedures.Any(p => p.ProcedureId == procedure.ProcedureId))
                return ApiResponse<Unit>.Succeed(new Unit());

            plan.PlanProcedures.Add(new PlanProcedure
            {
                PlanProcedureUsers = (ICollection<User>)request.UserIds
            });
            
            await _context.SaveChangesAsync();
            // Validate request
            // Your code here
            return ApiResponse<Unit>.Succeed(new Unit());
        }
        catch (Exception e)
        {
            return ApiResponse<Unit>.Fail(e);
        }
    }
}
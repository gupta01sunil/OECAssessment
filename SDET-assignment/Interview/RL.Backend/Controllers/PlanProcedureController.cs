using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using RL.Backend.Commands.Handlers.Plans;
using RL.Backend.Commands;
using RL.Backend.Models;
using RL.Data;
using RL.Data.DataModels;

namespace RL.Backend.Controllers;

[ApiController]
[Route("[controller]")]
public class PlanProcedureController : ControllerBase
{
    private readonly ILogger<PlanProcedureController> _logger;
    private readonly RLContext _context;
    private readonly IMediator _mediator;
    public PlanProcedureController(ILogger<PlanProcedureController> logger, RLContext context, IMediator mediator)
    {
        _logger = logger;
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpGet]
    [EnableQuery]
    public IEnumerable<PlanProcedure> Get()
    {
        return _context.PlanProcedures;
        //var names = dbContext.MyTable.Where(x => x.UserId > 10).Select(x => x.Name);
    }

    [HttpGet]
    [EnableQuery]
    public IEnumerable<PlanProcedure> Get(int? planId,int? procedureId)
    {
        var selectedUsers = _context.PlanProcedures.Where(x => x.PlanId == planId).Where(x =>x.ProcedureId == procedureId).Select(x => x.PlanProcedureUsers);
        return (IEnumerable<PlanProcedure>)selectedUsers;
    }

    [HttpPost("AssignUsersToPlanProcedure")]
    public async Task<IActionResult> AssignUsersToPlanProcedure([FromBody] AssignUsersToPlanProcedureCommand command, CancellationToken token)
    {
        var response = await _mediator.Send(command, token);

        return response.ToActionResult();
    }
}

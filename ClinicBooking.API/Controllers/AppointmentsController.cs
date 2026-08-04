using ClinicBooking.Application.Features.Commands.Appointments.CancelAppointment;
using ClinicBooking.Application.Features.Commands.Appointments.CheckInAppointment;
using ClinicBooking.Application.Features.Commands.Appointments.Commands.CreateAppointment;
using ClinicBooking.Application.Features.Commands.Appointments.ConfirmAppointment;
using ClinicBooking.Application.Features.Queries.Appointments.GetAllAppointments;
using ClinicBooking.Application.Features.Queries.Appointments.GetAppointmentById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClinicBooking.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AppointmentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AppointmentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAppointment(
            [FromBody] CreateAppointmentCommand command,
            CancellationToken cancellationToken)
        {
            var appointmentId = await _mediator.Send(command, cancellationToken);
            return Ok(new { Id = appointmentId });
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetAllAppointmentsQuery(), cancellationToken);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetAppointmentByIdQuery { Id = id }, cancellationToken);
            return Ok(result);
        }

        [HttpPut("{id}/confirm")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Confirm(int id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new ConfirmAppointmentCommand { Id = id }, cancellationToken);
            return Ok(result);
        }

        [HttpPut("{id}/cancel")]
        [Authorize(Roles = "Admin,Patient")]
        public async Task<IActionResult> Cancel(int id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new CancelAppointmentCommand { Id = id }, cancellationToken);
            return Ok(result);
        }

        [HttpPut("{id}/checkin")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CheckIn(int id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new CheckInAppointmentCommand { Id = id }, cancellationToken);
            return Ok(result);
        }
    }
}
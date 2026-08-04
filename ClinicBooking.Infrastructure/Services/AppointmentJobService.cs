using ClinicBooking.Application.Services;
using ClinicBooking.Domain.Enums;
using ClinicBooking.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ClinicBooking.Infrastructure.Services
{
    public class AppointmentJobService : IAppointmentJobService
    {
        private readonly AppDbContext _context;
        private readonly INotificationService _notificationService;

        public AppointmentJobService(AppDbContext context, INotificationService notificationService)
        {
            _context = context;
            _notificationService = notificationService;
        }

        public async Task AutoCancelUnconfirmedAppointmentsAsync()
        {
            var cutoffTime = DateTime.UtcNow.AddHours(-1);

            var expiredAppointments = await _context.Appointments
                .Where(a =>
                    a.Status == AppointmentStatus.Pending &&
                    a.CreatedAt <= cutoffTime)
                .ToListAsync();

            foreach (var appointment in expiredAppointments)
            {
                appointment.Cancel();
            }

            if (expiredAppointments.Any())
                await _context.SaveChangesAsync();
        }

        public async Task SendAppointmentReminderAsync(int appointmentId)
        {
            var appointment = await _context.Appointments
                .FirstOrDefaultAsync(a => a.Id == appointmentId);

            if (appointment is null)
                return;

            if (appointment.Status == AppointmentStatus.Cancelled)
                return;

            var message = $"Reminder: You have an appointment scheduled at {appointment.ScheduledAt}.";

            await _notificationService.SendReminderAsync(appointmentId, message);
        }
        public async Task MarkAsNoShowAsync(int appointmentId)
        {
            var appointment = await _context.Appointments
                .FirstOrDefaultAsync(a => a.Id == appointmentId);

            if (appointment is null)
                return;

            if (appointment.IsCheckedIn ||
                appointment.Status == AppointmentStatus.Cancelled ||
                appointment.Status == AppointmentStatus.Completed)
                return;

            appointment.MarkAsNoShow();

            await _context.SaveChangesAsync();

            await _notificationService.SendReminderAsync(
                appointmentId,
                "Appointment marked as No-Show. Patient did not check in.");
        }
    }
}
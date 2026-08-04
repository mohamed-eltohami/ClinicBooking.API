using ClinicBooking.Application.Services;
using Microsoft.Extensions.Logging;

namespace ClinicBooking.Infrastructure.Services
{
    public class NotificationService : INotificationService
    {
        private readonly ILogger<NotificationService> _logger;

        public NotificationService(ILogger<NotificationService> logger)
        {
            _logger = logger;
        }

        public Task SendReminderAsync(int appointmentId, string message)
        {
            _logger.LogInformation(
                "[REMINDER] Appointment #{AppointmentId}: {Message}",
                appointmentId, message);

            return Task.CompletedTask;
        }
    }
}
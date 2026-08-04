namespace ClinicBooking.Application.Services
{
    public interface INotificationService
    {
        Task SendReminderAsync(int appointmentId, string message);
    }
}
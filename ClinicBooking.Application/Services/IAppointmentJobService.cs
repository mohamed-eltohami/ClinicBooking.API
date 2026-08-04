namespace ClinicBooking.Application.Services
{
    public interface IAppointmentJobService
    {
        Task AutoCancelUnconfirmedAppointmentsAsync();
        Task SendAppointmentReminderAsync(int appointmentId);
        Task MarkAsNoShowAsync(int appointmentId);   

    }
}
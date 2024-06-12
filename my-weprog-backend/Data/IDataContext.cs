using my_weprog_backend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace my_weprog_backend.Data
{
    public interface IDataContext
    {
        // User
        void Register(RegisterRequest model);
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        List<User> GetAllUser();
        User GetUserById(int id);
        void UpdateUser(int id, UpdateRequest model);
        void DeleteUser(int id);

        // Camping
        void CreateCamping(Camping camping);
        List<Camping> GetAllCamping();

        // Booking
        Task AddBooking(Booking booking);
        List<Booking> GetBookingsByUserId(int userId);

        List<Booking> GetAllBookings();

        void DeleteBooking(int bookingId); 
    }
}

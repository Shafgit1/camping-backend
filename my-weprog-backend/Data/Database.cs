using LiteDB;
using my_weprog_backend.Models;

namespace my_weprog_backend.Data
{
    public class Database : IDataContext
    {
        private LiteDatabase db = new LiteDatabase("data.db");
        private EnumPrices _enumPrices;

        // User
        public void Register(RegisterRequest model)
        {
            var usersCollection = db.GetCollection<User>("users");
            var user = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Username = model.Username,
                PasswordHash = model.Password
            };
            usersCollection.Insert(user);
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            var user = db.GetCollection<User>("users")
                           .FindOne(x => x.Username == model.Username && x.PasswordHash == model.Password); 
            if (user == null) return null;

            return new AuthenticateResponse
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.Username,
               
            };
        }

        public List<User> GetAllUser()
        {
            return db.GetCollection<User>("users").FindAll().ToList();
        }

        public User GetUserById(int id)
        {
            return db.GetCollection<User>("users").FindById(id);
        }

        public void UpdateUser(int id, UpdateRequest model)
        {
            var collection = db.GetCollection<User>("users");
            var user = collection.FindById(id);
            if (user != null)
            {
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Username = model.Username;
                user.PasswordHash = model.Password;
                collection.Update(user);
            }
        }

        public void DeleteUser(int id)
        {
            db.GetCollection<User>("users").Delete(id);
        }


        // Camping
        public void CreateCamping(Camping camping)
        {
            var campings = db.GetCollection<Camping>("campings");
            campings.Insert(camping);
        }

        public List<Camping> GetAllCamping()
        {
            return db.GetCollection<Camping>("campings").FindAll().ToList();
        }


        // Bookings //
        public async Task AddBooking(Booking booking)
        {
            await Task.Run(() => {
                var bookings = db.GetCollection<Booking>("bookings");
                bookings.Insert(booking);
            });
        }

        public List<Booking> GetBookingsByUserId(int userId)
        {
            var bookings = db.GetCollection<Booking>("bookings").Find(b => b.UserId == userId).ToList();

            foreach (var booking in bookings)
            {
             
                var camping = db.GetCollection<Camping>("campings").FindOne(c => c.Id == booking.CampingId);
                if (camping != null)
                {
                    booking.CampingName = camping.Name;
                }
            }

            return bookings;
        }
        public List<Booking> GetAllBookings()
        {
            var bookings = db.GetCollection<Booking>("bookings").FindAll().ToList();

            foreach (var booking in bookings)
            {
                var camping = db.GetCollection<Camping>("campings").FindOne(c => c.Id == booking.CampingId);
                if (camping != null)
                {
                    booking.CampingName = camping.Name;
                }
            }

            return bookings;
        }
        public void DeleteBooking(int id)
        {
            var bookings = db.GetCollection<Booking>("bookings");
            bookings.Delete(id);
        }

    }
}

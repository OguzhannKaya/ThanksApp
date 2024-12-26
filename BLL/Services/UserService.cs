using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public class UserService : Service, IService<User, UserModel>
    {
        public UserService(Db db) : base(db)
        {
        }

        public Service Create(User record)
        {
            if (_db.Users.Any(u => u.UserName.ToUpper() == record.UserName.ToUpper().Trim() && u.Password.ToUpper() == record.Password.ToUpper().Trim() && u.Gender == record.Gender 
                && u.BirthDate == record.BirthDate))
                return Error("User with the same name,surname gender and birth date exist");
            record.UserName = record.UserName?.Trim();
            _db.Users.Add(record);
            _db.SaveChanges();
            return Success("User created successfully");
        }

        public Service Delete(int id)
        {
            var entity = _db.Users.SingleOrDefault(u => u.Id == id);
            if (entity == null)
                return Error("User not found!");
            _db.Users.Remove(entity);
            _db.SaveChanges();
            return Success("User deleted successfully");
        }

        public IQueryable<UserModel> Query()
        {
            return _db.Users.Include(u => u.Role).OrderByDescending(u => u.Role).ThenBy(u => u.IsActive).ThenBy(u => u.UserName).Select(u => new UserModel()
            { Record = u});
        }

        public Service Update(User record)
        {
            if (_db.Users.Any(u => u.Id!= record.Id && u.UserName.ToUpper() == record.UserName.ToUpper().Trim() && u.Password.ToUpper() == record.Password.ToUpper().Trim() && u.Gender == record.Gender
                && u.BirthDate == record.BirthDate))
                 return Error("User with the same name, surname,gender and birthdate exist!");
            var entity = _db.Users.SingleOrDefault(u =>u.Id == record.Id);
            if (entity == null)
                return Error("User not found!");
            entity.UserName = record.UserName;
            entity.Password = record.Password;
            entity.BirthDate = record.BirthDate;
            entity.Gender = record.Gender;
            entity.IsActive = record.IsActive;
            entity.RoleId = record.RoleId;
            _db.Users.Update(entity);
            _db.SaveChanges();
            return Success("User updated successfully");
            
        }
    }
}

using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public class RoleService : Service, IService<Role, RoleModel>
    {
        public RoleService(Db db) : base(db)
        {
        }

        public Service Create(Role record)
        {
            if (_db.Roles.Any(c => c.Name.ToUpper() == record.Name.ToUpper().Trim()))
                return Error("Role with the same name exist!");
            record.Name = record.Name?.Trim();
            _db.Roles.Add(record);
            _db.SaveChanges();
            return Success("Role is created successfully");
        }

        public Service Delete(int id)
        {
            Role role = _db.Roles.Include(r => r.Users).SingleOrDefault(r => r.Id == id);
            if (role == null)
                return Error("Role is not found!");
            if (role.Users.Any())
                return Error("Role has relational users");
            _db.Remove(role);
            _db.SaveChanges();
            return Success("Role is deleted successfully");
        }

        public IQueryable<RoleModel> Query()
        {
            return _db.Roles.OrderBy(c => c.Name).Select(c => new RoleModel()
            {
                Record = c,
            });
        }

        public Service Update(Role record)
        {
            if (_db.Roles.Any(r => r.Id != record.Id && r.Name.ToUpper() == record.Name.ToUpper().Trim()))
                return Error("Role with the same name exists!");
            var entity = _db.Roles.SingleOrDefault(r => r.Id == record.Id);
            if (entity == null)
                return Error("Role not found!");
            entity.Name = record.Name?.Trim();
            _db.Roles.Update(entity);
            _db.SaveChanges();
            return Success("Role is updated successfully");
        }
    }
}

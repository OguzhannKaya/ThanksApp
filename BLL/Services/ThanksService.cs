using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public class ThanksService : Service, IService<Thanks, ThanksModel>
    {
        public ThanksService(Db db) : base(db)
        {
        }

        public Service Create(Thanks record)
        {
            if (_db.Thanks.Any(t => t.Title.ToUpper() == record.Title.ToUpper().Trim() && t.Text.ToUpper() == record.Text.ToUpper().Trim()))
                return Error("Thanks with the same title and same text exists!");
            record.Title = record.Title?.Trim();
            record.Text = record.Text?.Trim();
            record.CreatedAt = DateTime.Now;
            _db.Thanks.Add(record);
            _db.SaveChanges();
            return Success("Thanks is created successfully.");
        }

        public Service Delete(int id)
        {
            var entity = _db.Thanks.SingleOrDefault(t => t.Id == id);
            if (entity == null)
                return Error("Thanks not found!");
            _db.ThanksTags.RemoveRange(entity.ThanksTags);
            _db.Thanks.Remove(entity);
            _db.SaveChanges();
            return Success("Thanks deleted successfully");
        }

        public IQueryable<ThanksModel> Query()
        {
            return _db.Thanks.Include(t => t.User).Include(t => t.Category).Include(t => t.ThanksTags).ThenInclude(t => t.Tag).OrderBy(t => t.CreatedAt).ThenByDescending(t => t.Category).Select(t => new ThanksModel()
            { Record = t });
        }

        public Service Update(Thanks record)
        {
            if(_db.Thanks.Any(t => t.Id != record.Id && t.Title.ToUpper() == record.Title.ToUpper().Trim() && t.Text.ToUpper() == record.Text.ToUpper().Trim()))
                return Error("Thanks with the same title and same text exists!");
            var entity = _db.Thanks.Include(t => t.ThanksTags).SingleOrDefault(t => t.Id == record.Id);
            if (entity == null)
                return Error("Thanks not found!");

            _db.ThanksTags.RemoveRange(entity.ThanksTags);
            entity.Title = record.Title;
            entity.Text = record.Text;
            entity.CreatedAt = DateTime.Now;
            entity.CategoryId = record.CategoryId;
            entity.UserId = record.UserId;
            entity.ThanksTags = record.ThanksTags;
            _db.Thanks.Update(entity);
            _db.SaveChanges();
            return Success("Thanks updated successfully");
        }
    }
}

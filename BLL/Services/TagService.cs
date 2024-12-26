using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public class TagService : Service, IService<Tag, TagModel>
    {
        public TagService(Db db) : base(db)
        {
        }

        public Service Create(Tag record)
        {
            if (_db.Tags.Any(t => t.Name.ToUpper() == record.Name.ToUpper().Trim()))
                return Error("Tags with the same name exist!");
            record.Name = record.Name?.Trim();
            _db.Tags.Add(record);
            _db.SaveChanges();
            return Success("Tag is created succesfully");
        }

        public Service Delete(int id)
        {
            Tag tag = _db.Tags.SingleOrDefault(t => t.Id == id);
            if (tag == null)
                return Error("Tag is not found!");
            _db.Remove(tag);
            _db.SaveChanges();
            return Success("Tag is deleted successfully");
        }

        public IQueryable<TagModel> Query()
        {
            return _db.Tags.OrderBy(t => t.Name).Select(t => new TagModel()
            {
                Record = t,
            });
        }

        public Service Update(Tag record)
        {

            if (_db.Tags.Any(c => c.Id != record.Id && c.Name.ToUpper() == record.Name.ToUpper().Trim()))
                return Error("Tag with the same name exist!");

            var entity = _db.Tags.SingleOrDefault(c => c.Id == record.Id);
            if (entity is null)
                return Error("Tag is not found!");
            entity.Name = record.Name?.Trim();
            _db.Tags.Update(entity);
            _db.SaveChanges();
            return Success("Tag is updated successfully.");
        }
    }
}

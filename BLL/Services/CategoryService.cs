using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public class CategoryService : Service, IService<Category, CategoryModel>
    {
        public CategoryService(Db db) : base(db)
        {
        }

        public Service Create(Category record)
        {
            if (_db.Categories.Any(c => c.Name.ToUpper() == record.Name.ToUpper().Trim()))
                return Error("Category with the same name exist!");
            record.Name = record.Name?.Trim();
            _db.Categories.Add(record);
            _db.SaveChanges();
            return Success("Category is created succesfully");
        }

        public Service Delete(int id)
        {
            Category category = _db.Categories.Include(c =>c.Thanks).SingleOrDefault(c=> c.Id == id);
            if (category == null)
                return Error("Category is not found!");
            if (category.Thanks.Any())
                return Error("Category has relational products!");
            _db.Remove(category);
            _db.SaveChanges();
            return Success("Category is deleted successfully");
        }

        public IQueryable<CategoryModel> Query()
        {
            return _db.Categories.OrderBy(c => c.Name).Select(c => new CategoryModel()
            {
                Record = c,
            });
        }

        public Service Update(Category record)
        {
            if (_db.Categories.Any(c => c.Id != record.Id && c.Name.ToUpper() == record.Name.ToUpper().Trim()))
                return Error("Category with the same name exist!");

            var entity = _db.Categories.SingleOrDefault(c => c.Id == record.Id);
            if (entity is null)
                return Error("Category not found!");
            entity.Name = record.Name?.Trim();
            _db.Categories.Update(entity);
            _db.SaveChanges();
            return Success("Category is updated successfully.");
        }
    }
}

using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace Inventory.ViewModel
{
    public abstract class BaseViewModel<T> : IViewModel<T> where T : class
    {
        public ProjectFullStack db = new ProjectFullStack();
        public T CurrentEntity { get; set; }
        public DbSet<T> DbSet => db.Set<T>();
      
        public virtual T Find(int id)
        {
            return CurrentEntity = DbSet.Find(id);
        }
        public abstract void New();

        public virtual T Save()
        {
            DbSet.Add(CurrentEntity);
            db.SaveChanges();
            return CurrentEntity;
        }
        public virtual T Update()
        {
            DbSet.AddOrUpdate(CurrentEntity);
            db.SaveChanges();
            return CurrentEntity;
        }
        public virtual bool Delete()
        {
            DbSet.Remove(CurrentEntity);
            return db.SaveChanges() > 0;

        }
    }
}

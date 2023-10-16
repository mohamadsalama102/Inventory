using System;
using System.Data.Entity.Migrations;

namespace Inventory.ViewModel
{
    public class StoreViewModel : IViewModel<Store>
    {
        ProjectFullStack db = new ProjectFullStack();
        public Store CurrentEntity { get; set; }
        public StoreViewModel()
        {
            New();
        }
        public Store Find(int id)
        {
            return CurrentEntity = db.Stores.Find(id);
        }
        public void New()
        {
            CurrentEntity = new Store();
        }

        public Store Save()
        {
            db.Stores.Add(CurrentEntity);
            db.SaveChanges();
            return CurrentEntity;
        }
        public Store Update()
        {
            db.Stores.AddOrUpdate(CurrentEntity);
            db.SaveChanges();
            return CurrentEntity;
        }
        public bool Delete()
        {
            db.Stores.Remove(CurrentEntity);
            return db.SaveChanges() > 0;

        }
    }

}

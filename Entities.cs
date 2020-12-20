using PiyatMandli.DBModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace PiyatMandli
{
    public class Entities
    {
        public PiyatMandliEntities GetDataContext()
        {
            return new PiyatMandliEntities();
        }

        #region Configs

        public IQueryable<Config> GetAll_Configs()
        {
            var db = GetDataContext();
            return db.Configs;
        }

        #endregion Configs

        #region Farmers

        public IQueryable<Farmer> GetAll_Farmers()
        {
            var db = GetDataContext();
            return db.Farmers.Include(x => x.FarmerLands);
        }

        public int AddEntity_Farmer(Farmer entity)
        {
            var db = GetDataContext();
            entity.DateCreated = DateTime.Now;
            entity.DateModified = DateTime.Now;
            entity.IsActive = true;
            entity.IsDeleted = false;
            if (entity.FarmerLands != null)
            {
                foreach (var land in entity.FarmerLands)
                {
                    land.IsActive = true;
                    land.IsDeleted = false;
                    land.CreatedDate = DateTime.Now;
                    land.ModifiedDate = DateTime.Now;
                }
            }
            db.Farmers.Add(entity);
            db.SaveChanges();
            return entity.Id;
        }

        public int UpdateEntity_Farmer(Farmer model)
        {
            var db = GetDataContext();
            var entity = db.Farmers.FirstOrDefault(x => x.Id == model.Id);
            entity.FarmerCode = model.FarmerCode;
            entity.Name = model.Name;
            entity.DateOfRegistration = model.DateOfRegistration;
            entity.DateOfRegistrationEng = model.DateOfRegistrationEng;
            entity.DateOfBirth = model.DateOfBirth;
            entity.DateOfBirthEng = model.DateOfBirthEng;
            entity.AddressLine1 = model.AddressLine1;
            entity.AddressLine2 = model.AddressLine2;
            entity.City = model.City;
            entity.State = model.State;
            entity.Country = model.Country;
            entity.Pincode = model.Pincode;
            entity.PhoneNo1 = model.PhoneNo1;
            entity.PhoneNo2 = model.PhoneNo2;
            entity.MobileNo1 = model.MobileNo1;
            entity.MobileNo2 = model.MobileNo2;
            entity.DateModified = DateTime.Now;

            foreach (var land in entity.FarmerLands)
            {
                var landModel = model.FarmerLands.FirstOrDefault(x => x.Id == land.Id);
                if (landModel != null)
                {
                    land.LandName = land.LandName;
                    land.LandArea = land.LandArea;
                    land.LandAreaEng = land.LandAreaEng;
                    land.ModifiedDate = DateTime.Now;
                }
                else
                {
                    db.FarmerLands.Remove(land);
                }
            }

            foreach (var land in model.FarmerLands)
            {
                if (land.Id == 0)
                {
                    FarmerLand landEntity = new FarmerLand();
                    landEntity.LandName = land.LandName;
                    landEntity.LandArea = land.LandArea;
                    landEntity.LandAreaEng = land.LandAreaEng;
                    landEntity.ModifiedDate = DateTime.Now;
                    landEntity.CreatedDate = DateTime.Now;
                    landEntity.IsActive = true;
                    landEntity.IsDeleted = false;
                    entity.FarmerLands.Add(landEntity);
                }
            }
            db.SaveChanges();
            return entity.Id;
        }

        public bool RemoveEntity_Farmer(int id)
        {
            var db = GetDataContext();
            var entity = db.Farmers.FirstOrDefault(x => x.Id == id);
            entity.IsDeleted = true;
            entity.DateModified = DateTime.Now;
            db.SaveChanges();
            return true;
        }

        #endregion Farmers

        #region Windows

        public IQueryable<WindowMaster> GetAll_Windows()
        {
            var db = GetDataContext();
            return db.WindowMasters;
        }

        public int AddEntity_Window(WindowMaster entity)
        {
            var db = GetDataContext();
            entity.CreatedDate = DateTime.Now;
            entity.ModifiedDate = DateTime.Now;
            entity.IsActive = true;
            entity.IsDeleted = false;
            db.WindowMasters.Add(entity);
            db.SaveChanges();
            return entity.Id;
        }

        public int UpdateEntity_Window(WindowMaster model)
        {
            var db = GetDataContext();
            var entity = db.WindowMasters.FirstOrDefault(x => x.Id == model.Id);
            entity.WindowName = model.WindowName;
            entity.ModifiedDate = DateTime.Now;
            db.SaveChanges();
            return entity.Id;
        }

        public bool RemoveEntity_Window(int windowId)
        {
            var db = GetDataContext();
            var entity = db.WindowMasters.FirstOrDefault(x => x.Id == windowId);
            entity.IsDeleted = true;
            entity.ModifiedDate = DateTime.Now;
            db.SaveChanges();
            return true;
        }

        #endregion Windows
    }
}

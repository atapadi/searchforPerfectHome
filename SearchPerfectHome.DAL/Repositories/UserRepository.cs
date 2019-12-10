using SearchPerfectHome.Interfaces;
using SearchPerfectHome.Models;
using SearchPerfectHome.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchPerfectHome.DAL
{
    public class UserRepository : EntityRepositoryBase<UsersModel>, IUserRepository
    {
        protected override bool OnSaveToStorage(UsersModel model)
        {
            bool returnValue = false;
            USER user = null;
            try
            {
                using (var db = new SearchHomeEntities())
                {
                    if (model.Id == 0)
                    {
                        user = new USER
                        {
                            UserId = model.UserId,
                            UserName = model.UserName,
                            UserImage = model.ImagePath,
                            Degree = model.Degree,
                            Interests = model.Interests,
                            University = model.University,
                            Status = model.Status,
                            UserEmail= model.Email,
                            LastUpdated = DateTime.Now,
                            IsFirstTimeRegister = model.IsFirstTimeRegister
                        };
                        db.USERS.Add(user);
                    }
                    else
                    {
                        user = db.USERS.First(x => x.UserId == model.UserId);
                        user.UserId = model.UserId;
                        user.UserName = model.UserName;
                        user.University = model.University;
                        user.Degree = model.Degree;
                        user.Interests = model.Interests;
                        user.Status = model.Status;
                        user.UserEmail = model.Email;
                        user.LastUpdated = DateTime.Now;
                        user.IsFirstTimeRegister = model.IsFirstTimeRegister;
                        user.UserImage = model.ImagePath;
                    }

                    if (returnValue = db.SaveChanges() > 0)
                        model.UserId = user.UserId;
                }
            }
            catch (Exception ex)
            {
                LogWriter.LogException("User Repository: ", ex);
            }

            return returnValue;
        }

        public UsersModel GetUserProfile(string userId)
        {
            UsersModel model = null;
            using (var db = new SearchHomeEntities())
            {
                model = db.USERS.Where(x => x.UserId == userId)
                     .Select(y => new UsersModel
                     {
                         Id = y.Id,
                         UserName = y.UserName,
                         University = y.University,
                         ImagePath = y.UserImage,
                         Status = y.Status,
                         Degree = y.Degree,
                         Interests = y.Interests,
                         LastUpdated = y.LastUpdated,
                         UserId = y.UserId,
                         Email = y.UserEmail,
                         IsFirstTimeRegister = y.IsFirstTimeRegister
                     }).FirstOrDefault();
            }
            return model;
        }

        public IQueryable<UsersModel> GetAllUsers()
        {
            List<UsersModel> usersModel = null;

            using(var db = new SearchHomeEntities())
            {
                usersModel = db.USERS.Select(x => new UsersModel
                {
                    Email = x.UserEmail,
                    UserId = x.UserId,
                    University = x.University,
                    Degree = x.Degree,
                    Interests = x.Interests,
                    Status = x.Status,
                    ImagePath = x.UserImage,
                    UserName = x.UserName,
                    LastUpdated = x.LastUpdated
                }).OrderByDescending(x => x.LastUpdated).ToList();
            }

            return usersModel.AsQueryable();
        }

        public async Task<bool> DeleteUser(UsersModel model)
        {
            bool returnValue;
            using (var db = new SearchHomeEntities())
            {
                var user = db.USERS.Where(x => x.UserId == model.UserId).First();
                db.USERS.Remove(user);

                returnValue = db.SaveChanges() > 0;
            }

            return await Task.FromResult(returnValue);
        }
    }
}

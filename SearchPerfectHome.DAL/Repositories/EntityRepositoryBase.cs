using SearchPerfectHome.Interfaces;
using SearchPerfectHome.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchPerfectHome.DAL
{
    public abstract class EntityRepositoryBase <T> : IRepository<T> where T : Entity
    {
        protected SearchHomeEntities Context { get; private set; }

        public EntityRepositoryBase(SearchHomeEntities entities)
        {
            Context = entities;
        }

        public EntityRepositoryBase()
        {
        }

        public bool SaveContextChanges()
        {
            bool returnValue = false;
            if (Context.ChangeTracker.HasChanges())
            {
                returnValue = Context.SaveChanges() > 0;
            }
            else
            {
                returnValue = true;
            }
            return returnValue;
        }

        public bool SaveContextChanges(SearchHomeEntities context)
        {
            bool returnValue = false;
            if (context.ChangeTracker.HasChanges())
            {
                returnValue = context.SaveChanges() > 0;
            }
            else
            {
                returnValue = true;
            }
            return returnValue;
        }

        public virtual Task<T> Get(int id, int profileId = 0)
        {
            var returnValue = this.OnGet(id, profileId);
            return Task.FromResult(returnValue);
        }

        public virtual async Task<T> Get(T item)
        {
            return await this.Get(item.Id);
        }

        public virtual Task<IQueryable<T>> GetAll(string userId = "")
        {
            var returnValue = this.OnGetAllItems(userId);
            return Task.FromResult(returnValue);
        }

        public virtual Task<T> Create()
        {
            var returnValue = this.OnCreateEmptyItem();
            return Task.FromResult(returnValue);
        }

        public virtual Task<bool> Add(T item)
        {
            bool returnValue = false;

            if (item != null)
            {
                if (item.Id == 0)
                {
                    returnValue = this.OnSaveToStorage(item);
                }
                else
                {
                    throw new Exception(string.Format("The item with ID {0} already exists and cannot be added.", item.Id));
                }
            }
            else
            {
                throw new Exception("A null item cannot be Added.");
            }

            return Task.FromResult(returnValue);
        }

        public virtual Task<bool> Update(T item)
        {
            bool returnValue = false;

            if (item != null)
            {
                if (item.Id != 0)
                {
                    returnValue = this.OnSaveToStorage(item);
                }
                else
                {
                    throw new Exception("The item to be updated is new and must be added to storage before it can be updated.");
                }
            }
            else
            {
                throw new Exception("A null item cannot be Updated.");
            }

            return Task.FromResult(returnValue);
        }

        public virtual Task<bool> Delete(T item)
        {
            bool returnValue = false;

            if (item != null)
            {
                if (item.Id != 0)
                {
                    returnValue = this.OnRemoveFromStorage(item);
                }
                else
                {
                    throw new Exception("The item to be updated is new and has not been added to storage yet.");
                }
            }
            else
            {
                throw new Exception("A null item cannot be Deleted.");
            }

            return Task.FromResult(returnValue);
        }

        protected virtual IQueryable<T> OnGetAllItems(string userId = "")
        {
            throw new NotImplementedException();
        }

        protected virtual T OnCreateEmptyItem()
        {
            throw new NotImplementedException();
        }

        protected virtual bool OnSaveToStorage(T item)
        {
            throw new NotImplementedException();
        }

        protected virtual bool OnRemoveFromStorage(T item)
        {
            throw new NotImplementedException();
        }

        protected virtual T OnGet(int id, int profileId = 0)
        {
            throw new NotImplementedException();
        }
    }
}

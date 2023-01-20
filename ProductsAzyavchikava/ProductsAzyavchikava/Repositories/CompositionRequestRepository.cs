using Microsoft.EntityFrameworkCore;
using ProductsAzyavchikava.Model;
using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsAzyavchikava.Repositories
{
    public class CompositionRequestRepository : BaseRepository, IRepository<CompositionRequest>
    {
        public CompositionRequestRepository(ApplicationContext context) : base(context)
        {
        }

        public void Create(CompositionRequest model)
        {
            db.CompositionRequests.Add(model);
        }

        public void Delete(CompositionRequest model)
        {
            db.CompositionRequests.Remove(model);
        }

        public IEnumerable<CompositionRequest> GetAll()
        {
            return db.CompositionRequests.Include(p => p.Product).Include(r => r.Request).ToList();
        }

        public IEnumerable<CompositionRequest> GetAllByValue(string value)
        {
            return db.CompositionRequests.Include(p => p.Product)
                                         .Include(r => r.Request)
                                         .Where(c => c.Count.ToString().Contains(value) ||
                                                c.Sum.ToString().Contains(value));
        }

        public CompositionRequest GetModel(Guid id)
        {
            return db.CompositionRequests.Include(p => p.Product).Include(r => r.Request).First(c => c.CompositionRequestId== id);
        }

        public void Update(CompositionRequest model)
        {
            db.CompositionRequests.Update(model);
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }

            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

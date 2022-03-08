using System;
using Infrastructure.DAL.DAL_Core.Interfaces;
using Infrastructure.DAL.DAL_Core.Model;
using Infrastructure.DAL.DAL_Core.EF;

namespace Infrastructure.DAL.DAL_Core.Repositories
{

    public class DbFactory : Disposable, IDbFactory
    {
        EntitiesContext _dbContext;

        public EntitiesContext Init()
        {
            return _dbContext ?? (_dbContext = new EntitiesContext());
        }

        protected override void DisposeCore()
        {
            if (_dbContext != null)
                _dbContext.Dispose();
        }
    }


    public class Disposable : IDisposable
    {
        private bool _isDisposed;

        ~Disposable()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private void Dispose(bool disposing)
        {
            if (!_isDisposed && disposing)
            {
                DisposeCore();
            }

            _isDisposed = true;
        }

        // Ovveride this to dispose custom objects
        protected virtual void DisposeCore()
        {
        }
    }

}

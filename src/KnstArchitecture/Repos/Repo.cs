using System;
using KnstArchitecture.DbSessions;
using KnstArchitecture.UnitOfWorks;

namespace KnstArchitecture.Repos
{
    public abstract class Repo : IRepo
    {
        private bool _disposed = false;
        private IDbSession _dbSession;
        IDbSession IRepo.DbSession
        {
            get
            {
                if (_dbSession is null)
                    _dbSession = _unitOfWork.GetDefaultDbSession();
                return _dbSession;
            }
            set => _dbSession = value;
        }

        protected IUnitOfWork _unitOfWork;

        protected Repo(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        ~Repo() => Dispose(false);

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            _dbSession?.Dispose();
            _unitOfWork?.Dispose();

            _disposed = true;
        }
        
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
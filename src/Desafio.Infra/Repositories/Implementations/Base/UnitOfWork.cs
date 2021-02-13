using Desafio.Infra.Repositories.Context;
using Desafio.Infra.Repositories.Contracts;
using Desafio.Infra.Repositories.Contracts.Base;
using Desafio.Infra.Repositories.Contracts.Writes;
using Desafio.Infra.Repositories.Implementations.Base;
using Desafio.Infra.Repositories.Implementations.Reads;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Desafio.Infra.Repositories.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDbConnection _connection;
        private IDbTransaction _transaction;
        protected DatabaseContext _databaseContext;

        private IUserRepository _userReadRepository;
        private IPdvRepository _pdvReadRepository;

        private IPdvHistoryRepository _pdvHistoryRepository;

        private bool _disposed;

        public UnitOfWork(IConfiguration configuration, DatabaseContext databaseContext)
        {
            _connection = new SqlConnection(configuration.GetConnectionString("dbPDV"));
            _connection.Open();
            _transaction = _connection.BeginTransaction();

            _databaseContext = databaseContext;
        }

        public IUserRepository UserRepository
        {
            get { return _userReadRepository ??= new UserRepository(_transaction); }
        }

        public IPdvRepository PdvRepository
        {
            get { return _pdvReadRepository ??= new PdvRepository(_transaction); }
        }

        public IPdvHistoryRepository PdvHistoryRepository
        {
            get { return _pdvHistoryRepository ??= new PdvHistoryRepository(_databaseContext); }
        }

        public void Commit()
        {
            try
            {
                _transaction.Commit();
            }
            catch
            {
                _transaction.Rollback();
                throw;
            }
            finally
            {
                _transaction.Dispose();
                _transaction = _connection.BeginTransaction();
                resetRepositories();
            }
        }

        private void resetRepositories()
        {
            _pdvReadRepository = null;
            _userReadRepository = null;
        }

        public void Dispose()
        {
            dispose(true);
            GC.SuppressFinalize(this);
        }

        private void dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_transaction != null)
                    {
                        _transaction.Dispose();
                        _transaction = null;
                    }
                    if (_connection != null)
                    {
                        _connection.Dispose();
                        _connection = null;
                    }
                }
                _disposed = true;
            }
        }

        ~UnitOfWork()
        {
            dispose(false);
        }
    }
}
using Desafio.Core.Domain.Models;
using Desafio.Infra.Repositories.Contracts;
using Desafio.Infra.Repositories.Contracts.Base;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Desafio.Infra.Repositories.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDbConnection _connection;
        private IDbTransaction _transaction;
        private IUserReadRepository _userReadRepository;
        private IPdvReadRepository _pdvReadRepository;
        private bool _disposed;

        public UnitOfWork(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }

        public IUserReadRepository UserRepository
        {
            get { return _userReadRepository ?? (_userReadRepository = new UserReadRepository(_transaction)); }
        }

        public IPdvReadRepository PdvRepository
        {
            get { return _pdvReadRepository ?? (_pdvReadRepository = new PdvReadRepository(_transaction)); }
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
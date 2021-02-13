using Desafio.Core.Domain.Models;
using Desafio.Core.Domain.Models.Enums;
using Desafio.Core.Services.Contracts;
using Desafio.Infra.Repositories.Contracts;
using Desafio.Infra.Repositories.Contracts.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Desafio.Core.Services.Implementations
{
    public class PdvServices : IPdvServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public PdvServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ICollection<PdvHistory> GetPdvHistories(DateTime startDate, DateTime endDate)
        {
            return _unitOfWork.PdvHistoryRepository.GetPaymentsHistory(startDate, endDate);
        }

        public Pdv PayBill(double payment, double total)
        {
            try
            {
                var _pdv = new Pdv();
                _pdv.InitiatePdv();
                _pdv.Compute(total, payment);

                // A gravação e tratativas do Histórico também poderia ser controlada
                // por um evento e um controlador deste evento
                // permitindo desacoplar mais a lógica deste código

                if (_pdv.IsClosed.HasValue && _pdv.IsClosed.Value)
                {
                    var historyIn = new PdvHistory
                    {
                        Amount = payment,
                        CreatedAt = DateTime.Now,
                        OperationType = OperationType.In
                    };
                    _unitOfWork.PdvHistoryRepository.Insert(historyIn);

                    if (_pdv.BankCoinsToReturn.Count > 0 || _pdv.BankNotesToReturn.Count > 0)
                    {
                        var historyOut = new PdvHistory
                        {
                            Amount = _pdv.BankNotesToReturn.Sum(b => b) + _pdv.BankCoinsToReturn.Sum(b => b),
                            CreatedAt = DateTime.Now,
                            OperationType = OperationType.Out
                        };
                        _unitOfWork.PdvHistoryRepository.Insert(historyOut);
                    }
                }

                return _pdv;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
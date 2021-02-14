using Desafio.Core.Domain.Models;
using Desafio.Core.Domain.Models.Enums;
using System.Collections.Generic;

namespace Web.Models.Response
{
    public class PdvHistoryResponse
    {
        public List<HistoryData> Histories { get; private set; }

        public PdvHistoryResponse(ICollection<PdvHistory> pdvHistories)
        {
            Histories = new List<HistoryData>();
            foreach (var item in pdvHistories)
            {
                Histories.Add(new HistoryData
                {
                    Amount = item.Amount,
                    Operation = item.OperationType == OperationType.In ? "Entrada" : "Saída",
                    CreateAt = item.CreatedAt.ToString("dd/MM/yyyy HH:mm:ss")
                });
            }
        }
    }

    public class HistoryData
    {
        public double Amount { get; set; }
        public string Operation { get; set; }
        public string CreateAt { get; set; }
    }
}
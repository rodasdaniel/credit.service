using Application.Credit.Common.Utils;
using Domain.Credit.Entities;
using Infrastructure.Credit.Data.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Credit.Data.Repository
{
    public class CreditRepository : ICreditRepository
    {
        #region Constructor
        private readonly ApplicationDBContext _context;
        public CreditRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        #endregion 
        public async Task<long> Create(CreditEntity credit, ClientEntity client,
            List<QuotaEntity> quotas)
        {
            var transaction = _context.Database.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
            try
            {
                ClientEntity oldClient = _context.Cliente.Find(client.IdCliente);
                GetUpdatedFields.updatedFields(ref oldClient, client);
                _context.Credito.Add(credit);
                _context.SaveChanges();
                foreach (QuotaEntity quota in quotas)
                {
                    quota.IdCredito = credit.IdCredito;
                    _context.Cuota.Add(quota);
                }
                _context.SaveChanges();
                transaction.Commit();
                return credit.IdCredito;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }
        }

        public async Task<CreditEntity> GetById(long idCredit)
        {
            return _context.Credito.Find(idCredit);
        }

        public async Task<List<CreditEntity>> GetByIdClient(long idClient)
        {
            return _context.Credito.Where(c => c.IdCliente == idClient).ToList();
        }
    }
}

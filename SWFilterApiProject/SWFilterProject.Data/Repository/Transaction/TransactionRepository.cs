using IdentityModel.Client;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using SWFilterProject.Data.Repository;
using System.Linq.Expressions;

namespace SWFilterProject.Data;

public class TransactionRepository : GenericRepository<Transaction>, ITransactionRepository
{
    private readonly FilterApiDbContext _dbContext;
    public TransactionRepository(FilterApiDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public List<Transaction> GetByParameter(Expression<Func<Transaction, bool>> expression)
    {
       return _dbContext.Set<Transaction>().Where(expression).ToList();
    }

    public List<Transaction> GetByReference(string reference)
    {
        return _dbContext.Set<Transaction>().Where(x=>x.ReferenceNumber == reference).ToList();
    }

}



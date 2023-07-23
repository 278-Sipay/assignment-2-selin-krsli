
using System.Linq.Expressions;

namespace SWFilterProject.Data.Repository;

public interface ITransactionRepository : IGenericRepository<Transaction>
{
    //IEnumerable<Transaction> GetByParameter(Expression<Func<Transaction, bool>> Parameters);
    //object GetByParameter(TransactionFilterParameters filterExpression);

    List<Transaction> GetByReference(string reference);
    List<Transaction> GetByParameter(Expression<Func<Transaction, bool>> expression);
}


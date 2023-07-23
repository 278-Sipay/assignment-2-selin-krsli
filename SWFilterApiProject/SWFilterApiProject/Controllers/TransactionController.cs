using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SWFilterApiProject.Base;
using SWFilterApiProject.Schema;
using SWFilterProject.Data.Repository;
using System.Linq.Expressions;
using Transaction = SWFilterProject.Data.Transaction;

namespace SWFilterProject.Service;

[ApiController]
[Route("second_week/[controller]")]
public class TransactionController : ControllerBase
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IMapper _mapper;
    public TransactionController(ITransactionRepository transactionRepository, IMapper mapper)
    {
        _transactionRepository = transactionRepository;
        _mapper = mapper;
    }

    [HttpGet("GetByParameter")]
    public ApiResponse<List<TransactionResponse>> GetByParameter(
     int? accountNumber = null,
     string referenceNumber = null,
     decimal? minCreditAmount = null,
     decimal? maxCreditAmount = null,
     decimal? minDebitAmount = null,
     decimal? maxDebitAmount = null,
     string description = null,
     DateTime? beginDate = null,
     DateTime? endDate = null)

    {
        Expression<Func<Transaction, bool>> filterExpression = x =>
            (!accountNumber.HasValue || x.AccountNumber == accountNumber.Value) &&
            (string.IsNullOrEmpty(referenceNumber) || x.ReferenceNumber == referenceNumber) &&
            (!minCreditAmount.HasValue || x.CreditAmount >= minCreditAmount) &&
            (!maxCreditAmount.HasValue || x.CreditAmount <= maxCreditAmount) &&
            (!minDebitAmount.HasValue || x.DebitAmount >= minDebitAmount) &&
            (!maxDebitAmount.HasValue || x.DebitAmount <= maxDebitAmount) &&
            (string.IsNullOrEmpty(description) || x.Description.Contains(description)) &&
            (!beginDate.HasValue || x.TransactionDate >= beginDate) &&
            (!endDate.HasValue || x.TransactionDate <= endDate);


        var entityList = _transactionRepository.GetByParameter(filterExpression);
        var mapped = _mapper.Map<List<Transaction>, List<TransactionResponse>>(entityList);
        return new ApiResponse<List<TransactionResponse>>(mapped);
    }

    [HttpGet("GetByReference")]
    public ApiResponse<List<TransactionResponse>> GetByReference(string ReferenceNumber)
    {
        var entityList = _transactionRepository.GetByReference(ReferenceNumber);
        var mapped = _mapper.Map<List<Transaction>, List<TransactionResponse>>(entityList);
        return new ApiResponse<List<TransactionResponse>>(mapped);
    }

    //[HttpGet]
    //public ApiResponse<List<TransactionResponse>> GetAll()
    //{
    //    var entityList = _transactionRepository.GetAll();
    //    var mapped = _mapper.Map<List<Transaction>, List<TransactionResponse>>(entityList);
    //    return new ApiResponse<List<TransactionResponse>>(mapped);
    //}
    //[HttpGet("{id}")]
    //public ApiResponse<TransactionResponse> Get(int id)
    //{
    //    var entity = _transactionRepository.GetById(id);
    //    var mapped = _mapper.Map<Transaction, TransactionResponse>(entity);
    //    return new ApiResponse<TransactionResponse>(mapped);
    //}

    //[HttpPost]
    //public ApiResponse Post([FromBody] TransactionRequest request)
    //{
    //    var entity = _mapper.Map<TransactionRequest, Transaction>(request);
    //    _transactionRepository.Insert(entity);
    //    _transactionRepository.Save();
    //    return new ApiResponse();
    //}

    //[HttpGet("GetByParameter")]
    //public IActionResult GetByParameter([FromQuery] TransactionFilterParameters filterExpression)
    //{
    //    var transactions = _transactionRepository.GetByParameter(filterExpression);
    //    return Ok(transactions);
    //}
}


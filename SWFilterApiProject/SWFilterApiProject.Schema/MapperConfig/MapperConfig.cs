
using AutoMapper;
using SWFilterProject.Data;

namespace SWFilterApiProject.Schema;

public class MapperConfig : Profile
{
    public MapperConfig()
    {
        CreateMap<TransactionRequest, Transaction>();
        CreateMap<Transaction, TransactionResponse>();
    }
}

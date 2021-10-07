using System;
using API.Data.Models;
using API.Data.Persistence.Entities;
using AutoMapper;

namespace API.Application.AutoMapperProfiles
{
    public class CryptoWalletTransactionProfie : Profile
    {
        public CryptoWalletTransactionProfie()
        {
            CreateMap<CryptoWalletTransaction, WalletTransaction>();
        }
    }
}

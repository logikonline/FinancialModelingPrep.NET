﻿using MatthiWare.FinancialModelingPrep.Model;
using MatthiWare.FinancialModelingPrep.Model.MarketIndexes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MatthiWare.FinancialModelingPrep.Abstractions.MarketIndexes
{
    public interface IMarketIndexesProvider
    {
        Task<ApiResponse<List<IndexConstituentResponse>>> GetSP500CompaniesAsync();
        Task<ApiResponse<List<IndexConstituentResponse>>> GetNasdaqCompaniesAsync();
        Task<ApiResponse<List<IndexConstituentResponse>>> GetDowJonesCompaniesAsync();
		Task<ApiResponse<List<SectorConstituentResponse>>> GetSectorPerformanceAsync();
	}
}

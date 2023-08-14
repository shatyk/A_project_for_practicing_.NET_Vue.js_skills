﻿using Backend.Models;
using Backend.Models.Requests;
using Backend.Models.Responses;

namespace Backend.Interfaces
{
    public interface IFundraisingService
    {
        Task<PagedList<FundraisingGetAllResponse>> GetAllAsync(
            string? searchCaptionTerm,
            string? sortDateOrder,
            int page,
            int pageSize);
        Task<IEnumerable<FundraisingGetAllCaptionsResponse>> GetAllCaptionssAsync();
        Task<FundraisingGetOneResponse> GetAsync(long id);
        Task<long> AddAsync(FundraisingAddRequest request, CancellationToken cancellationToken);
        Task UpdateAsync(FundraisingUpdateRequest request, CancellationToken cancellationToken);
        Task DeleteAsync(long id, CancellationToken cancellationToken);
    }
}

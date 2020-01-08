using AutoMapper;
using LSG.GenericCrud.Dto.Services;
using LSG.GenericCrud.Models;
using LSG.GenericCrud.Repositories;
using LSG.GenericCrud.Services;
using Sample.Complete.Models.DTOs;
using Sample.Complete.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sample.Complete.Services
{
    public class AccountService : IHistoricalCrudService<Guid, AccountDto>
    {
        private readonly IHistoricalCrudService<Guid, Account> _service;
        private readonly IMapper _mapper;

        public bool AutoCommit { get; set; }

        public AccountService(
            IHistoricalCrudService<Guid, Account> service,
            IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<AccountDto> CreateAsync(AccountDto dto)
        {
            var entity = _mapper.Map<Account>(dto);
            entity.Status = "Creating";
            var createdEntity = await _service.CreateAsync(entity);
            return _mapper.Map<AccountDto>(createdEntity);
        }

        public async Task<AccountDto> UpdateAsync(Guid id, AccountDto dto)
        {
            var actualEntity = _mapper.Map<Account>(dto);

            var existingEntity = await _service.GetByIdAsync(id);

            if (existingEntity.Status == "Creating") actualEntity.Status = "Created";
            else actualEntity.Status = existingEntity.Status;

            return _mapper.Map<AccountDto>(await _service.UpdateAsync(id, actualEntity));
        }

        public async Task<AccountDto> DeleteAsync(Guid id)
        {
            var existingEntity = await _service.GetByIdAsync(id);

            // should pass through injected data filler
            if (existingEntity.Status == "Created")
            {
                existingEntity.Status = "Deleted";
                await _service.UpdateAsync(id, existingEntity);
            }

            return _mapper.Map<AccountDto>(await _service.DeleteAsync(id));
        }


        public async Task<AccountDto> CopyFromChangeset(Guid entityId, Guid changesetId) => throw new NotImplementedException();
        // Not used anymore (dead code, will be removed in v5)
        public AccountDto Create(AccountDto entity) => throw new NotImplementedException();
        // Not used anymore (dead code, will be removed in v5)
        public AccountDto Delete(Guid id) => throw new NotImplementedException();
        public Task<object> Delta(Guid id, DeltaRequest request) => throw new NotImplementedException();
        // Not used anymore (dead code, will be removed in v5)
        public IEnumerable<AccountDto> GetAll() => throw new NotImplementedException();
        // Not used anymore (dead code, will be removed in v5)
        public Task<IEnumerable<AccountDto>> GetAllAsync() => throw new NotImplementedException();
        // Not used anymore (dead code, will be removed in v5)
        public async Task<AccountDto> CopyAsync(Guid id) => throw new NotImplementedException();
        public AccountDto GetById(Guid id) => throw new NotImplementedException();
        // Not used anymore (dead code, will be removed in v5)
        public Task<AccountDto> GetByIdAsync(Guid id) => throw new NotImplementedException();
        public IEnumerable<IEntity> GetHistory(Guid id) => throw new NotImplementedException();
        public Task<IEnumerable<IEntity>> GetHistoryAsync(Guid id) => throw new NotImplementedException();
        public Task<IEnumerable<ReadeableStatus<AccountDto>>> GetReadStatusAsync() => throw new NotImplementedException();
        public Task<ReadeableStatus<AccountDto>> GetReadStatusByIdAsync(Guid id) => throw new NotImplementedException();
        public Task MarkAllAsRead() => throw new NotImplementedException();
        public Task MarkAllAsUnread() => throw new NotImplementedException();
        public Task MarkOneAsRead(Guid id) => throw new NotImplementedException();
        public Task MarkOneAsUnread(Guid id) => throw new NotImplementedException();
        public AccountDto Restore(Guid id) => throw new NotImplementedException();
        public Task<AccountDto> RestoreAsync(Guid id) => throw new NotImplementedException();
        public Task<AccountDto> RestoreFromChangeset(Guid entityId, Guid changesetId) => throw new NotImplementedException();
        public AccountDto Update(Guid id, AccountDto entity) => throw new NotImplementedException();

    }
}

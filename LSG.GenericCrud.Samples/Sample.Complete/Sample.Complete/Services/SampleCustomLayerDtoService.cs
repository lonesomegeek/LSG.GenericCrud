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
    public class SampleCustomLayerDtoService : IHistoricalCrudService<Guid, AccountDto>
    {
        private readonly ICrudRepository _repository;
        private readonly IHistoricalCrudService<Guid, Account> _service;
        private readonly IMapper _mapper;

        //IHistoricalCrudService<Guid, Account> _service;

        public bool AutoCommit { get; set; }

        public SampleCustomLayerDtoService(
            IHistoricalCrudService<Guid, Account> service,
            IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public Task<AccountDto> CopyAsync(Guid id) { throw new NotImplementedException(); }

        public Task<AccountDto> CopyFromChangeset(Guid entityId, Guid changesetId)
        {
            throw new NotImplementedException();
        }

        public AccountDto Create(AccountDto entity)
        {
            throw new NotImplementedException();
        }

        public async Task<AccountDto> CreateAsync(AccountDto dto)
        {
            var entity = _mapper.Map<Account>(dto);
            entity.Status = "Creating";
            var createdEntity = await _service.CreateAsync(entity);
            return _mapper.Map<AccountDto>(createdEntity);
        }

        public AccountDto Delete(Guid id)
        {
            throw new NotImplementedException();
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
            //if (existingEntity.Status == "Creating") existingEntity.Status = "Hard Delete";
            //else if (existingEntity.Status == "Created") existingEntity.Status = "Soft Delete";

            //var updatedEntity = await _service.UpdateAsync(id, existingEntity);

            return _mapper.Map<AccountDto>(await _service.DeleteAsync(id));
        }

        public Task<object> Delta(Guid id, DeltaRequest request)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AccountDto> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AccountDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public AccountDto GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<AccountDto> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IEntity> GetHistory(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<IEntity>> GetHistoryAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ReadeableStatus<AccountDto>>> GetReadStatusAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ReadeableStatus<AccountDto>> GetReadStatusByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task MarkAllAsRead()
        {
            throw new NotImplementedException();
        }

        public Task MarkAllAsUnread()
        {
            throw new NotImplementedException();
        }

        public Task MarkOneAsRead(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task MarkOneAsUnread(Guid id)
        {
            throw new NotImplementedException();
        }

        public AccountDto Restore(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<AccountDto> RestoreAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<AccountDto> RestoreFromChangeset(Guid entityId, Guid changesetId)
        {
            throw new NotImplementedException();
        }

        public AccountDto Update(Guid id, AccountDto entity)
        {
            throw new NotImplementedException();
        }

        public async Task<AccountDto> UpdateAsync(Guid id, AccountDto dto)
        {
            var actualEntity = _mapper.Map<Account>(dto);

            var existingEntity = await _service.GetByIdAsync(id);
            
            if (existingEntity.Status == "Creating") actualEntity.Status = "Created";
            else actualEntity.Status = existingEntity.Status;
            
            return _mapper.Map<AccountDto>(await _service.UpdateAsync(id, actualEntity));
        }
    }
}

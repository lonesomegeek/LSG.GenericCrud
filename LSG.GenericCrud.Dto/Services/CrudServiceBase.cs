using AutoMapper;
using LSG.GenericCrud.Models;
using LSG.GenericCrud.Repositories;
using LSG.GenericCrud.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LSG.GenericCrud.Dto.Services
{   
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDto">The type of the dto.</typeparam>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <seealso cref="LSG.GenericCrud.Services.CrudService{TEntity}" />
    /// <seealso cref="LSG.GenericCrud.Services.ICrudService{TDto}" />
    public class CrudServiceBase<TId, TDto, TEntity> :
        ICrudService<TId, TDto>
        where TDto : IEntity<TId>
        where TEntity : class, IEntity<TId>, new()
    {
        private readonly ICrudService<TId, TEntity> _service;

        /// <summary>
        /// The mapper
        /// </summary>
        private readonly IMapper _mapper;

        private readonly ICrudRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CrudServiceBase{TDto, TEntity}"/> class.
        /// </summary>
        /// <param name="service"></param>
        /// <param name="repository">The repository.</param>
        /// <param name="mapper">The mapper.</param>
        public CrudServiceBase(ICrudService<TId, TEntity> service, ICrudRepository repository, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
            _repository = repository;
            AutoCommit = true;
            _service.AutoCommit = true;
        }

        public bool AutoCommit { get; set; }

        public virtual TDto Create(TDto dto) => CreateAsync(dto).GetAwaiter().GetResult();
        public virtual async Task<TDto> CreateAsync(TDto dto)
        {
            var entity = _mapper.Map<TEntity>(dto);
            var createdEntity = await _service.CreateAsync(entity);
            await _repository.SaveChangesAsync();
            return _mapper.Map<TDto>(createdEntity);
        }

        public virtual TDto Delete(TId id) => DeleteAsync(id).GetAwaiter().GetResult();
        public virtual async Task<TDto> DeleteAsync(TId id)
        {
            var deletedEntity = await _service.DeleteAsync(id);
            await _repository.SaveChangesAsync();
            return _mapper.Map<TDto>(deletedEntity);
        }

        public virtual async Task<TDto> CopyAsync(TId id)
        {
            var copiedEntity = await _service.CopyAsync(id);
            await _repository.SaveChangesAsync();
            return _mapper.Map<TDto>(copiedEntity);
        }

        public virtual IEnumerable<TDto> GetAll() => GetAllAsync().GetAwaiter().GetResult();

        public virtual async Task<IEnumerable<TDto>> GetAllAsync()
        {
            var entities = await _service.GetAllAsync();
            return entities.Select(_ => _mapper.Map<TDto>(_));
        }

        public virtual async Task<TDto> GetByIdAsync(TId id) => _mapper.Map<TDto>(await _service.GetByIdAsync(id));

        public virtual TDto GetById(TId id) => GetByIdAsync(id).GetAwaiter().GetResult();

        public virtual TDto Update(TId id, TDto dto) => UpdateAsync(id, dto).GetAwaiter().GetResult();

        public virtual async Task<TDto> UpdateAsync(TId id, TDto dto)
        {
            var updatedEntity = await _service.UpdateAsync(id, _mapper.Map<TEntity>(dto));
            await _repository.SaveChangesAsync();
            return _mapper.Map<TDto>(updatedEntity);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LSG.GenericCrud.Models;
using LSG.GenericCrud.Repositories;
using LSG.GenericCrud.Services;

namespace LSG.GenericCrud.Dto.Services
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDto">The type of the dto.</typeparam>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <seealso cref="LSG.GenericCrud.Services.HistoricalCrudService{TEntity}" />
    /// <seealso cref="LSG.GenericCrud.Services.IHistoricalCrudService{TDto}" />
    public class HistoricalCrudService<TDto, TEntity> : HistoricalCrudService<TEntity>, IHistoricalCrudService<TDto>
        where TDto : IEntity
        where TEntity : class, IEntity, new()
    {
        /// <summary>
        /// The mapper
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="HistoricalCrudService{TDto, TEntity}"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="eventRepository">The event repository.</param>
        /// <param name="mapper">The mapper.</param>
        public HistoricalCrudService(ICrudRepository repository, IMapper mapper) : base(repository)
        {
            _mapper = mapper;
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TDto> GetAll() => base.GetAll().Select(_ => _mapper.Map<TDto>(_));

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public TDto GetById(Guid id) => _mapper.Map<TDto>(base.GetById(id));

        /// <summary>
        /// Creates the specified dto.
        /// </summary>
        /// <param name="dto">The dto.</param>
        /// <returns></returns>
        public TDto Create(TDto dto)
        {
            var createdEntity = base.Create(_mapper.Map<TEntity>(dto));
            return _mapper.Map<TDto>(createdEntity);
        }

        /// <summary>
        /// Updates the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="dto">The dto.</param>
        /// <returns></returns>
        public TDto Update(Guid id, TDto dto)
        {
            var updatedEntity = base.Update(id, _mapper.Map<TEntity>(dto));
            return _mapper.Map<TDto>(updatedEntity);
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public TDto Delete(Guid id)
        {
            var deletedEntity = base.Delete(id);
            return _mapper.Map<TDto>(deletedEntity);
        }

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<TDto>> GetAllAsync()
        {
            var entities = await base.GetAllAsync();
            return entities.Select(_ => _mapper.Map<TDto>(_));
        }

        /// <summary>
        /// Gets the by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<TDto> GetByIdAsync(Guid id)
        {
            var entity = await base.GetByIdAsync(id);
            return _mapper.Map<TDto>(entity);
        }

        /// <summary>
        /// Creates the asynchronous.
        /// </summary>
        /// <param name="dto">The dto.</param>
        /// <returns></returns>
        public async Task<TDto> CreateAsync(TDto dto)
        {
            var createdEntity = await base.CreateAsync(_mapper.Map<TEntity>(dto));
            return _mapper.Map<TDto>(createdEntity);
        }

        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="dto">The dto.</param>
        /// <returns></returns>
        public async Task<TDto> UpdateAsync(Guid id, TDto dto)
        {
            var updatedEntity = await base.UpdateAsync(id, _mapper.Map<TEntity>(dto));
            return _mapper.Map<TDto>(updatedEntity);
        }

        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<TDto> DeleteAsync(Guid id)
        {
            var deletedEntity = await base.DeleteAsync(id);
            return _mapper.Map<TDto>(deletedEntity);
        }

        /// <summary>
        /// Restores the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public TDto Restore(Guid id)
        {
            var restoredEntity = base.Restore(id);
            return _mapper.Map<TDto>(restoredEntity);
        }

        /// <summary>
        /// Restores the asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<TDto> RestoreAsync(Guid id)
        {
            var restoredEntity = await base.RestoreAsync(id);
            return _mapper.Map<TDto>(restoredEntity);
        }
    }
}

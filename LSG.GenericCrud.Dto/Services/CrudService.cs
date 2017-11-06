using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// <seealso cref="LSG.GenericCrud.Services.CrudService{TEntity}" />
    /// <seealso cref="LSG.GenericCrud.Services.ICrudService{TDto}" />
    public class CrudService<TDto, TEntity> : CrudService<TEntity>, ICrudService<TDto>
        where TDto : IEntity 
        where TEntity : class, IEntity, new()
    {
        /// <summary>
        /// The mapper
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="CrudService{TDto, TEntity}"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="mapper">The mapper.</param>
        public CrudService(ICrudRepository repository, IMapper mapper) : base(repository)
        {
            _mapper = mapper;
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        public new IEnumerable<TDto> GetAll()
        {
            return base.GetAll().Select(_ => _mapper.Map<TDto>(_));
        }
        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <returns></returns>
        public new async Task<IEnumerable<TDto>> GetAllAsync()
        {
            var results = await base.GetAllAsync();
            return results.Select(_ => _mapper.Map<TDto>(_));
        }

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public new TDto GetById(Guid id)
        {
            return _mapper.Map<TDto>(base.GetById(id));
        }

        /// <summary>
        /// Gets the by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public new async Task<TDto> GetByIdAsync(Guid id)
        {
            var result = await base.GetByIdAsync(id);
            return _mapper.Map<TDto>(result);
        }

        /// <summary>
        /// Creates the specified dto.
        /// </summary>
        /// <param name="dto">The dto.</param>
        /// <returns></returns>
        public TDto Create(TDto dto)
        {
            var entity = _mapper.Map<TEntity>(dto);
            var createdEntity = base.Create(entity);
            return _mapper.Map<TDto>(createdEntity);
        }

        /// <summary>
        /// Creates the asynchronous.
        /// </summary>
        /// <param name="dto">The dto.</param>
        /// <returns></returns>
        public async Task<TDto> CreateAsync(TDto dto)
        {
            var entity = _mapper.Map<TEntity>(dto);
            var createdEntity = await base.CreateAsync(entity);
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
            var entity = _mapper.Map<TEntity>(dto);
            var updatedEntity = base.Update(id, entity);
            return _mapper.Map<TDto>(updatedEntity);
        }

        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="dto">The dto.</param>
        /// <returns></returns>
        public async Task<TDto> UpdateAsync(Guid id, TDto dto)
        {
            var entity = _mapper.Map<TEntity>(dto);
            var updatedEntity = await base.UpdateAsync(id, entity);
            return _mapper.Map<TDto>(updatedEntity);
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public new TDto Delete(Guid id)
        {
            var deletedEntity = base.Delete(id);
            return _mapper.Map<TDto>(deletedEntity);
        }

        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public new async Task<TDto> DeleteAsync(Guid id)
        {
            var deletedEntity = await base.DeleteAsync(id);
            return _mapper.Map<TDto>(deletedEntity);
        }
    }
}

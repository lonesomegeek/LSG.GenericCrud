using System;
using System.Linq;
using AutoMapper;
using LSG.GenericCrud.Dto.Services;
using LSG.GenericCrud.Exceptions;
using LSG.GenericCrud.Models;
using LSG.GenericCrud.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LSG.GenericCrud.Dto.Controllers
{
    public class CrudController<TDto, TEntity> : 
        Controller
        where TDto : class, IEntity, new()
        where TEntity : class, IEntity, new()
    {
        private readonly CrudService<TDto, TEntity> _service;

        public CrudController(CrudService<TDto, TEntity> service)
        {
            _service = service;
        }
    }
}

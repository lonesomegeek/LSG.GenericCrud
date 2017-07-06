using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LSG.GenericCrud.Controllers;
using LSG.GenericCrud.Models;
using LSG.GenericCrud.Repositories;
using LSG.GenericCrud.TestApi.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace LSG.GenericCrud.TestApi.Controllers
{
    [Route("api/[controller]")]
    public class EntitiesCustomDalController : CrudController<CustomCarrot>
    {
        public EntitiesCustomDalController(ICrud<CustomCarrot> dal) : base(dal) { }
    }

    public class CustomCarrotDal : ICrud<CustomCarrot>
    {
        public IEnumerable<CustomCarrot> GetAll()
        {
            return new List<CustomCarrot> { new CustomCarrot() { Id = Guid.NewGuid(), IsSuperPowered = true, Name = "Mighty S Banana!"} };
        }

        public CustomCarrot GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public CustomCarrot Create(CustomCarrot entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Guid id, CustomCarrot entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }

    public class CustomCarrot : IEntity
    {
        public Guid Id { get; set; }
        public bool IsSuperPowered { get; set; }
        public string Name { get; set; }
    }
}

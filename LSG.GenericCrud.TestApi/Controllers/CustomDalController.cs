using System;
using System.Collections.Generic;
using LSG.GenericCrud.Controllers;
using LSG.GenericCrud.Repositories;
using LSG.GenericCrud.TestApi.Models;
using LSG.GenericCrud.TestApi.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace LSG.GenericCrud.TestApi.Controllers
{
    [Route("api/[controller]")]
    public class CustomDalController : CrudController<Carrot>
    {
        public CustomDalController(CustomDal dal) : base(dal) { }
    }

    public class CustomDal : ICrud<Carrot> {
        public IEnumerable<Carrot> GetAll()
        {
            var carrots = new List<Carrot> {new Carrot() {Id = Guid.NewGuid(), Color = "test"}};
            return carrots;
        }

        public Carrot GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Carrot Create(Carrot entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Guid id, Carrot entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using LSG.GenericCrud.Models;

namespace WebApplication1.Controllers
{
    public class Sample : IEntity
    {
        public Guid Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
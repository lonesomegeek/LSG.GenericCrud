using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Running;
using Bogus;
using LSG.GenericCrud.Benchmark.Models;
using LSG.GenericCrud.Controllers;
using LSG.GenericCrud.Repositories;
using LSG.GenericCrud.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LSG.GenericCrud.Benchmark
{
    public class MyController : 
        ControllerBase,
        ICrudController<Guid, Item>
    {
        private readonly ICrudController<Guid, Item> _controller;        

        public MyController(ICrudController<Guid, Item> controller)
        {
            
            _controller = controller;
        }

        public Task<ActionResult<Item>> Create([FromBody] Item entity) => _controller.Create(entity);

        public Task<ActionResult<Item>> Delete(Guid id) => _controller.Delete(id);

        public Task<ActionResult<IEnumerable<Item>>> GetAll() => _controller.GetAll();

        public Task<ActionResult<Item>> GetById(Guid id) => _controller.GetById(id);

        public Task<IActionResult> HeadById(Guid id) => _controller.HeadById(id);

        public Task<IActionResult> Update(Guid id, [FromBody] Item entity) => _controller.Update(id, entity);
    }
    [MemoryDiagnoser]
    [MinColumn, MaxColumn, MeanColumn, MedianColumn]
    [SimpleJob(RunStrategy.Throughput, launchCount: 2, warmupCount: 1, targetCount: 5)]
    [RPlotExporter]
    public class CrudBenchmark
    {
        private readonly ICrudController<Guid, Item> _controller;
        private readonly ICrudRepository _repository;
        private readonly ICrudService<Guid, Item> _service;
        private readonly IDbContext _context;
        private readonly List<Item> _entities;
        private readonly Item _entity;

        public CrudBenchmark()
        {
            var optionsBuilder = new DbContextOptionsBuilder()
                .UseInMemoryDatabase("test");
            //.UseSqlServer("server=(localdb)\\mssqllocaldb;Initial Catalog=BenchmarkDb");
            _context = new BenchmarkingContext(optionsBuilder.Options, null);
            ((DbContext)_context).Database.EnsureCreated();
            _repository = new CrudRepository(_context);
            _service = new CrudServiceBase<Guid, Item>(_repository);
            _controller = new CrudControllerBase<Guid, Item>(_service);

            var entityFaker = new Faker<Item>().
                RuleFor(_ => _.Id, Guid.NewGuid()).
                RuleFor(_ => _.Name, _ => _.Lorem.Word());
            _entities = entityFaker.Generate(5);
            _entity = entityFaker.Generate();
        }

        [Benchmark]
        public async Task Create() => await _controller.Create(new Item());
        [Benchmark]
        public async Task Read() => await _controller.GetById(Guid.NewGuid());
        [Benchmark]
        public async Task ReadAll() => await _controller.GetAll();
        [Benchmark]
        public async Task Update() => await _controller.Update(Guid.NewGuid(), new Item());
        [Benchmark]
        public async Task Delete() => await _controller.Delete(Guid.NewGuid());
        [Benchmark]
        public async Task CreateAndRead()
        {
           var createdEntityResult = await _controller.Create(new Item());
           var createdEntity = ((Item)((CreatedAtActionResult)createdEntityResult.Result).Value);
           await _controller.GetById(createdEntity.Id);
        }
        [Benchmark]
        public async Task CreateAndDelete()
        {
           var createdEntityResult = await _controller.Create(new Item());
           var createdEntity = ((Item)((CreatedAtActionResult)createdEntityResult.Result).Value);
           await _controller.Delete(createdEntity.Id);
        }
        [Benchmark]
        public async Task CreateAndUpdate()
        {
           var createdEntityResult = await _controller.Create(new Item());
           var createdEntity = ((Item)((CreatedAtActionResult)createdEntityResult.Result).Value);
           createdEntity.Name = "Modified Value";
           var updatedEntityResult = await _controller.Update(createdEntity.Id, createdEntity);
        }
    }

    [MemoryDiagnoser]
    [MinColumn, MaxColumn, MeanColumn, MedianColumn]
    [SimpleJob(RunStrategy.Throughput, launchCount: 2, warmupCount: 1, targetCount: 5)]
    [RPlotExporter]
    public class HistoricalCrudBenchmark
    {
        private readonly ICrudController<Guid, Item> _controller;
        private readonly ICrudRepository _repository;
        private readonly ICrudService<Guid, Item> _service;
        private readonly IDbContext _context;
        private readonly List<Item> _entities;
        private readonly Item _entity;

        public HistoricalCrudBenchmark()
        {
            var optionsBuilder = new DbContextOptionsBuilder()
                .UseInMemoryDatabase("test");
            //.UseSqlServer("server=(localdb)\\mssqllocaldb;Initial Catalog=BenchmarkDb");
            _context = new BenchmarkingContext(optionsBuilder.Options, null);
            ((DbContext)_context).Database.EnsureCreated();
            _repository = new CrudRepository(_context);
            _service = new HistoricalCrudServiceBase<Guid, Item>(
                new CrudServiceBase<Guid, Item>(_repository),
                _repository,
                null,
                null,
                null);
            _controller = new HistoricalCrudControllerBase<Guid, Item>(
                new CrudControllerBase<Guid, Item>(new CrudServiceBase<Guid, Item>(_repository)),
                (IHistoricalCrudService<Guid, Item>)_service);

            var entityFaker = new Faker<Item>().
                RuleFor(_ => _.Id, Guid.NewGuid()).
                RuleFor(_ => _.Name, _ => _.Lorem.Word());
            _entities = entityFaker.Generate(5);
            _entity = entityFaker.Generate();
        }

        [Benchmark]
        public async Task Create() => await _controller.Create(new Item());
        [Benchmark]
        public async Task Read() => await _controller.GetById(Guid.NewGuid());
        [Benchmark]
        public async Task ReadAll() => await _controller.GetAll();
        [Benchmark]
        public async Task Update() => await _controller.Update(Guid.NewGuid(), new Item());
        [Benchmark]
        public async Task Delete() => await _controller.Delete(Guid.NewGuid());
        [Benchmark]
        public async Task CreateAndRead()
        {
           var createdEntityResult = await _controller.Create(new Item());
           var createdEntity = ((Item)((CreatedAtActionResult)createdEntityResult.Result).Value);
           await _controller.GetById(createdEntity.Id);
        }
        [Benchmark]
        public async Task CreateAndDelete()
        {
           var createdEntityResult = await _controller.Create(new Item());
           var createdEntity = ((Item)((CreatedAtActionResult)createdEntityResult.Result).Value);
           await _controller.Delete(createdEntity.Id);
        }
        [Benchmark]
        public async Task CreateAndUpdate()
        {
           var createdEntityResult = await _controller.Create(new Item());
           var createdEntity = ((Item)((CreatedAtActionResult)createdEntityResult.Result).Value);
           createdEntity.Name = "Modified Value";
           var updatedEntityResult = await _controller.Update(createdEntity.Id, createdEntity);
        }
    }
    class Program
    {
        // static void Main(string[] args)
        // {
        //     var summary = BenchmarkRunner
        //         .Run<HistoricalCrudBenchmark>(/*new DebugInProcessConfig()*/);
        //         BenchmarkRunner.Run<CrudBenchmark>();
        //         //.Run<IntroSetupCleanupIteration>(/*new DebugInProcessConfig()*/);
        //     Console.WriteLine(summary);
        // }

        public static void Main(string[] args) => BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Interfaces.Repositories;
using Weblu.Infrastructure.Data;

namespace Weblu.Infrastructure.Repositories
{
    public class UnitOfWork(ApplicationDbContext context) : IUnitOfWork
    {
        private readonly ApplicationDbContext _context = context;

        private IServiceRepository? _serviceRepo;
        public IServiceRepository Services => _serviceRepo ??= new ServiceRepository(_context);

        private IFeatureRepository? _featureRepo;
        public IFeatureRepository Features => _featureRepo ??= new FeatureRepository(_context);

        private IMethodRepository? _methodRepository;
        public IMethodRepository Methods => _methodRepository ??= new MethodRepository(_context);

        private IImageRepository? _imageRepository;
        public IImageRepository Images => _imageRepository ??= new ImageRepository(_context);

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
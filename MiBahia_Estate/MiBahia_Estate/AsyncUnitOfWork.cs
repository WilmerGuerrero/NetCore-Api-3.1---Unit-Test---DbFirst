﻿using Domain;
using Domain.Repositories;
using MiBahia_Estate.Persistence.Repositories;
using MiBahia_Estate.Repositories;
using MiBahia_Estate.Solares;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiBahia_Estate
{
    public class AsyncUnitOfWork:IAsyncUnitOfWork
    {
        private readonly bahia_estateContext _context;

        public AsyncUnitOfWork(bahia_estateContext context, DbSet<House> house, DbSet<BuildingSite> buildingSite )
        {
            this._context = context;
            Houses = new AsyncHouseRepository(_context);
            BuildingSites = new AsyncBuildingSiteRepository(_context,buildingSite);
        }

        //public IAsyncRepository RelatedEntities { get; private set; }
        public IAsyncPropertyAddressRepository PropertyAddress { get; private set; }
        public IAsyncPropertyPriceRepository PropertyPrice { get; private set; }
        
        public IAsyncPropertyPhotosRepository PropertyPhotos { get; private set; }
        public IAsyncHouseRepository Houses { get ; private set; }
        public IAsyncBuildingSiteRepository BuildingSites { get ; private set; }

        public async Task<int> Complete()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.DisposeAsync();
        }


    }
}

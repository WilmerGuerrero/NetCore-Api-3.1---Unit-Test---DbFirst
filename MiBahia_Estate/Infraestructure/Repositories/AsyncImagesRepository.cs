﻿using Domain.Repositories;
using MiBahia_Estate;
using MiBahia_Estate.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repositories
{
    public class AsyncImagesRepository : IAsyncImagesRepository<PropertyPhotos>
    {
        private readonly SaveImages _upload;
        private readonly bahia_estateContext _context;

        public AsyncImagesRepository(SaveImages upload, bahia_estateContext context)
        {
            this._upload = upload;
            this._context = context;
        }

        public async Task AddImage(PropertyPhotos entity)
        {
            var filePath = await _upload.SaveImageAsync(entity.PhotoFile);
            entity.PhotoPath = filePath;
            await _context.PropertiesPhotos.AddAsync(entity);
        }

        public async Task AddImages(IEnumerable<PropertyPhotos> entities)
        {
            foreach(PropertyPhotos entity in entities)
            {
                var filePath = await _upload.SaveImageAsync(entity.PhotoFile);
                entity.PhotoPath = filePath;
            }
            await _context.PropertiesPhotos.AddRangeAsync(entities);
        }
    }
}

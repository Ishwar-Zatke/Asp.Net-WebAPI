﻿using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using NZWalksAPI.Data;
using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Repositories
{
    public class SQLWalkRepository: IWalkRepository
    {
        private readonly NZWalksDbContext dbContext;

        public SQLWalkRepository(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Walk> CreateAsync(Walk walk)
        {
            await dbContext.Walks.AddAsync(walk);
            await dbContext.SaveChangesAsync();
            return walk;
        }
        public async Task<List<Walk>> GetAllAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 1000)
        {
            var walks = dbContext.Walks.Include("Difficulty").Include("Region").AsQueryable();
            if(string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery)== false)
            {
                if(filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(x => x.Name.Contains(filterQuery));
                }
            }

            if(string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if(sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(x=>x.Name): walks.OrderByDescending(x=>x.Name);
                }
                else if (sortBy.Equals("Length", StringComparison.OrdinalIgnoreCase)) 
                {
                    walks = isAscending ? walks.OrderBy(x => x.Name) : walks.OrderByDescending(x => x.Name);
                }
            }

            var skipResults = (pageNumber - 1) * pageSize;

            //var walks = await dbContext.Walks.Include("Difficulty").Include("Region").ToListAsync();
            return await walks.Skip(skipResults).Take(pageSize).ToListAsync(); ;
        }

        public async Task<Walk> GetByIdAsync(Guid id)
        {
            var walk = await dbContext.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(x=>x.Id == id);
            return walk;
        }

        public async Task<Walk?> UpdateAsync(Guid id, Walk walk)
        {
            var existingWalk = await dbContext.Walks.FindAsync(id);
            if(existingWalk == null)
            {
                return null;
            }
            existingWalk.Name = walk.Name;
            existingWalk.Description = walk.Description;
            existingWalk.LengthInKm = walk.LengthInKm;
            existingWalk.WalkImageUrl = walk.WalkImageUrl;
            existingWalk.DifficultyId = walk.DifficultyId;
            existingWalk.RegionId = walk.RegionId;
            await dbContext.SaveChangesAsync();
            return existingWalk;

        }

        public async Task<Walk?> DeleteAsync(Guid id)
        {
            var walk = dbContext.Walks.FirstOrDefault(x => x.Id == id);
            if(walk == null)
            {
                return null;
            }
            dbContext.Walks.Remove(walk);
            await dbContext.SaveChangesAsync();
            return walk;
        }
    }
}

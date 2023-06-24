﻿using Microsoft.EntityFrameworkCore;
using VeterinarySystem.Data;
using VeterinarySystem.Models.Db;
using VeterinarySystem.Repository.IRepository;

namespace VeterinarySystem.Repository
{
    public class AnimalRepository : Repository<Animal>, IAnimalRepository
    {
        private VeterinarySystemContext _context;
        public AnimalRepository(VeterinarySystemContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<Animal> GetAllWithData(string? whereString)
        {
            if (!string.IsNullOrEmpty(whereString))
            {
                return _context.Animals
                      .Include(a => a.Breed)
                      .Include(a => a.Client)
                      .Where(e => e.Name.Contains(whereString)).ToList();
            }
            else
            {
                return _context.Animals
                      .Include(a => a.Breed)
                      .Include(a => a.Client).ToList();
            }
        }

        public Animal? GetWithAllData(int id)
        {
            return _context.Animals
                .Include(a => a.Breed)
                   .Include(a => a.Client)
                   .Include(a => a.Weights)
                   .Where(e => e.Id == id)
                   .FirstOrDefault();
        }
    }
}
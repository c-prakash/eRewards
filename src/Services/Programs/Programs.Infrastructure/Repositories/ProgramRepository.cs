using ezloyalty.Services.Programs.Domain.ProgramsAggregate;
using ezloyalty.Services.Programs.Domain.Seedwork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ezloyalty.Services.Programs.Infrastructure.Repositories
{

    public class ProgramRepository
        : IProgramRepository
    {
        private readonly ProgramDbContext _context;

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        public ProgramRepository(ProgramDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Program> Add(Program program)
        {
            var result = _context.Programs.Add(program).Entity;
            await _context.SaveChangesAsync();

            return result;
        }

         

        public async Task<Program> GetAsync(int programId)
        {
            var action = await _context.Programs.FirstOrDefaultAsync(o => o.Id == programId);
            
            if (action == null)
            {
                action = _context
                            .Programs
                            .Local
                            .FirstOrDefault(o => o.Id == programId);
            }
            
            return action;

        }

        public async Task Update(Program program)
        {
            _context.Entry(program).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}

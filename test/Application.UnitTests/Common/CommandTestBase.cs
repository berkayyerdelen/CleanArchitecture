using System;
using AutoMapper;
using Persistence;

namespace Application.UnitTests.Common
{
    public class CommandTestBase:IDisposable
    {
        protected readonly ApplicationDbContext _context;
      

        public CommandTestBase()
        {
            _context = ApplicationContextFactory.Create();
           
        }
       
        public void Dispose()
        {
            ApplicationContextFactory.Destroy(_context);
        }
        
    }
}
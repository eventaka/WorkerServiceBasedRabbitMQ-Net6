using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WorkerServiceBasedRabbitMQ.Core.Dto;
using WorkerServiceBasedRabbitMQ.Core.Interfaces;
using WorkerServiceBasedRabbitMQ.Core.Models;
using WorkerServiceBasedRabbitMQ.Infrastructure.DbContexts;

namespace WorkerServiceBasedRabbitMQ.Infrastructure.Repositories
{
    public class CreateUserRepository : ICreateUserRepository
    {

        private readonly IDbContextFactory<AppDbContext> _contextFactory;
       
        

        public CreateUserRepository(IDbContextFactory<AppDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
       
        }

       
        public async Task CreateUserAsync(User user)
        {


            using (var context = _contextFactory.CreateDbContext())
            {

               context.User.Add(user);
                await context.SaveChangesAsync();
       

            }


        }
    }
}

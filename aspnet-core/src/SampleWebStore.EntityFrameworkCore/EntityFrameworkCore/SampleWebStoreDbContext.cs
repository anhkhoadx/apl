using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using SampleWebStore.Authorization.Roles;
using SampleWebStore.Authorization.Users;
using SampleWebStore.MultiTenancy;

namespace SampleWebStore.EntityFrameworkCore
{
    public class SampleWebStoreDbContext : AbpZeroDbContext<Tenant, Role, User, SampleWebStoreDbContext>
    {
        /* Define a DbSet for each entity of the application */
        
        public SampleWebStoreDbContext(DbContextOptions<SampleWebStoreDbContext> options)
            : base(options)
        {
        }
    }
}

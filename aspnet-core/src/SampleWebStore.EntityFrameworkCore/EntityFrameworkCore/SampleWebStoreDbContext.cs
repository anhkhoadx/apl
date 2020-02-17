using Abp.Zero.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SampleWebStore.Authorization.Roles;
using SampleWebStore.Authorization.Users;
using SampleWebStore.Categories;
using SampleWebStore.MultiTenancy;
using SampleWebStore.Products;
using SampleWebStore.Shops;

namespace SampleWebStore.EntityFrameworkCore
{
	public class SampleWebStoreDbContext : AbpZeroDbContext<Tenant, Role, User, SampleWebStoreDbContext>
    {
		public DbSet<Category> Categories { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<Shop> Shops { get; set; }

		public SampleWebStoreDbContext(DbContextOptions<SampleWebStoreDbContext> options)
            : base(options)
        {
        }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Shop>()
				.HasIndex(b => b.Name);

			modelBuilder.Entity<Product>()
				.HasIndex(b => b.Name);
		}
    }
}

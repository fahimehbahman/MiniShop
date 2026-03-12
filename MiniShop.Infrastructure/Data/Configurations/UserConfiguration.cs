using Microsoft.EntityFrameworkCore;
using MiniShop.Domain.Entities;


namespace MiniShop.Infrastructure.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(x => x.UserId);
            builder.Property(x => x.UserName).IsRequired().HasMaxLength(100); ;
            builder.Property(x => x.Role).IsRequired();
        }
    }
}

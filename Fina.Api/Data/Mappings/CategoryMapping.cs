using Fina.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fina.Api.Data.Mappings
{
    public class CategoryMapping : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Tb_Category");
            builder.HasKey(x => x.Id);  
            builder.Property(t=>t.Title).IsRequired().HasColumnType("nvarchar").HasMaxLength(80);  
            builder.Property(t=>t.Description).IsRequired(false).HasColumnType("nvarchar").HasMaxLength(255);
            
            builder.Property(c=>c.UserId).IsRequired().HasColumnType("varchar").HasMaxLength(160);
        }
    }
    public class TransactionMapping : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.ToTable("Tb_Tansaction");
            builder.HasKey(x => x.Id);
            builder.Property(t => t.Title).IsRequired().HasColumnType("nvarchar").HasMaxLength(80);
            builder.Property(t => t.Type).IsRequired().HasColumnType("smallint");
            builder.Property(t => t.Amount).IsRequired().HasColumnType("money");
            builder.Property(t => t.CreatedAt).IsRequired();
            builder.Property(t => t.PaidOrRecievedAt).IsRequired(false);

            builder.Property(c => c.UserId).IsRequired().HasColumnType("varchar").HasMaxLength(160);
        }
    }
}

using Data.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Infrastructure.Contexts;

public class QuoteContext: DbContext
{
    public QuoteContext(DbContextOptions<QuoteContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Quote>(entity =>
        {
            entity.Property(q => q.QuoteId)
                .HasColumnName("id")
                .ValueGeneratedOnAdd()
                .IsRequired();            
            
            entity.Property(q => q.BreakdownId)
                .HasColumnName("fk_breakdownId")
                .IsRequired();
            
            entity.Property(q => q.QuoteNumber)
                .HasColumnName("number")
                .IsRequired();
            
            entity.Property(q => q.QuotedName)
                .HasColumnName("name")
                .IsRequired();
            
            entity.Property(q => q.ContactName)
                .IsRequired();
           
            entity.Property(q => q.ContactEmail)
                .IsRequired();
            
            entity.Property(q => q.QuoteDate)
                .HasColumnName("date")
                .HasDefaultValueSql("now()")
                .IsRequired();
            //TODO: add raw sql to migrations to update date column with now on update
            
            entity.Property(q => q.QuotePrimaryType)
                .HasColumnName("type")
                .IsRequired();
            
            entity.Property(q => q.QuoteSecondaryType)
                .HasColumnName("secondType");
            
            entity.Property(q => q.QuoteCustomerExtId)
                .HasColumnName("fk_customer");
            
            entity.HasOne<CustomerExt>(q => q.QuoteCustomerExt)
                .WithMany(c => c.Quotes)
                .HasForeignKey(q => q.QuoteCustomerExtId)
                .IsRequired();
            
            entity.Property(q => q.QuoteCustomerMOId)
                .HasColumnName("fk_mo");
            
            entity.HasOne<CustomerMO>(q => q.QuoteCustomerMo)
                .WithMany(c => c.Quotes)
                .HasForeignKey(q => q.QuoteCustomerMOId)
                .IsRequired();

            entity.Property(q => q.QuoteExpiration)
                .HasColumnName("expires")
                .IsRequired();
            
            entity.HasKey(q => q.QuoteId);
        });
        
        modelBuilder.Entity<CustomerExt>(entity =>
        {
            entity.Property(c => c.CustomerId)
                .HasColumnName("id")
                .ValueGeneratedOnAdd()
                .IsRequired();
            entity.HasKey(c => c.CustomerId);
            
            entity.Property(c => c.CustomerName)
                .HasColumnName("name")
                .IsRequired();
            
            entity.Property(c => c.CustomerLocation)
                .HasConversion(
                    l => JsonConvert.SerializeObject(l, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }),
                    l => JsonConvert.DeserializeObject<Location>(l, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }))
                .HasColumnName("location")
                .IsRequired();
            
            entity.Property(c => c.CustomerCountry)
                .HasColumnName("country")
                .IsRequired();
        });        
        
        modelBuilder.Entity<CustomerMO>(entity =>
        {
            entity.Property(c => c.CustomerId)
                .HasColumnName("id")
                .ValueGeneratedOnAdd()
                .IsRequired(); 
            entity.HasKey(c => c.CustomerId);
            
            entity.Property(c => c.CustomerName)
                .HasColumnName("name")
                .IsRequired();
            
            entity.Property(c => c.CustomerLocation)
                .HasConversion(
                    l => JsonConvert.SerializeObject(l, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }),
                    l => JsonConvert.DeserializeObject<Location>(l, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }))
                .HasColumnName("location")
                .IsRequired();
            
            entity.Property(c => c.CustomerCountry)
                .HasColumnName("country")
                .IsRequired();
        });
        
        modelBuilder.Entity<LineItem>(entity =>
        {
            entity.Property(i => i.LineItemId)
                .HasColumnName("id")
                .ValueGeneratedOnAdd()
                .IsRequired();
            entity.HasKey(i => i.LineItemId);

            entity.Property(i => i.PartNumber)
                .HasMaxLength(100)
                .IsRequired();
            
            entity.Property(i => i.Quantity)
                .IsRequired();
            
            entity.Property(i => i.ItemPrice)
                .HasColumnType("money")
                .IsRequired();
            
            entity.Property(i => i.Description)
                .HasMaxLength(2000)
                .IsRequired();
            
            entity.Property(i => i.Conditions)
                .HasMaxLength(1000);
            
            entity.Property(i => i.ClassDesignation)
                .HasColumnType("enum")
                .IsRequired();
            
            entity.Property(i => i.LeadTime)
                .HasColumnType("interval")
                .IsRequired();
            
            entity.Property(i => i.QuoteId)
                .HasColumnName("fk_quoteId");
            entity.HasOne<Quote>(i => i.Quote)
                .WithMany(q => q.LineItems)
                .HasForeignKey(i => i.QuoteId);
        });        
        
        modelBuilder.Entity<AuditData>(entity =>
        {
            entity.Property(a => a.AuditId)
                .HasColumnName("id")
                .ValueGeneratedOnAdd()
                .IsRequired();
            entity.HasKey(a => a.AuditId);

            entity.Property(a => a.EditDate)
                .HasDefaultValueSql("now()")
                .IsRequired();
            
            entity.Property(a => a.EditorName)
                .IsRequired();
            
            entity.HasMany<ChangeRecord>(a => a.Changes);
            entity.HasOne<Quote>(a => a.Quote)
                .WithMany(q => q.Revisions)
                .HasForeignKey(i => i.QuoteId);
        });

        modelBuilder.Entity<ChangeRecord>(entity =>
        {
            entity.Property(a => a.AuditId)
                .HasColumnName("id")
                .ValueGeneratedOnAdd()
                .IsRequired();
            entity.HasKey(r => r.ChangeId);
            
            entity.Property(r => r.FieldName)
                .IsRequired();
            
            entity.Property(r => r.OldValue)
                .IsRequired();
            
            entity.Property(r => r.NewValue)
                .IsRequired();
            
            entity.HasOne<AuditData>(r => r.Audit)
                .WithMany(a => a.Changes)
                .HasForeignKey(r => r.AuditId);
        });
    }

    public DbSet<Quote> Quotes { get; set; }
    public DbSet<CustomerExt> CustomerExts { get; set; }
    public DbSet<CustomerMO> CustomerMOs { get; set; }
    public DbSet<AuditData> Audits { get; set; }
    public DbSet<LineItem> LineItems { get; set; }
    public DbSet<ChangeRecord> Changes { get; set; }
}
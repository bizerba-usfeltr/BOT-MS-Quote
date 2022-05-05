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

    protected QuoteContext()
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresEnum<Class>();
        modelBuilder.HasPostgresEnum<QuoteType>();
        
        CreateQuote(modelBuilder);
        CreateCustomer(modelBuilder);
        CreateLineItem(modelBuilder);
        CreateBreakdownItem(modelBuilder);
        CreateAuditData(modelBuilder);
        CreateChangeRecord(modelBuilder);
    }

    private void CreateQuote(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Quote>(entity =>
        {
            entity.Property(q => q.QuoteId)
                .HasColumnName("id")
                .ValueGeneratedOnAdd()
                .IsRequired();

            entity.Property(q => q.QuoteNumber)
                .HasColumnName("number")
                .IsRequired();
            
            entity.Property(q => q.QuoteCreatedBy)
                .HasColumnName("name")
                .IsRequired();            
            
            entity.Property(q => q.SalePersonName)
                .HasColumnName("sales_name")
                .IsRequired();
            
            entity.Property(q => q.ContactName)
                .IsRequired();
           
            entity.Property(q => q.ContactEmail)
                .IsRequired();
            
            entity.Property(q => q.QuoteDate)
                .HasColumnName("date")
                .HasDefaultValueSql("CURRENT_DATE")
                .IsRequired();
            //TODO: add raw sql to migrations to update date column with now on update
            
            entity.Property(q => q.Description)
                .HasMaxLength(2000)
                .IsRequired();
            
            entity.Property(q => q.Conditions)
                .HasMaxLength(1000);
            
            entity.Property(q => q.LeadTime)
                .HasMaxLength(1000)
                .IsRequired();
            
            entity.Property(q => q.QuotePrimaryType)
                .HasColumnType("quote_type")
                .HasColumnName("type")
                .IsRequired();
            
            entity.Property(q => q.QuoteSecondaryType)
                .HasColumnType("quote_type")
                .HasColumnName("second_type");
            
            entity.Property(q => q.QuoteCustomerExtId)
                .HasColumnName("fk_customer");
            
            entity.HasOne(q => q.QuoteCustomerExt)
                .WithMany(c => c.ExtQuotes)
                .HasForeignKey(q => q.QuoteCustomerExtId)
                .IsRequired();
            
            entity.Property(q => q.QuoteCustomerMOId)
                .HasColumnName("fk_mo");
            
            entity.HasOne(q => q.QuoteCustomerMo)
                .WithMany(c => c.MOQuotes)
                .HasForeignKey(q => q.QuoteCustomerMOId)
                .IsRequired();

            entity.Property(q => q.QuoteExpiration)
                .HasColumnName("expires")
                .HasDefaultValueSql("CURRENT_DATE + INTERVAL '90 days'")
                .IsRequired();
                                    
            entity.Property(q => q.BreakdownId)
                .HasColumnName("fk_breakdownId")
                .IsRequired();
                
            entity.HasKey(q => q.QuoteId);
            
            entity.HasIndex(q=>q.QuoteNumber)
                .IsUnique();
        });
    }   
    private void CreateLineItem(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<LineItem>(entity =>
        {
            entity.Property(i => i.LineItemId)
                .HasColumnName("id")
                .ValueGeneratedOnAdd()
                .IsRequired();
            entity.HasKey(i => i.LineItemId);

            entity.Property(i => i.LineNumber)
                .IsRequired();
            
            entity.Property(i => i.PartNumber)
                .HasMaxLength(100)
                .IsRequired();
            
            entity.Property(i => i.Quantity)
                .IsRequired();
            
            entity.Property(i => i.ItemPrice)
                .HasColumnType("money")
                .IsRequired();

            entity.Property(i => i.ClassDesignation)
                .HasColumnType("class")
                .IsRequired();

            entity.Property(i => i.QuoteId)
                .HasColumnName("fk_quoteId");
            entity.HasOne(i => i.Quote)
                .WithMany(q => q.LineItems)
                .HasForeignKey(i => i.QuoteId);
        });
    }   
    private void CreateCustomer(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
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
    }    
   
    private void CreateBreakdownItem(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BreakdownItem>(entity =>
        {
            entity.Property(b => b.Id)
                .IsRequired();
            entity.HasKey(b => b.Id);

            entity.Property(b => b.Description)
                .HasMaxLength(200)
                .IsRequired();
            entity.Property(b => b.Total)
                .HasColumnType("money")
                .IsRequired();
            entity.Property(b => b.LineItemId)
                .HasColumnName("fk_lineitemId");
            entity.HasOne(b => b.LineItem)
                .WithMany(i => i.BreakdownItems)
                .HasForeignKey(b => b.LineItemId);
        });
    }    
    private void CreateAuditData(ModelBuilder modelBuilder)
    {
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
            
            entity.HasMany(a => a.Changes);
            entity.HasOne(a => a.Quote)
                .WithMany(q => q.Revisions)
                .HasForeignKey(i => i.QuoteId);
        }); 
    }    
    private void CreateChangeRecord(ModelBuilder modelBuilder)
    {
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
            
            entity.HasOne(r => r.Audit)
                .WithMany(a => a.Changes)
                .HasForeignKey(r => r.AuditId);
        });
    }
    public DbSet<Quote> Quotes { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<AuditData> Audits { get; set; }
    public DbSet<LineItem> LineItems { get; set; }
    public DbSet<LineItem> BreakdownItems { get; set; }
    public DbSet<ChangeRecord> Changes { get; set; }
}
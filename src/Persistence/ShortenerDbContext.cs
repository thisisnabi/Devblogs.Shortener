namespace Devblogs.Shortener.Data;

public class ShortenerDbContext(DbContextOptions<ShortenerDbContext> dbContextOptions) 
    : DbContext(dbContextOptions)
{
    public const string DefaultSchema = "shortener";
    public const string ConnectionStringName = "SvcDbContext";
     
    public DbSet<Tag> Tags => Set<Tag>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    { 
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Tag>(link =>
        {
            link.ToTable(Tag.TableName, DefaultSchema);
            link.HasKey(x => x.Id);

            link.Property(x => x.ShortCode)
                .HasMaxLength(20)
                .IsRequired();

            link.Property(x => x.LongUrl)
                .HasMaxLength(2083)
                .IsRequired();

            link.HasIndex(x => x.ShortCode)
                    .IsUnique(true);

            link.HasIndex(x => x.LongUrl)
                    .IsUnique(true);
        });
    }
}

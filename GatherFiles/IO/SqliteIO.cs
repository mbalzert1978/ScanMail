using GatherFiles.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace GatherFiles.IO;

public class SqliteIO : DbContext, IDataBaseInteractor {
    public required DbSet<FileContentDto> FileContent { get; set; }
    public required DbSet<UnprocessedFiles> Unprocessed { get; set; }
    private IDbContextTransaction? _transaction;

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.Entity<FileContentDto>(entity => {
            entity.ToTable("FileContent");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Path).IsRequired();
            entity.Property(e => e.Content).IsRequired();
        });

        modelBuilder.Entity<UnprocessedFiles>(entity => {
            entity.ToTable("Unprocessed");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Files).IsRequired();
            entity.Property(e => e.IsRead).HasDefaultValue(false);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.ProcessedAt);
        });
    }

    public async Task AddRangeAsync(UnprocessedDto unprocessed) {
        _transaction = await Database.BeginTransactionAsync();
        await FileContent.AddRangeAsync(unprocessed.Files);
        await Unprocessed.AddAsync(UnprocessedFiles.From(unprocessed));
    }

    public async Task SaveAsync() {
        if (_transaction is null) {
            throw new InvalidOperationException("No active transaction.");
        }
        try {
            await SaveChangesAsync();
            await _transaction!.CommitAsync();
        } catch (Exception) {
            await _transaction!.RollbackAsync();
            throw;
        } finally {
            await _transaction!.DisposeAsync();
        }
    }
}

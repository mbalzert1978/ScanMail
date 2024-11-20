using GatherFiles.IO;

namespace GatherFiles.Abstractions;

public interface IDataBaseInteractor {
    Task AddRangeAsync(UnprocessedDto unprocessed);
    Task SaveAsync();
}

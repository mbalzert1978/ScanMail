using GatherFiles.Contracts;
using RustyOptions;

namespace GatherFiles.Abstractions;

public interface IUnprocessedRepository {
    public Task<Result<Unit, GatherError>> AddRangeAsync(Unprocessed unprocessed);

    public Task<Result<Unit, GatherError>> SaveAsync();
}

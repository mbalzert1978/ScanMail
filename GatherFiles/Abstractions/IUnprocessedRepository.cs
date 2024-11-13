using GatherFiles.Contracts;
using RustyOptions;

namespace GatherFiles.Abstractions;

public interface IUnprocessedRepository {
    public void Add(Unprocessed unprocessed);

    public Task<Result<Unit, Exception>> SaveAsync();
}

using GatherFiles.Abstractions;
using GatherFiles.Contracts;
using RustyOptions;
using RustyOptions.Async;
using static RustyOptions.Result;

namespace GatherFiles.UseCase;

// var dbContext = new SqliteIO();
// var repository = new DatabaseAdapter(dbContext);
// var reader = new SystemFileWrapper();
// var adapter = new FileAdapter(reader);
// var interactor = new GatherInteractor(repository, adapter);
public class GatherController(IUnprocessedRepository repository, IInteractor interactor) {
    public async Task<Result<Unit, GatherError>> GatherFilesAsync(
        GatherRequestFrom request,
        CancellationToken cancellationToken = default
    ) =>
        await interactor
            .Handle(request, cancellationToken)
            .AndThenAsync(repository.AddRangeAsync)
            .AndThenAsync(_ => repository.SaveAsync());
}

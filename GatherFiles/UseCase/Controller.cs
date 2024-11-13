using GatherFiles.Abstractions;
using GatherFiles.Contracts;
using RustyOptions;
using RustyOptions.Async;
using static RustyOptions.Result;

namespace GatherFiles.UseCase;

public class GatherController(IUnprocessedRepository repository, IInteractor interactor) {
    public async Task<Result<Unprocessed, GatherError>> GatherFilesAsync(
        string source,
        CancellationToken cancellationToken = default
    ) {
        var request = new GatherRequestFrom(new Uri(source));
        // var reader = new SystemFileWrapper();
        // var adapter = new FileAdapter(reader);
        // var interactor = new GatherInteractor(adapter);

        return await interactor
            .Handle(request, cancellationToken)
            .AndThenAsync(unprocessed => {
                repository.Add(unprocessed);
                return repository
                    .SaveAsync()
                    .MapAsync(_ => unprocessed)
                    .OrElseAsync(exc =>
                        Err<Unprocessed, GatherError>(GatherError.FromString(exc!.Message))
                    );
            });
    }
}

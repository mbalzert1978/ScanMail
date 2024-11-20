using GatherFiles.Abstractions;
using GatherFiles.Contracts;
using GatherFiles.IO;
using Microsoft.EntityFrameworkCore;
using RustyOptions;

namespace GatherFiles.Adapter;

public class DatabaseAdapter(IDataBaseInteractor database) : IUnprocessedRepository {
    private readonly IDataBaseInteractor _database = database;

    public async Task<Result<Unit, GatherError>> AddRangeAsync(Unprocessed unprocessed) {
        try {
            await _database.AddRangeAsync(UnprocessedDto.From(unprocessed));
        } catch (OperationCanceledException exc) {
            return Result.Err<Unit, GatherError>(GatherError.From(exc.Message));
        }
        return Result.Ok<Unit, GatherError>(new Unit());
    }

    public async Task<Result<Unit, GatherError>> SaveAsync() {
        try {
            await _database.SaveAsync();
        } catch (InvalidOperationException exc) {
            return Result.Err<Unit, GatherError>(GatherError.From(exc.Message));
        } catch (OperationCanceledException exc) {
            return Result.Err<Unit, GatherError>(GatherError.From(exc.Message));
        } catch (DbUpdateConcurrencyException exc) {
            return Result.Err<Unit, GatherError>(GatherError.From(exc.Message));
        } catch (DbUpdateException exc) {
            return Result.Err<Unit, GatherError>(GatherError.From(exc.Message));
        } catch (Exception exc) {
            return Result.Err<Unit, GatherError>(GatherError.From(exc.Message));
        }
        return Result.Ok<Unit, GatherError>(new Unit());
    }
}

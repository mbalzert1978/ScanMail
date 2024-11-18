using GatherFiles.Abstractions;
using GatherFiles.Contracts;
using RustyOptions;
using static GatherFiles.Contracts.GatherError;
using static RustyOptions.Result;

namespace GatherFiles.Adapter;

public class FileAdapter(IReader reader) : IFileReader {
    private const string PATH_EMPTY = "File path cannot be empty";
    private const string ACCESS_DENIED = "Access denied: {0}. {1}";
    private const string PATH_TOO_LONG = "Path is too long: {0}. {1}";
    private const string DIRECTORY_NOT_FOUND = "Directory not found: {0}. {1}";
    private const string IO_ERROR = "I/O error occurred: {0}. {1}";
    private const string UNEXPECTED_ERROR =
        "Unexpected error occurred while {0} the file: {1}. {2}";

    public async Task<Result<ICollection<FileData>, GatherError>> ReadFilesAsync(
        Uri @Path,
        CancellationToken cancellationToken = default
    ) {
        if (@Path == null) {
            return Err<ICollection<FileData>, GatherError>(FromString(PATH_EMPTY));
        }
        if (!reader.DirectoryExists(@Path.LocalPath)) {
            return Err<ICollection<FileData>, GatherError>(
                FromString(DIRECTORY_NOT_FOUND, @Path.LocalPath)
            );
        }
        try {
            return Ok<ICollection<FileData>, GatherError>(
                await Task.WhenAll(
                    reader
                        .EnumerateFiles(@Path.LocalPath)
                        .Select(async file => new FileData(
                            file,
                            await reader.ReadAllBytesAsync(file, cancellationToken)
                        ))
                )
            );
        } catch (UnauthorizedAccessException ex) {
            return Err<ICollection<FileData>, GatherError>(
                FromString(ACCESS_DENIED, @Path.LocalPath, ex.Message)
            );
        } catch (System.Security.SecurityException ex) {
            return Err<ICollection<FileData>, GatherError>(
                FromString(ACCESS_DENIED, @Path.LocalPath, ex.Message)
            );
        } catch (PathTooLongException ex) {
            return Err<ICollection<FileData>, GatherError>(
                FromString(PATH_TOO_LONG, @Path.LocalPath, ex.Message)
            );
        } catch (IOException ex) {
            return Err<ICollection<FileData>, GatherError>(
                FromString(IO_ERROR, @Path.LocalPath, ex.Message)
            );
        } catch (Exception ex) {
            return Err<ICollection<FileData>, GatherError>(
                FromString(UNEXPECTED_ERROR, "reading", @Path.LocalPath, ex.Message)
            );
        }
    }
}

using FileHandler.Abstractions;
using Result;
using static Result.Factories;

namespace FileHandler.Adapter;

public class FileAdapter(IReader reader, IWriter writer) : IFileReader, IFileWriter {
    private const string PATH_EMPTY = "File path cannot be empty";
    private const string FILE_NOT_FOUND = "File not found: {0}";
    private const string ACCESS_DENIED = "Access denied: {0}. {1}";
    private const string PATH_TOO_LONG = "Path is too long: {0}. {1}";
    private const string DIRECTORY_NOT_FOUND = "Directory not found: {0}. {1}";
    private const string IO_ERROR = "I/O error occurred: {0}. {1}";
    private const string UNEXPECTED_ERROR =
        "Unexpected error occurred while {0} the file: {1}. {2}";
    private const string BYTES_NULL = "Bytes cannot be null: {0}";
    private const string BYTES_EMPTY = "Bytes cannot be empty: {0}";
    private const string INVALID_DIRECTORY_PATH = "Invalid directory path: {0}";

    public async Task<Result<byte[]>> ReadFile(
        Uri filePath,
        CancellationToken cancellationToken = default
    ) {
        if (filePath == null) {
            return Err<byte[]>(PATH_EMPTY);
        }
        if (!reader.Exists(filePath.LocalPath)) {
            return Err<byte[]>(FILE_NOT_FOUND, filePath.LocalPath);
        }

        try {
            return Ok(await reader.ReadBytesAsync(filePath.LocalPath, cancellationToken));
        } catch (UnauthorizedAccessException ex) {
            return Err<byte[]>(ACCESS_DENIED, filePath.LocalPath, ex.Message);
        } catch (PathTooLongException ex) {
            return Err<byte[]>(PATH_TOO_LONG, filePath.LocalPath, ex.Message);
        } catch (DirectoryNotFoundException ex) {
            return Err<byte[]>(DIRECTORY_NOT_FOUND, filePath.LocalPath, ex.Message);
        } catch (IOException ex) {
            return Err<byte[]>(IO_ERROR, filePath.LocalPath, ex.Message);
        } catch (Exception ex) {
            return Err<byte[]>(UNEXPECTED_ERROR, "reading", filePath.LocalPath, ex.Message);
        }
    }

    public async Task<Result<Unit>> WriteFile(
        Uri filePath,
        byte[] data,
        CancellationToken cancellationToken = default
    ) {
        if (filePath == null) {
            return Err<Unit>(PATH_EMPTY);
        }
        if (data is null) {
            return Err<Unit>(BYTES_NULL, filePath.LocalPath);
        }
        if (data.Length == 0) {
            return Err<Unit>(BYTES_EMPTY, filePath.LocalPath);
        }
        try {
            string? dir = writer.GetDirectoryName(filePath.LocalPath);

            if (dir is null)
                return Err<Unit>(INVALID_DIRECTORY_PATH, filePath.LocalPath);

            if (!writer.DirectoryExists(dir))
                writer.CreateDirectory(dir);

            await writer.WriteAllBytesAsync(filePath.LocalPath, data, cancellationToken);
        } catch (UnauthorizedAccessException ex) {
            return Err<Unit>(ACCESS_DENIED, filePath.LocalPath, ex.Message);
        } catch (PathTooLongException ex) {
            return Err<Unit>(PATH_TOO_LONG, filePath.LocalPath, ex.Message);
        } catch (DirectoryNotFoundException ex) {
            return Err<Unit>(DIRECTORY_NOT_FOUND, filePath.LocalPath, ex.Message);
        } catch (IOException ex) {
            return Err<Unit>(IO_ERROR, filePath.LocalPath, ex.Message);
        } catch (Exception ex) {
            return Err<Unit>(UNEXPECTED_ERROR, "writing", filePath.LocalPath, ex.Message);
        }
        return Ok();
    }
}

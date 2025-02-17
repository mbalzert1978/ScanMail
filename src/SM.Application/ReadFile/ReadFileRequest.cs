using System;

using MediatR;

namespace SM.Application.ReadFile;

public sealed record ReadFileRequest(string Path) : IRequest;
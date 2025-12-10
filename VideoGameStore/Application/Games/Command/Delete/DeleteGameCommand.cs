using MediatR;
using VideoGameStore.Application.Interfaces;
using VideoGameStore.Domain.common;
using VideoGameStore.Domain.Entities;

namespace VideoGameStore.Application.Games.Command.Delete;

public record DeleteGameCommand(int id):ICommand<Result<Game>>;

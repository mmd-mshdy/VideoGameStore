using MediatR;
using VideoGameStore.Application.Interfaces;

namespace VideoGameStore.Application.Games.Command.Delete;

public record DeleteGameCommand(int id):ICommand<string>;

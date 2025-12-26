using Humanizer.Localisation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using VideoGameStore.Domain.Entities;
using VideoGameStore.Infrastructure.Data;

namespace VideoGameStore.Infrastructure;

public class SaveChangesInterceptor : ISaveChangesInterceptor
{
    public InterceptionResult<int> SavingChanges
        (
        DbContextEventData eventData,
        InterceptionResult<int> Result
        )
    {
        var context = eventData.Context as VideoGamesContext;
        if (context is null) return Result;
        var tracker = context.ChangeTracker;
        var deleted = tracker.Entries<Game>()
            .Where(entry => entry.State == EntityState.Deleted);
        foreach (var entrydeleted in deleted)
        {
            entrydeleted.Property<bool>("IsAvailable").CurrentValue = true;
            entrydeleted.State = EntityState.Modified;
        }
        return Result;
    }

    public ValueTask<InterceptionResult<int>> SavingChangesAsync
      (
      DbContextEventData eventData,
      InterceptionResult<int> Result,
      CancellationToken cancellationToken = default
      )
    {
        return ValueTask.FromResult(SavingChanges(eventData, Result));
    }
}

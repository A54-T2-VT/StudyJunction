using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using StudyJunction.Infrastructure.Data.Models.Contracts;

namespace StudyJunction.Infrastructure.Interceptors
{
    internal class SoftDeletionInterceptor : SaveChangesInterceptor
    {
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            if (eventData.Context is null) return result;

            foreach (var entry in eventData.Context.ChangeTracker.Entries())
            {
                if (entry is not { State: EntityState.Deleted, Entity: ISoftDelete delete }) continue;

                entry.State = EntityState.Modified;
                delete.IsDeleted = true;
                delete.DeletedOn = DateTime.Now;

            }

            return result;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using EFDisconnectedSample.Model;

namespace EFDisconnectedSample.Data
{
    public class CinemaContext :DbContext
    {
        public DbSet<Cinema> Cinemas { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Showing> Showings { get; set; }
        public DbSet<Film> Films { get; set; }

        public CinemaContext()
        {
            //Disabling LazyLoading and ProxyCreation to make this 
            //the most basic disconnected example possible
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;

            //As entitys leave the database set their state to Unchanged
            ((IObjectContextAdapter)this).ObjectContext.ObjectMaterialized += (sender, args) =>
            {
                var entity = args.Entity as IStateTracker;
                if (entity != null)
                {
                    entity.State = State.Unchanged;
                    entity.ModifiedProperties = new List<string>();
                }
            };
        }

        private void PaintState()
        {
            CheckForEntitiesWithoutStateInterface();
            foreach (var entry in ChangeTracker.Entries<IStateTracker>())
            {
                var stateInfo = entry.Entity;
                if (stateInfo.State == State.Unchanged)
                {
                    entry.State = EntityState.Unchanged;
                    foreach (var property in stateInfo.ModifiedProperties)
                    {
                        entry.Property(property).IsModified = true;
                    }
                }
                else
                {
                    entry.State = ConvertState(stateInfo.State);
                }
            }
        }

        public void AttachDisconnectedEntity<TEntity>(TEntity entity) where TEntity : class, IStateTracker
        {
            Set<TEntity>().Attach(entity);
            PaintState();
        }

        public void AddDisconnectedEntity<TEntity>(TEntity entity) where TEntity : class, IStateTracker
        {
            Set<TEntity>().Add(entity);
            PaintState();
        }

        private void CheckForEntitiesWithoutStateInterface()
        {
            var entitiesWithoutState = from e in ChangeTracker.Entries()
                                       where !(e.Entity is IStateTracker)
                                       select e;
            if (entitiesWithoutState.Any())
                throw new NotSupportedException("All entities must implement IStateTracker");
        }

        private EntityState ConvertState(State state)
        {
            switch (state)
            {
                case State.Added:
                    return EntityState.Added;
                case State.Deleted:
                    return EntityState.Deleted;
                case State.Unchanged:
                    return EntityState.Unchanged;
                default:
                    throw new ArgumentOutOfRangeException("Entity State Is Not Set!");
            }
        }
    }


}

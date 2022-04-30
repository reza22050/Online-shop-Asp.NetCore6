﻿using Application.Interfaces.Contexts;
using Domain.Attributes;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Context
{
    public class DataBaseContext:DbContext, IDataBaseContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options):base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<User>().Property<DateTime>("InsertTime");
            //modelBuilder.Entity<User>().Property<DateTime>("UpdateTime");

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (entityType.ClrType.GetCustomAttributes(typeof(AudiTableAttribute),true).Length>0)
                {
                    modelBuilder.Entity(entityType.Name).Property<DateTime>("InsertTime");
                    modelBuilder.Entity(entityType.Name).Property<DateTime?>("UpdateTime");
                    modelBuilder.Entity(entityType.Name).Property<DateTime?>("RemoveTime");
                    modelBuilder.Entity(entityType.Name).Property<bool>("IsRemove");
                    
                }

            }

            base.OnModelCreating(modelBuilder); 
        }

        public override int SaveChanges()
        {
            var modifiedEntries = ChangeTracker.Entries()
                .Where(p => p.State == EntityState.Modified ||
                          p.State == EntityState.Added ||
                          p.State == EntityState.Deleted);

            foreach (var item in modifiedEntries)
            {
                var entityType = item.Context.Model.FindEntityType(item.Entity.GetType());
                
                var Inserted = entityType?.FindProperty("InsertTime");
                var Updated = entityType?.FindProperty("UpdateTime");
                var RemoveTime = entityType?.FindProperty("RemoveTime");
                var IsRemove = entityType?.FindProperty("IsRemove");

                if (item.State == EntityState.Added && Inserted != null)
                {
                    item.Property("InsertTime").CurrentValue= DateTime.Now;
                }

                if (item.State == EntityState.Modified && Updated != null)
                {
                    item.Property("UpdateTime").CurrentValue = DateTime.Now;
                }

                if (item.State == EntityState.Deleted && RemoveTime != null && IsRemove != null)
                {
                    item.Property("RemoveTime").CurrentValue = DateTime.Now;
                    item.Property("IsRemove").CurrentValue = true;
                }

            }   

            return base.SaveChanges();
        }

    }
}
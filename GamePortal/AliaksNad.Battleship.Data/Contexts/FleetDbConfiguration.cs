﻿using AliaksNad.Battleship.Data.Models;
using System.Data.Entity.ModelConfiguration;

namespace AliaksNad.Battleship.Data.Contexts
{
    internal class FleetDbConfiguration : EntityTypeConfiguration<FleetDb>
    {
        public FleetDbConfiguration()
        {
            HasKey(x => x.FleetId).ToTable("Fleets");
            HasMany(x => x.Coordinates).WithOptional(x => x.Fleet).HasForeignKey(c => c.FleetId);
        }
    }
}
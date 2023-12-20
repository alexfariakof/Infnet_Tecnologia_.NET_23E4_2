﻿using Microsoft.EntityFrameworkCore;
using Domain.Account.Agreggates;
using Repository.Mapping.Account;
using Repository.Mapping.Streaming;
using Repository.Mapping.Transactions;

namespace __mock__
{
    public class MockRegisterContext : DbContext
    {
        public MockRegisterContext(DbContextOptions<MockRegisterContext> options) : base(options) {}

        public DbSet<Customer> Custumer{ get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new CustomerMap());
            modelBuilder.ApplyConfiguration(new MerchantMap());
            modelBuilder.ApplyConfiguration(new PlaylistPersonalMap());
            modelBuilder.ApplyConfiguration(new SignitureMap());
            modelBuilder.ApplyConfiguration(new AlbumMap());
            modelBuilder.ApplyConfiguration(new BandMap());
            modelBuilder.ApplyConfiguration(new FlatMap());
            modelBuilder.ApplyConfiguration(new MusicMap());
            modelBuilder.ApplyConfiguration(new MusicPersonalMap());
            modelBuilder.ApplyConfiguration(new PlaylistMap());
            modelBuilder.ApplyConfiguration(new CardMap());
            modelBuilder.ApplyConfiguration(new TransactionMap());
        }
    }
}
using Domain.Account.Agreggates;
using Domain.Notifications;
using Domain.Streaming.Agreggates;
using Domain.Transactions.Agreggates;
using Microsoft.EntityFrameworkCore;
using Repository.Mapping.Account;
using Repository.Mapping.Notifications;
using Repository.Mapping.Streaming;
using Repository.Mapping.Transactions;

namespace Migrations_MySqlServer
{
    public class MySqlContext : DbContext
    {
        public MySqlContext(DbContextOptions<MySqlContext> options) : base(options) { }
        public DbSet<Customer> Custumer { get; set; }
        public DbSet<Merchant> Merchant { get; set; }
        public DbSet<PlaylistPersonal> PlaylistPersonal { get; set; }
        public DbSet<Signature> Signature { get; set; }
        public DbSet<Album> Album { get; set; }
        public DbSet<Band> Band { get; set; }
        public DbSet<Flat> Flat { get; set; }
        public DbSet<Music<Playlist>> Music { get; set; }
        //public DbSet<Music<PlaylistPersonal>> MusicPersonal { get; set; }
        public DbSet<Playlist> Playlist { get; set; }
        public DbSet<Card> Card { get; set; }
        public DbSet<Transaction> Transaction { get; set; }
        public DbSet<Notification> Notification { get; set; }
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
            modelBuilder.ApplyConfiguration(new NotificationMap());
        }
    }
}
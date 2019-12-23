using CinemaChain.API.AuthModel;
using CinemaChain.Models.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaChain.Data.Context
{
    public class DB_Context : IdentityDbContext<AppUser>
    {
        public DB_Context(DbContextOptions options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Films> Films { get; set; }
        public virtual DbSet<Admins> Admins { get; set; }
        public virtual DbSet<Clients> Clients { get; set; }
        public virtual DbSet<Owners> Owners { get; set; }
        public virtual DbSet<Seances> Seances { get; set; }
        public virtual DbSet<Seats> Seats { get; set; }
        public virtual DbSet<BusySeats> BusySeats { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<Cinemas> Cinemas { get; set; }
        public virtual DbSet<FilmImages> FilmImages { get; set; }
        public virtual DbSet<CinemaImages> CinemaImages { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); 

            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new AdminConfiguration());
            modelBuilder.ApplyConfiguration(new OwnerConfiguration());
            modelBuilder.ApplyConfiguration(new ClientConfiguration());
            modelBuilder.ApplyConfiguration(new FilmConfiguration());
            modelBuilder.ApplyConfiguration(new CinemaConfiguration());
            modelBuilder.ApplyConfiguration(new SeanceConfiguration());
            modelBuilder.ApplyConfiguration(new SeatConfiguration());
            modelBuilder.ApplyConfiguration(new BusySeatConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration()); 
            modelBuilder.ApplyConfiguration(new FilmImageConfiguration());
            modelBuilder.ApplyConfiguration(new CinemaImageConfiguration());

            modelBuilder.Entity<Admins>()
            .HasOne(a => a.Users)
            .WithOne(a => a.Admins)
            .HasForeignKey<Users>(c => c.Id);

            modelBuilder.Entity<Clients>()
            .HasOne(a => a.Users)
            .WithOne(a => a.Clients)
            .HasForeignKey<Users>(c => c.Id);

            modelBuilder.Entity<Owners>()
            .HasOne(a => a.Users)
            .WithOne(a => a.Owners)
            .HasForeignKey<Users>(c => c.Id);


        }

        public class UserConfiguration : IEntityTypeConfiguration<Users>
        {
            public void Configure(EntityTypeBuilder<Users> builder)
            {
                builder
                    .ToTable("Users", schema: "dbo")
                    .HasKey(p => p.UserName);

                builder
                   .Property(p => p.UserName).IsRequired().HasMaxLength(50);

                builder
                    .Property(p => p.Password).IsRequired().HasMaxLength(50);

                builder
                    .HasOne(a => a.Admins)
                    .WithOne(o => o.Users)
                    .OnDelete(DeleteBehavior.Restrict);
                builder
                    .HasOne(c => c.Clients)
                    .WithOne(o => o.Users)
                    .OnDelete(DeleteBehavior.Restrict);

                builder
                    .HasOne(w => w.Owners)
                    .WithOne(o => o.Users)
                    .OnDelete(DeleteBehavior.Restrict);


            }
        }

        public class AdminConfiguration : IEntityTypeConfiguration<Admins>
        {
            public void Configure(EntityTypeBuilder<Admins> builder)
            {
                builder
                     .ToTable("Admins", schema: "dbo")
                     .HasKey(p => p.Id);

                //builder
                //   .Property(p => p.Users.Username).IsRequired().HasMaxLength(50);

                builder
                    .Property(p => p.FIO).IsRequired().HasMaxLength(300);

            }
        }
        public class ClientConfiguration : IEntityTypeConfiguration<Clients>
        {
            public void Configure(EntityTypeBuilder<Clients> builder)
            {
                builder
                     .ToTable("Clients", schema: "dbo")
                     .HasKey(p => p.Id);

                //builder
                //   .Property(p => p.Username).IsRequired().HasMaxLength(50);

                builder
                    .Property(p => p.FIO).IsRequired().HasMaxLength(300);

            }
        }
        public class OwnerConfiguration : IEntityTypeConfiguration<Owners>
        {
            public void Configure(EntityTypeBuilder<Owners> builder)
            {
                builder
                     .ToTable("Owners", schema: "dbo")
                     .HasKey(p => p.Id);

                //builder
                //   .Property(p => p.Username).IsRequired().HasMaxLength(50);

                builder
                    .Property(p => p.FIO).IsRequired().HasMaxLength(300);
                //builder
                //    .HasMany(c => c.Cinemas)
                //    .WithOne(o => o.Owners)
                //    .HasForeignKey(p => p.Id)
                //    .OnDelete(DeleteBehavior.Restrict);

            }
        }

        public class FilmConfiguration : IEntityTypeConfiguration<Films>
        {
            public void Configure(EntityTypeBuilder<Films> builder)
            {
                builder
                     .ToTable("Films", schema: "dbo")
                     .HasKey(p => p.Id);

                //builder
                //   .Property(p => p.FilmName).IsRequired().HasMaxLength(50);

                builder
                    .Property(p => p.StartDate).IsRequired();
                builder
                    .Property(p => p.EndDate).IsRequired();
                builder
                    .HasMany(c => c.Seances)
                    .WithOne(o => o.Films)
                    .HasForeignKey(p => p.FilmId)
                    .OnDelete(DeleteBehavior.Restrict);
                builder
                 .HasMany(c => c.FilmImages)
                 .WithOne(o => o.Films)
                 .HasForeignKey(p => p.FilmId)
                 .OnDelete(DeleteBehavior.Restrict);

            }
        }
        public class CinemaConfiguration : IEntityTypeConfiguration<Cinemas>
        {
            public void Configure(EntityTypeBuilder<Cinemas> builder)
            {
                builder
                     .ToTable("Cinemas", schema: "dbo")
                     .HasKey(p => p.Id);

                builder
                   .Property(p => p.CinemaName).IsRequired().HasMaxLength(50);

                builder
                    .Property(p => p.Address).IsRequired().HasMaxLength(100);
                builder
                    .Property(p => p.CountSeats).IsRequired();
                builder
                    .Property(p => p.Id).IsRequired();

                builder
                    .HasMany(c => c.Seats)
                    .WithOne(o => o.Cinemas)
                    .HasForeignKey(p => p.CinemaId);

                builder
                     .HasMany(c => c.Seances)
                     .WithOne(o => o.Cinemas)
                     .HasForeignKey(p => p.CinemaId)
                     .OnDelete(DeleteBehavior.Restrict);
                builder
                     .HasMany(c => c.CinemaImages)
                     .WithOne(o => o.Cinemas)
                     .HasForeignKey(p => p.CinemaId)
                     .OnDelete(DeleteBehavior.Restrict);

            }
        }
        public class SeanceConfiguration : IEntityTypeConfiguration<Seances>
        {
            public void Configure(EntityTypeBuilder<Seances> builder)
            {
                builder
                     .ToTable("Seances", schema: "dbo")
                     .HasKey(p => p.Id);

                builder
                   .Property(p => p.SeanceName).IsRequired().HasMaxLength(50);

                builder
                    .Property(p => p.SeanceDate).IsRequired();
                builder
                    .Property(p => p.AllSeats).IsRequired();
                builder
                   .Property(p => p.CountSeats).IsRequired();
                builder
                   .Property(p => p.Price).IsRequired();

                builder
                   .HasMany(c => c.Orders)
                   .WithOne(o => o.Seances)
                   .HasForeignKey(p => p.SeanceId)
                   .OnDelete(DeleteBehavior.Restrict);

                builder
                   .HasMany(c => c.BusySeats)
                   .WithOne(o => o.Seances)
                   .HasForeignKey(p => p.SeanceId)
                   .OnDelete(DeleteBehavior.Restrict);

            }
        }
        public class SeatConfiguration : IEntityTypeConfiguration<Seats>
        {
            public void Configure(EntityTypeBuilder<Seats> builder)
            {
                builder
                     .ToTable("Seats", schema: "dbo")
                     .HasKey(p => p.Id);

                builder
                   .Property(p => p.SeatNumber).IsRequired();

                builder
                    .Property(p => p.CinemaId).IsRequired();

            }
        }
        public class BusySeatConfiguration : IEntityTypeConfiguration<BusySeats>
        {
            public void Configure(EntityTypeBuilder<BusySeats> builder)
            {
                builder
                     .ToTable("BusySeats", schema: "dbo")
                     .HasKey(p => p.Id);

                builder
                   .Property(p => p.SeanceId).IsRequired();

                builder
                  .Property(p => p.SeatNumber).IsRequired();

                builder
                    .Property(p => p.IsBusy).IsRequired();

            }
        }
        public class OrderConfiguration : IEntityTypeConfiguration<Orders>
        {
            public void Configure(EntityTypeBuilder<Orders> builder)
            {
                builder
                     .ToTable("Orders", schema: "dbo")
                     .HasKey(p => p.Id);

                //builder
                //   .Property(p => p.Users.UserName).IsRequired().HasMaxLength(50);

                builder
                    .Property(p => p.SeanceId).IsRequired();
                builder
                    .Property(p => p.SeatNumber).IsRequired();
                builder
                    .Property(p => p.IsPaid).IsRequired();
            }
        }
        public class FilmImageConfiguration : IEntityTypeConfiguration<FilmImages>
        {
            public void Configure(EntityTypeBuilder<FilmImages> builder)
            {
                builder
                     .ToTable("FilmImages", schema: "dbo")
                     .HasKey(p => p.Id);

                builder
                   .Property(p => p.FilmId).IsRequired();

                builder
                    .Property(p => p.FilmImage).IsRequired();
            }
        }

        public class CinemaImageConfiguration : IEntityTypeConfiguration<CinemaImages>
        {
            public void Configure(EntityTypeBuilder<CinemaImages> builder)
            {
                builder
                     .ToTable("CinemaImages", schema: "dbo")
                     .HasKey(p => p.Id);

                builder
                   .Property(p => p.CinemaId).IsRequired();

                builder
                    .Property(p => p.CinemaImage).IsRequired();
            }
        }


        /*
                protected override void OnModelCreating(ModelBuilder modelBuilder)
                {


                    modelBuilder.Entity<Users>()
                        .ToTable("Users", schema: "dbo")
                        .HasKey(p => p.Username);

                    modelBuilder.Entity<Users>()
                       .Property(p => p.Username).IsRequired().HasMaxLength(50);

                    modelBuilder.Entity<Users>()
                        .Property(p => p.Password).IsRequired().HasMaxLength(50);

                    modelBuilder.Entity<Users>()
                       .HasOne(c => c.Admins)
                       .WithOne(o => o.Users)
                       .OnDelete(DeleteBehavior.Restrict);

                    modelBuilder.Entity<Users>()
                       .HasOne(c => c.Owners)
                       .WithOne(o => o.Users)
                       .OnDelete(DeleteBehavior.Restrict);

                    modelBuilder.Entity<Users>()
                       .HasOne(c => c.Clients)
                       .WithOne(o => o.Users)
                       .OnDelete(DeleteBehavior.Restrict);

                    modelBuilder.Entity<Users>()
                        .HasMany(c => c.Orders)
                        .WithOne(o => o.Users)
                       .OnDelete(DeleteBehavior.Restrict);




                    modelBuilder.Entity<Admins>()
                              .ToTable("Admins", schema: "dbo")
                            .HasKey(p => p.Id);

                    modelBuilder.Entity<Admins>()
                      .Property(p => p.Username).IsRequired().HasMaxLength(50);

                    modelBuilder.Entity<Admins>()
                       .Property(p => p.FIO).IsRequired().HasMaxLength(300);





                    modelBuilder.Entity<Clients>()
                             .ToTable("Clients", schema: "dbo")
                            .HasKey(p => p.Id);

                    modelBuilder.Entity<Clients>()
                       .Property(p => p.Username).IsRequired().HasMaxLength(50);

                    modelBuilder.Entity<Clients>()
                       .Property(p => p.FIO).IsRequired().HasMaxLength(300);


                    modelBuilder.Entity<Owners>()
                                .ToTable("Owners", schema: "dbo")
                             .HasKey(p => p.Id);

                    modelBuilder.Entity<Owners>()
                       .Property(p => p.Username).IsRequired().HasMaxLength(50);

                    modelBuilder.Entity<Owners>()
                         .Property(p => p.FIO).IsRequired().HasMaxLength(300);
                    modelBuilder.Entity<Owners>()
                        .HasMany(c => c.Cinemas)
                        .WithOne(o => o.Owners);
                    // .HasForeignKey(p => p.OwnerId) 



                    modelBuilder.Entity<Films>()
                          .ToTable("Films", schema: "dbo")
                            .HasKey(p => p.Id);

                    modelBuilder.Entity<Films>()
                       .Property(p => p.FilmName).IsRequired().HasMaxLength(50);

                    modelBuilder.Entity<Films>()
                       .Property(p => p.StartDate).IsRequired();

                    modelBuilder.Entity<Films>()
                       .Property(p => p.EndDate).IsRequired();

                    modelBuilder.Entity<Films>()
                      .HasMany(c => c.Seances)
                      .WithOne(o => o.Films);




                    modelBuilder.Entity<Cinemas>()
                        //.ToTable("Cinemas", schema: "dbo")
                        .HasKey(p => p.Id);

                    modelBuilder.Entity<Cinemas>()
                        .Property(p => p.CinemaName).IsRequired().HasMaxLength(50);

                    modelBuilder.Entity<Cinemas>()
                        .Property(p => p.Address).IsRequired().HasMaxLength(100);

                    modelBuilder.Entity<Cinemas>()
                        .Property(p => p.CountSeats).IsRequired();

                    modelBuilder.Entity<Cinemas>()
                        .Property(p => p.OwnerId).IsRequired();


                    modelBuilder.Entity<Cinemas>()
                      .HasMany(c => c.Seats)
                      .WithOne(o => o.Cinemas);

                    modelBuilder.Entity<Cinemas>()
                     .HasMany(c => c.Seances)
                     .WithOne(o => o.Cinemas);






                    modelBuilder.Entity<Seances>()
                        //.ToTable("Seances", schema: "dbo")
                        .HasKey(p => p.Id);

                    modelBuilder.Entity<Seances>()
                       .Property(p => p.SeanceName).IsRequired().HasMaxLength(50);

                    modelBuilder.Entity<Seances>()
                        .Property(p => p.SeanceDate).IsRequired();

                    modelBuilder.Entity<Seances>()
                        .Property(p => p.SeanceTime).IsRequired();

                    modelBuilder.Entity<Seances>()
                        .Property(p => p.AllSeats).IsRequired();

                    modelBuilder.Entity<Seances>()
                       .Property(p => p.CountSeats).IsRequired();

                    modelBuilder.Entity<Seances>()
                       .Property(p => p.Price).IsRequired();


                    modelBuilder.Entity<Seances>()
                       .HasMany(c => c.Orders)
                       .WithOne(o => o.Seances);

                    modelBuilder.Entity<Seances>()
                      .HasMany(c => c.BusySeats)
                      .WithOne(o => o.Seances);

                    modelBuilder.Entity<Seats>()
                           .ToTable("Seats", schema: "dbo")
                           .HasKey(p => p.Id);

                    modelBuilder.Entity<Seats>()
                       .Property(p => p.SeatNumber).IsRequired();

                    modelBuilder.Entity<Seats>()
                         .Property(p => p.CinemaId).IsRequired();


                    modelBuilder.Entity<BusySeats>()
                     //       .ToTable("BusySeats", schema: "dbo")
                             .HasKey(p => p.Id);

                    modelBuilder.Entity<BusySeats>()
                       .Property(p => p.SeanceId).IsRequired();

                    modelBuilder.Entity<BusySeats>()
                       .Property(p => p.SeatNumber).IsRequired();

                    modelBuilder.Entity<BusySeats>()
                         .Property(p => p.IsBusy).IsRequired();


                    modelBuilder.Entity<Orders>()
                     //     .ToTable("Orders", schema: "dbo")
                             .HasKey(p => p.Id);

                    modelBuilder.Entity<Orders>()
                        .Property(p => p.Username).IsRequired().HasMaxLength(50);

                    modelBuilder.Entity<Orders>()
                        .Property(p => p.SeanceId).IsRequired();
                    modelBuilder.Entity<Orders>()
                         .Property(p => p.SeatId).IsRequired();
                    modelBuilder.Entity<Orders>()
                         .Property(p => p.IsPaid).IsRequired();

                }
                */

    }
}

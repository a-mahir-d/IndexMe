using IndexMe.Domain.Abstractions;
using IndexMe.Domain.LinkClicks;
using IndexMe.Domain.Links;
using IndexMe.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace IndexMe.Infrastructure.Context;

public sealed class IndexMeDbContext : DbContext, IUnitOfWork
{
    public IndexMeDbContext(DbContextOptions<IndexMeDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Link> Links => Set<Link>();
    public DbSet<LinkClick> LinkClicks => Set<LinkClick>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(builder =>
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Username)
                .HasConversion(u => u.Value, v => new Username(v))
                .HasMaxLength(50)
                .IsRequired();

            builder.HasIndex(u => u.Username).IsUnique();

            builder.Property(u => u.Email)
                .HasConversion(e => e.Value, v => new Email(v))
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(u => u.Password)
                .HasConversion(p => p.Value, v => new Password(v, false))
                .IsRequired();

            builder.Property(u => u.DisplayName)
                .HasConversion(
                    dp => dp != null ? dp.Value : null,
                    v => v != null ? new DisplayName(v) : null
                )
                .IsRequired(false);

            builder.Property(u => u.Bio)
                .HasConversion(
                    b => b != null ? b.Value : null,
                    v => v != null ? new Bio(v) : null
                )
                .IsRequired(false);

            builder.Property(u => u.CreatedAt)
                .IsRequired();
        });

        modelBuilder.Entity<Link>(builder =>
        {
            builder.HasKey(l => l.Id);

            builder.Property(l => l.UserId)
                .IsRequired();

            builder.Property(l => l.Title)
                .HasConversion(t => t.Value, v => new Title(v))
                .IsRequired();

            builder.Property(l => l.Url)
                .HasConversion(u => u.Value, v => new Url(v))
                .IsRequired();

            builder.Property(l => l.DisplayOrder)
                .IsRequired();

            builder.Property(u => u.CreatedAt)
                .IsRequired();

            builder.HasOne(l => l.User)
                .WithMany(u => u.Links)
                .HasForeignKey(l => l.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<LinkClick>(builder =>
        {
            builder.HasKey(lc => lc.Id);

            builder.Property(lc => lc.LinkId)
                .IsRequired();

            builder.Property(lc => lc.ClickedAt)
                .IsRequired();

            builder.Property(lc => lc.IpAddress)
                .IsRequired(false);

            builder.Property(lc => lc.UserAgent)
                .IsRequired(false);

            builder.HasOne(lc => lc.Link)
                .WithMany(l => l.Clicks)
                .HasForeignKey(lc => lc.LinkId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}

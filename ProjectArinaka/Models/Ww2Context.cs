using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ProjectArinaka.Models;

public partial class Ww2Context : DbContext
{
    public Ww2Context()
    {
    }

    public Ww2Context(DbContextOptions<Ww2Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Ww2table> Ww2tables { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlite("DataSource=C:\\Users\\megat\\YandexDisk\\Programming\\Dev\\ProjectArinaka\\ProjectArinaka\\WW2.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Ww2table>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("WW2TABLE");

            entity.Property(e => e.ИнвентарныйНомер).HasColumnName("Инвентарный_номер");
            entity.Property(e => e.Списано)
                .HasDefaultValueSql("FALSE")
                .HasColumnType("BOOLEAN")
                .HasColumnName("списано");
            entity.Property(e => e.Фио).HasColumnName("ФИО");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

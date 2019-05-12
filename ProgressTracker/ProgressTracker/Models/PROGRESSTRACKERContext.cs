using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ProgressTracker.Models
{
    public partial class PROGRESSTRACKERContext : DbContext
    {
        public PROGRESSTRACKERContext()
        {
        }

        public PROGRESSTRACKERContext(DbContextOptions<PROGRESSTRACKERContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Ptobjective> Ptobjective { get; set; }
        public virtual DbSet<Ptproject> Ptproject { get; set; }
        public virtual DbSet<Ptuser> Ptuser { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.1-servicing-10028");

            modelBuilder.Entity<Ptobjective>(entity =>
            {
                entity.ToTable("PTObjective");

                entity.Property(e => e.Description).HasMaxLength(50);

                entity.Property(e => e.PtprojectId).HasColumnName("PTProjectId");

                entity.HasOne(d => d.Ptproject)
                    .WithMany(p => p.Ptobjective)
                    .HasForeignKey(d => d.PtprojectId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Ptproject>(entity =>
            {
                entity.ToTable("PTProject");

                entity.Property(e => e.Description).HasMaxLength(100);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.PtuserId).HasColumnName("PTUserId");

                entity.Property(e => e.Status).HasMaxLength(20);

                entity.HasOne(d => d.Ptuser)
                    .WithMany(p => p.Ptproject)
                    .HasForeignKey(d => d.PtuserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PTProject_PTUser_UserId");
            });

            modelBuilder.Entity<Ptuser>(entity =>
            {
                entity.ToTable("PTUser");

                entity.HasIndex(e => e.Email)
                    .HasName("UQ__PTUser__A9D105343F6A2CEC")
                    .IsUnique();

                entity.Property(e => e.Description).HasMaxLength(100);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(50);
            });
        }
    }
}

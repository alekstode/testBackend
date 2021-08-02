using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using DAL.EF.EF.Entities;

#nullable disable

namespace DAL.EF.EF.Context
{
    public partial class TestRestContext : DbContext
    {
        public TestRestContext()
        {
        }

        public TestRestContext(DbContextOptions<TestRestContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AcademicPerformance> AcademicPerformances { get; set; }
        public virtual DbSet<Sex> Sexes { get; set; }
        public virtual DbSet<Student> Students { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=(local);Database=TestRest;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<AcademicPerformance>(entity =>
            {
                entity.ToTable("AcademicPerformance");
            });

            modelBuilder.Entity<Sex>(entity =>
            {
                entity.ToTable("Sex");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("Student");

                entity.Property(e => e.dob).HasColumnType("datetime");

                entity.HasOne(d => d.idAcademicPerformanceNavigation)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.idAcademicPerformance)
                    .HasConstraintName("R_255");

                entity.HasOne(d => d.idSexNavigation)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.idSex)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("R_254");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

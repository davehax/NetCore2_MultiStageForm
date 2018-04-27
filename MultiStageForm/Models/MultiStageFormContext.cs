using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MultiStageForm.Models
{
    public partial class MultiStageFormContext : DbContext
    {
        public virtual DbSet<Stage1> Stage1 { get; set; }
        public virtual DbSet<Stage2> Stage2 { get; set; }
        public virtual DbSet<Stage3> Stage3 { get; set; }
        public virtual DbSet<Stagedform> Stagedform { get; set; }

        // 
        public MultiStageFormContext(DbContextOptions<MultiStageFormContext> options)
            : base (options)
        {}

        // The following generated method will have it's guts moved to "Startup.cs"
        // in order to conform with the standard asp.net Dependency Injection pattern
//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
        //        optionsBuilder.UseNpgsql(@"Host=localhost;Database=MultiStageForm;Username=postgres;Password=");
        //    }
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Stage1>(entity =>
            {
                entity.ToTable("stage1");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Stage2>(entity =>
            {
                entity.ToTable("stage2");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description");
            });

            modelBuilder.Entity<Stage3>(entity =>
            {
                entity.ToTable("stage3");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Date).HasColumnName("date");
            });

            modelBuilder.Entity<Stagedform>(entity =>
            {
                entity.ToTable("stagedform");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CurrentStage).HasColumnName("current_stage");

                entity.Property(e => e.Stage1).HasColumnName("stage1");

                entity.Property(e => e.Stage2).HasColumnName("stage2");

                entity.Property(e => e.Stage3).HasColumnName("stage3");

                entity.HasOne(d => d.Stage1Navigation)
                    .WithMany(p => p.Stagedform)
                    .HasForeignKey(d => d.Stage1)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_stage1");

                entity.HasOne(d => d.Stage2Navigation)
                    .WithMany(p => p.Stagedform)
                    .HasForeignKey(d => d.Stage2)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_stage2");

                entity.HasOne(d => d.Stage3Navigation)
                    .WithMany(p => p.Stagedform)
                    .HasForeignKey(d => d.Stage3)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_stage3");
            });
        }
    }
}

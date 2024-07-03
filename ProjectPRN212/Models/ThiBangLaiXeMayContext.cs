using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ProjectPRN212.Models;

public partial class ThiBangLaiXeMayContext : DbContext
{
    public ThiBangLaiXeMayContext()
    {
    }

    public ThiBangLaiXeMayContext(DbContextOptions<ThiBangLaiXeMayContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AdminAction> AdminActions { get; set; }

    public virtual DbSet<Exam> Exams { get; set; }

    public virtual DbSet<ExamDetail> ExamDetails { get; set; }

    public virtual DbSet<ExamResult> ExamResults { get; set; }

    public virtual DbSet<Question> Questions { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserHistory> UserHistories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var builder = new ConfigurationBuilder()
                               .SetBasePath(Directory.GetCurrentDirectory())
                               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
        IConfigurationRoot configuration = builder.Build();
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("MyCnn"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AdminAction>(entity =>
        {
            entity.HasKey(e => e.ActionId).HasName("PK__AdminAct__FFE3F4B9FA5BF240");

            entity.Property(e => e.ActionId).HasColumnName("ActionID");
            entity.Property(e => e.ActionDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ActionType).HasMaxLength(20);
            entity.Property(e => e.AdminId).HasColumnName("AdminID");
            entity.Property(e => e.QuestionId).HasColumnName("QuestionID");
        });

        modelBuilder.Entity<Exam>(entity =>
        {
            entity.HasKey(e => e.ExamId).HasName("PK__Exams__297521A72FCD7C87");

            entity.HasIndex(e => e.ExamDate, "idx_ExamDate");

            entity.Property(e => e.ExamId).HasColumnName("ExamID");
            entity.Property(e => e.ExamDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");
        });

        modelBuilder.Entity<ExamDetail>(entity =>
        {
            entity.HasKey(e => e.ExamDetailId).HasName("PK__ExamDeta__D30D6385817D5A3E");

            entity.Property(e => e.ExamDetailId).HasColumnName("ExamDetailID");
            entity.Property(e => e.ExamId).HasColumnName("ExamID");
            entity.Property(e => e.QuestionId).HasColumnName("QuestionID");
            entity.Property(e => e.UserAnswer)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
        });

        modelBuilder.Entity<ExamResult>(entity =>
        {
            entity.HasKey(e => e.ResultId).HasName("PK__ExamResu__976902288031933D");

            entity.Property(e => e.ResultId).HasColumnName("ResultID");
            entity.Property(e => e.ExamId).HasColumnName("ExamID");
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.HasKey(e => e.QuestionId).HasName("PK__Question__0DC06F8C935B9588");

            entity.Property(e => e.QuestionId).HasColumnName("QuestionID");
            entity.Property(e => e.CorrectOption)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCAC12DB4DE5");

            entity.HasIndex(e => e.Username, "UQ__Users__536C85E4BBF60180").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Users__A9D10534396C31FA").IsUnique();

            entity.HasIndex(e => e.UserType, "idx_UserType");

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.PhoneNumber).HasMaxLength(15);
            entity.Property(e => e.UserType).HasMaxLength(20);
            entity.Property(e => e.Username).HasMaxLength(50);
        });

        modelBuilder.Entity<UserHistory>(entity =>
        {
            entity.HasKey(e => e.HistoryId).HasName("PK__UserHist__4D7B4ADDB8797D3C");

            entity.ToTable("UserHistory");

            entity.HasIndex(e => e.Score, "idx_Score");

            entity.Property(e => e.HistoryId).HasColumnName("HistoryID");
            entity.Property(e => e.ExamDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ExamId).HasColumnName("ExamID");
            entity.Property(e => e.UserId).HasColumnName("UserID");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

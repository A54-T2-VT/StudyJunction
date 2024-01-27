using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudyJunction.Infrastructure.Constants;
using StudyJunction.Infrastructure.Data.Models;
using StudyJunction.Infrastructure.Interceptors;
using System.Diagnostics;
using System.Reflection.Emit;

namespace StudyJunction.Infrastructure.Data
{
    public class SJDbContext : IdentityDbContext
    {
        public SJDbContext(DbContextOptions<SJDbContext> options)
            : base(options)
        {            
        }

        public DbSet<UserDb> Users { get; set; }
        public DbSet<CourseDb> Courses { get; set; }
        public DbSet<LectureDb> Lectures { get; set; }
        public DbSet<NoteDb> Notes { get; set; }
        public DbSet<CategoryDb> Categories { get; set; }
        public DbSet<UsersCoursesDb> UsersCourses { get; set; }
        public DbSet<UsersLecturesDb> UsersLectures { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(new SoftDeletionInterceptor());

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //User entity
            builder.Entity<UserDb>(e =>
            {
                e.Property(u => u.FirstName)
                .IsRequired()
                .HasMaxLength(ModelsConstants.UserFirstNameMaxLength);
                
                e.Property(u => u.LastName)
                .IsRequired()
                .HasMaxLength(ModelsConstants.UserLastNameMaxLength);
            });

            builder.Entity<UserDb>()
                .HasIndex(u => u.Email)
                .IsUnique();
            builder.Entity<UserDb>()
                .HasIndex(u => u.UserName)
                .IsUnique();

            //Ensure it works!!! Possible exception point.!!!!
            builder.Entity<IdentityUser>().HasQueryFilter(e => ((UserDb)e).IsDeleted == false);
            //builder.Entity<IdentityUser>().HasQueryFilter(u => !EF.Property<bool>(u, "IsDeleted")); // genereated by chat gpt

            //Course entity
            builder.Entity<CourseDb>(e => 
            {
                e.HasKey(c => c.Id);

                e.Property(c => c.Title)
                .IsRequired()                
                .HasMaxLength(ModelsConstants.CourseTitleMaxLength);

                e.Property(c => c.Description)
                .IsRequired()
                .HasMaxLength(ModelsConstants.CouresDecsriptionMaxLength);

                e.HasOne(c => c.CreatedBy)
                .WithMany(u => u.MyCreatedCourses)
                .HasForeignKey(c => c.CreatorId)
                .OnDelete(DeleteBehavior.Restrict);

                e.HasOne(c => c.Category)
                .WithMany(cat => cat.Courses)
                .HasForeignKey(c => c.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<CourseDb>()
                .HasIndex(c => c.Title)
                .IsUnique();

            builder.Entity<CourseDb>().HasQueryFilter(e => e.IsDeleted == false);

            //Lecture entity
            builder.Entity<LectureDb>(e =>
            {
                e.HasKey(l => l.Id);

                e.Property(l => l.Title)
                .IsRequired()
                .HasMaxLength(ModelsConstants.LectureTitleMaxLength);

                e.Property(l => l.Description)
                .IsRequired()
                .HasMaxLength(ModelsConstants.LectureDescriptionMaxLength);

                e.HasOne(l => l.Course)
                .WithMany(c => c.Lectures)
                .HasForeignKey (l => l.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            });

            builder.Entity<LectureDb>().HasQueryFilter(e => e.IsDeleted == false);

            //Note entity
            builder.Entity<NoteDb>(e =>
            {
                e.HasKey(n => n.Id);

                e.Property(n => n.Content)
                .IsRequired()
                .HasMaxLength(ModelsConstants.NoteContentMaxLength);

                e.HasOne(n => n.Lecture)
                .WithMany(l => l.Notes)
                .HasForeignKey(n => n.LectureId)
                .OnDelete(DeleteBehavior.Cascade);

                e.HasOne(n => n.CreatedBy)
                .WithMany(u => u.MyNotes)
                .HasForeignKey(n => n.CreatorId)
                .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<NoteDb>().HasQueryFilter(e => e.IsDeleted == false);

            //Category entity
            builder.Entity<CategoryDb>(e =>
            {
                e.HasKey(c => c.Id);

                e.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(ModelsConstants.CategoryNameMaxLength);

                e.HasOne(c => c.ParentCategory)
                .WithMany()
                .HasForeignKey(c => c.ParentCategoryId)
                .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<CategoryDb>()
                .HasIndex(c => c.Name)
                .IsUnique();

            builder.Entity<CategoryDb>().HasQueryFilter(e => e.IsDeleted == false);

            //UsersCourses entity
            builder.Entity<UsersCoursesDb>(e =>
            {
                e.HasKey(uc => new
                {
                    uc.UserId,
                    uc.CourseId
                });

                e.HasOne(uc => uc.User)
                .WithMany(u => u.MyEnrolledCourses)
                .HasForeignKey(uc => uc.UserId)
                .OnDelete(DeleteBehavior.Restrict);

                e.HasOne(uc => uc.Course)
                .WithMany(c => c.EnrolledUsers)
                .HasForeignKey(uc => uc.CourseId)
                .OnDelete(DeleteBehavior.Restrict);
            });
            //UsersLectures entity
            builder.Entity<UsersLecturesDb>(e =>
            {
                e.HasKey(ul => new
                {
                    ul.UserId,
                    ul.LectureId
                });

                e.HasOne(ul => ul.User)
                .WithMany(u => u.MyLectures)
                .HasForeignKey(ul => ul.UserId)
                .OnDelete(DeleteBehavior.Restrict);

                e.HasOne(ul => ul.Lecture)
                .WithMany(l => l.UsersLectures)
                .HasForeignKey(ul => ul.LectureId)
                .OnDelete(DeleteBehavior.Restrict);
            });

            base.OnModelCreating(builder);
        }
    }
}

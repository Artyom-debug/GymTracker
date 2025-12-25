using GymTrackerProject.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GymTrackerProject.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    { }

    public DbSet<Workout> Workouts { get; set; }
    public DbSet<Exercise> Exercises { get; set; }
    public DbSet<ProgressTracker> Trackers { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<ProgressTracker>(entity =>
        {
            entity.HasKey(t => t.Id);

            entity.HasOne(t => t.Exercise)
                  .WithMany(e => e.Progress)
                  .HasForeignKey(t => t.ExerciseId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(t => t.Workout)
                  .WithMany(w => w.ProgressTrackers)
                  .HasForeignKey(t => t.WorkoutId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.ToTable(t => t.HasCheckConstraint("ValidReps", "Reps >= 0"));

            entity.ToTable(t => t.HasCheckConstraint("ValidWeight", "Weight >= 0"));
        });

        builder.Entity<Workout>(entity =>
        {
            entity.HasKey(w => w.Id);

            entity.HasMany(w => w.ProgressTrackers)
                  .WithOne(t => t.Workout)
                  .HasForeignKey(t => t.WorkoutId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(w => w.User)
                  .WithMany(u => u.Workouts)
                  .HasForeignKey(w => w.UsersId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.Property(w => w.Name)
                  .IsRequired()
                  .HasMaxLength(100);

            entity.Property(w => w.CreatedAt)
                  .HasColumnType("datetime");
        });

        builder.Entity<Exercise>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Name)
                  .IsRequired()
                  .HasMaxLength(100);

            entity.Property(e => e.Category)
                  .IsRequired()
                  .HasMaxLength(100);

            entity.HasMany(e => e.Progress)
                  .WithOne(p => p.Exercise)
                  .HasForeignKey(p => p.ExerciseId);
        });

        SeedData(builder);
    }

    private void SeedData(ModelBuilder builder)
    {
        builder.Entity<Exercise>().HasData(
            new Exercise { Id = 1, Name = "Жим штанги лёжа", Category = "Грудь"},
            new Exercise { Id = 2, Name = "Жим штанги в наклоне", Category = "Грудь" },
            new Exercise { Id = 3, Name = "Жим гантелей лёжа", Category = "Грудь" },
            new Exercise { Id = 4, Name = "Жим гантелей в наклоне", Category = "Грудь" },
            new Exercise { Id = 5, Name = "Отжимания на брусьях", Category = "Грудь" },
            new Exercise { Id = 6, Name = "Кроссовер на блоках", Category = "Грудь" },
            new Exercise { Id = 7, Name = "Кроссовер на блоках", Category = "Грудь" },
            new Exercise { Id = 8, Name = "Отжимания", Category = "Трицепс" },
            new Exercise { Id = 9, Name = "Бабочка", Category = "Грудь" },
            new Exercise { Id = 10, Name = "Кроссовер на блоках", Category = "Грудь" },
            new Exercise { Id = 11, Name = "Разведение гантелей лёжа", Category = "Грудь" },
            new Exercise { Id = 12, Name = "Разведение гантелей в наклоне", Category = "Грудь" },
            new Exercise { Id = 13, Name = "Кроссовер в блоке", Category = "Грудь" },
            new Exercise { Id = 14, Name = "Жим в хаммере", Category = "Грудь" },

            new Exercise { Id = 15, Name = "Приседания со штангой", Category = "Ноги" },
            new Exercise { Id = 16, Name = "Жим платформы", Category = "Ноги" },
            new Exercise { Id = 17, Name = "Болгарские выпады", Category = "Ноги" },
            new Exercise { Id = 18, Name = "Румынская тяга", Category = "Ноги" },
            new Exercise { Id = 19, Name = "Гакк-приседания", Category = "Ноги" },
            new Exercise { Id = 20, Name = "Разгибания ног в тренажёре", Category = "Ноги" },
            new Exercise { Id = 21, Name = "Приседания с гантелями", Category = "Ноги" },
            new Exercise { Id = 22, Name = "Сведение ног в тренажёре", Category = "Ноги" },
            new Exercise { Id = 23, Name = "Разведение ног в тренажёре", Category = "Ноги" },
            new Exercise { Id = 24, Name = "Сгибания ног лёжа", Category = "Ноги" },
            new Exercise { Id = 25, Name = "Икры в тренажёре", Category = "Ноги" },
            new Exercise { Id = 26, Name = "Икры со штангой", Category = "Ноги" },

            new Exercise { Id = 27, Name = "Гиперэкстензия", Category = "Спина" },
            new Exercise { Id = 28, Name = "Тяга верхнего блока", Category = "Спина" },
            new Exercise { Id = 29, Name = "Тяга нижнего блолка", Category = "Спина" },
            new Exercise { Id = 30, Name = "Тяга штанги в наклоне", Category = "Спина" },
            new Exercise { Id = 31, Name = "Подтягивания", Category = "Спина" },
            new Exercise { Id = 32, Name = "Шраги с гантелями", Category = "Спина" },
            new Exercise { Id = 33, Name = "Шраги со штангой", Category = "Спина" },
            new Exercise { Id = 34, Name = "Тяга в хаммере", Category = "Спина" },
            new Exercise { Id = 35, Name = "Становая тяга", Category = "Спина" },

            new Exercise { Id = 36, Name = "Подъём штанги на бицепс", Category = "Бицепс" },
            new Exercise { Id = 37, Name = "Подъём гантелей на бицепс", Category = "Бицепс" },
            new Exercise { Id = 38, Name = "Молотки на бицепс", Category = "Бицепс" },
            new Exercise { Id = 39, Name = "Подъём на бицепс в блоке", Category = "Бицепс" },
            new Exercise { Id = 40, Name = "Подъём на бицепс в тренажёре", Category = "Бицепс" },
            new Exercise { Id = 41, Name = "Подъём гантелей на бицепс сидя", Category = "Бицепс" },
            new Exercise { Id = 42, Name = "Подъём гантелей на бицепс с сублимацией", Category = "Спина" },

            new Exercise { Id = 43, Name = "Французский жим со штангой", Category = "Трицепс" },
            new Exercise { Id = 44, Name = "Французский жим с гантелями", Category = "Трицепс" },
            new Exercise { Id = 45, Name = "Жим гантели из-за головы", Category = "Трицепс" },
            new Exercise { Id = 46, Name = "Разгибание рук с косичкой в блоке", Category = "Трицепс" },
            new Exercise { Id = 47, Name = "Разгибание рук с прямой рукоятью в блоке", Category = "Трицепс" },

            new Exercise { Id = 48, Name = "Махи гантелями", Category = "Плечи" },
            new Exercise { Id = 49, Name = "Тяга штанги к подбородку", Category = "Плечи" },
            new Exercise { Id = 50, Name = "Армейский жим со штангой", Category = "Плечи" },
            new Exercise { Id = 51, Name = "Армейский жим с гантелями", Category = "Плечи" },
            new Exercise { Id = 52, Name = "Обратная бабочка", Category = "Плечи" }
        );
    }
}

using Microsoft.EntityFrameworkCore;
using AlHidayahPro.Data.Models;

namespace AlHidayahPro.Data;

/// <summary>
/// Database context for Islamic resources
/// </summary>
public class IslamicDbContext : DbContext
{
    public IslamicDbContext(DbContextOptions<IslamicDbContext> options) : base(options)
    {
    }

    public DbSet<Surah> Surahs => Set<Surah>();
    public DbSet<QuranVerse> QuranVerses => Set<QuranVerse>();
    public DbSet<Hadith> Hadiths => Set<Hadith>();
    public DbSet<AudioRecitation> AudioRecitations => Set<AudioRecitation>();
    public DbSet<User> Users => Set<User>();
    public DbSet<AiSettings> AiSettings => Set<AiSettings>();
    
    // Learning system
    public DbSet<LearningPath> LearningPaths => Set<LearningPath>();
    public DbSet<LearningMilestone> LearningMilestones => Set<LearningMilestone>();
    public DbSet<UserProgress> UserProgresses => Set<UserProgress>();
    public DbSet<Achievement> Achievements => Set<Achievement>();
    public DbSet<UserAchievement> UserAchievements => Set<UserAchievement>();
    
    // Quiz system
    public DbSet<Quiz> Quizzes => Set<Quiz>();
    public DbSet<QuizQuestion> QuizQuestions => Set<QuizQuestion>();
    public DbSet<QuizAttempt> QuizAttempts => Set<QuizAttempt>();
    
    // Spaced repetition
    public DbSet<FlashCard> FlashCards => Set<FlashCard>();
    public DbSet<UserFlashCardProgress> UserFlashCardProgresses => Set<UserFlashCardProgress>();
    
    // Prayer & Worship Tools
    public DbSet<PrayerLog> PrayerLogs => Set<PrayerLog>();
    public DbSet<ReadingGoal> ReadingGoals => Set<ReadingGoal>();
    public DbSet<DailyDhikr> DailyDhikrs => Set<DailyDhikr>();
    
    // Smart Navigation
    public DbSet<ReadingHistory> ReadingHistories => Set<ReadingHistory>();
    public DbSet<Bookmark> Bookmarks => Set<Bookmark>();
    
    // Gamification
    public DbSet<Challenge> Challenges => Set<Challenge>();
    public DbSet<UserChallenge> UserChallenges => Set<UserChallenge>();
    
    // Module system
    public DbSet<ContentModule> ContentModules => Set<ContentModule>();
    public DbSet<QuranTranslation> QuranTranslations => Set<QuranTranslation>();
    public DbSet<Book> Books => Set<Book>();
    public DbSet<BookChapter> BookChapters => Set<BookChapter>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure Surah entity
        modelBuilder.Entity<Surah>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.Number).IsUnique();
            entity.Property(e => e.ArabicName).IsRequired();
            entity.Property(e => e.EnglishName).IsRequired();
        });

        // Configure QuranVerse entity
        modelBuilder.Entity<QuranVerse>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => new { e.SurahNumber, e.AyahNumber }).IsUnique();
            entity.Property(e => e.ArabicText).IsRequired();
            
            // Configure relationship
            entity.HasOne(e => e.Surah)
                  .WithMany(s => s.Verses)
                  .HasForeignKey(e => e.SurahNumber)
                  .HasPrincipalKey(s => s.Number);
        });

        // Configure Hadith entity
        modelBuilder.Entity<Hadith>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => new { e.Collection, e.HadithNumber });
            entity.Property(e => e.EnglishText).IsRequired();
            entity.Property(e => e.Collection).IsRequired();
        });

        // Configure AudioRecitation entity
        modelBuilder.Entity<AudioRecitation>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => new { e.ReciterName, e.SurahNumber, e.AyahNumber });
            entity.Property(e => e.AudioUrl).IsRequired();
        });

        // Configure User entity
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.Username).IsUnique();
            entity.HasIndex(e => e.Email).IsUnique();
            entity.Property(e => e.Username).IsRequired();
            entity.Property(e => e.Email).IsRequired();
            entity.Property(e => e.PasswordHash).IsRequired();
        });

        // Configure AiSettings entity
        modelBuilder.Entity<AiSettings>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.UserId).IsUnique();
            entity.Property(e => e.Provider).IsRequired();
            
            entity.HasOne(e => e.User)
                  .WithMany()
                  .HasForeignKey(e => e.UserId);
        });

        // Configure LearningPath entity
        modelBuilder.Entity<LearningPath>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => new { e.UserId, e.IsActive });
            entity.Property(e => e.Title).IsRequired();
            entity.Property(e => e.PathType).IsRequired();
            
            entity.HasOne(e => e.User)
                  .WithMany()
                  .HasForeignKey(e => e.UserId);
        });

        // Configure LearningMilestone entity
        modelBuilder.Entity<LearningMilestone>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => new { e.LearningPathId, e.OrderIndex });
            entity.Property(e => e.Title).IsRequired();
            
            entity.HasOne(e => e.LearningPath)
                  .WithMany(lp => lp.Milestones)
                  .HasForeignKey(e => e.LearningPathId);
        });

        // Configure UserProgress entity
        modelBuilder.Entity<UserProgress>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.UserId).IsUnique();
            
            entity.HasOne(e => e.User)
                  .WithMany()
                  .HasForeignKey(e => e.UserId);
        });

        // Configure Achievement entity
        modelBuilder.Entity<Achievement>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.AchievementId).IsUnique();
            entity.Property(e => e.Title).IsRequired();
            entity.Property(e => e.Category).IsRequired();
        });

        // Configure UserAchievement entity
        modelBuilder.Entity<UserAchievement>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => new { e.UserId, e.AchievementId }).IsUnique();
            
            entity.HasOne(e => e.User)
                  .WithMany()
                  .HasForeignKey(e => e.UserId);
                  
            entity.HasOne(e => e.Achievement)
                  .WithMany(a => a.UserAchievements)
                  .HasForeignKey(e => e.AchievementId);
        });

        // Configure Quiz entity
        modelBuilder.Entity<Quiz>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.Category);
            entity.Property(e => e.Title).IsRequired();
        });

        // Configure QuizQuestion entity
        modelBuilder.Entity<QuizQuestion>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => new { e.QuizId, e.OrderIndex });
            entity.Property(e => e.QuestionText).IsRequired();
            
            entity.HasOne(e => e.Quiz)
                  .WithMany(q => q.Questions)
                  .HasForeignKey(e => e.QuizId);
        });

        // Configure QuizAttempt entity
        modelBuilder.Entity<QuizAttempt>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => new { e.UserId, e.QuizId, e.StartTime });
            
            entity.HasOne(e => e.User)
                  .WithMany()
                  .HasForeignKey(e => e.UserId);
                  
            entity.HasOne(e => e.Quiz)
                  .WithMany(q => q.Attempts)
                  .HasForeignKey(e => e.QuizId);
        });

        // Configure FlashCard entity
        modelBuilder.Entity<FlashCard>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.Category);
            entity.Property(e => e.Front).IsRequired();
            entity.Property(e => e.Back).IsRequired();
        });

        // Configure UserFlashCardProgress entity
        modelBuilder.Entity<UserFlashCardProgress>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => new { e.UserId, e.FlashCardId }).IsUnique();
            entity.HasIndex(e => new { e.UserId, e.NextReviewDate });
            
            entity.HasOne(e => e.User)
                  .WithMany()
                  .HasForeignKey(e => e.UserId);
                  
            entity.HasOne(e => e.FlashCard)
                  .WithMany(fc => fc.UserProgress)
                  .HasForeignKey(e => e.FlashCardId);
        });

        // Configure PrayerLog entity
        modelBuilder.Entity<PrayerLog>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => new { e.UserId, e.PrayerDate, e.PrayerName });
            entity.Property(e => e.PrayerName).IsRequired();
            
            entity.HasOne(e => e.User)
                  .WithMany()
                  .HasForeignKey(e => e.UserId);
        });

        // Configure ReadingGoal entity
        modelBuilder.Entity<ReadingGoal>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => new { e.UserId, e.IsActive });
            entity.Property(e => e.Title).IsRequired();
            entity.Property(e => e.GoalType).IsRequired();
            
            entity.HasOne(e => e.User)
                  .WithMany()
                  .HasForeignKey(e => e.UserId);
        });

        // Configure DailyDhikr entity
        modelBuilder.Entity<DailyDhikr>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => new { e.Category, e.OrderIndex });
            entity.Property(e => e.Title).IsRequired();
            entity.Property(e => e.ArabicText).IsRequired();
            entity.Property(e => e.Translation).IsRequired();
        });

        // Configure ReadingHistory entity
        modelBuilder.Entity<ReadingHistory>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => new { e.UserId, e.ContentType, e.ContentId }).IsUnique();
            entity.HasIndex(e => new { e.UserId, e.LastReadAt });
            entity.Property(e => e.ContentType).IsRequired();
            
            entity.HasOne(e => e.User)
                  .WithMany()
                  .HasForeignKey(e => e.UserId);
        });

        // Configure Bookmark entity
        modelBuilder.Entity<Bookmark>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => new { e.UserId, e.ContentType });
            entity.HasIndex(e => new { e.UserId, e.IsFavorite });
            entity.Property(e => e.Title).IsRequired();
            entity.Property(e => e.ContentType).IsRequired();
            entity.Property(e => e.Reference).IsRequired();
            
            entity.HasOne(e => e.User)
                  .WithMany()
                  .HasForeignKey(e => e.UserId);
        });

        // Configure Challenge entity
        modelBuilder.Entity<Challenge>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => new { e.IsActive, e.StartDate, e.EndDate });
            entity.Property(e => e.Title).IsRequired();
            entity.Property(e => e.ChallengeType).IsRequired();
        });

        // Configure UserChallenge entity
        modelBuilder.Entity<UserChallenge>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => new { e.UserId, e.ChallengeId }).IsUnique();
            entity.HasIndex(e => new { e.UserId, e.IsCompleted });
            
            entity.HasOne(e => e.User)
                  .WithMany()
                  .HasForeignKey(e => e.UserId);
                  
            entity.HasOne(e => e.Challenge)
                  .WithMany(c => c.UserChallenges)
                  .HasForeignKey(e => e.ChallengeId);
        });

        // Configure ContentModule entity
        modelBuilder.Entity<ContentModule>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.ModuleId).IsUnique();
            entity.Property(e => e.ModuleId).IsRequired();
            entity.Property(e => e.Name).IsRequired();
        });

        // Configure QuranTranslation entity
        modelBuilder.Entity<QuranTranslation>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => new { e.ModuleId, e.SurahNumber, e.AyahNumber });
            entity.Property(e => e.Translation).IsRequired();
            
            entity.HasOne(e => e.Module)
                  .WithMany()
                  .HasForeignKey(e => e.ModuleId);
        });

        // Configure Book entity
        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Title).IsRequired();
            entity.Property(e => e.Author).IsRequired();
            
            entity.HasOne(e => e.Module)
                  .WithMany()
                  .HasForeignKey(e => e.ModuleId);
        });

        // Configure BookChapter entity
        modelBuilder.Entity<BookChapter>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => new { e.BookId, e.ChapterNumber });
            entity.Property(e => e.Title).IsRequired();
            entity.Property(e => e.Content).IsRequired();
            
            entity.HasOne(e => e.Book)
                  .WithMany(b => b.Chapters)
                  .HasForeignKey(e => e.BookId);
        });
    }
}

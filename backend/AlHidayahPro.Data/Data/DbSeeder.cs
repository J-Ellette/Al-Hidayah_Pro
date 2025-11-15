using AlHidayahPro.Data.Models;

namespace AlHidayahPro.Data.Data;

/// <summary>
/// Seeds the database with sample Islamic content
/// </summary>
public static class DbSeeder
{
    /// <summary>
    /// Seeds sample data for development and testing
    /// </summary>
    public static void SeedData(IslamicDbContext context)
    {
        // Check if data already exists
        if (context.Surahs.Any())
        {
            return; // Database already seeded
        }

        // Seed Surah Al-Fatihah (Chapter 1)
        var fatihah = new Surah
        {
            Number = 1,
            ArabicName = "الفاتحة",
            EnglishName = "Al-Fatihah",
            EnglishTranslation = "The Opening",
            NumberOfAyahs = 7,
            RevelationType = "Meccan"
        };
        context.Surahs.Add(fatihah);

        // Seed verses of Al-Fatihah
        var verses = new[]
        {
            new QuranVerse
            {
                SurahNumber = 1,
                AyahNumber = 1,
                ArabicText = "بِسْمِ اللَّهِ الرَّحْمَٰنِ الرَّحِيمِ",
                EnglishTranslation = "In the name of Allah, the Entirely Merciful, the Especially Merciful.",
                Transliteration = "Bismillahir Rahmanir Raheem",
                JuzNumber = 1,
                PageNumber = 1
            },
            new QuranVerse
            {
                SurahNumber = 1,
                AyahNumber = 2,
                ArabicText = "الْحَمْدُ لِلَّهِ رَبِّ الْعَالَمِينَ",
                EnglishTranslation = "[All] praise is [due] to Allah, Lord of the worlds -",
                Transliteration = "Alhamdu lillahi rabbil 'aalameen",
                JuzNumber = 1,
                PageNumber = 1
            },
            new QuranVerse
            {
                SurahNumber = 1,
                AyahNumber = 3,
                ArabicText = "الرَّحْمَٰنِ الرَّحِيمِ",
                EnglishTranslation = "The Entirely Merciful, the Especially Merciful,",
                Transliteration = "Ar-Rahmaanir-Raheem",
                JuzNumber = 1,
                PageNumber = 1
            },
            new QuranVerse
            {
                SurahNumber = 1,
                AyahNumber = 4,
                ArabicText = "مَالِكِ يَوْمِ الدِّينِ",
                EnglishTranslation = "Sovereign of the Day of Recompense.",
                Transliteration = "Maaliki yawmid-deen",
                JuzNumber = 1,
                PageNumber = 1
            },
            new QuranVerse
            {
                SurahNumber = 1,
                AyahNumber = 5,
                ArabicText = "إِيَّاكَ نَعْبُدُ وَإِيَّاكَ نَسْتَعِينُ",
                EnglishTranslation = "It is You we worship and You we ask for help.",
                Transliteration = "Iyyaaka na'budu wa lyyaaka nasta'een",
                JuzNumber = 1,
                PageNumber = 1
            },
            new QuranVerse
            {
                SurahNumber = 1,
                AyahNumber = 6,
                ArabicText = "اهْدِنَا الصِّرَاطَ الْمُسْتَقِيمَ",
                EnglishTranslation = "Guide us to the straight path -",
                Transliteration = "Ihdinas-siraatal-mustaqeem",
                JuzNumber = 1,
                PageNumber = 1
            },
            new QuranVerse
            {
                SurahNumber = 1,
                AyahNumber = 7,
                ArabicText = "صِرَاطَ الَّذِينَ أَنْعَمْتَ عَلَيْهِمْ غَيْرِ الْمَغْضُوبِ عَلَيْهِمْ وَلَا الضَّالِّينَ",
                EnglishTranslation = "The path of those upon whom You have bestowed favor, not of those who have evoked [Your] anger or of those who are astray.",
                Transliteration = "Siraatal-lazeena an'amta 'alaihim ghayril-maghdoobi 'alaihim wa lad-daaalleen",
                JuzNumber = 1,
                PageNumber = 1
            }
        };

        context.QuranVerses.AddRange(verses);

        // Seed sample Hadith
        var sampleHadiths = new[]
        {
            new Hadith
            {
                Collection = "Sahih Bukhari",
                Book = "Book of Revelation",
                BookNumber = 1,
                HadithNumber = "1",
                ArabicText = "عَنْ عُمَرَ بْنِ الْخَطَّابِ رضي الله عنه قَالَ: سَمِعْتُ رَسُولَ اللَّهِ صلى الله عليه وسلم يَقُولُ: إِنَّمَا الْأَعْمَالُ بِالنِّيَّاتِ",
                EnglishText = "Actions are according to intentions, and everyone will get what was intended.",
                Grade = "Sahih",
                Narrator = "Umar ibn Al-Khattab",
                Chapter = "How the Divine Inspiration started"
            },
            new Hadith
            {
                Collection = "Sahih Muslim",
                Book = "Book of Faith",
                BookNumber = 1,
                HadithNumber = "1",
                ArabicText = "عَنْ عُمَرَ بْنِ الْخَطَّابِ رضي الله عنه أَنَّ جِبْرِيلَ عَلَيْهِ السَّلَامُ سَأَلَ النَّبِيَّ صلى الله عليه وسلم عَنِ الْإِيمَانِ",
                EnglishText = "Faith is to believe in Allah, His Angels, His Books, His Messengers, the Last Day, and to believe in Divine Destiny.",
                Grade = "Sahih",
                Narrator = "Umar ibn Al-Khattab",
                Chapter = "The Hadith of Jibreel"
            }
        };

        context.Hadiths.AddRange(sampleHadiths);

        // Seed sample audio recitation metadata
        var sampleRecitations = new[]
        {
            new AudioRecitation
            {
                ReciterName = "Mishary Rashid Alafasy",
                SurahNumber = 1,
                AudioUrl = "https://example.com/audio/mishary/001.mp3",
                Format = "mp3",
                DurationSeconds = 45,
                RecitationStyle = "Murattal"
            },
            new AudioRecitation
            {
                ReciterName = "Abdul Basit",
                SurahNumber = 1,
                AudioUrl = "https://example.com/audio/basit/001.mp3",
                Format = "mp3",
                DurationSeconds = 48,
                RecitationStyle = "Mujawwad"
            }
        };

        context.AudioRecitations.AddRange(sampleRecitations);

        // Save all changes
        context.SaveChanges();
    }
}

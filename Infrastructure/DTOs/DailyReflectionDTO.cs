using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DTOs;
public class DailyReflectionDTO
{
    public string UserName { get; set; }
    public DateTime Date { get; set; }
    public string MostEnjoyablePart { get; set; }
    public string MostChallengingPart { get; set; }
    public string OverallFeeling { get; set; }
    public List<string> FrequentEmotions { get; set; }
    public string NewLearning { get; set; }
    public List<string> ThingsGratefulFor { get; set; }
    public List<EmotionalLabel> EmotionalLabels { get; set; }
}

public class EmotionalLabel
{
    public string Emotion { get; set; }
    public string Cause { get; set; }
    public DateTime Time { get; set; }
}

public class DailyReflection
{
    public static List<DailyReflectionDTO> usersReflections = new List<DailyReflectionDTO>
    {
        new DailyReflectionDTO
        {
            UserName = "Jane Doe",
            Date = DateTime.Today,
            MostEnjoyablePart = "Walking in the park",
            MostChallengingPart = "Dealing with a difficult client",
            OverallFeeling = "Tired but satisfied",
            FrequentEmotions = new List<string> { "Happy", "Frustrated", "Calm" },
            NewLearning = "A new technique for managing stress",
            ThingsGratefulFor = new List<string> { "Support from family", "Good health", "Nature" },
            EmotionalLabels = new List<EmotionalLabel>
            {
                new EmotionalLabel { Emotion = "Happy", Cause = "Walking in the park", Time = DateTime.Today.AddHours(10) },
                new EmotionalLabel { Emotion = "Frustrated", Cause = "Difficult client", Time = DateTime.Today.AddHours(14) },
                new EmotionalLabel { Emotion = "Calm", Cause = "Evening meditation", Time = DateTime.Today.AddHours(20) }
            }
        },
        new DailyReflectionDTO
        {
            UserName = "John Smith",
            Date = DateTime.Today,
            MostEnjoyablePart = "Completing a challenging task at work",
            MostChallengingPart = "Handling a personal issue",
            OverallFeeling = "Accomplished and relieved",
            FrequentEmotions = new List<string> { "Proud", "Stressed", "Relieved" },
            NewLearning = "Improved time management skills",
            ThingsGratefulFor = new List<string> { "Supportive friends", "Opportunities at work", "Good health" },
            EmotionalLabels = new List<EmotionalLabel>
            {
                new EmotionalLabel { Emotion = "Proud", Cause = "Completing task", Time = DateTime.Today.AddHours(11) },
                new EmotionalLabel { Emotion = "Stressed", Cause = "Personal issue", Time = DateTime.Today.AddHours(13) },
                new EmotionalLabel { Emotion = "Relieved", Cause = "Resolved issue", Time = DateTime.Today.AddHours(18) }
            }
        },
        new DailyReflectionDTO
        {
            UserName = "Alice Johnson",
            Date = DateTime.Today,
            MostEnjoyablePart = "Spending time with family",
            MostChallengingPart = "Balancing work and personal life",
            OverallFeeling = "Content and slightly overwhelmed",
            FrequentEmotions = new List<string> { "Loved", "Overwhelmed", "Grateful" },
            NewLearning = "New recipe for healthy meals",
            ThingsGratefulFor = new List<string> { "Family support", "Good health", "Work flexibility" },
            EmotionalLabels = new List<EmotionalLabel>
            {
                new EmotionalLabel { Emotion = "Loved", Cause = "Family time", Time = DateTime.Today.AddHours(9) },
                new EmotionalLabel { Emotion = "Overwhelmed", Cause = "Balancing tasks", Time = DateTime.Today.AddHours(14) },
                new EmotionalLabel { Emotion = "Grateful", Cause = "Support from family", Time = DateTime.Today.AddHours(19) }
            }
        }
    };
}

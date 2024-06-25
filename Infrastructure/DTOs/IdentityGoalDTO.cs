using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DTOs
{

    public class UserProfile
    {
        public IKIGAI Ikigai { get; set; }
        public string MBTI { get; set; }
        public List<string> TopStrengths { get; set; }
        public List<string> LowStrengths { get; set; }
        public WheelOfLife WheelOfLife { get; set; }
        public List<IdentityGoal> IdentityGoals { get; set; }
    }

    public class IKIGAI
    {
        public string Loves { get; set; }
        public string Needs { get; set; }
        public string GoodAt { get; set; }
        public string PaidFor { get; set; }
    }

    public class WheelOfLife
    {
        public int HealthFitness { get; set; }
        public int CareerWork { get; set; }
        public int PersonalGrowth { get; set; }
    }

    public class IdentityGoal
    {
        public string Name { get; set; }
        public string Vision { get; set; }
    }


    public class UserProfileList
    {
        public static List<UserProfile> userProfiles = new List<UserProfile>
        {
            new UserProfile
            {
                Ikigai = new IKIGAI { Loves = "writing", Needs = "creativity", GoodAt = "storytelling", PaidFor = "novels" },
                MBTI = "INFP",
                TopStrengths = new List<string> { "Creativity", "Empathy" },
                LowStrengths = new List<string> { "Punctuality" },
                WheelOfLife = new WheelOfLife { HealthFitness = 5, CareerWork = 7, PersonalGrowth = 8 },
                IdentityGoals = new List<IdentityGoal> { new IdentityGoal { Name = "Publish a book", Vision = "Become a recognized author" } }
            },
            new UserProfile
            {
                Ikigai = new IKIGAI { Loves = "cooking", Needs = "nutritional health", GoodAt = "gourmet dishes", PaidFor = "culinary arts" },
                MBTI = "ESFJ",
                TopStrengths = new List<string> { "Social Skills", "Organizational Ability" },
                LowStrengths = new List<string> { "Risk-taking" },
                WheelOfLife = new WheelOfLife { HealthFitness = 8, CareerWork = 6, PersonalGrowth = 7 },
                IdentityGoals = new List<IdentityGoal> { new IdentityGoal { Name = "Open a restaurant", Vision = "Create a popular local dining spot" } }
            },
            new UserProfile
            {
                Ikigai = new IKIGAI { Loves = "tech innovations", Needs = "technological advancement", GoodAt = "programming", PaidFor = "software development" },
                MBTI = "INTJ",
                TopStrengths = new List<string> { "Analytical Thinking", "Strategic Planning" },
                LowStrengths = new List<string> { "Emotional Expression" },
                WheelOfLife = new WheelOfLife { HealthFitness = 6, CareerWork = 9, PersonalGrowth = 5 },
                IdentityGoals = new List<IdentityGoal> { new IdentityGoal { Name = "Develop a new app", Vision = "Innovate in mobile technology" } }
            }
        };
    }
}

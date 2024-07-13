using Infrastructure.DTOs;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Infrastructure.Services;

public class MotivationService
{
    public MultipartFormDataContent General()
    {
        var users = UserList.users;
        var formData = new MultipartFormDataContent();

        var batchPayloadsBuilder = new StringBuilder();

        foreach (var (request, index) in users.Select((user, idx) => (user, idx)))
        {
            var batchPayload = new
            {
                custom_id = $"request-{DateTime.Now.Ticks}",
                method = "POST",
                url = "/v1/chat/completions",
                body = new
                {
                    model = "gpt-3.5-turbo-0125",
                    messages = new[]
                    {
                new { role = "system", content = "You are a motivational coach who provides uplifting messages based on the person's age and gender, and make it more personalized" },
                new { role = "user", content = $"I need a motivational quote for {request.Name}, who is {request.Gender} aged {request.Age}." }
            },
                    max_tokens = 1000
                }
            };

            string serializedObject = JsonSerializer.Serialize(batchPayload);

            batchPayloadsBuilder.AppendLine(serializedObject);
        }

        var content = new ByteArrayContent(Encoding.UTF8.GetBytes(batchPayloadsBuilder.ToString()));
        content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        formData.Add(content, "file", "batch.json");

        formData.Add(new StringContent("batch"), "purpose");

        return formData;
    }

    public MultipartFormDataContent Morning()
    {
        var goals = UserProfileList.userProfiles;
        var formData = new MultipartFormDataContent();

        var batchPayloadsBuilder = new StringBuilder();

        foreach (var (request, index) in goals.Select((user, idx) => (user, idx)))
        {
            var batchPayload = new
            {
                custom_id = $"request-{DateTime.Now.Ticks}",
                method = "POST",
                url = "/v1/chat/completions",
                body = new
                {
                    model = "gpt-3.5-turbo-0125",
                    messages = new[]
                    {
                    new {
                        role = "system",
                        content = "You are a helpful assistant tasked with providing a personalized morning motivation message." +
                        " Use the user's detailed profile to craft a message that is inspiring and relevant. Consider:\n" +
                        "- IKIGAI elements to connect the motivation with their passions and what they can be paid for, ensuring they are encouraged to pursue what they love and excel at.\n" +
                        "- MBTI personality type to adjust the tone and approach of the motivation to match their personality traits, making it resonate more deeply.\n" +
                        "- Top Strengths to affirm their abilities and boost confidence, while using Low Strengths to suggest areas for growth and improvement.\n" +
                        "- Wheel of Life to address specific life domains that need attention, encouraging actions that balance and enhance their overall life satisfaction.\n" +
                        "- Identity Goals to remind them of their long-term aspirations, linking daily actions to these overarching goals for a sense of purpose and direction."
                    },
                    new {
                        role = "user",
                        content = $"Generate a morning motivation message using the profile data: {JsonSerializer.Serialize(request)}"
                    }
                },
                    max_tokens = 1000
                }
            };

            string serializedObject = JsonSerializer.Serialize(batchPayload);

            batchPayloadsBuilder.AppendLine(serializedObject);
        }

        var content = new ByteArrayContent(Encoding.UTF8.GetBytes(batchPayloadsBuilder.ToString()));
        content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        formData.Add(content, "file", "batch.json");

        formData.Add(new StringContent("batch"), "purpose");

        return formData;
    }

    public MultipartFormDataContent Evening()
    {
        var reflections = DailyReflection.usersReflections;
        var userProfiles = UserProfileList.userProfiles;
        var formData = new MultipartFormDataContent();

        var batchPayloadsBuilder = new StringBuilder();

        foreach (var (userProfile, index) in userProfiles.Select((userProfile, idx) => (userProfile, idx)))
        {
            var batchPayload = new
            {
                custom_id = $"request-{DateTime.Now.Ticks}",
                method = "POST",
                url = "/v1/chat/completions",
                body = new
                {
                    model = "gpt-3.5-turbo-0125",
                    messages = new[]
        {
            new
            {
                role = "system",
                content = @"You are a helpful assistant tasked with providing an evening notification message. Use the user's detailed profile to craft messages that are reflective, consolidate learning, and prepare them for tomorrow, specifically:
                                - Reflective Questions: Encourage introspection on how today's actions connected to broader goals.
                                - Progress Feedback: Highlight today's achievements and areas for improvement.
                                - Encouragement for the Next Day: Motivate the user to continue their progress.
                                - Emotional Labeling Support: Help the user identify and manage their emotions based on the day's events.
                                Consider the user's IKIGAI, MBTI, strengths, Wheel of Life, and identity goals to tailor the content effectively."
            },
            new
            {
                role = "user",
                content = $"Generate an evening notification message using the profile data: {JsonSerializer.Serialize(userProfile)}"
            }
        },
                    max_tokens = 1000
                }
            };

            string serializedObject = JsonSerializer.Serialize(batchPayload);

            batchPayloadsBuilder.AppendLine(serializedObject);
        }

        var content = new ByteArrayContent(Encoding.UTF8.GetBytes(batchPayloadsBuilder.ToString()));
        content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        formData.Add(content, "file", "batch.json");

        formData.Add(new StringContent("batch"), "purpose");

        return formData;
    }

}

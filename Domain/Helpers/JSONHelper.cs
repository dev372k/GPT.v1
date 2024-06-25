using System.Text.Json;

namespace Domain.Helpers
{
    public class JSONHelper
    {
        public static async Task<List<BatchOutputFileModel>> Read(string content)
        {
            List<BatchOutputFileModel> results = new List<BatchOutputFileModel>();

            using (StringReader reader = new StringReader(content))
            {
                string line;
                while ((line = await reader.ReadLineAsync()) != null)
                {
                    try
                    {
                        BatchOutputModel jsonLine = JsonSerializer.Deserialize<BatchOutputModel>(line);
                        results.Add(new BatchOutputFileModel
                        {
                            id = jsonLine.id,
                            custom_id = jsonLine.custom_id,
                            request_id = jsonLine.response?.request_id,
                            content = jsonLine.response?.body?.choices?[0]?.message?.content
                        });
                    }
                    catch (JsonException ex)
                    {
                        return null;
                    }
                }
            }

            return results;
        }

    }
}

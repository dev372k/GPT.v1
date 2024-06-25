namespace Domain.Helpers
{
    public class BatchOutputModel
    {
        public string id { get; set; }
        public string custom_id { get; set; }
        public Response response { get; set; }
    }

    public class Response
    {
        public string request_id { get; set; }
        public Body body { get; set; }
    }

    public class Body
    {
        public Choice[] choices { get; set; }
    }

    public class Choice
    {
        public Message message { get; set; }
    }

    public class Message
    {
        public string content { get; set; }
    }

    public class BatchOutputFileModel
    {
        public string id { get; set; }
        public string custom_id { get; set; }
        public string request_id { get; set; }
        public string content { get; set; }
    }
}


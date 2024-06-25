namespace Infrastructure.DTOs.BatchDTOs
{
    public class BatchResponse
    {
        public string id { get; set; }
        public string input_file_id { get; set; }
        public string output_file_id { get; set; }
        public string status { get; set; }
    }
}

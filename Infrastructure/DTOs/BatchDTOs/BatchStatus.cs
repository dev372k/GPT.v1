namespace Infrastructure.DTOs.BatchDTOs
{
    public enum BatchStatus
    {
        Validating,
        Failed,
        InProgress,
        Finalizing,
        Completed,
        Expired,
        Cancelling,
        Cancelled
    }

}

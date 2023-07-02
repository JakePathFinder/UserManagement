namespace UserManagement.DTO
{
    public class BulkOperationRequest
    {
        public OperationType OperationType { get; set; }
        public required string InputFile { get; set; }
    }
}

namespace UserManagement.DTO
{
    public class BulkOperationResponse
    {
        public List<string> Errors { get; set; }
        public List<Guid> Succeeded { get; set; } 
        public List<Guid> Failed { get; set; }

        public int TotalRequests { get; set; }
        public int TotalSucceededRequests { get; set; }
        public int TotalFailedRequests { get; set; }

        public BulkOperationResponse()
        {
            Errors = new List<string>();
            Succeeded = new List<Guid>();
            Failed = new List<Guid>();
            TotalRequests = TotalSucceededRequests = TotalFailedRequests = 0;
        }

        public static BulkOperationResponse From<TResponseDTO>(List<Response<TResponseDTO>> responses) where TResponseDTO : class, IIdEntityDto
        {
            var bulkResponse = new BulkOperationResponse();
            responses.ForEach(response =>
            {
                bulkResponse.TotalRequests += 1;
                if (response.Success)
                {
                    bulkResponse.TotalSucceededRequests += 1;
                    if (response.Result != null)
                    {
                        bulkResponse.Succeeded.Add(response.Result.Id);
                    }
                }
                else
                {
                    bulkResponse.TotalFailedRequests += 1;
                    bulkResponse.Errors.Add(response.Message);
                    if (response.Result != null)
                    {
                        bulkResponse.Failed.Add(response.Result.Id);
                    }
                }
            });
            return bulkResponse;
        }
    }
}

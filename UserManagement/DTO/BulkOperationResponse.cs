namespace UserManagement.DTO
{
    public class BulkOperationResponse<TResponseDTO> where TResponseDTO: class, IIdEntityDto
    {
        public List<TResponseDTO> Items { get; set; }
        public List<Guid> FailedIds { get; set; }
        public List<string> Errors { get; set; }

        public int TotalRequests { get; set; }
        public int TotalSucceededRequests { get; set; }
        public int TotalFailedRequests { get; set; }

        public BulkOperationResponse()
        {
            Errors = new List<string>();
            Items = new List<TResponseDTO>();
            FailedIds = new List<Guid>();
            TotalRequests = TotalSucceededRequests = TotalFailedRequests = 0;
        }

        public static BulkOperationResponse<TResponseDTO> From(List<Response<TResponseDTO>> responses)
        {
            var bulkResponse = new BulkOperationResponse<TResponseDTO>();
            responses.ForEach(response =>
            {
                bulkResponse.TotalRequests += 1;
                if (response.Success)
                {
                    bulkResponse.TotalSucceededRequests += 1;
                    if (response.Result != null)
                    {
                        bulkResponse.Items.Add(response.Result);
                    }
                }
                else
                {
                    bulkResponse.TotalFailedRequests += 1;
                    if (response.Message != null)
                    {
                        bulkResponse.Errors.Add(response.Message);
                    }

                    if (response?.Result?.Id != null)
                    {
                        bulkResponse.FailedIds.Add(response.Result.Id);
                    }
                }
            });
            return bulkResponse;
        }

        public static BulkOperationResponse<TResponseDTO> From(Exception ex)
        {
            var error = $"Bulk operation Failed due to: {ex.Message}";
            return new BulkOperationResponse<TResponseDTO>()
            {
                Errors = new List<string> { error },
                TotalFailedRequests = 1,
                TotalRequests = 1
            };
        }

        public static BulkOperationResponse<TResponseDTO> From(List<TResponseDTO> entities)
        {
            return new BulkOperationResponse<TResponseDTO>()
            {
                TotalSucceededRequests = entities.Count,
                TotalRequests = entities.Count,
                Items = entities
            };
        }

        public bool Success()
        {
            return !Errors.Any();
        }
    }
}

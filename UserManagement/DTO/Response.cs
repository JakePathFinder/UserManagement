namespace UserManagement.DTO
{
    public class Response<TResponseDTO> where TResponseDTO : class
    {
        public bool Success {get; set; }
        public TResponseDTO? Result { get; set; }

        public string? Message { get; set; }

        public static Response<TResponseDTO> From(TResponseDTO entity, string? message = null)
        {
            return new Response<TResponseDTO>
            {
                Success = true,
                Result = entity,
                Message = message
            };
        }

        public static Response<TResponseDTO> From(Exception exception, string? message)
        {
            return new Response<TResponseDTO>
            {
                Success = false,
                Message = string.IsNullOrEmpty(message) ? exception.Message : message
            };
        }

        public static Response<TResponseDTO> From(bool result, string? message)
        {
            return new Response<TResponseDTO>
            {
                Success = result,
                Message = message
            };
        }
    }
}

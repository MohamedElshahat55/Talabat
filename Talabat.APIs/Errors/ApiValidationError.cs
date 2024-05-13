namespace Talabat.APIs.Errors
{
    public class ApiValidationError : ApiResponse
    {
        // Validation Error always has a status code = 400
        public IEnumerable<string> Errors { get; set; }
        public ApiValidationError():base(400)
        {
            Errors = new List<string>();
        }
    }
}

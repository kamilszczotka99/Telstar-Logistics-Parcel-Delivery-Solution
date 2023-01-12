

namespace Models
{
    public class ErrorResponseDto
    {

        public string error_description { get; set; }

        public ErrorResponseDto(string error_description)
        {
            this.error_description = error_description;
        }

    }
}
namespace ERP.Identity.Application.DTOs.Requests
{
    public class CreateUserRequest
    {
        // persons
        public string? first_name { get; set; }
        public string? last_Name { get; set; }
        public string? document_type { get; set; }
        public string? email { get; set; }
        public DateTime birth_date { get; set; }

        // users
        public string? user_name { get; set; }
        public string? password { get; set; }
    }
}
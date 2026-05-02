namespace ERP.Identity.Application.DTOs.Requests
{
    public class CreateUserRequest
    {
        // persons
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DocumentType { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }

        // users
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
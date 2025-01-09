using System.ComponentModel.DataAnnotations;

namespace People.Application.DTOs
{
    public class PersonDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Firstname { get; set; }
    }
}

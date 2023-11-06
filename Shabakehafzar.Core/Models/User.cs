using System.ComponentModel.DataAnnotations.Schema;

namespace Shabakehafzar.Core.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [NotMapped]
        public string FullName 
        {
            get { return $"{FirstName} {LastName}"; } 
        }
    }
}

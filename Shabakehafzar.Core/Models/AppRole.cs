namespace Shabakehafzar.Core.Models
{
    public class AppRole : IdentityRole<string>
    {
        public AppRole(string name)
        {
            this.Name = name;
        }
        public DateTime CreatedDate { get; set; }
    }
}

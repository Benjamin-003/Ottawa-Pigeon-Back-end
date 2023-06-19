using System.ComponentModel.DataAnnotations;

namespace Ottawa.Pigeon.Domain.Entities
{
    public class Currency
    {
        public string Code { get; set; } = string.Empty;
        public string Label { get; set; } = string.Empty;
        public string Flag { get; set; } = string.Empty;

        public virtual ICollection<User> Users { get; set; }
        public Currency()
        {
            Users = new HashSet<User>();
        }
    }
}

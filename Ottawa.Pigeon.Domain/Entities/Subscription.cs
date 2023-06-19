
namespace Ottawa.Pigeon.Domain.Entities
{
    public class Subscription
    {
        public string Code { get; set; } = string.Empty;
        public string Label { get; set; } = string.Empty;
        public bool IsDefault { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public Subscription()
        {
            Users = new HashSet<User>();
        }
    }
}

using System.ComponentModel.DataAnnotations;

namespace Ottawa.Pigeon.Domain.Entities
{
    public class Language
    {
        public string Code { get; set; } = string.Empty;
        public string Label { get; set; } = string.Empty;

        public virtual ICollection<User> Users { get; set; } // virtual = peut overrider la definition pour EF

        // Cstor pour que EF charge la liste de User (pour lazy loading)
        public Language()
        {
            Users = new HashSet<User>();
        }
    }
}

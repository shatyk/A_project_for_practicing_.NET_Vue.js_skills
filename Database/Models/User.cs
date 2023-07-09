using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public ICollection<RefreshToken> RefreshTokens { get; init; }
        
        public User()
        {
            RefreshTokens = new HashSet<RefreshToken>();
        }
    }
}

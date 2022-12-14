using Persistence.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Entities;

/// <summary>
/// User entity
/// </summary>
public class User : BaseEntity
{
    /// <summary>
    /// Id
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Name of User
    /// </summary>
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// Byte array of the hashset of the user password
    /// </summary>
    public byte[] PasswordHash { get; set; }

    /// <summary>
    /// Byte array of the user password (ne znam kvo e trqq 4ekna)
    /// </summary>
    public byte[] PasswordSalt { get; set; }

    /// <summary>
    /// All cars for a user (nav prop)
    /// </summary>
    public ICollection<Recipe> Favourites { get; set; } = new List<Recipe>();
}

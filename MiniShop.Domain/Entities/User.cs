using MiniShop.Domain.Enums;
using System;

namespace MiniShop.Domain.Entities;

public class User
{
    public Guid UserId { get; private set; }
    public string UserName { get; set; }
    public UserRole Role { get; set; }

    public User(string userName, UserRole role)
    {
        UserId = Guid.NewGuid();
        UserName = userName;
        Role = role;
    }
}

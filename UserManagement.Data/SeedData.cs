using System;
using UserManagement.Models;

namespace UserManagement.Data;

internal static class SeedData
{
    internal static readonly User[] Users =
    [
        new User
        {
            Id          = 1,
            Forename    = "Peter",
            Surname     = "Loew",
            Email       = "ploew@example.com",
            IsActive    = true,
            DateOfBirth = new DateOnly(2002, 9, 27),
        },
        new User
        {
            Id          = 2,
            Forename    = "Benjamin Franklin",
            Surname     = "Gates",
            Email       = "bfgates@example.com",
            IsActive    = true,
            DateOfBirth = new DateOnly(1970, 11, 12),
        },
        new User
        {
            Id          = 3,
            Forename    = "Castor",
            Surname     = "Troy",
            Email       = "ctroy@example.com",
            IsActive    = false,
            DateOfBirth = new DateOnly(2005, 6, 30),
        },
        new User
        {
            Id          = 4,
            Forename    = "Memphis",
            Surname     = "Raines",
            Email       = "mraines@example.com",
            IsActive    = true,
            DateOfBirth = new DateOnly(1962, 7, 28),
        },
        new User
        {
            Id          = 5,
            Forename    = "Stanley",
            Surname     = "Goodspeed",
            Email       = "sgodspeed@example.com",
            IsActive    = true,
            DateOfBirth = new DateOnly(1992, 12, 3),
        },
        new User
        {
            Id          = 6,
            Forename    = "H.I.",
            Surname     = "McDunnough",
            Email       = "himcdunnough@example.com",
            IsActive    = true,
            DateOfBirth = new DateOnly(2004, 2, 14),
        },
        new User
        {
            Id          = 7,
            Forename    = "Cameron",
            Surname     = "Poe",
            Email       = "cpoe@example.com",
            IsActive    = false,
            DateOfBirth = new DateOnly(1955, 3, 15),
        },
        new User
        {
            Id          = 8,
            Forename    = "Edward",
            Surname     = "Malus",
            Email       = "emalus@example.com",
            IsActive    = false,
            DateOfBirth = new DateOnly(1978, 4, 29),
        },
        new User
        {
            Id          = 9,
            Forename    = "Damon",
            Surname     = "Macready",
            Email       = "dmacready@example.com",
            IsActive    = false,
            DateOfBirth = new DateOnly(2003, 1, 15),
        },
        new User
        {
            Id          = 10,
            Forename    = "Johnny",
            Surname     = "Blaze",
            Email       = "jblaze@example.com",
            IsActive    = true,
            DateOfBirth = new DateOnly(1985, 8, 16),
        },
        new User
        {
            Id          = 11,
            Forename    = "Robin",
            Surname     = "Feld",
            Email       = "rfeld@example.com",
            IsActive    = true,
            DateOfBirth = new DateOnly(1999, 5, 20),
        }
    ];
}

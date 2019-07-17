using System;
using BlogProject.Models;
using Microsoft.AspNet.Identity;

namespace BlogProject.Controllers
{
    internal class ApplicationUserManager<T>
    {
        public static implicit operator ApplicationUserManager<T>(UserManager<ApplicationUser>v)
        {
            throw new NotImplementedException();
        }

        internal object FindByName(string name)
        {
            throw new NotImplementedException();
        }
    }
}
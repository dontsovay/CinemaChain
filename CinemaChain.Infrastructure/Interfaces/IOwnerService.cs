using CinemaChain.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CinemaChain.Infrastructure.Interfaces
{
    public interface IOwnerService
    {
        IEnumerable<Owners> GetOwners();
        Owners CreateOwner(Owners owners);
        // int GetIdOwner(string enterted_username);
    }
}

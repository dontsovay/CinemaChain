using CinemaChain.Models.Models;
using CinemaChain.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CinemaChain.Infrastructure.Interfaces
{
    public interface IUserService
    {
        string USERNAME { get; set; }
        int id_owner { get; set; }
        IEnumerable<Users> getUsers();
        int IsAdmin(Role entered_role);
        bool IsUserExists(string entered_username);
        Role WhatRoleOfUser(string entered_username);
        IEnumerable<Users> FindUsers(Func<Users, bool> func);
        int WhatRoleOfUsers(string entered_username);
        Users GetById(string username);
        Users GetByIdNum(string password);
        Users UpdateUser(Users user);
        void DeleteUser(string username);
        void CreateUser(RegistrationVM users, string id);
        Users LOGIN(string username, string password);
        string HASH(string password);
        void CreateUserOwn(RegistrationVM users, string id);
        void CreateUserAdm(RegistrationVM users, string id);
    }
}

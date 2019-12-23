using CinemaChain.Data.Context;
using CinemaChain.Data.Repositories;
using CinemaChain.Infrastructure.Interfaces;
using CinemaChain.Models.Models;
using CinemaChain.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CinemaChain.Infrastructure.Services
{
    public class UserService : IUserService
    {
        public string USERNAME { get; set; }
        public int id_owner { get; set; }

        private UnitOfWork unitOfWork;
        private readonly IAdminService adminService;
        private readonly IClientService clientService;
        private readonly IOwnerService ownerService;


        public UserService(IAdminService adminService, IClientService clientService, IOwnerService ownerService, DB_Context context)
        {
            this.adminService = adminService;
            this.clientService = clientService;
            this.ownerService = ownerService;
            unitOfWork = new UnitOfWork(context);
        }

        public IEnumerable<Users> getUsers()
        {
            return unitOfWork.Users.GetAll();

        }

        public int IsAdmin(Role entered_role)
        {
            int m;
            if (entered_role == Role.ADMIN)
            {
                m = 1;
            }
            else if (entered_role == Role.CLIENT)
            {
                m = 0;
            }
            else if (entered_role == Role.OWNER)
            {
                m = 2;
            }
            else m = -1;
            return m;
        }

        public bool IsUserExists(string entered_username)
        {
            var data = unitOfWork.Users.GetBy(u => u.UserName.Equals(entered_username));
            //string user = data.Username;
            if (data == null)
            {
                return false;
            }
            else return true;
        }
        public Role WhatRoleOfUser(string entered_username)
        {
            if (entered_username == null)
            {
                return Role.CLIENT;
            }
            else
            {
                var data = unitOfWork.Users.Get(entered_username);
                return data.Role;
            };
        }
        public int WhatRoleOfUsers(string entered_username)
        {
            if (entered_username == null)
            {
                return 0;
            }
            else
            {
                var data = unitOfWork.Users.Get(entered_username);
                Role role = data.Role;
                if (role == Role.ADMIN)
                {
                    return 1;
                }
                else if (role == Role.OWNER)
                {
                    return 2;
                }
                else
                    return 0;
            };
        }

        public IEnumerable<Users> FindUsers(Func<Users, bool> func)
        {
            return unitOfWork.Users.GetBy(func);
        }
        public Users GetById(string username)
        {
            return unitOfWork.Users.Get(username);
        }
        public string HASH(string password)
        {
            return unitOfWork.Users.HASH(password);
        }
        public Users GetByIdNum(string username)
        {
            return unitOfWork.Users.GetUser(username);
        }
        public Users LOGIN(string username, string password)
        {
            return unitOfWork.Users.CustomGet(username, password);

        }

        public Users UpdateUser(Users user)
        {
            if (user == null)
            {
                throw new ArgumentNullException();
            }

            return unitOfWork.Users.Update(user);
        }

        public void DeleteUser(string username)
        {
            if (username == null)
            {
                throw new ArgumentNullException();
            }
            unitOfWork.Users.Delete(username);
        }

        public void CreateUser(RegistrationVM users, string id)
        {

            Clients client = new Clients
            {
                FIO = users.FIO,
                Phone = users.Phone,
                Address = users.Address,
                UserName = users.Username,
                Users = new Users()
                {
                    Id = id
                }
            };


            clientService.CreateClient(client);

        }
        public void CreateUserAdm(RegistrationVM users, string id)
        {

            Admins admin = new Admins
            {
                FIO = users.FIO,
                Phone = users.Phone,
                Address = users.Address,
                UserName = users.Username,
                Users = new Users()
                {
                    Id = id
                }
            };

            adminService.CreateAdmin(admin);
        }
        public void CreateUserOwn(RegistrationVM users, string id)
        {

            Owners owner = new Owners
            {
                FIO = users.FIO,
                Phone = users.Phone,
                Address = users.Address,
                UserName = users.Username,
                Users = new Users()
                {
                    Id = id
                }
            };

            ownerService.CreateOwner(owner);
        }






        //public bool LogIn(string entered_username, string entered_password)
        //{
        //    int id=0;
        //    var isexist = IsUserExists(entered_username);
        //    if (isexist != false)
        //    {

        //        USERNAME = entered_username;
        //        if(WhatRoleOfUser(entered_username)==Role.OWNER)
        //        {
        //            var datas = unitOfWork.Owners.GetAll().Where(idd => idd.Id_Owner.Equals(entered_username)).FirstOrDefault();
        //            id = datas.Id_Owner;
        //        }
        //        if(id!=0)
        //        {
        //            id_owner = id;
        //        }
        //        return true;
        //    }
        //    else return false;
        //}

        public bool LogOut()
        {

            USERNAME = null;
            id_owner = 0;
            return true;

        }
    }
}

﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using SalesSystem.Areas.Users.Models;
using SalesSystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesSystem.Library
{
    public class LUser : ListObject
    {

        public LUser(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _context = context;
            _usersRoles = new LUsersRoles();

        }
        public async Task<List<InputModelRegister>> getTUsuariosAsync(string valor, int id)
        {
            List<TUsers> listUser;
            List<SelectListItem> _listRoles;
            List<InputModelRegister> userList = new List<InputModelRegister>();
            if (valor == null && id.Equals(0))
            {
                listUser = _context.TUsers.ToList();
            }
            else
            {
                if (id.Equals(0))
                {
                    listUser = _context.TUsers.Where(u => u.NID.StartsWith(valor) || u.Name.StartsWith(valor)
                    || u.LastName.StartsWith(valor) || u.Email.StartsWith(valor)).ToList();

                }
                else
                {
                    listUser = _context.TUsers.Where(u => u.ID.Equals(id)).ToList();
                }
            }
            if (!listUser.Count.Equals(0))
            {
                foreach (var item in listUser)
                {
                    _listRoles = await _usersRoles.getRole(_userManager, _roleManager, item.IdUSer);//podria fallar
                    var user = _context.Users.Where(u => u.Id.Equals(item.IdUSer)).ToList().Last();
                    userList.Add(new InputModelRegister
                    {
                        Id = item.ID,
                        ID = item.IdUSer,
                        NID = item.NID,
                        Name = item.Name,
                        LastName = item.LastName,
                        Email = item.Email,
                        Role = _listRoles[0].Text,
                        Image = item.Image,
                        IdentityUser = user
                    });
                    _listRoles.Clear();
                }
            }

            return userList;
        }
    }

}


﻿using System;
using System.Collections.Generic;
using System.Linq;
using Oracle.DataAccess.Client;

namespace AspNet.Identity.Oracle
{
    /// <summary>
    /// Class that represents the Role table in the Oracle Database
    /// </summary>
    public class RoleTable
    {
        private readonly OracleDatabase _database;

        /// <summary>
        /// Constructor that takes a Oracle Database instance 
        /// </summary>
        /// <param name="database"></param>
        public RoleTable(OracleDatabase database)
        {
            _database = database;
        }

        /// <summary>
        /// Deltes a role from the Roles table
        /// </summary>
        /// <param name="roleId">The role Id</param>
        /// <returns></returns>
        public int Delete(string roleId)
        {
            const string commandText = @"DELETE FROM IDSSO_ROLES WHERE ID = :ID";
            var parameters = new List<OracleParameter>
            {
                new OracleParameter {ParameterName = "ID", Value = roleId, OracleDbType = OracleDbType.Varchar2 },
            };

            return _database.Execute(commandText, parameters);
        }

        /// <summary>
        /// Inserts a new Role in the Roles table
        /// </summary>
        /// <param name="role">The role's name</param>
        /// <returns></returns>
        public int Insert(IdentityRole role)
        {
            const string commandText = @"INSERT INTO IDSSO_ROLES (ID, NAME, DESCRIPTION) VALUES (:ID, :NAME, :DESCRIPTION)";
            var parameters = new List<OracleParameter>
            {
                new OracleParameter {ParameterName = "ID", Value = role.Id, OracleDbType = OracleDbType.Varchar2 },
                new OracleParameter {ParameterName = "NAME", Value = role.Name, OracleDbType = OracleDbType.Varchar2 },
                new OracleParameter {ParameterName = "DESCRIPTION", Value = role.Description, OracleDbType = OracleDbType.Varchar2 },
            };

            return _database.Execute(commandText, parameters);
        }

        /// <summary>
        /// Returns a role name given the roleId
        /// </summary>
        /// <param name="roleId">The role Id</param>
        /// <returns>Role name</returns>
        public string GetRoleName(string roleId)
        {
            const string commandText = @"SELECT NAME FROM IDSSO_ROLES WHERE ID = :ID";
            var parameters = new List<OracleParameter>
            {
                new OracleParameter {ParameterName = "ID", Value = roleId, OracleDbType = OracleDbType.Varchar2 },
            };

            return _database.GetStrValue(commandText, parameters);
        }

        /// <summary>
        /// Returns the role Id given a role name
        /// </summary>
        /// <param name="roleName">Role's name</param>
        /// <returns>Role's Id</returns>
        public string GetRoleId(string roleName)
        {
            const string commandText = @"SELECT ID FROM IDSSO_ROLES WHERE NAME = :NAME";
            var parameters = new List<OracleParameter>
            {
                new OracleParameter {ParameterName = "NAME", Value = roleName, OracleDbType = OracleDbType.Varchar2 },
            };

            var result = _database.QueryValue(commandText, parameters);
            return result != null ? Convert.ToString(result) : null;
        }

        /// <summary>
        /// Gets the IdentityRole given the role name
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public IdentityRole GetRoleByName(string roleName)
        {
            var roleId = GetRoleId(roleName);
            IdentityRole role = null;

            if (roleId != null)
            {
                role = new IdentityRole(roleName, roleId);
            }

            return role;
        }

        /// <summary>
        /// Update Role's attributes
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public int Update(IdentityRole role)
        {
            const string commandText = @"UPDATE IDSSO_ROLES SET NAME = :NAME , DESCRIPTION = :DESCRIPTION WHERE ID = :ID";
            var parameters = new List<OracleParameter>
            {
                new OracleParameter {ParameterName = "NAME", Value = role.Name, OracleDbType = OracleDbType.Varchar2 },
                new OracleParameter {ParameterName = "ID", Value = role.Id, OracleDbType = OracleDbType.Varchar2 },
                new OracleParameter {ParameterName = "DESCRIPTION", Value = role.Description, OracleDbType = OracleDbType.Varchar2 },
            };

            return _database.Execute(commandText, parameters);
        }

        /// <summary>
        /// Get the all Roles
        /// </summary>
        /// <returns>IdentityRole</returns>
        public IEnumerable<IdentityRole> GetRoles()
        {
            const string commandText = @"SELECT ID, NAME, DESCRIPTION FROM IDSSO_ROLES";
            var results = _database.Query(commandText, null);

            return results.Select(result => new IdentityRole
            {
                Id = string.IsNullOrEmpty(result["ID"]) ? null : result["ID"],
                Name = string.IsNullOrEmpty(result["NAME"]) ? null : result["NAME"],
                Description = string.IsNullOrEmpty(result["DESCRIPTION"]) ? null : result["DESCRIPTION"]

            }).ToList();
        }

        public IdentityRole GetRoleById(string id)
        {
            const string commandText = @"SELECT ID, NAME, DESCRIPTION FROM IDSSO_ROLES WHERE ID = :ID";
            var parameters = new List<OracleParameter>
            {
                new OracleParameter {ParameterName = "ID", Value = id, OracleDbType = OracleDbType.Varchar2 },
            };

            var results = _database.Query(commandText, parameters);

            return results.Select(result => new IdentityRole
            {
                Id = string.IsNullOrEmpty(result["ID"]) ? null : result["ID"],
                Name = string.IsNullOrEmpty(result["NAME"]) ? null : result["NAME"],
                Description = string.IsNullOrEmpty(result["DESCRIPTION"]) ? null : result["DESCRIPTION"]

            }).First();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineLibrary.Utility
{
    public static class SD
    {
        public const string Proc_Author_Create = "usp_CreateAuthor";
        public const string Proc_Author_Get = "usp_GetAuthor";
        public const string Proc_Author_GetAll = "usp_GetAuthors";
        public const string Proc_Author_Update = "usp_UpdateAuthor";
        public const string Proc_Author_Delete = "usp_DeleteAuthor";

        public const string Role_User_Indi = "Individual Customer";
        public const string Role_Admin = "Admin";
        public const string Role_Employee = "Employee";
    }
}

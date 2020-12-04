using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductRegister.Models
{
    public class User
    {
        #region Atributos privados
        private string _Login;
        private string _Password;
        #endregion

        #region Atributos Publicos
        public string Login { get { return this._Login; } set { this._Login = value; } }
        public string Password { get { return this._Password; } set { this._Password = value; } }
        #endregion
    }
}

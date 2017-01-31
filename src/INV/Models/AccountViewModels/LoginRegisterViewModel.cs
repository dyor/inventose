using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INV.Models.AccountViewModels
{
    public class LoginRegisterViewModel
    {
        public Models.AccountViewModels.LoginViewModel LoginViewModel { get; set; }
        public Models.AccountViewModels.RegisterViewModel RegisterViewModel { get; set; }
    }
}

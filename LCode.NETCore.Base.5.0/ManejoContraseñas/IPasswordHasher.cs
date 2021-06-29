using System;
using System.Collections.Generic;
using System.Text;

namespace LCode.NETCore.Base._5._0.ManejoContraseñas
{
    public interface IPasswordHasher
    {
        string Hash(string Contrasena);

        bool Check(string hash, string Contrasena);
    }
}

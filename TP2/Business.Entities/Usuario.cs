using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Entities
{
    public class Usuario:BusinessEntity
    {
        private string _Apellido;
        private string _Nombre;
        private string _Clave;
        private string _Email;
        private string _NombreUsuario;
        private bool _Habilitado;
        private int _ID;

        public string Apellido
        {
            get { return _Apellido; }
            set { _Apellido = value; }
        }
        public string Nombre
        {
            get { return _Nombre; }
            set { _Nombre = value; }
        }

        public string Clave
        {
            get { return _Clave; }
            set { _Clave = value; }
        }

        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }
        public string NombreUsuario
        {
            get { return _NombreUsuario; }
            set { _NombreUsuario = value; }
        }
        public bool Habilitado
        {
            get { return _Habilitado; }
            set { _Habilitado = value; }
        }

        public int ID
        {
            get
            { return _ID; }

            set
            { _ID = value; }
        }
    }
}


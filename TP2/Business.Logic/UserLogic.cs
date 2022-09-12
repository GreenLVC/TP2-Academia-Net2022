using Data.Database;
using Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Business.Logic
{
    public class UserLogic: BusinessLogic
    {
        private UsuarioAdapter _UsuarioData = new UsuarioAdapter();

        public UsuarioAdapter UsuarioData
        {
            get { return _UsuarioData; }
            set { _UsuarioData = value; }
        }


        public List<Usuario> GetAll()
        {

            return _UsuarioData.GetAll();
        }
        public Usuario GetOne(int ID)
        {
            return _UsuarioData.GetOne(ID);
        }


        public void Delete(int ID)
        {
            _UsuarioData.Delete(ID);
        }
        public void Save(Usuario usuario)
        {
            _UsuarioData.Save(usuario);
        }
        
    }
}

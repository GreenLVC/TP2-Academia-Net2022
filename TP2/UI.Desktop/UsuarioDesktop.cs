using Business.Entities;
using Business.Logic;
using System;
using System.Net.Mail;
using System.Windows.Forms;

namespace UI.Desktop
{
    public partial class UsuarioDesktop : ApplicationForm
    {
        private Usuario usuarioactual;
        public Usuario UsuarioActual { get => usuarioactual; set => usuarioactual = value; }

        public UsuarioDesktop()
        {
            InitializeComponent();
        }
        public UsuarioDesktop(ModoForm modo) : this()
        {
            this.Modo = modo;
        }
        public UsuarioDesktop(int idUsuario, ModoForm modo) : this()
        {
            this.Modo = modo;
            UserLogic usuarioLogic = new UserLogic();
            UsuarioActual = usuarioLogic.GetOne(idUsuario);

            MapearDeDatos();
        }
        public override void GuardarCambios()
        {
            base.GuardarCambios();
            MapearADatos();
            UserLogic usuarioLogic = new UserLogic();
            try
            {
                usuarioLogic.Save(UsuarioActual);
            }
            catch (Exception e)
            {

                Notificar("Ha ocurrido un error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public override void MapearADatos()
        {
            base.MapearADatos();
            if (Modo == ModoForm.Alta)
            {
                UsuarioActual = new Usuario();
            }
            else
            { 
                UsuarioActual.ID = int.Parse(txtID.Text);
            }
            UsuarioActual.Habilitado = cchkHabilitadp.Checked;
            UsuarioActual.Nombre = txtNombre.Text;
            UsuarioActual.Apellido = txtApellido.Text;
            UsuarioActual.Email = txtEmail.Text;
            UsuarioActual.NombreUsuario = txtUsuario.Text;
            UsuarioActual.Clave = txtClave.Text;

            if (Modo == ModoForm.Alta)
            {
                UsuarioActual.State = BusinessEntity.States.New;
            }
            else
            {
                UsuarioActual.State = BusinessEntity.States.Modified;
            }
            UsuarioActual.State = (Modo == ModoForm.Alta ? BusinessEntity.States.New : BusinessEntity.States.Modified);
        }

        public override void MapearDeDatos()
        {
            base.MapearDeDatos();
            txtID.Text = UsuarioActual.ID.ToString();
            cchkHabilitadp.Checked = UsuarioActual.Habilitado;
            txtNombre.Text = UsuarioActual.Nombre;
            txtApellido.Text = UsuarioActual.Apellido;
            txtEmail.Text = UsuarioActual.Email;
            txtUsuario.Text = UsuarioActual.NombreUsuario;
            txtClave.Text = UsuarioActual.Clave;
            txtConfirmarClave.Text = UsuarioActual.Clave;

            switch (Modo)
            {
                case ModoForm.Baja:
                    btnAceptar.Text = "Eliminar";
                    break;
                case ModoForm.Consulta:
                    btnAceptar.Text = "Aceptar";
                    break;
                default:
                    btnAceptar.Text = "Guardar";
                    break;
            }
        }

        public override bool Validar()
        {
            bool valid = true;

            // Debería haber uno por campo
            if (txtNombre.Text == string.Empty || txtApellido.Text == string.Empty || txtEmail.Text == string.Empty || 
                txtUsuario.Text == string.Empty || txtClave.Text == string.Empty || txtConfirmarClave.Text == string.Empty)
            {
                Notificar("Por favor complete todos los campos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                valid = false;
            }

            else if (txtClave.TextLength < 8)
            {
                Notificar("La clave debe tener como mínimo 8 caracteres", MessageBoxButtons.OK, MessageBoxIcon.Error);
                valid = false;
            }

            else if (txtClave.Text != txtConfirmarClave.Text)
            {
                Notificar("Las claves ingresadas son distintas", MessageBoxButtons.OK, MessageBoxIcon.Error);
                valid = false;
            }

            else if (!ValidarEmail(txtEmail.Text))
            {
                Notificar("La dirección de E-mail no es válida", MessageBoxButtons.OK, MessageBoxIcon.Error);
                valid = false;
            }

            return valid;
        }

        private bool ValidarEmail(String Email)
        {
            try
            {
                MailAddress mail = new MailAddress(Email);
                return true;
            }

            catch (Exception e)
            {
                return false;
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (Modo == ModoForm.Alta || Modo == ModoForm.Modificacion)
            {
                if (Validar())
                {
                    GuardarCambios();
                    Notificar("Se han guardado los cambios correctamente", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                //Borrar usuario
            }

            Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

    }
}

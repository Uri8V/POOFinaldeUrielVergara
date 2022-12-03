using POOFinaldeUriel.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POOFinaldeUriel.Windows
{
    public partial class frmNuevoSocio : Form
    {
        public frmNuevoSocio()
        {
            InitializeComponent();
        }
        private bool esEdicion=false;
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            LLenarComboBox();
            if (persona != null)
            {
                esEdicion = true;
                ApellidoTextBox.Text = persona.Apellido;
                PrimerNombreTextBox.Text = persona.PrimerNombre;
                SegundoNombreTextBox.Text = persona.SegundoNombre;
                DocumentoTextBox.Text = persona.Documento.ToString();
                DocumentoTextBox.Enabled = false;
                SexocomboBox.SelectedItem = (Sexo)persona.sexo;
                FechaDateTimePicker.Value = persona.Edad;
            }

        }

        private void LLenarComboBox()
        {
            SexocomboBox.DataSource = Enum.GetValues(typeof(Sexo));
            SexocomboBox.SelectedIndex = 0;
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
        private Persona persona;
        private void OKButton_Click(object sender, EventArgs e)
        {
            if (ValidarDtaos())
            {
                if (persona == null)
                {
                    persona = new Persona();
                }
                persona.PrimerNombre = PrimerNombreTextBox.Text;
                persona.SegundoNombre = SegundoNombreTextBox.Text;
                persona.Apellido = ApellidoTextBox.Text;
                persona.Documento = long.Parse(DocumentoTextBox.Text);
                persona.Edad = FechaDateTimePicker.Value.Date;
                persona.sexo = (Sexo)SexocomboBox.SelectedItem;

                DialogResult = DialogResult.OK;
            }
        }

        internal Persona GetPersona()
        {
            return persona;
        }

        private bool ValidarDtaos()
        {
            errorProvider1.Clear();
            bool valido = true;
            if (string.IsNullOrEmpty(ApellidoTextBox.Text))
            {
                valido = false;
                errorProvider1.SetError(ApellidoTextBox, "Datos mal ingresados");
            }
            else if( int.TryParse(ApellidoTextBox.Text,out int Apellido))
            {
                valido = false;
                errorProvider1.SetError(ApellidoTextBox, "Los datos deben ser un string");
            }
            if (string.IsNullOrEmpty(PrimerNombreTextBox.Text))
            {
                valido = false;
                errorProvider1.SetError(PrimerNombreTextBox, "Datos mal ingresados");
            }
            else if (int.TryParse(PrimerNombreTextBox.Text, out int PrimerNombre))
            {
                valido = false;
                errorProvider1.SetError(PrimerNombreTextBox, "Los datos deben ser un string");
            }
            if (int.TryParse(SegundoNombreTextBox.Text, out int SegundoNombre))
            {
                valido = false;
                errorProvider1.SetError(SegundoNombreTextBox, "Los datos deben ser un string");
            }
            if (!esEdicion)
            {
                if (!long.TryParse(DocumentoTextBox.Text, out long documento))
                {
                    valido = false;
                    errorProvider1.SetError(DocumentoTextBox, "El documento esta mal ingresado");
                }
                else if (documento > 7)
                {
                    valido = false;
                    errorProvider1.SetError(DocumentoTextBox, "El minimo del documento es 7");
                } 
            }
            if (FechaDateTimePicker.Value.Date > DateTime.Now.Date)
            {
                valido = false;
                errorProvider1.SetError(FechaDateTimePicker, "La fecha no es valida");
            }
            else if (FechaDateTimePicker.MinDate.Year>7 )
            {
                valido = false;
                errorProvider1.SetError(FechaDateTimePicker, "Los socios deben ser mayor a 7");
            }
            return valido;
        }

        internal void SetPersona(Persona per)
        {
            persona = per;
        }
    }
}

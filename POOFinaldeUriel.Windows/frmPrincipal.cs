using POOFinaldeUriel.Entidades;
using POOFinalDeUriel.Datos;
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
    public partial class frmPrincipal : Form
    {
        public frmPrincipal()
        {
            InitializeComponent();
        }

        private Club repo = new Club();
        private List<Persona> lista;
        private int cantidad;

        private void SalirToolStripButton_Click(object sender, EventArgs e)
        {
            repo.Guardar();
            DialogResult dr = MessageBox.Show("¿Desea cerrar el programa?", "Confirmar selección", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (dr == DialogResult.No)
            {
                return;
            }
            Application.Exit();
        }

        private void NuevoToolStripButton_Click(object sender, EventArgs e)
        {
            frmNuevoSocio frm = new frmNuevoSocio();
            DialogResult dr = frm.ShowDialog(this);
            if (dr == DialogResult.Cancel)
            {
                return;
            }
            Persona persona = frm.GetPersona();
            repo.Agregar(persona);
            DataGridViewRow r = ConstruirFila();
            SetearFila(r, persona, repo);
            AgregarFila(r);
            cantidad = repo.GetCantidad();
            MostrarTotal();
            repo.Guardar();
        }

        private void AgregarFila(DataGridViewRow r)
        {
            PrincipalDataGridView.Rows.Add(r);
        }

        private void SetearFila(DataGridViewRow r, Persona persona, Club repo)
        {
            r.Cells[colSocio.Index].Value = persona.MostrarNombre();
            r.Cells[colCategoria.Index].Value = repo.Categoria(persona);
            r.Cells[colEdad.Index].Value = persona.GetAntiguedad().ToString();
            r.Cells[colValor.Index].Value = repo.ValorDeCuota(persona);
            r.Cells[colSexo.Index].Value = persona.sexo;

            r.Tag = persona;
        }

        private DataGridViewRow ConstruirFila()
        {
            var r = new DataGridViewRow();
            r.CreateCells(PrincipalDataGridView);
            return r;
        }

        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            repo = new Club();
            lista = repo.Getlista();
            cantidad = repo.GetCantidad();
            if (cantidad > 0)
            {
                MostrarLista();
            }
            else
            {
                MessageBox.Show("El repo esta vacio", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            MostrarTotal();
        }

        private void MostrarLista()
        {
            PrincipalDataGridView.Rows.Clear();
            foreach (var persona in lista)
            {
                var r = ConstruirFila();
                SetearFila(r, persona, repo);
                AgregarFila(r);
            }
        }

        private void MostrarTotal()
        {
            CantidadLabel.Text = cantidad.ToString();
        }

        private void RefrescarToolStripButton_Click(object sender, EventArgs e)
        {
            lista = repo.Getlista();
            MostrarLista();
            cantidad = repo.GetCantidad();
            MostrarTotal();
        }

        private void GuardarToolStripButton_Click(object sender, EventArgs e)
        {
            ManejadorDeArchivoSecuancial.Guardarenarchivo(repo.Getlista());
            MessageBox.Show("Se guardaron los datos", "Mensaje", MessageBoxButtons.OK);
        }

        private void CantidadLabel_Click(object sender, EventArgs e)
        {

        }

        private void BorrarToolStripButton_Click(object sender, EventArgs e)
        {
            if (PrincipalDataGridView.SelectedRows.Count == 0)
            {
                return;
            }
            var r = PrincipalDataGridView.SelectedRows[0];
            Persona persona = r.Tag as Persona;
            DialogResult dr = MessageBox.Show("¿Desea eliminar esta fila?", "Confirmar seleccion", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (dr == DialogResult.No)
            {
                return;
            }
            repo.Borrar(persona);
            PrincipalDataGridView.Rows.Remove(r);
            cantidad = repo.GetCantidad();
            MostrarTotal();
            repo.Guardar();
        }

        private void EditarToolStripButton_Click(object sender, EventArgs e)
        {
            if (PrincipalDataGridView.SelectedRows.Count == 0)
            {
                return;
            }
            var r = PrincipalDataGridView.SelectedRows[0];
            Persona persona = r.Tag as Persona;
            frmNuevoSocio frm = new frmNuevoSocio();
            frm.SetPersona(persona);
            DialogResult dr = frm.ShowDialog(this);
            if (dr == DialogResult.Cancel)
            {
                return;
            }
            persona = frm.GetPersona();
            SetearFila(r, persona, repo);
            repo.Guardar();
        }

        private void edadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void ascendenteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var ordenar = Orden.Ascendente;
            lista = repo.ListaPorEdad(ordenar);
            MostrarLista();
        }

        private void descendenteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var ordenar = Orden.Descendente;
            lista = repo.ListaPorEdad(ordenar);
            MostrarLista();
        }

        private void aZToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var letra = Orden.Ascendente;
            lista = repo.ListaLetra(letra);
            MostrarLista();
        }

        private void zAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var letra = Orden.Descendente;
            lista = repo.ListaLetra(letra);
            MostrarLista();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using MosaicoSolutions.ViaCep;

namespace Cantina
{
    public partial class frmFuncionarios : Form
    {
        //Criando variáveis para controle do menu
        const int MF_BYCOMMAND = 0X400;
        [DllImport("user32")]
        static extern int RemoveMenu(IntPtr hMenu, int nPosition, int wFlags);
        [DllImport("user32")]
        static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);
        [DllImport("user32")]
        static extern int GetMenuItemCount(IntPtr hWnd);


        public frmFuncionarios()
        {
            InitializeComponent();
            //executando o metodo desabilitar campos
            desabilitarcampos();
        }

        private void frmFuncionarios_Load(object sender, EventArgs e)
        {
            IntPtr hMenu = GetSystemMenu(this.Handle, false);
            int MenuCount = GetMenuItemCount(hMenu) - 1;
            RemoveMenu(hMenu, MenuCount, MF_BYCOMMAND);
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }
            //CRIANDO METODO DESABILITAR CAMPOS
            public void desabilitarcampos()
            {
                txtcod.Enabled = false;
                txtnome.Enabled = false;
                txtemail.Enabled = false;
                txtbairro.Enabled = false;
                txtcidade.Enabled = false;
                txtendereco.Enabled = false;
                txtnumero.Enabled = false;
                mskcep.Enabled = false;
                mskcpf.Enabled = false;
                msktelefone.Enabled = false;
                cbbestado.Enabled = false;
                btnCadastrar.Enabled = false;
                btnAlterar.Enabled = false;
                btnLimpar.Enabled = false;
                btnExcluir.Enabled = false;
            }

        public void habilitarcampos()
        {
            txtnome.Enabled = true;
            txtemail.Enabled = true;
            txtbairro.Enabled = true;
            txtcidade.Enabled = true;
            txtendereco.Enabled = true;
            txtnumero.Enabled = true;
            mskcep.Enabled = true;
            mskcpf.Enabled = true;
            msktelefone.Enabled = true;
            cbbestado.Enabled = true;
            btnCadastrar.Enabled = true;
            btnLimpar.Enabled = true;
        }


        //CRIANDO METODO HABILITAR LIMPAR CAMPOS
        public void Limparcampos()
        {
            txtcod.Clear();
            txtnome.Clear();
            txtemail.Clear();
            txtbairro.Clear();
            txtcidade.Clear();
            txtendereco.Clear();
            txtnumero.Clear();
            mskcep.Clear();
            mskcpf.Clear();
            msktelefone.Clear();
            cbbestado.Text = "";
            btnCadastrar.Enabled = false;
            btnAlterar.Enabled = false;
            btnLimpar.Enabled = false;
            btnExcluir.Enabled = false;
            btnPesquisar.Enabled = false;
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            frmMenuPrincipal abrir = new frmMenuPrincipal();
            abrir.Show();
            this.Hide();
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            habilitarcampos();
            btnNovo.Enabled = false;
            txtnome.Focus();
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            //verificando se os campos estão preenchidos 
            if (txtnome.Text.Equals("")||
                txtemail.Text.Equals("")||
                txtbairro.Text.Equals("") ||
                txtcidade.Text.Equals("") ||
                txtendereco.Text.Equals("") || 
                txtnumero.Text.Equals("") ||
                mskcpf.Text.Equals("")||
                mskcep.Text.Equals("")||
                msktelefone.Text.Equals(""))
            {
                MessageBox.Show("Favor inserir valores","Sistema",MessageBoxButtons.OK,MessageBoxIcon.Error,MessageBoxDefaultButton.Button1);
            }
            else
            {
                MessageBox.Show("Cadastrado com sucesso", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                desabilitarcampos();
                Limparcampos();

            }
        }

        //criando metodo busca cep

        public void buscaCEP(String cep)
        {
            var viaCEPService = ViaCepService.Default();
            var endereco = viaCEPService.ObterEndereco(cep);
            txtendereco.Text = endereco.Logradouro;
            txtbairro.Text = endereco.Bairro;
            txtcidade.Text = endereco.Localidade;
            cbbestado.Text = endereco.UF;

        }
        private void mskcep_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //buscar cep
                buscaCEP(mskcep.Text);
                txtnumero.Focus();
            }
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            Limparcampos();
        }
    }
}

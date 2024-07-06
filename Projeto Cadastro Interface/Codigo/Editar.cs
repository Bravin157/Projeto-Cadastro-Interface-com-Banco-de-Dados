using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace Projeto_Cadastro_Interface
{
    public partial class frmEditar : Form
    {
        public frmEditar(string id, string nome, string email, string cargo,  string sexo)
        {
            InitializeComponent();

            //Preenche os campos com os dados recebidos da nossa lista
            txtId.Text = id;
            txtNome.Text = nome;
            txtEmail.Text = email;
            txtCargo.Text = cargo;
            txtSexo.Text = sexo;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            string baseDados = Application.StartupPath + @"\db\BancoDeDadosSQL.db";

            string strConection = @"Data Source = " + baseDados + "; Version = 3";

            SQLiteConnection conexao = new SQLiteConnection(strConection);

            int idSelecionado = int.Parse(txtId.Text);

            try
            {
                //Abre a conexao com o banco de dados
                conexao.Open();

                //Cria um novo comando SQL para inserção de dados
                SQLiteCommand comando = new SQLiteCommand();
                comando.Connection = conexao;



                //Cria o comando SQL para excluir o registro com o Id obtido
                comando.CommandText = "DELETE From Contatos WHERE Id = @id";
                comando.Parameters.AddWithValue("@id", idSelecionado);


                //Executa a consulta SQL de inserção no banco de dados
                comando.ExecuteNonQuery();

                //Libera recursos do objeto comando
                comando.Dispose();

                //Obtem uma referencia a instancia existente
                frmPrincipal principal = (frmPrincipal)Application.OpenForms["frmPrincipal"];
                //Chama o metodo para atualizar os itens
                principal.listar_itens();

                //Força a atualização da tela do formulario principal
                principal.Refresh();

                MessageBox.Show("Registro deletado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //Fecha o formulario
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao alterar os dados: " + ex.Message, "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            finally
            {
                //Fecha a conexao com o banco de dados
                conexao.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string baseDados = Application.StartupPath + @"\db\BancoDeDadosSQL.db";

            string strConection = @"Data Source = " + baseDados + "; Version = 3";

            SQLiteConnection conexao = new SQLiteConnection(strConection);

            try
            {
                //Abre a conexao com o banco de dados
                conexao.Open();

                //Cria um novo comando SQL para inserção de dados
                SQLiteCommand comando = new SQLiteCommand();
                comando.Connection = conexao;

                //Cria as variaveis e pega os valores dos campos
                int id = int.Parse(txtId.Text);
                string nome = txtNome.Text;
                string email = txtEmail.Text;
                string cargo = cmbCargo.SelectedItem.ToString();
                string sexo = cmbSexo.SelectedItem.ToString();

                //Query para alterar as informações
                string query = "UPDATE Contatos SET Nome = '" + nome + "',Email = '" + email + " ' ,Cargo = '" + cargo + "' ,Sexo = '" + sexo + "' WHERE Id LIKE'" + id + "'";
                comando.CommandText = query;


                //Executa a consulta SQL de inserção no banco de dados
                comando.ExecuteNonQuery();

                //Libera recursos do objeto comando
                comando.Dispose();

                //Obtem uma referencia a instancia existente
                frmPrincipal principal = (frmPrincipal)Application.OpenForms["frmPrincipal"];
                //Chama o metodo para atualizar os itens
                principal.listar_itens();

                //Força a atualização da tela do formulario principal
                principal.Refresh();

                MessageBox.Show("Dados alterados com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //Fecha o formulario
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao alterar os dados: " + ex.Message, "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            finally
            {
                //Fecha a conexao com o banco de dados
                conexao.Close();
            }


        }

        private void Editar_Load(object sender, EventArgs e)
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projeto_Cadastro_Interface
{
    public partial class Cadastrar : Form
    {
        public Cadastrar()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            
        }

        private void btnSalvar_Click(object sender, EventArgs e)
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
                string nome = txtNome.Text;
                string email = txtEmail.Text;
                string cargo = cmbCargo.SelectedItem.ToString();
                string sexo = cmbSexo.SelectedItem.ToString();

                //Define a consulta SQL para inserção dos dados na tabela
                comando.CommandText = "INSERT INTO Contatos (nome, email, cargo, sexo) VALUES (@nome, @email, @cargo, @sexo)";
                comando.Parameters.AddWithValue("@nome", nome);
                comando.Parameters.AddWithValue("@email", email);
                comando.Parameters.AddWithValue("@cargo", cargo);
                comando.Parameters.AddWithValue("@sexo", sexo);

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

                MessageBox.Show("Dados salvos com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //Fecha o formulario
                this.Close();
            }
            catch (Exception ex) {
                MessageBox.Show("Erro ao salvar os dados: " + ex.Message, "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            finally { 
                //Fecha a conexao com o banco de dados
                conexao.Close(); 
            }
            
        }
            
    }
}

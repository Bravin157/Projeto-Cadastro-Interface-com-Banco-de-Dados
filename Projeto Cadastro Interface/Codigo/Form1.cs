using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite; //Importado o SQLite

namespace Projeto_Cadastro_Interface
{
    public partial class frmPrincipal : Form
    {
        public frmPrincipal()
        {
            InitializeComponent();
            conectar_banco();

            listar_itens();

            //Recebe o double click
            lista.CellDoubleClick += new DataGridViewCellEventHandler(lista_CellDoubleClick);
        }

        private void lista_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //Obtem a linha selecionada
            DataGridViewRow linhaSelecionada = lista.Rows[e.RowIndex];

            //Obtem os dados associados a linha
            string id = linhaSelecionada.Cells["Id"].Value.ToString();
            string nome = linhaSelecionada.Cells["Nome"].Value.ToString();
            string email = linhaSelecionada.Cells["Email"].Value.ToString();
            string cargo = linhaSelecionada.Cells["Cargo"].Value.ToString();
            string sexo = linhaSelecionada.Cells["Sexo"].Value.ToString();

            frmEditar dados = new frmEditar(id, nome, email, cargo, sexo);
            dados.ShowDialog();
        }

        //Função para conectar com o banco de dados
        public void conectar_banco()
        {
            
            //Define o caminho do arquivo de banco de dados SQLite
            string baseDados = Application.StartupPath + @"\db\BancoDeDadosSQL.db";
            /* Com o '@' não é necessario colocar todo o caminho do arquivo, apenas
              a parte que foi criada por nós */

            //Define a string de conexao com o banco de dados SQLite
            string strConection = @"Data Source = " + baseDados + "; Version = 3";

            //Cria uma nova conexao com o banco de dados
            SQLiteConnection conexao = new SQLiteConnection(strConection);

            try
            {
                conexao.Open();
                lblMensagens.Text = "Conectado ao banco de dados com sucesso!";

            }
            catch (Exception ex) {
                lblMensagens.Text = "Erro ao conectar com o banco de dados!" + ex;
            }
            //Fecha conexao
            finally { conexao.Close(); }
        }

        public void listar_itens()
        {
            //Limpa as linhas do DataGridView
            lista.Rows.Clear();

            //Define o caminho do arquivo de banco de dados SQLite
            string baseDados = Application.StartupPath + @"\db\BancoDeDadosSQL.db";
            /* Com o '@' não é necessario colocar todo o caminho do arquivo, apenas
              a parte que foi criada por nós */

            //Define a string de conexao com o banco de dados SQLite
            string strConection = @"Data Source = " + baseDados + "; Version = 3";

            //Cria uma nova conexao com o banco de dados
            SQLiteConnection conexao = new SQLiteConnection(strConection);

            try
            {
                //Define uma consulta SQL para selecionar todos os itens da tabela
                string query = "SELECT * FROM Contatos";

                //Cria um DataTable para armazenar os dados da tabela
                DataTable dados = new DataTable();

                //Cria um adaptador para execuar a consulta e preencher o DataTable com os resultados
                SQLiteDataAdapter adaptador = new SQLiteDataAdapter(query, strConection);

                conexao.Open();

                //Preenche o DataTable com os resultados da consulta usando o metodo Fill
                adaptador.Fill(dados);

                //Adiciona as linhas
                foreach(DataRow linha in dados.Rows)
                {
                    lista.Rows.Add(linha.ItemArray);
                }
                
                //Obtem o numero de itens na lista
                int numeroItens = lista.Rows.Count - 1;
                lblMensagens.Text = $"Total de itens: {numeroItens}";

            }
            catch (Exception ex) {

                lista.Rows.Clear();
                lblMensagens.Text = ex.Message;
            }
            finally { conexao.Close(); }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            filtrar();
        }

        private void label2_Click(object sender, EventArgs e)
        {
           
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            Cadastrar abrirTela = new Cadastrar();

            //Exibe a tela de Cadastro
            abrirTela.ShowDialog();
        }

        public void filtrar()
        {
            //Limpa as linhas do DataGridView
            lista.Rows.Clear();

            //Define o caminho do arquivo de banco de dados SQLite
            string baseDados = Application.StartupPath + @"\db\BancoDeDadosSQL.db";


            //Define a string de conexao com o banco de dados SQLite
            string strConection = @"Data Source = " + baseDados + "; Version = 3";

            //Cria uma nova conexao com o banco de dados
            SQLiteConnection conexao = new SQLiteConnection(strConection);

            try
            {
                //Define uma consulta SQL para selecionar todos os itens da tabela
                string query = "SELECT * FROM Contatos WHERE Nome LIKE '%' || @nome || '%'";

                //Cria um DataTable para armazenar os dados da tabela
                DataTable dados = new DataTable();

                //Cria um adaptador para execuar a consulta e preencher o DataTable com os resultados
                SQLiteDataAdapter adaptador = new SQLiteDataAdapter(query, strConection);

                //Define o parametro "@nome" com o valor do campo txtNome
                adaptador.SelectCommand.Parameters.AddWithValue("@nome", txtNome.Text);

                conexao.Open();

                //Preenche o DataTable com os resultados da consulta usando o metodo Fill
                adaptador.Fill(dados);

                //Adiciona as linhas
                foreach (DataRow linha in dados.Rows)
                {
                    lista.Rows.Add(linha.ItemArray);
                }

                //Obtem o numero de itens na lista
                int numeroItens = lista.Rows.Count - 1;
                lblMensagens.Text = $"Total de itens: {numeroItens}";

            }
            catch (Exception ex)
            {

                lista.Rows.Clear();
                lblMensagens.Text = ex.Message;
            }
            finally { conexao.Close(); }
        }

        private void frmPrincipal_Load(object sender, EventArgs e)
        {

        }

        
    }
}

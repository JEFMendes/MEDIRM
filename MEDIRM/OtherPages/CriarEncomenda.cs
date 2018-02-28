﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MEDIRM.Navegacao;

namespace MEDIRM
{
    public partial class CriarEncomenda : Form
    {
        public CriarEncomenda()
        {
            InitializeComponent();
        }

        private void back_Click(object sender, EventArgs e)
        {
            MainFormView.ShowForm(new Menu());
        }

        private void criarMaquina_Click(object sender, EventArgs e)     // adicionar encomenda
        {
            try
            {
                //Insert in the database
                string connectionString = ConfigurationManager.ConnectionStrings["MedirmDB"].ConnectionString;
                SqlConnection con = new SqlConnection(connectionString);

                SqlCommand com = new SqlCommand("INSERT INTO Encomenda (Artigo, Cliente, Quantidade, DataLimite, Estado) VALUES (@Artigo, @Cliente, @Quantidade, @DataLimite, @Estado)", con);
                com.CommandType = CommandType.Text;

                com.Parameters.AddWithValue("@Quantidade", textBox3.Text);
                com.Parameters.AddWithValue("@DataLimite", dateTimePicker1.Value);
                com.Parameters.AddWithValue("@Estado", "EmProducao");
                com.Parameters.AddWithValue("@Artigo", comboBox2.SelectedValue.ToString());
                com.Parameters.AddWithValue("@Cliente", comboBox1.SelectedValue.ToString());

                con.Open();
                int i = com.ExecuteNonQuery();
                con.Close();

                //Confirmation Message 
                MessageBox.Show("Componente adicionado com sucesso!");

                //clear
                comboBox2.ResetText();
                comboBox1.ResetText();
                textBox3.Clear();

            }
            catch (Exception x)
            {
                //Error Message 
                MessageBox.Show("Erro ao adicionar componente. Por favor tente novamente.");
            }
        }

        private void CriarEncomenda_Load(object sender, EventArgs e)
        {
            // TODO: esta linha de código carrega dados na tabela 'medirmDBDataSet.ArtigosClientes'. Você pode movê-la ou removê-la conforme necessário.
            this.artigosClientesTableAdapter.Fill(this.medirmDBDataSet.ArtigosClientes);
            // TODO: esta linha de código carrega dados na tabela 'medirmDBDataSet.Cliente'. Você pode movê-la ou removê-la conforme necessário.
            this.clienteTableAdapter.Fill(this.medirmDBDataSet.Cliente);

            //clear
            comboBox2.ResetText();
            comboBox1.ResetText();
            textBox3.Clear();
        }

        private void fillByToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.artigosClientesTableAdapter.FillBy(this.medirmDBDataSet.ArtigosClientes);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ReadDBSchema;
using SimpleEnterprisesFramework;
using Generater;
using GenericForm;
using Microsoft.EntityFrameworkCore;
using MySqlX.XDevAPI;
using GenericForm.DBContext;
using System.Xml.Linq;
using GenericForm.ModelForms;

namespace Main.Forms
{
    public partial class DatabaseConnection : Form
    {
        private string Username { get; }
        private string Password { get; }
        private string Address { get; }
        private string Port { get; }
        private string DatabaseName { get; }
        private string DatabaseType { get; }
        private DatabaseConfigForm ConfigForm { get; }
        DatabaseManagement databaseManagement;
        public DatabaseConnection(DatabaseConfigForm configForm, string username, string password, string address, string port, string databaseName, string databaseType, DatabaseManagement databaseManagement)
        {
            InitializeComponent();
            Username = username;
            Password = password;
            Address = address;
            Port = port;
            DatabaseName = databaseName;
            DatabaseType = databaseType;
            ConfigForm = configForm;
            this.databaseManagement = databaseManagement;

            // assign connection string to text box
            ConnectionStringTextBox.Text = databaseManagement.GetConnectionString();
        }

        private void CheckConnection_Click(object sender, EventArgs e)
        {
            if (databaseManagement.CheckConnection())
            {
                MessageBox.Show("Connection successful");
                GenerateButton.Visible = true;
                
                // debug
                DatabaseSchema schema = databaseManagement.GetDatabaseSchema();
                List<TableSchema> tableSchemas = schema.GetTables();

                string tmp = "";
                foreach (TableSchema table in tableSchemas)
                {
                    tmp += table.ToString();
                }

                MessageBox.Show(tmp);
            }
            else
            {
                MessageBox.Show("Connection failed");
            }
        }

        private void Generate_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("Not implemented yet");
            //DatabaseSchema schema = databaseManagement.GetDatabaseSchema();
            //List<TableSchema> tableSchemas = schema.GetTables();
            //foreach (TableSchema s in tableSchemas)
            //{
            //    GenerateModel(s);
            //} 

            this.Hide();
            MainWindow mainWindow = new MainWindow(databaseManagement.GetDatabaseType(), databaseManagement.GetConnectionString());
            mainWindow.Show();
        }

        private void Previous_Click(object sender, EventArgs e)
        {
            this.Hide();
            ConfigForm.Show();

        }

        private void GenerateModel(TableSchema schema)
        {
            // Generate and save model code
            Generater.Generator _generator = new Generator(this.databaseManagement.GetDatabaseTypeMapper());
            var code =  _generator.GenerateModelCode(schema);
            _generator.SaveGeneratedCode(schema.GetTableName(), code);

            // Compile and load
            var assembly = RuntimeCompiler.CompileCode(code);
            var modelType = assembly.GetType(schema.GetTableName());

            Console.WriteLine(modelType.Name);
        }

    }
}

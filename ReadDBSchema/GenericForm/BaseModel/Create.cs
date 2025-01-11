﻿using System.Reflection;
using System.Windows.Forms;
using GenericForm.Fields;
using System.Linq;
using System.Collections.Generic;
using System;
using GenericForm.DBContext;
using Microsoft.EntityFrameworkCore;

namespace GenericForm.BaseModel
{
    public partial class Create<T> : Form where T : class, IBaseModel, new()
    {
        private readonly Dictionary<PropertyInfo, IInputControlStrategy> _strategies;
        private BaseApplicationDbContext<T> _context;

        public Create(BaseApplicationDbContext<T> context)
        {
            _context = context;
            InitializeComponent();
            _strategies = new Dictionary<PropertyInfo, IInputControlStrategy>();
            GenerateFields();
        }

        private void GenerateFields()
        {
            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.Name != "ID" && p.Name != "id");

            foreach (var property in properties)
            {
                var label = new Label { Text = property.Name, AutoSize = true };
                flowLayoutPanel.Controls.Add(label);

                var strategy = InputControlStrategyFactory.CreateStrategy(property.PropertyType);
                Control inputControl = strategy.CreateControl(property);
                inputControl.Name = property.Name + "Control";
                flowLayoutPanel.Controls.Add(inputControl);
                _strategies.Add(property, strategy);
            }

            var saveButton = new Button { Text = "Save", Width = 100 };
            saveButton.Click += SaveButton_Click!;
            flowLayoutPanel.Controls.Add(saveButton);
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            try
            {
                var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Where(p => p.Name != "ID" && p.Name != "id");
                var product = new T();
                foreach (var property in properties)
                {
                    if (_strategies.TryGetValue(property, out var strategy))
                    {
                        var control = flowLayoutPanel.Controls.Find(property.Name + "Control", true).FirstOrDefault();
                        if (control != null)
                        {
                            object value = strategy.GetValue();
                            property.SetValue(product, value);
                        }
                    }
                }

                //DbContextHelper.GetDbSet<T>().Add(product);
                //DbContextHelper.Context.SaveChanges();

                _context.Set().Add(product);
                _context.SaveChanges();
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating product: {ex.Message}");
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SampleEnterpriseFramework.Generic
{
    public partial class GenericForm : Form
    {
        public GenericForm() 
        {
            InitializeComponent();
        }

        private void btnGenerateForm_Click(object sender, EventArgs e)
        {
            Type newFormType = GenerateDerivedForm();
            Form newForm = (Form)Activator.CreateInstance(newFormType);
            newForm.Show();
        }

        private Type GenerateDerivedForm()
        {
            var assemblyName = new AssemblyName("DynamicFormsAssembly");
            var assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
            var moduleBuilder = assemblyBuilder.DefineDynamicModule("MainModule");
            var typeBuilder = moduleBuilder.DefineType("DynamicForm", TypeAttributes.Public, typeof(BaseForm));

            // Override InitializeComponent in the derived form
            var methodBuilder = typeBuilder.DefineMethod("InitializeComponent",
                MethodAttributes.Family | MethodAttributes.Virtual | MethodAttributes.HideBySig,
                null, Type.EmptyTypes);

            var ilGenerator = methodBuilder.GetILGenerator();
            ilGenerator.Emit(OpCodes.Ldarg_0);
            ilGenerator.Emit(OpCodes.Call, typeof(BaseForm).GetMethod("InitializeComponent", BindingFlags.Instance | BindingFlags.NonPublic));
            ilGenerator.Emit(OpCodes.Ldarg_0);
            ilGenerator.Emit(OpCodes.Ldstr, "Dynamic Form");
            ilGenerator.Emit(OpCodes.Call, typeof(Form).GetProperty("Text").GetSetMethod());
            ilGenerator.Emit(OpCodes.Ret);

            typeBuilder.DefineMethodOverride(methodBuilder, typeof(BaseForm).GetMethod("InitializeComponent", BindingFlags.Instance | BindingFlags.NonPublic));

            return typeBuilder.CreateType();
        }

        private Button btnGenerateForm;

        private void InitializeComponent()
        {
            btnGenerateForm = new Button();
            SuspendLayout();
            // 
            // btnGenerateForm
            // 
            btnGenerateForm.Location = new Point(10, 10);
            btnGenerateForm.Name = "btnGenerateForm";
            btnGenerateForm.Size = new Size(243, 43);
            btnGenerateForm.TabIndex = 0;
            btnGenerateForm.Text = "Generate Form";
            btnGenerateForm.Click += btnGenerateForm_Click;
            // 
            // GenericForm
            // 
            ClientSize = new Size(282, 253);
            Controls.Add(btnGenerateForm);
            Name = "GenericForm";
            ResumeLayout(false);
        }
    }
}

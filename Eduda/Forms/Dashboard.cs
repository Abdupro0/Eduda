
using DevExpress.DXperience.Demos;
using DevExpress.XtraBars;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Eduda.Forms
{
    public partial class Dashboard : DevExpress.XtraBars.FluentDesignSystem.FluentDesignForm
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        
        async Task LoadModuleAsync(ModuleInfo module)
        {
            try
            {
                await Task.Factory.StartNew(() =>
                {
                    if (!fluentDesignFormContainer.Controls.ContainsKey(module.Name))
                    {

                        TutorialControlBase control = module.TModule as TutorialControlBase;
                        if (control != null)
                        {
                            control.Dock = DockStyle.Fill;
                            control.CreateWaitDialog();
                            fluentDesignFormContainer.Invoke(new MethodInvoker(delegate ()
                            {
                                fluentDesignFormContainer.Controls.Add(control);
                                control.BringToFront();
                            }));
                        }
                    }
                    else
                    {
                        // If control already exists, bring it to the front
                        var control = fluentDesignFormContainer.Controls.Find(module.Name, true);
                        if (control.Length == 1)
                            fluentDesignFormContainer.Invoke(new MethodInvoker(delegate () { control[0].BringToFront(); }));
                    }
                });


            }
            catch (Exception ex)
            {
                // Log or handle exceptions as needed
                Debug.WriteLine($"Error loading module: {ex.Message}");
            }
        }

        private void accordionControl1_Click(object sender, EventArgs e)
        {

        }

        private void accordionControlElementInfo_Click(object sender, EventArgs e)
        {
            var AboutUS = new AboutUS();
            AboutUS.ShowDialog();
        }

        private void Dashboard_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private async void accordionControlElementStudents_Click(object sender, EventArgs e)
        {
            this.fluentDesignFormContainer.Controls.Clear();
            this.fluentDesignFormContainer.Controls.Add(new USstudents() { Dock = DockStyle.Fill });

            this.itemNav.Caption = $"{accordionControlElementStudents.Text}";

            var module = ModulesInfo.GetItem("usStudents");
            if (module != null)
            {
                await LoadModuleAsync(module);
            }
            else
            {
                Debug.WriteLine("Module 'usStudents' not found!");
            }
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
        }

        private async void accordionControlElement11_Click(object sender, EventArgs e)
        {
            this.fluentDesignFormContainer.Controls.Clear();
            this.fluentDesignFormContainer.Controls.Add(new USstudents() { Dock = DockStyle.Fill });

            this.itemNav.Caption = $"{accordionControlElement11.Text}";

            var module = ModulesInfo.GetItem("usStudents");
            if (module != null)
            {
                await LoadModuleAsync(module);
            }
            else
            {
                Debug.WriteLine("Module 'usStudents' not found!");
            }
        }

        private async void accordionControlElementEmployees_Click(object sender, EventArgs e)
        {
            this.fluentDesignFormContainer.Controls.Clear();
            this.fluentDesignFormContainer.Controls.Add(new USWorker() { Dock = DockStyle.Fill });
            this.itemNav.Caption = $"{accordionControlElementEmployees.Text}";
            var module = ModulesInfo.GetItem("USWorker");
            if (module != null)
            {
                await LoadModuleAsync(module);
            }
            else
            {
                Debug.WriteLine("Module 'USWorker' not found!");
            }
        }

        private void accordionControlElementSubjects_Click(object sender, EventArgs e)
        {
            this.itemNav.Caption = $"{accordionControlElementSubjects.Text}";
            this.fluentDesignFormContainer.Controls.Clear();
            this.fluentDesignFormContainer.Controls.Add(new UScourses() { Dock = DockStyle.Fill });
        }

        private void accordionControlElementMange_Click(object sender, EventArgs e)
        {

            if (Globals.typevv == "Manager")
            {
                this.itemNav.Caption = $"{accordionControlElementMange.Text}";
                this.fluentDesignFormContainer.Controls.Clear();
                this.fluentDesignFormContainer.Controls.Add(new USmange() { Dock = DockStyle.Fill });
            }
            else
            {
                MessageBox.Show("ليس مسموح لك الدخول هنا ");
            }
        }

        private void accordionControlElement24_Click(object sender, EventArgs e)
        {
            this.fluentDesignFormContainer.Controls.Clear();
            this.fluentDesignFormContainer.Controls.Add(new USWorker() { Dock = DockStyle.Fill });
        }

        private void fluentDesignFormControl1_Click(object sender, EventArgs e)
        {

        }
    }
}

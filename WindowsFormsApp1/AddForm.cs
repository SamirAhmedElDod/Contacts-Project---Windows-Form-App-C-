using ContactsBusinessLayer;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace WindowsFormsApp1
{
    public partial class AddForm : Form
    {
        OpenFileDialog Dialog = new OpenFileDialog();
        public enum enMode { AddNew = 0, Update = 1};
        private enMode _Mode;

        int _ContactID;
        clsContact _Contact;

        public AddForm(int ContactID)
        {
            InitializeComponent();
            _ContactID = ContactID;

            if(_ContactID == -1)
            {
                _Mode = enMode.AddNew;
            } 
            else
            {
                _Mode = enMode.Update;
            }
        }

        private void _FillCountriesInComoboBox()
        {
            DataTable dataTable = clsCountry.GetAllCountries();
            foreach (DataRow row in dataTable.Rows)
            {
                guna2ComboBox2.Items.Add(row["CountryName"]);
            }
        }
        private void LoadData()
        {
            _FillCountriesInComoboBox();
            guna2ComboBox2.SelectedIndex = 0;

            if (_Mode == enMode.AddNew)
            {
                label1.Text = "Add New Contact";
                _Contact = new clsContact();
                return;
            }

            _Contact = clsContact.Find(_ContactID);

            if (_Contact == null)
            {
                MessageBox.Show("This Form Will Be Closed Becasue No Contact With This ID");
                this.Close();
                return;
            }

            label1.Text = "Edit Contact";
            label10.Text = _ContactID.ToString();

            guna2TextBox1.Text = _Contact.FirstName;
            guna2TextBox2.Text = _Contact.LastName;
            guna2TextBox3.Text = _Contact.Email;
            guna2TextBox4.Text = _Contact.Phone;
            guna2TextBox7.Text = _Contact.Address;
            guna2DateTimePicker1.Value = _Contact.DateOfBirth;
            if (_Contact.ImagePath != "")
            {
                guna2PictureBox1.Load(_Contact.ImagePath);
            }
            guna2ComboBox2.SelectedIndex = guna2ComboBox2.FindString(clsCountry.Find(_Contact.CountryID).CountryName);

        }

        private void AddForm_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        // Save Button
        private void guna2Button2_Click(object sender, EventArgs e)
        {
            int CountryID = clsCountry.Find(guna2ComboBox2.Text).ID;

            _Contact.FirstName = guna2TextBox1.Text;
            _Contact.LastName = guna2TextBox2.Text;
            _Contact.Email = guna2TextBox3.Text;
            _Contact.Phone = guna2TextBox4.Text;
            _Contact.Address = guna2TextBox7.Text;
            _Contact.DateOfBirth = guna2DateTimePicker1.Value;
            _Contact.CountryID = CountryID;
            if (Dialog.FileName != "")
            {

                _Contact.ImagePath = Dialog.FileName;

            }
            else
            {
                _Contact.ImagePath = "";
            }

            if (_Contact.Save())
            {
                MessageBox.Show("Data Saved Successfully", "Congrats", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
                
            } else
            {
                MessageBox.Show("Data Faild To Save", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
            _Mode = enMode.Update;
            label1.Text = "Edit Contact";
            label10.Text =_Contact.ID.ToString();
        }




        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Choose Image Button
        private void guna2TileButton1_Click(object sender, EventArgs e)
        {
            DialogResult Result = Dialog.ShowDialog();
            Dialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            Dialog.FilterIndex = 1;
            Dialog.RestoreDirectory = true;

            if (Result == DialogResult.OK)
            {
                guna2PictureBox1.Image = Image.FromFile(Dialog.FileName);
            }
        }




      
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ContactsBusinessLayer;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void _RefreshContactsLists() 
        {
            guna2DataGridView1.DataSource = clsContact.GetAllContacts();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            AddForm Add_Form = new AddForm(-1);
            Add_Form.Show();
            Add_Form.FormClosed += (s, args) => _RefreshContactsLists();
            _RefreshContactsLists();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _RefreshContactsLists();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (guna2DataGridView1.SelectedRows.Count > 0) 
            {
                int selectedRowIndex = guna2DataGridView1.SelectedRows[0].Index;
                int id = (int)guna2DataGridView1.Rows[selectedRowIndex].Cells[0].Value;
                AddForm Add_Form = new AddForm(id);
                Add_Form.Show();
                _RefreshContactsLists();
                Add_Form.FormClosed += (s, args) => _RefreshContactsLists();
            }
            else
            {
                MessageBox.Show("Please Select Row To Edit" , "Error" , MessageBoxButtons.OK , MessageBoxIcon.Error); 
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (guna2DataGridView1.SelectedRows.Count > 0)
            {
                int selectedRowIndex = guna2DataGridView1.SelectedRows[0].Index;
                int id = (int)guna2DataGridView1.Rows[selectedRowIndex].Cells[0].Value;

                DialogResult Answer = MessageBox.Show($"Are You Sure You Want To Delete {id}  ?", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (Answer == DialogResult.OK)
                {
                    if (clsContact.DeleteContact(id)) 
                    {
                        MessageBox.Show($"{id} Deleted Successfully", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        _RefreshContactsLists();
                    };
                }
            }
            else
            {
                MessageBox.Show("Please Select Row To Edit", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

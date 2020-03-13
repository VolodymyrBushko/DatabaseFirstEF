using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VolodDatabaseFirstEF
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitializeDataGridViewSage();
            InitializeDataGridViewBook();
        }

        private void InitializeDataGridViewBook()
        {
            using (SagesAndBooksEntities context = new SagesAndBooksEntities())
            {
                bindingSourceBook.DataSource = context.Books.Select(b => new
                {
                    Id = b.Id,
                    Name = b.Name,
                    Price = b.Price,
                    SageId = b.SageId
                }).ToArray();
                dataGridViewBook.DataSource = bindingSourceBook;
            }
        }

        private void InitializeDataGridViewSage()
        {
            using (SagesAndBooksEntities context = new SagesAndBooksEntities())
            {
                bindingSourceSage.DataSource = context.Sages.Select(s => new
                {
                    Id = s.Id,
                    Name = s.Name,
                    Age = s.Age
                }).ToArray();
                dataGridViewSage.DataSource = bindingSourceSage;
            }
        }

        private void insertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SageForms.InsertAndUpdate insert = new SageForms.InsertAndUpdate();
            insert.ShowDialog();
            InitializeDataGridViewSage();
        }

        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                int sageId = Convert.ToInt32(dataGridViewSage.SelectedRows[0].Cells["Id"].Value);
                SageForms.InsertAndUpdate update = new SageForms.InsertAndUpdate(sageId);
                update.ShowDialog();
                InitializeDataGridViewSage();
            }
            catch { MessageBox.Show("Selected row == null"); }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                int sageId = Convert.ToInt32(dataGridViewSage.SelectedRows[0].Cells["Id"].Value);
                using (SagesAndBooksEntities context = new SagesAndBooksEntities())
                {
                    Sage sage = context.Sages.Where(s => s.Id == sageId).FirstOrDefault();
                    context.Sages.Remove(sage);
                    context.SaveChanges();
                }
                InitializeDataGridViewSage();
            }
            catch { MessageBox.Show("Selected row == null"); }
        }

        private void insertToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            BookForms.InsertAndUpdate insert = new BookForms.InsertAndUpdate();
            insert.ShowDialog();
            InitializeDataGridViewBook();
        }

        private void updateToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                int bookId = Convert.ToInt32(dataGridViewBook.SelectedRows[0].Cells["Id"].Value);
                BookForms.InsertAndUpdate update = new BookForms.InsertAndUpdate(bookId);
                update.ShowDialog();
                InitializeDataGridViewBook();
            }
            catch { MessageBox.Show("Selected row == null"); }
        }

        private void deleteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                int bookId = Convert.ToInt32(dataGridViewBook.SelectedRows[0].Cells["Id"].Value);
                using (SagesAndBooksEntities context = new SagesAndBooksEntities())
                {
                    Book book = context.Books.Where(b => b.Id == bookId).FirstOrDefault();
                    context.Books.Remove(book);
                    context.SaveChanges();
                }
                InitializeDataGridViewBook();
            }
            catch { MessageBox.Show("Selected row == null"); }
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VolodDatabaseFirstEF.BookForms
{
    public partial class InsertAndUpdate : Form
    {
        private int bookId;

        public InsertAndUpdate()
        {
            InitializeComponent();
            InitializeComboBoxSages();

            this.Text = "Insert";
            button.Click += buttonInsert_Click;
        }

        public InsertAndUpdate(int bookId)
        {
            InitializeComponent();
            InitializeComboBoxSages();

            this.Text = "Update";
            this.comboBoxSages.Enabled = false;
            this.bookId = bookId;

            button.Click += buttonUpdate_Click;
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            using (SagesAndBooksEntities context = new SagesAndBooksEntities())
            {
                Book book = context.Books.Where(b => b.Id == bookId).FirstOrDefault();

                if(!string.IsNullOrWhiteSpace(textBoxName.Text) &&
                    !string.IsNullOrWhiteSpace(textBoxPrice.Text) && book != null)
                {
                    book.Name = textBoxName.Text;
                    book.Price = Convert.ToInt32(textBoxPrice.Text);

                    context.SaveChanges();
                }
                this.Dispose();
                this.Close();
            }
        }

        private void InitializeComboBoxSages()
        {
            using (SagesAndBooksEntities context = new SagesAndBooksEntities())
            {
                comboBoxSages.Items.AddRange(context.Sages.ToArray());
            }
        }

        private void buttonInsert_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBoxName.Text) &&
                !string.IsNullOrWhiteSpace(textBoxPrice.Text) &&
                !string.IsNullOrWhiteSpace(comboBoxSages.Text))
            {
                using (SagesAndBooksEntities context = new SagesAndBooksEntities())
                {
                    Book book = new Book()
                    {
                        Name = textBoxName.Text,
                        Price = Convert.ToInt32(textBoxPrice.Text),
                        SageId = (comboBoxSages.SelectedItem as Sage).Id
                    };
                    context.Books.Add(book);
                    context.SaveChanges();
                }
                this.Dispose();
                this.Close();
            }
        }
    }
}
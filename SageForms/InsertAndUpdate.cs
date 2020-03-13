using System;
using System.Linq;
using System.Windows.Forms;

namespace VolodDatabaseFirstEF.SageForms
{
    public partial class InsertAndUpdate : Form
    {
        private int sageId;

        public InsertAndUpdate()
        {
            InitializeComponent();

            this.Text = "Insert";
            this.button.Click += buttonInsert_Click;
        }

        public InsertAndUpdate(int sageId)
        {
            InitializeComponent();

            this.sageId = sageId;
            this.Text = "Update";
            this.button.Click += buttonUpdate_Click;
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            using (SagesAndBooksEntities context = new SagesAndBooksEntities())
            {
                Sage sage = context.Sages.Where(s => s.Id == sageId).FirstOrDefault();

                if (!string.IsNullOrWhiteSpace(textBoxName.Text) &&
                   !string.IsNullOrWhiteSpace(textBoxAge.Text))
                {
                    sage.Name = textBoxName.Text;
                    sage.Age = Convert.ToInt32(textBoxAge.Text);

                    context.SaveChanges();
                }
                this.Dispose();
                this.Close();
            }
        }

        private void buttonInsert_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBoxName.Text) &&
               !string.IsNullOrWhiteSpace(textBoxAge.Text))
            {
                using (SagesAndBooksEntities context = new SagesAndBooksEntities())
                {
                    Sage sage = new Sage()
                    {
                        Name = textBoxName.Text,
                        Age = Convert.ToInt32(textBoxAge.Text)
                    };
                    context.Sages.Add(sage);
                    context.SaveChanges();
                }
                this.Dispose();
                this.Close();
            }
        }
    }
}
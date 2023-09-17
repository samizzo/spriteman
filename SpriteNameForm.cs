using System.Windows.Forms;

namespace spriteman
{
    public partial class SpriteNameForm : Form
    {
        public string SpriteName
        {
            get { return textBox1.Text; }
            set { textBox1.Text = value; }
        }

        public SpriteNameForm()
        {
            InitializeComponent();
            okButton.Enabled = false;
        }

        private void okButton_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void cancelButton_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void textBox1_TextChanged(object sender, System.EventArgs e)
        {
            okButton.Enabled = !string.IsNullOrEmpty(textBox1.Text) && textBox1.Text.Length > 0;
        }
    }
}

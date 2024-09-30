using Newtonsoft.Json;
using System.Windows.Forms;

namespace TextEditor
{
    public partial class Form1 : Form
    {
        FileManager fileManager = new FileManager();
        TextFormatter textFomatter = new TextFormatter();
        public Form1()
        {
            InitializeComponent();
            panel1.Dock = DockStyle.Left;
            panel1.Width = 50;
            textBox1.Dock = DockStyle.Fill;
            textBox1.Padding = new Padding(panel1.Width, 0, 0, 0);
            defaultTheme();
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                string jsonFilePath = openFileDialog.FileName + ".font.json";
                if (File.Exists(jsonFilePath))
                {
                    string json = File.ReadAllText(jsonFilePath);
                    FontSettings fontSettings = JsonConvert.DeserializeObject<FontSettings>(json);

                    //Apply font changes
                    textBox1.Font = new Font(fontSettings.FontName, fontSettings.FontSize, fontSettings.FontStyle);
                }
            }
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void UpdateLineNumbers()
        {
            Point pt = new Point(0, 0);
            int firstIndex = textBox1.GetCharIndexFromPosition(pt);
            int firstLine = textBox1.GetLineFromCharIndex(firstIndex);

            pt.Y += textBox1.Height;
            int lastIndex = textBox1.GetCharIndexFromPosition(pt);
            int lastLine = textBox1.GetLineFromCharIndex(lastIndex);

            panel1.Controls.Clear();
            for (int i = firstLine; i <= lastLine + 1; i++)
            {
                Label lbl = new Label();
                lbl.Text = (i + 1).ToString();
                lbl.AutoSize = false;
                lbl.Width = panel1.Width;
                lbl.Height = textBox1.Font.Height;
                lbl.TextAlign = ContentAlignment.MiddleRight;
                lbl.Location = new Point(0, (i - firstLine) * textBox1.Font.Height);
                panel1.Controls.Add(lbl);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            UpdateLineNumbers();
        }

        // Manage the font settings
        private void fuenteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textFomatter.changeFont(textBox1);
        }

        // Manage the key combinations 
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            fileManager.saveFile(sender, e);
            fileManager.openFile(sender, e);
            textFomatter.undo(sender, e, textBox1);
        }

        private void temaSistema()
        {
            this.BackColor = SystemColors.Window;
            textBox1.BackColor = SystemColors.WindowFrame;
            textBox1.ForeColor = SystemColors.WindowText;
            menuStrip1.BackColor = SystemColors.Menu;
            menuStrip1.ForeColor = SystemColors.MenuText;
            Properties.Settings.Default.TemaOscuro = false;
            Properties.Settings.Default.TemaClaro = false;
            Properties.Settings.Default.TemaSistema = true;
            Properties.Settings.Default.Save();
        }

        private void temaClaro()
        {
            this.BackColor = Color.White;
            textBox1.BackColor = Color.White;
            textBox1.ForeColor = Color.Black;
            menuStrip1.BackColor = Color.LightGray;
            menuStrip1.ForeColor = Color.Black;
            Properties.Settings.Default.TemaOscuro = false;
            Properties.Settings.Default.TemaClaro = true;
            Properties.Settings.Default.TemaSistema = false;
            Properties.Settings.Default.Save();
        }

        private void temaOscuro()
        {
            this.BackColor = Color.FromArgb(30, 30, 30); // Fondo oscuro del formulario
            textBox1.BackColor = Color.FromArgb(45, 45, 48); // Fondo oscuro del TextBox
            textBox1.ForeColor = Color.White; // Color de texto del TextBox
            menuStrip1.BackColor = Color.FromArgb(45, 45, 48); // Fondo oscuro del menú
            menuStrip1.ForeColor = Color.White;
            Properties.Settings.Default.TemaOscuro = true;
            Properties.Settings.Default.TemaClaro = false;
            Properties.Settings.Default.TemaSistema = false;
            Properties.Settings.Default.Save();
        }

        private void claroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            temaClaro();
        }

        private void sistemaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            temaSistema();
        }

        private void oscuroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            temaOscuro();
        }

        private void defaultTheme()
        {
            if (Properties.Settings.Default.TemaOscuro)
                temaOscuro();
            if (Properties.Settings.Default.TemaClaro)
                temaClaro();
            if (Properties.Settings.Default.TemaSistema)
                temaSistema();
        }

    }
}

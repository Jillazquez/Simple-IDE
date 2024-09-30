using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace TextEditor
{
    internal class TextFormatter
    {
        public void changeFont(TextBox textBox1)
        {
            using (FontDialog fontDialog = new FontDialog())
            {
                if (fontDialog.ShowDialog() == DialogResult.OK)
                {
                    textBox1.Font = fontDialog.Font;
                }
            }

            FontSettings fontSettings = new FontSettings
            {
                FontName = textBox1.Font.Name,
                FontSize = textBox1.Font.Size,
                FontStyle = textBox1.Font.Style,
            };
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                string json = Newtonsoft.Json.JsonConvert.SerializeObject(fontSettings);
                File.WriteAllText(saveFileDialog.FileName + ".font.json", json);
            }
        }

        public void undo(object sender, KeyEventArgs e,TextBox textBox)
        {
            if (e.Control && e.KeyCode == Keys.Z)
            {
                textBox.Undo();
                e.SuppressKeyPress = true; // Avoid 'Z' to appear in textbox trying to save
            }
        }
    }
}

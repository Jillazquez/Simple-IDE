using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextEditor
{
    class FileManager
    {
        public void saveFile(object sender, KeyEventArgs e)
        {
            if(e.Control && e.KeyCode == Keys.S)
            {
                MessageBox.Show("Guardando Archivo");
                e.SuppressKeyPress = true; // Avoid 'S' to appear in textbox trying to save
            }
        }

        public void openFile(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.O)
            {
                MessageBox.Show("Abriendo Archivo");
                e.SuppressKeyPress = true; // Avoid 'S' to appear in textbox trying to save
            }
        }
    }
}

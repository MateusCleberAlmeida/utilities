using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
       public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            string path = openFileDialog1.FileName;
            using (Bitmap bmp=new Bitmap(path))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    bmp.Save(ms, bmp.RawFormat);
                    string base64 = Convert.ToBase64String(ms.ToArray());
                    textBox1.Text = base64;
                }
            }
        }

        static public string EncodeToBase64(string texto)
        {
            byte[] textoAsBytes = Encoding.ASCII.GetBytes(texto);
            string resultado = Convert.ToBase64String(textoAsBytes);
            return resultado;
        }        

        private void button5_Click(object sender, EventArgs e)
        {
            pctimg.Image = Base64ToImage(textBox1.Text);
        }

        static public Image Base64ToImage(string base64)
        {
            try
            {                
                byte[] imageByte = Convert.FromBase64String(base64);

                MemoryStream ms = new MemoryStream(imageByte, 0, imageByte.Length);
                Image image = Image.FromStream(ms, true);
                return image;
            }
            catch
            {
                MessageBox.Show("Erro ao processar imagem!");
                return null;
            }           
        }
    }
}

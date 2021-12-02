using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MessagingToolkit.QRCode;
using MessagingToolkit.QRCode.Codec;
using MessagingToolkit.QRCode.Codec.Data;
using MessagingToolkit.QRCode.Geom;
using MessagingToolkit.QRCode.ExceptionHandler;

namespace QR_Kode123
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void create_qr_Click(object sender, EventArgs e)
        {
            string qr_code_text = text_from_qr.Text;
            text_from_qr.Text = "";
            if (qr_code_text.Replace(" ", "").Length > 0)
            {
                QRCodeEncoder qr_encoder = new QRCodeEncoder();
                Bitmap qr_code_image = qr_encoder.Encode(qr_code_text, Encoding.UTF8);
                qr_code_view.Image = qr_code_image;
            }
            else MessageBox.Show("Невозможно закодировать пустой текст");
        }

        private void save_qr_code_Click(object sender, EventArgs e)
        {
            Image current_qr_code = qr_code_view.Image;
            if (current_qr_code != null)
            {
                if (save_qr_code_dialog.ShowDialog() == DialogResult.OK)
                {
                    current_qr_code.Save(save_qr_code_dialog.FileName);
                }
            }
        }

        private void load_qr_code_Click(object sender, EventArgs e)
        {
            if (open_qr_code_dialog.ShowDialog() == DialogResult.OK)
            {
                string path = open_qr_code_dialog.FileName;
                QRCodeDecoder qr_decoder = new QRCodeDecoder();
                Bitmap qr_code_bitmap = (Bitmap)Bitmap.FromFile(path);
                QRCodeBitmapImage qr_code_image = new QRCodeBitmapImage(qr_code_bitmap);
                string qr_code_decode_text = qr_decoder.decode(qr_code_image,Encoding.UTF8);
                text_from_qr.Text = qr_code_decode_text;
                qr_code_view.Image = qr_code_bitmap;
            }
        }
    }
}

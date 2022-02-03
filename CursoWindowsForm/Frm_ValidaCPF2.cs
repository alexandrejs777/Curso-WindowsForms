using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CursoWindowsForm
{
    public partial class Frm_ValidaCPF2 : Form
    {
        public Frm_ValidaCPF2()
        {
            InitializeComponent();
        }

        private void btn_Reset_Click(object sender, EventArgs e)
        {
            msk_ValidaCPF.Text = "";
        }

        private void btn_Valida_Click(object sender, EventArgs e)
        {
            string vConteudo;

            vConteudo = msk_ValidaCPF.Text;
            vConteudo = vConteudo.Replace(".", "").Replace("-", "");
            vConteudo = vConteudo.Trim();

            if (vConteudo == "")
            {
                MessageBox.Show("Você deve digitar um CPF válido!", "Mensagem de validação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else {

                if (vConteudo.Length != 11)
                {
                    MessageBox.Show("O campo de CPF precisa conter 11 dígitos!", "Mensagem de validação", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (MessageBox.Show("Você deseja realmente validar o CPF?", "Mensagem de validação", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        bool validaCPF = false;
                        validaCPF = Cls_Uteis.Valida(msk_ValidaCPF.Text);
                        if (validaCPF == true)
                        {
                            MessageBox.Show("CPF VÁLIDO", "Mensagem de validação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("CPF INVÁLIDO", "Mensagem de validação", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                } 
            }                      
        }
    }
}

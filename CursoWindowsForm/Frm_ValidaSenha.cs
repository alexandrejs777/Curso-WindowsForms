using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CursoWindowsForm
{
    public partial class Frm_ValidaSenha : Form
    {
        bool verSenhaTxt = false;

        public Frm_ValidaSenha()
        {
            InitializeComponent();
        }

        public class ChecaForcaSenha
        {
            public enum ForcaDaSenha
            {
                Inaceitavel,
                Fraca,
                Aceitavel,
                Forte,
                Segura
            }

            public int geraPontosSenha(string senha)
            {
                if (senha == null) return 0;
                int pontosPorTamanho = GetPontoPorTamanho(senha);
                int pontosPorMinusculas = GetPontoPorMinusculas(senha);
                int pontosPorMaiusculas = GetPontoPorMaiusculas(senha);
                int pontosPorDigitos = GetPontoPorDigitos(senha);
                int pontosPorSimbolos = GetPontoPorSimbolos(senha);
                int pontosPorRepeticao = GetPontoPorRepeticao(senha);

                return pontosPorTamanho + pontosPorMinusculas
                    + pontosPorMaiusculas + pontosPorDigitos
                    + pontosPorSimbolos + pontosPorRepeticao;
            }

            private int GetPontoPorTamanho(string senha)
            {
                return Math.Min(10, senha.Length) * 7;
            }

            private int GetPontoPorMinusculas(string senha)
            {
                int rawplacar = senha.Length - Regex.Replace(senha, "[a-z]", "").Length;
                return Math.Min(2, rawplacar) * 5;
            }

            private int GetPontoPorMaiusculas(string senha)
            {
                int rawplacar = senha.Length - Regex.Replace(senha, "[A-Z]", "").Length;
                return Math.Min(2, rawplacar) * 5;
            }

            private int GetPontoPorDigitos(string senha)
            {
                int rawplacar = senha.Length - Regex.Replace(senha, "[0-9]", "").Length;
                return Math.Min(2, rawplacar) * 6;
            }

            private int GetPontoPorSimbolos(string senha)
            {
                int rawplacar = Regex.Replace(senha, "[a-zA-Z0-9]", "").Length;
                return Math.Min(2, rawplacar) * 5;
            }

            private int GetPontoPorRepeticao(string senha)
            {
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"(\w)*.*\1");
                bool repete = regex.IsMatch(senha);
                if (repete)
                {
                    return 30;
                }
                else
                {
                    return 0;
                }
            }

            public ForcaDaSenha GetForcaDaSenha(string senha)
            {
                int placar = geraPontosSenha(senha);

                if (placar < 50)
                {
                    return ForcaDaSenha.Inaceitavel;
                }
                else if (placar < 60)
                {
                    return ForcaDaSenha.Fraca;
                }
                else if (placar < 80)
                {
                    return ForcaDaSenha.Aceitavel;
                }
                else if (placar < 100)
                {
                    return ForcaDaSenha.Forte;
                }
                else
                {
                    return ForcaDaSenha.Segura;
                }
            }
        }

        private void btn_VerSenha_Click(object sender, EventArgs e)
        {
            if (verSenhaTxt == false)
            {
                txt_Senha.PasswordChar = '\0';
                verSenhaTxt = true;
                btn_VerSenha.Text = "Esconder Senha";
            } else
            {
                txt_Senha.PasswordChar = '*';
                verSenhaTxt = false;
                btn_VerSenha.Text = "Ver Senha";
            }
            
        }

        private void btn_Reset_Click(object sender, EventArgs e)
        {
            txt_Senha.Text = "";
            lbl_Resultado.Text = "";
        }

        private void txt_Senha_KeyDown(object sender, KeyEventArgs e)
        {
            ChecaForcaSenha verifica = new ChecaForcaSenha();
            ChecaForcaSenha.ForcaDaSenha forca;
            forca = verifica.GetForcaDaSenha(txt_Senha.Text);
            lbl_Resultado.Text = forca.ToString();

            if (lbl_Resultado.Text == "Inaceitavel")
            {
                lbl_Resultado.ForeColor = Color.Red;
            }

            if (lbl_Resultado.Text == "Fraca")
            {
                lbl_Resultado.ForeColor = Color.Orange;
            }

            if (lbl_Resultado.Text == "Aceitavel")
            {
                lbl_Resultado.ForeColor = Color.Yellow;
            }

            if (lbl_Resultado.Text == "Forte")
            {
                lbl_Resultado.ForeColor = Color.Blue;
            }
            if (lbl_Resultado.Text == "Segura")
            {
                lbl_Resultado.ForeColor = Color.Green;
            }
        }
    }
}

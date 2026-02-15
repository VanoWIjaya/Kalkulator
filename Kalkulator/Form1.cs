using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Kalkulator
{
    public partial class Calculator : Form
    {
        List<string> expression = new List<string>();

        public Calculator()
        {
            InitializeComponent();
        }

        private double EvaluateExpression(List<string> exp) // Evaluasi ekspresi dengan memperhatikan prioritas operator
        {
            List<string> temp = new List<string>(exp); // Buat salinan dari List expression untuk dimodifikasi

            // ===== PRIORITAS 1 { x dan ÷ }=====
            for (int i = 0; i < temp.Count; i++) // Loop untuk mencari operator x dan ÷ 
            {
                if (temp[i] == "x" || temp[i] == ":") // Jika ditemukan operator x atau ÷ 
                {
                    double left = Convert.ToDouble(temp[i - 1]); // Ambil angka di sebelah kiri operator
                    double right = Convert.ToDouble(temp[i + 1]); // Ambil angka di sebelah kanan operator

                    double result = 0; // Siapkan variabel untuk menyimpan hasil operasi

                    if (temp[i] == "x") // Jika operator adalah x, lakukan perkalian
                        result = left * right;
                    else // Jika operator adalah ÷, lakukan pembagian
                    {
                        if (right == 0) // Cek pembagian dengan nol, jika benar maka lempar exception
                            throw new DivideByZeroException();
                        result = left / right;
                    }

                    // Ganti 3 item dengan hasil
                    temp[i - 1] = result.ToString(); // Ganti angka kiri dengan hasil
                    temp.RemoveAt(i);     // Hapus operator
                    temp.RemoveAt(i);     // Hapus angka kanan (karena setelah menghapus operator, angka kanan sekarang berada di indeks i)
                    i--; // Kurangi indeks i untuk kembali ke posisi sebelumnya setelah penghapusan, agar tidak melewatkan operator x atau ÷ lainnya yang mungkin ada setelahnya.

                    /* Setelah operasi selesai, kita kembali ke indeks sebelumnya untuk memastikan bahwa kita tidak melewatkan operator x atau ÷ lainnya yang mungkin ada setelahnya. 
                     * Misalnya, jika kita memiliki ekspresi "2 x 3 x 4", setelah kita menghitung "2 x 3" menjadi "6", kita ingin memastikan bahwa kita juga menghitung "6 x 4" tanpa melewatkannya. 
                     * Dengan mengurangi indeks i, kita akan memeriksa kembali posisi operator berikutnya yang mungkin masih ada di daftar temp.
                     */
                }
            }

            // ===== PRIORITAS 2 { + dan - }=====
            double finalResult = Convert.ToDouble(temp[0]); // Mulai dengan angka pertama sebagai hasil awal

            for (int i = 1; i < temp.Count; i += 2) // Loop untuk mencari operator + dan - (dimulai dari indeks 1, karena indeks 0 adalah angka pertama)
            {
                string op = temp[i]; // Ambil operator
                double num = Convert.ToDouble(temp[i + 1]); // Ambil angka di sebelah kanan operator

                if (op == "+") 
                    finalResult += num;
                else
                    finalResult -= num;
            }

            return finalResult;
        }


        private void btnNumber_Click(object sender, EventArgs e)
        {
            if (answer.Text == "0" || answer.Text == null)
                answer.Text = ((Button)sender).Text;

            else if (answer.Text == "Error")
                answer.Text = ((Button)sender).Text;
            
            else
                answer.Text += ((Button)sender).Text;
        }

        private void btnCalc_Click(object sender, EventArgs e)
        {
            // Simpan angka yang sedang tampil
            expression.Add(answer.Text);

            // Simpan operator
            string op = ((Button)sender).Text;
            expression.Add(op);

            // reset input angka berikutnya
            answer.Text = "0";

            // update tampilan atas
            answerTemp.Text = string.Join(" ", expression);
        }

        private void btnEqual_Click(object sender, EventArgs e)
        {
            try
            {
                // Simpan angka terakhir
                expression.Add(answer.Text);
                // Evaluasi ekspresi
                double result = EvaluateExpression(expression);
                // Tampilkan hasil
                answer.Text = result.ToString();
                // Reset ekspresi
                expression.Clear();
                answerTemp.Text = "";
            } catch (DivideByZeroException)
            {
                answer.Text = "Error";
                expression.Clear();
                answerTemp.Text = "";
            }
        }

        private void btnComma_Click(object sender, EventArgs e)
        {
            base.OnClick(e);
             if (!answer.Text.Contains(","))
                answer.Text += ",";
        }

        private void btnPlusMinus_Click(object sender, EventArgs e)
        {
            base.OnClick(e);
            if (answer.Text.StartsWith("-"))
                answer.Text = answer.Text.Substring(1);
            if (answer.Text == "0")
                answer.Text = "Error";
            else
                answer.Text = "-" + answer.Text;
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (answer.Text == "0")
                answer.Text = "0";
            else if (answer.Text.Length == 1)
                answer.Text = "0";
            else
                answer.Text = answer.Text.Substring(0, answer.Text.Length - 1);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            answer.Text = "0";
            answerTemp.Text = "";
            expression.Clear();
        }


    }
}

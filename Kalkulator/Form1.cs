using System;
using System.Windows.Forms;

namespace Kalkulator
{
    public partial class Calculator : Form
    {
        double firstNumber, secondNumber;
        string operation;
        public Calculator()
        {
            InitializeComponent();
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
            firstNumber = Convert.ToDouble(answer.Text);
            operation = ((Button)sender).Text;
            answer.Text = "0";
            answerTemp.Text = firstNumber.ToString() + " " + operation;
        }

        private void btnEqual_Click(object sender, EventArgs e)
        {
            double result;

            secondNumber = Convert.ToDouble(answer.Text);

            switch (operation)
            {
                case "+":
                    result = firstNumber + secondNumber;
                    answer.Text = result.ToString();
                    break;
                case "-":
                    result = firstNumber - secondNumber;
                    answer.Text = result.ToString();
                    break;
                case "x":
                    result = firstNumber * secondNumber;
                    answer.Text = result.ToString();
                    break;
                case ":":
                    if (secondNumber == 0)
                        answer.Text = "Error";
                    else
                    {
                        result = firstNumber / secondNumber;
                        answer.Text = result.ToString();
                    }
                    break;
                default:
                    break;
            }
            answerTemp.Text = "";
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
            firstNumber = 0;
            secondNumber = 0;
            operation = "";
        }


    }
}

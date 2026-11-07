using System;
using System.Windows.Forms;
using CalculatorPrivateAssembly;

namespace BasicCalculator
{
    public partial class FrmBasicCalculator : Form
    {
        public FrmBasicCalculator()
        {
            InitializeComponent();
        }

        private void FrmBasicCalculator_Load(object sender, EventArgs e)
        {
            cboOperator.SelectedIndex = 0;
        }

        // Allow only digits, one decimal point, and a leading minus sign
        private void NumberTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox txt = (TextBox)sender;

            if (char.IsControl(e.KeyChar) || char.IsDigit(e.KeyChar))
                return;

            if (e.KeyChar == '.' && !txt.Text.Contains("."))
                return;

            if (e.KeyChar == '-' && txt.SelectionStart == 0 && !txt.Text.Contains("-"))
                return;

            e.Handled = true;
        }

        private void btnCompute_Click(object sender, EventArgs e)
        {
            float first, second, result;

            if (!float.TryParse(txtFirstNumber.Text, out first) ||
                !float.TryParse(txtSecondNumber.Text, out second))
            {
                MessageBox.Show("Please enter valid numbers.", "Invalid Input",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            switch (cboOperator.Text)
            {
                case "+":
                    result = BasicComputation.Addition(first, second);
                    break;
                case "-":
                    result = BasicComputation.Subtraction(first, second);
                    break;
                case "*":
                    result = BasicComputation.Multiplication(first, second);
                    break;
                case "/":
                    if (second == 0)
                    {
                        MessageBox.Show("Cannot divide by zero.", "Invalid Operation",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    result = BasicComputation.Division(first, second);
                    break;
                default:
                    MessageBox.Show("Please select an operator.", "Invalid Input",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
            }

            lblTotal.Text = result.ToString();
        }
    }
}
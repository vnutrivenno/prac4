using System;
using System.Windows;
using System.Windows.Controls;
using System.Data;

namespace EngineeringCalculator
{
    public partial class MainWindow : Window
    {
        private string currentInput = "0";
        private string expression = "";
        private double lastResult = 0;
        private bool isNewInput = true;
        private bool isResultDisplayed = false;

        public MainWindow()
        {
            InitializeComponent();
            InitializeButtons();
            UpdateDisplay();
        }

        private void InitializeButtons()
        {
            // Numbers
            Btn0.Click += Number_Click;
            Btn1.Click += Number_Click;
            Btn2.Click += Number_Click;
            Btn3.Click += Number_Click;
            Btn4.Click += Number_Click;
            Btn5.Click += Number_Click;
            Btn6.Click += Number_Click;
            Btn7.Click += Number_Click;
            Btn8.Click += Number_Click;
            Btn9.Click += Number_Click;

            // Basic operations
            BtnAdd.Click += Operator_Click;
            BtnSubtract.Click += Operator_Click;
            BtnMultiply.Click += Operator_Click;
            BtnDivide.Click += Operator_Click;
            BtnEquals.Click += BtnEquals_Click;

            // Advanced functions
            BtnClear.Click += BtnClear_Click;
            BtnBackspace.Click += BtnBackspace_Click;
            BtnDecimal.Click += BtnDecimal_Click;
            BtnPi.Click += BtnPi_Click;
            BtnE.Click += BtnE_Click;
            BtnSquareRoot.Click += BtnSquareRoot_Click;
            BtnSquare.Click += BtnSquare_Click;
            BtnReciprocal.Click += BtnReciprocal_Click;
            BtnPlusMinus.Click += BtnPlusMinus_Click;
            BtnSin.Click += BtnSin_Click;
            BtnCos.Click += BtnCos_Click;
            BtnTan.Click += BtnTan_Click;
            BtnLog.Click += BtnLog_Click;
            BtnLn.Click += BtnLn_Click;
            BtnPowerOf10.Click += BtnPowerOf10_Click;
            BtnFactorial.Click += BtnFactorial_Click;
            BtnModulus.Click += BtnModulus_Click;
            BtnOpenBracket.Click += BtnOpenBracket_Click;
            BtnCloseBracket.Click += BtnCloseBracket_Click;
            BtnComma.Click += BtnComma_Click;
            BtnAns.Click += BtnAns_Click;
            BtnPercent.Click += BtnPercent_Click;
            BtnExp.Click += BtnExp_Click;
            BtnPower.Click += Operator_Click;
            BtnMod.Click += Operator_Click;
        }

        private void UpdateDisplay()
        {
            ResultDisplay.Text = currentInput;
            ExpressionDisplay.Text = expression;
        }

        private void Number_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            string number = button.Content.ToString();

            if (isNewInput || currentInput == "0")
            {
                currentInput = number;
                isNewInput = false;
            }
            else
            {
                currentInput += number;
            }

            UpdateDisplay();
        }

        private void Operator_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            string op = button.Content.ToString();

            if (isResultDisplayed)
            {
                expression = currentInput + " " + op + " ";
                currentInput = "0";
                isResultDisplayed = false;
            }
            else
            {
                expression += currentInput + " " + op + " ";
                currentInput = "0";
            }

            isNewInput = true;
            UpdateDisplay();
        }

        private void BtnEquals_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string fullExpression = expression + currentInput;
                double result = EvaluateExpression(fullExpression);

                lastResult = result;
                currentInput = result.ToString();
                expression = fullExpression + " =";
                isNewInput = true;
                isResultDisplayed = true;

                UpdateDisplay();
            }
            catch (Exception ex)
            {
                currentInput = "Error";
                expression = "";
                UpdateDisplay();
            }
        }

        private double EvaluateExpression(string expr)
        {
            try
            {
                expr = expr.Replace("×", "*")
                          .Replace("÷", "/")
                          .Replace("π", Math.PI.ToString())
                          .Replace("e", Math.E.ToString())
                          .Replace(",", ".")
                          .Replace("mod", "%");

                var dataTable = new DataTable();
                var result = dataTable.Compute(expr, null);
                return Convert.ToDouble(result);
            }
            catch
            {
                throw new Exception("Invalid expression");
            }
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            currentInput = "0";
            expression = "";
            isNewInput = true;
            isResultDisplayed = false;
            UpdateDisplay();
        }

        private void BtnBackspace_Click(object sender, RoutedEventArgs e)
        {
            if (currentInput.Length > 1)
            {
                currentInput = currentInput.Substring(0, currentInput.Length - 1);
            }
            else
            {
                currentInput = "0";
                isNewInput = true;
            }
            UpdateDisplay();
        }

        private void BtnDecimal_Click(object sender, RoutedEventArgs e)
        {
            if (!currentInput.Contains("."))
            {
                currentInput += ".";
                isNewInput = false;
                UpdateDisplay();
            }
        }

        private void BtnPi_Click(object sender, RoutedEventArgs e)
        {
            currentInput = Math.PI.ToString();
            isNewInput = true;
            UpdateDisplay();
        }

        private void BtnE_Click(object sender, RoutedEventArgs e)
        {
            currentInput = Math.E.ToString();
            isNewInput = true;
            UpdateDisplay();
        }

        private void BtnSquareRoot_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double value = double.Parse(currentInput);
                if (value < 0) throw new Exception();
                double result = Math.Sqrt(value);
                currentInput = result.ToString();
                expression = "√(" + value + ")";
                isNewInput = true;
                isResultDisplayed = true;
                UpdateDisplay();
            }
            catch
            {
                currentInput = "Error";
                UpdateDisplay();
            }
        }

        private void BtnSquare_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double value = double.Parse(currentInput);
                double result = Math.Pow(value, 2);
                currentInput = result.ToString();
                expression = "sqr(" + value + ")";
                isNewInput = true;
                isResultDisplayed = true;
                UpdateDisplay();
            }
            catch
            {
                currentInput = "Error";
                UpdateDisplay();
            }
        }

        private void BtnReciprocal_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double value = double.Parse(currentInput);
                if (value == 0) throw new Exception();
                double result = 1 / value;
                currentInput = result.ToString();
                expression = "1/(" + value + ")";
                isNewInput = true;
                isResultDisplayed = true;
                UpdateDisplay();
            }
            catch
            {
                currentInput = "Error";
                UpdateDisplay();
            }
        }

        private void BtnPlusMinus_Click(object sender, RoutedEventArgs e)
        {
            if (currentInput.StartsWith("-"))
            {
                currentInput = currentInput.Substring(1);
            }
            else if (currentInput != "0")
            {
                currentInput = "-" + currentInput;
            }
            UpdateDisplay();
        }

        private void BtnSin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double value = double.Parse(currentInput);
                double result = Math.Sin(value * Math.PI / 180);
                currentInput = result.ToString();
                expression = "sin(" + value + ")";
                isNewInput = true;
                isResultDisplayed = true;
                UpdateDisplay();
            }
            catch
            {
                currentInput = "Error";
                UpdateDisplay();
            }
        }

        private void BtnCos_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double value = double.Parse(currentInput);
                double result = Math.Cos(value * Math.PI / 180);
                currentInput = result.ToString();
                expression = "cos(" + value + ")";
                isNewInput = true;
                isResultDisplayed = true;
                UpdateDisplay();
            }
            catch
            {
                currentInput = "Error";
                UpdateDisplay();
            }
        }

        private void BtnTan_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double value = double.Parse(currentInput);
                double result = Math.Tan(value * Math.PI / 180);
                currentInput = result.ToString();
                expression = "tan(" + value + ")";
                isNewInput = true;
                isResultDisplayed = true;
                UpdateDisplay();
            }
            catch
            {
                currentInput = "Error";
                UpdateDisplay();
            }
        }

        private void BtnLog_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double value = double.Parse(currentInput);
                if (value <= 0) throw new Exception();
                double result = Math.Log10(value);
                currentInput = result.ToString();
                expression = "log(" + value + ")";
                isNewInput = true;
                isResultDisplayed = true;
                UpdateDisplay();
            }
            catch
            {
                currentInput = "Error";
                UpdateDisplay();
            }
        }

        private void BtnLn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double value = double.Parse(currentInput);
                if (value <= 0) throw new Exception();
                double result = Math.Log(value);
                currentInput = result.ToString();
                expression = "ln(" + value + ")";
                isNewInput = true;
                isResultDisplayed = true;
                UpdateDisplay();
            }
            catch
            {
                currentInput = "Error";
                UpdateDisplay();
            }
        }

        private void BtnPowerOf10_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double value = double.Parse(currentInput);
                double result = Math.Pow(10, value);
                currentInput = result.ToString();
                expression = "10^(" + value + ")";
                isNewInput = true;
                isResultDisplayed = true;
                UpdateDisplay();
            }
            catch
            {
                currentInput = "Error";
                UpdateDisplay();
            }
        }

        private void BtnFactorial_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int value = int.Parse(currentInput);
                if (value < 0 || value > 20) throw new Exception(); // 20! is max for long

                long result = 1;
                for (int i = 2; i <= value; i++)
                {
                    result *= i;
                }

                currentInput = result.ToString();
                expression = value + "!";
                isNewInput = true;
                isResultDisplayed = true;
                UpdateDisplay();
            }
            catch
            {
                currentInput = "Error";
                UpdateDisplay();
            }
        }

        private void BtnModulus_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double value = double.Parse(currentInput);
                double result = Math.Abs(value);
                currentInput = result.ToString();
                expression = "|" + value + "|";
                isNewInput = true;
                isResultDisplayed = true;
                UpdateDisplay();
            }
            catch
            {
                currentInput = "Error";
                UpdateDisplay();
            }
        }

        private void BtnOpenBracket_Click(object sender, RoutedEventArgs e)
        {
            expression += "(";
            UpdateDisplay();
        }

        private void BtnCloseBracket_Click(object sender, RoutedEventArgs e)
        {
            expression += currentInput + ")";
            currentInput = "0";
            isNewInput = true;
            UpdateDisplay();
        }

        private void BtnComma_Click(object sender, RoutedEventArgs e)
        {
            if (!currentInput.Contains("."))
            {
                currentInput += ".";
                isNewInput = false;
                UpdateDisplay();
            }
        }

        private void BtnAns_Click(object sender, RoutedEventArgs e)
        {
            currentInput = lastResult.ToString();
            isNewInput = true;
            UpdateDisplay();
        }

        private void BtnPercent_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double value = double.Parse(currentInput);
                double result = value / 100;
                currentInput = result.ToString();
                expression = value + "%";
                isNewInput = true;
                isResultDisplayed = true;
                UpdateDisplay();
            }
            catch
            {
                currentInput = "Error";
                UpdateDisplay();
            }
        }

        private void BtnExp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double value = double.Parse(currentInput);
                double result = Math.Exp(value);
                currentInput = result.ToString();
                expression = "exp(" + value + ")";
                isNewInput = true;
                isResultDisplayed = true;
                UpdateDisplay();
            }
            catch
            {
                currentInput = "Error";
                UpdateDisplay();
            }
        }
    }
}

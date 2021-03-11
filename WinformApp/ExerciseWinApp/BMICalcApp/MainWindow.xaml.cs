using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BMICalcApp
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnCalc_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TxtHeight.Text) && string.IsNullOrEmpty(TxtWeight.Text))
            {
                TxtResult.Text = "키와 체중을 입력해 주세요.";
                return;
            }

            double height = double.Parse(TxtHeight.Text) / 100.0;
            double weight = double.Parse(TxtWeight.Text);
            double bmi = weight / (height * height);
            TxtResult.Text = $"당신의 BMI는  {bmi}  입니다."; 
        }
    }
}

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
using wpfPracticeApp.BusinessLogic;

namespace wpfPracticeApp
{
    /// <summary>
    /// Product.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Products : Page
    {
        private ProductsFactory factory;

        public Products()
        {
            InitializeComponent();
        }

        private void Page_Initialized(object sender, EventArgs e)
        {
            #region commit
            /*
            var cars = new List<Car>();
            for (int i = 0; i < 10; i++)
            {
                byte red = (byte)(i % 3 == 0 ? 255 : (i * 50) % 255);
                byte green = 0;
                byte blue = (byte)(i % 3 == 0 ? 255 : (i * 90) % 255);
                cars.Add(new Car()
                {
                    Speed = i * 10,
                    MainColor = Color.FromRgb(red, green, blue)
                });
            }
            this.DataContext = cars;
            */
            #endregion
            factory = new ProductsFactory();

            GrdProducts.ItemsSource = factory.GetAllProducts();
        }

        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            GrdProducts.ItemsSource = factory.FindProducts(TxtSearch.Text);
        }
    }
}

using Sjerrul.Bloqchain.Ledger;
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

namespace Sjerrul.Bloqchain.Demo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int difficulty = 3;

        private BloqChain<string> bloqchain;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void BloqControl_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.bloqchain = new BloqChain<string>(difficulty);

            RenderBloqChain();
        }


        private void RenderBloqChain()
        {
            BloqGrid.Children.Clear();

            int x = 0;
            foreach (var bloq in this.bloqchain)
            {
                BloqControl bloqControl = new BloqControl(bloq);
                bloqControl.Margin = new Thickness(x, 10, 0, 0);
                bloqControl.HorizontalAlignment = HorizontalAlignment.Left;
                bloqControl.VerticalAlignment = VerticalAlignment.Top;
                bloqControl.Width = 230;
                bloqControl.Height = 230;

                BloqGrid.Children.Add(bloqControl);
                x += 240;
            }
        }

        private void ButtonNewBloq_Click(object sender, RoutedEventArgs e)
        {
            this.bloqchain.AddBloq("New data");
            RenderBloqChain();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (this.bloqchain.IsValid)
            {
                this.BloqGrid.Background = Brushes.Green;
            }
            else
            {
                this.BloqGrid.Background = Brushes.Red;
            }

            foreach (var item in this.BloqGrid.Children.OfType<BloqControl>())
            {
                item.Validate();
            }
        }
    }
}

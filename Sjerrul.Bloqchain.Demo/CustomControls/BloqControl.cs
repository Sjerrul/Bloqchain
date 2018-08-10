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
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:Sjerrul.Bloqchain.WpfControls"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:Sjerrul.Bloqchain.WpfControls;assembly=Sjerrul.Bloqchain.WpfControls"
    ///
    /// You will also need to add a project reference from the project where the XAML file lives
    /// to this project and Rebuild to avoid compilation errors:
    ///
    ///     Right click on the target project in the Solution Explorer and
    ///     "Add Reference"->"Projects"->[Select this project]
    ///
    ///
    /// Step 2)
    /// Go ahead and use your control in the XAML file.
    ///
    ///     <MyNamespace:CustomControl1/>
    ///
    /// </summary>
    public partial class BloqControl : UserControl
    {
        /// <summary>
        /// The bloq that this control represents
        /// </summary>
        private Bloq<string> bloq;

        /// <summary>
        /// Initializes a new instance of the <see cref="BloqControl"/> class.
        /// </summary>
        /// <param name="bloq">The bloq.</param>
        public BloqControl(Bloq<string> bloq)
        {
            InitializeComponent();

            this.bloq = bloq ?? throw new ArgumentNullException(nameof(bloq));
            this.TextboxIndex.Text = this.bloq.Index.ToString();
            this.TextboxNonce.Text = this.bloq.Nonce.ToString();
            this.TextboxPreviousHash.Text = this.bloq.PreviousHash;
            this.TextboxHash.Text = this.bloq.Hash;
            this.TextboxData.Text = this.bloq.Data;
            this.TextboxTimestamp.Text = this.bloq.Timestamp.ToString();
        }

        /// <summary>
        /// Validates the validity of the bloq.
        /// </summary>
        public void  Validate()
        {
            this.MainGrid.Background = Brushes.Red;
            if (this.bloq.IsValid)
            {
                this.MainGrid.Background = Brushes.Green;
            }
        }

        /// <summary>
        /// Handles the LostFocus event of the TextboxIndex control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void TextboxIndex_LostFocus(object sender, RoutedEventArgs e)
        {
            this.bloq.Index = int.Parse(this.TextboxIndex.Text);
        }

        /// <summary>
        /// Handles the LostFocus event of the TextboxData control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void TextboxData_LostFocus(object sender, RoutedEventArgs e)
        {
            this.bloq.Data = this.TextboxData.Text;
        }

        /// <summary>
        /// Handles the LostFocus event of the TextboxTimestamp control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void TextboxTimestamp_LostFocus(object sender, RoutedEventArgs e)
        {
            this.bloq.Timestamp = DateTime.Parse(this.TextboxTimestamp.Text);
        }

        /// <summary>
        /// Handles the LostFocus event of the TextboxNonce control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void TextboxNonce_LostFocus(object sender, RoutedEventArgs e)
        {
            this.bloq.Nonce = int.Parse(this.TextboxNonce.Text);
        }

        private void TextboxPreviousHash_LostFocus(object sender, RoutedEventArgs e)
        {
            this.bloq.PreviousHash = this.TextboxPreviousHash.Text;

        }

        private void ButtonRemine_Click(object sender, RoutedEventArgs e)
        {
            this.bloq.Mine(3);
            this.TextboxHash.Text = this.bloq.Hash;
        }
    }
}

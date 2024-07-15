using System;
using System.Windows;
using System.Windows.Controls;

namespace Optika_Lens
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            mainFrame.Navigate(new IntroPage());
        }

        public void NavigateToSearchPage()
        {
            mainFrame.Navigate(new SearchPage());
        }

        public void NavigateToAddPatientPage()
        {
            mainFrame.Navigate(new AddPatientPage());
        }
    }
}

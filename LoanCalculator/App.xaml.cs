using LoanCalculator.View;

namespace LoanCalculator
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(root: new LoanProgramsPage());
        }
    }
}

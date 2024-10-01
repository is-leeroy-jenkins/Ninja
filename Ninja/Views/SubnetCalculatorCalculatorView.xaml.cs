using Ninja.ViewModels;

namespace Ninja.Views
{
    using ViewModels;

    public partial class SubnetCalculatorCalculatorView
    {
        private readonly SubnetCalculatorCalculatorViewModel _viewModel = new();

        public SubnetCalculatorCalculatorView()
        {
            InitializeComponent();
            DataContext = _viewModel;
        }
    }
}
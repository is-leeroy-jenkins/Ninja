using Ninja.ViewModels;

namespace Ninja.Views
{
    using ViewModels;

    public partial class SubnetCalculatorHostView
    {
        private readonly SubnetCalculatorHostViewModel _viewModel = new();

        public SubnetCalculatorHostView()
        {
            InitializeComponent();
            DataContext = _viewModel;
        }

        public void OnViewHide()
        {
            _viewModel.OnViewHide();
        }

        public void OnViewVisible()
        {
            _viewModel.OnViewVisible();
        }
    }
}
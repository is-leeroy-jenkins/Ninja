using MahApps.Metro.Controls.Dialogs;
using Ninja.ViewModels;

namespace Ninja.Views
{
    using ViewModels;

    public partial class BitCalculatorView
    {
        private readonly BitCalculatorViewModel _viewModel = new(DialogCoordinator.Instance);

        public BitCalculatorView()
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
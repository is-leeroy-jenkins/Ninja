using Ninja.ViewModels;

namespace Ninja.Views
{
    using ViewModels;

    public partial class LookupHostView
    {
        private readonly LookupHostViewModel _viewModel = new();

        public LookupHostView()
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

namespace Ninja.Views
{
    using System.Threading;
    using System.Windows.Controls;

    /// <summary>
    /// RouteWindow.xaml
    /// </summary>
    public partial class RouteWindow : UserControl
    {
        /// <summary>
        /// The path
        /// </summary>
        private protected object _entry = new object();

        /// <summary>
        /// The time
        /// </summary>
        private protected int _time;

        /// <summary>
        /// The timer
        /// </summary>
        private protected Timer _timer;

        /// <summary>
        /// The timer
        /// </summary>
        private protected TimerCallback _timerCallback;

        public RouteWindow( )
        {
            InitializeComponent( );
        }
    }
}

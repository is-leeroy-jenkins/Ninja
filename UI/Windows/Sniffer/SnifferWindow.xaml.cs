
namespace Ninja.Views
{
    using System.Threading;
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// SnifferWindow.xaml
    /// </summary>
    public partial class SnifferWindow : UserControl
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

        public SnifferWindow()
        {
            InitializeComponent();
        }
    }
}

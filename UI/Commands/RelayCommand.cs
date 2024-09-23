

namespace Ninja
{
    using System;
    using System.Windows.Input;

    /// <summary>
    /// RelayCommand allows you to inject the command's logic via delegates
    /// passed into its contructor. This method
    /// enables ViewModel classes to implement commands in a concise manner.
    /// </summary>
    /// <seealso cref="System.Windows.Input.ICommand" />

    public class RelayCommand : ICommand
    {
        /// <summary>
        /// The execute
        /// </summary>
        private Action<object> _execute;
        /// <summary>
        /// The can execute
        /// </summary>
        private Func<object, bool> _canExecute;
        /// <summary>
        /// The command tasks
        /// </summary>
        private Action _commandTasks;
        /// <summary>
        /// Initializes a new instance of the <see cref="RelayCommand"/> class.
        /// </summary>
        /// <param name="workTodo">The work todo.</param>
        public RelayCommand(Action workTodo)
        {
            _commandTasks = workTodo;

        }
        /// <summary>
        /// Initializes a new instance of the <see cref="RelayCommand"/> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        public RelayCommand(Action<object> execute)
        {
            _execute = execute;
            _canExecute = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RelayCommand"/> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <param name="canExecute">The can execute.</param>
        public RelayCommand(Action<object> execute, Func<object, bool> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        /// <inheritdoc />
        /// <summary>
        /// CanExecuteChanged delegates the event subscription to the
        /// CommandManager.RequerySuggested event.
        /// This ensures that the WPF commanding infrastructure asks all
        /// RelayCommand objects if they can execute whenever
        /// it asks the built-in commands.
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <inheritdoc />
        /// <summary>
        /// Defines the method that determines whether the
        /// command can execute in its current state.
        /// </summary>
        /// <param name="parameter">Data used by the command.
        /// If the command does not require data to be passed, this object
        /// can be set to <see langword="null" />.</param>
        /// <returns>
        ///   <see langword="true" /> if this command can be executed;
        /// otherwise, <see langword="false" />.
        /// </returns>
        public bool CanExecute(object parameter)
        {
            return _canExecute == null || CanExecute(parameter);
        }

        /// <inheritdoc />
        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">Data used by the command.
        /// If the command does not require data to be passed,
        /// this object can be set to <see langword="null" />.
        /// </param>
        public void Execute(object parameter)
        {
            _execute(parameter);
        }
    }
}

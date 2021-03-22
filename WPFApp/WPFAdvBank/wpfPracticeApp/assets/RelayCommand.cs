using System;
using System.Windows.Input;
using System.Diagnostics;

// Source: http://blog.walteralmeida.com/2010/06/modelviewviewmodel-design-pattern-an-implementation-of-relaycommand-for-silverlight.html
//Relay Command class for WPF.
/*
   This class makes use of the CommandManager object and the RequerySuggested event. 
   When a user actions the UI in WPF, this triggers the CommandManager to requery all the commands.
*/

public class RelayCommand : ICommand
{

    #region Fields

    readonly Action<object> _execute;
    readonly Predicate<object> _canExecute;

    #endregion // Fields

    #region Constructors

    public RelayCommand(Action<object> execute)
        : this(execute, null)
    {
    }

    public RelayCommand(Action<object> execute, Predicate<object> canExecute)
    {
        if (execute == null)
            throw new ArgumentNullException("execute");

        _execute = execute;
        _canExecute = canExecute;
    }

    #endregion // Constructors

    #region ICommand Members

    [DebuggerStepThrough]
    public bool CanExecute(object parameter)
    {
        return _canExecute == null ? true : _canExecute(parameter);
    }

    public event EventHandler CanExecuteChanged
    {
        add { CommandManager.RequerySuggested += value; }
        remove { CommandManager.RequerySuggested -= value; }
    }

    public void Execute(object parameter)
    {
        _execute(parameter);
    }

    #endregion // ICommand Members
}


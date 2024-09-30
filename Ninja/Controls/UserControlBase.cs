using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace Ninja.Controls;

public abstract class UserControlBase : UserControl, INotifyPropertyChanged
{
    #region PropertyChangedEventHandler

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    #endregion
}
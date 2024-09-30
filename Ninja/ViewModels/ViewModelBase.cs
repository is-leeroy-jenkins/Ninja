using System.Windows.Input;
using Ninja.Utilities;

namespace Ninja.ViewModels;

using Utilities;

public abstract class ViewModelBase : PropertyChangedBase
{
    public ICommand CopyDataToClipboardCommand => new RelayCommand(CopyDataToClipboardAction);

    private static void CopyDataToClipboardAction(object data)
    {
        ClipboardHelper.SetClipboard(data.ToString());
    }
}
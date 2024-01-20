using CommunityToolkit.Mvvm.Messaging.Messages;
using System.Windows.Controls;

namespace Metalhead.Examples.Mvvm.WpfSGDI
{
    public class ChangedViewMessage(UserControl view) : ValueChangedMessage<UserControl>(view)
    {
    }
}

using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;

using Metalhead.Examples.Mvvm.WpfSGDI.ViewModels;

namespace Metalhead.Examples.Mvvm.WpfSGDI.Views;

public partial class Name : UserControl
{
    public Name()
    {
        InitializeComponent();
        DataContext = App.Current.Host.Services.GetService<NameViewModel>();
    }
}

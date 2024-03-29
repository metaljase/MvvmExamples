﻿using System.Windows;

using Metalhead.Examples.Mvvm.WpfSGDI.ViewModels;

namespace Metalhead.Examples.Mvvm.WpfSGDI.Views;

public partial class Shell : Window
{
    public Shell(ShellViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}

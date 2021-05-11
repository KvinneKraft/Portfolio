﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SDScan
{
    public partial class MainWindow : Window
    {
	public MainWindow()
	{
	    InitializeComponent();

	    Image image = new Image();

	    image.RenderSize = new Size(200, 200);
	}

	void mBarExit(object sender, RoutedEventArgs e)
	{
	    Environment.Exit(-1);
	}

	private void mBarDrag(object sender, MouseButtonEventArgs e)
	{
	    if (e.ChangedButton == MouseButton.Left)
	    {
		DragMove();
	    }
	}
    }
}

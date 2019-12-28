﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace YandereSimulatorLauncher2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            UnpackVideoResources();
            InitializeComponent();
            HandleVisualStyles();
        }

        protected override sealed void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
        }

        private void HandleVisualStyles()
        {
            if (NativeMethods.DwmCompositionIsEnabled)
            //if (false)
            {
                EnableHighQuality();
            }
            else
            {
                DisableHighQuality();
            }
        }

        private void EnableHighQuality()
        {
            // Allow shadows
            Width = 906;
            Height = 701;
            AllowsTransparency = true;
            Background = Brushes.Transparent;
            ShadowBorder.Margin = new Thickness(13);

            //
        }

        private void DisableHighQuality()
        {
            // Disable shadows
            Width = 882;
            Height = 677;
            AllowsTransparency = false;
            Background = Brushes.Black;
            ShadowBorder.Margin = new Thickness(1);

            //
        }

        private void UnpackVideoResources()
        {
            // On my PC, these two operations add a total of ~19ms to load time.
            // Probably don't need to do anything fancy.
            UnpackVideoFile(YandereSimulatorLauncher2.Properties.Resources.mainpanel_dere, "mainpanel-dere.wmv");
            UnpackVideoFile(YandereSimulatorLauncher2.Properties.Resources.mainpanel_yan, "mainpanel-yan.wmv");
        }

        private static void UnpackVideoFile(byte[] inResource, string inFilename)
        {
            try
            {
                File.WriteAllBytes(inFilename, inResource);
            }
            catch (Exception)
            {
                // Just cosmetic.
                // Probably lock to just the static images.
            }
        }

        private void TitleBar_MouseDown(object sender, MouseButtonEventArgs evt)
        {
            if (evt.ChangedButton == MouseButton.Left && evt.Handled == false)
            {
                DragMove();
            }
        }

        private void MainPanelDereVideo_OnMediaEnded(object sender, RoutedEventArgs e)
        {
            MainPanelDereVideo.Position = new TimeSpan(0, 0, 1);
        }

        private void MainPanelDereVideo_OnLoaded(object sender, RoutedEventArgs e)
        {
            MainPanelDereVideo.Play();
        }
    }
}

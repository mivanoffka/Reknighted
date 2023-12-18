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
using Reknighted.Collections;
using Reknighted.Controller;
using Reknighted.Model;

namespace Reknighted
{
    public partial class StartWindow : Window
    {

        public StartWindow()
        {
            InitializeComponent();
            Items.Initialize();
            Entities.Initialize();

            Game.GenerateJorneyCost();
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void newGameButton_Click(object sender, RoutedEventArgs e)
        {
            SelectionWindow selectionWindow = new SelectionWindow();
            selectionWindow.Show();

            this.Close();
        }


        private void loadGameButtom_Click(object sender, RoutedEventArgs e)
        {   
            //gameWindow.Show();
            SaveWindow saveWindow = new SaveWindow();
            saveWindow.isSaving = false;
            if (saveWindow.ShowDialog() == false) return;
            
            this.Close();
        }

        private void settingsButton_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow settingsWindow = new SettingsWindow();
            settingsWindow.Show();
        }
    }
}

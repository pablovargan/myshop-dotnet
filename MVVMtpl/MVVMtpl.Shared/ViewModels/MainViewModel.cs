using MVVMtpl.Base;
using MVVMtpl.Models;
using MVVMtpl.Services;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using System.Collections.Generic;
using System;
using Windows.UI.Xaml.Controls;
using MVVMtpl.Views;

namespace MVVMtpl.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        private NavigationService navigationService;
        private NetworkService networkService;
        private Frame scenarioFrame;
        private List<Scenario> scenarios;

        public ICommand ScenarioCommand { get; set; }

        public Frame ScenarioFrame
        {
            get { return this.scenarioFrame; }
            set { this.Set(ref this.scenarioFrame, value); }
        }

        public List<Scenario> Scenarios
        {
            get { return this.scenarios; }
            set { Set(ref this.scenarios, value); }
        }

        public MainViewModel(NavigationService navigationService, NetworkService networkService)
        {
            this.navigationService = navigationService;
            this.networkService = networkService;

            this.scenarioFrame = new Frame();
            this.scenarios = new List<Scenario>
            {
                new Scenario() { Title = "Tienda", ClassType = typeof(ShopPage) },
                new Scenario() { Title = "Pedidos", ClassType = typeof(OrdersPage) },
                new Scenario() { Title = "Pedidos SQLite", ClassType = typeof(OrdersSqlPage) },
                new Scenario() { Title = "Productos Mobile", ClassType = typeof(ProductsMobilePage)}
            };

            ScenarioCommand = new RelayCommand<object>(ScenarioExecute);
        }

        private void ScenarioExecute(object sender)
        {
            ListBox scenarioListBox = sender as ListBox;
            Scenario s = scenarioListBox.SelectedItem as Scenario;
            if(s != null)
            {
                ScenarioFrame.Navigate(s.ClassType);
            }
        }
    }
}

using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ToDoList.ViewModels;

namespace ToDoList
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            if (MainPage.BindingContext is MainViewModel viewModel)
            {
                viewModel.LoadToDoListCollectionPreferences();
            }
        }

        protected override void OnSleep()
        {
            if (MainPage.BindingContext is MainViewModel viewModel)
            {
                viewModel.SaveToDoListCollectionPreferences();
            }
        }

        protected override void OnResume()
        {
            if (MainPage.BindingContext is MainViewModel viewModel)
            {
                viewModel.LoadToDoListCollectionPreferences();
            }
        }
    }
}

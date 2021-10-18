using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.ViewModels;
using Xamarin.Forms;

namespace ToDoList
{
    public partial class MainPage : ContentPage
    {
        public Grid PopUpDeleteProp => PopUpDelete;
        //public Image PortalPopUpProp => PortalPopUp;
        public ImageButton PopUpDeleteCancelProp => PopUpDeleteCancel;
        public ImageButton PopUpDeleteConfirmProp => PopUpDeleteConfirm;
        public Image PortalProp => Portal;
        public MainPage()
        {
            BindingContext = new MainViewModel(this);
            InitializeComponent();
        }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Shapes;
using MyTask = ToDoList.Models.Task;
using System.Timers;

namespace ToDoList.ViewModels
{
    public class MainViewModel : BindableObject
    {
        public enum Themes
        {
            Light = 0,
            Dark = 1
        }

        public string DarkThemeColor = "#2E2E2E";
        public string DarkThemeTextColor = "#FFFFFF";
        public string DarkThemeTaskIsDone = "Green";
        public string DarkThemeTaskIsNotDone = "Red";
        public string DarkThemePlaceholderTextColor = "#ACACAC";
        public string DarkAddButtonColor = "#FFFFFF";


        public string LightThemeColor = "#FFFFFF";
        public string LightThemeTextColor = "#000000";
        public string LightThemeTaskIsDone = "Blue";
        public string LightThemeTaskIsNotDone = "Yellow";
        public string LightThemePlaceholderTextColor = "#B4B4B4";
        public string LightAddButtonColor = "#A8A8A8";

        private readonly MainPage _page;

        private MyTask selectedItem = null;

        public MainViewModel(ContentPage page)
        {
            _page = (MainPage)page;
            EntryNameAddedTask = "";
            SetTimer();
        }

        private void SetTimer()
        {
            // Create a timer with a two second interval.
            System.Timers.Timer aTimer = new System.Timers.Timer(7000);
            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += ATimerOnElapsed;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
            aTimer.Start();
        }

        private void ATimerOnElapsed(object sender, ElapsedEventArgs e)
        {
            ImageRotationAsync();
        }

        public async void ImageRotationAsync()
        {
            //await Task.Run(() => { _page.PortalProp.RelRotateTo(360, 4000); });
            await Task.WhenAll(
                _page.PortalProp.RelRotateTo(360, 7000),
                _page.PopUpDeleteCancelProp.RelRotateTo(360, 7000),
                _page.PopUpDeleteConfirmProp.RelRotateTo(360, 7000)
            );
        }

        private Themes _theme { get; set; }
        public Themes Theme
        {
            get => _theme;
            set
            {
                _theme = value;
                //Task.Factory.StartNew(ImageRotation, TaskCreationOptions.LongRunning);
                if (_theme == Themes.Dark)
                {
                    BackgroundColor = DarkThemeColor;
                    TextColor = DarkThemeTextColor;
                    TaskIsDone = DarkThemeTaskIsDone;
                    TaskIsNotDone = DarkThemeTaskIsNotDone;
                    PlaceholderTextColor = DarkThemePlaceholderTextColor;
                    AddButtonColor = DarkAddButtonColor;
                }
                else
                {
                    BackgroundColor = LightThemeColor;
                    TextColor = LightThemeTextColor;
                    TaskIsDone = LightThemeTaskIsDone;
                    TaskIsNotDone = LightThemeTaskIsNotDone;
                    PlaceholderTextColor = LightThemePlaceholderTextColor;
                    AddButtonColor = LightAddButtonColor;
                }
                OnPropertyChanged(nameof(Theme));
            }
        }

        private string _addButtonColor { get; set; }
        public string AddButtonColor
        {
            get => _addButtonColor;
            set
            {
                if (_addButtonColor != value)
                {
                    _addButtonColor = value;
                    OnPropertyChanged(nameof(AddButtonColor));
                }
            }
        }

        private string _taskIsDone { get; set; }
        public string TaskIsDone
        {
            get => _taskIsDone;
            set
            {
                if (_taskIsDone != value)
                {
                    _taskIsDone = value;
                    OnPropertyChanged(nameof(TaskIsDone));
                }
            }
        }

        private string _taskIsNotDone { get; set; }
        public string TaskIsNotDone
        {
            get => _taskIsNotDone;
            set
            {
                if (_taskIsNotDone != value)
                {
                    _taskIsNotDone = value;
                    OnPropertyChanged(nameof(TaskIsNotDone));
                }
            }
        }

        private string _placeholderTextColor { get; set; }
        public string PlaceholderTextColor
        {
            get => _placeholderTextColor;
            set
            {
                if (_placeholderTextColor != value)
                {
                    _placeholderTextColor = value;
                    OnPropertyChanged(nameof(PlaceholderTextColor));
                }
            }
        }

        private string _textColor { get; set; }
        public string TextColor
        {
            get => _textColor;
            set
            {
                if (_textColor != value)
                {
                    _textColor = value;
                    OnPropertyChanged(nameof(TextColor));
                }
            }
        }

        private string _backgroundColor { get; set; }
        public string BackgroundColor
        {
            get => _backgroundColor;
            set
            {
                if (_backgroundColor != value)
                {
                    _backgroundColor = value;
                    OnPropertyChanged(nameof(BackgroundColor));
                }
            }
        }

        private Command _addTaskToToDoListCollection;
        public Command AddTaskToToDoListCollection => _addTaskToToDoListCollection ?? (_addTaskToToDoListCollection = new Command(() =>
        {
            if (EntryNameAddedTask.Length != 0)
            {
                ToDoListCollection.Insert(0, new MyTask() { Name = EntryNameAddedTask, Description = "", IsDone = false });
                EntryNameAddedTask = "";
            }
        }));

        private Command _deleteTaskFromToDoCollection;
        public Command DeleteTaskFromToDoCollection => _deleteTaskFromToDoCollection ?? (_deleteTaskFromToDoCollection = new Command<MyTask>(item =>
        {
            EnablePopUpDelete();
            selectedItem = item;
            //ToDoListCollection.Remove(item);
        }));

        private Command _confirmToDeleteItemFromToDoCollection;
        public Command ConfirmToDeleteItemFromToDoCollection => _confirmToDeleteItemFromToDoCollection ?? (_confirmToDeleteItemFromToDoCollection = new Command(() =>
        {
            DisablePopUpDelete();
            ToDoListCollection.Remove(selectedItem);
            selectedItem = null;
        }));

        private Command _cancelToDeleteItemFromToDoCollection;
        public Command CancelToDeleteItemFromToDoCollection => _cancelToDeleteItemFromToDoCollection ?? (_cancelToDeleteItemFromToDoCollection = new Command(() =>
        {
            DisablePopUpDelete();
            selectedItem = null;
        }));

        private Command _changeTheme;
        public Command ChangeTheme => _changeTheme ?? (_changeTheme = new Command(() =>
        {
            if (Theme == Themes.Dark)
            {
                BackgroundColor = LightThemeColor;
                TextColor = LightThemeTextColor;
                TaskIsDone = LightThemeTaskIsDone;
                TaskIsNotDone = LightThemeTaskIsNotDone;
                PlaceholderTextColor = LightThemePlaceholderTextColor;
                Theme = Themes.Light;
            }
            else
            {
                BackgroundColor = DarkThemeColor;
                TextColor = DarkThemeTextColor;
                TaskIsDone = DarkThemeTaskIsDone;
                TaskIsNotDone = DarkThemeTaskIsNotDone;
                PlaceholderTextColor = DarkThemePlaceholderTextColor;
                Theme = Themes.Dark;
            }
        }));

        private ObservableCollection<MyTask> _toDoListCollection { get; set; }

        public ObservableCollection<MyTask> ToDoListCollection
        {
            get => _toDoListCollection;
            set
            {
                _toDoListCollection = value;
                OnPropertyChanged(nameof(ToDoListCollection));
            }
        }

        private string _entryNameAddedTask { get; set; }

        public string EntryNameAddedTask
        {
            get => _entryNameAddedTask;
            set
            {
                _entryNameAddedTask = value;
                OnPropertyChanged(nameof(EntryNameAddedTask));
            }
        }

        public void SaveToDoListCollectionPreferences()
        {
            Preferences.Set("ToDoList", JsonConvert.SerializeObject(ToDoListCollection));
            Preferences.Set("Theme", (int)Theme);
        }

        public void LoadToDoListCollectionPreferences()
        {
            var itemJson = Preferences.Get("ToDoList", "[]");
            ToDoListCollection = JsonConvert.DeserializeObject<ObservableCollection<MyTask>>(itemJson);
            var themeJson = Preferences.Get("Theme", 0);
            Theme = (Themes)themeJson;
        }

        public void DisablePopUpDelete()
        {
            _page.PopUpDeleteProp.IsVisible = false;
            _page.PopUpDeleteProp.InputTransparent = true;
        }

        public void EnablePopUpDelete()
        {
            _page.PopUpDeleteProp.IsVisible = true;
            _page.PopUpDeleteProp.InputTransparent = false;
        }
    }
}

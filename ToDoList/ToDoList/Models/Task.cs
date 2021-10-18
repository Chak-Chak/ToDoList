using Xamarin.Forms;
using ToDoList.ViewModels;

namespace ToDoList.Models
{
    public class Task : BindableObject
    {
        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private string _description;
        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        private bool _isDone;
        public bool IsDone
        {
            get => _isDone;
            set
            {
                _isDone = value;
                OnPropertyChanged(nameof(IsDone));
                //OnPropertyChanged(nameof(MainViewModel.TaskIsDone));
                //OnPropertyChanged(nameof(MainViewModel.TaskIsNotDone));
            }
        }
    }
}

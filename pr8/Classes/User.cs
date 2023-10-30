using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace pr8.Classes
{
    class User : INotifyPropertyChanged
    {
        private string username;
        public string Username {
            get
            {
                return username;
            }
            set
            {
                username = value;
                NotifyPropertyChanged();
            } 
        }
        public void NotifyPropertyChanged([CallerMemberName] string propertyName="")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private string status;
        public string Status
        {
            get
            {
                return status;
            }
            set
            {
                status = value;
                NotifyPropertyChanged();
            }
        }
        private DateTime lastlogin;
        public DateTime LastLogin {
            get
            {
                return lastlogin;
            }
            set
            {
                lastlogin = value;
                NotifyPropertyChanged();
            }
        }
        public User(string username, string status, DateTime lastlogin)
        {
            Username = username;
            Status = status;
            this.lastlogin = lastlogin;
        }
        public User()
        {

        }
        public class ResRoot
        {
            public Dictionary<string, User> Files { set; get; }
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}


using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Athena
{
    public class Athena : INotifyPropertyChanged
    {
        private String _currentTime;
        private String _currentDate;

        private String currentTime
        {
            get { return _currentTime; }
            set
            {
                _currentTime = DateTime.Now.ToString("h:mm:ss tt"); 
            }
        }

        private String currentDate
        {
            get { return _currentDate; }
            set { _currentDate = DateTime.Now.Date.ToShortDateString(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
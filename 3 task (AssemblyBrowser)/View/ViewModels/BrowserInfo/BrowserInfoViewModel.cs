using System.Collections.Generic;
using System.ComponentModel;
using Core.Browser;
using Microsoft.Win32;
using View.Annotations;
using View.Command;
using View.Models.BrowserInfo;

namespace View.ViewModels.BrowserInfo
{
    public class BrowserInfoViewModel : INotifyPropertyChanged
    {
        private List<NamespaceBrowserInfo> namespaces;

        public List<NamespaceBrowserInfo> Namespaces
        {
            get => namespaces;
            set
            {
                if (Equals(value, namespaces)) return;
                namespaces = value;
                OnPropertyChanged(nameof(Namespaces));
            }
        }

        private string selectedFile;

        public string SelectedFile
        {
            get => selectedFile;
            set
            {
                selectedFile = value;
                var browser = new AssemblyBrowser(value);
                var ns = new List<NamespaceBrowserInfo>();
                browser.GetNamespaces().ForEach(n =>
                {
                    var nn = new NamespaceBrowserInfo
                    {
                        Name = n
                    };
                    browser.GetTypes(n).ForEach(t =>
                    {
                        var tt = new TypeBrowserInfo
                        {
                            Name = t
                        };
                        browser.GetMethods(n, t).ForEach(m => { tt.Signatures.Add(m); });
                        nn.Types.Add(tt);
                    });
                    ns.Add(nn);
                });
                Namespaces = ns;
            }
        }

        private RelayCommand openCommand;

        public RelayCommand OpenCommand
        {
            get
            {
                return openCommand ??
                       (openCommand = new RelayCommand(obj =>
                       {
                           var d = new OpenFileDialog();
                           d.Multiselect = false;
                           d.Filter = "Assembly | *.dll";
                           if (d.ShowDialog() == true)
                           {
                               SelectedFile = d.FileName;
                           }
                       }));
            }
        }

        public BrowserInfoViewModel()
        {
            namespaces = new List<NamespaceBrowserInfo>();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
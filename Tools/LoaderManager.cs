using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Zadontsevaa_01.Tools;

namespace Zadontseva_01.Tools
{
    internal class LoaderManager
    {
        private static LoaderManager instance;

        private static object locker = new object();

        private ILoaderOwner loaderOwner;

        internal void Innitialize(ILoaderOwner loaderOwner)
        {
            this.loaderOwner = loaderOwner;
        }

        internal void ShowLoader()
        {
            loaderOwner.IsEnabled = false;
            loaderOwner.LoaderVisibility = Visibility.Visible;
        }

        internal void HideLoader()
        {
            loaderOwner.IsEnabled = true;
            loaderOwner.LoaderVisibility = Visibility.Hidden;
        }

        private LoaderManager()
        {
            
        }

        internal static LoaderManager Instance
        {
            get
            {
                lock (locker)
                {
                    if (instance == null)
                    {
                        instance = new LoaderManager();
                    }
                }

                return instance;
            }
        }
    }
}

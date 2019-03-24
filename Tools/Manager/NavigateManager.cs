namespace Zadontseva_01.Tools.Managers
{
    class NavigateManager
    {
        private static NavigateManager instance;

        private static object locker = new object();

        private INavigationModel _navigationModel;

        internal void Innitialize(INavigationModel navigationModel)
        {
            _navigationModel = navigationModel;
        }
        internal object Navigate(ViewType viewType)
        {
            return _navigationModel.Navigate(viewType);
        }
        private NavigateManager()
        {

        }

        internal static NavigateManager Instance
        {
            get
            {
                lock (locker)
                {
                    if (instance == null)
                    {
                        instance = new NavigateManager();
                    }
                }

                return instance;
            }
        }
    }
}


using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CampaignPlanner.Utility
{
    public static class ObservableCollectionExtensionMethods
    {
        public static void SafeClear<T>(this ObservableCollection<T> observableCollection)
        {
            if (!MainThread.IsMainThread)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    while (observableCollection.Any())
                    {
                        observableCollection.RemoveAt(0);
                    }
                });
            }
            else
            {
                while (observableCollection.Any())
                {
                    observableCollection.RemoveAt(0);
                }
            }
        }
    }
}

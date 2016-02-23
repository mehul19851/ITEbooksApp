using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Acr.UserDialogs;
using EbooksApp.ProgressReporting.Common;
using System.Threading.Tasks;

namespace EbooksApp.Droid
{
    [Activity(Label = "IT Ebooks", Icon = "@drawable/appicon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
    {
        ProgressBar _progressBar;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            UserDialogs.Init(() => (Activity)global::Xamarin.Forms.Forms.Context);
            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());
        }

        async void StartDownloadHandler(object sender, System.EventArgs e)
        {
            _progressBar.Progress = 0;
            Progress<DownloadBytesProgress> progressReporter = new Progress<DownloadBytesProgress>();
            progressReporter.ProgressChanged += (s, args) => _progressBar.Progress = (int)(100 * args.PercentComplete);

            Task<int> downloadTask = EbooksApp.ProgressReporting.Common.DownloadHelper.CreateDownloadTask(DownloadHelper.ImageToDownload, progressReporter);
            int bytesDownloaded = await downloadTask;
            System.Diagnostics.Debug.WriteLine("Downloaded {0} bytes.", bytesDownloaded);
        }
    }
}


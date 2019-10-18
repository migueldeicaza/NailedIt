using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Glass.App;
using Android.Glass.Timeline;
using Android.Provider;
using System.IO;

namespace NailedIt
{
	class MonitorFileWritten : FileObserver {
		Activity activity;
		Action<string> callback;
		string file;
		bool fileWritten;

		public MonitorFileWritten (Activity activity, string directory, string file, Action<string> callback) : base (directory, FileObserverEvents.CloseWrite)
		{
			this.file = file;
		}

		public override void OnEvent (FileObserverEvents e, string path)
		{
			if (fileWritten)
				return;
			if (path == file)
				fileWritten = true;
			activity.RunOnUiThread (() => {
				callback (path);
			});
		}
	}

	[Activity (Label = "NailedIt", MainLauncher = true)]
	[IntentFilter(new String[]{"com.google.android.glass.action.VOICE_TRIGGER"})]
	[MetaData("com.google.android.glass.VoiceTrigger", Resource = "@xml/voice_trigger_start")]
	public class MainActivity : Activity
	{
		int count = 1;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			StartActivityForResult (new Intent (MediaStore.ActionImageCapture), 100);


		}
			
		protected override void OnActivityResult (int requestCode, Result resultCode, Intent data)
		{
			if (requestCode == 100 && resultCode == Result.Ok) {
				var thumb = data.GetStringExtra (Android.Glass.Media.CameraManager.ExtraThumbnailFilePath);
				var full = data.GetStringExtra (Android.Glass.Media.CameraManager.ExtraPictureFilePath);

				var thumbUri = Android.Net.Uri.FromFile (new Java.IO.File (thumb));
				var card = new Card (this);
				card.SetImageLayout (Card.ImageLayout.Full);

				card.AddImage (thumbUri);
				card.SetText ("Nailed it!");
				SetContentView (card.ToView());
				var timeline = TimelineManager.From (this);
				timeline.Insert (card);

				// Save this to the gallery
				var mediaScanIntent = new Intent (Intent.ActionMediaScannerScanFile);
				mediaScanIntent.SetData (thumbUri);
				SendBroadcast (mediaScanIntent);

				new MonitorFileWritten (this, Path.GetDirectoryName (full), Path.GetFileName (full), (x) => {
					Console.WriteLine ("Full image has been loaded, and it is here {0}", x);
				});
				return;


				if (File.Exists (full))
					return;

				var container = Path.GetDirectoryName (full);
				//var observer = new FileObserver (container);
		
			}
		}
	}

	[Activity (Label = "CaptureMoment", MainLauncher = true)]
	[IntentFilter(new String[]{"com.google.android.glass.action.VOICE_TRIGGER"})]
	[MetaData("com.google.android.glass.VoiceTrigger", Resource = "@xml/capture")]
	public class CaptureMomentActivity : Activity
	{
		int count = 1;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			StartActivityForResult (new Intent (MediaStore.ActionImageCapture), 100);
		}
		protected override void OnActivityResult (int requestCode, Result resultCode, Intent data)
		{
			if (requestCode == 100 && resultCode == Result.Ok) {
				var thumb = data.GetStringExtra (Android.Glass.Media.CameraManager.ExtraThumbnailFilePath);
				var full = data.GetStringExtra (Android.Glass.Media.CameraManager.ExtraPictureFilePath);

				var thumbUri = Android.Net.Uri.FromFile (new Java.IO.File (thumb));
				var card = new Card (this);
				card.SetImageLayout (Card.ImageLayout.Full);

				card.AddImage (thumbUri);
				card.SetText ("Captured the moment!");
				SetContentView (card.ToView());
				var timeline = TimelineManager.From (this);
				timeline.Insert (card);

				// Save this to the gallery
				var mediaScanIntent = new Intent (Intent.ActionMediaScannerScanFile);
				mediaScanIntent.SetData (thumbUri);
				SendBroadcast (mediaScanIntent);

				new MonitorFileWritten (this, Path.GetDirectoryName (full), Path.GetFileName (full), (x) => {
					Console.WriteLine ("Full image has been loaded, and it is here {0}", x);
				});
				return;


				if (File.Exists (full))
					return;

				var container = Path.GetDirectoryName (full);
				//var observer = new FileObserver (container);

			}
		}

	}

	[Activity (Label = "ImmortalizeMoment", MainLauncher = true)]
	[IntentFilter(new String[]{"com.google.android.glass.action.VOICE_TRIGGER"})]
	[MetaData("com.google.android.glass.VoiceTrigger", Resource = "@xml/immortalize")]
	public class ImmortalizeMomentActivity : Activity
	{
		int count = 1;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			StartActivityForResult (new Intent (MediaStore.ActionImageCapture), 100);
		}

		protected override void OnActivityResult (int requestCode, Result resultCode, Intent data)
		{
			if (requestCode == 100 && resultCode == Result.Ok) {
				var thumb = data.GetStringExtra (Android.Glass.Media.CameraManager.ExtraThumbnailFilePath);
				var full = data.GetStringExtra (Android.Glass.Media.CameraManager.ExtraPictureFilePath);

				var thumbUri = Android.Net.Uri.FromFile (new Java.IO.File (thumb));
				var card = new Card (this);
				card.SetImageLayout (Card.ImageLayout.Full);

				card.AddImage (thumbUri);
				card.SetText ("Immortalized the moment!");
				SetContentView (card.ToView());
				var timeline = TimelineManager.From (this);
				timeline.Insert (card);

				// Save this to the gallery
				var mediaScanIntent = new Intent (Intent.ActionMediaScannerScanFile);
				mediaScanIntent.SetData (thumbUri);
				SendBroadcast (mediaScanIntent);

				new MonitorFileWritten (this, Path.GetDirectoryName (full), Path.GetFileName (full), (x) => {
					Console.WriteLine ("Full image has been loaded, and it is here {0}", x);
				});
				return;


				if (File.Exists (full))
					return;

				var container = Path.GetDirectoryName (full);
				//var observer = new FileObserver (container);

			}
		}

	}
}



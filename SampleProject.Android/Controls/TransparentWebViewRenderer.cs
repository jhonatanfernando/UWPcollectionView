using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SampleProject.Controls;
using SampleProject.Droid.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(TransparentWebView), typeof(TransparentWebViewRenderer))]
namespace SampleProject.Droid.Controls
{
    public class TransparentWebViewRenderer : WebViewRenderer
    {
        public TransparentWebViewRenderer(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<WebView> e)
        {
            base.OnElementChanged(e);

            UrlWebViewSource webview = Element.Source as UrlWebViewSource;
            if (Element != null && Element.Source != null && Control != null)
            {
                Control.SetBackgroundColor(Android.Graphics.Color.Transparent);
                try
                {
                    if (!webview.Url.Contains("file:///android_asset/"))
                        Control.LoadUrl("file:///android_asset/" + webview.Url);
                }
                catch { }
                Control.VerticalScrollBarEnabled = false;
                Control.HorizontalScrollBarEnabled = false;
            }
        }
    }
}
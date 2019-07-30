using SampleProject.Controls;
using SampleProject.UWP.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(TransparentWebView), typeof(TransparentWebViewRenderer))]
namespace SampleProject.UWP.Controls
{
    public class TransparentWebViewRenderer : WebViewRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<WebView> e)
        {
            base.OnElementChanged(e);

            if (Element != null && Element.Source != null && this.Control != null)
            {
                this.Control.DefaultBackgroundColor = Windows.UI.Colors.Transparent;
                UrlWebViewSource webview = Element.Source as UrlWebViewSource;
                this.Control.Source = new Uri("ms-appx-web:///" + webview.Url);
            }

        }
    }
}
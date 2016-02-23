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
using Xamarin.Forms.Platform.Android;
using EbooksApp.Droid.CustomRenderer;
using Xamarin.Forms;
using EbooksApp.CustomViews;
using System.ComponentModel;
using Android.Graphics;

[assembly: ExportRendererAttribute(typeof(RoundedBorderView), typeof(BorderRenderer))]
namespace EbooksApp.Droid.CustomRenderer
{
    public class BorderRenderer : VisualElementRenderer<RoundedBorderView>
    {

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            //HandlePropertyChanged (sender, e);
            BorderRendererVisual.UpdateBackground(Element, this.ViewGroup);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<RoundedBorderView> e)
        {
            base.OnElementChanged(e);
            BorderRendererVisual.UpdateBackground(Element, this.ViewGroup);
        }

        /*void HandlePropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Content")
            {
                BorderRendererVisual.UpdateBackground (Element, this.ViewGroup);
            }
        }*/

        protected override void DispatchDraw(Canvas canvas)
        {
            if (Element.IsClippedToBorder)
            {
                canvas.Save(SaveFlags.Clip);
                BorderRendererVisual.SetClipPath(this, canvas);
                base.DispatchDraw(canvas);
                canvas.Restore();
            }
            else
            {
                base.DispatchDraw(canvas);
            }
        }
    }
}
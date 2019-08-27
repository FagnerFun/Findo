using System;
using Android.Content;
using Android.Graphics;
using FindoApp.Control;
using FindoApp.Droid.Control;
using FindoApp.Model.enumerables;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Color = Xamarin.Forms.Color;

[assembly: ExportRenderer(typeof(GradientColorContent), typeof(GradientColorContentRender))]
namespace FindoApp.Droid.Control
{
    public class GradientColorContentRender : VisualElementRenderer<Frame>
    {
        private Color[] Colors { get; set; }

        private GradientColorStackMode Mode { get; set; }

        public GradientColorContentRender(Context context) : base(context)
        {
        }

        protected override void DispatchDraw(Canvas canvas)
        {
            Android.Graphics.LinearGradient gradient;

            int[] colors = new int[Colors.Length];

            for (int i = 0, l = Colors.Length; i < l; i++)
            {
                colors[i] = Colors[i].ToAndroid().ToArgb();
            }

            switch (Mode)
            {
                default:
                case GradientColorStackMode.ToRight:
                    gradient = new LinearGradient(0, 0, Width, 0, colors, null, Shader.TileMode.Mirror);
                    break;
                case GradientColorStackMode.ToLeft:
                    gradient = new LinearGradient(Width, 0, 0, 0, colors, null, Shader.TileMode.Mirror);
                    break;
                case GradientColorStackMode.ToTop:
                    gradient = new LinearGradient(0, Height, 0, 0, colors, null, Shader.TileMode.Mirror);
                    break;
                case GradientColorStackMode.ToBottom:
                    gradient = new LinearGradient(0, 0, 0, Height, colors, null, Shader.TileMode.Mirror);
                    break;
                case GradientColorStackMode.ToTopLeft:
                    gradient = new LinearGradient(Width, Height, 0, 0, colors, null, Shader.TileMode.Mirror);
                    break;
                case GradientColorStackMode.ToTopRight:
                    gradient = new LinearGradient(0, Height, Width, 0, colors, null, Shader.TileMode.Mirror);
                    break;
                case GradientColorStackMode.ToBottomLeft:
                    gradient = new LinearGradient(Width, 0, 0, Height, colors, null, Shader.TileMode.Mirror);
                    break;
                case GradientColorStackMode.ToBottomRight:
                    gradient = new LinearGradient(0, 0, Width, Height, colors, null, Shader.TileMode.Mirror);
                    break;
            }

            var paint = new Paint()
            {
                Dither = true,
            };

            paint.SetShader(gradient);
            canvas.DrawPaint(paint);

            base.DispatchDraw(canvas);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Frame> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null || Element == null)
                return;

            try
            {
                if (e.NewElement is GradientColorContent layout)
                {
                    Colors = layout.Colors;
                    Mode = layout.Mode;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(@"ERROR:", ex.Message);
            }
        }
    }
}
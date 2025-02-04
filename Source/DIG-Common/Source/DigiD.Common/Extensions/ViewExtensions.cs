using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DigiD.Common.Extensions
{
    public static class ViewExtensions
    {
        private const string ColorToAnimation = "ColorTo";

        public static Task<bool> ColorTo(this VisualElement self, Color fromColor, Color toColor, Action<Color> callback, uint length = 250, Easing easing = null)
        {
            Func<double, Color> transform = (t) =>
                Color.FromRgba(
                    fromColor.R + (t * (toColor.R - fromColor.R)),
                    fromColor.G + (t * (toColor.G - fromColor.G)),
                    fromColor.B + (t * (toColor.B - fromColor.B)),
                    fromColor.A + (t * (toColor.A - fromColor.A)));
            return ColorAnimation(self, ColorToAnimation, transform, callback, length, easing);
        }

        public static void CancelAnimation(this VisualElement self)
        {
            self.AbortAnimation(ColorToAnimation);
        }

        private static Task<bool> ColorAnimation(VisualElement element, string name, Func<double, Color> transform, Action<Color> callback, uint length, Easing easing)
        {
            easing = easing ?? Easing.Linear;
            var taskCompletionSource = new TaskCompletionSource<bool>();

            element.Animate(name, transform, callback, 16, length, easing, (v, c) => taskCompletionSource.SetResult(c));
            return taskCompletionSource.Task;
        }
    }
}

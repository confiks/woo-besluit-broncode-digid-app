using DigiD.Common.Constants;
using System.Linq;
using Xamarin.Forms;

namespace DigiD.Common.Effects
{
    public class A11YEffect : RoutingEffect
    {
        public static readonly BindableProperty ControlTypeProperty = BindableProperty.CreateAttached("ControlType", typeof(A11YControlTypes), typeof(A11YEffect), A11YControlTypes.None, propertyChanged: ControlTypePropertyChanged);

        private static void ControlTypePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (!(bindable is View view))
                return;

            var oldControlType = (A11YControlTypes)oldValue;
            var newControlType = (A11YControlTypes)newValue;

            if (oldControlType != newControlType)
            {
                if (newControlType != A11YControlTypes.None)
                {
                    view.Effects.Add(new A11YEffect());
                }
                else
                {
                    var toRemove = view.Effects.FirstOrDefault(e => e is A11YEffect && GetControlType(view) == oldControlType);
                    if (toRemove != null)
                        view.Effects.Remove(toRemove);
                }
            }
        }

        public static A11YControlTypes GetControlType(BindableObject view) => (A11YControlTypes)view?.GetValue(ControlTypeProperty);
        public static void SetControlType(BindableObject view, A11YControlTypes value) => view?.SetValue(ControlTypeProperty, value);

        public A11YEffect() : base($"{AppConfigConstants.A11YEffectGroupName}.{nameof(A11YEffect)}")
        {
        }
    }
}

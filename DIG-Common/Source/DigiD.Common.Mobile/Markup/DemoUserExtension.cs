using System;
using DigiD.Common.Mobile.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DigiD.Common.Mobile.Markup
{
    public abstract class DemoUserExtension<T> : IMarkupExtension
    {
        public T On { get; set; }
        public T Off { get; set; }

        public virtual object ProvideValue(IServiceProvider serviceProvider)
        {
            // Vraag het object op die referenties bevat naar de property en het object
            // waaraan deze markupextension is gekoppeld
            var provideValueTarget = serviceProvider.GetService<IProvideValueTarget>();

            var value = GetValue();
            // Het TargetObject is de View en de TargetProperty is de property waaraan deze markupextension is gekoppeld
            if (provideValueTarget.TargetObject is BindableObject targetObject &&
                provideValueTarget.TargetProperty is BindableProperty targetProperty)
            {

                // Als de systeem font grootte is aangepast
                // schrijven we de waarde behorende bij de nieuwe system fontsize naar de eigenschap
                // waaraan deze markupextension is gekoppeld 
                targetObject.SetValue(targetProperty, value);
            }

            return value;
        }

        protected T GetValue() => DemoHelper.CurrentUser != null ? On : Off;
    }

    public class ThicknessDemoUserExtension : DemoUserExtension<Thickness> { }
    public class BooleanDemoUserExtension : DemoUserExtension<bool> { }
}

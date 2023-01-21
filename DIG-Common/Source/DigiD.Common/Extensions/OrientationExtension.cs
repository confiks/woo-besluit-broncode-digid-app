﻿using System;
using DigiD.Common.Enums;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DigiD.Common.Markup
{
    public abstract class OrientationExtension<T> : IMarkupExtension
    {
        public T Landscape { get; set; }
        public T Portrait { get; set; }

        public virtual object ProvideValue(IServiceProvider serviceProvider)
        {
            // Vraag het object op die referenties bevat naar de property en het object
            // waaraan deze markupextension is gekoppeld
            var provideValueTarget = serviceProvider.GetService<IProvideValueTarget>();

            // Het TargetObject is de View en de TargetProperty is de property waaraan deze markupextension is gekoppeld
            if (provideValueTarget.TargetObject is BindableObject targetObject &&
                provideValueTarget.TargetProperty is BindableProperty targetProperty)
            {
               
                // Als de orientatie van het beeldscherm wijzigt
                DeviceDisplay.MainDisplayInfoChanged += (s, e) => {
                    var value = GetValue();

                    // schrijven we de waarde behorende bij de nieuwe orientatie naar de eigenschap
                    // waaraan deze markupextension is gekoppeld 
                    targetObject.SetValue(targetProperty, value);
                };
            }
            var value = GetValue();
            return value;
        }

        protected T GetValue() => DeviceDisplay.MainDisplayInfo.Orientation == DisplayOrientation.Landscape ?
            Landscape :
            Portrait;
    }

    public class StackOrientationExtension : OrientationExtension<StackOrientation> { }
    public class ThicknessOrientationExtension : OrientationExtension<Thickness> { }
    // Als de property's optioneel zijn, dan moeten alle variabelen nullable zijn.
    public class ChunkSizeOrientationExtension : OrientationExtension<int> { }
    public class ChunkOrientationOrientationExtension : OrientationExtension<ChunkOrientationEnum> { }
    public class DoubleOrientationExtension : OrientationExtension<double?> { }
    public class IntOrientationExtension : OrientationExtension<int> { }
    public class GridColumnOrientationExtension : OrientationExtension<int?> { }
    public class GridRowOrientationExtension : OrientationExtension<int?> { }
    public class ColorOrientationExtension : OrientationExtension<Color> { }
    public class BooleanOrientationExtension : OrientationExtension<bool> { }
    public class LayoutOptionsOrientationExtension : OrientationExtension<LayoutOptions> { }
}

#if A11YTEST
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DigiD.Common;
using DigiD.Common.BaseClasses;
using DigiD.Common.Enums;
using DigiD.Common.Interfaces;
using DigiD.Common.SessionModels;
using DigiD.Common.Settings;
using Xamarin.Forms;

namespace DigiD.Helpers
{
    public class A11YTestHelper
    {
        private readonly bool _auto;
        static readonly INavigationService NavigationService = DependencyService.Get<INavigationService>();
        private List<Type> _viewModelsWithParameters;
        private List<Type> _viewModelsNoParameters;
        private int _index;

        public A11YTestHelper(bool auto)
        {
            _auto = auto;
        }
        
        public async Task Start()
        {
            var firstPage = new ContentPage
            {
                Content = new Label
                {
                    TextColor = Color.Orange,
                    Text = "Alle viewmodels zijn nu getest",
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    HorizontalOptions = LayoutOptions.CenterAndExpand
                }
            };

            // Eerst het 1 en ander aanmaken om NullReferences te voorkomen.
            WhatsNewHelper.Init();
            Application.Current.MainPage = new CustomNavigationPage(firstPage);
            
            await App.GetActualConfiguration();
            
            DependencyService.Get<IPreferences>().LetterRequestDate = DateTimeOffset.Now.AddDays(-3);

            TestViewModels();

            await CheckPage();
        }

        public async Task Next()
        {
            _index++;

            if (_index <= _viewModelsNoParameters.Count-1)
            {
                var viewModel = (BaseViewModel)Activator.CreateInstance(_viewModelsNoParameters[_index]);
                await NavigationService.PushAsync(viewModel);    
            }
        }

        private async Task CheckPage()
        {
            // nodig voor RdaScanFailedViewModel.ctor
            HttpSession.ActivationSessionData = new ActivationSessionData
            {
                ActivationMethod = ActivationMethod.SMS
            };

            var viewModel = (BaseViewModel)Activator.CreateInstance(_viewModelsNoParameters[_index]);
            await NavigationService.PushAsync(viewModel);

            if (_auto)
            {
                await Task.Delay(5000);

                while (_index != _viewModelsNoParameters.Count)
                {
                    await Next();
                    await Task.Delay(5000);
                }
            }
        }

        private void TestViewModels()
        {
            _viewModelsNoParameters = new List<Type>();
            _viewModelsWithParameters = new List<Type>();

            foreach (var vm in NavigationService.RegisteredPages.Keys)
            {
                if (ViewModelHasParameterLessCtor(vm))
                    _viewModelsNoParameters.Add(vm);
                else
                    _viewModelsWithParameters.Add(vm);
            }
            var teller = 1;
            System.Diagnostics.Debug.WriteLine($"" +
                $"\n***************************************************" +
                $"\n* ViewModels with parameterless constructors    *" +
                $"\n***************************************************");
            foreach (var vm in _viewModelsNoParameters)
                System.Diagnostics.Debug.WriteLine($"{teller++:D2} - {vm.FullName}");

            teller = 1;
            System.Diagnostics.Debug.WriteLine($"" +
                $"\n***************************************************" +
                $"\n* ViewModels with parameter constructors    *" +
                $"\n***************************************************");
            foreach (var vm in _viewModelsWithParameters)
                System.Diagnostics.Debug.WriteLine($"{teller++:D2} - {vm.FullName}");

            _viewModelsNoParameters = _viewModelsNoParameters.OrderBy(x => x.FullName).ToList();
        }

        private static bool ViewModelHasParameterLessCtor(Type vm)
        {
            var constructors = vm.GetConstructors();
            foreach (var ctr in constructors)
            {
                if (ctr.GetParameters().Length == 0)
                    return true;
            }

            return false;
        }
    }
}
#endif

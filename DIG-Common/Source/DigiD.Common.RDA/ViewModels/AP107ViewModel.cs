﻿using System;
using System.Threading.Tasks;
using DigiD.Common.Enums;
using DigiD.Common.Http.Enums;
using DigiD.Common.Interfaces;
using DigiD.Common.Mobile.BaseClasses;
using DigiD.Common.Models;
using DigiD.Common.Services;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace DigiD.Common.RDA.ViewModels
{
    public class AP107ViewModel : BaseViewModel
    {
#if A11YTEST
        public AP107ViewModel() : this(new InitForeignDocumentRequestModel(), async (r) => { await Task.FromResult(false); }, async () => { await Task.CompletedTask; })
        {

        }
#endif
        public AP107ViewModel(InitForeignDocumentRequestModel model, Func<bool, Task> completeAction, Func<Task> retryAction)
        {
            PageId = "AP107";
            ButtonCommand = new AsyncCommand(async () =>
            {
                DependencyService.Get<IDialog>().ShowProgressDialog();
                var response = await DependencyService.Get<IRdaServices>().InitForeignDocument(model);
                DependencyService.Get<IDialog>().HideProgressDialog();

                if (response.ApiResult == ApiResult.Ok)
                    await NavigationService.PushAsync(new RdaViewModel(response, completeAction, "AP038", false, retryAction));
                else
                    await NavigationService.ShowMessagePage(MessagePageType.UnknownError);
            });
        }
    }
}

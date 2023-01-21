using System;
using System.Threading.Tasks;

namespace DigiD.Common.Models
{
    public class RegisterEmailModel
    {
        public bool ExistingEmailAddress { get; }

        public RegisterEmailModel(bool existingEmailAddress = false)
        {
            ExistingEmailAddress = existingEmailAddress;
        }

        public RegisterEmailModel(Enums.ActivationMethod type, bool upgradeRda)
        {
            ActivationMethod = type;
            UpgradeRda = upgradeRda;
        }

        public Enums.ActivationMethod ActivationMethod {get;}
        public bool UpgradeRda {get;}
        public Func<Task> NextAction {get;set;}
        public Func<Task> AbortAction {get;set;}
    }
}

using System.Diagnostics;
using Android.Content;
using Android.Nfc;

namespace DigiD.Droid.Helpers
{
    internal class NfcBroadCastReceiver : BroadcastReceiver
    {
        private readonly System.Action<Tag> _action;
        public NfcBroadCastReceiver(System.Action<Tag> tagDiscoveredAction)
        {
            _action = tagDiscoveredAction;
        }

        public override void OnReceive(Context context, Intent intent)
        {
            if (intent != null && NfcConstants.ACTION_NFC.Equals(intent.Action))
            {
                var tag = (Tag)intent.GetParcelableExtra(NfcAdapter.ExtraTag);
                if (IsTagSupported(tag))
                {
                    Debug.WriteLine("Supported tag discovered");
                    _action.Invoke(tag);
                }
                else
                    Debug.WriteLine("Unsupported tag discovered");
            }

            bool IsTagSupported(Tag tag)
            {
                if (tag == null) return false;
                for (var i = 0; i < tag.GetTechList().Length; i++)
                {
                    var sText = tag.GetTechList()[i];
                    if (sText.Equals(NfcConstants.SUPPORTED_NFC))
                    {
                        return true;
                    }
                }
                return false;
            }
        }
    }
}

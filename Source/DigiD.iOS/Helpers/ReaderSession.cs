using System;
using System.Threading.Tasks;
using CoreNFC;
using DigiD.Common.NFC.Enums;
using Foundation;

namespace DigiD.iOS.Helpers
{
    public class ReaderSession : NFCTagReaderSessionDelegate
    {
        private readonly Func<Task> _tagFoundAction;
        private readonly Func<NfcError, Task> _error;
        private readonly Action _resetAction;
        public INFCIso7816Tag Tag { get; private set; }


        public ReaderSession(Func<Task> tagFoundAction, Func<NfcError, Task> errorAction, Action resetAction)
        {
            _tagFoundAction = tagFoundAction;
            _error = errorAction;
            _resetAction = resetAction;
        }

        public void Reset()
        {
            Tag = null;
        }

        public override void DidDetectTags(CoreNFC.NFCTagReaderSession session, INFCTag[] tags)
        {
            var tag = tags[0].GetNFCIso7816Tag();
            Tag = tag;

            if (tag == null)
                return;

            session.ConnectTo(tag, async (e) =>
            {
                if (e == null)
                {
                    await _tagFoundAction.Invoke();
                }
            });
        }

        public override async void DidInvalidate(CoreNFC.NFCTagReaderSession session, NSError error)
        {
            if (Tag == null && error.Code == 200)
                await _error.Invoke(NfcError.Cancelled);

            if (Tag == null && error.Code == 201)
                await _error.Invoke(NfcError.Timeout);

            _resetAction.Invoke();
        }
    }
}

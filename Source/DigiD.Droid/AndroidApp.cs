using System;
using Android.App;
using Android.Runtime;

namespace DigiD.Droid
{
#if !DEBUG && !TESTCLOUD
    [Application(Debuggable=false)]
#else
    [Application(Debuggable=true)]
#endif
    internal class AndroidApp : Application
    {
		internal AndroidApp (IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }  
    }
}

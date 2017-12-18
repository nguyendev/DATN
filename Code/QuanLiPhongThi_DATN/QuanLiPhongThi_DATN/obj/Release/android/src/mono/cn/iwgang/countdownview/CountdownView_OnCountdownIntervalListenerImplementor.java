package mono.cn.iwgang.countdownview;


public class CountdownView_OnCountdownIntervalListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		cn.iwgang.countdownview.CountdownView.OnCountdownIntervalListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onInterval:(Lcn/iwgang/countdownview/CountdownView;J)V:GetOnInterval_Lcn_iwgang_countdownview_CountdownView_JHandler:CN.Iwgang.Countdownview.CountdownView/IOnCountdownIntervalListenerInvoker, CountdownTimer\n" +
			"";
		mono.android.Runtime.register ("CN.Iwgang.Countdownview.CountdownView+IOnCountdownIntervalListenerImplementor, CountdownTimer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", CountdownView_OnCountdownIntervalListenerImplementor.class, __md_methods);
	}


	public CountdownView_OnCountdownIntervalListenerImplementor ()
	{
		super ();
		if (getClass () == CountdownView_OnCountdownIntervalListenerImplementor.class)
			mono.android.TypeManager.Activate ("CN.Iwgang.Countdownview.CountdownView+IOnCountdownIntervalListenerImplementor, CountdownTimer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onInterval (cn.iwgang.countdownview.CountdownView p0, long p1)
	{
		n_onInterval (p0, p1);
	}

	private native void n_onInterval (cn.iwgang.countdownview.CountdownView p0, long p1);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}

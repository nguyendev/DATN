package mono.cn.iwgang.countdownview;


public class CountdownView_OnCountdownEndListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		cn.iwgang.countdownview.CountdownView.OnCountdownEndListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onEnd:(Lcn/iwgang/countdownview/CountdownView;)V:GetOnEnd_Lcn_iwgang_countdownview_CountdownView_Handler:CN.Iwgang.Countdownview.CountdownView/IOnCountdownEndListenerInvoker, CountdownTimer\n" +
			"";
		mono.android.Runtime.register ("CN.Iwgang.Countdownview.CountdownView+IOnCountdownEndListenerImplementor, CountdownTimer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", CountdownView_OnCountdownEndListenerImplementor.class, __md_methods);
	}


	public CountdownView_OnCountdownEndListenerImplementor ()
	{
		super ();
		if (getClass () == CountdownView_OnCountdownEndListenerImplementor.class)
			mono.android.TypeManager.Activate ("CN.Iwgang.Countdownview.CountdownView+IOnCountdownEndListenerImplementor, CountdownTimer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onEnd (cn.iwgang.countdownview.CountdownView p0)
	{
		n_onEnd (p0);
	}

	private native void n_onEnd (cn.iwgang.countdownview.CountdownView p0);

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

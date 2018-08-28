/************************************************************
UnityでFPSを設定する方法
	http://unityleaning.blog.fc2.com/blog-entry-2.html
	
	Unity menu
		"Edit"→"Project Settings"→"Quality"
			inspector:"VSync Count"の項目を変更する。
				Every VBlank（垂直同期） = monitor refresh rate
				Every Second VBlank（垂直同期の半分）
				Don't Sync（垂直同期無し）
				
			FPSを固定する場合は"Don't Sync"（垂直同期無し）を選ぶ。
		
		
		SJ note
			"Every VBlank"にすると、script上で設定する値の速い遅いに関わらず、単に反映されなかった(RefreshRate = 60Hzのmonitorでは、scriptによる 設定に関わらず常に60fps)。
			この点で、oFのofSetVerticalSync() とは動作が異なる。
			
			oFでは、
				ofSetVerticalSync(true);
			とすると、高fpsのみ制限がかかり、低fpsは、自由に設定可能.
			
			つまり、
				ofSetVerticalSync(true);
				ofSetFrameRate(xx)
			において、
				monitor refresh rateより速い値を設定してもmonitor refresh rateのそれとなる().
				
				monitor refresh rateより遅い値の場合は、ofSetFrameRate()の値が反映される。
				倍半分の制限もなし。
				
			例えば、
				monitor refresh rate = 60Hz
				ofSetFrameRate(50);
			などの場合、processは、50Hzで動作し、monitorのrefresh timingでprocessの内容が反映される。と言った具合のようだ。
************************************************************/

using UnityEngine;
using System.Collections;

public class SetFrameRate : MonoBehaviour {
	
	[SerializeField]	int FrameRate = 60;
	float DeltaTime;
	float LastMeas_FrameRate = 0;
	
	void Awake () {
		Application.targetFrameRate = FrameRate;
	}
	
	void Start () {
	}
	
	void Update () {
		DeltaTime = Time.deltaTime;
	}
	
	void OnGUI()
	{
		float alpha = 0.1f;
		float Meas_FrameRate = alpha * 1/DeltaTime + (1 - alpha) * LastMeas_FrameRate;
		
		string label = string.Format("{0:00.0}", Meas_FrameRate);

		GUI.color = Color.black;
		GUI.Label(new Rect(20, 15, 100, 30), label);
		
		
		LastMeas_FrameRate = Meas_FrameRate;
	}
}

﻿using UnityEngine;
using System.Collections;

//ステージ管理
public class StageManager : MonoBehaviour {
	public static int nowStageCount; //現在のステージ位置
	void Start () {
		nowStageCount = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public static void AddNextStageCount() {
		nowStageCount++;
	}
}
using UnityEngine;
using System.Collections;

//ステージ作成のための親クラス
public class StageSystem : MonoBehaviour {

	//typeの種類によってx軸の距離を変更
	protected static int X(int type) {
		int x = 1;
		switch (type) {
			case 0: x = 4; break;
			case 1: x = 2; break;
			case 2: x = 1; break;
		}

		return x;
	}

	//typeの種類によってy軸の距離を変更
	protected static float Y(int type) {
		float y = 1;
		switch (type) {
			case -2: y = -0.5f; break;
			case -1: y = -0.25f; break;
			case 0: y = 1; break;
			case 1: y = 0.5f; break;
			case 2: y = 0.25f; break;
		}

		return y;
	}

}

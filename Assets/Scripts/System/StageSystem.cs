using UnityEngine;
using System.Collections;

//ステージ作成のための親クラス
//子クラスではGetXとGetY関数を用意して、x軸とy軸を取得する
public class StageSystem : MonoBehaviour {

	//typeの種類によってx軸の距離を変更
	protected static int X(int type) {
		int x = 1;
		switch (type) {
			case 0: x = 0; break;
			case 1: x = 1; break; //interval : 0.25s
			case 2: x = 2; break; //interval : 0.5s
			case 3: x = 3; break; //interval : 0.75s
			case 4: x = 4; break; //interval : 1.0s
		}

		return x;
	}

	//typeの種類によってy軸の距離を変更
	protected static float Y(int type) {
		float y = 1;
		switch (type) {
			case -4: y = -1.0f; break;
			case -3: y = -0.75f; break;
			case -2: y = -0.5f; break;
			case -1: y = -0.25f; break;
			case 0: y = 0; break;
			case 1: y = 0.25f; break;
			case 2: y = 0.5f; break;
			case 3: y = 0.75f; break;
			case 4: y = 1.0f; break;
		}

		return y;
	}

}

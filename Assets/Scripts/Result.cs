using UnityEngine;
using System.Collections;
//結果の処理
public class Result : MonoBehaviour {

  //スコアによって結果が違う
	public static Sprite[] GetSprite(int score) {
		float parsent = (float)ScoreManager.GetScore() / MaxScore() * 100;
		Debug.Log(ScoreManager.GetScore() + "%" + MaxScore() +"=" +parsent);
		if (parsent > 40 && parsent <= 60)  return Resources.LoadAll<Sprite>("Sprite/turtle");
		else if (parsent > 60 && parsent <= 80) return Resources.LoadAll<Sprite>("Sprite/pengin");
		else if (parsent > 80)  return Resources.LoadAll<Sprite>("Sprite/tyira");
		return Resources.LoadAll<Sprite>("Sprite/hiyoko");
	}
	public static int MaxScore() {
		int n = CreateStage.GetMaxBard();
		if (n < 8) {
			return 10 * n * n + 90 * n;
		} else 
			return 300 * n - 1040;
	}
}

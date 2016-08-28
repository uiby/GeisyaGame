using UnityEngine;
using System.Collections;

//ランキング処理
public class Ranking : MonoBehaviour {

  //現在のランキングを取得
	public static int[] GetRanking() {
		int[] ranking = new int[3];
		ranking[0] = PlayerPrefs.GetInt("GoldScore", 0);
		ranking[1] = PlayerPrefs.GetInt("SliverScore", 0);
		ranking[2] = PlayerPrefs.GetInt("BronzeScore", 0);

		return ranking;
	}

  //ランキングを更新
	private static void Save(int[] newRanking) {
		PlayerPrefs.SetInt("GoldScore", newRanking[0]);
		PlayerPrefs.SetInt("SliverScore", newRanking[1]);
		PlayerPrefs.SetInt("BronzeScore", newRanking[2]);

		PlayerPrefs.Save();
	}

  //レコードが記録を塗り替えるか判断
	public static bool CanChangeRank(int[] nowRanking, int record) {
		for (int i = 0;i < nowRanking.Length; i++) {
			if (record >= nowRanking[i]) return true; //大きかったらtrue
		}

		return false;
	}

	public static void RenewalRank(int[] nowRanking, int record) {
		int[] ranking = new[]{nowRanking[0], nowRanking[1], nowRanking[2], record};
		bool isChange = true;
		while (isChange) {
			isChange = false;
		  for (int i = 0;i < ranking.Length - 1; i++) {
		  	if (ranking[i] < ranking[i + 1]) {
		  		int temp = ranking[i];
		  		ranking[i] = ranking[i + 1];
		  		ranking[i + 1] = temp;
		  		isChange = true;
		  	}
		  }
	  }

	  Debug.Log("0:"+ ranking[0] +"1:"+ ranking[1]+ "2:"+ ranking[2]+ "3:"+ ranking[3]);
	  Save(new int[]{ranking[0], ranking[1], ranking[2]});
	}

}

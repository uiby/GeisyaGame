using UnityEngine;
using System.Collections;

//ステージ管理
public class StageManager : MonoBehaviour {
	public static int nowStageCount; //現在のステージ位置
	private static GameObject timingGauge; //次のタイミングまでのゲージ
	void Start () {
		nowStageCount = 0;
		timingGauge = (GameObject)Resources.Load("UI/timingGauge");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//次のステージカウントを1足す
	public static void AddNextStageCount() {
		nowStageCount++;
	}

	//タイミングゲージの生成
	public static void CreateTimingGauge() {
		Vector2 pos = CreateStage.stages[PrevStageNumber()].nextPos;
		GameObject temp = (GameObject)Instantiate(timingGauge, pos, Quaternion.identity);
		temp.GetComponent<TimingGauge>().SetInterval(CreateStage.stages[PrevStageNumber()].interval);
	}

	//次のステージカウントを返す
	public static int NextStageNumber() {
		return nowStageCount + 1;
	}
	//前のステージカウントを返す
	public static int PrevStageNumber() {
		return nowStageCount - 1;
	}
	public static bool IsFirstStageNumber() {
		if (nowStageCount == 0)  return true;

		return false;
	}

	public static void Init() {
		nowStageCount = 0;
	}
}

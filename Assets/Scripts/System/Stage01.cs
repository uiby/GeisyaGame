using UnityEngine;
using System.Collections;

//ステージ01
public class Stage01 : StageSystem {
	private const int maxStageNum = 20;
	public static int GetMaxStageNum() {
		return maxStageNum;
	}

	public static int[] GetX() {
		int[] x = new int[]{X(0), X(0), X(0), X(0), X(0),
		  X(0), X(0), X(0), X(0), X(0),
		  X(0), X(0), X(0), X(0), X(0),
	    X(0), X(0), X(0), X(0), X(0)};
		return x;
	}

	public static float[] GetY() {
		float[] y = new float[]{Y(0), Y(0), Y(0), Y(0), Y(0),
		  Y(0), Y(0), Y(0), Y(0), Y(0),
		  Y(0), Y(0), Y(0), Y(0), Y(0),
	    Y(0), Y(0), Y(0), Y(0), Y(0)};
		return y;
	}

}

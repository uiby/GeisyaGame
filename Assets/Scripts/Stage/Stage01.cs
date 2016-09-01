using UnityEngine;
using System.Collections;

//ステージ01
public class Stage01 : StageSystem {
	private const int maxBard = 30;
	public static int GetMaxBard() {
		return maxBard;
	}

	public static int[] GetX() {
		int[] x = new int[]{X(4), X(2), X(2), X(2), X(2), X(2), X(2), X(4),
			                  X(2), X(2), X(2), X(2), X(2), X(2), X(4),
			                  X(4), X(4), X(4), X(4), 
			                  X(1), X(1), X(1), X(1), X(1), X(1), X(1), X(1), 
			                  X(2), X(2), X(4)};
		return x;
	}

	public static float[] GetY() {
		float[] y = new float[]{Y(4), Y(2), Y(2), Y(2), Y(2), Y(2), Y(2), Y(4),
			                      Y(2), Y(2), Y(2), Y(2), Y(2), Y(2), Y(4),
			                      Y(4), Y(4), Y(4), Y(4), 
			                      Y(1), Y(1), Y(1), Y(1), Y(1), Y(1), Y(1), Y(1), 
			                      Y(2), Y(2), Y(4)};
		return y;
	}

}

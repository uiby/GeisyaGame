using UnityEngine;
using System.Collections;

//ステージ01
public class Stage01 : StageSystem {
	private const int maxBard = 29;
	public static int GetMaxBard() {
		return maxBard;
	}

	public static int[] GetX() {
		int[] x = new int[]{X(1), X(1), X(1), X(1), X(1), X(1), X(0),
			                  X(1), X(1), X(1), X(1), X(1), X(1), X(0),
			                  X(0), X(0), X(0), X(0), 
			                  X(2), X(2), X(2), X(2), X(2), X(2), X(2), X(2), 
			                  X(1), X(1), X(0)};
		return x;
	}

	public static float[] GetY() {
		float[] y = new float[]{Y(1), Y(1), Y(1), Y(1), Y(1), Y(1), Y(0),
			                      Y(1), Y(1), Y(1), Y(1), Y(1), Y(1), Y(0),
			                      Y(0), Y(0), Y(0), Y(0), 
			                      Y(2), Y(2), Y(2), Y(2), Y(2), Y(2), Y(2), Y(2), 
			                      Y(1), Y(1), Y(0)};
		return y;
	}

}

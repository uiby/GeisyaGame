﻿using UnityEngine;
using System.Collections;

public class Stage03 : StageSystem {
	private const int maxBard = 64;
	public static int GetMaxBard() {
		return maxBard;
	}

	public static int[] GetX() {
		int[] x = new int[]{X(4), 
		                    X(2), X(2), X(3), X(3), X(1), X(1), X(4), X(2), X(2), X(3), X(3), X(1), X(1), X(4),
		                    X(2), X(2), X(2), X(1), X(3), X(1), X(1), X(4), X(2), X(2), X(2), X(1), X(3), X(1), X(1), X(4),
		                    X(1), X(1), X(1), X(1), X(2), X(2), X(2), X(2), X(2), X(2), X(2), X(2), X(4), X(2), X(1), X(1), X(4),
		                    X(1), X(1), X(2), X(2), X(2), X(2), X(2), X(2), X(2), X(2), X(3), X(1), X(2), X(1), X(2), X(4)};
		return x;
	}

	public static float[] GetY() {
		float[] y = new float[]{Y(4), 
			                      Y(2), Y(2), Y(3), Y(3), Y(1), Y(1), Y(4), Y(2), Y(2), Y(3), Y(3), Y(1), Y(1), Y(4),
			                      Y(2), Y(2), Y(2), Y(1), Y(3), Y(1), Y(1), Y(4), Y(2), Y(2), Y(2), Y(1), Y(3), Y(1), Y(1), Y(4),
		                        Y(1), Y(1), Y(1), Y(1), Y(2), Y(2), Y(2), Y(2), Y(2), Y(2), Y(2), Y(2), Y(4), Y(2), Y(1), Y(1), Y(4),
		                        Y(1), Y(1), Y(2), Y(2), Y(2), Y(2), Y(2), Y(2), Y(2), Y(2), Y(3), Y(1), Y(2), Y(1), Y(2), Y(4)};
		return y;
	}
}

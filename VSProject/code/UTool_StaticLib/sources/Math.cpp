/*
 *  // Made By: Santiago Arribas Maroto
 *  // 2018/2019
 *  // Contact: Santisabirra@gmail.com
 */

#include "Math.hpp"

	//Función matemática que permite conseguir la potencia de 2 mas cercana al valor indicado
	int UTool::Math::get_closest_pow2(int value)
	{
		int highest_distance = 0;
		int lowest_distance = 0;

		int lowest_pow2 = 0;
		for (int i = value; i >= 1; i--)
		{
			lowest_distance++;
			if ((i & (i - 1)) == 0)
			{
				lowest_pow2 = i;
				break;
			}
		}

		int highest_pow2 = 0;
		for (int i = value; i >= 1; i++)
		{
			highest_distance++;
			if ((i & (i - 1)) == 0)
			{
				highest_pow2 = i;
				break;
			}
		}

		return	(highest_distance > lowest_distance) ? lowest_pow2 : highest_pow2;
	}

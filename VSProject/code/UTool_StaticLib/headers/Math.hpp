/*
 *  // Made By: Santiago Arribas Maroto
 *  // 2018/2019
 *  // Contact: Santisabirra@gmail.com
 */

#ifndef MATH_HEADER
#define MATH_HEADER

#include "StaticSource.hpp"

namespace UTool
{
	class Math : public UTool_Static
	{

	public:

		//Función matemática que permite conseguir la potencia de 2 mas cercana al valor indicado
		static int get_closest_pow2(int value);
	};
}

#endif MATH_HEADER
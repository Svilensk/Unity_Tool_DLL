/*
 *  // Made By: Santiago Arribas Maroto
 *  // 2018/2019
 *  // Contact: Santisabirra@gmail.com
 */

#ifndef FILTER_HEADER
#define FILTER_HEADER

#include "StaticSource.hpp"
typedef unsigned char uint8_t;

namespace UTool
{
	class TextureFilter : public UTool_Static
	{
	public:

		//Aplicaci�n de filtro homog�neo blanco y negro
		static uint8_t* apply_bw_filter(int size, uint8_t* input_color_values);

		//Aplicaci�n de filtro homogeneo sepia
		static uint8_t* apply_sepia_filter(int size, uint8_t* input_color_values);
	};
}

#endif FILTER_HEADER

/*
 *  // Made By: Santiago Arribas Maroto
 *  // 2018/2019
 *  // Contact: Santisabirra@gmail.com
 */

#include "Filters.hpp"
typedef unsigned char uint8_t;

	 //Aplicación de filtro homogéneo blanco y negro
	uint8_t* UTool::TextureFilter::apply_bw_filter(int size, uint8_t* input_color_values)
	{
		uint8_t *output_color_values = new uint8_t[size];
		for (size_t i = 0; i < size; i += 4)
		{
			output_color_values[i + 0] = static_cast<char>((input_color_values[i + 0] + input_color_values[i + 1] + input_color_values[i + 2]) * 0.3333f);
			output_color_values[i + 1] = static_cast<char>((input_color_values[i + 0] + input_color_values[i + 1] + input_color_values[i + 2]) * 0.3333f);
			output_color_values[i + 2] = static_cast<char>((input_color_values[i + 0] + input_color_values[i + 1] + input_color_values[i + 2]) * 0.3333f);
			output_color_values[i + 3] = input_color_values[i + 3];
		}
		return output_color_values;
	}

	//Aplicación de filtro homogéneo sepia
	uint8_t* UTool::TextureFilter::apply_sepia_filter(int size, uint8_t* input_color_values)
	{
		uint8_t *output_color_values = new uint8_t[size];

		for (size_t i = 0; i < size; i += 4)
		{
			output_color_values[i + 0] = static_cast<char>(((input_color_values[i + 0] + input_color_values[i + 1] + input_color_values[i + 2]) * 0.3333f) * 1.0f);
			output_color_values[i + 1] = static_cast<char>(((input_color_values[i + 0] + input_color_values[i + 1] + input_color_values[i + 2]) * 0.3333f) * 0.6f);
			output_color_values[i + 2] = static_cast<char>(((input_color_values[i + 0] + input_color_values[i + 1] + input_color_values[i + 2]) * 0.3333f) * 0.3f);
			output_color_values[i + 3] = input_color_values[i + 3];
		}
		return output_color_values;
	}

/*
 *  // Made By: Santiago Arribas Maroto
 *  // 2018/2019
 *  // Contact: Santisabirra@gmail.com
 */

#include "Texture.hpp"

namespace UTool
{
	//Funcion que permite establecer el array de bytes guardados
	void Texture::set_byte_array(uint8_t * input_texture)
	{
		saved_texture_byte_array = new uint8_t[absolute_byte_size];

		for (size_t i = 0; i < absolute_byte_size; i++)
		{
			saved_texture_byte_array[i] = input_texture[i];
		}
	}

	//Función que permite establecer el tamaño de la textura
	void Texture::set_texture_size(int input_width, int input_height)
	{
		saved_texture_height = input_width;
		saved_texture_width = input_width;

		absolute_byte_size = input_width * input_height * 4;
	}

	//Función que permite cambiar los bytes de una textura por otra
	void UTool::Texture::reset_texture_to(uint8_t * target_texture)
	{
		for (size_t i = 0; i < absolute_byte_size; i++)
		{
			saved_texture_byte_array[i] = target_texture[i];
		}
	}
}
/*
 *  // Made By: Santiago Arribas Maroto
 *  // 2018/2019
 *  // Contact: Santisabirra@gmail.com
 */

#include "DLL_UnityTool.hpp"
#include "Texture.hpp"

#include "StaticSource.hpp"
#include "BrushPainting.hpp"
#include "Filters.hpp"
#include "Math.hpp"

namespace UTool
{
	typedef unsigned char uint8_t;

	//Dos texturas necesarias para los procesos
	Texture  editing_texture;
	Texture original_texture;

	extern "C"
	{	
		//Setters para las texturas
		UNITY_API bool set_texture_byte_array(uint8_t* textureByteArray)
		{
			editing_texture.set_byte_array(textureByteArray);
			return true;
		}

		UNITY_API bool set_default_texture_byte_array(uint8_t* textureByteArray, int absolute_size)
		{
			original_texture.set_byte_array(textureByteArray);
			return true;
		}

		UNITY_API bool set_texture_size(int texture_height, int texture_width)
		{
			//Ambas poseen el mismo tamaño
			editing_texture.set_texture_size(texture_height, texture_width);
			original_texture.set_texture_size(texture_height, texture_width);
			return true;
		}

		//Getters de valores de la textura
		UNITY_API uint8_t* get_texture_byte_array()
		{
			return editing_texture.saved_texture_byte_array;
		}

		UNITY_API uint8_t* get_default_texture_byte_array()
		{
			return original_texture.saved_texture_byte_array;
		}

		UNITY_API int get_texture_width()
		{
			return editing_texture.saved_texture_width;
		}

		UNITY_API int get_texture_height()
		{
			return editing_texture.saved_texture_height;
		}

		//Funcion que permite resetear la textura con otra textura
		UNITY_API void reset_texture()
		{
			editing_texture.reset_texture_to(original_texture.saved_texture_byte_array);
		}

		//Funciones de dibujado sobre la textura
		UNITY_API void paint_pixel_buffer(float mouse_x, float mouse_y, uint8_t* input_brush_values, int brush_size)
		{
			editing_texture.saved_texture_byte_array = BrushPainting::paint_pixel_buffer
			(
				editing_texture.saved_texture_width, 
				editing_texture.saved_texture_height, 
				editing_texture.saved_texture_byte_array,
				mouse_x, mouse_y, input_brush_values, brush_size
			);
		}

		UNITY_API void erase_pixel_buffer(float mouse_x, float mouse_y, int brush_size)
		{
			editing_texture.saved_texture_byte_array = BrushPainting::erase_pixel_buffer
			(
				editing_texture.saved_texture_width, 
				editing_texture.saved_texture_height, 
				original_texture.saved_texture_byte_array, 
				editing_texture.saved_texture_byte_array, 
				mouse_x, mouse_y, brush_size
			);
		}

		//Funciones de filtros para la textura
		UNITY_API void filter_bw()
		{
			editing_texture.saved_texture_byte_array = TextureFilter::apply_bw_filter
			(
				editing_texture.absolute_byte_size,
				editing_texture.saved_texture_byte_array
			);
		}

		UNITY_API void filter_sepia()
		{
			editing_texture.saved_texture_byte_array = TextureFilter::apply_sepia_filter
			(
				editing_texture.absolute_byte_size,
				editing_texture.saved_texture_byte_array
			);
		}

		//Funciones matemáticas
		UNITY_API int get_closest_pow2(int value)
		{
			return Math::get_closest_pow2(value);
		}
	}
}
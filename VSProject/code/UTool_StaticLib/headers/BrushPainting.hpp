/*
 *  // Made By: Santiago Arribas Maroto
 *  // 2018/2019
 *  // Contact: Santisabirra@gmail.com
 */

#ifndef BRUSH_PAINTING_HEADER
#define BRUSH_PAINTING_HEADER

#include "StaticSource.hpp"

namespace UTool
{
	class BrushPainting : public UTool_Static
	{

	public:
		//Establece el color de los píxeles al color introducido en el área y posición indicada
		static uint8_t* paint_pixel_buffer(int size_x, int size_y, uint8_t* input_color_values, float mouse_x, float mouse_y, uint8_t* input_brush_values, int brush_size);

		//Establece los píxeles en la localización y área hacia la textura original
		static uint8_t* erase_pixel_buffer(int size_x, int size_y, uint8_t* input_color_values, uint8_t* default_color_values, float mouse_x, float mouse_y, int brush_size);

		//Comprobación de si el píxel indicado está en el área del pincel/borrador
		static bool brush_size_area_comprobation(size_t byte_iterator, int brush_size, size_t mouse_absolute_coordinates, size_t absolute_byte_row_size);
	};
}

#endif // !BRUSH_PAINTING_HEADER


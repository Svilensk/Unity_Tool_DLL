/*
 *  // Made By: Santiago Arribas Maroto
 *  // 2018/2019
 *  // Contact: Santisabirra@gmail.com
 */

#include "BrushPainting.hpp"
typedef unsigned char uint8_t;

	//Establece el color de los píxeles al color introducido en el área y posición indicada
	uint8_t* UTool::BrushPainting::paint_pixel_buffer(int size_x, int size_y, uint8_t* input_color_values, float mouse_x, float mouse_y, uint8_t* input_brush_values, int brush_size)
	{
		//Valor absoluto en bytes de la textura
		int absolute_byte_size = (size_x * size_y) * 4;
		int absolute_byte_row_size = size_x * 4;

		int local_mouse_x_relative_pos = static_cast<int> (size_x * (mouse_x * 0.01f));
		int local_mouse_y_relative_pos = static_cast<int> (size_y * (mouse_y * 0.01f));

		//Cálculo del offset en la textura para la posición del cursor
		size_t mouse_local_coordinates = local_mouse_y_relative_pos * size_x + (local_mouse_x_relative_pos);
		size_t mouse_absolute_coordinates = mouse_local_coordinates * 4;

		uint8_t *output_color_values = new uint8_t[absolute_byte_size];

		for (size_t byte_iterator = 0; byte_iterator < absolute_byte_size; byte_iterator += 4)
		{
			if (brush_size_area_comprobation(byte_iterator, brush_size, mouse_absolute_coordinates, absolute_byte_row_size))
			{
				output_color_values[byte_iterator + 0] = input_brush_values[0];
				output_color_values[byte_iterator + 1] = input_brush_values[1];
				output_color_values[byte_iterator + 2] = input_brush_values[2];
				output_color_values[byte_iterator + 3] = input_brush_values[3];
			}

			else
			{
				output_color_values[byte_iterator + 0] = input_color_values[byte_iterator + 0];
				output_color_values[byte_iterator + 1] = input_color_values[byte_iterator + 1];
				output_color_values[byte_iterator + 2] = input_color_values[byte_iterator + 2];
				output_color_values[byte_iterator + 3] = input_color_values[byte_iterator + 3];
			}
		}

		return output_color_values;
	}

	//Establece los píxeles en la localización y área hacia la textura original
	uint8_t* UTool::BrushPainting::erase_pixel_buffer(int size_x, int size_y, uint8_t* default_color_values, uint8_t* input_color_values, float mouse_x, float mouse_y, int brush_size)
	{
		//Valor absoluto en bytes de la textura
		int absolute_byte_size = (size_x * size_y) * 4;
		int absolute_byte_row_size = size_x * 4;

		int local_mouse_x_relative_pos = static_cast<int> (size_x * (mouse_x * 0.01f));
		int local_mouse_y_relative_pos = static_cast<int> (size_y * (mouse_y * 0.01f));

		//Cálculo del offset en la textura para la posición del cursor
		size_t mouse_local_coordinates = local_mouse_y_relative_pos * size_x + (local_mouse_x_relative_pos);
		size_t mouse_absolute_coordinates = mouse_local_coordinates * 4;

		uint8_t *output_color_values = new uint8_t[absolute_byte_size];

		for (size_t byte_iterator = 0; byte_iterator < absolute_byte_size; byte_iterator += 4)
		{
			if (brush_size_area_comprobation(byte_iterator, brush_size, mouse_absolute_coordinates, absolute_byte_row_size))
			{
				output_color_values[byte_iterator + 0] = default_color_values[byte_iterator + 0];
				output_color_values[byte_iterator + 1] = default_color_values[byte_iterator + 1];
				output_color_values[byte_iterator + 2] = default_color_values[byte_iterator + 2];
				output_color_values[byte_iterator + 3] = default_color_values[byte_iterator + 3];
			}

			else
			{
				output_color_values[byte_iterator + 0] = input_color_values[byte_iterator + 0];
				output_color_values[byte_iterator + 1] = input_color_values[byte_iterator + 1];
				output_color_values[byte_iterator + 2] = input_color_values[byte_iterator + 2];
				output_color_values[byte_iterator + 3] = input_color_values[byte_iterator + 3];
			}
		}

		return output_color_values;
	}

	//Comprobación de si el píxel indicado está en el área del pincel/borrador
	bool UTool::BrushPainting::brush_size_area_comprobation(size_t byte_iterator, int brush_size, size_t mouse_absolute_coordinates, size_t absolute_byte_row_size)
	{
		//La siguiente estructura permite crear cualquier forma de pincel que se desee, modificando directamente estructura que tendrá el pincel
		switch (brush_size)
		{

		case 1:
			//3 X 3 Pixel SQUARE Brush
			if (byte_iterator == (mouse_absolute_coordinates + 4)
				|| byte_iterator == (mouse_absolute_coordinates - 4)

				|| byte_iterator == (mouse_absolute_coordinates + absolute_byte_row_size)
				|| byte_iterator == (mouse_absolute_coordinates - absolute_byte_row_size)

				|| byte_iterator == (mouse_absolute_coordinates + absolute_byte_row_size + 4)
				|| byte_iterator == (mouse_absolute_coordinates + absolute_byte_row_size - 4)
				|| byte_iterator == (mouse_absolute_coordinates - absolute_byte_row_size + 4)
				|| byte_iterator == (mouse_absolute_coordinates - absolute_byte_row_size - 4)
				|| byte_iterator == (mouse_absolute_coordinates)
				) return true;

			else return false;
			break;

		case 2:
			//5 X 5 Pixel SQUARE Brush
			if (byte_iterator == (mouse_absolute_coordinates + 4)
				|| byte_iterator == (mouse_absolute_coordinates - 4)
				|| byte_iterator == (mouse_absolute_coordinates + 8)
				|| byte_iterator == (mouse_absolute_coordinates - 8)

				|| byte_iterator == (mouse_absolute_coordinates + absolute_byte_row_size)
				|| byte_iterator == (mouse_absolute_coordinates - absolute_byte_row_size)
				|| byte_iterator == (mouse_absolute_coordinates + absolute_byte_row_size * 2)
				|| byte_iterator == (mouse_absolute_coordinates - absolute_byte_row_size * 2)

				|| byte_iterator == (mouse_absolute_coordinates + absolute_byte_row_size + 4)
				|| byte_iterator == (mouse_absolute_coordinates + absolute_byte_row_size - 4)
				|| byte_iterator == (mouse_absolute_coordinates - absolute_byte_row_size + 4)
				|| byte_iterator == (mouse_absolute_coordinates - absolute_byte_row_size - 4)

				|| byte_iterator == (mouse_absolute_coordinates + absolute_byte_row_size + 8)
				|| byte_iterator == (mouse_absolute_coordinates + absolute_byte_row_size - 8)
				|| byte_iterator == (mouse_absolute_coordinates - absolute_byte_row_size + 8)
				|| byte_iterator == (mouse_absolute_coordinates - absolute_byte_row_size - 8)

				|| byte_iterator == (mouse_absolute_coordinates + absolute_byte_row_size * 2 + 4)
				|| byte_iterator == (mouse_absolute_coordinates + absolute_byte_row_size * 2 - 4)
				|| byte_iterator == (mouse_absolute_coordinates - absolute_byte_row_size * 2 + 4)
				|| byte_iterator == (mouse_absolute_coordinates - absolute_byte_row_size * 2 - 4)

				|| byte_iterator == (mouse_absolute_coordinates + absolute_byte_row_size * 2 + 8)
				|| byte_iterator == (mouse_absolute_coordinates + absolute_byte_row_size * 2 - 8)
				|| byte_iterator == (mouse_absolute_coordinates - absolute_byte_row_size * 2 + 8)
				|| byte_iterator == (mouse_absolute_coordinates - absolute_byte_row_size * 2 - 8)
				|| byte_iterator == (mouse_absolute_coordinates)
				) return true;

			else return false;
			break;
		case 3:
			//7 X 7 Pixel ROUND Brush
			if (byte_iterator == (mouse_absolute_coordinates + 4)
				|| byte_iterator == (mouse_absolute_coordinates - 4)
				|| byte_iterator == (mouse_absolute_coordinates + 8)
				|| byte_iterator == (mouse_absolute_coordinates - 8)
				|| byte_iterator == (mouse_absolute_coordinates + 12)
				|| byte_iterator == (mouse_absolute_coordinates - 12)

				|| byte_iterator == (mouse_absolute_coordinates + absolute_byte_row_size)
				|| byte_iterator == (mouse_absolute_coordinates - absolute_byte_row_size)
				|| byte_iterator == (mouse_absolute_coordinates + absolute_byte_row_size * 2)
				|| byte_iterator == (mouse_absolute_coordinates - absolute_byte_row_size * 2)
				|| byte_iterator == (mouse_absolute_coordinates + absolute_byte_row_size * 3)
				|| byte_iterator == (mouse_absolute_coordinates - absolute_byte_row_size * 3)

				|| byte_iterator == (mouse_absolute_coordinates + absolute_byte_row_size + 4)
				|| byte_iterator == (mouse_absolute_coordinates + absolute_byte_row_size - 4)
				|| byte_iterator == (mouse_absolute_coordinates - absolute_byte_row_size + 4)
				|| byte_iterator == (mouse_absolute_coordinates - absolute_byte_row_size - 4)

				|| byte_iterator == (mouse_absolute_coordinates + absolute_byte_row_size + 8)
				|| byte_iterator == (mouse_absolute_coordinates + absolute_byte_row_size - 8)
				|| byte_iterator == (mouse_absolute_coordinates - absolute_byte_row_size + 8)
				|| byte_iterator == (mouse_absolute_coordinates - absolute_byte_row_size - 8)

				|| byte_iterator == (mouse_absolute_coordinates + absolute_byte_row_size + 12)
				|| byte_iterator == (mouse_absolute_coordinates + absolute_byte_row_size - 12)
				|| byte_iterator == (mouse_absolute_coordinates - absolute_byte_row_size + 12)
				|| byte_iterator == (mouse_absolute_coordinates - absolute_byte_row_size - 12)

				|| byte_iterator == (mouse_absolute_coordinates + absolute_byte_row_size * 2 + 4)
				|| byte_iterator == (mouse_absolute_coordinates + absolute_byte_row_size * 2 - 4)
				|| byte_iterator == (mouse_absolute_coordinates - absolute_byte_row_size * 2 + 4)
				|| byte_iterator == (mouse_absolute_coordinates - absolute_byte_row_size * 2 - 4)

				|| byte_iterator == (mouse_absolute_coordinates + absolute_byte_row_size * 2 + 8)
				|| byte_iterator == (mouse_absolute_coordinates + absolute_byte_row_size * 2 - 8)
				|| byte_iterator == (mouse_absolute_coordinates - absolute_byte_row_size * 2 + 8)
				|| byte_iterator == (mouse_absolute_coordinates - absolute_byte_row_size * 2 - 8)

				|| byte_iterator == (mouse_absolute_coordinates)
				) return true;

			else return false;
			break;

		default:
			return false;
			break;
		}
	}

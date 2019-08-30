/*
 *  // Made By: Santiago Arribas Maroto
 *  // 2018/2019
 *  // Contact: Santisabirra@gmail.com
 */

#ifndef TEXTURE_DLL_HEADER
#define TEXTURE_DLL_HEADER

namespace UTool
{
	class Texture
	{
		typedef unsigned char uint8_t;

	public:

		//array de bytes con los valores por byte de la textura
		uint8_t* saved_texture_byte_array;

		//Parámetros de la textura 
		int saved_texture_height;
		int saved_texture_width;
		int absolute_byte_size;

		//Funciones de edición de la textura
		void set_byte_array(uint8_t* input_texture);
		void set_texture_size(int input_width, int input_height);
		void reset_texture_to(uint8_t* target_texture);
	};
}
#endif TEXTURE_DLL_HEADER
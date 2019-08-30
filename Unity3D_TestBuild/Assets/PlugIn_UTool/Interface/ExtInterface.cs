/*
 *  // Made By: Santiago Arribas Maroto
 *  // 2018/2019
 *  // Contact: Santisabirra@gmail.com
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Runtime.InteropServices;

//Esta clase sirve como wrapper para las funciones externas de la DLL, para que el acceso a las mismas sea mas simple y no haya que tener en cuenta
//los detalles de las funciones externas a la hora de usarlas durante la ejecución de la herramienta
public class ExtInterface
{

    // ----- GETTERS Y SETTERS PARA PARÁMETROS DE TEXTURAS EN LA DLL ------

    [DllImport("UnityTool_DLL", CallingConvention = CallingConvention.Cdecl)]
    private static extern bool set_texture_size(int texture_height, int texture_width);

    [DllImport("UnityTool_DLL", CallingConvention = CallingConvention.Cdecl)]
    private static extern int get_texture_height();

    [DllImport("UnityTool_DLL", CallingConvention = CallingConvention.Cdecl)]
    private static extern int get_texture_width();

    [DllImport("UnityTool_DLL", CallingConvention = CallingConvention.Cdecl)]
    private static extern bool set_texture_byte_array(byte[] textureByteArray);

    [DllImport("UnityTool_DLL", CallingConvention = CallingConvention.Cdecl)]
    private static extern IntPtr get_texture_byte_array();

    [DllImport("UnityTool_DLL", CallingConvention = CallingConvention.Cdecl)]
    private static extern bool set_default_texture_byte_array(byte[] textureByteArray);

    [DllImport("UnityTool_DLL", CallingConvention = CallingConvention.Cdecl)]
    private static extern IntPtr get_default_texture_byte_array();

    [DllImport("UnityTool_DLL", CallingConvention = CallingConvention.Cdecl)]
    private static extern void reset_texture();

    //Encapsulación de la función de la DLL que permite: OBTENER EL ALTO DE LA TEXTURA
    public static int uToolGetTextureHeight()
    {
        return get_texture_height();
    }

    //Encapsulación de la función de la DLL que permite: OBTENER EL ANCHO DE LA TEXTURA
    public static int uToolGetTextureWidth()
    {
        return get_texture_width();
    }

    //Encapsulación de la función de la DLL que permite: OBTENER LOS BYTES QUE FORMAN LA TEXTURA
    public static IntPtr uToolGetTextureByteArray()
    {
        return get_texture_byte_array();
    }

    //Encapsulación de la función de la DLL que permite: OBTENER LOS BYTES QUE FORMAN LA TEXTURA ORIGINAL
    public static IntPtr uToolGetDefaultTextureByteArray()
    {
        return get_default_texture_byte_array();
    }

    //Encapsulación de la función de la DLL que permite: ESTABLECER EL TAMAÑO DE LA TEXTURA
    public static bool uToolSetTextureSize(int texture_height, int texture_width)
    {
        return set_texture_size(texture_height, texture_width);
    }

    //Encapsulación de la función de la DLL que permite: ESTABLECER LOS BYTES DE LA TEXTURA ORIGINAL
    public static bool uToolSetDefaultTextureByteArray(byte[] textureByteArray)
    {
        return set_default_texture_byte_array(textureByteArray);
    }

    //Encapsulación de la función de la DLL que permite: ESTABLECER LOS BYTES QUE FORMAN LA TEXTURA
    public static bool uToolSetTextureByteArray(byte[] textureByteArray)
    {
        return set_texture_byte_array(textureByteArray);
    }

    //Encapsulación de la función de la DLL que permite: CAMBIAR LOS BYTES DE UNA TEXTURA POR LOS DE OTRA
    public static void resetTextureToDefault()
    {
        reset_texture();
    }


    // ----- EDICIÓN DE TEXTURAS, FILTROS, PINCEL, BORRADOR, MATH ------//

    [DllImport("UnityTool_DLL", CallingConvention = CallingConvention.Cdecl)]
    private static extern void paint_pixel_buffer(float mouseX, float mouseY, Color32[] brushColor, int brushSize);

    [DllImport("UnityTool_DLL", CallingConvention = CallingConvention.Cdecl)]
    private static extern void erase_pixel_buffer(float mouseX, float mouseY, int brushSize);

    [DllImport("UnityTool_DLL", CallingConvention = CallingConvention.Cdecl)]
    private static extern void filter_sepia();

    [DllImport("UnityTool_DLL", CallingConvention = CallingConvention.Cdecl)]
    private static extern void filter_bw();

    [DllImport("UnityTool_DLL", CallingConvention = CallingConvention.Cdecl)]
    private static extern int  get_closest_pow2(int value);

    //Encapsulación de la función de la DLL que permite: DIBUJADO A MANO SOBRE LA TEXTURA
    public static void uToolPaintPixelBuffer(float mouseX, float mouseY, Color32[] brushColor, int brushSize)
    {
        paint_pixel_buffer(mouseX, mouseY, brushColor, brushSize);
    }

    //Encapsulación de la función de la DLL que permite: BORRADO A MANO SOBRE LA TEXTURA
    public static void uToolErasePixelBuffer(float mouseX, float mouseY, int brushSize)
    {
        erase_pixel_buffer(mouseX, mouseY, brushSize);
    }

    //Encapsulación de la función de la DLL que permite: APLICACIÓN DE FILTRO BLANCO Y NEGRO
    public static void uToolSetBwFilter()
    {
        filter_bw();
    }

    //Encapsulación de la función de la DLL que permite: APLICACIÓN DE FILTRO SEPIA
    public static void uToolSetSepiaFilter()
    {
        filter_sepia();
    }

    //Encapsulación de la función de la DLL que permite: CÁLCULO DE LA POTENCIA DE 2 MAS CERCANA AL VALOR INTRODUCIDO
    public static int uToolGetClosestPow2(int value)
    {
        return get_closest_pow2(value);
    }
}

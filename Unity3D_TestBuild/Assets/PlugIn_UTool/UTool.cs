/*
 *  // Made By: Santiago Arribas Maroto
 *  // 2018/2019
 *  // Contact: Santisabirra@gmail.com
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public static class UTool
{
    //Enumeración con los estados del pincel,
    //Paint permite el dibujado en la textura y Erase permite borrar sobre los cambios a la textura
    public enum BrushState
    {
        paint = 0,
        erase = 1,
    };

    //Enumeracion con los estados de filtro
    public enum FilterType
    {
        blackWhite = 0,
        sepia      = 1,
    };

    //Existen 3 texturas, textura previa seleccionada(si hubiese), textura original seleccionada, textura siendo editada
    public static Texture2D oldTexture;
    public static Texture2D texturePreselect;
    public static Texture2D textureBeingEdited;

    //Si el usuario modifica el nombre de la textura, vamos guardando esos cambios
    public static string textureNewName;
    public static int    textureNewWidth;
    public static int    textureNewHeight;

    //Valores que se guardarán en la textura final al guardar
    public static string textureSavedName;

    //Nombre de la textura original
    public static string oldTextLabel = "None Selected";

    //Booleano que no permite el acceso al editor de texturas si no se cumplen
    //los requisitos
    public static bool sucessfulParametrization = false;

    //Parámetros del pincel, color y tamaño del mismo
    public static Color32 brushColor = new Color32(0, 0, 0, 255);
    public static int     brushSize;

    //Valores que poseen las coordenadas del ratón sobre la textura
    //En la ventana del editor
    public static float   texCoordsY;
    public static float   texCoordsX;
    public static Vector2 screenPos;

    //Función que permite guardar la textura seleccionada en la ventana de la interfaz de forma local
    public static void LoadPreselectedTexture(Texture2D editorSelectedTexture)
    {
        texturePreselect = editorSelectedTexture;
    }

    //Una vez se comprueba que los parámetros son válidos, se cargan en la DLL
    public static void SaveParameters()
    {
       //Ajustamos los parámetros de la textura a editar en la DLL (Se establece Original y Edited)
        ExtInterface.uToolSetTextureSize(textureNewHeight, textureNewWidth);
        textureSavedName = textureNewName;

        //Reescalamos la textura al tamaño deseado del usuario
        TextureScale.Point(texturePreselect, ExtInterface.uToolGetTextureWidth(), ExtInterface.uToolGetTextureHeight());  

        //Cargamos el la DLL la información de la textura por defecto
        ExtInterface.uToolSetDefaultTextureByteArray(texturePreselect.GetRawTextureData());

        //Creamos la nueva textura que se mostrará en el editor
        InstatiateTextureToEdit(ExtInterface.uToolGetTextureWidth(), ExtInterface.uToolGetTextureHeight());
    }

    public static void InstatiateTextureToEdit(int textureSavedWidth, int textureSavedHeight)
    {
        //Modificamos la textura a editar para mostrarla en la ventana del editor
        textureBeingEdited = new Texture2D(textureSavedWidth, textureSavedHeight, TextureFormat.RGBA32, false, false);

        //Descargamos los datos de la DLL con la información de la textura original en la copia que se modificará
        textureBeingEdited.LoadRawTextureData(ExtInterface.uToolGetDefaultTextureByteArray(), textureBeingEdited.GetRawTextureData().Length);
        textureBeingEdited.Apply();

        //Cargamos en la DLL los datos de la nueva textura
        ExtInterface.uToolSetTextureByteArray(textureBeingEdited.GetRawTextureData());
    }

    //Función que actualiza el buffer de pixeles unicamente en el área del pincel, bien para borrar o bien para pintar con el nuevo color
    public static void UpdateTexture(BrushState brushState)
    {
        switch (brushState)
        {
            //Caso 1: Pintar sobre la textura, modificamos la textura
            case (BrushState.paint):


                //Convertimos el color a array unidimensional para pasar el valor como byte[]
                Color32[] inputBrushColor = new Color32[1];
                inputBrushColor[0] = brushColor;

                ExtInterface.uToolPaintPixelBuffer(texCoordsX, texCoordsY, inputBrushColor, brushSize);
                break;

            //Caso 2: Borrar lo pintado sobre la textura, modificamos la textura
            case (BrushState.erase):
                ExtInterface.uToolErasePixelBuffer(texCoordsX, texCoordsY, brushSize);
                break;
        }

        //Cargamos los cambios de la DLL en la textura del editor
        textureBeingEdited.LoadRawTextureData(ExtInterface.uToolGetTextureByteArray(), textureBeingEdited.GetRawTextureData().Length);
        textureBeingEdited.Apply();
    }

    //Función que permite modificar toda la textura aplicando un filtro a la imágen
    public static void ApplyFilter(FilterType filterType)
    {
        switch (filterType)
        {
            //Caso 1: Aplicamos el filtro Sepia
            case (FilterType.sepia):
                ExtInterface.uToolSetSepiaFilter();
                break;

            //Caso 2: Aplicamos el filtro Blanco/Negro
            case (FilterType.blackWhite):
                ExtInterface.uToolSetBwFilter();
                break;
        }

        //Cargamos los cambios realizados en la DLL a la textura de la UI
        textureBeingEdited.LoadRawTextureData(ExtInterface.uToolGetTextureByteArray(), textureBeingEdited.GetRawTextureData().Length);
        textureBeingEdited.Apply();
    }

    //Función de reseteo de la imágen, vuelve completamente a su estado original
    public static void ResetChanges()
    {
        ExtInterface.resetTextureToDefault();
        textureBeingEdited.LoadRawTextureData(ExtInterface.uToolGetTextureByteArray(), textureBeingEdited.GetRawTextureData().Length);
        textureBeingEdited.Apply();
    }

    public static void SaveTextureToFile()
    {
        System.IO.File.WriteAllBytes(Application.dataPath + "/../Assets/PlugIn_UTool/Created Textures/" + UTool.textureNewName + ".png", textureBeingEdited.EncodeToPNG());
        AssetDatabase.Refresh();
    }

    //Operación matemática realizada en la DLL, soy consciente de que se puede usar (Mathf.ClosestPowerOfTwo())
    //Pero igualmente quería poder demostrar que se pueden añadir tantas operaciones a la DLL como se desee
    public static int GetClosestPow2(int inputValue)
    {
        return ExtInterface.uToolGetClosestPow2(inputValue);
    }
}

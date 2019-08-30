/*
 *  // Made By: Santiago Arribas Maroto
 *  // 2018/2019
 *  // Contact: Santisabirra@gmail.com
 */

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Text.RegularExpressions;

public class UIutool : EditorWindow
{
    //Toggles en el editor, para forzar que los parámetros de alto y ancho
    //sean fuerza de 2 y para forzar que el alto y el ancho sean igual
    public bool forcePow2    = false;
    public bool constraintWh = true;

    [MenuItem("Window/UTool")]
    public static void ShowWindow()
    {
        GetWindow<UIutool>("UTool");
        UIutool window = (UIutool) EditorWindow.GetWindow(typeof(UIutool));

        //Limite del tamaño para que se mantenga la estructura
        window.maxSize = new Vector2(500,950);
        window.minSize = new Vector2(500,950);
        window.Show();
    }

    void OnGUI()
    {
        //Obtenemos la textura del editor
        GUILayout.Label("Select a base texture", EditorStyles.boldLabel);
        UTool.LoadPreselectedTexture((Texture2D)EditorGUILayout.ObjectField(UTool.oldTextLabel, UTool.texturePreselect, typeof(Texture2D), false));

        //Si se ha seleccionado textura, comprobamos su validez
        if (UTool.texturePreselect != null)
        {
            //Si la textura no puede leerse, se indica al usuario que modifique esa propiedad
            if (UTool.texturePreselect.isReadable == false)
            {
                Debug.LogError("UTOOL ERROR: Texture is not readable! (mark readable property in the importer settings)");
                UTool.sucessfulParametrization = false;
                UTool.texturePreselect         = null;
            }

            //Comprobamos si la textura seleccionada es la misma que se había seleccionado previamente
            //si no es así, actualizamos los valores de los parámetros de la textura
            else if (UTool.oldTexture != UTool.texturePreselect)
            {
                //UTool.textureNewName   = UTool.oldTexture.name;
                UTool.textureNewHeight = UTool.texturePreselect.height;
                UTool.textureNewWidth  = UTool.texturePreselect.width;
                UTool.textureNewName   = UTool.texturePreselect.name;

                UTool.oldTexture = UTool.texturePreselect;
                UTool.sucessfulParametrization = false;
            }
        }

        //Si no se ha seleccionado textura, no se permite el acceso al editor y se revierten los posibles cambios
        if (UTool.texturePreselect == null)
        {
            UTool.oldTextLabel     = "None Selected";
            UTool.textureNewHeight = 0;
            UTool.textureNewWidth  = 0;

            UTool.textureNewName = null;
            UTool.oldTexture     = null;

            UTool.sucessfulParametrization = false;
        } 

        //Se permite al usuario cambiar el nombre de la textura
        EditorGUILayout.Separator();
        GUILayout.Label("Setup the new parameters", EditorStyles.boldLabel);
        UTool.textureNewName = EditorGUILayout.TextField("Rename Texture", UTool.textureNewName);

        //ConstraintWH hace que el valor de alto y ancho de la textura sean el mismo
        EditorGUILayout.Separator();
        constraintWh = EditorGUILayout.Toggle("Constraint W/H Values", constraintWh);

        if (constraintWh)
        {
            UTool.textureNewWidth = UTool.textureNewHeight;
            UTool.textureNewWidth = UTool.textureNewHeight = EditorGUILayout.IntField("New Texture Height", UTool.textureNewHeight);
            UTool.textureNewWidth = UTool.textureNewHeight = EditorGUILayout.IntField("New Texture Width", UTool.textureNewWidth);
        }
        else
        {
            UTool.textureNewHeight = EditorGUILayout.IntField("New Texture Height", UTool.textureNewHeight);
            UTool.textureNewWidth  = EditorGUILayout.IntField("New Texture Width", UTool.textureNewWidth);
        }

        //No se permiten texturas mayores a 512 por problemas de rendimiento
        if (UTool.textureNewHeight > 512 || UTool.textureNewWidth > 512)
        {
            Debug.LogWarning("UTOOL WARNING: A texture bigger than 512x512 Is not currently Supported, Performance might be diminished");
            UTool.textureNewHeight = UTool.texturePreselect.height;
            UTool.textureNewWidth  = UTool.texturePreselect.width;
        }

        //Si aplicamos este toggle, se fuerza que los valores introducidos por el usuario sean potencia de 2
        forcePow2 = EditorGUILayout.Toggle("Power of 2 (Force)", forcePow2);
        if (forcePow2)
        {
            UTool.textureNewHeight = UTool.GetClosestPow2(UTool.textureNewHeight);
            UTool.textureNewWidth  = UTool.GetClosestPow2(UTool.textureNewWidth);
        }

        EditorGUILayout.Separator();

        //Botón que analizará la validez de los parametros introducidos, y en caso de ser válidos,
        //Permitirá el acceso al editor de texturas
        if (GUILayout.Button("Save Parameters & Load Editor"))
        {
            //Marcamos a true el valor de acceso, por si previamente se marcó a false si el usuario intentó
            //acceder al editor de forma errónea
            UTool.sucessfulParametrization = true;

            //Si no se ha seleccionado textura, no se permite el acceso
            if (UTool.texturePreselect == null)
            {
                Debug.LogError("UTOOL ERROR: No Texture Selected!");
                UTool.sucessfulParametrization = false;
            }

            //Si la textura tiene valores no válidos, no se permite el acceso
            if (UTool.textureNewHeight <= 0 || UTool.textureNewWidth <= 0)
            {
                Debug.LogError("UTOOL ERROR: Invalid Texure Size!");
                UTool.sucessfulParametrization = false;
            }

            //Si la textura no es cuadrada, no se permite el acceso
            if (UTool.textureNewHeight - UTool.textureNewWidth != 0)
            {
                Debug.LogError("UTOOL ERROR: Textures must be Squared!");
                UTool.sucessfulParametrization = false;
            }

            //Si el nombre no es válido, no se permite el acceso
            Regex regex = new Regex("^[A-Za-z0-9][A-Za-z0-9_-]*$");
            if (!string.IsNullOrEmpty(UTool.textureNewName) && !regex.Match(UTool.textureNewName).Success)
            {
                Debug.LogError("UTOOL ERROR: Invalid Name!");
                UTool.sucessfulParametrization = false;
            }

            //En caso de no haber encontrado ningun error de parametrización, se ajustan
            //los parámetros de la textura a editar y se permite el acceso al editor
            if (UTool.sucessfulParametrization)
            {
                UTool.SaveParameters();
            }
        }

        //El caso en el cual no se haya encontrado un error durante la parametrizacion de los valores, se permite el acceso al editor de la misma
        if (UTool.sucessfulParametrization)
        {
            GUILayout.Label("| Name: " + UTool.textureSavedName + "| Size: " + ExtInterface.uToolGetTextureWidth() + " X " + ExtInterface.uToolGetTextureHeight() + " |", EditorStyles.boldLabel);

            Event mouseEvent = Event.current;
            UTool.screenPos  = mouseEvent.mousePosition;
            
            //Ajustamos la posición del ratón sobre el área de la textura ( 0 a 100 en X e Y sobre textura)
            EditorGUILayout.Separator();
            UTool.texCoordsY = (UTool.screenPos.y - 960) / 5;
            UTool.texCoordsX = (UTool.screenPos.x) / 5;

            //Clamp Coordenada X
            if (UTool.texCoordsX <    0) UTool.texCoordsX =    0;
            if (UTool.texCoordsX >  100) UTool.texCoordsX =  100;

            //Clam e inversión de coordenada Y
            if (UTool.texCoordsY >    0) UTool.texCoordsY =    0;
            if (UTool.texCoordsY < -100) UTool.texCoordsY = -100;
            UTool.texCoordsY = Mathf.Abs(UTool.texCoordsY);

            //Si se pulsa boton izquierdo sobre el área de dibujo, se DIBUJA sobre la textura
            if (EditorWindow.focusedWindow == this && EditorWindow.mouseOverWindow == this && mouseEvent.button == 0 && mouseEvent.isMouse
                && UTool.texCoordsX > 0 && UTool.texCoordsX < 100 && UTool.texCoordsY > 0 && UTool.texCoordsY < 100)
            {
                UTool.UpdateTexture(UTool.BrushState.paint);
                Repaint();
            }

            //Si se pulsa boton derecho sobre el área de dibujo, BORRA lo pintado sobre la textura
            if (EditorWindow.focusedWindow == this && EditorWindow.mouseOverWindow == this && mouseEvent.button == 1 && mouseEvent.isMouse
                && UTool.texCoordsX > 0 && UTool.texCoordsX < 100 && UTool.texCoordsY > 0 && UTool.texCoordsY < 100)
            {
                UTool.UpdateTexture(UTool.BrushState.erase);
                Repaint();
            }

            GUILayout.Label("Optional Filters", EditorStyles.boldLabel);

            //Aplicación del filtro Blanco/Negro
            if (GUILayout.Button("Apply Black/White Filter"))
            {
                UTool.ApplyFilter(UTool.FilterType.blackWhite);
                Repaint();
            }

            //Aplicación del filtro sepia
            if (GUILayout.Button("Apply Sepia Filter"))
            {
                UTool.ApplyFilter(UTool.FilterType.sepia);
                Repaint();
            }

            //Selector de Color del pincel y tamaño del mismo
            EditorGUILayout.Separator();

            GUILayout.Label("Custom Painting | LMB - Paint / RMB - Erase |" + "Tex_Coord: ( " + UTool.texCoordsX + " , " + UTool.texCoordsY + " )", EditorStyles.boldLabel);
            UTool.brushColor = EditorGUILayout.ColorField("Brush Color"     , UTool.brushColor);
            UTool.brushSize  = EditorGUILayout.IntSlider ("Brush Pixel Size", UTool.brushSize , 1, 3);

            //Reinicio de los cambios a la textura original
            if (GUILayout.Button("Reset Changes"))
            {
                UTool.ResetChanges();
                Repaint();
            }

            //Guardado de los cambios sobre la textura
            if (GUILayout.Button("Save Texture"))
            {
                UTool.SaveTextureToFile();
            }

            //Área dispuesta para el dibujado de la textura
            GUI.DrawTexture(new Rect(0, 460, 500, 500), UTool.textureBeingEdited, ScaleMode.ScaleToFit, true);
            Vector2 convertedGUIPos = GUIUtility.ScreenToGUIPoint(UTool.screenPos);
        }
    }
}

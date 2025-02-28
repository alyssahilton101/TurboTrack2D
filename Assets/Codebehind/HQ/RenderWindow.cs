using HQ;
using System;
using System.Runtime.ConstrainedExecution;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.GraphicsBuffer;


public class RenderWindow
{


    internal void draw(Mesh mesh, Material mat, Matrix4x4 fixAspect)
    {

        //Graphics.DrawMesh(mesh, Vector3.zero, Quaternion.identity, mat, layer);
        if (mat.SetPass(0))
        {
            Graphics.DrawMeshNow(mesh, fixAspect);
        }
    }

    internal void clear(Color color)
    {
        throw new NotImplementedException();
    }

    internal void draw(Rect offsetSource, Sprite sprite, Rect taret, bool flip)
    {
        //Graphics.DrawTexture(
        //    new Rect(
        //        taret.x + taret.x * Source.x,
        //        taret.y + taret.y * Source.y,
        //        taret.width * Source.width,
        //        taret.height * Source.height
        //    ),
        //    s.texture,
        //    new Rect(
        //        s.rect.x / s.texture.width + Source.x,
        //        s.rect.y / s.texture.height + Source.y,
        //        s.rect.width / s.texture.width  * Source.width,
        //        s.rect.height / s.texture.height * Source.height)
        //    , 0, 0, 0, 0);

      
        var sign = flip ? -1: 1;

       Graphics.DrawTexture(
            new Rect(
                taret.x,
                taret.y,
                taret.width * sign,
                taret.height * offsetSource.height
            ),
            sprite.texture,
            new Rect(
                sprite.rect.x / sprite.texture.width,
                sprite.rect.y / sprite.texture.height + (1 - offsetSource.height) * sprite.rect.height / sprite.texture.height,
                sprite.rect.width / sprite.texture.width,
                sprite.rect.height / sprite.texture.height * offsetSource.height)
        , 0, 0, 0, 0);

        
       

    }


    public static bool Intersecting(Rect a, Rect b)
    {
        return !(a.xMax < b.xMin || a.xMin > b.xMax || a.yMax < b.yMin || a.yMin > b.yMax);
    }

    private void DrawRect(Rect rect, Color color)
    {
        Vector3 bottomLeft = new Vector3(rect.xMin, rect.yMin, 0);
        Vector3 bottomRight = new Vector3(rect.xMax, rect.yMin, 0);
        Vector3 topLeft = new Vector3(rect.xMin, rect.yMax, 0);
        Vector3 topRight = new Vector3(rect.xMax, rect.yMax, 0);

        Debug.DrawLine(bottomLeft, bottomRight, color, 2f);
        Debug.DrawLine(bottomRight, topRight, color, 2f);
        Debug.DrawLine(topRight, topLeft, color, 2f);
        Debug.DrawLine(topLeft, bottomLeft, color, 2f);
    }



}
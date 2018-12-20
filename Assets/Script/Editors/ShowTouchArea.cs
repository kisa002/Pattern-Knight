using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("CircleCollider2D")]
public class ShowTouchArea : MonoBehaviour
{
    public CircleCollider2D areaCol;

    public void OnDrawGizmos()
    {
        if (areaCol == null)
            areaCol = GetComponent<CircleCollider2D>();

    }
}

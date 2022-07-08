using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transform : MonoBehaviour
{   public Vector3 axis = new Vector3(0, 0, 0);

    public float angle=0;

    private double[,] rotationMatrix = new double[4, 4];

    // private GameObject cube;
    Vector3[] origVerts;
    Vector3[] newVerts;
    Mesh mesh;

    Matrix4x4 mat = new Matrix4x4();

    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        mat[3, 1] = 0; mat[3, 1] = 0; mat[3, 1] = 0; mat[3, 1] = 1;
        mat[0, 3] = 0;
        mat[1, 3] = 0; 
        mat[2, 3] = 0;

        // origVerts = cube.GetComponent<Mesh>().vertices;
        origVerts = mesh.vertices;
        newVerts = new Vector3[origVerts.Length];
    }

    // Update is called once per frame
    void Update()
    {
        // can be cleaned so this only get's calculated when the user makes changes to the axes or angle
        float c = Mathf.Cos(angle);
        float s = Mathf.Sin(angle);

        axis = axis.normalized;

        mat[0,0] = c + Mathf.Pow(axis.x, 2)*(1 - c); mat[0,1] = axis.x*axis.y*(1 - c) - (axis.z * s); mat[0,2] = axis.x*axis.z*(1-c)+(axis.y*s);
        mat[1,0] = (axis.y*axis.x)*(1-c)+(axis.z*s); mat[1,1] = c + Mathf.Pow(axis.y, 2)*(1-c); mat[1,2] = (axis.y*axis.z)*(1-c)-axis.x*s;
        mat[2,0] = (axis.z*axis.x)*(1-c)-(axis.y*s); mat[2,1] = (axis.z*axis.y)*(1-c)+(axis.x*s); mat[2,2] = c + Mathf.Pow(axis.z, 2)*(1-c);

        for(int i = 0; i < origVerts.Length; i++) {
            newVerts[i] = mat.MultiplyPoint3x4(origVerts[i]);
        }

         mesh.vertices = newVerts;

    }
}

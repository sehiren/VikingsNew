using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MeshGenerator : MonoBehaviour
{
    public SquareGrid squareGrid;
    public MeshFilter walls;
    public MeshFilter cave;

    List<Vector3> vertices; //Lista de posiciones de los vértices
    List<int> triangles;
    List<List<int>> outlines = new List<List<int>>();
    HashSet<int> checkedVertices = new HashSet<int>();

    public MeshFilter getWalls()
    {
        return walls;
    }

    Dictionary<int, List<Triangle>> triangleDictionary = new Dictionary<int, List<Triangle>>();//para sacar los nodos conectados a un triángulo
    public class Node //los nodos que estan en mitad de las aristas
    {
        public Vector3 position; //su posición en el mundo
        public int vertexIndex = -1; //su id

        public Node(Vector3 _pos)
        {
            position = _pos;
        }
    }

    public class ControlNode : Node //los nodos que estan en las esquinas de las aristas
    {

        public bool active; //active wall, not active tile
        public Node above, right;

        public ControlNode(Vector3 _pos, bool _active, float squareSize) : base(_pos) // base(_pos) hereda el valor _pos de la clase mare
        {
            active = _active;
            above = new Node(position + Vector3.forward * squareSize / 2f);//es la mitad porque cres un Nodo que está en la mitad de la arista
            right = new Node(position + Vector3.right * squareSize / 2f);
        }

    }

    public class Square
    {

        public ControlNode topLeft, topRight, bottomRight, bottomLeft;
        public Node centreTop, centreRight, centreBottom, centreLeft;
        public int configuration;//guarda els bits ctius

        public Square(ControlNode _topLeft, ControlNode _topRight, ControlNode _bottomRight, ControlNode _bottomLeft)
        {
            topLeft = _topLeft;
            topRight = _topRight;
            bottomRight = _bottomRight;
            bottomLeft = _bottomLeft;

            centreTop = topLeft.right;
            centreRight = bottomRight.above;
            centreBottom = bottomLeft.right;
            centreLeft = bottomLeft.above;

            if (topLeft.active)
                configuration += 8; //el bists que se sumen segons el nodo actiu
            if (topRight.active)
                configuration += 4;
            if (bottomRight.active)
                configuration += 2;
            if (bottomLeft.active)
                configuration += 1;
        }
    }

    public class SquareGrid
    {
        public Square[,] squares; //array de cuadrados

        public SquareGrid(int[,] map, float squareSize)
        {
            int nodeCountX = map.GetLength(0);
            int nodeCountY = map.GetLength(1);
            float mapWidth = nodeCountX * squareSize;
            float mapHeight = nodeCountY * squareSize;

            ControlNode[,] controlNodes = new ControlNode[nodeCountX, nodeCountY];

            for (int x = 0; x < nodeCountX; x++)
            {
                for (int y = 0; y < nodeCountY; y++)
                {
                    Vector3 pos = new Vector3(-mapWidth / 2 + x * squareSize + squareSize / 2, 0, -mapHeight / 2 + y * squareSize + squareSize / 2);
                    //-mapWith/2 far left (no antec)
                    controlNodes[x, y] = new ControlNode(pos, map[x, y] == 1, squareSize);//pasa true or false (==1)
                }
            }

            squares = new Square[nodeCountX - 1, nodeCountY - 1]; //sempre hi ha un quadrat menos que nodos
            for (int x = 0; x < nodeCountX - 1; x++)
            {
                for (int y = 0; y < nodeCountY - 1; y++)
                {
                    squares[x, y] = new Square(controlNodes[x, y + 1], controlNodes[x + 1, y + 1], controlNodes[x + 1, y], controlNodes[x, y]);
                }
            }
            //va a cada quadrat i fa els seus control Nodes, bottom left, bottom right etc. 
        }
    }


    void TriangulateSquare(Square square)
    {
        switch (square.configuration)//este switch mira totes les combinacions possibles de nodos actius
        {
            case 0://no tinguem mesh
                break;

            // 1 points:
            case 1://el case es el número de confi (la suma de bits actius vamos)
                MeshFromPoints(square.centreLeft, square.centreBottom, square.bottomLeft);
                break; ;
            case 2:
                MeshFromPoints(square.bottomRight, square.centreBottom, square.centreRight);
                break;
            case 4:
                MeshFromPoints(square.topRight, square.centreRight, square.centreTop);
                break;
            case 8:
                MeshFromPoints(square.topLeft, square.centreTop, square.centreLeft);
                break;

            // 2 points:
            case 3:
                MeshFromPoints(square.centreRight, square.bottomRight, square.bottomLeft, square.centreLeft);
                break;
            case 6:
                MeshFromPoints(square.centreTop, square.topRight, square.bottomRight, square.centreBottom);
                break;
            case 9:
                MeshFromPoints(square.topLeft, square.centreTop, square.centreBottom, square.bottomLeft);
                break;
            case 12:
                MeshFromPoints(square.topLeft, square.topRight, square.centreRight, square.centreLeft);
                break;
            case 5:
                MeshFromPoints(square.centreTop, square.topRight, square.centreRight, square.centreBottom, square.bottomLeft, square.centreLeft);
                break;
            case 10:
                MeshFromPoints(square.topLeft, square.centreTop, square.centreRight, square.bottomRight, square.centreBottom, square.centreLeft);
                break;

            // 3 point:
            case 7:
                MeshFromPoints(square.centreTop, square.topRight, square.bottomRight, square.bottomLeft, square.centreLeft);
                break;
            case 11:
                MeshFromPoints(square.topLeft, square.centreTop, square.centreRight, square.bottomRight, square.bottomLeft);
                break;
            case 13:
                MeshFromPoints(square.topLeft, square.topRight, square.centreRight, square.centreBottom, square.bottomLeft);
                break;
            case 14:
                MeshFromPoints(square.topLeft, square.topRight, square.bottomRight, square.centreBottom, square.centreLeft);
                break;

            // 4 point:
            case 15:
                MeshFromPoints(square.topLeft, square.topRight, square.bottomRight, square.bottomLeft);
                checkedVertices.Add(square.topLeft.vertexIndex);
                checkedVertices.Add(square.topRight.vertexIndex);
                checkedVertices.Add(square.bottomRight.vertexIndex);
                checkedVertices.Add(square.bottomLeft.vertexIndex);
                break;
        }
    }

    void MeshFromPoints(params Node[] points) //crea una malla a parti de punts
    {
        AssignVertices(points);

        if (points.Length >= 3)//1er triangle
            CreateTriangle(points[0], points[1], points[2]);
        if (points.Length >= 4)//2n trinagle
            CreateTriangle(points[0], points[2], points[3]);
        if (points.Length >= 5)//3er triangle
            CreateTriangle(points[0], points[3], points[4]);
        if (points.Length >= 6)//4t triangle, nomes pasa en el cas diagonal, esto torna anar del bits dels nodos 
            CreateTriangle(points[0], points[4], points[5]);
    }

    void AssignVertices(Node[] points)
    {
        for (int i = 0; i < points.Length; i++) //va a cada punt 
        {
            if (points[i].vertexIndex == -1)//si ixe punt es -1, no ha sigut assignat
            {
                points[i].vertexIndex = vertices.Count;//li fiquem el tamany actual de la llista de vertices
                vertices.Add(points[i].position);//i despres el anyadim a ixa llista
            }
        }
    }

    void CreateTriangle(Node a, Node b, Node c)
    {
        triangles.Add(a.vertexIndex);
        triangles.Add(b.vertexIndex);
        triangles.Add(c.vertexIndex);

        Triangle triangle = new Triangle(a.vertexIndex, b.vertexIndex, c.vertexIndex);

        //metemos todas las nuevas entradas al diccionario
        AddTriangleToDictionary(triangle.vertexIndexA, triangle);
        AddTriangleToDictionary(triangle.vertexIndexB, triangle);
        AddTriangleToDictionary(triangle.vertexIndexC, triangle);
    }

    public void GenerateMesh(int[,] map, float squareSize)
    {
        outlines.Clear();
        checkedVertices.Clear();
        triangleDictionary.Clear();

        squareGrid = new SquareGrid(map, squareSize);//pasa el mapa y el tamaño del cuadrado
        vertices = new List<Vector3>();
        triangles = new List<int>();

        for (int x = 0; x < squareGrid.squares.GetLength(0); x++)//anem a visitar cada quadrat
        {
            for (int y = 0; y < squareGrid.squares.GetLength(1); y++)
            {
                TriangulateSquare(squareGrid.squares[x, y]);
            }
        }

        Mesh mesh = new Mesh();
        cave.mesh = mesh;

        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.RecalculateNormals();

        CreateWallMesh();
    }

    void CreateWallMesh()
    {

        CalculateMeshOutlines();

        List<Vector3> wallVertices = new List<Vector3>();
        List<int> wallTriangles = new List<int>();
        Mesh wallMesh = new Mesh();
        float wallHeight = 5;

        foreach (List<int> outline in outlines)//per a cada outline vertex
        {
            for (int i = 0; i < outline.Count - 1; i++)
            {
                int startIndex = wallVertices.Count;
                //per a fer la seccio de la pared en la que estem treballant
                wallVertices.Add(vertices[outline[i]]); // left
                wallVertices.Add(vertices[outline[i + 1]]); // right
                wallVertices.Add(vertices[outline[i]] - Vector3.up * wallHeight); // bottom left
                wallVertices.Add(vertices[outline[i + 1]] - Vector3.up * wallHeight); // bottom right

                wallTriangles.Add(startIndex + 0);
                wallTriangles.Add(startIndex + 2);
                wallTriangles.Add(startIndex + 3);

                wallTriangles.Add(startIndex + 3);
                wallTriangles.Add(startIndex + 1);
                wallTriangles.Add(startIndex + 0);
            }
        }
        wallMesh.vertices = wallVertices.ToArray();
        wallMesh.triangles = wallTriangles.ToArray();
        walls.mesh = wallMesh;

        MeshCollider wallCollider = walls.gameObject.AddComponent<MeshCollider>();
       // wallCollider.sharedMesh = wallMesh;

    }
    struct Triangle
    {
        public int vertexIndexA;
        public int vertexIndexB;
        public int vertexIndexC;
        int[] vertices;

        public Triangle(int a, int b, int c)
        {
            vertexIndexA = a;
            vertexIndexB = b;
            vertexIndexC = c;

            vertices = new int[3];
            vertices[0] = a;
            vertices[1] = b;
            vertices[2] = c;
        }
        public int this[int i]//accedeir al vértices com un array
        {
            get
            {
                return vertices[i];
            }
        }


        public bool Contains(int vertexIndex)
        {
            return vertexIndex == vertexIndexA || vertexIndex == vertexIndexB || vertexIndex == vertexIndexC;
        }

    }

    void AddTriangleToDictionary(int vertexIndexKey, Triangle triangle)//key es el vertice, devuelve los triángulos que lo contienen
    {
        if (triangleDictionary.ContainsKey(vertexIndexKey))//si ya existe el vértice en el diccionario
        {
            triangleDictionary[vertexIndexKey].Add(triangle);//le añadimos el nuevo triángulo que lo contiene
        }
        else //si no
        {
            List<Triangle> triangleList = new List<Triangle>();//creamos una nueva lista de triángulos
            triangleList.Add(triangle);//metemos el el triángulo este
            triangleDictionary.Add(vertexIndexKey, triangleList);//creamos la entrada con el vértice y una lista de triángulos
        }
    }

    bool IsOutlineEdge(int vertexA, int vertexB)//Nos dice si la arista formada por los vértices A y B es un otuline edge o no (cada vértice solo pertenece a un triángulo)
    {
        List<Triangle> trianglesContainingVertexA = triangleDictionary[vertexA];
        int sharedTriangleCount = 0;

        for (int i = 0; i < trianglesContainingVertexA.Count; i++)
        {
            if (trianglesContainingVertexA[i].Contains(vertexB))
            {
                sharedTriangleCount++;
                if (sharedTriangleCount > 1)
                {
                    break;
                }
            }
        }
        return sharedTriangleCount == 1;
    }

    int GetConnectedOutlineVertex(int vertexIndex)//dona el seguent vertex de uno donat per a crear el outline
    {
        List<Triangle> trianglesContainingVertex = triangleDictionary[vertexIndex];

        for (int i = 0; i < trianglesContainingVertex.Count; i++)
        {
            Triangle triangle = trianglesContainingVertex[i];

            for (int j = 0; j < 3; j++)
            {
                int vertexB = triangle[j];
                if (vertexB != vertexIndex && !checkedVertices.Contains(vertexB))
                {
                    if (IsOutlineEdge(vertexIndex, vertexB))
                    {
                        return vertexB;
                    }
                }
            }
        }

        return -1;
    }

    void CalculateMeshOutlines()//Busca un outline vertex i quan el troba, recorre tota la outline a partir d'ell fins que es torna a trobar a ell mateixa.
    {

        for (int vertexIndex = 0; vertexIndex < vertices.Count; vertexIndex++)
        {
            if (!checkedVertices.Contains(vertexIndex))
            {
                int newOutlineVertex = GetConnectedOutlineVertex(vertexIndex);
                if (newOutlineVertex != -1)//si exiteix es un outline 
                {
                    checkedVertices.Add(vertexIndex);

                    List<int> newOutline = new List<int>();
                    newOutline.Add(vertexIndex);
                    outlines.Add(newOutline);//aixina els clava en la outline list
                    FollowOutline(newOutlineVertex, outlines.Count - 1);
                    outlines[outlines.Count - 1].Add(vertexIndex);
                }
            }
        }
    }

    void FollowOutline(int vertexIndex, int outlineIndex)//
    {
        outlines[outlineIndex].Add(vertexIndex);
        checkedVertices.Add(vertexIndex);
        int nextVertexIndex = GetConnectedOutlineVertex(vertexIndex);

        if (nextVertexIndex != -1)
        {
            FollowOutline(nextVertexIndex, outlineIndex);//metodo recursiu per a recorrer el outline
        }
    }

}










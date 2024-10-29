using System;
using System.Collections.Generic;
using System.Drawing;
using System.Security.Cryptography;
using System.Windows.Forms;
using static lab6.Form1;

namespace lab6
{
    public partial class Form1 : Form
    {
        Polyhedron polyhedron;
        int del = 50;
        private bool IsPersp = true;
        private TransformationMatrix currentProjectionMatrix;
        public Form1()
        {
            InitializeComponent();
            polyhedron = CreateDodecahedr();

            pictureBox1.Paint += new PaintEventHandler(pictureBox1_Paint);

            txtOffsetX.KeyPress += ApplyTranslation;
            txtOffsetY.KeyPress += ApplyTranslation;
            txtOffsetZ.KeyPress += ApplyTranslation;

            txtRotationX.KeyPress += ApplyRotation;
            txtRotationY.KeyPress += ApplyRotation;
            txtRotationZ.KeyPress += ApplyRotation;

            txtScaleX.KeyPress += ApplyScaling;
            txtScaleY.KeyPress += ApplyScaling;
            txtScaleZ.KeyPress += ApplyScaling;

            
            currentProjectionMatrix = TransformationMatrix.PerspectiveProjection(del);
            comboBox1.Items.Add("Центральная");
            comboBox1.Items.Add("Аксонометрическая");
            comboBox1.SelectedItem = "Центральная"; 
            comboBox1.SelectedIndex = 0; 
            pictureBox1.Invalidate();
        }

        // Класс точки
        public class Point3D
        {
            public double X { get; set; }
            public double Y { get; set; }
            public double Z { get; set; }

            public Point3D(double x, double y, double z)
            {
                X = x;
                Y = y;
                Z = z;
            }
        }

        // Класс грани
        public class Face
        {
            public List<int> Vertices { get; set; }

            public Face(List<int> vertices)
            {
                Vertices = vertices;
            }
        }

        // Класс многогранника
        public class Polyhedron
        {
            public List<Point3D> Vertices { get; set; } //точки
            public List<Face> Faces { get; set; } //грани

            public Polyhedron()
            {
                Vertices = new List<Point3D>();
                Faces = new List<Face>();
            }

            public Polyhedron(List<Point3D> vertices, List<Face> faces)
            {
                Vertices = vertices;
                Faces = faces;
            }
        }

        // Класс матрицы афинных преобразований
        // =========================================================================
        public class TransformationMatrix
        {
            public double[,] matrix = new double[4, 4];

            public TransformationMatrix()
            {
                matrix[0, 0] = 1;
                matrix[1, 1] = 1;
                matrix[2, 2] = 1;
                matrix[3, 3] = 1;
            }


            public Point3D Transform(Point3D p)
            {
                double x = matrix[0, 0] * p.X + matrix[1, 0] * p.Y + matrix[2, 0] * p.Z + matrix[3, 0];
                double y = matrix[0, 1] * p.X + matrix[1, 1] * p.Y + matrix[2, 1] * p.Z + matrix[3, 1];
                double z = matrix[0, 2] * p.X + matrix[1, 2] * p.Y + matrix[2, 2] * p.Z + matrix[3, 2];
                

                return new Point3D(x, y, z);
            }

            public Point3D TransformForPerspect(Point3D p)
            {
                double x = matrix[0, 0] * p.X + matrix[1, 0] * p.Y + matrix[2, 0] * p.Z + matrix[3, 0];
                double y = matrix[0, 1] * p.X + matrix[1, 1] * p.Y + matrix[2, 1] * p.Z + matrix[3, 1];
                double z = matrix[0, 2] * p.X + matrix[1, 2] * p.Y + matrix[2, 2] * p.Z + matrix[3, 2];
                double w = matrix[0, 3] * p.X + matrix[1, 3] * p.Y + matrix[2, 3] * p.Z + matrix[3, 3];

                if (w != 0)
                {
                    x /= w;
                    y /= w;
                    z /= w;
                }

                return new Point3D(x, y, z);
            }
            public static TransformationMatrix PerspectiveProjection(double distance)
            {
                // Перспективная проекция
                TransformationMatrix result = new TransformationMatrix();
                //result.matrix[0, 0] = 1;
                //result.matrix[1, 1] = 1;
                //result.matrix[2, 2] = 1;
                //result.matrix[2, 3] = 1 / distance; // Устанавливаем масштаб по Z
                //result.matrix[3, 2] = -1;
                //result.matrix[3, 3] = 0;

                result.matrix[0, 0] = 1;
                result.matrix[1, 1] = 1;
                result.matrix[2,2] = 0;
                result.matrix[2,3] = -1/distance;
                result.matrix[3, 3] = 1;
                return result;
            }

            public static TransformationMatrix AxonometricProjection(double theta, double phi)
            {
                
                double cosTheta = Math.Cos(theta * Math.PI / 180);
                double sinTheta = Math.Sin(theta * Math.PI / 180);
                double cosPhi = Math.Cos(phi * Math.PI / 180);
                double sinPhi = Math.Sin(phi * Math.PI / 180);

                TransformationMatrix result = new TransformationMatrix();
                result.matrix[0, 0] = cosTheta;
                result.matrix[0, 1] = sinTheta * sinPhi;
                result.matrix[1, 1] = cosPhi;
                result.matrix[2, 0] = sinTheta;
                result.matrix[2, 1] = -cosTheta * sinPhi;
                result.matrix[3, 3] = 1;
                result.matrix[2,2] = 0;
                return result;
            }

            public static TransformationMatrix Multiply(TransformationMatrix a, TransformationMatrix b)
            {
                TransformationMatrix result = new TransformationMatrix();
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        result.matrix[i, j] = 0;
                        for (int k = 0; k < 4; k++)
                        {
                            result.matrix[i, j] += a.matrix[i, k] * b.matrix[k, j];
                        }
                    }
                }
                return result;
            }

            public static TransformationMatrix CreateTranslationMatrix(double dx, double dy, double dz)
            {
                TransformationMatrix result = new TransformationMatrix();
                result.matrix[3, 0] = dx;
                result.matrix[3, 1] = dy;
                result.matrix[3, 2] = dz;
                return result;
            }

            public static TransformationMatrix CreateScalingMatrix(double scaleX, double scaleY, double scaleZ)
            {
                TransformationMatrix result = new TransformationMatrix();
                result.matrix[0, 0] = scaleX;
                result.matrix[1, 1] = scaleY;
                result.matrix[2, 2] = scaleZ;
                return result;
            }

            public static TransformationMatrix CreateScalingMatrix(double scaleX, double scaleY, double scaleZ, Point3D center)
            {
                var translateToOrigin = CreateTranslationMatrix(-center.X, -center.Y, -center.Z);
                var scalingMatrix = CreateScalingMatrix(scaleX, scaleY, scaleZ);
                var translateBack = CreateTranslationMatrix(center.X, center.Y, center.Z);

                return Multiply(Multiply(translateToOrigin, scalingMatrix), translateBack);
            }


            public static TransformationMatrix CreateRotationMatrixX(double angle)
            {
                double radians = angle * Math.PI / 180;
                TransformationMatrix result = new TransformationMatrix();
                double cos = Math.Cos(radians);
                double sin = Math.Sin(radians);
                result.matrix[1, 1] = cos;
                result.matrix[1, 2] = -sin;
                result.matrix[2, 1] = sin;
                result.matrix[2, 2] = cos;
                return result;
            }

            public static TransformationMatrix CreateRotationMatrixY(double angle)
            {
                double radians = angle * Math.PI / 180;
                TransformationMatrix result = new TransformationMatrix();
                double cos = Math.Cos(radians);
                double sin = Math.Sin(radians);
                result.matrix[0, 0] = cos;
                result.matrix[0, 2] = -sin;
                result.matrix[2, 0] = sin;
                result.matrix[2, 2] = cos;
                return result;
            }

            public static TransformationMatrix CreateRotationMatrixZ(double angle)
            {
                double radians = angle * Math.PI / 180;
                TransformationMatrix result = new TransformationMatrix();
                double cos = Math.Cos(radians);
                double sin = Math.Sin(radians);
                result.matrix[0, 0] = cos;
                result.matrix[0, 1] = sin;
                result.matrix[1, 0] = -sin;
                result.matrix[1, 1] = cos;
                return result;
            }


            public static TransformationMatrix CreateRotationMatrix(double angleX, double angleY, double angleZ)
            {
                var rotationX = CreateRotationMatrixX(angleX);
                var rotationY = CreateRotationMatrixY(angleY);
                var rotationZ = CreateRotationMatrixZ(angleZ);
                return Multiply(Multiply(rotationX, rotationY), rotationZ);
            }

            public static TransformationMatrix CreateRotationAroundAxis(double angleX, double angleY, double angleZ, Point3D center)
            {
                TransformationMatrix translateToOrigin = CreateTranslationMatrix(-center.X, -center.Y, -center.Z);
                TransformationMatrix rotationMatrix = CreateRotationMatrix(angleX, angleY, angleZ);
                TransformationMatrix translateBack = CreateTranslationMatrix(center.X, center.Y, center.Z);
                return Multiply(Multiply(translateToOrigin, rotationMatrix), translateBack);
            }


            public static TransformationMatrix CreateReflectionMatrixXY()
            {
                TransformationMatrix result = new TransformationMatrix();
                result.matrix[2, 2] = -1;  // Отражение относительно плоскости XY меняет знак координаты Z
                return result;
            }

            public static TransformationMatrix CreateReflectionMatrixXZ()
            {
                TransformationMatrix result = new TransformationMatrix();
                result.matrix[1, 1] = -1;  // Отражение относительно плоскости XZ меняет знак координаты Y
                return result;
            }

            public static TransformationMatrix CreateReflectionMatrixYZ()
            {
                TransformationMatrix result = new TransformationMatrix();
                result.matrix[0, 0] = -1;  // Отражение относительно плоскости YZ меняет знак координаты X
                return result;
            }

            public static TransformationMatrix CreateRotationToZAxis(Point3D p1, Point3D p2)
            {
                double dx = p2.X - p1.X;
                double dy = p2.Y - p1.Y;
                double dz = p2.Z - p1.Z;

                double dXZ = Math.Sqrt(dx * dx + dz * dz);
                double cosY = dz / dXZ;
                double sinY = dx / dXZ;

                var rotateY = new TransformationMatrix();
                rotateY.matrix[0, 0] = cosY;
                rotateY.matrix[0, 2] = sinY;
                rotateY.matrix[2, 0] = -sinY;
                rotateY.matrix[2, 2] = cosY;

                double dXYZ = Math.Sqrt(dXZ * dXZ + dy * dy);
                double cosX = dXZ / dXYZ;
                double sinX = dy / dXYZ;

                var rotateX = new TransformationMatrix();
                rotateX.matrix[1, 1] = cosX;
                rotateX.matrix[1, 2] = -sinX;
                rotateX.matrix[2, 1] = sinX;
                rotateX.matrix[2, 2] = cosX;

                return Multiply(rotateX, rotateY);
            }

            public static TransformationMatrix CreateInverseRotationToZAxis(Point3D p1, Point3D p2)
            {
                double dx = p2.X - p1.X;
                double dy = p2.Y - p1.Y;
                double dz = p2.Z - p1.Z;

                double dXZ = Math.Sqrt(dx * dx + dz * dz);
                double cosY = dz / dXZ;
                double sinY = dx / dXZ;

                var inverseRotateY = new TransformationMatrix();
                inverseRotateY.matrix[0, 0] = cosY;
                inverseRotateY.matrix[0, 2] = -sinY;
                inverseRotateY.matrix[2, 0] = sinY;
                inverseRotateY.matrix[2, 2] = cosY;

                double dXYZ = Math.Sqrt(dXZ * dXZ + dy * dy);
                double cosX = dXZ / dXYZ;
                double sinX = dy / dXYZ;

                var inverseRotateX = new TransformationMatrix();
                inverseRotateX.matrix[1, 1] = cosX;
                inverseRotateX.matrix[1, 2] = sinX;
                inverseRotateX.matrix[2, 1] = -sinX;
                inverseRotateX.matrix[2, 2] = cosX;

                return Multiply(inverseRotateY, inverseRotateX);
            }


            public static TransformationMatrix CreateRotationAroundLine(Point3D p1, Point3D p2, double angle)
            {
                // Шаг 1: Переносим p1 в начало координат
                var translateToOrigin = CreateTranslationMatrix(-p1.X, -p1.Y, -p1.Z);

                // Шаг 2: Вращаем прямую, чтобы она совпала с осью Z
                var rotationToZAxis = CreateRotationToZAxis(p1, p2);

                // Шаг 3: Вращение вокруг оси Z на заданный угол
                var rotationAroundZ = CreateRotationMatrixZ(angle);

                // Шаг 4: Обратное вращение, чтобы вернуть прямую в исходное положение
                var inverseRotationToZAxis = CreateInverseRotationToZAxis(p1, p2);

                // Шаг 5: Возвращаем точку p1 на её место
                var translateBack = CreateTranslationMatrix(p1.X, p1.Y, p1.Z);

                return Multiply(Multiply(Multiply(Multiply(translateToOrigin, rotationToZAxis), rotationAroundZ), inverseRotationToZAxis), translateBack);
            }
        }


        // =========================================================================

        //создание икосаэдра
        public Polyhedron CreateIcosahedron()
        {
            List<Point3D> vertices = new List<Point3D>();
            List<Face> faces = new List<Face>();

            double radius = 1.0;
            double height = 0.5;
            double sqrt5 = Math.Sqrt(5) / 2.0;

            // нижняя окружность Z = -0.5
            for (int i = 0; i < 5; i++)
            {
                double angle = 2 * Math.PI * i / 5;
                double x = radius * Math.Cos(angle);
                double y = radius * Math.Sin(angle);
                vertices.Add(new Point3D(x, y, -height));
            }

            // верхняя окружность Z = +0.5
            for (int i = 0; i < 5; i++)
            {
                double angle = 2 * Math.PI * (i + 0.5) / 5; // Смещаем на полуградуса
                double x = radius * Math.Cos(angle);
                double y = radius * Math.Sin(angle);
                vertices.Add(new Point3D(x, y, height));
            }

            // Добавляем две вершины на оси Z
            vertices.Add(new Point3D(0, 0, sqrt5));  // Верхняя вершина Z = sqrt(5)/2
            vertices.Add(new Point3D(0, 0, -sqrt5)); // Нижняя вершина Z = -sqrt(5)/2

            // Создаем грани на основе цилиндра
            for (int i = 0; i < 5; i++)
            {
                int next = (i + 1) % 5;

                // Соединяем нижний и верхний пояс
                faces.Add(new Face(new List<int> { i, next, i + 5 }));
                faces.Add(new Face(new List<int> { next, next + 5, i + 5 }));
            }

            // Соединяем верхнюю и нижнюю вершины с поясом
            for (int i = 0; i < 5; i++)
            {
                faces.Add(new Face(new List<int> { 10, i + 5, (i + 1) % 5 + 5 })); // Верхняя вершина с верхним поясом
                faces.Add(new Face(new List<int> { 11, i, (i + 1) % 5 }));          // Нижняя вершина с нижним поясом
            }

            return new Polyhedron(vertices, faces);
        }
        public static Polyhedron CreateDodecahedr()
        {
            double phi = (1 + Math.Sqrt(5)) / 2;
            double a = 1.0;

            List<Point3D> vertices = new List<Point3D>
            {
                new Point3D( a,  a,  a), //0
                new Point3D( a,  a, -a), //1
                new Point3D( a, -a,  a), //2
                new Point3D( a, -a, -a), //3
                new Point3D(-a,  a,  a), //4
                new Point3D(-a,  a, -a), //5
                new Point3D(-a, -a,  a), //6
                new Point3D(-a, -a, -a), //7
                new Point3D( 0,  1/phi * a,  phi * a), //8
                new Point3D( 0,  1/phi * a, -phi * a), //9
                new Point3D( 0, -1/phi * a,  phi * a), //10
                new Point3D( 0, -1/phi * a, -phi * a), //11
                new Point3D( 1/phi * a,  phi * a,  0), //12
                new Point3D(-1/phi * a,  phi * a,  0), //13
                new Point3D( 1/phi * a, -phi * a,  0), //14
                new Point3D(-1/phi * a, -phi * a,  0), //15
                new Point3D( phi * a,  0,  1/phi * a), //16
                new Point3D(-phi * a,  0,  1/phi * a), //17
                new Point3D( phi * a,  0, -1/phi * a), //18
                new Point3D(-phi * a,  0, -1/phi * a) //19
            };

            List<Face> faces = new List<Face>
            {
                new Face(new List<int> { 0, 8, 4, 13, 12 }),
                new Face(new List<int> { 16, 18, 3, 14, 2 }),
                new Face(new List<int> { 19, 5, 9, 11, 7 }),
                new Face(new List<int> { 6, 10, 2, 14, 15 }),
                new Face(new List<int> { 12, 13, 5, 9, 1 }),
                new Face(new List<int> { 0, 8 ,10, 2, 16 }),
                new Face(new List<int> { 13, 4, 17, 19, 5 }),
                new Face(new List<int> { 18, 1, 9, 11, 3 }),
                new Face(new List<int> { 0, 12, 1, 18, 16 }),
                new Face(new List<int> { 19, 17, 6, 15, 7 }),
                new Face(new List<int> { 3, 14, 15, 7, 11 }),
                new Face(new List<int> { 8, 4, 17, 6, 10 }),
            };

            return new Polyhedron(vertices, faces);
        }




        public Polyhedron CreateOctahedron()
        {
            List<Point3D> vertices = new List<Point3D>();
            List<Face> faces = new List<Face>();

            double sideLength = 2.0;  
            double halfSide = sideLength / 2.0;  

            vertices.Add(new Point3D(halfSide, 0, 0));   
            vertices.Add(new Point3D(-halfSide, 0, 0)); 
            vertices.Add(new Point3D(0, halfSide, 0));   
            vertices.Add(new Point3D(0, -halfSide, 0));
            vertices.Add(new Point3D(0, 0, halfSide));   
            vertices.Add(new Point3D(0, 0, -halfSide));  

            
            faces.Add(new Face(new List<int> { 0, 2, 4 }));
            faces.Add(new Face(new List<int> { 0, 4, 3 }));
            faces.Add(new Face(new List<int> { 0, 3, 5 }));
            faces.Add(new Face(new List<int> { 0, 5, 2 }));

            faces.Add(new Face(new List<int> { 1, 4, 2 }));
            faces.Add(new Face(new List<int> { 1, 3, 4 }));
            faces.Add(new Face(new List<int> { 1, 5, 3 }));
            faces.Add(new Face(new List<int> { 1, 2, 5 }));

            return new Polyhedron(vertices, faces);
        }
        public Polyhedron CreateTetrahedron()
        {
            List<Point3D> vertices = new List<Point3D>();
            List<Face> faces = new List<Face>();

            double sideLength = 1.0;  
            double halfSide = sideLength / 2.0;  

           
            vertices.Add(new Point3D(halfSide, halfSide, halfSide));   
            vertices.Add(new Point3D(-halfSide, -halfSide, halfSide)); 
            vertices.Add(new Point3D(halfSide, -halfSide, -halfSide)); 
            vertices.Add(new Point3D(-halfSide, halfSide, -halfSide)); 

           
            faces.Add(new Face(new List<int> { 0, 1, 2 })); 
            faces.Add(new Face(new List<int> { 0, 2, 3 })); 
            faces.Add(new Face(new List<int> { 0, 3, 1 })); 
            faces.Add(new Face(new List<int> { 1, 2, 3 })); 

            return new Polyhedron(vertices, faces);
        }


        public PointF Project(Point3D point, TransformationMatrix projectionMatrix)
        {
            

            if (!IsPersp)
            {

                Point3D projectedPoint = projectionMatrix.Transform(point);
                
                float x = ((float)projectedPoint.X / 1) * 100 + pictureBox1.Width / 2;
                float y = ((float)projectedPoint.Y / 1) * 100 + pictureBox1.Height / 2;
                return new PointF(x, y);
            }
            else
            {
                Point3D projectedPoint = projectionMatrix.TransformForPerspect(point);
                
                float x = ((float)projectedPoint.X) * 100 + pictureBox1.Width / 2;
                float y = ((float)projectedPoint.Y) * 100 + pictureBox1.Height / 2;
                return new PointF(x, y);
            }
        }
        //public PointF Project(Point3D point, TransformationMatrix projectionMatrix)
        //{

        //    Point3D projectedPoint = projectionMatrix.Transform(point);
        //    float zAdjusted = Math.Max((float)projectedPoint.Z, 0.01f); // предотвращение деления на 0
        //    float x = ((float)projectedPoint.X / 1) ;
        //    float y = ((float)projectedPoint.Y / 1) ;
        //    return new PointF(x, y);
            

        //}

        //// проецирование через x/z y/z (вызывается в DrawPolyhedron)
        //public Polyhedron CreateIcosahedron()
        //{
        //    List<Point3D> vertices = new List<Point3D>();
        //    List<Face> faces = new List<Face>();

        //    double radius = 100.0;  // Увеличиваем радиус, чтобы икосаэдр был крупнее
        //    double height = radius / 2.0;
        //    double sqrt5 = Math.Sqrt(5) / 2.0 * radius;

        //    // нижняя окружность Z = -height
        //    for (int i = 0; i < 5; i++)
        //    {
        //        double angle = 2 * Math.PI * i / 5;
        //        double x = radius * Math.Cos(angle);
        //        double y = radius * Math.Sin(angle);
        //        vertices.Add(new Point3D(x, y, -height));
        //    }

        //    // верхняя окружность Z = +height
        //    for (int i = 0; i < 5; i++)
        //    {
        //        double angle = 2 * Math.PI * (i + 0.5) / 5; // Смещаем на полуградуса
        //        double x = radius * Math.Cos(angle);
        //        double y = radius * Math.Sin(angle);
        //        vertices.Add(new Point3D(x, y, height));
        //    }

        //    // Добавляем две вершины на оси Z
        //    vertices.Add(new Point3D(0, 0, sqrt5));  // Верхняя вершина Z = sqrt(5)/2 * radius
        //    vertices.Add(new Point3D(0, 0, -sqrt5)); // Нижняя вершина Z = -sqrt(5)/2 * radius

        //    // Создаем грани
        //    for (int i = 0; i < 5; i++)
        //    {
        //        int next = (i + 1) % 5;

        //        // Соединяем нижний и верхний пояс
        //        faces.Add(new Face(new List<int> { i, next, i + 5 }));
        //        faces.Add(new Face(new List<int> { next, next + 5, i + 5 }));
        //    }

        //    // Соединяем верхнюю и нижнюю вершины с поясом
        //    for (int i = 0; i < 5; i++)
        //    {
        //        faces.Add(new Face(new List<int> { 10, i + 5, (i + 1) % 5 + 5 })); // Верхняя вершина с верхним поясом
        //        faces.Add(new Face(new List<int> { 11, i, (i + 1) % 5 }));          // Нижняя вершина с нижним поясом
        //    }

        //    // Центрирование координат
        //    double offsetX = pictureBox1.Width / 2;
        //    double offsetY = pictureBox1.Height / 2;
        //    for (int i = 0; i < vertices.Count; i++)
        //    {
        //        vertices[i] = new Point3D(vertices[i].X + offsetX, vertices[i].Y + offsetY, vertices[i].Z);
        //    }

        //    return new Polyhedron(vertices, faces);
        //}


        public void DrawPolyhedron(Polyhedron polyhedron, Graphics g, TransformationMatrix projectionMatrix)
        {
            
            Pen pen = new Pen(Color.Black, 1);

            foreach (Face face in polyhedron.Faces)
            {
                for (int i = 0; i < face.Vertices.Count; i++)
                {
                    int start = face.Vertices[i];
                    int end = face.Vertices[(i + 1) % face.Vertices.Count];

                    // Проецируем точки, используя переданную матрицу
                    PointF p1 = Project(polyhedron.Vertices[start], projectionMatrix);
                    PointF p2 = Project(polyhedron.Vertices[end], projectionMatrix);

                    // Рисуем линию между проецированными точками
                    g.DrawLine(pen, p1, p2);
                }
            }
        }


        // Обработчик события Paint для отрисовки на pictureBox1
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            
            DrawPolyhedron(polyhedron, e.Graphics, currentProjectionMatrix);
        }


        public static Point3D GetPolyhedronCenter(Polyhedron p)
        {
            if (p.Vertices == null || p.Vertices.Count == 0)
                return new Point3D(0, 0, 0);

            double sumX = 0, sumY = 0, sumZ = 0;

            foreach (var vertex in p.Vertices)
            {
                sumX += vertex.X;
                sumY += vertex.Y;
                sumZ += vertex.Z;
            }

            int numVertices = p.Vertices.Count;

            double centroidX = sumX / numVertices;
            double centroidY = sumY / numVertices;
            double centroidZ = sumZ / numVertices;

            return new Point3D(centroidX, centroidY, centroidZ);
        }

        // Применяем преобразования к многограннику
        // =========================================================================
        private bool TransformPolyhedron(TransformationMatrix transformationMatrix)
        {
            if (polyhedron != null)
            {
                for (int i = 0; i < polyhedron.Vertices.Count; i++)
                {
                    polyhedron.Vertices[i] = transformationMatrix.Transform(polyhedron.Vertices[i]);
                }
                pictureBox1.Invalidate();
                return true;
            }
            return false;
        }

        private void ApplyTranslation(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (double.TryParse(txtOffsetX.Text, out double dx) && double.TryParse(txtOffsetY.Text, out double dy)
                && double.TryParse(txtOffsetZ.Text, out double dz))
                {
                    TransformationMatrix matrix = TransformationMatrix.CreateTranslationMatrix(dx, dy, dz);
                    TransformPolyhedron(matrix);
                }
            }
        }

        private void ApplyRotation(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (double.TryParse(txtRotationX.Text, out double angleX) && double.TryParse(txtRotationY.Text, out double angleY)
                    && double.TryParse(txtRotationZ.Text, out double angleZ))
                {
                    Point3D center = GetPolyhedronCenter(polyhedron);
                    TransformationMatrix matrix = TransformationMatrix.CreateRotationAroundAxis(angleX, angleY, angleZ, center);
                    TransformPolyhedron(matrix);
                }
            }
        }

        private void ApplyScaling(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (double.TryParse(txtScaleX.Text, out double scaleFactorX) && double.TryParse(txtScaleY.Text, out double scaleFactorY)
                    && double.TryParse(txtScaleZ.Text, out double scaleFactorZ))
                {
                    Point3D center = GetPolyhedronCenter(polyhedron);
                    TransformationMatrix matrix = TransformationMatrix.CreateScalingMatrix(scaleFactorX, scaleFactorY, scaleFactorZ, center);
                    TransformPolyhedron(matrix);
                }
            }
        }

        private void cbFlipXY_CheckedChanged(object sender, EventArgs e)
        {
            TransformationMatrix matrix = TransformationMatrix.CreateReflectionMatrixXY();
            TransformPolyhedron(matrix);
        }

        private void cbFlipXZ_CheckedChanged(object sender, EventArgs e)
        {
            TransformationMatrix matrix = TransformationMatrix.CreateReflectionMatrixXZ();
            TransformPolyhedron(matrix);
        }

        private void cbFlipYZ_CheckedChanged(object sender, EventArgs e)
        {
            TransformationMatrix matrix = TransformationMatrix.CreateReflectionMatrixYZ();
            TransformPolyhedron(matrix);
        }

        // проекции
        
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) 
        {
            
            switch (comboBox1.SelectedItem.ToString())
            {
                case "Центральная":
                    currentProjectionMatrix = TransformationMatrix.PerspectiveProjection(del);
                    IsPersp = true;
                    break;
                case "Аксонометрическая":
                    currentProjectionMatrix = TransformationMatrix.AxonometricProjection(45, 30);
                    IsPersp = false;
                    break;
            }
            pictureBox1.Invalidate();
        }
    }
}

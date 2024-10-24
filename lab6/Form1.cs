using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace lab6
{
    public partial class Form1 : Form
    {
        Polyhedron polyhedron;

        public Form1()
        {
            InitializeComponent();
            polyhedron = CreateIcosahedron();

            pictureBox1.Paint += new PaintEventHandler(pictureBox1_Paint);

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

        // проецирование через x y
        public PointF Project(Point3D point)
        {
            float x = (float)point.X * 100 + pictureBox1.Width / 2;  // Масштабируем и смещаем к центру
            float y = (float)point.Y * 100 + pictureBox1.Height / 2; // Масштабируем и смещаем к центру
            return new PointF(x, y);
        }

        // проецирование через x/z y/z (вызывается в DrawPolyhedron)
        public PointF ProjectPerspective(Point3D point)
        {
            float x = ((float)point.X / (float)point.Z) * 100 + pictureBox1.Width / 2;  // Масштабируем и смещаем к центру
            float y = ((float)point.Y / (float)point.Z) * 100 + pictureBox1.Height / 2; // Масштабируем и смещаем к центру
            return new PointF(x, y);
        }

        // Отрисовка многогранника
        private void DrawPolyhedron(Polyhedron polyhedron, Graphics g)
        {
            Pen pen = new Pen(Color.Black, 1);

            foreach (Face face in polyhedron.Faces)
            {
                for (int i = 0; i < face.Vertices.Count; i++)
                {
                    int start = face.Vertices[i];
                    int end = face.Vertices[(i + 1) % face.Vertices.Count];

                    PointF p1 = Project(polyhedron.Vertices[start]);
                    PointF p2 = Project(polyhedron.Vertices[end]);

                    g.DrawLine(pen, p1, p2);
                }
            }
        }

        // Обработчик события Paint для отрисовки на pictureBox1
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            DrawPolyhedron(polyhedron, e.Graphics);
        }
    }
}

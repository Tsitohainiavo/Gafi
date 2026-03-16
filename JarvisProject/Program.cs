using System;
using System.Collections.Generic;

public class Point {
    public double X, Y;
    public Point(double x, double y) { X = x; Y = y; }
}

public class Program {
    // Le point d'entrée du programme
    public static void Main(string[] args) {
        // 1. Création d'un jeu de données de test
        List<Point> points = new List<Point> {
            new Point(0, 3), new Point(1, 1), new Point(2, 2),
            new Point(4, 4), new Point(0, 0), new Point(1, 2),
            new Point(3, 1), new Point(3, 3)
        };

        // 2. Appel de l'algorithme
        List<Point> enveloppe = ComputeConvexHull(points);

        // 3. Affichage du résultat
        Console.WriteLine("Sommets de l'enveloppe convexe :");
        foreach (var p in enveloppe) {
            Console.WriteLine($"({p.X}, {p.Y})");
        }

        // AJOUT : On affiche explicitement le retour au point de départ
        if (enveloppe.Count > 0) {
            Console.WriteLine($"({enveloppe[0].X}, {enveloppe[0].Y})  <- Retour au départ");
        }
    }

    // Ton algorithme (tel que décrit dans tes notes)
    private static int Orientation(Point p, Point q, Point r) {
        double val = (q.Y - p.Y) * (r.X - q.X) - (q.X - p.X) * (r.Y - q.Y);
        if (Math.Abs(val) < 1e-10) return 0; // Utilisation d'une marge pour les doubles
        return (val > 0) ? 1 : 2; 
    }

    public static List<Point> ComputeConvexHull(List<Point> points) {
        int n = points.Count;
        if (n < 3) return points;

        List<Point> hull = new List<Point>();

        int l = 0;
        for (int i = 1; i < n; i++) {
            if (points[i].Y < points[l].Y || (points[i].Y == points[l].Y && points[i].X < points[l].X))
                l = i;
        }

        int p = l, q;
        do {
            hull.Add(points[p]);
            q = (p + 1) % n;
            for (int i = 0; i < n; i++) {
                if (Orientation(points[p], points[i], points[q]) == 2)
                    q = i;
            }
            p = q;
        } while (p != l);

        return hull;
    }
}
using System;
using System.Collections.Generic;

public class Point {
    public double X, Y;
    public Point(double x, double y) { X = x; Y = y; }
}

public class JarvisAlgorithm {
    // Fonction pour déterminer l'orientation de trois points (p, q, r)
    // 0 -> Colinéaires, 1 -> Sens horaire, 2 -> Sens anti-horaire
    private static int Orientation(Point p, Point q, Point r) {
        double val = (q.Y - p.Y) * (r.X - q.X) - (q.X - p.X) * (r.Y - q.Y);
        if (val == 0) return 0; 
        return (val > 0) ? 1 : 2; 
    }

    public static List<Point> ComputeConvexHull(List<Point> points) {
        int n = points.Count;
        if (n < 3) return points; // Une enveloppe nécessite au moins 3 points

        List<Point> hull = new List<Point>();

        // 1. Trouver le point le plus bas (Pmin dans vos notes)
        int l = 0;
        for (int i = 1; i < n; i++) {
            if (points[i].Y < points[l].Y || (points[i].Y == points[l].Y && points[i].X < points[l].X))
                l = i;
        }

        int p = l, q;
        do {
            // Ajouter le point courant à l'enveloppe
            hull.Add(points[p]);

            // Chercher le point 'q' tel que (p, q, x) soit toujours dans le sens direct
            q = (p + 1) % n;
            for (int i = 0; i < n; i++) {
                // Si i est plus "à gauche" que q, alors i devient le nouveau candidat q
                if (Orientation(points[p], points[i], points[q]) == 2)
                    q = i;
            }

            // p devient le point que l'on vient de trouver
            p = q;

        } while (p != l); // Jusqu'à revenir au point de départ

        return hull;
    }
}
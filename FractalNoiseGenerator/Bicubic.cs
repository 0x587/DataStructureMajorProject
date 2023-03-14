using System;
using Godot;

public partial class FractalNoiseGenerator
{
    // private double Bicubic(double x, double a)
    // {
    //     double _x = Mathf.Abs(x);
    //     if (_x <= 1)
    //         return 1 - (a + 3) * _x * _x + (a + 2) * _x * _x * _x;
    //     if (_x <= 2)
    //         return -4 * a + 8 * a * _x - 5 * a * _x * _x + a * _x * _x * _x;
    //     return 0;
    // }
    
    /// <summary>
    ///   双三次插值
    /// </summary>
    private double BicubicInterpolation(int seed, double tarX, double tarY)
    {
        int[] x = new int[4];
        int[] y = new int[4];
        x[1] = Mathf.FloorToInt(tarX);
        y[1] = Mathf.FloorToInt(tarY);
        double _x = tarX - x[1];
        double _y = tarY - y[1];
        x[0] = x[1] - 1; y[0] = y[1] - 1;
        x[2] = x[1] + 1; y[2] = y[1] + 1;
        x[3] = x[1] + 2; y[3] = y[1] + 2;
        return CubicInterpolation(
            CubicInterpolation(Pseudorandom(seed, x[0], y[0]), Pseudorandom(seed, x[1], y[0]), Pseudorandom(seed, x[2], y[0]), Pseudorandom(seed, x[3], y[0]),
                      _x),
            CubicInterpolation(Pseudorandom(seed, x[0], y[1]), Pseudorandom(seed, x[1], y[1]), Pseudorandom(seed, x[2], y[1]), Pseudorandom(seed, x[3], y[1]),
                      _x),
            CubicInterpolation(Pseudorandom(seed, x[0], y[2]), Pseudorandom(seed, x[1], y[2]), Pseudorandom(seed, x[2], y[2]), Pseudorandom(seed, x[3], y[2]),
                      _x),
            CubicInterpolation(Pseudorandom(seed, x[0], y[3]), Pseudorandom(seed, x[1], y[3]), Pseudorandom(seed, x[2], y[3]), Pseudorandom(seed, x[3], y[3]),
                      _x),
            _y) * (1 / (1.5f * 1.5f));
    }
}
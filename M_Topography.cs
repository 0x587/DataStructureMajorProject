using Godot;
using System;

public partial class M_Topography : Node2D
{
    private FractalNoiseGenerator noise;
    public int HorCellNum = 1000;
    public int VerCellNum = 580;
    public float LevelCnt = 8.0f;
    public override void _Ready()
    {
        GD.Randomize();
        noise = new FractalNoiseGenerator(637);
        noise.Frequency = 1 / 150.0;
        noise.Octaves = 8;
        noise.WeightedStrength = -0.08;
        noise.Lacunarity = 2.2;
        noise.Gain = 0.56;
        noise.Amplitude = 1 / 2.5;
    }
    public override void _Draw()
    {
		const int size = 1;
        for (int y = 0; y < VerCellNum; y += size)
        {
            for (int x = 0; x < HorCellNum; x += size)
            {
                float curNoiseVal = (float)noise.GetFractalNoise(x, y);
                curNoiseVal = (1 - curNoiseVal) * 0.7f;
                curNoiseVal = (int)(curNoiseVal * LevelCnt) / LevelCnt;
                base.DrawRect(new Rect2(new Vector2(x, y), new Vector2(size, size)), new Color((float)curNoiseVal, curNoiseVal, curNoiseVal));
            }
        }
    }
}

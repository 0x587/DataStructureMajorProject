using System;
using Godot;

public partial class FractalNoiseGenerator
{
    /// <summary>
    ///   随机种子。
    /// </summary>
    public readonly int Seed;
    /// <summary>
    ///   频率（放大倍数的倒数）。（默认为0.01）
    /// </summary>
    public double Frequency;
    /// <summary>
    ///   振幅（分形时的初始振幅）。（默认为1 / 1.75）
    /// </summary>
    public double Amplitude;
    /// <summary>
    ///   分形时叠加的噪声个数。（默认为1）
    /// </summary>
    public int Octaves;
    /// <summary>
    ///   PingPong强度。（默认为2.0）
    /// </summary>
    public double PingPongStrength;
    /// <summary>
    ///   更高的WeightedStrength代表着当较低的octave影响较大（噪声值较低）时，则较高的octave影响变小。（默认为0，取值0~1）
    /// </summary>
    public double WeightedStrength;
    /// <summary>
    ///   octave每高一级，采样点坐标乘以Lacunarity。
    ///   当Lacunarity>1，相当于octave每高一级，要叠加的噪声图像缩小Lacunarity倍。（默认为2.0）
    /// </summary>
    public double Lacunarity;
    /// <summary>
    ///   octave每高一级，振幅乘以Gain。
    ///   当Gain<1，相当于octave每高一级，要叠加的噪声值影响变小。（默认为0.5）
    /// </summary>
    public double Gain;
    public FractalNoiseGenerator(int seed = 637)
    {
        Seed = seed;
        Frequency = 0.01;
        Amplitude = 1 / 1.75;
        Octaves = 1;
        PingPongStrength = 2.0;
        WeightedStrength = 0.0;
        Lacunarity = 2.0;
        Gain = 0.5;
    }
    /// <summary>
    ///   分形噪声
    /// </summary>
    public double GetFractalNoise(double x, double y)
    {
        x *= Frequency;
        y *= Frequency;
        int seed = Seed;

        double sum = 0;
        double amp = Amplitude;
        for (int i = 0; i < Octaves; ++i)
        {
            // seed++ —— 改变种子以生成不同噪声
            double singleNoise = BicubicInterpolation(seed++, x, y);
            // 使用PingPong函数进行标准化
            double normalizedNoise = PingPong((singleNoise + 1) * PingPongStrength);
            // 叠加
            // (normalizedNoise - 0.5) * 2 —— 从[0, 1]映射到[-1, 1]
            // * amp —— amp越大，当前噪声对结果影响越大
            sum += (normalizedNoise - 0.5) * 2 * amp;
            // 当WeightedStrength>0，若normalizedNoise值较小，amp也相应的会衰减的更快
            amp *= LinearInterpolation(1.0, normalizedNoise, WeightedStrength);

            x *= Lacunarity;
            y *= Lacunarity;
            amp *= Gain;
        }
        return TransformResult(sum);
    }
}
using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace FixedMath.NET.Bench
{
  public class Trigonometry
  {
    private const int N = 10000;
    private readonly Fix64[] sinAngles;
    private readonly Fix64[] atanAngles;

    public Trigonometry()
    {
      sinAngles = new Fix64[N];
      for (int i = 0; i < N; i++)
      {
        var angle = ((2 * Math.PI) / N) * i;
        sinAngles[i] = (Fix64)angle;
      }

      atanAngles = new Fix64[N];
      for (int i = 0; i < N; i++)
      {
        var angle = -1.0 + (2.0 / N) * i;
        atanAngles[i] = (Fix64)angle;
      }
    }

    [Benchmark]
    public void Sin()
    {
      foreach (var angle in sinAngles)
      {
        var actualF = Fix64.Sin(angle);
      }
    }

    [Benchmark]
    public void Atan()
    {
      foreach (var angle in atanAngles)
      {
        var actualF = Fix64.Atan(angle);
      }
    }
  }
  
  class Program
  {
    static void Main(string[] args)
    {
      var summary = BenchmarkRunner.Run<Trigonometry>();
    }
  }
}

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

using System;
using System.Text;
using System.IO;
using System.Runtime.CompilerServices;

namespace FixedMath.NET.Generators
{
  [Generator]
  public class LutGenerator : ISourceGenerator
  {
    private void BuildSinLut(GeneratorExecutionContext context)
    {
      var sourceBuilder = new StringBuilder(@"namespace FixedMath 
{
    partial struct Fix64 
    {
        public static readonly long[] SinLut = new[] 
        {");

      int lineCounter = 0;
      for (int i = 0; i < Fix64Constants.LUT_SIZE; ++i)
      {
        var angle = i * Math.PI * 0.5 / (Fix64Constants.LUT_SIZE - 1);
        if (lineCounter++ % 8 == 0)
        {
          sourceBuilder.AppendLine();
          sourceBuilder.Append("            ");
        }
        var sin = Math.Sin(angle);
        var rawValue = (long)(sin * Fix64Constants.ONE);
        sourceBuilder.Append(string.Format("0x{0:X}L, ", rawValue));
      }
      sourceBuilder.Append(
@"
        };
    }
}");

      // inject the created source into the users compilation
      context.AddSource("Fix64SinLut.g.cs", SourceText.From(sourceBuilder.ToString(), Encoding.UTF8));
    }

    private void BuildTanLut(GeneratorExecutionContext context)
    {
      var sourceBuilder = new StringBuilder(@"namespace FixedMath 
{
    partial struct Fix64 
    {
        public static readonly long[] TanLut = new[] 
        {");

      int lineCounter = 0;
      for (int i = 0; i < Fix64Constants.LUT_SIZE; ++i)
      {
        var angle = i * Math.PI * 0.5 / (Fix64Constants.LUT_SIZE - 1);
        if (lineCounter++ % 8 == 0)
        {
          sourceBuilder.AppendLine();
          sourceBuilder.Append("            ");
        }
        var tan = Math.Tan(angle);
        if (tan > (double)Fix64Constants.MAX_VALUE || tan < 0.0)
        {
          tan = (double)Fix64Constants.MAX_VALUE;
        }
        var rawValue = (((decimal)tan > (decimal)Fix64Constants.MAX_VALUE || tan < 0.0) ? Fix64Constants.MAX_VALUE : (long)(tan * Fix64Constants.ONE));
        sourceBuilder.Append(string.Format("0x{0:X}L, ", rawValue));
      }
      sourceBuilder.Append(
@"
        };
    }
}");

      // inject the created source into the users compilation
      context.AddSource("Fix64TanLut.g.cs", SourceText.From(sourceBuilder.ToString(), Encoding.UTF8));
    }

    public void Execute(GeneratorExecutionContext context)
    {
      BuildSinLut(context);
      BuildTanLut(context);
    }

    public void Initialize(GeneratorInitializationContext context)
    {
      // No initialization required for this one
    }
  }
}
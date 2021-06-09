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
      var sourceBuilder = new StringBuilder(@"namespace FixedMath.NET 
{
    partial struct Fix64 
    {
        public static readonly long[] SinLut = new[] 
        {");

      int lineCounter = 0;
      for (int i = 0; i < Constants.LUT_SIZE; ++i)
      {
        var angle = i * Math.PI * 0.5 / (Constants.LUT_SIZE - 1);
        if (lineCounter++ % 8 == 0)
        {
          sourceBuilder.AppendLine();
          sourceBuilder.Append("            ");
        }
        var sin = Math.Sin(angle);
        var rawValue = (long)(sin * Constants.ONE);
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
      var sourceBuilder = new StringBuilder(@"namespace FixedMath.NET 
{
    partial struct Fix64 
    {
        public static readonly long[] TanLut = new[] 
        {");

      int lineCounter = 0;
      for (int i = 0; i < Constants.LUT_SIZE; ++i)
      {
        var angle = i * Math.PI * 0.5 / (Constants.LUT_SIZE - 1);
        if (lineCounter++ % 8 == 0)
        {
          sourceBuilder.AppendLine();
          sourceBuilder.Append("            ");
        }
        var tan = Math.Tan(angle);
        if (tan > (double)Constants.MAX_VALUE || tan < 0.0)
        {
          tan = (double)Constants.MAX_VALUE;
        }
        var rawValue = (((decimal)tan > (decimal)Constants.MAX_VALUE || tan < 0.0) ? Constants.MAX_VALUE : (long)(tan * Constants.ONE));
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
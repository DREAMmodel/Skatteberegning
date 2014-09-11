using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Skatteberegning
{
  class Program
  {
    static void Main(string[] args)
    {
      Satser.SætÅr(2012); //foretag beregninger baseret på 2012-tal

      Console.WriteLine("Beregn skat:");

      int løn = 486530;
      int personalegode = 2600;
      int kapitalindkomst = -28500;
      int ligningsmæssigefradrag = 10800;      
      int skat = Skatteberegning.Skat(løn, personalegode, kapitalindkomst, ligningsmæssigefradrag);

      Console.WriteLine("Beregnet skat (indkomstskat + arbejdsmarkedsbidrag - grøncheck): " + skat + " Kr.");

      Console.ReadKey();
    }
  }
}
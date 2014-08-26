using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skatteberegning
{
  class Satser
  {

    static double[] _kirkeskat = { 0.7, 0.7, 0.7 };
    static double[] _kommuneskat = { 25,	24.9,	24.9 };
    static double[] _sundhedsbidrag = { 7,	6,	5 };
    static double[] _bundskat = { 4.64, 5.83, 6.83 };
    static double[] _topskat = { 15, 15, 15 };
    static int[] _topskattegrænse = { 389900, 421000, 449100 };

    static double[] _udligningsskat = { 15, 15, 15 };
    static double[] _skatteloftPersonligindkomst = { 51.5,	51.7,	51.7 };
    static double[] _skatteloftKapitalindkomst = { 45.5, 43.5, 42 };
    static double[] _arbejdsmarkedsbidrag = { 8, 8, 8 };

    static double[] _beskæftigelsesfradrag = { 4.40, 6.95, 7.65 };

    static int[] _beskæftigelsesfradragMaks = { 14100, 22300, 25000 };
    static int[] _personfradrag = {42900, 42000, 42800 };
    static int[] _bundfradragKapitalindkomstITopskat = { 40000, 40000, 40800 };
    static double[] _udligningsskattegrænse = { 362800, 362800, 369400 };
    static double[] _maksUudnyttetBundfradragMellemÆgtefæller = { 121000, 121000, 123200 };
    static double[] _progressionsgrænseAktieindkomstskat = { 48300, 48300, 49200};
    static int[] _grønCheckFyldt18 = { 1300, 1300, 1300 }; //Reguleres ikke
    static int[] _grønCheckU18 = { 300, 300, 300 }; //Reguleres ikke
    static int[] _aftrapningsgrænseGrønCheckTopskattegrundlag = { 362800, 362800, 369400 }; //Reguleres efter PSL Grundbeløb 362.800 kr. (2010)
    static double[] _aftrapningsprocentGrønCheck = { 7.5, 7.5, 7.5 };

    public static double Kommuneskat(int år, int kid = 0)
    {
      if (kid == 0)
        return _kommuneskat[år - 2012] / 100; //gennemsnitlig kommuneskat
      else
        return Kommuneskat(år); //bør finde skatteprocent for den pågældende kommune - ikke implementeret
    }

    public static double Kirkeskat(int år)
    {
      return _kirkeskat[år - 2012] / 100; //gennemsnitlig kirkeskat
    }

    public static double Sundhedsbidrag(int år)
    {
      return _sundhedsbidrag[år - 2012] / 100;
    }

    public static double Bundskat(int år)
    {
      return _bundskat[år - 2012] / 100;
    }

    public static int BundfradragKapitalindkomstITopskat(int år)
    {
      return _bundfradragKapitalindkomstITopskat[år - 2012];
    }

    public static double SkatteloftPersonligindkomst(int år)
    {
      return _skatteloftPersonligindkomst[år - 2012] / 100;
    }

    public static double Topskat(int år)
    {
      return _topskat[år - 2012] / 100;
    }

    public static int Topskattegrænse(int år)
    {
      return _topskattegrænse[år - 2012];
    }

    public static int GrønCheckFyldt18(int år)
    {
      return _grønCheckFyldt18[år - 2012];
    }

    public static int GrønCheckU18(int år)
    {
      return _grønCheckU18[år - 2012];
    }

    public static int AftrapningsgrænseGrønCheckTopskattegrundlag(int år)
    {
      return _aftrapningsgrænseGrønCheckTopskattegrundlag[år - 2012];
    }

    public static double AftrapningsprocentGrønCheck(int år)
    {
      return _aftrapningsprocentGrønCheck[år - 2012] / 100;
    }

    public static double Arbejdsmarkedsbidrag(int år)
    {
      return _arbejdsmarkedsbidrag[år - 2012] / 100;
    }

    public static double Beskæftigelsesfradrag(int år)
    {
      return _beskæftigelsesfradrag[år - 2012] / 100;

    }

    public static int BeskæftigelsesfradragMaks(int år)
    {
      return _beskæftigelsesfradragMaks[år - 2012];
    }

    public static int Personfradrag(int år)
    {
      return _personfradrag[år - 2012];
    }
  }
}

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
    static double[] _udligningsskat = { 15, 15, 15 };
    static double[] _skatteloftPersonligindkomst = { 51.5,	51.7,	51.7 };
    static double[] _skatteloftKapitalindkomst = { 45.5, 43.5, 42 };
    static double[] _arbejdsmarkedsbidrag = { 8, 8, 8 };

    static double[] _beskæftigelsesfradrag = {4.40,6.95,7.65};
    static double[] _aftrapningsprocentGrønCheck = {7.5,7.5,7.5};
    static double[] _beskæftigelsesfradragMaks = {14.1,22.3,25};
    static double[] _personfradrag = {42.9,42,42.8};
    static double[] _topskattegrænse = {389.9,421,449.1};
    static double[] _bundfradragKapitalindkomstITopskat = {40,40,40.8};
    static double[] _udligningsskattegrænse = {362.8,362.8,369.4};
    static double[] _maksUudnyttetBundfradragMellemÆgtefæller = {121,121,123.2};
    static double[] _progressionsgrænseAktieindkomstskat = {48.3,48.3,49.2};
    static double[] _grønCheckFyldt18 = {1.3,1.3,1.3};
    static double[] _aftrapningsgrænseGrønCheckTopskattegrundlag = {362.8,362.8,369.4};


    public static double Kommuneskat(int år, int kid = 0)
    {
      if (kid == 0)
        return _kommuneskat[år - 2012]; //gennemsnitlig kommuneskat
      else
        return Kommuneskat(år); //bør finde skatteprocent for den pågældende kommune - ikke implementeret
    }

    public static double Kirkeskat(int år)
    {
      return _kirkeskat[år - 2012]; //gennemsnitlig kirkeskat
    }



  }
}

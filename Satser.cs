using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skatteberegning
{
  class Satser
  {
    const int FØRSTEDATAÅR = 2012;
    const int HISTORISKEÅR = 3;

    static int _år = 0; //0 svarer til 2012

    //Data
    static double[] _kirkeskatData = { .00731, .00723, .00723 }; //Reguleres ikke
    static double _kirkeskat = _kirkeskatData[0]; //gennemsnit for alle (også ikke-betalere), 

    static double[] _kommuneskatData = { .2592, .24907, .24907 }; //reguleres ikke, kilde: Familietypemodelen (16012014)
    static double _kommuneskat = _kommuneskatData[0]; //gennemsnit

    static double[] _sundhedsbidragData = { .07, .06, .05 }; //Sundhedsbidraget er på 5,0% (2014-sats). Fra 2013 til 2019 sænkes sundhedsbidraget hvert år med 1 pct.point, samtidig med at bundskatten hæves med 1 pct.point. I 2019 er sundhedsskatten således helt forsvundet.
    static double _sundhedsbidrag = _sundhedsbidragData[0];

    static double[] _bundskatData = { .0464, .0583, .0683 }; //Se sundhedsbidrag
    static double _bundskat = _bundskatData[0];

    static double _arbejdsmarkedsbidrag = 8; //Reguleres ikke

    static double _topskat = 15; //Reguleres ikke
    static int[] _topskattegrænseData = { 389900, 421000, 449100 }; //PL-reguleres - korrekt??
    static int _topskattegrænse = _topskattegrænseData[0];

    static double[] _skatteloftPersonligindkomstData = { .515, .517, .517 }; //Reguleres ikke
    static double _skatteloftPersonligindkomst = _skatteloftPersonligindkomstData[0];

    static double[] _skatteloftKapitalindkomstData = { .455, .435, .42 }; //fremskrives ikke
    static double _skatteloftKapitalindkomst = _skatteloftKapitalindkomstData[0];

    static double[] _beskæftigelsesfradragData = { .0440, .0695, .0765 }; //fremskrivning???
    static double _beskæftigelsesfradrag = _beskæftigelsesfradragData[0];
    static int[] _beskæftigelsesfradragMaksData = { 14100, 22300, 25000 }; //PL-reguleres
    static int _beskæftigelsesfradragMaks = _beskæftigelsesfradragMaksData[0];

    static int[] _personfradragData = { 42900, 42000, 42800 }; //PL-reguleres
    static int _personfradrag = _personfradragData[0];

    static int[] _bundfradragKapitalindkomstITopskatData = { 40000, 40000, 40800 }; //PL-reguleres
    static int _bundfradragKapitalindkomstITopskat = _bundfradragKapitalindkomstITopskatData[0];

    static double _udligningsskat = 0.06; //reguleres ikke
    static int[] _udligningsskattegrænseData = { 362800, 362800, 369400 }; //PL-reguleres
    static int _udligningsskattegrænse = _udligningsskattegrænseData[0];

    static int[] _maksUudnyttetBundfradragMellemÆgtefællerData = { 121000, 121000, 123200 }; //PL-reguleres
    static int _maksUudnyttetBundfradragMellemÆgtefæller = _maksUudnyttetBundfradragMellemÆgtefællerData[0];

    static int[] _progressionsgrænseAktieindkomstskatData = { 48300, 48300, 49200 }; //PL-reguleres
    static int _progressionsgrænseAktieindkomstskat = _progressionsgrænseAktieindkomstskatData[0];

    static int[] _aftrapningsgrænseGrønCheckTopskattegrundlagData = { 362800, 362800, 369400 }; //Reguleres efter PSL Grundbeløb 362.800 kr. (2010)
    static int _aftrapningsgrænseGrønCheckTopskattegrundlag = _aftrapningsgrænseGrønCheckTopskattegrundlagData[0];
    static double _aftrapningsprocentGrønCheck = .075; //Reguleres ikke
    static int _grønCheckFyldt18 = 1300; //Reguleres ikke
    static int _grønCheckU18 = 300; //Reguleres ikke

    public static void SætÅr(int år)
    {
      år -= FØRSTEDATAÅR;

      if (år < 0)
        throw new System.ArgumentException("År kan ikke være mindre end " + FØRSTEDATAÅR, "år");
      else if (_år == år)
        return;
      else if (år - FØRSTEDATAÅR < HISTORISKEÅR)
      {
        //opdater satser med historiske værdier...
        _kirkeskat = _kirkeskatData[år]; //gennemsnit for alle (også ikke-betalere), 
        _kommuneskat = _kommuneskatData[år]; //gennemsnit
        _sundhedsbidrag = _sundhedsbidragData[år];
        _bundskat = _bundskatData[år];
        _topskattegrænse = _topskattegrænseData[år];
        _skatteloftPersonligindkomst = _skatteloftPersonligindkomstData[år];
        _skatteloftKapitalindkomst = _skatteloftKapitalindkomstData[år];
        _beskæftigelsesfradrag = _beskæftigelsesfradragData[år];
        _beskæftigelsesfradragMaks = _beskæftigelsesfradragMaksData[år];
        _personfradrag = _personfradragData[år];
        _bundfradragKapitalindkomstITopskat = _bundfradragKapitalindkomstITopskatData[år];
        _udligningsskattegrænse = _udligningsskattegrænseData[år];
        _maksUudnyttetBundfradragMellemÆgtefæller = _maksUudnyttetBundfradragMellemÆgtefællerData[år];
        _progressionsgrænseAktieindkomstskat = _progressionsgrænseAktieindkomstskatData[år];
        _aftrapningsgrænseGrønCheckTopskattegrundlag = _aftrapningsgrænseGrønCheckTopskattegrundlagData[år];
      }
      else
      {
        if (_år < FØRSTEDATAÅR + HISTORISKEÅR || år < _år) //vi har nyere data at tage udgangspunkt i eller vi må fremskriv forfra (året ændres tilbage)
          SætÅr(FØRSTEDATAÅR + HISTORISKEÅR - 1);

        while (NæsteÅr() < år)
          ; //fremskriv indtil år nås
      }

      _år = år;
    }

    public static int NæsteÅr()
    {
      double pl = 1.036; //bestem reguleringsfaktor pr år

      //opdater satser...
      if (_sundhedsbidrag >= 1)
      {
        _sundhedsbidrag--; //Sundhedsbidraget er på 5,0% (2014-sats). Fra 2013 til 2019 sænkes sundhedsbidraget hvert år med 1 pct.point, samtidig med at bundskatten hæves med 1 pct.point. I 2019 er sundhedsskatten således helt forsvundet.        
        _bundskat++;
      }

      //PL-regulering
      _topskattegrænse = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(_topskattegrænse) * pl / 100) * 100); //PSL-20 reguleres, rundes op til nærmeste 100
      _personfradrag = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(_personfradrag) * pl / 100) * 100); ; //PSL-20 reguleres, rundes op til nærmeste 100
      _bundfradragKapitalindkomstITopskat = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(_personfradrag) * pl / 100) * 100); //PSL-20 reguleres, rundes op til nærmeste 100
      _udligningsskattegrænse = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(_udligningsskattegrænse * pl / 100) * 100); //PSL-20 reguleres, rundes op til nærmeste 100
      _maksUudnyttetBundfradragMellemÆgtefæller = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(_maksUudnyttetBundfradragMellemÆgtefæller) * pl / 100) * 100); //PSL-20 reguleres, rundes op til nærmeste 100
      _progressionsgrænseAktieindkomstskat = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(_progressionsgrænseAktieindkomstskat) * pl / 100) * 100); //PSL-20 reguleres, rundes op til nærmeste 100

      return _år++;
    }

    public static double Kommuneskat(int kid = 0)
    {
      return kid == 0 ? _kommuneskat : Kommuneskat(); //gennemsnitlig kommuneskat

      //bør kunne finde skatteprocent for den pågældende kommune - ikke implementeret
    }

    public static double Kirkeskat()
    {
      return _kirkeskat; //gennemsnitlig kirkeskat
    }

    public static double Sundhedsbidrag()
    {
      return _sundhedsbidrag;
    }

    public static double Bundskat()
    {
      return _bundskat;
    }

    public static int BundfradragKapitalindkomstITopskat()
    {
      return _bundfradragKapitalindkomstITopskat;
    }

    public static double SkatteloftPersonligindkomst()
    {
      return _skatteloftPersonligindkomst;
    }

    public static double Topskat()
    {
      return _topskat;
    }

    public static int Topskattegrænse()
    {
      return _topskattegrænse;
    }

    public static int GrønCheckFyldt18()
    {
      return _grønCheckFyldt18;
    }

    public static int GrønCheckU18()
    {
      return _grønCheckU18;
    }

    public static int AftrapningsgrænseGrønCheckTopskattegrundlag()
    {
      return _aftrapningsgrænseGrønCheckTopskattegrundlag;
    }

    public static double AftrapningsprocentGrønCheck()
    {
      return _aftrapningsprocentGrønCheck;
    }

    public static double Arbejdsmarkedsbidrag()
    {
      return _arbejdsmarkedsbidrag;
    }

    public static double Beskæftigelsesfradrag()
    {
      return _beskæftigelsesfradrag;

    }

    public static int BeskæftigelsesfradragMaks()
    {
      return _beskæftigelsesfradragMaks;
    }

    public static int Personfradrag()
    {
      return _personfradrag;
    }

  }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skatteberegning
{
  class Skatteberegning
  {
    //Kilde: http://www.skm.dk/skattetal/beregning/skatteberegning/skatteberegning-hovedtraekkene-i-personbeskatningen-2014/

    public static int Skat(int år, int lønindkomst, int personalegoder, int kapitalindkomst, int ligningsmæssigeFradrag)
    {
      int lønindkomstPersonalegoder = lønindkomst + personalegoder;
      int arbejdsmarkedsbidrag = Arbejdsmarkedsbidrag(år, lønindkomstPersonalegoder);

      int personligIndkomst = lønindkomst + personalegoder - arbejdsmarkedsbidrag;

      int beskæftigelsesfradrag = Beskæftigelsesfradrag(år, lønindkomstPersonalegoder);

      int skattepligtigindkomst = personligIndkomst + kapitalindkomst - ligningsmæssigeFradrag - beskæftigelsesfradrag;

      int tmp = Bundskat(år, personligIndkomst, kapitalindkomst);
      int tmp2 = Topskat(år, personligIndkomst, kapitalindkomst);
      int indkomstskat = Kirkeskat(år, skattepligtigindkomst) + Kommuneskat(år, skattepligtigindkomst) + Sundhedsbidrag(år, skattepligtigindkomst) + Bundskat(år, personligIndkomst, kapitalindkomst) + Topskat(år, personligIndkomst, kapitalindkomst);
      int grøncheck = GrønCheck(år, 20, personligIndkomst);

      return indkomstskat + arbejdsmarkedsbidrag - grøncheck;
    }

    static int Arbejdsmarkedsbidrag(int år, int lønindkomstPersonalegoder)
    {
      return Convert.ToInt32(Satser.Arbejdsmarkedsbidrag(år) * lønindkomstPersonalegoder);
    }

    static int Beskæftigelsesfradrag(int år, int lønindkomstPersonalegoder)
    {
      return Convert.ToInt32(Math.Min(Satser.BeskæftigelsesfradragMaks(år), Satser.Beskæftigelsesfradrag(år) * lønindkomstPersonalegoder));

    }

    static int Kommuneskat(int år, int skattepligtigIndkomst)
    {
      return Math.Max(0, Convert.ToInt32((skattepligtigIndkomst - Satser.Personfradrag(år)) * Satser.Kommuneskat(år)));
    }

    static int Kirkeskat(int år, int skattepligtigIndkomst)
    {
      return Math.Max(0, Convert.ToInt32((skattepligtigIndkomst - Satser.Personfradrag(år)) * Satser.Kirkeskat(år)));
    }

    static int Sundhedsbidrag(int år, int skattepligtigIndkomst)
    {
      return Math.Max(0, Convert.ToInt32((skattepligtigIndkomst - Satser.Personfradrag(år)) * Satser.Sundhedsbidrag(år)));
    }

    static int Bundskat(int år, int personligIndkomst, int nettoKapitalindkomst = 0)
    {
      int positivNettoKapitalindkomst = Math.Max(0, nettoKapitalindkomst);
      return Math.Max(0, Convert.ToInt32((personligIndkomst + positivNettoKapitalindkomst - Satser.Personfradrag(år)) * Satser.Bundskat(år)));
    }

    static int Topskat(int år, int personligIndkomst, int positivNettoKapitalindkomst = 0)
    {
      int evtPositivNettoKapitalindkomst = Math.Max(0, positivNettoKapitalindkomst - Satser.BundfradragKapitalindkomstITopskat(år)); //fratrukket bundfradrag

      //skatteloftgrænse
      double overskridelseAfSkatteloft = Math.Max(0, Satser.Kommuneskat(år) + Satser.Sundhedsbidrag(år) + Satser.Bundskat(år) + Satser.Topskat(år) - Satser.SkatteloftPersonligindkomst(år)); //Skatteloftet vedrørende personlig indkomst er på 51,7 pct., og nedslaget i topskatteprocenten udgør den del af summen af skatteprocenterne til kommunen og staten, der samlet overstiger 51,7 pct. Arbejdsmarkedsbidrag og kirkeskat er ikke omfattet af skatteloftet.
      double topskat = Satser.Topskat(år) - overskridelseAfSkatteloft;

      return Math.Max(0, Convert.ToInt32((personligIndkomst + evtPositivNettoKapitalindkomst - Satser.Topskattegrænse(år)) * topskat));
    }

    //Udligningsskat

    //Aktie-indkomstskat

    //Kompensation for lavere værdi af rentefradrag

    /*Kompensation for lavere værdi af rentefradrag:
3 pct. af negativ nettokapitalindkomst op til 50.000 kr.*/


    static int GrønCheck(int år, int alder, int personligindkomst, int børn = 0)
    {
      //Tillæg til grøn check?

      int førAftrapning = alder > 18 ? Satser.GrønCheckFyldt18(år) : Satser.GrønCheckU18(år);

      if (børn > 0)
        førAftrapning += Math.Min(2, børn) * Satser.GrønCheckU18(år); //tillæg pr barn under 18 (tilfalder normalt moderen)

      Math.Min(0, Math.Max(1300, (Satser.AftrapningsprocentGrønCheck(år) - Satser.AftrapningsgrænseGrønCheckTopskattegrundlag(år)) * personligindkomst));

      return 0;
    }
    
  }
}
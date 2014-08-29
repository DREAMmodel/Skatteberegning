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

    public static int Skat(int lønindkomst, int personalegoder, int kapitalindkomst, int ligningsmæssigeFradrag)
    {
      int lønindkomstPersonalegoder = lønindkomst + personalegoder;
      int arbejdsmarkedsbidrag = Arbejdsmarkedsbidrag(lønindkomstPersonalegoder);

      int personligIndkomst = lønindkomst + personalegoder - arbejdsmarkedsbidrag;

      int beskæftigelsesfradrag = Beskæftigelsesfradrag(lønindkomstPersonalegoder);

      int skattepligtigindkomst = personligIndkomst + kapitalindkomst - ligningsmæssigeFradrag - beskæftigelsesfradrag;

      int tmp = Bundskat(personligIndkomst, kapitalindkomst);
      int tmp2 = Topskat(personligIndkomst, kapitalindkomst);
      int indkomstskat = Kirkeskat(skattepligtigindkomst) + Kommuneskat(skattepligtigindkomst) + Sundhedsbidrag(skattepligtigindkomst) + Bundskat(personligIndkomst, kapitalindkomst) + Topskat(personligIndkomst, kapitalindkomst);
      int grøncheck = GrønCheck(20, personligIndkomst);

      return indkomstskat + arbejdsmarkedsbidrag - grøncheck;
    }

    static int Arbejdsmarkedsbidrag(int lønindkomstPersonalegoder)
    {
      return Convert.ToInt32(Satser.Arbejdsmarkedsbidrag() * lønindkomstPersonalegoder);
    }

    static int Beskæftigelsesfradrag(int lønindkomstPersonalegoder)
    {
      return Convert.ToInt32(Math.Min(Satser.BeskæftigelsesfradragMaks(), Satser.Beskæftigelsesfradrag() * lønindkomstPersonalegoder));

    }

    static int Kommuneskat(int skattepligtigIndkomst)
    {
      return Math.Max(0, Convert.ToInt32((skattepligtigIndkomst - Satser.Personfradrag()) * Satser.Kommuneskat()));
    }

    static int Kirkeskat(int skattepligtigIndkomst)
    {
      return Math.Max(0, Convert.ToInt32((skattepligtigIndkomst - Satser.Personfradrag()) * Satser.Kirkeskat()));
    }

    static int Sundhedsbidrag(int skattepligtigIndkomst)
    {
      return Math.Max(0, Convert.ToInt32((skattepligtigIndkomst - Satser.Personfradrag()) * Satser.Sundhedsbidrag()));
    }

    static int Bundskat(int personligIndkomst, int nettoKapitalindkomst = 0)
    {
      int positivNettoKapitalindkomst = Math.Max(0, nettoKapitalindkomst);
      return Math.Max(0, Convert.ToInt32((personligIndkomst + positivNettoKapitalindkomst - Satser.Personfradrag()) * Satser.Bundskat()));
    }

    static int Topskat(int personligIndkomst, int positivNettoKapitalindkomst = 0)
    {
      int evtPositivNettoKapitalindkomst = Math.Max(0, positivNettoKapitalindkomst - Satser.BundfradragKapitalindkomstITopskat()); //fratrukket bundfradrag

      //skatteloftgrænse
      double overskridelseAfSkatteloft = Math.Max(0, Satser.Kommuneskat() + Satser.Sundhedsbidrag() + Satser.Bundskat() + Satser.Topskat() - Satser.SkatteloftPersonligindkomst()); //Skatteloftet vedrørende personlig indkomst er på 51,7 pct., og nedslaget i topskatteprocenten udgør den del af summen af skatteprocenterne til kommunen og staten, der samlet overstiger 51,7 pct. Arbejdsmarkedsbidrag og kirkeskat er ikke omfattet af skatteloftet.
      double topskat = Satser.Topskat() - overskridelseAfSkatteloft;

      return Math.Max(0, Convert.ToInt32((personligIndkomst + evtPositivNettoKapitalindkomst - Satser.Topskattegrænse()) * topskat));
    }

    //Udligningsskat

    //Aktie-indkomstskat

    //Kompensation for lavere værdi af rentefradrag

    /*Kompensation for lavere værdi af rentefradrag:
3 pct. af negativ nettokapitalindkomst op til 50.000 kr.*/


    static int GrønCheck(int alder, int personligindkomst, int børn = 0)
    {
      //Tillæg til grøn check?

      int førAftrapning = alder > 18 ? Satser.GrønCheckFyldt18() : Satser.GrønCheckU18();

      if (børn > 0)
        førAftrapning += Math.Min(2, børn) * Satser.GrønCheckU18(); //tillæg pr barn under 18 (tilfalder normalt moderen)

      Math.Min(0, Math.Max(1300, (Satser.AftrapningsprocentGrønCheck() - Satser.AftrapningsgrænseGrønCheckTopskattegrundlag()) * personligindkomst));

      return 0;
    }
    
  }
}
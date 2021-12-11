namespace _2021.Day3;

internal sealed record Day3(IList<LigneRapport> LignesRapport)
{
    public int LongueurLigne => LignesRapport[0].Bits.Count;

    public int GetTauxGamma(IList<LigneRapport>? lignesRapport = null)
    {
        return GetTauxPart1(lignesRapport ?? LignesRapport, GetBitLePlusUtiliseALaColonne);
    }

    public int GetTauxEpsilon(IList<LigneRapport>? lignesRapport = null)
    {
        return GetTauxPart1(lignesRapport ?? LignesRapport, GetBitLeMoinsUtiliseALaColonne);
    }

    private int GetTauxPart1(
        IList<LigneRapport> lignesRapport,
        Func<IList<string>, string> getBitLePlusOuLeMoinsUtiliseDuneColonne)
    {
        var resultat = string.Empty;
        for (int i = 0; i < LongueurLigne; i++)
        {
            var bitsDeLaColonne = lignesRapport
                .Select(l => l.Bits[i])
                .ToList();
            resultat += getBitLePlusOuLeMoinsUtiliseDuneColonne(bitsDeLaColonne);
        }

        return Convert.ToInt32(resultat, 2);
    }

    public int GetConsommationElectrique()
    {
        return this.GetTauxGamma(LignesRapport) * this.GetTauxEpsilon(LignesRapport);
    }

    public int GetTauxOxygeneGeneree()
    {
        return GetTauxPart2(LignesRapport, GetBitLePlusUtiliseALaColonne);
    }

    public int GetTauxCoDeuxEpure()
    {
        return GetTauxPart2(LignesRapport, GetBitLeMoinsUtiliseALaColonne);
    }

    private int GetTauxPart2(
        IList<LigneRapport> lignesRapport,
        Func<IList<string>, string> getBitLePlusOuLeMoinsUtiliseDuneColonne)
    {
        var resultats = lignesRapport.ToList();
        while (resultats.Count > 1)
        {
            for (int i = 0; i < LongueurLigne; i++)
            {
                var bitsDeLaColonne = resultats
                    .Select(l => l.Bits[i])
                    .ToList();
                var bitLePlusUtiliseALaColonne = getBitLePlusOuLeMoinsUtiliseDuneColonne(bitsDeLaColonne);
                resultats.RemoveAll(ligne => !ligne.Bits[i].Equals(bitLePlusUtiliseALaColonne));
            }
        }
        return Convert.ToInt32(resultats.Single().ToString(), 2);
    }

    public int GetTauxDeSurvie()
    {
        return this.GetTauxOxygeneGeneree() * this.GetTauxCoDeuxEpure();
    }

    private string GetBitLePlusUtiliseALaColonne(IList<string> bitsDeLaColonne)
    {
        return GetBitLeMoinsOuLePlusUtiliseDansUneColonne(bitsDeLaColonne, false);
    }

    private string GetBitLeMoinsUtiliseALaColonne(IList<string> bitsDeLaColonne)
    {
        return GetBitLeMoinsOuLePlusUtiliseDansUneColonne(bitsDeLaColonne, true);
    }

    private string GetBitLeMoinsOuLePlusUtiliseDansUneColonne(IList<string> bitsDeLaColonne, bool leMoinsUtilise)
    {
        var lignesParNbOccurence = bitsDeLaColonne
            .GroupBy(c => c)
            .Select(c => new { Bit = c.Key, NbOccurence = c.Count() })
            .ToList();

        int nbOccurences;
        string bitParDefautEnCasDEgalite;
        if (leMoinsUtilise)
        {
            nbOccurences = lignesParNbOccurence.Min(c => c.NbOccurence);
            bitParDefautEnCasDEgalite = "0";
        }
        else
        {
            nbOccurences = lignesParNbOccurence.Max(c => c.NbOccurence);
            bitParDefautEnCasDEgalite = "1";
        }

        return lignesParNbOccurence.Count(l => l.NbOccurence == nbOccurences) > 1
            ? bitParDefautEnCasDEgalite
            : lignesParNbOccurence
                .Single(l => l.NbOccurence == nbOccurences).Bit;
    }
}

internal record LigneRapport(IList<string> Bits)
{
    public override string ToString() => string.Join(null, Bits);
}
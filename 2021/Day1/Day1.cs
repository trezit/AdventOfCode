namespace _2021.Day1;

internal record Day1(int[] Profondeurs)
{
    public int GetNombreDeDescentesEntreDeuxProfondeurs()
    {
        var nombreDeDescentes = 0;
        for (var i = 0; i < Profondeurs.Length - 1; i++)
            if (Profondeurs[i + 1] - Profondeurs[i] > 0)
                nombreDeDescentes += 1;
        return nombreDeDescentes;
    }

    public int GetNombreDeSommesDe3ProfondeursSuperieuresALaSommePrecedente()
    {
        var nombreDeDescentes = 0;
        for (var i = 1; i < Profondeurs.Length - 2; i++)
        {
            var sommeProfondeur1A3 = Profondeurs[i - 1] + Profondeurs[i] + Profondeurs[i + 1];
            var sommeProfondeur2A4 = Profondeurs[i] + Profondeurs[i + 1] + Profondeurs[i + 2];
            if (sommeProfondeur2A4 - sommeProfondeur1A3 > 0)
                nombreDeDescentes += 1;
        }
        return nombreDeDescentes;
    }
}
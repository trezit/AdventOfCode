using System.Collections.Generic;

namespace _2021.Day4;

internal sealed record Day4
{
    public Day4(string inputFilePath)
    {
        var lignes = File.ReadAllLines(inputFilePath);
        var numerosTires = lignes[0].Split(',');
        for (int i = 0; i < numerosTires.Length; i++)
        {
            NumerosTires.Enqueue(new NumeroTire(int.Parse(numerosTires[i]), i));
        }
        for (int i = 2; i < lignes.Length; i = i + 6)
        {
            Plateaux.Add(new Plateau(new[]
            {
                lignes[i],
                lignes[i+1],
                lignes[i+2],
                lignes[i+3],
                lignes[i+4],
            }));
        }
    }

    public Queue<NumeroTire> NumerosTires { get; init; } = new();
    public List<Plateau> Plateaux { get; init; } = new();
    public List<Plateau> PlateauxGagnants => Plateaux.Where(p => p.EstPlateauGagnant(out _)).ToList();

    public int? DeroulerJeuPart1()
    {
        NumeroTire? numeroTire;
        do
        {
            numeroTire = NumerosTires.Dequeue();
            foreach (var plateau in Plateaux)
            {
                plateau.AppliquerNumeroTire(numeroTire);
            }
        } while (!this.PlateauxGagnants.Any() && this.NumerosTires.Any());

        return this.PlateauxGagnants.Any() ? numeroTire.Valeur : null;
    }
    public int? DeroulerJeuPart2()
    {
        NumeroTire? numeroTire;
        do
        {
            numeroTire = NumerosTires.Dequeue();
            foreach (var plateau in Plateaux.Where(p => !p.EstPlateauGagnant(out _)))
            {
                plateau.AppliquerNumeroTire(numeroTire);
            }
        } while (this.PlateauxGagnants.Count != this.Plateaux.Count && this.NumerosTires.Any());

        return this.PlateauxGagnants.Count == this.Plateaux.Count ? numeroTire.Valeur : null;
    }

    public void TirerProchainsNumeros(int nbreNumerosTires)
    {
        for (int i = 0; i < nbreNumerosTires; i++)
        {
            var numeroTire = NumerosTires.Dequeue();
            foreach (var plateau in Plateaux)
            {
                plateau.AppliquerNumeroTire(numeroTire);
            }
        }
    }
}

internal record NumeroTire(int Valeur, int NumeroTour);

internal record Plateau
{
    private bool _estPlateauGagnant = false;
    public Plateau(string[] lignes)
    {
        for (int i = 0; i < lignes.Length; i++)
        {
            var ligne = lignes[i];
            var numerosLigne1 = ligne.Split(" ").Where(l => !string.IsNullOrEmpty(l)).Select(int.Parse).ToList();
            NumerosPlateau.AddRange(numerosLigne1.Select((valeur, j) => new NumeroPlateau(valeur, i, j)));
        }
    }

    public NumeroTire? NumeroTireQuiFaitGagner { get; private set; } = null;

    public bool EstPlateauGagnant(out List<NumeroPlateau> numerosGagnant)
    {
        if (!_estPlateauGagnant)
            return TryGetLigneGagnante(out numerosGagnant) || TryGetColonneGagnante(out numerosGagnant);
        numerosGagnant = new();
        return true;
    }

    public bool TryGetLigneGagnante(out List<NumeroPlateau> ligneGagnante)
    {
        ligneGagnante = new();
        for (int indexLigne = 0; indexLigne < 5; indexLigne++)
        {
            var numerosDeLaLigne = NumerosPlateau.Where(np => np.X == indexLigne).ToList();
            if (numerosDeLaLigne.All(np => np.Tire))
            {
                ligneGagnante.AddRange(numerosDeLaLigne);
                return true;
            }
        }
        return false;
    }

    public bool TryGetColonneGagnante(out List<NumeroPlateau> colonneGagnante)
    {
        colonneGagnante = new();
        for (int indexColonne = 0; indexColonne < 5; indexColonne++)
        {
            var numerosDeLaColonne = NumerosPlateau.Where(np => np.Y == indexColonne).ToList();
            if (numerosDeLaColonne.All(np => np.Tire))
            {
                colonneGagnante.AddRange(numerosDeLaColonne);
                return true;
            }
        }
        return false;
    }

    public List<NumeroPlateau> NumerosPlateau { get; init; } = new();

    public NumeroPlateau GetNumeroPlateau(int x, int y)
    {
        return NumerosPlateau.Single(np => np.X.Equals(x) && np.Y.Equals(y));
    }

    public void AppliquerNumeroTire(NumeroTire numeroTire)
    {
        var numeroExiste = NumerosPlateau.FirstOrDefault(np => np.Valeur == numeroTire.Valeur);
        if (numeroExiste != null)
        {
            numeroExiste.MarqueCommeTire();
            if (EstPlateauGagnant(out _))
                NumeroTireQuiFaitGagner = numeroTire;
        }
    }
}

internal record NumeroPlateau(int Valeur, int X, int Y)
{
    public bool Tire { get; private set; }

    public void MarqueCommeTire()
    {
        Tire = true;
    }
}
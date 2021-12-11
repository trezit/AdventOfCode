namespace _2021.Day2;

internal sealed record Day2(IList<Commande> Commandes)
{
    private readonly Position _pointDepart = new(0, 0);

    public int Viseur { get; private set; }

    public Position ExecuterPart1()
    {
        return TraiterCommandes(TraiterCommandesPart1);
    }

    public Position ExecuterPart2()
    {
        return TraiterCommandes(TraiterCommandesPart2);
    }

    private Position TraiterCommandes(Func<Position, Commande, Position> traiterCommande)
    {
        return Commandes.Aggregate(_pointDepart, traiterCommande);
    }

    private Position TraiterCommandesPart1(Position position, Commande commande)
    {
        var (positionHorizontale, profondeur) = position;
        var (typeCommande, valeur) = commande;
        return typeCommande switch
        {
            TypeCommande.forward => new Position(positionHorizontale + valeur, profondeur),
            TypeCommande.down => new Position(positionHorizontale, profondeur + valeur),
            TypeCommande.up => new Position(positionHorizontale, profondeur - valeur),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private Position TraiterCommandesPart2(Position position, Commande commande)
    {
        var (typeCommande, valeur) = commande;
        switch (typeCommande)
        {
            case TypeCommande.forward:
                var nouvellePositionHorizontale = position.PositionHorizontale + valeur;
                var nouvelleProfondeur = position.Profondeur + (Viseur * valeur);
                return new Position(nouvellePositionHorizontale, nouvelleProfondeur);
            case TypeCommande.down:
                Viseur += valeur;
                return position;
            case TypeCommande.up:
                Viseur -= valeur;
                return position;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
internal enum TypeCommande { forward, down, up, }

internal record Commande(TypeCommande TypeCommande, int Valeur)
{
    public Commande(string ligne) : this(
        Enum.Parse<TypeCommande>(ligne.Split(' ')[0]),
        int.Parse(ligne.Split(' ')[1])
        )
    {
    }
    public override string ToString() => $"{TypeCommande} {Valeur}";
}

internal record Position(int PositionHorizontale, int Profondeur)
{
    public override string ToString() => $"{PositionHorizontale},{Profondeur}";
    public int GetProduitCoordonnees() => PositionHorizontale * Profondeur;
}
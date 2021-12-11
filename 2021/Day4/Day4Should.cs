using Xunit.Abstractions;

namespace _2021.Day4
{
    public class Day4Should
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public Day4Should(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        private const string Day4InputFile = "Day4\\Day4Input.txt";
        private const string Day4ExampleFile = "Day4\\Day4Example.txt";

        #region Part1

        [Fact]
        public void IlExiste3PlateauxDansLExemple()
        {
            var bingo = new Day4(Day4ExampleFile);
            bingo.Plateaux.Count.Should().Be(3);
            _testOutputHelper.WriteLine($"Le nombre de plateau est de : {bingo.Plateaux.Count}");
        }

        [Fact]
        public void ChaquePlateauAVingtCinqNumerosPlateau()
        {
            var bingo = new Day4(Day4ExampleFile);
            bingo.Plateaux.Count.Should().Be(3);

            foreach (var plateau in bingo.Plateaux)
            {
                plateau.NumerosPlateau.Count.Should().Be(25);
            }
            _testOutputHelper.WriteLine($"Chaque plateau a 25 numéros plateau.");
        }

        [Fact]
        public void ApresCinqNumerosTiresLesPlateauxSont()
        {
            var bingo = new Day4(Day4ExampleFile);
            bingo.TirerProchainsNumeros(5);

            var coordonneesNumerosTiresSurLePlateauUn = new[]
            {
                (0, 3),
                (1, 3),
                (2, 1),
                (2, 4),
                (3, 4)
            };
            var plateauUn = bingo.Plateaux[0];
            foreach (var (x, y) in coordonneesNumerosTiresSurLePlateauUn)
            {
                var numeroPlateau = plateauUn.GetNumeroPlateau(x, y);
                numeroPlateau.Tire.Should().BeTrue();
                plateauUn.NumerosPlateau.Count(np => np.Tire).Should().Be(5);
                _testOutputHelper.WriteLine($"Le numéro {numeroPlateau.Valeur} du plateau un a été tiré.");
            }

            var coordonneesNumerosTiresSurLePlateauDeux = new[]
            {
                (1, 0),
                (1, 4),
                (2, 2),
                (3, 1),
                (3, 4),
            };
            var plateauDeux = bingo.Plateaux[1];
            foreach (var (x, y) in coordonneesNumerosTiresSurLePlateauDeux)
            {
                var numeroPlateau = plateauDeux.GetNumeroPlateau(x, y);
                numeroPlateau.Tire.Should().BeTrue();
                plateauDeux.NumerosPlateau.Count(np => np.Tire).Should().Be(5);
                _testOutputHelper.WriteLine($"Le numéro {numeroPlateau.Valeur} du plateau deux a été tiré.");
            }

            var coordonneesNumerosTiresSurLePlateauTrois = new[]
            {
                (0, 4),
                (1, 3),
                (3, 1),
                (3, 4),
                (4, 4)
            };
            var plateauTrois = bingo.Plateaux[2];
            foreach (var (x, y) in coordonneesNumerosTiresSurLePlateauTrois)
            {
                var numeroPlateau = plateauTrois.GetNumeroPlateau(x, y);
                numeroPlateau.Tire.Should().BeTrue();
                plateauTrois.NumerosPlateau.Count(np => np.Tire).Should().Be(5);
                _testOutputHelper.WriteLine($"Le numéro {numeroPlateau.Valeur} du plateau trois a été tiré.");
            }
        }

        [Fact]
        public void ApresDouzeNumerosTiresLeSeulPlateauGagnatEstLeTroisAvecUnePremiereLigneCompleteEtSonScoreEst4512()
        {
            var bingo = new Day4(Day4ExampleFile);
            var numeroTireGagnant = bingo.DeroulerJeuPart1();
            var seulPlateauGagnant = bingo.PlateauxGagnants.Single();
            seulPlateauGagnant.Should().Be(bingo.Plateaux[2]);
            seulPlateauGagnant
                .NumerosPlateau
                .Where(np => np.X == 0)
                .All(np => np.Tire)
                .Should()
                .BeTrue();

            seulPlateauGagnant.EstPlateauGagnant(out _);
            var sommeValeursNumerosPlateauNonTires =
                seulPlateauGagnant
                    .NumerosPlateau
                    .Where(n => !n.Tire)
                    .Sum(n => n.Valeur);
            var score = numeroTireGagnant * sommeValeursNumerosPlateauNonTires;
            score.Should().Be(4512);
            _testOutputHelper.WriteLine($"Le score est de {score} pour l'exemple.");
        }

        [Fact]
        public void PuzzleReelPart1()
        {
            var bingo = new Day4(Day4InputFile);
            var numeroTireGagnant = bingo.DeroulerJeuPart1();
            var seulPlateauGagnant = bingo.PlateauxGagnants.Single();
            var sommeValeursNumerosPlateauNonTires =
                seulPlateauGagnant
                    .NumerosPlateau
                    .Where(n => !n.Tire)
                    .Sum(n => n.Valeur);
            var score = numeroTireGagnant * sommeValeursNumerosPlateauNonTires;
            _testOutputHelper.WriteLine($"Le score est de {score} pour le puzzle réel part 1.");
        }

        #endregion

        #region Part2

        [Fact]
        public void ExemplePart2()
        {
            var bingo = new Day4(Day4ExampleFile);
            var numeroTireGagnant = bingo.DeroulerJeuPart2()!;
            var dernierPlateauGagnant = bingo.PlateauxGagnants
                .First(p =>
                    p.NumeroTireQuiFaitGagner != null &&
                    p.NumeroTireQuiFaitGagner.Valeur.Equals(numeroTireGagnant));
            var sommeValeursNumerosPlateauNonTires =
                dernierPlateauGagnant
                    .NumerosPlateau
                    .Where(n => !n.Tire)
                    .Sum(n => n.Valeur);
            var score = numeroTireGagnant * sommeValeursNumerosPlateauNonTires;
            score.Should().Be(1924);
            _testOutputHelper.WriteLine($"Le score est de {score} pour l'exemple part 2.");
        }

        [Fact]
        public void PuzzleReelPart2()
        {
            var bingo = new Day4(Day4InputFile);
            var numeroTireGagnant = bingo.DeroulerJeuPart2()!;
            var dernierPlateauGagnant = bingo.PlateauxGagnants
                .First(p =>
                    p.NumeroTireQuiFaitGagner != null &&
                    p.NumeroTireQuiFaitGagner.Valeur.Equals(numeroTireGagnant));
            var sommeValeursNumerosPlateauNonTires =
                dernierPlateauGagnant
                    .NumerosPlateau
                    .Where(n => !n.Tire)
                    .Sum(n => n.Valeur);
            var score = numeroTireGagnant * sommeValeursNumerosPlateauNonTires;
            _testOutputHelper.WriteLine($"Le score est de {score} pour le puzzle réel part 2.");
        }

        #endregion
    }
}
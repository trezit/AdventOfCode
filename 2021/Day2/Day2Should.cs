namespace _2021.Day2
{
    public class Day2Should
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public Day2Should(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        private const string Day2InputFile = "Day2\\Day2Input.txt";
        private const string Day2ExampleFile = "Day2\\Day2Example.txt";

        #region Part1

        [Fact]
        public void TraiterCommandeForwardFaitAvancerLeSousMarinDeLaValeurDeLaCommande()
        {
            var commande = new Commande("forward 2");
            var day2 = new Day2(new[] { commande });
            var arrivee = day2.ExecuterPart1();
            arrivee.ToString().Should().Be("2,0");
        }
        [Fact]
        public void TraiterCommandeDownFaitDescendreLeSousMarinDeLaValeurDeLaCommande()
        {
            var commande = new Commande("down 2");
            var day2 = new Day2(new[] { commande });
            var arrivee = day2.ExecuterPart1();
            arrivee.ToString().Should().Be("0,2");
        }
        [Fact]
        public void TraiterCommandeUpFaitMonterLeSousMarinDeLaValeurDeLaCommande()
        {
            var commande = new Commande("up 2");
            var day2 = new Day2(new[] { commande });
            var arrivee = day2.ExecuterPart1();
            arrivee.ToString().Should().Be("0,-2");
        }
        [Fact]
        public async Task ExemplePart1()
        {
            var day2 = new Day2(await GetCommandes(Day2ExampleFile));
            var arrivee = day2.ExecuterPart1();

            arrivee.PositionHorizontale.Should().Be(15);
            arrivee.Profondeur.Should().Be(10);
            var resultat = arrivee.GetProduitCoordonnees();
            resultat.Should().Be(150);
        }
        [Fact]
        public async Task PuzzleReelPart1()
        {
            var day2 = new Day2(await GetCommandes(Day2InputFile));
            var resultat = day2.ExecuterPart1().GetProduitCoordonnees();
            resultat.Should().Be(1924923);
            _testOutputHelper.WriteLine($"Le résultat est {resultat}");
        }

        #endregion

        #region Part2

        [Fact]
        public void TraiterCommandeDownFaitAugmenterLeViseurDeLaValeurDeLaCommande()
        {
            var commande = new Commande("down 2");
            var day2 = new Day2(new[] { commande });
            var _ = day2.ExecuterPart2();
            day2.Viseur.Should().Be(2);
        }
        [Fact]
        public void TraiterCommandeUpFaitDiminuerLeViseurDeLaValeurDeLaCommande()
        {
            var commande = new Commande("up 2");
            var day2 = new Day2(new[] { commande });
            var _ = day2.ExecuterPart2();
            day2.Viseur.Should().Be(-2);
        }
        [Fact]
        public void TraiterCommandeForwardFaitAvancerLeSousMarinDeLaValeurDeLaCommandeEtAugmenteLaProfondeurDeLaValeurDuViseurMultiplieParLaValeurDeLaCommande()
        {
            var commande1 = new Commande("down 2");
            var commande2 = new Commande("forward 2");
            var day2 = new Day2(new[] { commande1, commande2 });
            var arrivee = day2.ExecuterPart2();
            arrivee.ToString().Should().Be("2,4");
        }
        [Fact]
        public async Task ExemplePart2()
        {
            var day2 = new Day2(await GetCommandes(Day2ExampleFile));
            var arrivee = day2.ExecuterPart2();
            arrivee.PositionHorizontale.Should().Be(15);
            arrivee.Profondeur.Should().Be(60);
            var resultat = arrivee.GetProduitCoordonnees();
            resultat.Should().Be(900);
        }
        [Fact]
        public async Task PuzzleReelPart2()
        {
            var day2 = new Day2(await GetCommandes(Day2InputFile));
            var resultat = day2.ExecuterPart2().GetProduitCoordonnees();
            resultat.Should().Be(1982495697);
            _testOutputHelper.WriteLine($"Le résultat est {resultat}");
        }

        #endregion

        private async Task<IList<Commande>> GetCommandes(string filePath)
        {
            return (await File.ReadAllLinesAsync(filePath)).Select(ligne => new Commande(ligne)).ToList();
        }
    }
}
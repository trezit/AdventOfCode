using Xunit.Abstractions;

namespace _2021.Day3
{
    public class Day3Should
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public Day3Should(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        private const string Day3InputFile = "Day3\\Day3Input.txt";
        private const string Day3ExampleFile = "Day3\\Day3Example.txt";

        #region Part1

        [Fact]
        public async Task GetTauxGammaPart1()
        {
            var day3 = new Day3(await GetLignesRapport(Day3ExampleFile));
            int tauxGamme = day3.GetTauxGamma(day3.LignesRapport);
            tauxGamme.Should().Be(22);
            _testOutputHelper.WriteLine($"Le taux gamma est : {tauxGamme}");
        }
        [Fact]
        public async Task GetTauxEpsilonPart1()
        {
            var day3 = new Day3(await GetLignesRapport(Day3ExampleFile));
            int tauxEpsilon = day3.GetTauxEpsilon();
            tauxEpsilon.Should().Be(9);
            _testOutputHelper.WriteLine($"Le taux epsilon est : {tauxEpsilon}");
        }
        [Fact]
        public async Task ExemplePart1()
        {
            var day3 = new Day3(await GetLignesRapport(Day3ExampleFile));
            int consommationElectrique = day3.GetConsommationElectrique();
            consommationElectrique.Should().Be(198);
            _testOutputHelper.WriteLine($"La consommation électrique est : {consommationElectrique}");
        }
        [Fact]
        public async Task PuzzleReelPart1()
        {
            var day3 = new Day3(await GetLignesRapport(Day3InputFile));
            var resultat = day3.GetConsommationElectrique();
            resultat.Should().Be(1092896);
            _testOutputHelper.WriteLine($"Le résultat est : {resultat}");
        }

        #endregion

        #region Part2

        [Fact]
        public async Task GetTauxOxygeneGenereePart2()
        {
            var day3 = new Day3(await GetLignesRapport(Day3ExampleFile));
            int tauxOxygeneGeneree = day3.GetTauxOxygeneGeneree();
            tauxOxygeneGeneree.Should().Be(23);
            _testOutputHelper.WriteLine($"Le taux d'oxygène générée est : {tauxOxygeneGeneree}");
        }
        [Fact]
        public async Task GetTauxCoDeuxEpurePart2()
        {

            var day3 = new Day3(await GetLignesRapport(Day3ExampleFile));
            int tauxCoDeuxEpure = day3.GetTauxCoDeuxEpure();
            tauxCoDeuxEpure.Should().Be(10);
            _testOutputHelper.WriteLine($"Le taux de CO² épuré est : {tauxCoDeuxEpure}");
        }
        [Fact]
        public async Task ExemplePart2()
        {
            var day3 = new Day3(await GetLignesRapport(Day3ExampleFile));
            int tauxDeSurvie = day3.GetTauxDeSurvie();
            tauxDeSurvie.Should().Be(230);
            _testOutputHelper.WriteLine($"Le taux de survie est : {tauxDeSurvie}");
        }
        [Fact]
        public async Task PuzzleReelPart2()
        {
            var day3 = new Day3(await GetLignesRapport(Day3InputFile));
            var resultat = day3.GetTauxDeSurvie();
            resultat.Should().Be(4672151);
            _testOutputHelper.WriteLine($"Le résultat est : {resultat}");
        }

        #endregion

        private async Task<IList<LigneRapport>> GetLignesRapport(string filePath)
            => (await File.ReadAllLinesAsync(filePath))
                .Select(ligne =>
                    new LigneRapport(ligne.Select(c => c.ToString()).ToList()))
                .ToList();
    }
}
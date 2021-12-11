namespace _2021.Day1
{
    public class Day1Should
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public Day1Should(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        private const string Day1InputFile = "Day1\\Day1Input.txt";
        private const string Day1ExampleFile = "Day1\\Day1Example.txt";

        #region Part1

        [Fact]
        public async Task ExemplePart1()
        {
            var resultat = new Day1(await GetProfondeurs(Day1ExampleFile)).GetNombreDeDescentesEntreDeuxProfondeurs();
            resultat.Should().Be(7);
            _testOutputHelper.WriteLine($"Le résultat de l'exemple part 1 est : {resultat}");
        }
        [Fact]
        public async Task PuzzleReelPart1()
        {
            var resultat = new Day1(await GetProfondeurs(Day1InputFile)).GetNombreDeDescentesEntreDeuxProfondeurs();
            resultat.Should().Be(1215);
            _testOutputHelper.WriteLine($"Le résultat du puzzle part 1 est : {resultat}");
        }

        #endregion

        #region Part2

        [Fact]
        public async Task ExemplePart2()
        {
            var resultat = new Day1(await GetProfondeurs(Day1ExampleFile)).GetNombreDeSommesDe3ProfondeursSuperieuresALaSommePrecedente();
            resultat.Should().Be(5);
            _testOutputHelper.WriteLine($"Le résultat de l'exemple part 2 est : {resultat}");
        }
        [Fact]
        public async Task PuzzleReelPart2()
        {
            var resultat = new Day1(await GetProfondeurs(Day1InputFile)).GetNombreDeSommesDe3ProfondeursSuperieuresALaSommePrecedente();
            resultat.Should().Be(1150);
            _testOutputHelper.WriteLine($"Le résultat du puzzle part 2 est : {resultat}");
        }

        #endregion

        private async Task<int[]> GetProfondeurs(string filePath) =>
            (await File.ReadAllLinesAsync(filePath)).Select(int.Parse).ToArray();
    }
}
using Bogus;
using Xunit;
using Xunit.Abstractions;
using CursoOnline.Dominio.Cursos;
using CursoOnline.DominioTest._Builders;
using ExpectedObjects;

namespace CursoOnline.DominioTest.Cursos
{
    public class CursoTest : IDisposable
    {
        private readonly ITestOutputHelper _output;
        private readonly double _cargaHoraria;
        private readonly string _nome;
        private readonly double _valor;
        private readonly PublicoAlvo _publicoAlvo;
        private readonly string _descricao;

        public CursoTest(ITestOutputHelper output)
        {
            _output = output;
            var fake = new Faker();
            
            _nome = fake.Random.Word();
            _descricao = fake.Random.Words();
            _cargaHoraria = fake.Random.Double(50, 1000);
            _publicoAlvo = PublicoAlvo.Estudante;
            _valor = fake.Random.Double(100, 1000);
        }


        public void Dispose()
        {
            _output.WriteLine("Dispose executado");
        }
        
        [Fact]
        public void DeveCriarCurso()
        {
            //Arrange
            var cursoEsperado = new
            {
                Nome = _nome,
                CargaHoraria = _cargaHoraria,
                PublicoAlvo = _publicoAlvo,
                Valor = _valor,
                Descricao = _descricao
            };

            //Act
            var curso = new Curso(
                cursoEsperado.Nome,
                cursoEsperado.CargaHoraria,
                cursoEsperado.PublicoAlvo,
                cursoEsperado.Valor,
                cursoEsperado.Descricao);

            //Assert
            cursoEsperado.ToExpectedObject().ShouldMatch(curso);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void NaoDeveCursoTerUmNomeVazioOuNulo(string nomeInvalido)
        {
            Assert.Throws<ArgumentException>(() =>
                new CursoBuilder().ComNome(nomeInvalido).Build());
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-4)]
        [InlineData(-100)]
        public void NaoDeveCursoTerCargaHorariaMenorQueUm(double horaInvalida)
        {
            Assert.Throws<ArgumentException>(() =>
                new CursoBuilder().ComCargaHoraria(horaInvalida).Build());
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-4)]
        [InlineData(-100)]
        public void NaoDeveCursoTerValorMenorQueUm(double valorInvalido)
        {
            Assert.Throws<ArgumentException>(() =>
                new CursoBuilder().ComValor(valorInvalido).Build());
        }

    }
}
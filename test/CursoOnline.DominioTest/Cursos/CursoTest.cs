using ExpectedObjects;
using Xunit;

namespace CursoOnline.DominioTest.Cursos
{
    public class CursoTest
    {
        [Fact]
        public void DeveCriarCurso()
        {
            //Arrange
            var cursoEsperado = new
            {
                Nome = "Informática básica",
                CargaHoraria = (double)80,
                PublicoAlvo = PublicoAlvo.Estudante,
                Valor = (double)950
            };

            //Act
            var curso = new Curso(cursoEsperado.Nome, cursoEsperado.CargaHoraria, cursoEsperado.PublicoAlvo,
                cursoEsperado.Valor);

            //Assert

            cursoEsperado.ToExpectedObject().ShouldMatch(curso);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void NaoDeveCursoTerUmNomeVazioOuNulo(string nomeInvalido)
        {
            var cursoEsperado = new
            {
                Nome = nomeInvalido,
                CargaHoraria = (double)80,
                PublicoAlvo = PublicoAlvo.Estudante,
                Valor = (double)950
            };

            var message = Assert.Throws<ArgumentException>(() =>
                new Curso(cursoEsperado.Nome, cursoEsperado.CargaHoraria,
                    cursoEsperado.PublicoAlvo,
                    cursoEsperado.Valor)).Message;
            
            Assert.Equal("Nome inválido", message);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-4)]
        [InlineData(-100)]
        public void NaoDeveCursoTerCargaHorariaMenorQueUm(double horaInvalida)
        {
            var cursoEsperado = new
            {
                Nome = "Informática básica",
                CargaHoraria = horaInvalida,
                PublicoAlvo = PublicoAlvo.Estudante,
                Valor = (double)950
            };

            var message = Assert.Throws<ArgumentException>(() =>
                new Curso(cursoEsperado.Nome, cursoEsperado.CargaHoraria,
                    cursoEsperado.PublicoAlvo,
                    cursoEsperado.Valor)).Message;
            
            Assert.Equal("Carga horaria inválida", message);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-4)]
        [InlineData(-100)]
        public void NaoDeveCursoTerValorMenorQueUm(double valorInvalido)
        {
            var cursoEsperado = new
            {
                Nome = "Informática básica",
                CargaHoraria = (double)80,
                PublicoAlvo = PublicoAlvo.Estudante,
                Valor = valorInvalido
            };

            var message = Assert.Throws<ArgumentException>(() =>
                new Curso(cursoEsperado.Nome, cursoEsperado.CargaHoraria,
                    cursoEsperado.PublicoAlvo,
                    cursoEsperado.Valor)).Message;
            
            Assert.Equal("Valor inválido", message);
        }
    }
}


public class Curso
{
    public string Nome { get; private set; }
    public double CargaHoraria { get; private set; }
    public PublicoAlvo PublicoAlvo { get; private set; }
    public double Valor { get; private set; }

    public Curso(string nome, double cargaHoraria, PublicoAlvo publicoAlvo, double valor)
    {
        if (string.IsNullOrEmpty(nome))
            throw new ArgumentException("Nome inválido");

        if (cargaHoraria < 1)
            throw new ArgumentException("Carga horaria inválida");

        if (valor < 1)
            throw new ArgumentException("Valor inválido");


        Nome = nome;
        CargaHoraria = cargaHoraria;
        PublicoAlvo = publicoAlvo;
        Valor = valor;
    }
}

public enum PublicoAlvo
{
    Estudante,
    Universitario,
    Empregado,
    Empreeendedor
}
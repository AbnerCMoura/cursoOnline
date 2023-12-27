using Bogus;
using CursoOnline.Dominio.Cursos;
using Moq;
using Xunit;

namespace CursoOnline.DominioTest.Cursos;

public class ArmazenadorDeCursoTest
{
    private readonly CursoDto _cursoDto;
    
    public ArmazenadorDeCursoTest()
    {
        var fake = new Faker();

        _cursoDto = new CursoDto
        {
            Nome = fake.Random.Words(),
            CargaHoraria = fake.Random.Double(50, 1000),
            PublicoAlvoId = 1,
            Valor = fake.Random.Double(700, 2000),
            Descricao = fake.Random.Words()
        };
    }
    
    [Fact]
    public void DeveAdicionarCurso()
    {
        var cursoRespositorioMock = new Mock<ICursoRespositorio>();

        var armazenadorDeCurso = new ArmazenadorDeCurso(cursoRespositorioMock.Object);

        armazenadorDeCurso.Armazenar(_cursoDto);

        cursoRespositorioMock.Verify(r =>
            r.Adicionar(It.Is<Curso>(c =>
                c.Nome == _cursoDto.Nome && c.Descricao == _cursoDto.Descricao)
            ));
    }
}

public interface ICursoRespositorio
{
    public void Adicionar(Curso curso);
    public void Atualizar(Curso curso);
}

public class ArmazenadorDeCurso
{
    private readonly ICursoRespositorio _cursoRespositorio;

    public ArmazenadorDeCurso(ICursoRespositorio cursoRespositorio)
    {
        _cursoRespositorio = cursoRespositorio;
    }

    public void Armazenar(CursoDto cursoDto)
    {
        var curso = new Curso(
            cursoDto.Nome,
            cursoDto.CargaHoraria,
            PublicoAlvo.Empregado,
            cursoDto.Valor,
            cursoDto.Descricao
        );

        _cursoRespositorio.Adicionar(curso);
    }
}

public class CursoDto
{
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public int PublicoAlvoId { get; set; }
    public double CargaHoraria { get; set; }
    public double Valor { get; set; }
}
using CursoOnline.Dominio.Cursos;
using Moq;
using Xunit;

namespace CursoOnline.DominioTest.Cursos;

public class ArmazenadorDeCursoTest
{
    [Fact]
    public void DeveAdicionarCurso()
    {
        var cursoDto = new CursoDto
        {
            Nome = "Curso A",
            CargaHoraria = 80,
            PublicoAlvo = 1,
            Valor = 850.00,
            Descricao = "Descrição"
        };
        
        var cursoRespositorioMock = new Mock<ICursoRespositorio>();
        
        var armazenadorDeCurso = new ArmazenadorDeCurso(cursoRespositorioMock.Object);

        armazenadorDeCurso.Armazenar(cursoDto);
        cursoRespositorioMock.Verify(r => r.Adicionar(It.IsAny<Curso>()));
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
        var curso = new Curso(cursoDto.Nome,
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
    public int CargaHoraria { get; set; }
    public int PublicoAlvo { get; set; }
    public double Valor { get; set; }
}
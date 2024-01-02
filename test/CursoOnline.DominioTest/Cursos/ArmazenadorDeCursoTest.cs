using Bogus;
using CursoOnline.Dominio.Cursos;
using CursoOnline.Dominio.Cursos.Entidades.DTOs;
using CursoOnline.Dominio.Cursos.Servicos;
using CursoOnline.DominioTest._Builders;
using CursoOnline.DominioTest._Util;
using Moq;
using Xunit;

namespace CursoOnline.DominioTest.Cursos;

public class ArmazenadorDeCursoTest
{
    private readonly CursoDto _cursoDto;
    private readonly Mock<ICursoRepositorio> _cursoRespositorioMock;
    private readonly ArmazenadorDeCurso _armazenadorDeCurso;

    public ArmazenadorDeCursoTest()
    {
        var fake = new Faker();

        _cursoDto = new CursoDto
        {
            Nome = fake.Random.Words(),
            Descricao = fake.Lorem.Paragraph(),
            PublicoAlvo = "Estudante",
            CargaHoraria = fake.Random.Double(50, 1000),
            Valor = fake.Random.Double(700, 2000)
        };

        _cursoRespositorioMock = new Mock<ICursoRepositorio>();
        _armazenadorDeCurso = new ArmazenadorDeCurso(_cursoRespositorioMock.Object);
    }

    [Fact]
    public void DeveAdicionarCurso()
    {
        _armazenadorDeCurso.Armazenar(_cursoDto);

        _cursoRespositorioMock.Verify(r =>
            r.Adicionar(It.Is<Curso>(c =>
                c.Nome == _cursoDto.Nome && c.Descricao == _cursoDto.Descricao)
            ));
    }

    [Fact]
    public void NaoDeveInformarPublicoAlvoInvalido()
    {
        const string publicoAlvoInvalido = "Médico";
        
        _cursoDto.PublicoAlvo = publicoAlvoInvalido;
        Assert.Throws<ArgumentException>(() => _armazenadorDeCurso.Armazenar(_cursoDto))
        .ComMensagem("Público alvo inválido");
    }

    [Fact]
    public void NaoDeveAdicionarCursoComMesmoDeOutroJaSalvo()
    {
        var cursoJaSalvo = CursoBuilder.Novo().ComNome(_cursoDto.Nome).Build();
        
        _cursoRespositorioMock.Setup(x => x.ObterPeloNome(_cursoDto.Nome)).Returns(cursoJaSalvo);
        
        Assert.Throws<ArgumentException>(() => _armazenadorDeCurso.Armazenar(_cursoDto))
            .ComMensagem("Curso já registrado");
    }
}
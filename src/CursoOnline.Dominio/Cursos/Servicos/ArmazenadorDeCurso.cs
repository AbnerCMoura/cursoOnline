using CursoOnline.Dominio.Cursos.Entidades.DTOs;

namespace CursoOnline.Dominio.Cursos.Servicos;

public class ArmazenadorDeCurso
{
    private readonly ICursoRepositorio _cursoRespositorio;

    public ArmazenadorDeCurso(ICursoRepositorio cursoRepositorio)
    {
        _cursoRespositorio = cursoRepositorio;
    }

    public void Armazenar(CursoDto cursoDto)
    {
        var cursoJaSalvo = _cursoRespositorio.ObterPeloNome(cursoDto.Nome);
        if (cursoJaSalvo != null) throw new ArgumentException("Curso já registrado");

        if (!Enum.TryParse<PublicoAlvo>(cursoDto.PublicoAlvo, out var publicoAlvo))
            throw new ArgumentException("Público alvo inválido");

        var curso = new Curso(
            cursoDto.Nome,
            cursoDto.CargaHoraria,
            publicoAlvo,
            cursoDto.Valor,
            cursoDto.Descricao
        );

        _cursoRespositorio.Adicionar(curso);
    }
}
using LCFila.Application.Notificacoes;

namespace LCFila.Application.Interfaces;

public interface INotificador
{
    bool TemNotificacao();
    List<Notificacao> ObterNotificacoes();
    void Handle(Notificacao notificacao);
}

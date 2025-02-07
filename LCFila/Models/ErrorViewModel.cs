namespace LCFila.Models;

public class ErrorViewModel
{
    public string RequestId { get; set; }

    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

    public int ErroCode { get; set; }
    public string Titulo { get; set; }
    public string Mensagem { get; set; }
}

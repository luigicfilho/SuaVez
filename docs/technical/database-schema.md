# Database Schema

This diagram represents the database schema for our .NET C# application, based on the provided entity classes. It illustrates the relationships between the tables and their respective columns.

```mermaid
erDiagram
    Pessoa {
        Guid Id PK
        Guid FilaId FK
        string Nome
        string Documento
        string Celular
        bool Ativo
        bool Preferencial
        int Posicao
        int Status
    }

    Fila {
        Guid Id PK
        string Nome
        DateTime DataInicio
        DateTime DataFim
        string TempoMedio
        int Tipofila
        Guid EmpresaId FK
        int Status
        bool Ativo
        Guid UserId FK
    }

    EmpresaLogin {
        Guid Id PK
        string NomeEmpresa
        string CNPJ
        Guid IdAdminEmpresa
        bool Ativo
    }

    EmpresaConfiguracao {
        Guid Id PK
        string NomeDaEmpresa
        string LinkLogodaEmpresa
        string CorPrincipalEmpresa
        string CorSegundariaEmpresa
        string FooterEmpresa
    }

    AppUser {
        string Id PK
    }

    Agendamento {
        Guid Id PK
        DateTime DatadoAgendamento
        Guid PessoaId FK
    }

    Pessoa ||--o{ Agendamento : "PessoaId"
    Pessoa ||--o{ Fila : "FilaId"
    Fila ||--o{ EmpresaLogin : "EmpresaId"
    EmpresaLogin ||--o{ EmpresaConfiguracao : "EmpresaConfiguracaoId"
    EmpresaLogin ||--o{ AppUser : "IdAdminEmpresa"
    Fila ||--o{ AppUser : "UserId"
```

﻿namespace LCFila.Domain.Models;

public class Agendamento: Entity
{
    public DateTime DatadoAgendamento { get; set; }
    public Pessoa Pessoanodia { get; set; } = new();
}

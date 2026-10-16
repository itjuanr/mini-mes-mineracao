namespace Mes.Domain;

/// <summary>
/// Erro lançado quando uma regra de negócio é violada.
/// A mensagem é pensada para ser mostrada direto ao usuário.
/// </summary>
public class RegraDeNegocioException(string mensagem) : Exception(mensagem);

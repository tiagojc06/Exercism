public static class Bob
{
    public static string Response(string statement)
    {
        statement = statement.Trim();
        
        if (statement.Length == 0)
            return "Fine. Be that way!";
        
        bool statementIsAQuestion = statement.EndsWith('?');

        // --- Enfoque 1: Bucle Imperativo (foreach con early exit) ---
        // Ventajas: Rendimiento absoluto. No asigna memoria extra y frena en seco al encontrar la primera letra (O(N) en el peor de los casos, O(1) en el mejor). Es fácil de debugear paso a paso.
        // Desventajas: Verboso. Requiere declarar una variable bandera externa ('statementHasLetters') y usar 7 líneas de código para algo muy simple.
        
        bool statementHasLetters = false;
        
        foreach (char c in statement)
        {
            if (char.IsLetter(c))
            {
                statementHasLetters = true;
                break;
            }
        }
        
        // --- Enfoque 2: Enfoque Funcional con LINQ (.Any) ---
        // Ventajas: Extremadamente declarativo y conciso (1 sola línea). Comunica la "intención" del código al instante. También tiene salida temprana (Early Exit) porque Any frena al primer 'true'.
        // Desventajas: Tiene un micro-costo de rendimiento imperceptible por detrás (crea un enumerador y hace llamadas a delegados), pero es el estándar preferido de la industria por su legibilidad.
        // Nota técnica: Usa "Conversión de Grupo de Métodos". Como char.IsLetter recibe un 'char' y retorna un 'bool', encaja perfecto en la firma que pide Any(), permitiendo omitir la expresión lambda (c => ...).
        
        // bool statementHasLetters = statement.Any(char.IsLetter);

        bool statementIsInCapitalLetters = statementHasLetters && statement.ToUpper() == statement;
        
        // --- Enfoque 1: Cláusulas de Guarda (Bouncer Pattern / Guard Clauses) ---
        // Ventajas: Flujo de lectura natural de arriba hacia abajo. Excelente semántica: el último 'return' actúa claramente como la respuesta por defecto genérica para cualquier caso no contemplado en los 'if' superiores.
        // Desventajas: Ocupa más espacio vertical (más líneas de código).
        
        if (statementIsAQuestion && statementIsInCapitalLetters)
            return "Calm down, I know what I'm doing!";
        if (statementIsAQuestion)
            return "Sure.";
        if (statementIsInCapitalLetters)
            return "Whoa, chill out!";
        
        return "Whatever.";
        
        // --- Enfoque 2: Expresión Switch con Tuplas (Tuple Switch Expression) (C# 8.0+) ---
        // Ventajas: Extremadamente declarativo y moderno. Permite visualizar toda la lógica como si fuera una tabla de verdad matemática en un solo bloque. Es muy seguro porque obliga al desarrollador a visualizar todas las combinaciones posibles.
        // Desventajas: Requiere que las condiciones complejas hayan sido evaluadas previamente en variables booleanas (como hicimos arriba) para no hacer que la sintaxis de la tupla quede ilegible.

        /*
        return (statementIsAQuestion, statementIsInCapitalLetters) switch
        {
            (true, true)   => "Calm down, I know what I'm doing!", // Es pregunta Y grito
            (true, false)  => "Sure.",                             // Es solo pregunta
            (false, true)  => "Whoa, chill out!",                  // Es solo grito
            (false, false) => "Whatever."                          // Ninguna de las dos (Respuesta genérica)
        };
        */
    }
}
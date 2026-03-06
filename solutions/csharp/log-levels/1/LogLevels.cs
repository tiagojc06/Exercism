using System.Text.RegularExpressions;

static class LogLine
{
    public static string Message(string logLine)
    {
        if (string.IsNullOrWhiteSpace(logLine))
            return string.Empty;
        
        // --- Enfoque 1: Split y Patrones de Lista ---
        // Puntos fuertes: Máxima legibilidad y código moderno. Puntos bajos: Mayor consumo de memoria al instanciar arreglos.
        
        // El parámetro 'count' especifica el número máximo de elementos en el array resultante.
        // Si la cadena contiene más separadores que su valor, se ignoran, garantizando que el último elemento del array contenga todo el todo el texto restante.
        // IMPORTANTE: Si Split no encuentra el separador especificado, retorna un array de 1 solo elemento que contiene la cadena original completa.
        string[] logLineSubstrings = logLine.Split("]: ", 2);
        
        return logLineSubstrings is [_, var message] ? message.Trim() : string.Empty; // logLineSubstrings.Length > 1 ? logLineSubstrings[1].Trim() : string.Empty;
        
        /*
        // --- Enfoque 2: IndexOf y Substring ---
        // Puntos fuertes: El más rápido y liviano en memoria RAM. Puntos bajos: Más verboso, requiere manejo manual de índices.
        
        int startOfMessageIndex = logLine.IndexOf(':');
        
        if (startOfMessageIndex == -1)
            return string.Empty;
        
        startOfMessageIndex++;
        
        return logLine.Substring(startOfMessageIndex).Trim();
        */

        /*
        // --- Enfoque 3: Expresiones Regulares (Regex) ---
        // Puntos fuertes: Ideal para buscar patrones muy complejos o dinámicos. Puntos bajos: Costoso en rendimiento de CPU (overkill para este problema).
        
        // (?<=\]: ) -> Lookbehind: verifica que exista "]: " antes, pero no lo captura en el resultado.
        // .+        -> Cuantificador codicioso (greedy): captura 1 o más de cualquier carácter hasta el final de la línea.
        string pattern = @"(?<=\]: ).+";
        Match match = Regex.Match(logLine, pattern);
        
        // Se puede usar match.Success para comprobar si la operación fue exitosa.
        // En este caso omitirlo es seguro porque, si falla, match.Value retorna una cadena vacía ("").
        return match.Value.Trim();
        */
    }

    public static string LogLevel(string logLine)
    {
        if (string.IsNullOrWhiteSpace(logLine))
            return string.Empty;
        
        // --- Enfoque 1: Split y Patrones de Lista ---
        // Puntos fuertes: Código declarativo y muy fácil de leer. Puntos bajos: Crea arrays temporales innecesarios en memoria.

        int startOfLogLevelIndex = logLine.IndexOf('[');

        if (startOfLogLevelIndex == -1)
                return string.Empty;

        startOfLogLevelIndex++;

        // El operador de rango (..) es la sintaxis moderna (C# 8+) recomendada. A nivel de rendimiento en strings, es idéntico a usar Substring.
        string logLevelFormatted = logLine[startOfLogLevelIndex..]; // logLine.Substring(startOfLogLevelIndex);

        string[] logLineSubstrings = logLevelFormatted.Split("]: ", 2);

        return logLineSubstrings is [var logLevel, _] ? logLevel.ToLower() : string.Empty; // logLineSubstrings.Length > 1 ? logLineSubstrings[0].ToLower() : string.Empty;

        /*
        // --- Enfoque 2: IndexOf y Substring ---
        // Puntos fuertes: Rendimiento máximo, ideal para motores de videojuegos. Puntos bajos: Lógica matemática que puede inducir a errores humanos.
        
        int startOfLogLevelIndex = logLine.IndexOf('[');
        int endOfLogLevelIndex = logLine.IndexOf(']');
        
        if (startOfLogLevelIndex == -1 || endOfLogLevelIndex == -1)
            return string.Empty;
        
        startOfLogLevelIndex++;

        return logLine.Substring(startOfLogLevelIndex, endOfLogLevelIndex - startOfLogLevelIndex).ToLower();
        */

        /*
        // --- Enfoque 3.A: Regex usando Grupo de Captura (paréntesis) ---
        // Puntos fuertes: Estándar de la industria para Regex, buen rendimiento dentro de su categoría. Puntos bajos: Sintaxis críptica (valor en .Groups[1]).

        // \[ y \] -> Escapan los corchetes para buscar los literales "[" y "]".
        // (.+?)   -> Grupo de captura perezoso (lazy). El '+' exige al menos uno o más caracteres.
        //            El '?' hace que se detenga en el PRIMER corchete de cierre que encuentre (evita atrapar texto de más).
        string pattern = @"\[(.+?)\]";
        Match match = Regex.Match(logLine, pattern);
        
        // Groups[0]: Siempre representa la coincidencia completa (ej. "[INFO]").
        // Groups[1] en adelante: Representan el contenido de los paréntesis () (ej. "INFO").
        return match.Groups[1].Value.ToLower();
        */

        /*
        // --- Enfoque 3.B: Regex usando Lookarounds ---
        // Puntos fuertes: Extrae el valor directo en match.Value. Puntos bajos: Menor rendimiento que los grupos de captura.

        // Lookbehind (?<=...): Asegura que exista el patrón antes, pero no lo incluye en el resultado.
        // Lookahead  (?=...):  Asegura que exista el patrón después, pero no lo incluye en el resultado.
        // Combinarlos es ideal para extraer el texto del medio directamente en match.Value sin usar Groups.

        // (?<=\[) -> Asegura que tenga un '[' justo antes.
        // .+?     -> Perezoso (lazy): captura 1 o más caracteres (+) hasta chocar con la siguiente regla.
        // (?=\])  -> Asegura que tenga un ']' justo después.
        string pattern = @"(?<=\[).+?(?=\])";
        Match match = Regex.Match(logLine, pattern);

        return match.Value.ToLower();
        */
    }

    public static string Reformat(string logLine)
    {
        if (string.IsNullOrWhiteSpace(logLine))
            return string.Empty;
        
        // --- Enfoque 1: Respetando el principio DRY (Don't Repeat Yourself) ---
        // Puntos fuertes: Código hiper limpio y centralizado. Puntos bajos: Procesa la misma cadena original dos veces por debajo.

        string message = Message(logLine);
        string logLevel = LogLevel(logLine);

        // Comprobar la propiedad .Length es la forma más rápida en C# para saber si un string tiene texto.
        // Solo lee un número entero en memoria (O(1)), evitando la sobrecarga que genera comparar objetos string (como pasaría con != "").
        return message.Length != 0 && logLevel.Length != 0 ? $"{message} ({logLevel})" : string.Empty;

        /*
        // --- Enfoque 2: Rompiendo DRY con procesamiento único ---
        // Puntos fuertes: Optimización pura, recorre y corta la cadena una sola vez. Puntos bajos: Si el formato del log cambia, deberás actualizar múltiples partes del código.
        
        int startOfLogLevelIndex = logLine.IndexOf('[');

        if (startOfLogLevelIndex == -1)
            return string.Empty;

        startOfLogLevelIndex++;

        string logLevelFormatted = logLine[startOfLogLevelIndex..];

        string[] logLineSubstrings = logLevelFormatted.Split("]: ", 2);

        return logLineSubstrings is [var logLevel, var message] ? $"{message.Trim()} ({logLevel.ToLower()})" : string.Empty; // logLineSubstrings.Length > 1 ? $"{logLineSubstrings[1].Trim()} ({logLineSubstrings[0].ToLower()})" : string.Empty;
        */
    }
}

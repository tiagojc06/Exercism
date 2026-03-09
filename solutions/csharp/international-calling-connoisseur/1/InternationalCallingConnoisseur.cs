public static class DialingCodes
{
    public static Dictionary<int, string> GetEmptyDictionary() => [];

    public static Dictionary<int, string> GetExistingDictionary() =>
        // --- Enfoque 1: Inicialización por Índice (Indexer Syntax - C# 6.0+) (Recomendado) ---
        // Ventajas: Semántica clara (los corchetes [] representan visualmente una búsqueda por clave). Es seguro a prueba de fallos: si por error humano duplicás una clave (ej: [1] = "A", [1] = "B"), simplemente sobrescribe el valor sin romper el programa.
        new()
        {
            [1] = "United States of America",
            [55] = "Brazil",
            [91] = "India"
        };
        
        // --- Enfoque 2: Inicializador de Colección Clásico ---
        // Ventajas: Es el estándar antiguo, lo vas a ver en mucho código heredado (Legacy).
        // Desventajas: Si accidentalmente duplicás una clave (ej: { 1, "A" }, { 1, "B" }), el compilador no se queja, pero el programa va a lanzar una excepción (crasheo fatal) en tiempo de ejecución al intentar inicializar el diccionario.
        /*
        new()
        {
            { 1, "United States of America"},
            { 55, "Brazil" },
            { 91, "India" }
        };
        */

    public static Dictionary<int, string> AddCountryToEmptyDictionary(int countryCode, string countryName)
    {
        // --- Enfoque 1: Inicialización Imperativa (Recomendado acá) ---
        // Ventajas: Respeta el principio DRY (Don't Repeat Yourself). Si la forma de crear un diccionario vacío cambia en 'GetEmptyDictionary()', este método hereda el cambio automáticamente.
        // Desventajas: Ocupa más líneas de código.
        var dict = GetEmptyDictionary();
        dict.Add(countryCode, countryName);
        return dict;
        
        // --- Enfoque 2: Sintaxis de Inicializador de Colección (Collection Initializer) ---
        // Ventajas: Código hiper moderno, limpio y declarativo (todo en una sola sentencia).
        // Desventajas: Rompe el principio DRY en este ejercicio en particular, ya que crea un diccionario nuevo desde cero ignorando por completo el método 'GetEmptyDictionary()'.
        /*
        return new()
        {
            { countryCode, countryName }
        };
        */
    }

    public static Dictionary<int, string> AddCountryToExistingDictionary(
        Dictionary<int, string> existingDictionary, int countryCode, string countryName)
    {
        existingDictionary.Add(countryCode, countryName);
        return existingDictionary;
    }

    public static string GetCountryNameFromDictionary(
        Dictionary<int, string> existingDictionary, int countryCode) =>
        existingDictionary.TryGetValue(countryCode, out var countryName) ? countryName : string.Empty;

    public static bool CheckCodeExists(Dictionary<int, string> existingDictionary, int countryCode) =>
        existingDictionary.ContainsKey(countryCode);

    public static Dictionary<int, string> UpdateDictionary(
        Dictionary<int, string> existingDictionary, int countryCode, string countryName)
    {
        if (existingDictionary.ContainsKey(countryCode))
            existingDictionary[countryCode] = countryName;
        
        return existingDictionary;
    }

    public static Dictionary<int, string> RemoveCountryFromDictionary(
        Dictionary<int, string> existingDictionary, int countryCode)
    {
        existingDictionary.Remove(countryCode);
        return existingDictionary;
    }

    public static string FindLongestCountryName(Dictionary<int, string> existingDictionary)
    {
        // --- Enfoque 1: LINQ MaxBy con Null Coalescing (C# 10+) (Recomendado) ---
        // Ventajas: Código declarativo ultra limpio de 1 sola línea y rendimiento optimizado (O(N)) que no genera basura en memoria.
        
        // Explicación del Operador '??' (Null-Coalescing):
        // Si el diccionario llega a estar completamente vacío, MaxBy no tiene a quién elegir como ganador, por lo que devuelve 'null'. 
        // Como el método exige devolver un 'string' estricto (no nulo), el operador '??' actúa como paracaídas: 
        // "Si lo de la izquierda es null, devolvé lo de la derecha (string.Empty)".
        return existingDictionary.Values.MaxBy(countryName => countryName.Length) ?? string.Empty;
        
        // var longestCountryName = string.Empty;

        // --- Enfoque 2: Bucle Foreach clásico ---
        // Ventajas: Máximo rendimiento absoluto (O(N)). Utiliza la memoria rápida (Stack) y evalúa la condición directamente sin crear objetos temporales ni delegados en memoria.
        // Desventajas: Es más verboso e imperativo.
        // foreach (var countryName in existingDictionary.Values)
        // {
        //     if (countryName.Length > longestCountryName.Length)
        //         longestCountryName = countryName;
        // }
        
        // --- Enfoque 3: Filtro LINQ con variable modificada (Modified Closure) ---
        // Ventajas: Se lee como una sola frase lógica (Azúcar sintáctico puro). El '.Where' filtra dinámicamente usando el valor actualizado en cada iteración.
        // Desventajas: Pésimo rendimiento por debajo. Obliga al compilador a crear una clase invisible en el Heap para poder compartir la variable 'longestCountryName' entre el bucle y la expresión lambda, generando "basura" en la memoria (Garbage Collection overhead).
        /*
        foreach (var countryName in existingDictionary.Values.Where(countryName => countryName.Length > longestCountryName.Length))
        {
            longestCountryName = countryName;
        }
        */

        // return longestCountryName;
    }
}
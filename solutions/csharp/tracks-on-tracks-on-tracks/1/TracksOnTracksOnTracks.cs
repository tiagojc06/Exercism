/* * ENFOQUE IMPERATIVO vs ENFOQUE FUNCIONAL
 * * - Enfoque Imperativo (Mutabilidad): Modifica la estructura de datos original directamente en memoria. 
 * Cuándo usarlo: Cuando el rendimiento y el ahorro de memoria son críticos (por ejemplo, actualizando miles de entidades por frame en Unity).
 * * - Enfoque Funcional (Inmutabilidad): Crea y devuelve una copia nueva con los cambios, dejando la lista original intacta.
 * Cuándo usarlo: Cuando querés evitar "efectos secundarios" (bugs donde modificás datos que otra parte del programa todavía necesitaba usar).
 */

public static class Languages
{
    public static List<string> NewList() => [];

    public static List<string> GetExistingLanguages() => ["C#", "Clojure", "Elm"];

    public static List<string> AddLanguage(List<string> languages, string language)
    {
        // Enfoque Funcional
        // return [..languages, language];
        
        // Enfoque Imperativo
        languages.Add(language);
        return languages;
    }

    public static int CountLanguages(List<string> languages) => languages.Count;

    public static bool HasLanguage(List<string> languages, string language) => languages.Contains(language);

    public static List<string> ReverseList(List<string> languages)
    {
        // Enfoque Funcional (con LINQ, menos eficiente)
        // AsEnumerable(): Retorna la lista como una secuencia genérica (IEnumerable) para evitar mutar la original.
        // Reverse() y ToList(): Invierte el orden de los elementos de esa secuencia y luego crea una nueva lista con esos datos.
        return languages.AsEnumerable().Reverse().ToList();
        
        // Enfoque Funcional (Creando nueva instancia)
        // List<string> updatedLanguages = new(languages);
        // updatedLanguages.Reverse();
        // return updatedLanguages;
        
        // Enfoque Imperativo
        languages.Reverse();
        return languages;
    }

    public static bool IsExciting(List<string> languages)
    {
        // if (languages.Count == 0)
        //     return false;
        //
        // if (languages[0] == "C#")
        //     return true;
        //
        // return languages.Count >= 2 && languages[1] == "C#" && languages.Count is 2 or 3;
        // return languages is [_, "C#", ..] && languages.Count is 2 or 3;

        // Verifica automáticamente el tamaño y el contenido de forma segura evitando errores de índice (List Patterns de C# 11)
        // - 'is': Operador de coincidencia. Evalúa si la lista 'languages' encaja en alguna de las formas (patrones) de la derecha.
        // - 'or': Operador lógico "O" para patrones. Permite encadenar varias condiciones.
        // - ["C#", ..]: ¿El 1º es "C#" y le siguen cero o más elementos? El '..' (Slice Pattern) representa "el resto de la lista".
        // - [_, "C#"]: ¿Tiene EXACTAMENTE 2 elementos y el 2º es "C#"? El '_' (Discard) significa "acepta cualquier valor en esta posición".
        // - [_, "C#", _]: ¿Tiene EXACTAMENTE 3 elementos y el del medio es "C#"? (Los '_' ignoran qué hay en las puntas).
        return languages is ["C#", ..] or [_, "C#"] or [_, "C#", _];
    }

    public static List<string> RemoveLanguage(List<string> languages, string language)
    {
        // Enfoque Funcional
        // List<string> updatedLanguages = new(languages);
        // updatedLanguages.Remove(language);
        // return updatedLanguages;
        
        // Enfoque Imperativo
        languages.Remove(language);
        return languages;
    }

    public static bool IsUnique(List<string> languages)
    {
        // --- 1º LUGAR (LA MEJOR SOLUCIÓN) ---
        // Desc: Uso de estructura Hash con salida temprana (Enfoque Imperativo local / No muta la lista original)
        // Ventaja: Extremadamente veloz. Si encuentra un duplicado en la posición 2, se detiene al instante (Early Exit).
        // Desventaja: Consume un poco de memoria extra para crear la estructura Hash.
        // Velocidad: $O(N)$ (Lineal: El tiempo de ejecución aumenta de forma proporcional y directa a la cantidad de elementos).
        // Memoria: $O(N)$ (Lineal: El consumo de memoria extra aumenta proporcionalmente al tamaño de la lista).
        
        /* ¿Qué es un HashSet y para qué sirve?
         * Es una colección especial en C# diseñada exclusivamente para búsquedas ultrarrápidas y evitar duplicados. 
         * En lugar de buscar elemento por elemento (como una Lista), usa una fórmula matemática (Hash) 
         * para saber instantáneamente si un elemento ya está guardado o no.
         */
        HashSet<string> seenLanguages = new();
        foreach (string lang in languages)
        {
            // El método .Add() intenta agregarlo. Si ya existía en el Hash, devuelve false automáticamente.
            if (!seenLanguages.Add(lang)) 
                return false; 
        }
        return true;


        // --- 2º LUGAR ---
        // Desc: Evaluación completa con LINQ (Enfoque Funcional)
        // Ventaja: Código muy limpio, declarativo y fácil de leer. No muta la lista original.
        // Desventaja: Obliga a recorrer TODA la lista sí o sí para contar los elementos, aunque el duplicado esté al principio.
        // Velocidad: $O(N)$ (Lineal)
        // Memoria: $O(N)$ (Lineal)
        
        // Funcionamiento: Distinct() retorna un IEnumerable sin duplicados (se filtran usando HashSet por detrás), 
        // Count() cuenta los elementos, y si ese número es igual al tamaño original, todos son únicos.
        // return languages.Distinct().Count() == languages.Count;


        // --- 3º LUGAR ---
        // Desc: Ordenamiento y comparación adyacente (Enfoque Imperativo)
        // Ventaja: No consume casi nada de memoria extra.
        // Desventaja: ¡Peligroso! Modifica (muta) el orden de la lista original, generando un efecto secundario. Es más lento matemáticamente por el proceso de ordenar.
        // Velocidad: $O(N \log N)$ (Logarítmica / Linealítmica: Tarda un poco más que el tiempo lineal, es el estándar para algoritmos de ordenamiento rápido).
        // Memoria: $O(1)$ (Constante: El mejor rendimiento de memoria. Usa la misma cantidad casi nula de RAM extra sin importar si la lista tiene 10 o 10.000 elementos).
        
        // languages.Sort();
        // for (int i = 1; i < languages.Count; i++)
        // {
        //     if (languages[i] == languages[i - 1])
        //         return false;
        // }
        // return true;


        // --- 4º LUGAR (LA PEOR PARA LISTAS GRANDES) ---
        // Desc: Búsqueda por fuerza bruta con bucles anidados (Enfoque Imperativo local / No muta la lista original)
        // Ventaja: Excelente uso de memoria (no crea copias) y no modifica la lista original.
        // Desventaja: Pésimo rendimiento en listas grandes porque compara todo contra todo.
        // Velocidad: $O(N^2)$ (Cuadrática: El tiempo se dispara exponencialmente. Si la lista se multiplica por 10, el tiempo de procesamiento se multiplica por 100).
        // Memoria: $O(1)$ (Constante).
        
        // for (int i = 0; i < languages.Count; i++)
        // {
        //     // j arranca un paso adelante de i, así nunca comparamos el mismo elemento consigo mismo
        //     for (int j = i + 1; j < languages.Count; j++)
        //     {
        //         if (languages[i] == languages[j])
        //             return false;
        //     }
        // }
        // return true;
    }
}

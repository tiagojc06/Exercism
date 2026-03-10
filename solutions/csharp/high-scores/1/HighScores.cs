public class HighScores
{
    private readonly List<int> _highScoreList;

    // Al usar el operador spread (..), creamos una COPIA FÍSICA de la lista en memoria.
    // Ventaja: Evita la fuga de encapsulamiento. Si la lista original que nos pasaron por parámetro se modifica desde afuera, nuestra lista privada (_highScoreList) se mantiene intacta y segura.
    public HighScores(List<int> list) => _highScoreList = [.. list];

    public List<int> Scores() => _highScoreList;

    public int Latest() =>
        // Enfoque 1: Operador de Índice desde el final (Hat Operator '^')
        // Ventaja: Rendimiento matemático instantáneo ($O(1)$). Va directo a la última posición del array interno.
        // Desventaja: Si la lista llega a estar vacía, el programa crashea (excepción). Para este ejercicio es seguro porque siempre hay datos.
        _highScoreList[^1];

    // Enfoque 2: LINQ clásico
    // Ventaja: Es "a prueba de balas". Si la lista está vacía, devuelve el valor por defecto (0) sin crashear.
    // Desventaja: Tiene una pequeñísima sobrecarga de procesamiento por invocar los métodos de extensión de LINQ.
    // _highScoreList.LastOrDefault();

    public int PersonalBest() => _highScoreList.Max();

    public List<int> PersonalTopThree()
    {
        // Enfoque 1: Solución Idiomática (LINQ Puro)
        // Ventajas: Código ultra limpio, fácil de leer y es el estándar de la industria para listas normales.
        // Desventajas: Rendimiento $O(N \log N)$. La CPU desperdicia tiempo ordenando TODOS los elementos de la lista, incluso los más bajos, solo para quedarse con los primeros 3.
        return _highScoreList.OrderByDescending(hs => hs).Take(3).ToList();

        // Enfoque 2: Obra Maestra (Híbrido Optimizado)
        // Ventajas: Rendimiento extremo ($O(N)$ real). Solo ordena los primeros 3 (costo casi cero) y hace una sola pasada lineal por el resto. Además, si la lista tiene menos de 3 elementos, el bucle 'for' se saltea solo, evitando crasheos. Nivel de Arquitecto.
        // Desventajas: Es más verboso y ocupa más líneas de código.
        /*
        List<int> topThree = _highScoreList.Take(3).OrderByDescending(s => s).ToList();

        for (int i = 3; i < _highScoreList.Count; i++)
        {
            if (_highScoreList[i] > topThree[0])
            {
                topThree[2] = topThree[1];
                topThree[1] = topThree[0];

                topThree[0] = _highScoreList[i];
            }
            else if (_highScoreList[i] > topThree[1])
            {
                topThree[2] = topThree[1];

                topThree[1] = _highScoreList[i];
            }
            else if (_highScoreList[i] > topThree[2])
                topThree[2] = _highScoreList[i];
        }
        return topThree;
        */

        // Enfoque 3: Escaneo Lineal Manual con "Magic Numbers"
        // Ventajas: También logra un rendimiento $O(N)$ recorriendo la lista una sola vez.
        // Desventajas: Usa un parche (`int.MinValue`) para rellenar vacíos. Te obliga a hacer una segunda pasada con `RemoveAll` al final por si el jugador tenía menos de 3 puntajes guardados. Es funcional, pero arquitectónicamente más "sucio" que el Enfoque 2.
        /*
        List<int> topThree = [int.MinValue, int.MinValue, int.MinValue]; // Expresión de colección (Mucho más rápido que Enumerable.Repeat(int.MinValue, 3).ToList())

        foreach (var score in _highScoreList)
        {
            if (score > topThree[0])
            {
                topThree[2] = topThree[1];
                topThree[1] = topThree[0];

                topThree[0] = score;
            }
            else if (score > topThree[1])
            {
                topThree[2] = topThree[1];

                topThree[1] = score;
            }
            else if (score > topThree[2])
                topThree[2] = score;
        }
        
        topThree.RemoveAll(score => score == int.MinValue);
        return topThree;
        */
    }
}
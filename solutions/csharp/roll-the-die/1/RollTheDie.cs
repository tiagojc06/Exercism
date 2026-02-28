public class Player
{
    // Opción 1: Creamos el dado UNA sola vez para todo el jugador
    // private Random _random = new Random();
    
    // Opción 2: Usar Random.Shared para evitar instanciar (new) objetos repetidos en memoria (Mejora el rendimiento y evita lag por Garbage Collection).
    // Random.Shared es un singleton que se puede usar en toda la aplicación sin necesidad de crear nuevas instancias de Random, lo que es especialmente útil en escenarios donde se necesitan muchas llamadas a métodos aleatorios, como en juegos o simulaciones.
    public int RollDie() =>
        // _random.Next(1, 19);
        Random.Shared.Next(1, 19);

    public double GenerateSpellStrength() =>
        // _random.NextDouble() * 100;
        Random.Shared.NextDouble() * 100;
}

class BirdCount
{
    private int[] birdsPerDay;

    public BirdCount(int[] birdsPerDay)
    {
        this.birdsPerDay = birdsPerDay;
    }

    public static int[] LastWeek()
    {
        return [0, 2, 5, 3, 7, 8, 4];
    }

    public int Today()
    {
        return birdsPerDay[^1];
    }

    public void IncrementTodaysCount()
    {
        birdsPerDay[^1]++;
    }
    
    public bool HasDayWithoutBirds()
    {
        return Array.Exists(birdsPerDay, birds => birds == 0);
    }

    public int CountForFirstDays(int numberOfDays)
    {
        // Opción 1.1: Bucle tradicional con control de límites dentro del bucle
        // int birdsCount = 0;
        // for (int i = 0; i < numberOfDays && i < birdsPerDay.Length; i++)
        // {
        //     birdsCount += birdsPerDay[i];
        // }
        // return birdsCount;
        
        // Opción 1.2: Bucle tradicional con control de límites antes del bucle
        // Nos quedamos con el número más chico entre lo que nos piden y lo que realmente tenemos
        // int limite = Math.Min(numberOfDays, birdsPerDay.Length); 
        // for (int i = 0; i < limite; i++)
        
        // Opción 2: Usar LINQ para una solución más elegante y segura
        // Usamos Take() para extraer una sub-lista con los primeros 'numberOfDays' elementos y Sum() para sumarlos todos.
        // Superpoder: A diferencia de un bucle 'for' tradicional, si le pedimos más días de los que existen en el arreglo, 
        // Take es seguro y no rompe el programa (evita el IndexOutOfRangeException); simplemente devuelve los que encuentre.
        return birdsPerDay.Take(numberOfDays).Sum();
    }

    public int BusyDays()
    {
        return birdsPerDay.Count(birds => birds >= 5);
    }
}

using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client;

class RemoteControlCar
{
    private int speed;
    private int batteryDrain;
    
    private int distanceDriven;
    private int battery = 100;

    public RemoteControlCar(int speed, int batteryDrain)
    {
        this.speed = speed;
        this.batteryDrain = batteryDrain;
    }

    public bool BatteryDrained() => battery < batteryDrain;

    public int DistanceDriven() => distanceDriven;

    public void Drive()
    {
        if (!BatteryDrained())
        {
            distanceDriven += speed;
            // battery = battery - batteryDrain > 0 ? battery - batteryDrain : 0;
            battery = Math.Max(0, battery - batteryDrain);
        }
    }

    public static RemoteControlCar Nitro() => new(50, 4);
}

class RaceTrack
{
    private int distance;
    
    public RaceTrack(int distance) => this.distance = distance;

    public bool TryFinishTrack(RemoteControlCar car)
    {
        // Opción 1: Simular la carrera respetando la encapsulación de la clase RemoteControlCar
        int initialDistanceDriven = car.DistanceDriven();
        int netDistanceDriven = 0;

        while (netDistanceDriven < distance)
        {
            if (car.BatteryDrained())
                return false;
            
            car.Drive();
            netDistanceDriven = car.DistanceDriven() - initialDistanceDriven;
        }
        return true;
        
        // Opción 2: Solución matemática (sin mover el auto) pero violando la encapsulación de la clase RemoteControlCar (para usarla deberíamos exponer estas variables convirtiendolas en Propiedades Públicas { get; })
        // ¿La distancia máxima que puede recorrer es mayor o igual a la de la pista?
        // return ((100 / batteryDrain) * speed) >= distance;
    }
}

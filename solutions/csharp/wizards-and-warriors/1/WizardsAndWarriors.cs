abstract class Character
{
    private readonly string _type;
    
    protected Character(string characterType)
    {
        _type = characterType;
    }

    public abstract int DamagePoints(Character target);

    public virtual bool Vulnerable() => false;

    // Sobrescribimos ToString() (heredado de Object) para definir qué texto devolverá el personaje si lo imprimimos directamente en la consola o en la interfaz gráfica.
    
    /* * SOLUCIÓN ÓPTIMA RECOMENDADA: GetType().Name
     * public override string ToString() => $"Character is a {GetType().Name}";
     * * ¿Por qué es mejor?
     * 1. GetType() (de la clase base Object) obtiene el tipo real de la instancia en tiempo de ejecución.
     * 2. .Name extrae el nombre limpio de la clase (ej. "Warrior" o "Wizard").
     * 3. Como 'Character' es abstracta, nunca devolverá "Character". Las clases derivadas
     * heredan este método y se auto-identifican mágicamente.
     * 4. Rendimiento y limpieza (Principio DRY): Elimina por completo la necesidad de declarar
     * la variable privada '_type' y de pasar strings hardcodeados a través de los constructores. */
    public override string ToString() => $"Character is a {_type}";
}

class Warrior : Character
{
    public Warrior() : base("Warrior")
    {
    }

    public override int DamagePoints(Character target) =>
        target.Vulnerable() ? 10 : 6;
}

class Wizard : Character
{
    private bool _preparedSpell;
    
    public Wizard() : base("Wizard")
    {
    }

    public override int DamagePoints(Character target) =>
        _preparedSpell ? 12 : 3;

    public void PrepareSpell() => _preparedSpell = true;
    
    public override bool Vulnerable() => !_preparedSpell;
}

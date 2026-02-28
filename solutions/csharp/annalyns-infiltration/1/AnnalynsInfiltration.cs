static class QuestLogic
{
    public static bool CanFastAttack(bool knightIsAwake) => 
        !knightIsAwake;

    public static bool CanSpy(bool knightIsAwake, bool archerIsAwake, bool prisonerIsAwake) =>
        knightIsAwake || archerIsAwake || prisonerIsAwake;

    public static bool CanSignalPrisoner(bool archerIsAwake, bool prisonerIsAwake) =>
        !archerIsAwake && prisonerIsAwake;

    public static bool CanFreePrisoner(bool knightIsAwake, bool archerIsAwake, bool prisonerIsAwake,
        bool petDogIsPresent) =>
        // (petDogIsPresent && !archerIsAwake) || (!petDogIsPresent && prisonerIsAwake && !knightIsAwake && !archerIsAwake);
        // Aplicando la ley distributiva y de absorción mejorando rendimiento y legibilidad:
        !archerIsAwake && (petDogIsPresent || (prisonerIsAwake && !knightIsAwake));
}

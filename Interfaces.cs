public interface IInteractable
{
    void Interact(PlayerMovement player);
    bool CanInteract(bool isPlayerInteraction);
    void StartHover();
    void StopHover();
}

public interface IAction
{
    void Cancel();
    void Resume();
}
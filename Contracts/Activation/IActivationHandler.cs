namespace MIM_Tool.Contracts.Activation;

public interface IActivationHandler
{
    bool CanHandle();

    Task HandleAsync();
}

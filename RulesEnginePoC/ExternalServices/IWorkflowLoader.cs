using RulesEngine.Models;

namespace ExternalServices;

public interface IWorkflowLoader
{
    public List<Workflow> GetWorkflows();
}
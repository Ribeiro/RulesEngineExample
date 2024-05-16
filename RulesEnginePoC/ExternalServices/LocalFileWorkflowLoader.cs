using Newtonsoft.Json;
using RulesEngine.Models;

namespace ExternalServices;

public class LocalFileWorkflowLoader : IWorkflowLoader
{
    public string Name { get; }
    public List<Workflow> Workflows { get; }
    public LocalFileWorkflowLoader(string name)
    {
        Name = name ?? throw new ArgumentException("Workflow name cannot be null!");
        Workflows = PopulateWorkflows(name);
    }
    public List<Workflow> GetWorkflows()
    {
        return Workflows;
    }

    private static List<Workflow> PopulateWorkflows(string name)
    {
        var files = Directory.GetFiles(Directory.GetCurrentDirectory(), name, SearchOption.AllDirectories);
        if (files == null || files.Length == 0)
        {
            throw new ArgumentException("Workflow Rules not found");
        }

        var fileData = File.ReadAllText(files[0]);

        return JsonConvert.DeserializeObject<List<Workflow>>(fileData) ?? throw new ArgumentException("fileData cannot be null");
    }
}
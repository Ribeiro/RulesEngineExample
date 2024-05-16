namespace RulesEnginePoC.Tests;

using ExternalServices;
using RulesEngine.Models;

public class DiscountRulesTes
{

private IWorkflowLoader _workflowLoaderService;
private List<Workflow> _workflows;

public DiscountRulesTes()
{
    Prepare();
}



public void Prepare()
{
    //_workflowLoaderService = new LocalFileWorkflowLoader("Discount.json");
    //_workflows = _workflowLoaderService.GetWorkflows();

    //HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
    //builder.Services.AddWorkflowServices(builder.Configuration);

    //using IHost host = builder.Build();
    //var workflowLoaderService = host.Services.CreateScope().ServiceProvider.GetRequiredService<IWorkflowLoader>();
    //var bre = new RulesEngine.RulesEngine(workflowLoaderService.GetWorkflows().ToArray(), null);
}


[Fact]
public void RulesEngine_ShouldOnlyExecuteEnabledRules()
{
    // Arrange
    //var bre = new RulesEngine.RulesEngine(_workflows.ToArray(), null);

    // Act
    //int result = a + b;

    // Assert
    Assert.Equal(1, 1);
}
}
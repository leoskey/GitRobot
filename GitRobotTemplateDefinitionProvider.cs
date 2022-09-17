using Microsoft.Extensions.FileProviders;
using Volo.Abp.TextTemplating;
using Volo.Abp.VirtualFileSystem;

namespace GitRobot;

public class GitRobotTemplateDefinitionProvider : TemplateDefinitionProvider
{
    private readonly IVirtualFileProvider _virtualFileProvider;

    public GitRobotTemplateDefinitionProvider(
        IVirtualFileProvider virtualFileProvider
    )
    {
        _virtualFileProvider = virtualFileProvider;
    }

    public override void Define(ITemplateDefinitionContext context)
    {
        var directoryContents = _virtualFileProvider.GetDirectoryContents("/Templates");
        foreach (var directoryContent in directoryContents)
        {
            var name = Path.GetFileNameWithoutExtension(directoryContent.Name);
            var virtualPath = directoryContent.GetVirtualOrPhysicalPathOrNull();
            context.Add(
                new TemplateDefinition(name)
                    .WithVirtualFilePath(virtualPath, true)
            );
        }
    }
}
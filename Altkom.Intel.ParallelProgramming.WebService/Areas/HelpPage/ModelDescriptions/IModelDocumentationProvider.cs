using System;
using System.Reflection;

namespace Altkom.Intel.ParallelProgramming.WebService.Areas.HelpPage.ModelDescriptions
{
    public interface IModelDocumentationProvider
    {
        string GetDocumentation(MemberInfo member);

        string GetDocumentation(Type type);
    }
}
﻿using Core;
using Scriban;

namespace Templating.Infra;

public class FileBuilder
{
    public FileBuilder()
    {
    }

    public void Build(ObjectBuilderContext builderMetadata)
    {
        var textTemplate = File.ReadAllText(builderMetadata.TextTemplateFilePath);

        var tpl = Template.Parse(textTemplate);

        var model = builderMetadata.Model;

        try
        {
            var res = tpl.Render(model);
            var fileName = $"{builderMetadata.FileName}.cs";
            var fileDirectory = builderMetadata.OutputFilePath;

            var fileLoader = new FileLoader();
            fileLoader.AddFileToProject(fileDirectory, fileName, res);

            Console.WriteLine(res);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
}
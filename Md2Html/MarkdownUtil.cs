using Markdig;
using Markdig.Extensions.AutoLinks;

namespace Md2Html
{
    static class MarkdownUtil
    {
        private static readonly MarkdownPipeline pipeline = new MarkdownPipelineBuilder()
            .UsePipeTables()
            .UseEmphasisExtras()
            .UseTaskLists()
            .UseDefinitionLists()
            .UseAutoLinks(new AutoLinkOptions { OpenInNewWindow = true })
            .Build();

        /// <summary>
        /// 渲染为html
        /// </summary>
        /// <param name="markdown"></param>
        /// <returns></returns>
        public static string RenderAsHtml(string markdown)
        {
            return Markdown.ToHtml(markdown, pipeline);
        }
    }
}

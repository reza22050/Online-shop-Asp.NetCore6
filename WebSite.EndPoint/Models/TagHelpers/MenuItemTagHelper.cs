using Application.Catalogs.GetMenuItem;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.Runtime.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace WebSite.EndPoint.Models.TagHelpers
{
    // You may need to install the Microsoft.AspNetCore.Razor.Runtime package into your project
    //[HtmlTargetElement("tag-name")]
    public class MenuItemTagHelper : TagHelper
    {
        private readonly IGetMenuItemService _getMenuItemService;

        public MenuItemTagHelper(IGetMenuItemService getMenuItemService)
        {
            _getMenuItemService = getMenuItemService;
        }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            //output.TagName = "ul";
            output.Content.AppendHtml(GetContent());
        }

        private TagBuilder GetContent()
        {
            var data = _getMenuItemService.Execute();
            TagBuilder ulMain = new TagBuilder("ul");

            int maxColumnCount = data.Where(x => x.ParentId == null).Count();

            foreach (var item in data.Where(x => x.ParentId == null).ToList())
            {
                if (item.SubMenu.Count() > 0)
                {
                    var liAll = CreateLi(item.Name, "#", "submenu");

                    var ulSub = new TagBuilder("ul");
                    ulSub.AddCssClass("category-mega-menu");
                    
                    var ulSubLi = CreateLi();
                    var ulSubLiDiv = CreateDiv("row");
                    for (var i = 0; i < 4; i++)
                    {
                        var sub1Div = CreateDiv("col-xl-3");

                        var sub1DivChild = CreateDiv("category-childmenu");
                        if (i < item.SubMenu.Count())
                        {
                            var categoryDiv = CreateDiv("title-category-item");
                            var cateroryTitle = CreateH6(item.SubMenu[i].Name);
                            categoryDiv.InnerHtml.AppendHtml(cateroryTitle);

                            var ulSub2 = CreateUl();
                            foreach (var sub2 in item.SubMenu[i].SubMenu.Take(maxColumnCount)) {
                                var sub2Title = CreateLi(sub2.Name, "#", null);
                                ulSub2.InnerHtml.AppendHtml(sub2Title);
                            }

                            sub1DivChild.InnerHtml.AppendHtml(categoryDiv);
                            sub1DivChild.InnerHtml.AppendHtml(ulSub2);
                        }

                        sub1Div.InnerHtml.AppendHtml(sub1DivChild);
                        ulSubLiDiv.InnerHtml.AppendHtml(sub1Div);
                    }
                    ulSubLi.InnerHtml.AppendHtml(ulSubLiDiv);

                    ulSub.InnerHtml.AppendHtml(ulSubLi);
                    liAll.InnerHtml.AppendHtml(ulSub);
                    ulMain.InnerHtml.AppendHtml(liAll);
                }
                else {
                    var liAll = CreateLi(item.Name,"#", null);
                    ulMain.InnerHtml.AppendHtml(liAll);
                }

            }

            return ulMain;
        }

        private TagBuilder CreateUl()
        {
            return new TagBuilder("ul");
        }

        private TagBuilder CreateLi()
        {
            TagBuilder li = new TagBuilder("li");
            return li;
        }

        private TagBuilder CreateLi(string Text, string Link, string? liClass)
        {
            var a = new TagBuilder("a");
            a.MergeAttribute("href", Link);
            a.MergeAttribute("title", Text);
            a.InnerHtml.Append(Text);
            var li = new TagBuilder("li");
            li.InnerHtml.AppendHtml(a);

            if (liClass != null)
            {
                li.AddCssClass(liClass);
            }
            return li;
        }

        private TagBuilder CreateDiv(string? className)
        {
            var div = new TagBuilder("div");
            if (className != null)
            {
                div.AddCssClass(className);    
            }
            return div;

        }

        private TagBuilder CreateH6(string title)
        {
            var h6 = new TagBuilder("h6");
            if (title != null)
            {
                h6.InnerHtml.Append(title);
            }
            return h6;
        }

    }
}
